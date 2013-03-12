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
        protected readonly IPageEventHandler _eventHandler;
        protected readonly IOrchardServices _orchardServices;
        protected readonly IContentManager _contentManager;

        abstract protected IEngineContext EngineContext { get; }

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
            IPageEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices)
        {
            _frontendServices = frontendServices;
            _frontendContextAccessor = frontendServices.FrontendContextAccessor;

            _eventHandler = eventHandler;
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;

            T = NullLocalizer.Instance;
        }


        protected virtual IContent NewPage(string pageName)
        {
            var graph = _graphManager.FindGraph(GraphContext);
            if (graph == null) throw new HttpException(404, "The graph was not found.");

            var page = _contentManager.NewPage(
                EngineContext.EngineName + pageName,
                FrontendsPageConfigs.Group,
                (content) =>
                    {
                        var engineCommonPart = new AssociativyFrontendCommonPart();
                        engineCommonPart.GraphDescriptor = graph;
                        engineCommonPart.EngineContext = EngineContext;

                        content.ContentItem.Weld(engineCommonPart);
                    },
                _eventHandler);

            return page;
        }

        protected virtual bool IsAuthorized(IContent page)
        {
            var authorizationContext = new PageAutorizationContext(page, FrontendsPageConfigs.Group, _orchardServices.WorkContext.CurrentUser);
            _eventHandler.OnAuthorization(authorizationContext);
            return authorizationContext.Granted;
        }
    }
}