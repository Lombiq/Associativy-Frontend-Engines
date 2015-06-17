using System.Linq;
using System.Web.Mvc;
using Associativy.Frontends.Services;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

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
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices, orchardServices)
        {
        }


        public virtual ActionResult WholeGraph()
        {
            var page = NewPage("WholeGraph");

            if (page == null) return HttpNotFound();
            if (!IsAuthorized(page)) return new HttpUnauthorizedResult();

            return new ShapeResult(this, _contentManager.BuildFrontendPageDisplay(page, GraphContext));
        }

        public virtual ActionResult Associations()
        {
            var page = NewPage("Associations");

            if (page == null) return HttpNotFound();
            if (!IsAuthorized(page)) return new HttpUnauthorizedResult();

            _contentManager.UpdateEditor(page, this);

            if (ModelState.IsValid)
            {
                return new ShapeResult(
                    this,
                    _contentManager.BuildFrontendPageDisplay(page, GraphContext));
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
