using System.Collections.Generic;
using System.Web.Mvc;
using Associativy.Frontends.Controllers;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using QuickGraph;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Associativy.Frontends.Models;
using Piedone.HelpfulLibraries.Serialization;
using Associativy.GraphDiscovery;
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Engines.JIT.Controllers
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class JITEngineController : EngineControllerBase<IJITConfigurationProvider>
    {
        protected override IEngineContext EngineContext
        {
            get { return DefaultJITConfigurationProvider.DescribedEngineContext; }
        }

        public JITEngineController(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IEngineConfigurationManager configurationManager)
            : base(associativyServices, orchardServices, configurationManager)
        {
        }

        public virtual JsonResult FetchAssociations(int zoomLevel = 0)
        {
            var page = NewPage("FetchAssociations");

            _contentManager.UpdateEditor(page, this);

            var settings = ConfigurationProvider.MakeDefaultMindSettings();
            settings.ZoomLevel = zoomLevel;

            IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph;
            if (ModelState.IsValid)
            {
                graph = RetrieveSearchedGraph(page, settings);
            }
            else
            {
                graph = _mind.GetAllAssociations(GraphContext, settings);
            }

            var viewNodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

            foreach (var vertex in graph.Vertices)
            {
                // Setting the ContentItem causes "A circular reference was detected while serializing an object of type 'Orchard.ContentManagement.Records.ContentItemRecord'."
                viewNodes[vertex.Id] = ConfigurationProvider.ViewModelSetup(vertex, new NodeViewModel());
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