using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Associativy.Frontends.Controllers;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement.Handlers;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Piedone.HelpfulLibraries.Contents;

namespace Associativy.Frontends.Filters
{
    // This filter is a hackish way to set the context for frontend engine dynamic pages. Better would be this: https://github.com/OrchardCMS/Orchard/issues/4602
    public class FrontendContextSettingFilter : ContentHandler, Orchard.Mvc.Filters.IFilterProvider, IActionFilter
    {
        private readonly IGraphManager _graphManager;

        private FrontendControllerBase _controller = null;


        public FrontendContextSettingFilter(IGraphManager graphManager)
        {
            _graphManager = graphManager;
        }


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.Controller is FrontendControllerBase)) return;

            _controller = (FrontendControllerBase)filterContext.Controller;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }


        protected override void Activated(ActivatedContentContext context)
        {
            if (_controller == null) return;

            var pageContext = context.PageContext();

            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            pageContext.Page.Weld<AssociativyFrontendCommonPart>(part =>
                {
                    part.GraphDescriptor = _graphManager.FindGraph(_controller.GraphContext);
                    part.EngineContext = _controller.EngineContext;
                });
        }

        public void AddFilters(FilterInfo filterInfo)
        {
        }
    }
}