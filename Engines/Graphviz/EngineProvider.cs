using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.EngineDiscovery;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Graphviz
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class EngineProvider : EngineProviderBase
    {
        public override void Describe(DescribeContext describeContext)
        {
            describeContext.DescribeEngine(
                "Graphviz",
                () => T("Graphviz"),
                DefaultRoute("Graphviz"));
        }
    }
}