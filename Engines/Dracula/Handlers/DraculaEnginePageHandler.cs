using System.Collections.Generic;
using Associativy.Frontends.Engines.Dracula.Models;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.Frontends.EventHandlers;
using Associativy.Frontends.Models;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Piedone.HelpfulLibraries.Contents;

namespace Associativy.Frontends.Engines.Dracula.Handlers
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaEnginePageHandler : ContentHandler
    {
        private readonly IDraculaConfigurationHandler _configurationHandler;


        public DraculaEnginePageHandler(IDraculaConfigurationHandler configurationHandler)
        {
            _configurationHandler = configurationHandler;
        }


        protected override void Initializing(InitializingContentContext context)
        {
            var page = context.ContentItem;

            if (page.PageGroup() != FrontendsPageConfigs.Group) return;

            if (page.As<IEngineConfigurationAspect>().EngineContext.EngineName != "Dracula") return;

            page.Weld<DraculaPart>(part =>
                part.NodesField.Loader(() =>
                {
                    var graph = part.As<IGraphAspect>().Graph;

                    var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

                    foreach (var node in graph.Vertices)
                    {
                        var viewModel = new NodeViewModel { ContentItem = node };
                        var configAspect = page.As<IEngineConfigurationAspect>();
                        _configurationHandler.SetupViewModel(new FrontendContext(configAspect.EngineContext, configAspect.GraphDescriptor.MaximalContext()), node, viewModel);
                        nodes[node.ContentItem.Id] = viewModel;
                    }

                    return nodes;
                }));
        }
    }
}