using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.FrontendEngines.Models;
using Associativy.FrontendEngines.Engines.Dracula.ViewModels;
using Orchard.ContentManagement;

namespace Associativy.FrontendEngines.Engines.Dracula.Models
{
    public interface IDraculaSetup : IFrontendEngineSetup
    {
        NodeViewModel SetViewModel(IContent node, NodeViewModel viewModel);
    }
}
