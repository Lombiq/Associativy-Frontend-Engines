using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Events;
using QuickGraph.Graphviz;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.Graphviz
{
    public interface IGraphvizConfigurationHandler : IEventHandler
    {
        void FormatVertex(object sender, FormatVertexEventArgs<IContent> e);
    }
}
