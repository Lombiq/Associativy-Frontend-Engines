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