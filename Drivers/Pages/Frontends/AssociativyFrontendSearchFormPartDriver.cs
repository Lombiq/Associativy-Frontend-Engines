using System.Linq;
using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Models.Services;
using Associativy.Queryable;
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
                var config = part.As<IEngineConfigurationAspect>();
                var graph = config.GraphDescriptor;


                if (part.LabelsArray.Length == 0)
                {
                    part.GraphRetrieverField = () =>
                    {
                        return graph.Services.Mind.GetAllAssociations(config.MindSettings).TakeConnections(config.GraphSettings.MaxConnectionCount);
                    };
                }
                else
                {
                    var searched = graph.Services.NodeManager.GetManyByLabelQuery(part.LabelsArray).List();

                    if (searched.Count() != part.LabelsArray.Length) // Some nodes were not found
                    {
                        part.GraphRetrieverField = EmptyRetriever;
                    }
                    else
                    {
                        if (part.LabelsArray.Length == 1 && part.IsPartialGraph)
                        {
                            part.GraphRetrieverField = () =>
                            {
                                return graph.Services.PathFinder
                                    .GetPartialGraph(searched.First(), new PathFinderSettings { MaxDistance = config.MindSettings.MaxDistance, UseCache = config.MindSettings.UseCache })
                                    .TakeConnections(config.GraphSettings.MaxConnectionCount);
                            };
                        }
                        else
                        {
                            part.GraphRetrieverField = () =>
                            {
                                return graph.Services.Mind.MakeAssociations(searched, config.MindSettings).TakeConnections(config.GraphSettings.MaxConnectionCount);
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
            }

            return Editor(part, shapeHelper);
        }

        private IQueryableGraph<int> EmptyRetriever()
        {
            return _associativyServices.QueryableGraphFactory.Create<int>((parameters) =>
                {
                    if (parameters.Method == ExecutionMethod.ToGraph) return _associativyServices.GraphEditor.GraphFactory<int>();
                    return 0;
                });
        }
    }
}