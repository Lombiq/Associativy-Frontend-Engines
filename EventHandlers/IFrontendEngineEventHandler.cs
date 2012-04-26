﻿using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;

namespace Associativy.Frontends.EventHandlers
{
    public interface IFrontendEngineEventHandler : IEventHandler
    {
        void OnPageInitializing(FrontendContext frontendContext, IContent page);
        void OnPageInitialized(FrontendContext frontendContext, IContent page);
        void OnPageBuilt(FrontendContext frontendContext, IContent page);
    }
}
