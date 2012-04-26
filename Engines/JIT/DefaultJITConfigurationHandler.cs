using System.Web.Mvc;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Associativy.GraphDiscovery;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Environment.Extensions;
using Associativy.Frontends.EventHandlers;

namespace Associativy.Frontends.Engines.JIT
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class DefaultJITConfigurationHandler : IJITConfigurationHandler
    {
        private readonly IOrchardServices _orchardServices;

        public DefaultJITConfigurationHandler(IOrchardServices orchardServices)
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