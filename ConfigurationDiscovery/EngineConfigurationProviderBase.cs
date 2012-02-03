using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Core.Routable.Models;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class EngineConfigurationProviderBase : IEngineConfigurationProvider
    {
        public IEngineContext EngineContext { get; protected set; }

        public IGraphContext GraphContext { get; protected set; }

        public virtual Func<IContentQuery<ContentItem>, IContentQuery<ContentItem>> GraphQueryModifier
        {
            get { return (query) => query.Join<RoutePartRecord>(); }
        }

        public virtual int MaxZoomLevel
        {
            get { return 10; }
        }
    }
}