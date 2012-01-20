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
    public abstract class FrontendsRoutesBase : IRouteProvider
    {
        private readonly List<RouteDescriptor> _routes = new List<RouteDescriptor>();
        abstract protected string ModuleName { get; }

        protected void AddFrontendEngineRoute(IGraphContext graphContext, string relativeUrl, string frontendEngine)
        {
            AddRoute(
                ModuleName + " " + frontendEngine,
                graphContext,
                new Route(
                        ModuleName + "/" + relativeUrl,
                        new RouteValueDictionary {
                                                                {"area", "Associativy.Frontends"},
                                                                {"controller", frontendEngine + "FrontendEngine"},
                                                                {"action", "Index"}
                                                            },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                                {"area", "Associativy.Frontends"},
                                                            },
                        new MvcRouteHandler()));
        }

        protected void AddRoute(string name, IGraphContext graphContext, Route route)
        {
            route.DataTokens["GraphContext"] = graphContext;

            _routes.Add(new RouteDescriptor
                {
                    Name = name,
                    Route = route
                });
        }

        public virtual void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public virtual IEnumerable<RouteDescriptor> GetRoutes()
        {
            return _routes;
        }
    }
}