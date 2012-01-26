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
    public class DefaultJITConfigurationProvider : EngineConfigurationProviderBase, IJITConfigurationProvider
    {
        private static readonly IEngineContext _describedEngineContext = new EngineContext { EngineName = "JIT" };
        public static IEngineContext DescribedEngineContext
        {
            get { return _describedEngineContext; }
        }

        public Action<IContent, NodeViewModel> ViewModelSetup { get; protected set; }

        public DefaultJITConfigurationProvider()
        {
            GraphContext = new GraphContext();
            EngineContext = DescribedEngineContext;

            ViewModelSetup =
                (node, viewModel) =>
                {
                    // .Has<> doesn't work here
                    if (node.As<ITitleAspect>() != null) viewModel.name = node.As<ITitleAspect>().Title;
                };
        }
    }
}