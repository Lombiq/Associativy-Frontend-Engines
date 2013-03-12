using Associativy.Queryable;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Models
{
    public delegate IQueryableGraph<int> GraphRetriever();

    public interface IGraphRetrieverAspect : IContent
    {
        GraphRetriever RetrieveGraph { get; }
    }
}
