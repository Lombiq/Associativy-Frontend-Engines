using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.FrontendEngines.Models;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Engines.Dracula.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;

namespace Associativy.FrontendEngines.Engines.Dracula.Models
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class DefaultDraculaSetup : FrontendEngineSetupBase, IDraculaSetup
    {
        public NodeViewModel SetViewModel(IContent node, NodeViewModel viewModel)
        {
            viewModel.Label = node.As<ITitleAspect>().Title;

            return viewModel;
        }
    }
}