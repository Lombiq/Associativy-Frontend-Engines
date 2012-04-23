using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.EventHandlers;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.Models;
using Associativy.Frontends.Models;
using Associativy.Frontends.Engines.Dracula.ViewModels;
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

        public void OnPageInitializing(IEngineContext engineContext, IGraphContext graphContext, IContent page)
        {
            var draculaPart = new DraculaPart();
            draculaPart.NodesField.Loader(() =>
            {
                var graph = draculaPart.As<IGraphAspect>().Graph;

                var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

                foreach (var node in graph.Vertices)
                {
                    var viewModel = new NodeViewModel { ContentItem = node };
                    _configurationHandler.SetupViewModel(engineContext, graphContext, node, viewModel);
                    nodes[node.Id] = viewModel;
                }

                return nodes;
            });
            page.ContentItem.Weld(draculaPart);
        }

        public void OnPageInitialized(IEngineContext engineContext, IGraphContext graphContext, IContent page)
        {
        }

        public void OnPageBuilt(IEngineContext engineContext, IGraphContext graphContext, IContent page)
        {
        }
    }
}