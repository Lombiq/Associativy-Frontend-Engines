using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public class Routes : IRouteProvider
    {
        public Routes(RouteCollection routeCollection)
        {
            // This is to prohibit direct access to frontend engines with unpredictable results
            routeCollection.Ignore(
                "Associativy.Frontends/{frontendEngineName}Frontend/{action}"
            );
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new List<RouteDescriptor>();
        }
    }
}