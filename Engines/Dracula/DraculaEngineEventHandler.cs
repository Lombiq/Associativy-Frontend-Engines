using System.Collections.Generic;
using Associativy.Frontends.Engines.Dracula.Models;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.Frontends.EventHandlers;
using Associativy.Frontends.Models;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaEngineEventHandler : IFrontendEngineEventHandler
    {
        private readonly IDraculaConfigurationHandler _configurationHandler;

        public DraculaEngineEventHandler(IDraculaConfigurationHandler configurationHandler)
        {
            _configurationHandler = configurationHandler;
        }

        public void OnPageInitializing(FrontendContext frontendContext, IContent page)
        {
            var draculaPart = new DraculaPart();
            draculaPart.NodesField.Loader(() =>
            {
                var graph = draculaPart.As<IGraphAspect>().Graph;

                var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

                foreach (var node in graph.Vertices)
                {
                    var viewModel = new NodeViewModel { ContentItem = node };
                    _configurationHandler.SetupViewModel(frontendContext, node, viewModel);
                    nodes[node.Id] = viewModel;
                }

                return nodes;
            });
            page.ContentItem.Weld(draculaPart);
        }

        public void OnPageInitialized(FrontendContext frontendContext, IContent page)
        {
        }

        public void OnPageBuilt(FrontendContext frontendContext, IContent page)
        {
        }
    }
}