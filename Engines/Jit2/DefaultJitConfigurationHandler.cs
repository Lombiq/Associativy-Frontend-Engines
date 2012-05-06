using System.Web.Mvc;
using Associativy.Frontends.Engines.Jit.ViewModels;
using Associativy.GraphDiscovery;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Environment.Extensions;
using Associativy.Frontends.EventHandlers;

namespace Associativy.Frontends.Engines.Jit
{
    [OrchardFeature("Associativy.Frontends.Jit")]
    public class DefaultJitConfigurationHandler : IJitConfigurationHandler
    {
        private readonly IOrchardServices _orchardServices;

        public DefaultJitConfigurationHandler(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public void SetupViewModel(FrontendContext frontendContext, IContent node, NodeViewModel viewModel)
        {
            // .Has<> doesn't work here
            if (node.As<ITitleAspect>() != null) viewModel.name = node.As<ITitleAspect>().Title;

            if (node.As<IAliasAspect>() != null)
            {
                viewModel.data["url"] = new UrlHelper(_orchardServices.WorkContext.HttpContext.Request.RequestContext)
                                            .RouteUrl(_orchardServices.ContentManager.GetItemMetadata(node).DisplayRouteValues);
            }
        }
    }
}