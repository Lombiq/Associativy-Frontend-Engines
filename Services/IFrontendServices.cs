using Orchard;
using Associativy.Frontends.EngineDiscovery;

namespace Associativy.Frontends.Services
{
    public interface IFrontendServices : IDependency
    {
        IFrontendContextAccessor FrontendContextAccessor { get; }
        IEngineManager EngineManager { get; }
    }
}
