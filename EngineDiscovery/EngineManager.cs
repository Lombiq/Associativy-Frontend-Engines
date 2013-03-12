using System.Collections.Generic;
using System.Linq;

namespace Associativy.Frontends.EngineDiscovery
{
    public class EngineManager : IEngineManager
    {
        private readonly IEnumerable<IEngineProvider> _engineProviders;

        private IEnumerable<IEngineDescriptor> _descriptors;
        private IEnumerable<IEngineDescriptor> Descriptors
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


        public IEnumerable<IEngineDescriptor> GetEngines()
        {
            return Descriptors;
        }
    }
}