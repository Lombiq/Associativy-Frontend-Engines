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
        private readonly IAssociativyServices _associativyServices;

        protected override string Prefix
        {
            get { return "Associativy.Frontends.GraphPart"; }
        }

        public GraphPartDriver(IAssociativyServices associativyServices)
        {
            _associativyServices = associativyServices;
        }

        protected override DriverResult Display(GraphPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("AssociativyGraph",
                        () =>
                        {
                            if (part.Graph == null || part.Graph.IsVerticesEmpty)
                            {
                                return shapeHelper.DisplayTemplate(
                                        TemplateName: "NotFound",
                                        Model: part.As<SearchFormPart>(),
                                        Prefix: Prefix);
                            }

                            return shapeHelper.DisplayTemplate(
                                        TemplateName: "Engines/" + part.As<EngineCommonPart>().EngineContext.EngineName + "/Graph",
                                        Model: part,
                                        Prefix: Prefix);
                        });
        }

        // GET
        protected override DriverResult Editor(GraphPart part, dynamic shapeHelper)
        {
            return Display(part, "", shapeHelper);
        }

        // POST
        protected override DriverResult Editor(GraphPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}