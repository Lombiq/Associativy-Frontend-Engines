using Orchard.ContentManagement;
using QuickGraph;

namespace Associativy.Frontends.Models
{
    public interface IGraphAspect : IContent
    {
        IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph { get; }
        int ZoomLevelCount { get; }
    }
}
