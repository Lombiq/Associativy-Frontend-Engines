using System.Web.Mvc;
using Associativy.Frontends.EngineDiscovery;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Extensions
{
    [OrchardFeature("Associativy.Frontends")]
    public static class UrlHelperExtensions
    {
        public static string RouteEngineUrl(this UrlHelper helper, EngineDescriptor engine, string graphName)
        {
            return helper.RouteUrl(engine.Route.Name, new { GraphName = graphName, Action = "" });
        }
    }
}