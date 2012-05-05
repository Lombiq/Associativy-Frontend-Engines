using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Environment.Extensions;
using QuickGraph;

namespace Associativy.Frontends.Models.Pages.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public class AssociativyFrontendGraphPart : ContentPart, IGraphAspect
    {
        private readonly LazyField<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>> _graph = new LazyField<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>>();
        public LazyField<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>> GraphField { get { return _graph; } }
        public IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph
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