using System.Collections.Generic;
using Associativy.Frontends.Engines.Dracula.Models;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.Frontends.EventHandlers;
using Associativy.Frontends.Models;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

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

        public void OnPageInitializing(IContent page)
        {
            if (page.As<IEngineConfigurationAspect>().EngineContext.EngineName != "Dracula") return;

            var draculaPart = new DraculaPart();
            draculaPart.NodesField.Loader(() =>
            {
                var graph = draculaPart.As<IGraphAspect>().Graph;

                var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

                foreach (var node in graph.Vertices)
                {
                    var viewModel = new NodeViewModel { ContentItem = node };
                    var configAspect = page.As<IEngineConfigurationAspect>();
                    _configurationHandler.SetupViewModel(new FrontendContext(configAspect.EngineContext, configAspect.GraphContext), node, viewModel);
                    nodes[node.Id] = viewModel;
                }

                return nodes;
            });
            page.ContentItem.Weld(draculaPart);
        }

        public void OnPageInitialized(IContent page)
        {
        }

        public void OnPageBuilt(IContent page)
        {
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
        }
    }
}