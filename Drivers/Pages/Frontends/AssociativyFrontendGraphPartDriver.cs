using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

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
            return ContentShape("Pages_AssociativyFrontendGraph",
                        () =>
                        {
                            if (part.Graph == null)
                            {
                                part.GraphField.Loader(() => part.As<IGraphRetrieverAspect>().RetrieveContentGraph(part.As<IEngineConfigurationAspect>().MindSettings)); 
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