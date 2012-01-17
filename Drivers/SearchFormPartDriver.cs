using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Models;
using Associativy.Models;

namespace Associativy.FrontendEngines.Drivers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class SearchFormPartDriver : ContentPartDriver<SearchFormPart>
    {
        private readonly IGraphDescriptor _graphDescriptor;

        protected override string Prefix
        {
            get { return "Associativy.SearchForm"; }
        }

        public SearchFormPartDriver(IGraphDescriptor graphDescriptor)
        {
            _graphDescriptor = graphDescriptor;
        }

        protected override DriverResult Display(SearchFormPart part, string displayType, dynamic shapeHelper)
        {
            part.GraphDescriptor = _graphDescriptor;

            return Editor(part, shapeHelper);
        }

        // GET
        protected override DriverResult Editor(SearchFormPart part, dynamic shapeHelper)
        {
            part.GraphDescriptor = _graphDescriptor;

            return ContentShape("Parts_SearchForm",
                () => shapeHelper.EditorTemplate(
                        TemplateName: "Parts/SearchForm",
                        Model: part,
                        Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(SearchFormPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            part.GraphDescriptor = _graphDescriptor;
            
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}