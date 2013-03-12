using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Queryable;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Associativy.Frontends.Drivers.Pages.Frontends
{
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
                                part.GraphField.Loader(() => part.As<IGraphRetrieverAspect>().RetrieveGraph().ToGraph().ToContentGraph(part.As<IEngineConfigurationAspect>().GraphDescriptor)); 
                            }

                            if (part.As<IGraphRetrieverAspect>().RetrieveGraph().NodeCount() == 0)
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