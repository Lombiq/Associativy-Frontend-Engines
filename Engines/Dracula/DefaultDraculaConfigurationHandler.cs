using Associativy.Frontends.Engines.Dracula.ViewModels;
using Associativy.Frontends.EventHandlers;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DefaultDraculaConfigurationHandler : IDraculaConfigurationHandler
    {
        public void SetupViewModel(FrontendContext frontendContext, IContent node, NodeViewModel viewModel)
        {
            // .Has<> doesn't work here
            if (node.As<ITitleAspect>() != null) viewModel.Label = node.As<ITitleAspect>().Title;
        }
    }
}