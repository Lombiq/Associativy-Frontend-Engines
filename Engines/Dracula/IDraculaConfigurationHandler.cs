using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Events;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Engines.Dracula
{
    public interface IDraculaConfigurationHandler : IEventHandler
    {
        void SetupViewModel(IEngineContext engineContext, IGraphContext graphContext, IContent node, NodeViewModel viewModel);
    }
}
