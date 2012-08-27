using Associativy.Controllers;
using Associativy.Frontends.Services;
using Associativy.Services;
using Associativy.GraphDiscovery;
using System;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Associativy.Frontends.EventHandlers;
using Orchard;
using Associativy.Frontends.Engines;
using Orchard.Localization;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Models.Mind;
using Orchard.Core.Common.Models;

namespace Associativy.Frontends.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class FrontendControllerBase : AssociativyControllerBase
    {
        protected readonly IFrontendServices _frontendServices;
        protected readonly IFrontendContextAccessor _frontendContextAccessor;
        protected readonly IAssociativyFrontendEngineEventHandler _eventHandler;
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
            IAssociativyFrontendEngineEventHandler eventHandler,
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
            var page = _contentManager.NewPage(
                EngineContext.EngineName + pageName, 
                (content) =>
                    {
                        var engineCommonPart = new AssociativyFrontendCommonPart();
                        engineCommonPart.GraphContext = GraphContext;
                        engineCommonPart.EngineContext = EngineContext;
                        engineCommonPart.MindSettings = new MindSettings
                        {
                            ModifyQuery = (query) =>
                            {
                                var recordQuery = query.Where<CommonPartRecord>(r => true);
                                foreach (var contentType in _associativyServices.GraphManager.FindGraph(GraphContext).ContentTypes)
                                {
                                    recordQuery.WithQueryHintsFor(contentType);
                                }
                            }
                        };

                        content.ContentItem.Weld(engineCommonPart);
                    },
                _eventHandler);

            return page;
        }

        protected virtual bool IsAuthorized(IContent page)
        {
            var authorizationContext = new PageAutorizationContext(page, _orchardServices.WorkContext.CurrentUser);
            _eventHandler.OnAuthorization(authorizationContext);
            return authorizationContext.Granted;
        }
    }
}