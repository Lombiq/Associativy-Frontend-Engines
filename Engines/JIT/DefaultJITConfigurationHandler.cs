using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Orchard.ContentManagement.Aspects;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Engines.JIT
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class DefaultJITConfigurationHandler : IJITConfigurationHandler
    {
        public void SetupViewModel(IEngineContext engineContext, IGraphContext graphContext, IContent node, NodeViewModel viewModel)
        {
            // .Has<> doesn't work here
            if (node.As<ITitleAspect>() != null) viewModel.name = node.As<ITitleAspect>().Title;
            if (node.As<IAliasAspect>() != null) viewModel.data["url"] = node.As<IAliasAspect>().Path;
        }
    }
}