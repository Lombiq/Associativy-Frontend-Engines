using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Associativy.Frontends.EngineDiscovery;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends
{
    public class Routes : IRouteProvider
    {
        private readonly RouteCollection _routeCollection;
        private readonly IEngineManager _engineManager;


        public Routes(
            RouteCollection routeCollection,
            IEngineManager engineManager)
        {
            _routeCollection = routeCollection;
            _engineManager = engineManager;
        }


        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            // This is to prohibit direct access to frontend engines with unpredictable results
            _routeCollection.Ignore(
                "Associativy.Frontends/{frontendEngineName}Engine/{action}"
            );

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