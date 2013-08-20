using System.Web.Mvc;
using Associativy.Frontends.EngineDiscovery;

namespace Associativy.Frontends.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string RouteEngineUrl(this UrlHelper helper, IEngineDescriptor engine, string graphName)
        {
            var routeValues = ((System.Web.Routing.Route)engine.Route.Route).Defaults;
            routeValues.Add("GraphName", graphName);
            return helper.RouteUrl(routeValues);
        }
    }
}