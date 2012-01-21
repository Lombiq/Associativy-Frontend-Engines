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
        TConfigurationProvider FindLastProvider<TConfigurationProvider>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationProvider : IEngineConfigurationProvider;

        IEnumerable<TConfigurationProvider> FindProviders<TConfigurationProvider>(IEngineContext engineContext, IGraphContext graphContext)
            where TConfigurationProvider : IEngineConfigurationProvider;
    }
}
