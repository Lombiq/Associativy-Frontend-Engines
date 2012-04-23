using System.Linq;
using Associativy.Frontends.Models;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using QuickGraph;

namespace Associativy.Frontends.Drivers
{
    [OrchardFeature("Associativy.Frontends")]
    public class AssociativySearchFormPartDriver : ContentPartDriver<AssociativySearchFormPart>
    {
        private readonly IAssociativyServices _associativyServices;
        private readonly IWorkContextAccessor _workContextAccessor;

        protected override string Prefix
        {
            get { return "Associativy.SearchForm"; }
        }

        public Localizer T { get; set; }

        public AssociativySearchFormPartDriver(
            IAssociativyServices associativyServices,
            IWorkContextAccessor workContextAccessor)
        {
            _associativyServices = associativyServices;
            _workContextAccessor = workContextAccessor;

            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(AssociativySearchFormPart part, string displayType, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }

        // GET
        protected override DriverResult Editor(AssociativySearchFormPart part, dynamic shapeHelper)
        {
            return ContentShape("AssociativySearchForm",
                        () => shapeHelper.DisplayTemplate(
                                TemplateName: "SearchForm",
                                Model: part,
                                Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(AssociativySearchFormPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                if (part.LabelsArray.Length == 0)
                {
                    part.GraphRetrieverField = EmptyRetriever;
                }
                else
                {
                    var searched = _associativyServices.NodeManager.GetMany(part.As<IEngineConfigurationAspect>().GraphContext, part.LabelsArray);

                    if (searched.Count() != part.LabelsArray.Length) // Some nodes were not found
                    {
                        part.GraphRetrieverField = EmptyRetriever;
                    }
                    else
                    {
                        part.GraphRetrieverField = (settings) =>
                            {
                                return _associativyServices.Mind.MakeAssociations(part.As<IEngineConfigurationAspect>().GraphContext, searched, settings);
                            };  
                    }
                }

                // Maybe this should be elsewhere, e.g. in a handler
                _workContextAccessor.GetContext().Layout.Title = T("Associations for {0} - {1}", part.Labels, _associativyServices.GraphManager.FindGraph(part.As<IEngineConfigurationAspect>().GraphContext).DisplayGraphName).ToString();
            }
            else part.GraphRetrieverField = (settings) => _associativyServices.GraphEditor.GraphFactory();

            return Editor(part, shapeHelper);
        }

        private IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> EmptyRetriever(IMindSettings settings)
        {
            return _associativyServices.GraphEditor.GraphFactory();
        }
    }
}