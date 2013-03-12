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
