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
        private readonly IDescriptorFilterer _providerFilterer;
        private Dictionary<string, List<EngineConfigurationDescriptor>> _descriptors;

        public EngineConfigurationManager(
            IEnumerable<IEngineConfigurationProvider> registeredProviders,
            IDescriptorFilterer providerFilterer)
        {
            _registeredProviders = registeredProviders;
            _providerFilterer = providerFilterer;
            _descriptors = new Dictionary<string, List<EngineConfigurationDescriptor>>();
        }

        public TConfigurationDescriptor FindConfiguration<TConfigurationDescriptor>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationDescriptor : EngineConfigurationDescriptor, new()
        {
            return FindConfigurations<TConfigurationDescriptor>(engineContext, graphContext).LastOrDefault();
        }

        public IEnumerable<TConfigurationDescriptor> FindConfigurations<TConfigurationDescriptor>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationDescriptor : EngineConfigurationDescriptor, new()
        {
            return _providerFilterer.FilterByMatchingGraphContext(ProduceDescriptors<TConfigurationDescriptor>(), graphContext);
        }

        private IEnumerable<TConfigurationDescriptor> ProduceDescriptors<TConfigurationDescriptor>()
            where TConfigurationDescriptor : EngineConfigurationDescriptor, new()
        {
            var typeName = typeof(TConfigurationDescriptor).FullName;
            if (!_descriptors.ContainsKey(typeName))
            {
                _descriptors[typeName] = new List<EngineConfigurationDescriptor>();

                var providerType = typeof(IEngineConfigurationProvider<TConfigurationDescriptor>);

                foreach (var provider in _registeredProviders.Where(provider => providerType.IsAssignableFrom(provider.GetType())))
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