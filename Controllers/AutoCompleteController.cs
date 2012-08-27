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
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public class AutoCompleteController : FrontendControllerBase
    {
        protected override IEngineContext EngineContext
        {
            get { return new EngineContext(); }
        }

        public AutoCompleteController(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IAssociativyFrontendEngineEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
        }

        public virtual ActionResult FetchSimilarLabels(string graphName, string labelSnippet)
        {
            var page = NewPage("FetchSimilarLabels");
            _eventHandler.OnPageBuilt(page);
            if (!IsAuthorized(page))
            {
                return new HttpUnauthorizedResult();
            }

            return Json(_nodeManager.GetSimilarNodes(GraphContext, labelSnippet).Select(node => node.As<IAssociativyNodeLabelAspect>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
