using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public static class ProviderFiltererExtensions
    {
        public static IEnumerable<TConfigurationProvider> FilterByMatchingGraphContext<TConfigurationProvider>(
            this IProviderFilterer providerFilterer, 
            IEnumerable<TConfigurationProvider> providers, 
            IGraphContext graphContext)
            where TConfigurationProvider : IEngineConfigurationProvider
        {
            return providerFilterer.FilterByMatchingGraphContext(providers, graphContext, (provider) => provider.GraphContext.GraphName, (provider) => provider.GraphContext.ContentTypes);
        }
    }
}