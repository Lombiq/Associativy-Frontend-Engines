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
            this IDescriptorFilterer descriptorFilterer, 
            IEnumerable<TConfigurationDescriptor> descriptors, 
            IGraphContext graphContext)
            where TConfigurationDescriptor : ConfigurationDescriptor
        {
            return descriptorFilterer.FilterByMatchingGraphContext(descriptors, graphContext, (descriptor) => descriptor.GraphContext.GraphName, (provider) => provider.GraphContext.ContentTypes);
        }
    }
}