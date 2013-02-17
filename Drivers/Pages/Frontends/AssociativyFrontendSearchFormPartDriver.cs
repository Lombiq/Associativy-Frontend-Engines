using System.Linq;
using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Models.Mind;
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
                var graphContext = part.As<IEngineConfigurationAspect>().GraphContext;

                if (part.LabelsArray.Length == 0)
                {
                    part.GraphRetrieverField = (settings) =>
                        {
                            return _associativyServices.Mind.GetAllAssociations(graphContext, settings);
                        };
                }
                else
                {
                    var searched = _associativyServices.NodeManager.GetManySimilarNodesQuery(graphContext, part.LabelsArray).List();

                    if (searched.Count() != part.LabelsArray.Length) // Some nodes were not found
                    {
                        part.GraphRetrieverField = EmptyRetriever;
                    }
                    else
                    {
                        if (part.LabelsArray.Length == 1 && part.IsPartialGraph)
                        {
                            part.GraphRetrieverField = (settings) =>
                                {
                                    return _associativyServices.Mind.GetPartialGraph(graphContext, searched.First(), settings);
                                };   
                        }
                        else
                        {
                            part.GraphRetrieverField = (settings) =>
                                {
                                    return _associativyServices.Mind.MakeAssociations(graphContext, searched, settings);
                                };  
                        }
                    }
                }

                // Maybe this should be elsewhere, e.g. in a handler
                _workContextAccessor.GetContext().Layout.Title = T("Associations for {0} - {1}", part.Labels, _associativyServices.GraphManager.FindGraph(graphContext).DisplayGraphName).ToString();
            }
            else part.GraphRetrieverField = (settings) => _associativyServices.GraphEditor.GraphFactory<IContent>();

            return Editor(part, shapeHelper);
        }

        private IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> EmptyRetriever(IMindSettings settings)
        {
            return _associativyServices.GraphEditor.GraphFactory<IContent>();
        }
    }
}