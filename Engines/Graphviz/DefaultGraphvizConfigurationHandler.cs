using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using QuickGraph.Graphviz;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using QuickGraph.Graphviz.Dot;
using Orchard;
using System.Web.Mvc;

namespace Associativy.Frontends.Engines.Graphviz
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class DefaultGraphvizConfigurationHandler : IGraphvizConfigurationHandler
    {
        private readonly IOrchardServices _orchardServices;

        public DefaultGraphvizConfigurationHandler(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public void FormatVertex(object sender, FormatVertexEventArgs<IContent> e)
        {
            // .Has<> doesn't work here
            if (e.Vertex.As<ITitleAspect>() != null) e.VertexFormatter.Label = e.Vertex.As<ITitleAspect>().Title;

            e.VertexFormatter.Url = new UrlHelper(_orchardServices.WorkContext.HttpContext.Request.RequestContext)
                                        .RouteUrl(_orchardServices.ContentManager.GetItemMetadata(e.Vertex).DisplayRouteValues);

            e.VertexFormatter.Shape = GraphvizVertexShape.Diamond;
        }
    }
}