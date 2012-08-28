using Associativy.Frontends.Models.Pages.Frontends;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Jit.Drivers
{
    [OrchardFeature("Associativy.Frontends.Jit")]
    public class AssociativyFrontendSearchFormPartDriver : ContentPartDriver<AssociativyFrontendSearchFormPart>
    {
        protected override string Prefix
        {
            get { return "Associativy.SearchForm"; }
        }

        protected override DriverResult Display(AssociativyFrontendSearchFormPart part, string displayType, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }

        // GET
        protected override DriverResult Editor(AssociativyFrontendSearchFormPart part, dynamic shapeHelper)
        {
            return ContentShape("Pages_AssociativyFrontendSearchForm_Jit",
                        () => shapeHelper.DisplayTemplate(
                                TemplateName: "Pages/Frontends/Engines/Jit/FrontendSearchForm",
                                Model: part,
                                Prefix: Prefix));
        }
    }
}