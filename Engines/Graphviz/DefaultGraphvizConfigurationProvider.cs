using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ConfigurationDiscovery;
using Orchard.ContentManagement;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement.Aspects;
using QuickGraph.Graphviz;

namespace Associativy.Frontends.Engines.Graphviz
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class DefaultGraphvizConfigurationProvider : EngineConfigurationProviderBase, IGraphvizConfigurationProvider
    {
        private static readonly IEngineContext _describedEngineContext = new EngineContext { EngineName = "Graphviz" };
        public static IEngineContext DescribedEngineContext
        {
            get { return _describedEngineContext; }
        }

        public DefaultGraphvizConfigurationProvider()
        {
            GraphContext = new GraphContext();
            EngineContext = DescribedEngineContext;
        }

        public void FormatVertext(object sender, FormatVertexEventArgs<IContent> e)
        {
            // .Has<> doesn't work here
            if (e.Vertex.As<ITitleAspect>() != null) e.VertexFormatter.Label = e.Vertex.As<ITitleAspect>().Title;
            if (e.Vertex.As<IRoutableAspect>() != null) e.VertexFormatter.Url = e.Vertex.As<IRoutableAspect>().Path;

            e.VertexFormatter.Shape = QuickGraph.Graphviz.Dot.GraphvizVertexShape.Diamond;
        }
    }
}