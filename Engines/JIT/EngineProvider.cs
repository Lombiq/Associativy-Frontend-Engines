using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.EngineDiscovery;
using Orchard.Localization;
using Orchard.Mvc.Routes;
using System.Web.Routing;
using System.Web.Mvc;

namespace Associativy.Frontends.Engines.JIT
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class EngineProvider : EngineProviderBase
    {
        public override void Describe(DescribeContext describeContext)
        {
            describeContext.DescribeEngine(
                "JIT",
                () => T("JIT"),
                DefaultRoute("JIT"));
        }
    }
}