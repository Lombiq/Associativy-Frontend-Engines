using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;
using Associativy.Frontends.EngineDiscovery;
using System.Linq;

namespace Associativy.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public class Routes : IRouteProvider
    {
        private readonly IEngineManager _engineManager;

        public Routes(
            RouteCollection routeCollection,
            IEngineManager engineManager)
        {
            // This is to prohibit direct access to frontend engines with unpredictable results
            routeCollection.Ignore(
                "Associativy.Frontends/{frontendEngineName}Engine/{action}"
            );

            _engineManager = engineManager;
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            var routes = new[]
            {
                new RouteDescriptor
                {
                    Name = "Associativy.Frontends.AutoComplete.FetchSimilarLabels",
                    Route = new Route(
                        "Associativy.Frontends/AutoComplete/FetchSimilarLabels",
                        new RouteValueDictionary {
                                                    {"area", "Associativy.Frontends"},
                                                    {"controller", "AutoComplete"},
                                                    {"action", "FetchSimilarLabels"}
                                                },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                    {"area", "Associativy.Frontends"}
                                                },
                        new MvcRouteHandler())
                }
            };

            return routes.Union(_engineManager.GetEngines().Select(engine => engine.Route));
        }
    }
}