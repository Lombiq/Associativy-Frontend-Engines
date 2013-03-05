﻿using System;
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

            var mindSettings = page.As<IEngineConfigurationAspect>().MindSettings;

            List<string> graphImageUrls;

            var graph = _associativyServices.GraphManager.FindGraph(GraphContext);
            if (graph == null) return HttpNotFound();

            if (ModelState.IsValid)
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            graph,
                            mindSettings,
                            (currentSettings) =>
                            {
                                return page.As<IGraphRetrieverAspect>().RetrieveContentGraph(mindSettings);
                            });
            }
            else
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            graph,
                            mindSettings,
                            (currentSettings) =>
                            {
                                return graph.Services.NodeManager.MakeContentGraph(graph.Services.Mind.GetAllAssociations(mindSettings));
                            });
            }


            return Json(new { GraphImageUrls = graphImageUrls });
        }

        protected virtual List<string> FetchZoomedGraphUrls(IGraphDescriptor graphDescriptor, IMindSettings settings, Func<IMindSettings, IUndirectedGraph<IContent, IUndirectedEdge<IContent>>> fetchGraph)
        {
            var graphImageUrls = new List<string>(settings.ZoomLevelCount);

            Func<int, string> getImageUrl =
                (zoomLevel) =>
                {
                    settings.ZoomLevel = zoomLevel;
                    return _graphImageService.ToSvg(graphDescriptor, fetchGraph(settings), algorithm =>
                            {
                                algorithm.FormatVertex += _configurationHandler.FormatVertex;
                            });
                };

            graphImageUrls.Add(getImageUrl(0));

            var currentImageUrl = getImageUrl(1);
            int i = 1;
            while (i < settings.ZoomLevelCount && graphImageUrls[i - 1] != currentImageUrl)
            {
                graphImageUrls.Add(currentImageUrl);
                i++;
                currentImageUrl = getImageUrl(i);
            }

            return graphImageUrls;
        }
    }
}