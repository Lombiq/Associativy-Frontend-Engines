using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.JIT.ViewModels;
using Orchard.ContentManagement.Aspects;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;
using Orchard;
using System.Web.Mvc;

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

        public void SetupViewModel(IEngineContext engineContext, IGraphContext graphContext, IContent node, NodeViewModel viewModel)
        {
            // .Has<> doesn't work here
            if (node.As<ITitleAspect>() != null) viewModel.name = node.As<ITitleAspect>().Title;

            viewModel.data["url"] = new UrlHelper(_orchardServices.WorkContext.HttpContext.Request.RequestContext)
                                        .RouteUrl(_orchardServices.ContentManager.GetItemMetadata(node).DisplayRouteValues);
        }
    }
}