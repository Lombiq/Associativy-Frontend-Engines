using Associativy.Frontends.Controllers;
using Associativy.Services;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Shapes;
using Associativy.Frontends.Engines.Dracula.Models;
using QuickGraph;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using System.Collections.Generic;
using Associativy.Frontends.Models;

namespace Associativy.Frontends.Engines.Dracula.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public class DraculaFrontendController : FrontendControllerBase
    {
        protected readonly IDraculaSetup _setup;

        protected override IFrontendEngineContext FrontendEngineContext
        {
            get { return new DraculaContext(); }
        }

        public DraculaFrontendController(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IFrontendShapes frontendShapes,
            IShapeFactory shapeFactory,
            IDraculaSetup setup)
            : base(associativyServices, orchardServices, frontendShapes, shapeFactory, setup)
        {
            _setup = setup;
        }

        protected override dynamic GraphShape(IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph)
        {
            var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

            foreach (var node in graph.Vertices)
            {
                nodes[node.Id] = _setup.SetViewModel(node, new NodeViewModel() { ContentItem = node });
            }

            return GraphShape(new GraphViewModel() { Graph = graph, Nodes = nodes });
        }
    }
}