using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.FrontendEngines.Models;
using Orchard.ContentManagement;
using Associativy.FrontendEngines.Engines.JIT.ViewModels;

namespace Associativy.FrontendEngines.Engines.JIT.Models
{
    public interface IJITSetup : IFrontendEngineSetup
    {
        NodeViewModel SetViewModel(IContent node, NodeViewModel viewModel);
    }
}
