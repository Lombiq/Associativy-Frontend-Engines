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
    public class DefaultDraculaConfigurationProvider : EngineConfigurationProviderBase, IDraculaConfigurationProvider
    {
        private static readonly IEngineContext _describedEngineContext = new EngineContext { EngineName = "Dracula" };
        public static IEngineContext DescribedEngineContext
        {
            get { return _describedEngineContext; }
        }

        public DefaultDraculaConfigurationProvider()
        {
            GraphContext = new GraphContext();
            EngineContext = DescribedEngineContext;
        }

        public virtual NodeViewModel ViewModelSetup(IContent node, NodeViewModel viewModel)
        {
            viewModel.Label = node.As<ITitleAspect>().Title;

            return viewModel;
        }
    }
}