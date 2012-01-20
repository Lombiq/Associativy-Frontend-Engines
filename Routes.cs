using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace Associativy.FrontendEngines
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class Routes : IRouteProvider
    {
        public Routes(RouteCollection routeCollection)
        {
            // This is to prohibit direct access to frontend engines with unpredictable results
            routeCollection.Ignore(
                "Associativy.FrontendEngines/{frontendEngineName}FrontendEngine/{action}"
            );

            routeCollection.Ignore(
                "Associativy.FrontendEngines/Json/{action}"
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