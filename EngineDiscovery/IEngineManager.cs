using System.Collections.Generic;
using Orchard;

namespace Associativy.Frontends.EngineDiscovery
{
    public interface IEngineManager : IDependency
    {
        IEnumerable<EngineDescriptor> GetEngines();
    }
}
