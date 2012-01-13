using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Models;

namespace Associativy.FrontendEngines.Drivers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class SearchFormPartDriver : ContentPartDriver<SearchFormPart>
    {
        protected override string Prefix
        {
            get { return "Associativy.SearchForm"; }
        }

        protected override DriverResult Display(SearchFormPart part, string displayType, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }

        // GET
        protected override DriverResult Editor(SearchFormPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_SearchForm",
                () => shapeHelper.EditorTemplate(
                        TemplateName: "Parts/SearchForm",
                        Model: part,
                        Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(SearchFormPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}