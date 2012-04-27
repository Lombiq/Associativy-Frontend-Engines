using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.EngineDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineManager : IEngineManager
    {
        private readonly IEnumerable<IEngineProvider> _engineProviders;

        private IEnumerable<EngineDescriptor> _descriptors;
        private IEnumerable<EngineDescriptor> Descriptors
        {
            get
            {
                if (_descriptors == null)
                {
                    _descriptors = Enumerable.Empty<EngineDescriptor>();
                    var describeContext = new DescribeContext();
                    foreach (var provider in _engineProviders)
                    {
                        provider.Describe(describeContext);
                    }
                    _descriptors = describeContext.Descriptors;
                }

                return _descriptors;
            }
        }


        public EngineManager(IEnumerable<IEngineProvider> engineProviders)
        {
            _engineProviders = engineProviders;
        }

        public IEnumerable<EngineDescriptor> GetEngines()
        {
            return Descriptors;
        }
    }
}