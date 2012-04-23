using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Associativy.GraphDiscovery;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class FrontendsRoutesProviderBase : IRouteProvider
    {
        protected readonly List<RouteDescriptor> _routes = new List<RouteDescriptor>();

        protected void RegisterDefaultEngineRoute(string instanceModule, string frontendEngine, IGraphContext graphContext, string engineModule = "Associativy.Frontends")
        {
            RegisterEngineRoute(instanceModule + "/" + frontendEngine + "Engine/{action}", instanceModule, frontendEngine, graphContext, engineModule);
        }

        protected void RegisterAnyEngineRoute(string instanceModule, string frontendEngine, IGraphContext graphContext, string engineModule = "Associativy.Frontends")
        {
            RegisterEngineRoute(instanceModule + "/{controller}/{action}", instanceModule, frontendEngine, graphContext, engineModule);
        }

        protected void RegisterEngineRoute(string url, string instanceModule, string frontendEngine, IGraphContext graphContext, string engineModule = "Associativy.Frontends")
        {
            RegisterRoute(
                graphContext.Stringify() + " " + frontendEngine,
                new Route(
                        url,
                        new RouteValueDictionary {
                                                                {"area", instanceModule},
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