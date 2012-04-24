using Associativy.Frontends.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Models.Pages.Frontends;

namespace Associativy.Frontends.Drivers.Pages.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public class AssociativyFrontendGraphPartDriver : ContentPartDriver<AssociativyFrontendGraphPart>
    {
        protected override string Prefix
        {
            get { return "Associativy.Frontends.GraphPart"; }
        }

        protected override DriverResult Display(AssociativyFrontendGraphPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Page_AssociativyFrontendGraph",
                        () =>
                        {
                            if (part.Graph == null)
                            {
                                part.Graph = part.As<IGraphRetrieverAspect>().RetrieveGraph(part.As<IEngineConfigurationAspect>().MindSettings);
                            }

                            if (part.Graph.IsVerticesEmpty)
                            {
                                return shapeHelper.DisplayTemplate(
                                        TemplateName: "Pages/Frontends/NotFound",
                                        Model: part,
                                        Prefix: Prefix);
                            }

                            return shapeHelper.DisplayTemplate(
                                        TemplateName: "Pages/Frontends/Engines/" + part.As<IEngineConfigurationAspect>().EngineContext.EngineName + "/Graph",
                                        Model: part,
                                        Prefix: Prefix);
                        });
        }
    }
}