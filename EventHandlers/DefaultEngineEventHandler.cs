using Associativy.Frontends.Engines;
using Associativy.Frontends.Extensions;
using Associativy.Frontends.Models;
using Associativy.Frontends.Models.Pages.Frontends;
using Associativy.GraphDiscovery;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Core.Common.Models;

namespace Associativy.Frontends.EventHandlers
{
    [OrchardFeature("Associativy.Frontends")]
    public class DefaultEngineEventHandler : IFrontendEngineEventHandler
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

        public void OnPageInitializing(FrontendEventContext frontendEventContext)
        {
            var engineCommonPart = new AssociativyFrontendCommonPart();
            engineCommonPart.GraphContext = frontendEventContext.GraphContext;
            engineCommonPart.EngineContext = frontendEventContext.EngineContext;
            engineCommonPart.MindSettings = new MindSettings
                {
                    ModifyQuery = (query) =>
                        {
                            var recordQuery = query.Where<CommonPartRecord>(r => true);
                            foreach (var contentType in _associativyServices.GraphManager.FindGraph(frontendEventContext.GraphContext).ContentTypes)
                            {
                                recordQuery.WithQueryHintsFor(contentType);
                            }
                        }
                };

            frontendEventContext.Page.ContentItem.Weld(engineCommonPart);

            frontendEventContext.Page.ContentItem.Weld(new AssociativyFrontendSearchFormPart
                {
                    GraphRetrieverField = (settings) =>
                        {
                            return _associativyServices.Mind.GetAllAssociations(engineCommonPart.GraphContext, settings);
                        }
                });

            var graphPart = new AssociativyFrontendGraphPart();
            graphPart.ZoomLevelCountField.Loader(() =>
                {
                    var settings = engineCommonPart.MindSettings.MakeShallowCopy();
                    settings.ZoomLevelCount = 1;
                    settings.ZoomLevel = 0;
                    return _associativyServices.GraphEditor.CalculateZoomLevelCount(graphPart.As<IGraphRetrieverAspect>().RetrieveGraph(settings), graphPart.As<IEngineConfigurationAspect>().MindSettings.ZoomLevelCount);
                });
            frontendEventContext.Page.ContentItem.Weld(graphPart);
        }

        public void OnPageInitialized(FrontendEventContext frontendEventContext)
        {
        }

        public void OnPageBuilt(FrontendEventContext frontendEventContext)
        {
            if (frontendEventContext.Page.IsPage("WholeGraph"))
            {
                _orchardServices.WorkContext.Layout.Title = T("The whole graph - {0}", _associativyServices.GraphManager.FindGraph(frontendEventContext.GraphContext).DisplayGraphName).ToString();
            }
        }

        public void OnAuthorization(FrontendAuthorizationEventContext frontendAuthorizationEventContext)
        {
            frontendAuthorizationEventContext.Granted = true;
        }
    }
}