using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement.Handlers;
using Associativy.Frontends.Models;
using Associativy.Services;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.Models;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Engines.Dracula.Handlers
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaHandler : EngineHandlerBase
    {
        public DraculaHandler(IAssociativyServices associativyServices)
            : base(associativyServices)
        {
            var engineContext = DefaultDraculaConfigurationProvider.DescribedEngineContext;

            AddCommonPartsToBasicContentTypes(engineContext);
            AddPartToBasicContentTypes<DraculaPart>(engineContext);

            OnActivated<DraculaPart>((context, part) =>
            {
                part.NodesField.Loader(() =>
                    {
                        var graph = part.As<GraphPart>().Graph;
                        var configurationDescriptor = (DraculaConfigurationDescriptor)part.As<EngineCommonPart>().ConfigurationDescriptor;

                        var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

                        foreach (var node in graph.Vertices)
                        {
                            var viewModel = new NodeViewModel { ContentItem = node };
                            configurationDescriptor.ViewModelSetup(node, viewModel);
                            nodes[node.Id] = viewModel;
                        }

                        return nodes;
                    });
            });
        }
    }
}