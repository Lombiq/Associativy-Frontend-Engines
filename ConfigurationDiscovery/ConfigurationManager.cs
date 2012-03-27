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
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IEnumerable<IConfigurationProvider> _configurationProviders;
        private readonly IDescriptorFilterer _providerFilterer;
        private Dictionary<string, List<ConfigurationDescriptor>> _descriptors;

        public ConfigurationManager(
            IEnumerable<IConfigurationProvider> configurationProviders,
            IDescriptorFilterer providerFilterer)
        {
            _configurationProviders = configurationProviders;
            _providerFilterer = providerFilterer;
            _descriptors = new Dictionary<string, List<ConfigurationDescriptor>>();
        }

        public TConfigurationDescriptor FindConfiguration<TConfigurationDescriptor>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationDescriptor : ConfigurationDescriptor, new()
        {
            return FindConfigurations<TConfigurationDescriptor>(engineContext, graphContext).LastOrDefault();
        }

        public IEnumerable<TConfigurationDescriptor> FindConfigurations<TConfigurationDescriptor>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationDescriptor : ConfigurationDescriptor, new()
        {
            return _providerFilterer.FilterByMatchingGraphContext(ProduceDescriptors<TConfigurationDescriptor>(), graphContext);
        }

        private IEnumerable<TConfigurationDescriptor> ProduceDescriptors<TConfigurationDescriptor>()
            where TConfigurationDescriptor : ConfigurationDescriptor, new()
        {
            var typeName = typeof(TConfigurationDescriptor).FullName;
            if (!_descriptors.ContainsKey(typeName))
            {
                _descriptors[typeName] = new List<ConfigurationDescriptor>();

                var providerType = typeof(IEngineConfigurationProvider<TConfigurationDescriptor>);

                foreach (var provider in _configurationProviders.Where(provider => providerType.IsAssignableFrom(provider.GetType())))
                {
                    var descriptor = new TConfigurationDescriptor();
                    ((IEngineConfigurationProvider<TConfigurationDescriptor>)provider).Describe(descriptor);
                    descriptor.Freeze();
                    _descriptors[typeName].Add(descriptor);
                }
            }

            return _descriptors[typeName].Cast<TConfigurationDescriptor>();
        }
    }
}