using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ConfigurationDiscovery;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.JIT
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class JITConfigurationDescriptor : EngineConfigurationDescriptor
    {
        public delegate void ViewModelSetup(IContent contentItem, NodeViewModel viewModel);

        private ViewModelSetup _setupViewModel;
        public ViewModelSetup SetupViewModel
        {
            get { return _setupViewModel; }
            set
            {
                ThrowIfFrozen();
                _setupViewModel = value;
            }
        }
    }
}