using Associativy.Frontends.Engines.JIT.ViewModels;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Events;

namespace Associativy.Frontends.Engines.JIT
{
    public interface IJITConfigurationHandler : IEventHandler
    {
        void SetupViewModel(IEngineContext engineContext, IGraphContext graphContext, IContent node, NodeViewModel viewModel);
    }
}
