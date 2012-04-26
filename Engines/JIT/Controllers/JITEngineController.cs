using System.Collections.Generic;
using System.Web.Mvc;
using Associativy.Frontends.Controllers;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Associativy.Frontends.EventHandlers;
using Associativy.Frontends.Models;
using Associativy.Frontends.Services;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using QuickGraph;

namespace Associativy.Frontends.Engines.JIT.Controllers
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class JITEngineController : EngineControllerBase
    {
        private readonly IJITConfigurationHandler _configurationHandler;

        private readonly IEngineContext _engineContext = new EngineContext { EngineName = "JIT" };
        protected override IEngineContext EngineContext
        {
            get { return _engineContext; }
        }

        public JITEngineController(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IFrontendEngineEventHandler eventHandler,
            IOrchardServices orchardServices,
            IJITConfigurationHandler configurationHandler)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
            _configurationHandler = configurationHandler;
        }

        public virtual JsonResult FetchAssociations(int zoomLevel = 0)
        {
            var page = NewPage("FetchAssociations");

            var mindSettings = page.As<IEngineConfigurationAspect>().MindSettings;
            mindSettings.ZoomLevel = zoomLevel;

            _contentManager.UpdateEditor(page, this);

            IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph;

            if (ModelState.IsValid)
            {
                graph = page.As<IGraphRetrieverAspect>().RetrieveGraph(mindSettings);
            }
            else
            {
                graph = _mind.GetAllAssociations(GraphContext, mindSettings);
            }

            var viewNodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

            foreach (var vertex in graph.Vertices)
            {
                // Setting the ContentItem causes "A circular reference was detected while serializing an object of type 'Orchard.ContentManagement.Records.ContentItemRecord'."
                var viewModel = new NodeViewModel { id = vertex.Id.ToString() };
                _configurationHandler.SetupViewModel(FrontendContext, vertex, viewModel);
                viewNodes[vertex.Id] = viewModel;
            }

            foreach (var edge in graph.Edges)
            {
                viewNodes[edge.Source.Id].adjacencies.Add(edge.Target.Id.ToString());
                viewNodes[edge.Target.Id].adjacencies.Add(edge.Source.Id.ToString());
            }

            return Json(viewNodes.Values, JsonRequestBehavior.AllowGet);
        }
    }
}