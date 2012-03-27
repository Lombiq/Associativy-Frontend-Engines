using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class ConfigurationProviderBase<TConfigurationDescriptor> : IEngineConfigurationProvider<TConfigurationDescriptor>
        where TConfigurationDescriptor : ConfigurationDescriptor, new()
    {
        public virtual void Describe(TConfigurationDescriptor descriptor)
        {
            // Setting defaults
            descriptor.GraphContext = new GraphContext();
            descriptor.ModifyGraphQuery = (query) => query.Join<TitlePartRecord>();
            descriptor.MaxZoomLevel = 10;
        }
    }
}