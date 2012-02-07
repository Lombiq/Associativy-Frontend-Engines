using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;
using Associativy.Models;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class FrontendsRoutesProviderBase : IRouteProvider
    {
        protected readonly List<RouteDescriptor> _routes = new List<RouteDescriptor>();

        protected void RegisterEngineRoute(string url, string frontendEngine, IGraphContext graphContext, string engineModule = "Associativy.Frontends")
        {
            RegisterRoute(
                graphContext.Stringify() + " " + frontendEngine,
                new Route(
                        url,
                        new RouteValueDictionary {
                                                                {"area", engineModule},
                                                                {"controller", frontendEngine + "Engine"},
                                                                {"action", "Index"}
                                                            },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                                {"area", engineModule},
                                                            },
                        new MvcRouteHandler()),
                 graphContext);
        }

        protected void RegisterRoute(string name, Route route, IGraphContext graphContext)
        {
            route.DataTokens["GraphContext"] = graphContext;
            route.DataTokens["RouteName"] = name;

            _routes.Add(new RouteDescriptor
            {
                Name = name,
                Route = route
            });
        }

        protected void RegisterRoute(Route route, IGraphContext graphContext)
        {
            RegisterRoute(graphContext.Stringify() + route.Url, route, graphContext);
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return _routes;
        }
    }
}