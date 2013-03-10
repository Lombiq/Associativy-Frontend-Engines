using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineDescriptor : IEngineDescriptor
    {
        public string EngineName { get; private set; }

        private readonly DisplayNameGetter _displayNameGetter;
        public LocalizedString DisplayEngineName { get { return _displayNameGetter(); } }

        public RouteDescriptor Route { get; private set; }


        public EngineDescriptor(string engineName, DisplayNameGetter displayNameGetter, RouteDescriptor route)
        {
            EngineName = engineName;
            _displayNameGetter = displayNameGetter;
            Route = route;
        }
    }
}