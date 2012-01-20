using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Models;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;

namespace Associativy.Frontends.Engines.JIT.Models
{
    [OrchardFeature("Associativy.Frontends")]
    public class DefaultJITSetup : FrontendEngineSetupBase, IJITSetup
    {
        public NodeViewModel SetViewModel(IContent node, NodeViewModel viewModel)
        {
            viewModel.id = node.Id.ToString();
            viewModel.name = node.As<ITitleAspect>().Title;
            viewModel.adjacencies = new List<string>();

            return viewModel;
        }
    }
}