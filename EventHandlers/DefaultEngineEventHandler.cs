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
    public class DefaultEngineEventHandler : IPageEventHandler
    {
        private readonly IWorkContextAccessor _wca;

        public Localizer T { get; set; }


        public DefaultEngineEventHandler(IWorkContextAccessor wca)
        {
            _wca = wca;

            T = NullLocalizer.Instance;
        }


        public void OnPageInitializing(PageContext pageContext)
        {
            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            var page = pageContext.Page;

            page.ContentItem.Weld(new AssociativyFrontendSearchFormPart
                {
                    GraphRetrieverField = () =>
                        {
                            var config = page.As<IEngineConfigurationAspect>();
                            return config.GraphDescriptor.Services.Mind.GetAllAssociations(config.MindSettings).TakeConnections(config.GraphSettings.MaxConnectionCount);
                        }
                });


            var graphPart = new AssociativyFrontendGraphPart();
            graphPart.ZoomLevelCountField.Loader(() => page.As<IGraphRetrieverAspect>().RetrieveGraph().ZoomLevelCount(page.As<IEngineConfigurationAspect>().GraphSettings.ZoomLevelCount));
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
                _wca.GetContext().Layout.Title = T("The whole graph - {0}", pageContext.Page.As<IEngineConfigurationAspect>().GraphDescriptor.DisplayName).ToString();
            }
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
            authorizationContext.Granted = true;
        }
    }
}