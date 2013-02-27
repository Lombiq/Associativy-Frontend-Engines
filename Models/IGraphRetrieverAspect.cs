using Associativy.Models.Mind;
using Orchard.ContentManagement;
using QuickGraph;

namespace Associativy.Frontends.Models
{
    public delegate IUndirectedGraph<int, IUndirectedEdge<int>> GraphRetriever(IMindSettings settings);
    public delegate IUndirectedGraph<IContent, IUndirectedEdge<IContent>> ContentGraphRetriever(IMindSettings settings);

    public interface IGraphRetrieverAspect : IContent
    {
        GraphRetriever RetrieveGraph { get; }
        ContentGraphRetriever RetrieveContentGraph { get; }
    }
}
