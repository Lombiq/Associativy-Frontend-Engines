using Orchard;

namespace Associativy.Frontends.EngineDiscovery
{
    public interface IEngineProvider : IDependency
    {
        void Describe(DescribeContext describeContext);
    }
}
