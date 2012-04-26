using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;

namespace Associativy.Frontends.EventHandlers
{
    public interface IFrontendEngineEventHandler : IEventHandler
    {
        void OnPageInitializing(FrontendEventContext frontendEventContext);
        void OnPageInitialized(FrontendEventContext frontendEventContext);
        void OnPageBuilt(FrontendEventContext frontendEventContext);
    }
}
