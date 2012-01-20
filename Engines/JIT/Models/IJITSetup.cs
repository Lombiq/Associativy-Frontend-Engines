using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.Frontends.Models;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.JIT.ViewModels;

namespace Associativy.Frontends.Engines.JIT.Models
{
    public interface IJITSetup : IFrontendEngineSetup
    {
        NodeViewModel SetViewModel(IContent node, NodeViewModel viewModel);
    }
}
