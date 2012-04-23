using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Events;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.EventHandlers
{
    public interface IFrontendEngineEventHandler : IEventHandler
    {
        void OnPageInitializing(IEngineContext engineContext, IGraphContext graphContext, IContent page);
        void OnPageInitialized(IEngineContext engineContext, IGraphContext graphContext, IContent page);
        void OnPageBuilt(IEngineContext engineContext, IGraphContext graphContext, IContent page);
    }
}
