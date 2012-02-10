using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;
using Associativy.GraphDiscovery;
using Associativy.Frontends.Engines;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    public interface IEngineConfigurationManager : IDependency
    {
        TConfigurationDescriptor FindConfiguration<TConfigurationDescriptor>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationDescriptor : EngineConfigurationDescriptor, new();

        IEnumerable<TConfigurationDescriptor> FindConfigurations<TConfigurationDescriptor>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationDescriptor : EngineConfigurationDescriptor, new();
    }
}
