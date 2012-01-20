using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Models;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;

namespace Associativy.Frontends.Engines.Dracula.Models
{
    [OrchardFeature("Associativy.Frontends")]
    public class DefaultDraculaSetup : FrontendEngineSetupBase, IDraculaSetup
    {
        public NodeViewModel SetViewModel(IContent node, NodeViewModel viewModel)
        {
            viewModel.Label = node.As<ITitleAspect>().Title;

            return viewModel;
        }
    }
}