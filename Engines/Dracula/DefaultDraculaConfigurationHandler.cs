using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Orchard.ContentManagement.Aspects;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DefaultDraculaConfigurationHandler : IDraculaConfigurationHandler
    {
        public void SetupViewModel(IEngineContext engineContext, IGraphContext graphContext, IContent node, NodeViewModel viewModel)
        {
            // .Has<> doesn't work here
            if (node.As<ITitleAspect>() != null) viewModel.Label = node.As<ITitleAspect>().Title;
        }
    }
}