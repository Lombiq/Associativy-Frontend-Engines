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
using Associativy.Frontends.Shapes;
using Associativy.Frontends.Engines.JIT.Models;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Associativy.Frontends.Models;

namespace Associativy.Frontends.Engines.JIT.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public class JITFrontendController : FrontendControllerBase
    {
        protected readonly IJITSetup _setup;

        protected override IFrontendEngineContext FrontendEngineContext
        {
            get { return new JITContext(); }
        }

        public JITFrontendController(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IFrontendShapes frontendShapes,
            IShapeFactory shapeFactory,
            IJITSetup setup)
            : base(associativyServices, orchardServices, frontendShapes, shapeFactory, setup)
        {
            _setup = setup;
        }

        protected override dynamic GraphShape(IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph)
        {
            return GraphShape(new GraphViewModel() { Graph = graph, MaxZoomLevel = _associativyServices.GraphService.CalculateZoomLevelCount(graph, _setup.MaxZoomLevel) - 1 });
        }

        public virtual JsonResult FetchAssociations(int zoomLevel = 0)
        {
            var searchForm = _contentManager.New(FrontendEngineContext.SearchFormContentType);
            _contentManager.UpdateEditor(searchForm, this);

            var settings = MakeDefaultMindSettings();
            settings.ZoomLevel = zoomLevel;

            IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph;
            if (ModelState.IsValid)
            {
                if (!TryGetGraph(searchForm, out graph, settings))
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                graph = _mind.GetAllAssociations(GraphContext, settings);
            }


            var viewNodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

            foreach (var vertex in graph.Vertices)
            {
                // Setting the ContentItem causes "A circular reference was detected while serializing an object of type 'Orchard.ContentManagement.Records.ContentItemRecord'."
                viewNodes[vertex.Id] = _setup.SetViewModel(vertex, new NodeViewModel());
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