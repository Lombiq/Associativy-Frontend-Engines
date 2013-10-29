using System;
using System.Web.Mvc;
using System.Web.Routing;
using Associativy.Frontends.EngineDiscovery;

namespace Associativy.Frontends.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string RouteEngineUrl(this UrlHelper helper, IEngineDescriptor engine, string graphName)
        {
            return RouteEngineUrl(helper, engine, graphName, new RouteValueDictionary());
        }

        public static string RouteEngineUrl(this UrlHelper helper, IEngineDescriptor engine, string graphName, object routeValues)
        {
            return RouteEngineUrl(helper, engine, graphName, new RouteValueDictionary(routeValues));
        }

        public static string RouteEngineUrl(this UrlHelper helper, IEngineDescriptor engine, string graphName, RouteValueDictionary routeValues)
        {
            if (routeValues == null) throw new ArgumentNullException("routeValues");

            var originalRouteValues = ((System.Web.Routing.Route)engine.Route.Route).Defaults;
            originalRouteValues.Add("GraphName", graphName);
            foreach (var routeValue in routeValues)
            {
                originalRouteValues[routeValue.Key] = routeValue.Value;
            }
            return helper.RouteUrl(originalRouteValues);
        }
    }
}