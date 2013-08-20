using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Models.Pages.Frontends;
using Orchard.ContentManagement.Drivers;

namespace Associativy.Frontends.Engines.Jit.Drivers
{
    public class AssociativyFrontendGraphPartDriver : ContentPartDriver<AssociativyFrontendGraphPart>
    {
        protected override DriverResult Display(AssociativyFrontendGraphPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Pages_AssociativyFrontendGraph_Jit",
                        () => shapeHelper.DisplayTemplate(
                                TemplateName: "Pages/Frontends/Engines/Jit/AssociativyFrontendGraph",
                                Model: part,
                                Prefix: Prefix));
        }
    }
}