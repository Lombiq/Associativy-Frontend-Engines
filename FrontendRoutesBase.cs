using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace Associativy.FrontendEngines
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public abstract class FrontendRoutesBase : IRouteProvider
    {
        public string FrontendEngine { get; set; }
        abstract public string ModuleName { get; }

        public FrontendRoutesBase()
        {
            FrontendEngine = "JIT";
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor
                {
                    Name = ModuleName + " Associations",
                    Route = new Route(
                        ModuleName +"/Associations/{action}",
                        new RouteValueDictionary {
                                                    {"area", "Associativy.FrontendEngines"},
                                                    {"controller", FrontendEngine + "FrontendEngine"},
                                                    {"action", "Index"}
                                                },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                    {"area", "Associativy.FrontendEngines"}
                                                },
                        new MvcRouteHandler())
                }
            };
        }
    }
}