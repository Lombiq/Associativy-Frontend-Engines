using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Associativy.Frontends.ConfigurationDiscovery;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.JIT
{
    public interface IJITConfigurationProvider : IEngineConfigurationProvider
    {
        NodeViewModel ViewModelSetup(IContent node, NodeViewModel viewModel);
    }
}
