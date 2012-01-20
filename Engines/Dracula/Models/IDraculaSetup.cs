using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.Frontends.Models;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.Dracula.Models
{
    public interface IDraculaSetup : IFrontendEngineSetup
    {
        NodeViewModel SetViewModel(IContent node, NodeViewModel viewModel);
    }
}
