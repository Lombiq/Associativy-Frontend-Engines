using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using Associativy.FrontendEngines.Controllers;
using Associativy.FrontendEngines.Engines.Graphviz.Services;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Tasks;
using QuickGraph;
using Associativy.FrontendEngines.Shapes;
using Associativy.FrontendEngines.Engines.Graphviz.Models;
using Associativy.FrontendEngines.Models;

namespace Associativy.FrontendEngines.Engines.Graphviz.Controllers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class GraphvizFrontendEngineController : FrontendEngineControllerBase
    {
        protected readonly IGraphvizSetup _setup;
        protected readonly IDetachedDelegateBuilder _detachedDelegateBuilder;
        protected readonly IGraphImageService _graphImageService;

        protected override IFrontendEngineContext FrontendEngineContext
        {
            get { return new GraphvizContext(); }
        }

        public GraphvizFrontendEngineController(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IFrontendShapes frontendShapes,
            IShapeFactory shapeFactory,
            IGraphvizSetup setup,
            IDetachedDelegateBuilder detachedDelegateBuilder,
            IGraphImageService graphImageService)
            : base(associativyServices, orchardServices, frontendShapes, shapeFactory, setup)
        {
            _setup = setup;
            _detachedDelegateBuilder = detachedDelegateBuilder;
            _graphImageService = graphImageService;

            _graphShapeTemplateName = "Engines/Graphviz/Graph.OpenLayers";
        }

        public void Index()
        {
            var count = 2;
            var settings = MakeDefaultMindSettings();

            var sw = new Stopwatch();
            sw.Start();

            var graphs = new IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>[count];
            for (int i = 0; i < count; i++)
            {
                graphs[i] = _mind.GetAllAssociations(settings);
            }

            sw.Stop();
            var z = sw.ElapsedMilliseconds;
            sw.Restart();

            var tasks = new Task<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>>[count];
            for (int i = 0; i < count; i++)
            {
                tasks[i] = Task<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>>.Factory.StartNew(
                    _detachedDelegateBuilder.BuildBackgroundFunction<IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>>(
                        () => _mind.GetAllAssociations(settings)
                    )
                    );
            }
            Task.WaitAll(tasks);

            sw.Stop();
            var y = sw.ElapsedMilliseconds;
            int ze = 5 + 5;
        }

        [RequireRequestValue("engine")]
        public virtual ActionResult ShowWholeGraph(string engine)
        {
            SetEngineToUse(engine);

            return base.ShowWholeGraph();
        }

        [RequireRequestValue("engine")]
        public virtual ActionResult ShowAssociations(string engine)
        {
            SetEngineToUse(engine);

            return base.ShowAssociations();
        }

        public virtual JsonResult Render()
        {
            var searchForm = _contentManager.New(FrontendEngineContext.SearchFormContentType);
            _contentManager.UpdateEditor(searchForm, this);

            var settings = MakeDefaultMindSettings();

            List<string> graphImageUrls;

            if (ModelState.IsValid)
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            settings,
                            (currentSettings) =>
                            {
                                IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> currentGraph;
                                TryGetGraph(searchForm, out currentGraph, settings, _setup.GraphQueryModifier);
                                return currentGraph;
                            });
            }
            else
            {
                graphImageUrls = FetchZoomedGraphUrls(
                            settings,
                            (currentSettings) =>
                            {
                                return _mind.GetAllAssociations(settings);
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
                    return _graphImageService.ToSvg(fetchGraph(settings), algorithm =>
                            {
                                algorithm.FormatVertex += _setup.VertexFormatter;
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

        protected virtual void SetEngineToUse(string engineParam)
        {
            // This is to ensure that no arbitrary shape template can be set from a GET param
            string engineName = "OpenLayers";

            if (!String.IsNullOrEmpty(engineParam))
            {
                switch (engineParam)
                {
                    case "Mapz":
                        engineName = "Mapz";
                        break;
                    case "Mapbox":
                        engineName = "Mapbox";
                        break;
                }
            }

            _graphShapeTemplateName = "Engines/Graphviz/Graph." + engineName;
        }
    }

    [OrchardFeature("Associativy.FrontendEngines")]
    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        public RequireRequestValueAttribute(string valueName)
        {
            ValueName = valueName;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return (controllerContext.HttpContext.Request[ValueName] != null);
        }

        public string ValueName { get; private set; }
    }
}