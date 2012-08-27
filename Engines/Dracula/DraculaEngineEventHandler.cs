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
    public class DraculaEngineEventHandler : IAssociativyFrontendEngineEventHandler
    {
        private readonly IDraculaConfigurationHandler _configurationHandler;

        public DraculaEngineEventHandler(IDraculaConfigurationHandler configurationHandler)
        {
            _configurationHandler = configurationHandler;
        }

        public void OnPageInitializing(FrontendEventContext frontendEventContext)
        {
            if (frontendEventContext.EngineContext.EngineName != "Dracula") return;

            var draculaPart = new DraculaPart();
            draculaPart.NodesField.Loader(() =>
            {
                var graph = draculaPart.As<IGraphAspect>().Graph;

                var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

                foreach (var node in graph.Vertices)
                {
                    var viewModel = new NodeViewModel { ContentItem = node };
                    _configurationHandler.SetupViewModel(frontendEventContext, node, viewModel);
                    nodes[node.Id] = viewModel;
                }

                return nodes;
            });
            frontendEventContext.Page.ContentItem.Weld(draculaPart);
        }

        public void OnPageInitialized(FrontendEventContext frontendEventContext)
        {
        }

        public void OnPageBuilt(FrontendEventContext frontendEventContext)
        {
        }

        public void OnAuthorization(FrontendAuthorizationEventContext frontendAuthorizationEventContext)
        {
        }
    }
}