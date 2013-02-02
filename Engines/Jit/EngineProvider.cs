using Associativy.Frontends.EngineDiscovery;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Jit
{
    [OrchardFeature("Associativy.Frontends.Jit")]
    public class EngineProvider : EngineProviderBase
    {
        public override void Describe(DescribeContext describeContext)
        {
            describeContext.DescribeEngine("Jit", T("Jit"), DefaultRoute("Jit"));
        }
    }
}