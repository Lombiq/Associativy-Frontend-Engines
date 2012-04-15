using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Services
{
    public class FrontendServices : IFrontendServices
    {
        private readonly IConfigurationManager _configurationManager;
        private readonly IFrontendContextAccessor _frontendContextAccessor;

        public IConfigurationManager ConfigurationManager
        {
            get { return _configurationManager; }
        }

        public IFrontendContextAccessor FrontendContextAccessor
        {
            get { return _frontendContextAccessor; }
        }

        public FrontendServices(
            IConfigurationManager configurationManager,
            IFrontendContextAccessor frontendContextAccessor)
        {
            _configurationManager = configurationManager;
            _frontendContextAccessor = frontendContextAccessor;
        }
    }
}