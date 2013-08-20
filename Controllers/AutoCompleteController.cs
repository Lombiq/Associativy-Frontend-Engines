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

            var labels = page.As<IEngineConfigurationAspect>().GraphDescriptor.Services.NodeManager.GetBySimilarLabelQuery(labelSnippet).Slice(15).Select(node => node.As<IAssociativyNodeLabelAspect>().Label);

            // Re-ordering labels so the most matching one is at the top. This is necessary since ContentQuery doesn't maintain ordering the of search hits.
            var trimmedLabelSnippet = labelSnippet.Trim();
            labels = labels.OrderBy(label => LevenshteinDistance(label, trimmedLabelSnippet));

            return Json(labels, JsonRequestBehavior.AllowGet);
        }


        // Taken from. http://sadgeeksinsnow.blogspot.hu/2010/10/optimizing-levenshtein-algorithm-in-c.html
        private static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            int cost;

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    cost = (t.Substring(j - 1, 1) == s.Substring(i - 1, 1) ? 0 : 1);
                    d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }
    }
}
