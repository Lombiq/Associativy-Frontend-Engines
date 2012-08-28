using System.Linq;
using System.Web.Mvc;
using Associativy.Frontends.EventHandlers;
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
    public abstract class EngineControllerBase : FrontendControllerBase, IUpdateModel
    {
        protected EngineControllerBase(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IAssociativyFrontendEngineEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
        }

        public virtual ActionResult WholeGraph()
        {
            var page = NewPage("WholeGraph");

            if (!IsAuthorized(page))
            {
                return new HttpUnauthorizedResult();
            }

            _eventHandler.OnPageBuilt(page);

            return new ShapeResult(this, _contentManager.BuildDisplay(page));
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
                _eventHandler.OnPageBuilt(page);

                return new ShapeResult(
                    this,
                    _contentManager.BuildDisplay(page));
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
    }
}
