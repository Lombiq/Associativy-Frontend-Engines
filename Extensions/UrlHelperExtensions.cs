using System.Web.Mvc;
using Associativy.Frontends.EngineDiscovery;

namespace Associativy.Frontends.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string RouteEngineUrl(this UrlHelper helper, IEngineDescriptor engine, string graphName)
        {
            return helper.RouteUrl(engine.Route.Name, new { GraphName = graphName, Action = "" });
        }
    }
}