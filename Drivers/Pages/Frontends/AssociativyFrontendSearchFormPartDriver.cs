using System.Linq;
using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Models.Services;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using QuickGraph;

namespace Associativy.Frontends.Drivers.Pages.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public class AssociativyFrontendSearchFormPartDriver : ContentPartDriver<AssociativyFrontendSearchFormPart>
    {
        private readonly IAssociativyServices _associativyServices;
        private readonly IWorkContextAccessor _workContextAccessor;

        protected override string Prefix
        {
            get { return "Associativy.SearchForm"; }
        }

        public Localizer T { get; set; }


        public AssociativyFrontendSearchFormPartDriver(
            IAssociativyServices associativyServices,
            IWorkContextAccessor workContextAccessor)
        {
            _associativyServices = associativyServices;
            _workContextAccessor = workContextAccessor;

            T = NullLocalizer.Instance;
        }


        protected override DriverResult Display(AssociativyFrontendSearchFormPart part, string displayType, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }

        // GET
        protected override DriverResult Editor(AssociativyFrontendSearchFormPart part, dynamic shapeHelper)
        {
            return ContentShape("Pages_AssociativyFrontendSearchForm",
                        () => shapeHelper.DisplayTemplate(
                                TemplateName: "Pages/Frontends/FrontendSearchForm",
                                Model: part,
                                Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(AssociativyFrontendSearchFormPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                var graph = _associativyServices.GraphManager.FindGraph(part.As<IEngineConfigurationAspect>().GraphContext);
                if (graph == null) return null;

                part.ContentGraphRetrieverField = (settings) =>
                {
                    return graph.Services.NodeManager.MakeContentGraph(part.RetrieveGraph(settings));
                };

                if (part.LabelsArray.Length == 0)
                {
                    part.GraphRetrieverField = (settings) =>
                    {
                        return graph.Services.Mind.GetAllAssociations(settings).ToGraph();
                    };
                }
                else
                {
                    var searched = graph.Services.NodeManager.GetManyByLabelQuery(part.LabelsArray).List();

                    if (searched.Count() != part.LabelsArray.Length) // Some nodes were not found
                    {
                        part.GraphRetrieverField = EmptyRetriever;
                        part.ContentGraphRetrieverField = EmptyContentRetriever;
                    }
                    else
                    {
                        if (part.LabelsArray.Length == 1 && part.IsPartialGraph)
                        {
                            part.GraphRetrieverField = (settings) =>
                            {
                                return graph.Services.Mind.GetPartialGraph(searched.First(), settings);
                            };
                        }
                        else
                        {
                            part.GraphRetrieverField = (settings) =>
                            {
                                return graph.Services.Mind.MakeAssociations(searched, settings);
                            };
                        }
                    }
                }

                // Maybe this should be elsewhere, e.g. in a handler
                _workContextAccessor.GetContext().Layout.Title = T("Associations for {0} - {1}", part.Labels, graph.DisplayName).ToString();
            }
            else
            {
                part.GraphRetrieverField = EmptyRetriever;
                part.ContentGraphRetrieverField = EmptyContentRetriever;
            }

            return Editor(part, shapeHelper);
        }

        private IMutableUndirectedGraph<int, IUndirectedEdge<int>> EmptyRetriever(IMindSettings settings)
        {
            return _associativyServices.GraphEditor.GraphFactory<int>();
        }

        private IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> EmptyContentRetriever(IMindSettings settings)
        {
            return _associativyServices.GraphEditor.GraphFactory<IContent>();
        }
    }
}