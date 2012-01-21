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
    public interface IEngineConfigurationProvider : IAssociativyProvider
    {
        IGraphContext GraphContext { get; }
        IEngineContext EngineContext { get; }
        Func<IContentQuery<ContentItem>, IContentQuery<ContentItem>> GraphQueryModifier { get; }
        int MaxZoomLevel { get; }
    }
}
