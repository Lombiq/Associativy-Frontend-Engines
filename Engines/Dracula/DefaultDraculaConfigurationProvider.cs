using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ConfigurationDiscovery;
using Orchard.ContentManagement;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement.Aspects;
using Associativy.Frontends.Engines.Dracula.ViewModels;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DefaultDraculaConfigurationProvider : ConfigurationProviderBase<DraculaConfigurationDescriptor>
    {
        private static readonly IEngineContext _describedEngineContext = new EngineContext { EngineName = "Dracula" };
        public static IEngineContext DescribedEngineContext
        {
            get { return _describedEngineContext; }
        }

        public override void Describe(DraculaConfigurationDescriptor descriptor)
        {
            base.Describe(descriptor);

            descriptor.EngineContext = DescribedEngineContext;
            descriptor.SetupViewModel =
                (node, viewModel) =>
                {
                    // .Has<> doesn't work here
                    if (node.As<ITitleAspect>() != null) viewModel.Label = node.As<ITitleAspect>().Title;
                };
        }
    }
}