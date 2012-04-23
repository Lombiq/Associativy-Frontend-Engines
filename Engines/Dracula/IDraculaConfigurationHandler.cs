using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;

namespace Associativy.Frontends.Engines.Dracula
{
    public interface IDraculaConfigurationHandler : IEventHandler
    {
        void SetupViewModel(IEngineContext engineContext, IGraphContext graphContext, IContent node, NodeViewModel viewModel);
    }
}
