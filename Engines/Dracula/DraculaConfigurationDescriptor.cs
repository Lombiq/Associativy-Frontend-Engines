using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ConfigurationDiscovery;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaConfigurationDescriptor : ConfigurationDescriptor
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