using System.Linq;
using System.Web.Mvc;
using Associativy.Frontends.Engines;
using Associativy.Frontends.Services;
using Associativy.Models;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
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
            IPageEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
        }

        public virtual ActionResult FetchSimilarLabels(string graphName, string labelSnippet)
        {
            var page = NewPage("FetchSimilarLabels");
            _eventHandler.OnPageBuilt(new PageContext(page, FrontendsPageConfigs.Group));

            if (!IsAuthorized(page)) return new HttpUnauthorizedResult();

            return Json(_nodeManager.GetSimilarNodesQuery(GraphContext, labelSnippet).List().Select(node => node.As<IAssociativyNodeLabelAspect>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
