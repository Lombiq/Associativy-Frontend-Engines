using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.EngineDiscovery;
using System.Web.Mvc;

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