using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;

namespace Associativy.Frontends.EventHandlers
{
    public interface IFrontendEngineEventHandler : IEventHandler
    {
        void OnPageInitializing(IEngineContext engineContext, IGraphContext graphContext, IContent page);
        void OnPageInitialized(IEngineContext engineContext, IGraphContext graphContext, IContent page);
        void OnPageBuilt(IEngineContext engineContext, IGraphContext graphContext, IContent page);
    }
}
