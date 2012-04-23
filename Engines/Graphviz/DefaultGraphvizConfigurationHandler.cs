using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using QuickGraph.Graphviz;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using QuickGraph.Graphviz.Dot;

namespace Associativy.Frontends.Engines.Graphviz
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class DefaultGraphvizConfigurationHandler : IGraphvizConfigurationHandler
    {
        public void FormatVertex(object sender, FormatVertexEventArgs<IContent> e)
        {
            // .Has<> doesn't work here
            if (e.Vertex.As<ITitleAspect>() != null) e.VertexFormatter.Label = e.Vertex.As<ITitleAspect>().Title;
            if (e.Vertex.As<IAliasAspect>() != null) e.VertexFormatter.Url = e.Vertex.As<IAliasAspect>().Path;

            e.VertexFormatter.Shape = GraphvizVertexShape.Diamond;
        }
    }
}