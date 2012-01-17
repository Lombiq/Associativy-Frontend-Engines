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

namespace Associativy.FrontendEngines.Controllers
{
    // TODO: better name
    [OrchardFeature("Associativy.FrontendEngines")]
    public class JsonController : AssociativyControllerBase
    {
        protected readonly IGraphDescriptorLocator _graphDescriptorLocator;

        public JsonController(
            IAssociativyServices associativyServices, IGraphDescriptorLocator graphDescriptorLocator)
            : base(associativyServices)
        {
            _graphDescriptorLocator = graphDescriptorLocator;
        }

        public virtual JsonResult FetchSimilarLabels(string technicalGraphName, string labelSnippet)
        {
            _associativyServices.GraphDescriptor = _graphDescriptorLocator.FindGraphDescriptor(technicalGraphName);

            return Json(_nodeManager.GetSimilarNodes(labelSnippet).Select(node => node.As<AssociativyNodeLabelPart>().Label), JsonRequestBehavior.AllowGet);
        }
    }
}
