using Associativy.FrontendEngines.Controllers;
using Associativy.Services;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Shapes;
using Associativy.FrontendEngines.Engines.Dracula.Models;
using QuickGraph;
using Orchard.ContentManagement;
using Associativy.FrontendEngines.Engines.Dracula.ViewModels;
using System.Collections.Generic;
using Associativy.FrontendEngines.Models;

namespace Associativy.FrontendEngines.Engines.Dracula.Controllers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class DraculaFrontendEngineController : FrontendEngineControllerBase
    {
        protected readonly IDraculaSetup _setup;

        protected override IFrontendEngineContext FrontendEngineContext
        {
            get { return new DraculaContext(); }
        }

        public DraculaFrontendEngineController(
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