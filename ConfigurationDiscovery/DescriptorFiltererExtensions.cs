using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public static class DescriptorFiltererExtensions
    {
        public static IEnumerable<TConfigurationDescriptor> FilterByMatchingGraphContext<TConfigurationDescriptor>(
            this IDescriptorFilterer providerFilterer, 
            IEnumerable<TConfigurationDescriptor> descriptors, 
            IGraphContext graphContext)
            where TConfigurationDescriptor : EngineConfigurationDescriptor
        {
            return providerFilterer.FilterByMatchingGraphContext(descriptors, graphContext, (descriptor) => descriptor.GraphContext.GraphName, (provider) => provider.GraphContext.ContentTypes);
        }
    }
}