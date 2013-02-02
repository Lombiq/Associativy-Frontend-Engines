using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineDescriptor
    {
        public string EngineName { get; private set; }
        public LocalizedString DisplayEngineName { get; private set; }
        public RouteDescriptor Route { get; private set; }

        public EngineDescriptor(string engineName, LocalizedString displayName, RouteDescriptor route)
        {
            EngineName = engineName;
            DisplayEngineName = displayName;
            Route = route;
        }
    }
}