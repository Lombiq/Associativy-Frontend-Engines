using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using Associativy.Frontends.Controllers;
using Associativy.Frontends.Engines.Graphviz.Services;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Tasks;
using QuickGraph;
using Associativy.Frontends.Models;
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Engines.Graphviz.Controllers
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class GraphvizEngineController : EngineControllerBase<IGraphvizConfigurationProvider>
    {
        protected readonly IDetachedDelegateBuilder _detachedDelegateBuilder;
        protected readonly IGraphImageService _graphImageService;

        protected override IEngineContext EngineContext
        {
            get { return DefaultGraphvizConfigurationProvider.DescribedEngineContext; }
        }

        public GraphvizEngineController(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IEngineConfigurationManager configurationManager,
            IDetachedDelegateBuilder detachedDelegateBuilder,
            IGraphImageService graphImageService)
            : base(associativyServices, orchardServices, configurationManager)
        {
            _detachedDelegateBuilder = detachedDelegateBuilder;
            _graphImageService = graphImageService;
        }

        public void Index()
        {
            var count = 2;
            var settings = ConfigurationProvider.MakeDefaultMindSettings();

            var sw = new Stopwatch();
            sw.Start();

            var graphs = new IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>[count];
            for (int i = 0; i < count; i++)
            {
                graphs[i] = _mind.GetAllAssociations(GraphContext, settings);
            }

            sw.Stop();
            var z = sw.ElapsedMilliseconds;
            sw.Restart();

            var tasks = new Task<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>>[count];
            for (int i = 0; i < count; i++)
            {
                tasks[i] = Task<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>>.Factory.StartNew(
                    _detachedDelegateBuilder.BuildBackgroundFunction<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>>(
                        () => _mind.GetAllAssociations(GraphContext, settings)
                    )
                    );
            }
            Task.WaitAll(tasks);

            sw.Stop();
            var y = sw.ElapsedMilliseconds;
            int ze = 5 + 5;
        }

        public virtual JsonResult Render()
        {
            var page = NewPage("Render");
            _contentManager.UpdateEditor(page, this);

            var settings = ConfigurationProvider.MakeDefaultMindSettings();

            List<string> graphImageUrls;

            if (ModelState.IsValid)
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            settings,
                            (currentSettings) =>
                            {
                                return RetrieveSearchedGraph(page, settings);
                            });
            }
            else
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            settings,
                            (currentSettings) =>
                            {
                                return _mind.GetAllAssociations(GraphContext, settings);
                            });
            }


            return Json(new { GraphImageUrls = graphImageUrls }, JsonRequestBehavior.AllowGet);
        }

        protected virtual List<string> FetchZoomedGraphUrls(IMindSettings settings, Func<IMindSettings, IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>> fetchGraph)
        {
            var graphImageUrls = new List<string>(settings.MaxZoomLevel);

            Func<int, string> getImageUrl =
                (zoomLevel) =>
                {
                    settings.ZoomLevel = zoomLevel;
                    return _graphImageService.ToSvg(GraphContext, fetchGraph(settings), algorithm =>
                            {
                                algorithm.FormatVertex += ConfigurationProvider.FormatVertext;
                            });
                };

            graphImageUrls.Add(getImageUrl(0));

            var currentImageUrl = getImageUrl(1);
            int i = 1;
            while (i < settings.MaxZoomLevel && graphImageUrls[i - 1] != currentImageUrl)
            {
                graphImageUrls.Add(currentImageUrl);
                i++;
                currentImageUrl = getImageUrl(i);
            }

            return graphImageUrls;
        }
    }
}