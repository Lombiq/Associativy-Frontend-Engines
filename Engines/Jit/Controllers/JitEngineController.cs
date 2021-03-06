﻿using System.Collections.Generic;
using System.Web.Mvc;
using Associativy.Frontends.Controllers;
using Associativy.Frontends.Engines.Jit.ViewModels;
using Associativy.Frontends.EventHandlers;
using Associativy.Frontends.Models;
using Associativy.Frontends.Services;
using Associativy.Queryable;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Engines.Jit.Controllers
{
    [OrchardFeature("Associativy.Frontends.Jit")]
    public class JitEngineController : EngineControllerBase
    {
        private readonly IJitConfigurationHandler _configurationHandler;

        private readonly IEngineContext _engineContext = new EngineContext("Jit");
        public override IEngineContext EngineContext
        {
            get { return _engineContext; }
        }

        public JitEngineController(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IOrchardServices orchardServices,
            IJitConfigurationHandler configurationHandler)
            : base(associativyServices, frontendServices, orchardServices)
        {
            _configurationHandler = configurationHandler;
        }

        [HttpPost]
        public virtual ActionResult FetchGraph(int zoomLevel = 0)
        {
            var page = NewPage("FetchGraph");

            if (page == null) return HttpNotFound();
            if (!IsAuthorized(page)) return new HttpUnauthorizedResult();

            var config = page.As<IEngineConfigurationAspect>();
            var graphSettings = config.GraphSettings;

            _contentManager.UpdateEditor(page, this);

            var graph = page.As<IGraphRetrieverAspect>().RetrieveGraph().Zoom(zoomLevel, graphSettings.ZoomLevelCount).ToGraph().ToContentGraph(config.GraphDescriptor);
            var viewNodes = new Dictionary<int, NodeViewModel>(graph.VertexCount);

            foreach (var vertex in graph.Vertices)
            {
                var viewModel = new NodeViewModel { ContentItem = vertex };
                _configurationHandler.SetupViewModel(new FrontendContext(EngineContext, GraphContext), vertex, viewModel);
                viewNodes[vertex.ContentItem.Id] = viewModel;
            }

            foreach (var edge in graph.Edges)
            {
                viewNodes[edge.Source.ContentItem.Id].adjacencies.Add(edge.Target.ContentItem.Id.ToString());
                viewNodes[edge.Target.ContentItem.Id].adjacencies.Add(edge.Source.ContentItem.Id.ToString());
            }

            return Json(viewNodes.Values);
        }
    }
}