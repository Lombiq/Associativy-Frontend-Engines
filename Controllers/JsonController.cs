using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Associativy.Controllers;
using Associativy.Models;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Routable.Models;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using QuickGraph;
using Associativy.FrontendEngines.Shapes;
using System.Diagnostics;
using Associativy.FrontendEngines.Models;
using Associativy.GraphDiscovery;

namespace Associativy.FrontendEngines.Controllers
{
    // TODO: better name
    [OrchardFeature("Associativy.FrontendEngines")]
    public class JsonController : AssociativyControllerBase
    {
        public JsonController(
            IAssociativyServices associativyServices)
            : base(associativyServices)
        {
        }

        public virtual JsonResult FetchSimilarLabels(string graphContext, string labelSnippet)
        {
            // graphContext deserialization
            return null;
            //return Json(_nodeManager.GetSimilarNodes(GraphContext, labelSnippet).Select(node => node.As<AssociativyNodeLabelPart>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
