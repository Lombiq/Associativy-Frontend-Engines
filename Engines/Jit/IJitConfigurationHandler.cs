using Associativy.Frontends.Engines.Jit.ViewModels;
using Associativy.Frontends.EventHandlers;
using Orchard.ContentManagement;
using Orchard.Events;

namespace Associativy.Frontends.Engines.Jit
{
    public interface IJitConfigurationHandler : IEventHandler
    {
        void SetupViewModel(FrontendContext frontendContext, IContent node, NodeViewModel viewModel);
    }
}
