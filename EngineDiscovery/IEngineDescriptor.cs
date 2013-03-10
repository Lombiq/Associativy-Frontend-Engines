using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
    public interface IEngineDescriptor
    {
        string EngineName { get; }
        LocalizedString DisplayEngineName { get; }
        RouteDescriptor Route { get; }
    }
}
