using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.Frontends.EventHandlers;
using Orchard.ContentManagement;
using Orchard.Events;

namespace Associativy.Frontends.Engines.Dracula
{
    public interface IDraculaConfigurationHandler : IEventHandler
    {
        void SetupViewModel(FrontendContext frontendContext, IContent node, NodeViewModel viewModel);
    }
}
