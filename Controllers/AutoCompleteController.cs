using System.Linq;
using System.Web.Mvc;
using Associativy.Controllers;
using Associativy.GraphDiscovery;
using Associativy.Models;
using Associativy.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Services;
using Associativy.Frontends.EventHandlers;
using Orchard;
using Associativy.Frontends.Engines;

namespace Associativy.Frontends.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public class AutoCompleteController : DynamicallyContextedControllerBase
    {
        private readonly IAssociativyFrontendEngineEventHandler _eventHandler;
        private readonly IOrchardServices _orchardServices;

        public AutoCompleteController(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IAssociativyFrontendEngineEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices)
        {
            _eventHandler = eventHandler;
            _orchardServices = orchardServices;
        }

        public virtual ActionResult FetchSimilarLabels(string graphName, string labelSnippet)
        {
            var authorizationContext = new FrontendAuthorizationEventContext(_orchardServices.WorkContext.CurrentUser, null, new EngineContext(), GraphContext);
            _eventHandler.OnAuthorization(authorizationContext);
            if (!authorizationContext.Granted)
            {
                return new HttpUnauthorizedResult();
            }

            return Json(_nodeManager.GetSimilarNodes(GraphContext, labelSnippet).Select(node => node.As<IAssociativyNodeLabelAspect>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
