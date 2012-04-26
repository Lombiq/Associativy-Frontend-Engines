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

        public void OnPageInitializing(FrontendContext frontendContext, IContent page)
        {
            var engineCommonPart = new AssociativyFrontendCommonPart();
            engineCommonPart.GraphContext = frontendContext.GraphContext;
            engineCommonPart.EngineContext = frontendContext.EngineContext;
            engineCommonPart.MindSettings = new MindSettings
                {
                    ModifyQuery = (query) =>
                        {
                            var recordQuery = query.Where<CommonPartRecord>(r => true);
                            foreach (var contentType in _associativyServices.GraphManager.FindGraph(frontendContext.GraphContext).ContentTypes)
                            {
                                recordQuery.WithQueryHintsFor(contentType);
                            }
                        }
                };

            page.ContentItem.Weld(engineCommonPart);

            page.ContentItem.Weld(new AssociativyFrontendSearchFormPart
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
            page.ContentItem.Weld(graphPart);
        }

        public void OnPageInitialized(FrontendContext frontendContext, IContent page)
        {
        }


        public void OnPageBuilt(FrontendContext frontendContext, IContent page)
        {
            if (page.IsPage("WholeGraph"))
            {
                _orchardServices.WorkContext.Layout.Title = T("The whole graph - {0}", _associativyServices.GraphManager.FindGraph(frontendContext.GraphContext).DisplayGraphName).ToString();
            }
        }
    }
}