using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Associativy.Frontends.Controllers;
using Associativy.Frontends.Engines.Graphviz.Services;
using Associativy.Frontends.Models;
using Associativy.Frontends.Services;
using Associativy.GraphDiscovery;
using Associativy.Models.Services;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using QuickGraph;
using Associativy.Queryable;

namespace Associativy.Frontends.Engines.Graphviz.Controllers
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class GraphvizEngineController : EngineControllerBase
    {
        private readonly IGraphvizConfigurationHandler _configurationHandler;
        private readonly IGraphImageService _graphImageService;

        private readonly IEngineContext _engineContext = new EngineContext("Graphviz");
        protected override IEngineContext EngineContext
        {
            get { return _engineContext; }
        }

        public GraphvizEngineController(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IPageEventHandler eventHandler,
            IOrchardServices orchardServices,
            IGraphvizConfigurationHandler configurationHandler,
            IGraphImageService graphImageService)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
            _configurationHandler = configurationHandler;
            _graphImageService = graphImageService;
        }

        [HttpPost]
        public virtual ActionResult Render()
        {
            var page = NewPage("Render");

            if (!IsAuthorized(page))
            {
                return new HttpUnauthorizedResult();
            }

            _contentManager.UpdateEditor(page, this);

            List<string> graphImageUrls;

            var config = page.As<IEngineConfigurationAspect>();
            if (ModelState.IsValid)
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            config,
                            (zoomLevel) =>
                            {
                                return page.As<IGraphRetrieverAspect>().RetrieveGraph().Zoom(zoomLevel, config.GraphSettings.ZoomLevelCount).ToGraph().ToContentGraph(config.GraphDescriptor);
                            });
            }
            else
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            config,
                            (zoomLevel) =>
                            {
                                return config.GraphDescriptor.Services.Mind.GetAllAssociations(config.MindSettings).Zoom(zoomLevel, config.GraphSettings.ZoomLevelCount).ToGraph().ToContentGraph(config.GraphDescriptor);
                            });
            }


            return Json(new { GraphImageUrls = graphImageUrls });
        }

        protected virtual List<string> FetchZoomedGraphUrls(IEngineConfigurationAspect config, Func<int, IUndirectedGraph<IContent, IUndirectedEdge<IContent>>> fetchGraph)
        {
            var graphImageUrls = new List<string>();

            Func<int, string> getImageUrl =
                (zoomLevel) =>
                {
                    return _graphImageService.ToSvg(config.GraphDescriptor, fetchGraph(zoomLevel), algorithm =>
                            {
                                algorithm.FormatVertex += _configurationHandler.FormatVertex;
                            });
                };

            graphImageUrls.Add(getImageUrl(0));

            var currentImageUrl = getImageUrl(1);
            int i = 1;
            while (i < config.GraphSettings.ZoomLevelCount && graphImageUrls[i - 1] != currentImageUrl)
            {
                graphImageUrls.Add(currentImageUrl);
                i++;
                currentImageUrl = getImageUrl(i);
            }

            return graphImageUrls;
        }
    }
}