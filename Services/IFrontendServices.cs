using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Services
{
    public interface IFrontendServices : IDependency
    {
        IConfigurationManager ConfigurationManager { get; }
        IFrontendContextAccessor FrontendContextAccessor { get; }
    }
}
