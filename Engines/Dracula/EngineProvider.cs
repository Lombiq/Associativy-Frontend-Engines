using Associativy.Frontends.EngineDiscovery;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class EngineProvider : EngineProviderBase
    {
        public override void Describe(DescribeContext describeContext)
        {
            describeContext.DescribeEngine(
                "Dracula",
                () => T("Dracula"),
                DefaultRoute("Dracula"));
        }
    }
}