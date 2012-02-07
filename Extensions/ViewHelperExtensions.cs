using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard;
using Associativy.Frontends.Services;
using Associativy.GraphDiscovery;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace Associativy.Frontends.Extensions
{
    public static class ViewHelperExtensions
    {
        public static string EncodeGraphContext(this HtmlHelper html, IGraphContext graphContext)
        {
            return html.ViewContext.RequestContext.GetWorkContext().Resolve<IGraphContextEncoder>().EncodeGraphContext(graphContext);
        }

        public static MvcForm BeginRouteToCurrentForm(this HtmlHelper html, RouteBase currentRoute, object routeValues, FormMethod method, object htmlAttributes)
        {
            return html.BeginRouteForm(((Route)currentRoute).DataTokens["RouteName"].ToString(), routeValues, method, htmlAttributes);
        }

        public static string RouteToCurrent(this UrlHelper url, RouteBase currentRoute, object routeValues)
        {
            return url.RouteUrl(((Route)currentRoute).DataTokens["RouteName"].ToString(), routeValues);
        }
    }
}