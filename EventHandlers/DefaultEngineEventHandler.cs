using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

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
            page.ContentItem.Weld(new AssociativyFrontendSearchFormPart
                {
                    GraphRetrieverField = (settings) =>
                        {
                            return _associativyServices.Mind.GetAllAssociations(page.As<IEngineConfigurationAspect>().GraphContext, settings);
                        }
                });

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
                _orchardServices.WorkContext.Layout.Title = T("The whole graph - {0}", _associativyServices.GraphManager.FindGraph(pageContext.Page.As<IEngineConfigurationAspect>().GraphContext).DisplayGraphName).ToString();
            }
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
            authorizationContext.Granted = true;
        }
    }
}