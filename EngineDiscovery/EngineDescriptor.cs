using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;
using Orchard.Localization;

namespace Associativy.Frontends.EngineDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineDescriptor
    {
        public string EngineName { get; private set; }

        private readonly DisplayNameGetter _displayNameGetter;
        public LocalizedString DisplayEngineName
        {
            get
            {
                return _displayNameGetter();
            }
        }

        public RouteDescriptor Route { get; private set; }

        public EngineDescriptor(string engineName, DisplayNameGetter displayNameGetter, RouteDescriptor route)
        {
            EngineName = engineName;
            _displayNameGetter = displayNameGetter;
            Route = route;
        }
    }
}