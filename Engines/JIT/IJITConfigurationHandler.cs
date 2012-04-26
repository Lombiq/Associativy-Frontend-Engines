using Associativy.Frontends.Engines.JIT.ViewModels;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;
using Associativy.Frontends.EventHandlers;

namespace Associativy.Frontends.Engines.JIT
{
    public interface IJITConfigurationHandler : IEventHandler
    {
        void SetupViewModel(FrontendContext frontendContext, IContent node, NodeViewModel viewModel);
    }
}
