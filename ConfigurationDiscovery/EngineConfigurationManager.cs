using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;
using Associativy.Frontends.Engines;
using System.Diagnostics;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineConfigurationManager : IEngineConfigurationManager
    {
        private readonly IEnumerable<IEngineConfigurationProvider> _registeredProviders;
        private readonly IProviderFilterer _providerFilterer;

        public EngineConfigurationManager(
            IEnumerable<IEngineConfigurationProvider> registeredProviders,
            IProviderFilterer providerFilterer)
        {
            _registeredProviders = registeredProviders;
            _providerFilterer = providerFilterer;
        }

        public TConfigurationProvider FindLastProvider<TConfigurationProvider>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationProvider : IEngineConfigurationProvider
        {
            return FindProviders<TConfigurationProvider>(engineContext, graphContext).LastOrDefault();
        }

        public IEnumerable<TConfigurationProvider> FindProviders<TConfigurationProvider>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationProvider : IEngineConfigurationProvider
        {
            // That's very fast (~50 ticks), so there's no point in caching anything.
            // If it gets heavy, could be stored in an instance cache.
            var providerList = new List<TConfigurationProvider>();
            foreach (var provider in _registeredProviders.Where(provider => typeof(TConfigurationProvider).IsAssignableFrom(provider.GetType())))
            {
                providerList.Add((TConfigurationProvider)provider);
            }

            return _providerFilterer.FilterByMatchingGraphContext(providerList.AsEnumerable(), graphContext);
        }
    }
}