using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ConfigurationDiscovery;
using QuickGraph.Graphviz;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.Graphviz
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class GraphvizConfigurationDescriptor : EngineConfigurationDescriptor
    {
        public delegate void VertexFormatter(object sender, FormatVertexEventArgs<IContent> e);

        private VertexFormatter _formatVertex;
        public VertexFormatter FormatVertex
        {
            get { return _formatVertex; }
            set
            {
                ThrowIfFrozen();
                _formatVertex = value;
            }
        }
    }
}