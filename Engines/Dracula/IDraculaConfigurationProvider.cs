using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.Frontends.ConfigurationDiscovery;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.ViewModels;

namespace Associativy.Frontends.Engines.Dracula
{
    public interface IDraculaConfigurationProvider : IEngineConfigurationProvider
    {
        Action<IContent, NodeViewModel> ViewModelSetup { get; }
    }
}
