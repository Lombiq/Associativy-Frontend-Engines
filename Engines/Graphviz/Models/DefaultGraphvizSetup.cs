using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Models;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;
using QuickGraph.Graphviz;
using Orchard.ContentManagement.Aspects;

namespace Associativy.Frontends.Engines.Graphviz.Models
{
    [OrchardFeature("Associativy.Frontends")]
    public class DefaultGraphvizSetup : FrontendEngineSetupBase, IGraphvizSetup
    {
        public FormatVertexEventHandler<IContent> VertexFormatter
        {
            get
            {
                return (sender, e) =>
                {
                    e.VertexFormatter.Label = e.Vertex.As<ITitleAspect>().Title;
                    e.VertexFormatter.Shape = QuickGraph.Graphviz.Dot.GraphvizVertexShape.Diamond;
                    e.VertexFormatter.Url = e.Vertex.As<IRoutableAspect>().Path;
                };
            }
        }
    }
}