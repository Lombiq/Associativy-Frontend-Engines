using Associativy.Frontends.Models.Pages.Frontends;
using Orchard.ContentManagement.Drivers;

namespace Associativy.Frontends.Drivers.Pages.Frontends
{
    public class AssociativyFrontendHeaderPartDriver : ContentPartDriver<AssociativyFrontendHeaderPart>
    {
        protected override string Prefix
        {
            get { return "AssociativyFrontendHeaderPart"; }
        }


        protected override DriverResult Display(AssociativyFrontendHeaderPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Pages_AssociativyFrontendHeader",
                () => shapeHelper.DisplayTemplate(
                                        TemplateName: "Pages/Frontends/Header",
                                        Model: part,
                                        Prefix: Prefix));
        }
    }
}