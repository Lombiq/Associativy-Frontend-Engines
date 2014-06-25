using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Piedone.HelpfulLibraries.Contents;

namespace Associativy.Frontends.Handlers
{
    public class DefaultEnginePageHandler : ContentHandler
    {
        private readonly IWorkContextAccessor _wca;

        public Localizer T { get; set; }


        public DefaultEnginePageHandler(IWorkContextAccessor wca)
        {
            _wca = wca;

            T = NullLocalizer.Instance;
        }


        protected override void Initializing(InitializingContentContext context)
        {
            var pageContext = context.PageContext();

            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            pageContext.Page.Weld<AssociativyFrontendHeaderPart>();

            pageContext.Page.Weld<AssociativyFrontendSearchFormPart>(part =>
                part.GraphRetrieverField = () =>
                {
                    var config = pageContext.Page.As<IEngineConfigurationAspect>();
                    return config.GraphDescriptor.Services.Mind.GetAllAssociations(config.MindSettings).TakeConnections(config.GraphSettings.MaxConnectionCount);
                });

            pageContext.Page.Weld<AssociativyFrontendGraphPart>(part => part.ZoomLevelCountField.Loader(() => pageContext.Page.As<IGraphRetrieverAspect>().RetrieveGraph().ZoomLevelCount(pageContext.Page.As<IEngineConfigurationAspect>().GraphSettings.ZoomLevelCount)));
        }

        protected override void Initialized(InitializingContentContext context)
        {
            var pageContext = context.PageContext();

            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            if (pageContext.Page.IsPage(pageContext.Page.As<IEngineConfigurationAspect>().EngineContext.EngineName + "WholeGraph", pageContext.Group))
            {
                _wca.GetContext().Layout.Title = T("The whole graph - {0}", pageContext.Page.As<IEngineConfigurationAspect>().GraphDescriptor.DisplayName).ToString();
            }
        }
    }
}