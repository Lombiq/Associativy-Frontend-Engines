using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ConfigurationDiscovery;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Orchard.ContentManagement;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement.Aspects;

namespace Associativy.Frontends.Engines.JIT
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class DefaultJITConfigurationProvider : EngineConfigurationProviderBase<JITConfigurationDescriptor>
    {
        private static readonly IEngineContext _describedEngineContext = new EngineContext { EngineName = "JIT" };
        public static IEngineContext DescribedEngineContext
        {
            get { return _describedEngineContext; }
        }

        public override void Describe(JITConfigurationDescriptor descriptor)
        {
            base.Describe(descriptor);

            descriptor.EngineContext = DescribedEngineContext;
            descriptor.ViewModelSetup =
                (node, viewModel) =>
                {
                    // .Has<> doesn't work here
                    if (node.As<ITitleAspect>() != null) viewModel.name = node.As<ITitleAspect>().Title;
                    if (node.As<IRoutableAspect>() != null) viewModel.data["url"] = node.As<IRoutableAspect>().Path; // Needs revision after 1.4
                };
        }
    }
}