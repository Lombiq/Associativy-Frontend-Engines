using Associativy.Frontends.Engines.Jit.ViewModels;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;
using Associativy.Frontends.EventHandlers;

namespace Associativy.Frontends.Engines.Jit
{
    public interface IJitConfigurationHandler : IEventHandler
    {
        void SetupViewModel(FrontendContext frontendContext, IContent node, NodeViewModel viewModel);
    }
}
