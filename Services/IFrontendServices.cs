using Orchard;

namespace Associativy.Frontends.Services
{
    public interface IFrontendServices : IDependency
    {
        IFrontendContextAccessor FrontendContextAccessor { get; }
    }
}
