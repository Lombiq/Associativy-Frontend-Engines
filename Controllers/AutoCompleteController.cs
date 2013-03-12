using System.Linq;
using System.Web.Mvc;
using Associativy.Frontends.Engines;
using Associativy.Frontends.Models;
using Associativy.Frontends.Services;
using Associativy.Models;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Controllers
{
    public class AutoCompleteController : FrontendControllerBase
    {
        protected override IEngineContext EngineContext
        {
            get { return new EngineContext(string.Empty); }
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

            return Json(page.As<IEngineConfigurationAspect>().GraphDescriptor.Services.NodeManager.GetSimilarNodesQuery(labelSnippet).Slice(15).Select(node => node.As<IAssociativyNodeLabelAspect>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
