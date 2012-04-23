using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Models;
using Associativy.Models;
using Associativy.Services;
using System.Linq;

namespace Associativy.Frontends.Drivers
{
    [OrchardFeature("Associativy.Frontends")]
    public class GraphPartDriver : ContentPartDriver<GraphPart>
    {
        protected override string Prefix
        {
            get { return "Associativy.Frontends.GraphPart"; }
        }

        protected override DriverResult Display(GraphPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("AssociativyGraph",
                        () =>
                        {
                            if (part.Graph == null)
                            {
                                part.Graph = part.As<IGraphRetrieverAspect>().RetrieveGraph(part.As<IEngineConfigurationAspect>().MindSettings);
                            }

                            if (part.Graph.IsVerticesEmpty)
                            {
                                return shapeHelper.DisplayTemplate(
                                        TemplateName: "NotFound",
                                        Model: part,
                                        Prefix: Prefix);
                            }

                            return shapeHelper.DisplayTemplate(
                                        TemplateName: "Engines/" + part.As<IEngineConfigurationAspect>().EngineContext.EngineName + "/Graph",
                                        Model: part,
                                        Prefix: Prefix);
                        });
        }
    }
}