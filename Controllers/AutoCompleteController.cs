using System.Linq;
using System.Web.Mvc;
using Associativy.Controllers;
using Associativy.GraphDiscovery;
using Associativy.Models;
using Associativy.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public class AutoCompleteController : AssociativyControllerBase
    {
        public AutoCompleteController(IAssociativyServices associativyServices)
            : base(associativyServices)
        {
        }

        public virtual JsonResult FetchSimilarLabels(string graphName, string labelSnippet)
        {
            return Json(_nodeManager.GetSimilarNodes(new GraphContext { GraphName = graphName }, labelSnippet).Select(node => node.As<IAssociativyNodeLabelAspect>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
