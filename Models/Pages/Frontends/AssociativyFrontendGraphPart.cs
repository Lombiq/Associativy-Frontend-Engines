using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Environment.Extensions;
using QuickGraph;

namespace Associativy.Frontends.Models.Pages.Frontends
{
    public class AssociativyFrontendGraphPart : ContentPart, IGraphAspect
    {
        private readonly LazyField<IUndirectedGraph<IContent, IUndirectedEdge<IContent>>> _graph = new LazyField<IUndirectedGraph<IContent, IUndirectedEdge<IContent>>>();
        public LazyField<IUndirectedGraph<IContent, IUndirectedEdge<IContent>>> GraphField { get { return _graph; } }
        public IUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph
        {
            get { return _graph.Value; }
        }

        private readonly LazyField<int> _zoomLevelCount = new LazyField<int>();
        public LazyField<int> ZoomLevelCountField { get { return _zoomLevelCount; } }
        public int ZoomLevelCount
        {
            get { return _zoomLevelCount.Value; }
        }
    }
}