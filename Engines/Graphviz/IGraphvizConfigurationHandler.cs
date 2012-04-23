using Orchard.ContentManagement;
using Orchard.Events;
using QuickGraph.Graphviz;

namespace Associativy.Frontends.Engines.Graphviz
{
    public interface IGraphvizConfigurationHandler : IEventHandler
    {
        void FormatVertex(object sender, FormatVertexEventArgs<IContent> e);
    }
}
