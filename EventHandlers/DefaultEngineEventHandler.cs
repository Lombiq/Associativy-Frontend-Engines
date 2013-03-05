using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Associativy.Models.Services;
using QuickGraph;

namespace Associativy.Frontends.EventHandlers
{
    [OrchardFeature("Associativy.Frontends")]
    public class DefaultEngineEventHandler : IPageEventHandler
    {
        private readonly IAssociativyServices _associativyServices;
        private readonly IOrchardServices _orchardServices;

        public Localizer T { get; set; }


        public DefaultEngineEventHandler(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices)
        {
            _associativyServices = associativyServices;
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
        }


        public void OnPageInitializing(PageContext pageContext)
        {
            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            var page = pageContext.Page;

            var searchFormPart = new AssociativyFrontendSearchFormPart
                {
                    GraphRetrieverField = (settings) =>
                        {
                            var graph = _associativyServices.GraphManager.FindGraph(page.As<IEngineConfigurationAspect>().GraphContext);
                            if (graph == null) return _associativyServices.GraphEditor.GraphFactory<int>();
                            return graph.Services.Mind.GetAllAssociations(settings);
                        }
                };
            searchFormPart.ContentGraphRetrieverField = (settings) =>
            {
                var graph = _associativyServices.GraphManager.FindGraph(page.As<IEngineConfigurationAspect>().GraphContext);
                if (graph == null) return _associativyServices.GraphEditor.GraphFactory<IContent>();
                return graph.Services.NodeManager.MakeContentGraph(searchFormPart.RetrieveGraph(settings));
            };
            page.ContentItem.Weld(searchFormPart);


            var graphPart = new AssociativyFrontendGraphPart();
            graphPart.ZoomLevelCountField.Loader(() =>
                {
                    var settings = page.As<IEngineConfigurationAspect>().MindSettings.MakeShallowCopy();
                    settings.ZoomLevelCount = 1;
                    settings.ZoomLevel = 0;
                    return _associativyServices.GraphEditor.CalculateZoomLevelCount(graphPart.As<IGraphRetrieverAspect>().RetrieveGraph(settings), graphPart.As<IEngineConfigurationAspect>().MindSettings.ZoomLevelCount);
                });
            page.ContentItem.Weld(graphPart);
        }

        public void OnPageInitialized(PageContext pageContext)
        {
        }

        public void OnPageBuilt(PageContext pageContext)
        {
            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            if (pageContext.Page.IsPage(pageContext.Page.As<IEngineConfigurationAspect>().EngineContext.EngineName + "WholeGraph", pageContext.Group))
            {
                _orchardServices.WorkContext.Layout.Title = T("The whole graph - {0}", _associativyServices.GraphManager.FindGraph(pageContext.Page.As<IEngineConfigurationAspect>().GraphContext).Name).ToString();
            }
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
            authorizationContext.Granted = true;
        }
    }
}