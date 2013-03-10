using Associativy.Models.Services;
using Associativy.Queryable;
using Orchard.ContentManagement;
using QuickGraph;

namespace Associativy.Frontends.Models
{
    public delegate IQueryableGraph<int> GraphRetriever();

    public interface IGraphRetrieverAspect : IContent
    {
        GraphRetriever RetrieveGraph { get; }
    }
}
