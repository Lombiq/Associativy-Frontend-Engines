using Associativy.GraphDiscovery;
using Orchard;

namespace Associativy.Frontends.Services
{
    public interface IFrontendContextAccessor : IDependency
    {
        IGraphContext GetCurrentGraphContext();
    }
}
