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
    public class DraculaConfigurationDescriptor : EngineConfigurationDescriptor
    {
        private Action<IContent, NodeViewModel> _viewModelSetup;
        public Action<IContent, NodeViewModel> ViewModelSetup
        {
            get { return _viewModelSetup; }
            set
            {
                ThrowIfFrozen();
                _viewModelSetup = value;
            }
        }
    }
}