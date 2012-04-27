using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.EngineDiscovery;

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