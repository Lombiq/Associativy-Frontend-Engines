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
                        var configurationProvider = (IDraculaConfigurationProvider)part.As<EngineCommonPart>().ConfigurationProvider;

                        var nodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

                        foreach (var node in graph.Vertices)
                        {
                            nodes[node.Id] = configurationProvider.ViewModelSetup(node, new NodeViewModel() { ContentItem = node });
                        }

                        return nodes;
                    });
            });
        }
    }
}