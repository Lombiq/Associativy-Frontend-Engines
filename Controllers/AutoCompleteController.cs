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
using System.Diagnostics;
using Associativy.Frontends.Models;
using Associativy.GraphDiscovery;
using Piedone.HelpfulLibraries.Serialization;
using Associativy.Frontends.Services;

namespace Associativy.Frontends.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public class AutoCompleteController : AssociativyControllerBase
    {
        protected readonly IGraphContextEncoder _graphContextEncoder;

        public AutoCompleteController(
            IAssociativyServices associativyServices,
            IGraphContextEncoder graphContextEncoder)
            : base(associativyServices)
        {
            _graphContextEncoder = graphContextEncoder;
        }

        public virtual JsonResult FetchSimilarLabels(string encodedGraphContext, string labelSnippet)
        {
            return Json(_nodeManager.GetSimilarNodes(_graphContextEncoder.DecodeGraphContext(encodedGraphContext), labelSnippet).Select(node => node.As<AssociativyNodeLabelPart>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
