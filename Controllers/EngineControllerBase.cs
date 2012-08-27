﻿using System.Linq;
using System.Web.Mvc;
using Associativy.Frontends.Engines;
using Associativy.Frontends.EventHandlers;
using Associativy.Frontends.Extensions;
using Associativy.Frontends.Services;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;

namespace Associativy.Frontends.Controllers
{
    /// <summary>
    /// Base class for frontend engine controllers
    /// </summary>
    [Themed, OrchardFeature("Associativy.Frontends")]
    public abstract class EngineControllerBase : DynamicallyContextedControllerBase, IUpdateModel
    {
        protected readonly IAssociativyFrontendEngineEventHandler _eventHandler;
        protected readonly IOrchardServices _orchardServices;
        protected readonly IContentManager _contentManager;

        abstract protected IEngineContext EngineContext { get; }

        public Localizer T { get; set; }

        protected EngineControllerBase(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IAssociativyFrontendEngineEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices)
        {
            _eventHandler = eventHandler;
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;

            T = NullLocalizer.Instance;
        }

        public virtual ActionResult WholeGraph()
        {
            var page = NewPage("WholeGraph");

            if (!IsAuthorized(page))
            {
                return new HttpUnauthorizedResult();
            }

            _eventHandler.OnPageBuilt(FrontendEventContext(page));

            return new ShapeResult(this, _contentManager.BuildEnginePageDisplay(GraphContext, page));
        }

        public virtual ActionResult Associations()
        {
            var page = NewPage("Associations");

            if (!IsAuthorized(page))
            {
                return new HttpUnauthorizedResult();
            }

            _contentManager.UpdateEditor(page, this);

            if (ModelState.IsValid)
            {
                _eventHandler.OnPageBuilt(FrontendEventContext(page));

                return new ShapeResult(
                    this,
                    _contentManager.BuildEnginePageDisplay(GraphContext, page));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage))
                {
                    //_notifier.Error(T(error));
                }

                return null;
            }
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }

        protected virtual IContent NewPage(string pageName)
        {
            var page = _contentManager.NewEnginePage(EngineContext, pageName);

            _eventHandler.OnPageInitializing(FrontendEventContext(page));
            _eventHandler.OnPageInitialized(FrontendEventContext(page));

            return page;
        }

        protected virtual FrontendEventContext FrontendEventContext(IContent page)
        {
            return new FrontendEventContext(page, EngineContext, GraphContext);
        }

        protected virtual bool IsAuthorized(IContent page)
        {
            var authorizationContext = new FrontendAuthorizationEventContext(_orchardServices.WorkContext.CurrentUser, page, EngineContext, GraphContext);
            _eventHandler.OnAuthorization(authorizationContext);
            return authorizationContext.Granted;
        }
    }
}
