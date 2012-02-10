using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    public interface IEngineConfigurationProvider : IDependency
    {
    }

    public interface IEngineConfigurationProvider<TConfigurationDescriptor> : IEngineConfigurationProvider
        where TConfigurationDescriptor : EngineConfigurationDescriptor, new()
    {
        void Describe(TConfigurationDescriptor descriptor);
    }
}
