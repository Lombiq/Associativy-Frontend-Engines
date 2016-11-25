using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using QuickGraph;

namespace Associativy.Frontends.Models.Pages.Frontends
{
    public class AssociativyFrontendGraphPart : ContentPart, IGraphAspect
    {
        private readonly LazyField<IUndirectedGraph<IContent, IUndirectedEdge<IContent>>> _graph = new LazyField<IUndirectedGraph<IContent, IUndirectedEdge<IContent>>>();
        internal LazyField<IUndirectedGraph<IContent, IUndirectedEdge<IContent>>> GraphField { get { return _graph; } }
        public IUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph
        {
            get { return _graph.Value; }
        }

        private readonly LazyField<int> _zoomLevelCount = new LazyField<int>();
        internal LazyField<int> ZoomLevelCountField { get { return _zoomLevelCount; } }
        public int ZoomLevelCount
        {
            get { return _zoomLevelCount.Value; }
        }
    }
}