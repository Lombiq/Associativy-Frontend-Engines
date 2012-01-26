using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.Frontends.ConfigurationDiscovery;
using Orchard.ContentManagement;
using QuickGraph.Graphviz;

namespace Associativy.Frontends.Engines.Graphviz
{
    public interface IGraphvizConfigurationProvider : IEngineConfigurationProvider
    {
        Action<object, FormatVertexEventArgs<IContent>> VertexFormatter { get; }
    }
}
