using System;
using System.Web;
using Associativy.Controllers;
using Associativy.Frontends.Engines;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Frontends.Services;
using Associativy.GraphDiscovery;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Controllers
{
    public abstract class FrontendControllerBase : AssociativyControllerBase
    {
        protected readonly IFrontendServices _frontendServices;
        protected readonly IFrontendContextAccessor _frontendContextAccessor;
        protected readonly IOrchardServices _orchardServices;
        protected readonly IContentManager _contentManager;

        abstract public IEngineContext EngineContext { get; }

        public Localizer T { get; set; }

        private IGraphContext _graphContext;
        public IGraphContext GraphContext
        {
            get
            {
                if (_graphContext == null)
                {
                    _graphContext = _frontendServices.FrontendContextAccessor.GetCurrentGraphContext();
                    if (_graphContext == null)
                    {
                        throw new InvalidOperationException("The graph context was not set for the current request.");
                    }
                }

                return _graphContext;
            }
        }


        protected FrontendControllerBase(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IOrchardServices orchardServices)
            : base(associativyServices)
        {
            _frontendServices = frontendServices;
            _frontendContextAccessor = frontendServices.FrontendContextAccessor;

            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;

            T = NullLocalizer.Instance;
        }


        protected virtual IContent NewPage(string pageName)
        {
            var graph = _graphManager.FindGraph(GraphContext);
            if (graph == null) throw new HttpException(404, "The graph was not found.");

            // The context for the page is set in FrontendContextSettingFilter
            return _contentManager.NewPage(
                EngineContext.EngineName + pageName,
                FrontendsPageConfigs.Group);
        }

        protected virtual bool IsAuthorized(IContent page)
        {
            return _orchardServices.Authorizer.Authorize(Permissions.ViewGraphs, page);
        }
    }
}