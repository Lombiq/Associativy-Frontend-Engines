using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
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
                    Name = "Associativy " + engineName + " frontend engine route",
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