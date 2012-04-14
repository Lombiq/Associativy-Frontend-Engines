using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Models;
using Associativy.Models;

namespace Associativy.Frontends.Drivers
{
    [OrchardFeature("Associativy.Frontends")]
    public class AssociativySearchFormPartDriver : ContentPartDriver<AssociativySearchFormPart>
    {
        protected override string Prefix
        {
            get { return "Associativy.SearchForm"; }
        }

        protected override DriverResult Display(AssociativySearchFormPart part, string displayType, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }

        // GET
        protected override DriverResult Editor(AssociativySearchFormPart part, dynamic shapeHelper)
        {
            //return ContentShape("Parts_SearchForm",
            //            () => shapeHelper.EditorTemplate(
            //                    TemplateName: "Parts/SearchForm",
            //                    Model: part,
            //                    Prefix: Prefix));

            return ContentShape("AssociativySearchForm",
                        () => shapeHelper.DisplayTemplate(
                                TemplateName: "SearchForm",
                                Model: part,
                                Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(AssociativySearchFormPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}