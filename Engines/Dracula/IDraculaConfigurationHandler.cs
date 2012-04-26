using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;
using Associativy.Frontends.EventHandlers;

namespace Associativy.Frontends.Engines.Dracula
{
    public interface IDraculaConfigurationHandler : IEventHandler
    {
        void SetupViewModel(FrontendContext frontendContext, IContent node, NodeViewModel viewModel);
    }
}
