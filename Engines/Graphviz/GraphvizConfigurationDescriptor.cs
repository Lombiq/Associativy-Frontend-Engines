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
        private Action<object, FormatVertexEventArgs<IContent>> _vertexFormatter;
        public Action<object, FormatVertexEventArgs<IContent>> VertexFormatter
        {
            get { return _vertexFormatter; }
            set
            {
                ThrowIfFrozen();
                _vertexFormatter = value;
            }
        }
    }
}