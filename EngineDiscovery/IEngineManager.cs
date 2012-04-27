using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;

namespace Associativy.Frontends.EngineDiscovery
{
    public interface IEngineManager : IDependency
    {
        IEnumerable<EngineDescriptor> GetEngines();
    }
}
