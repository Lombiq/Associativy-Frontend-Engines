using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
    public abstract class EngineProviderBase : IEngineProvider
    {
        public Localizer T { get; set; }


        public EngineProviderBase()
        {
            T = NullLocalizer.Instance;
        }


        public abstract void Describe(DescribeContext describeContext);

        protected RouteDescriptor DefaultRoute(string engineName, string area = "Associativy.Frontends")
        {
            return new RouteDescriptor
                {
                    Route = new Route(
                        "Associativy/Graphs/{GraphName}/" + engineName + "Engine/{action}",
                        new RouteValueDictionary {
                                                    {"area", area},
                                                    {"controller", engineName + "Engine"},
                                                    {"action", "WholeGraph"}
                                                },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                    {"area", area}
                                                },
                        new MvcRouteHandler())
                };
        }
    }
}