using System.Collections.Generic;
using System.Web.Mvc;
using Associativy.Frontends.Controllers;
using Associativy.Frontends.Engines.Jit.ViewModels;
using Associativy.Frontends.EventHandlers;
using Associativy.Frontends.Models;
using Associativy.Frontends.Services;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Jit.Controllers
{
    [OrchardFeature("Associativy.Frontends.Jit")]
    public class JitEngineController : EngineControllerBase
    {
        private readonly IJitConfigurationHandler _configurationHandler;

        private readonly IEngineContext _engineContext = new EngineContext { EngineName = "Jit" };
        protected override IEngineContext EngineContext
        {
            get { return _engineContext; }
        }

        public JitEngineController(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IAssociativyFrontendEngineEventHandler eventHandler,
            IOrchardServices orchardServices,
            IJitConfigurationHandler configurationHandler)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
            _configurationHandler = configurationHandler;
        }

        [HttpPost]
        public virtual ActionResult FetchGraph(int zoomLevel = 0)
        {
            var page = NewPage("FetchGraph");

            if (!IsAuthorized(page))
            {
                return new HttpUnauthorizedResult();
            }

            var mindSettings = page.As<IEngineConfigurationAspect>().MindSettings;
            mindSettings.ZoomLevel = zoomLevel;

            _contentManager.UpdateEditor(page, this);

            var graph = page.As<IGraphRetrieverAspect>().RetrieveGraph(mindSettings);
            var viewNodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

            foreach (var vertex in graph.Vertices)
            {
                var viewModel = new NodeViewModel { ContentItem = vertex };
                _configurationHandler.SetupViewModel(new FrontendContext(EngineContext, GraphContext), vertex, viewModel);
                viewNodes[vertex.Id] = viewModel;
            }

            foreach (var edge in graph.Edges)
            {
                viewNodes[edge.Source.Id].adjacencies.Add(edge.Target.Id.ToString());
                viewNodes[edge.Target.Id].adjacencies.Add(edge.Source.Id.ToString());
            }

            return Json(viewNodes.Values);
        }
    }
}