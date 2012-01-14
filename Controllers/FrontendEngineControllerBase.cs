using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Associativy.Controllers;
using Associativy.Models;
using Associativy.Models.Mind;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Routable.Models;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using QuickGraph;
using Associativy.FrontendEngines.Shapes;
using System.Diagnostics;
using Associativy.FrontendEngines.Models;

namespace Associativy.FrontendEngines.Controllers
{
    /// <summary>
    /// Base class for frontend engine controllers
    /// </summary>
    [Themed]
    [OrchardFeature("Associativy.FrontendEngines")]
    public abstract class FrontendEngineControllerBase : AssociativyControllerBase, IUpdateModel
    {
        protected readonly IOrchardServices _orchardServices;
        protected readonly IContentManager _contentManager;
        protected readonly IFrontendShapes _frontendShapes;
        protected readonly dynamic _shapeFactory;
        private readonly IFrontendEngineSetup _setup;

        abstract protected IFrontendEngineContext FrontendEngineContext { get; }

        protected string _graphShapeTemplateName;

        public Localizer T { get; set; }

        protected FrontendEngineControllerBase(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IFrontendShapes frontendShapes,
            IShapeFactory shapeFactory,
            IFrontendEngineSetup setup)
            : base(associativyServices)
        {
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _frontendShapes = frontendShapes;
            _shapeFactory = shapeFactory;
            _setup = setup;

            T = NullLocalizer.Instance;
            _graphShapeTemplateName = "Engines/" + FrontendEngineContext.Name + "/Graph";
        }

        public virtual ActionResult ShowWholeGraph()
        {
            _orchardServices.WorkContext.Layout.Title = T("The whole graph").ToString();

            return new ShapeResult(this, _frontendShapes.SearchResultShape(
                    _frontendShapes.SearchBoxShape(_contentManager.New(FrontendEngineContext.SearchFormContentType)),
                    GraphShape(_mind.GetAllAssociations(MakeDefaultMindSettings(), _setup.GraphQueryModifier)))
                );
        }

        public virtual ActionResult ShowAssociations()
        {
            var searchForm = _contentManager.New(FrontendEngineContext.SearchFormContentType);
            _contentManager.UpdateEditor(searchForm, this);

            if (ModelState.IsValid)
            {
                _orchardServices.WorkContext.Layout.Title = T("Associations for {0}", searchForm.As<SearchFormPart>().Terms).ToString();

                IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph;
                if (TryGetGraph(searchForm, out graph, MakeDefaultMindSettings(), _setup.GraphQueryModifier))
                {
                    return new ShapeResult(this, _frontendShapes.SearchResultShape(
                            _frontendShapes.SearchBoxShape(searchForm),
                            GraphShape(graph))
                        );
                }
                else
                {
                    return new ShapeResult(this, _frontendShapes.SearchResultShape(
                            _frontendShapes.SearchBoxShape(searchForm),
                            _frontendShapes.AssociationsNotFoundShape(searchForm))
                        );
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage))
                {
                    //_notifier.Error(T(error));
                }

                return null;
            }
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }

        protected virtual dynamic GraphShape(IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph)
        {
            return GraphShape((object)graph);
        }

        protected virtual dynamic GraphShape(object model)
        {
            return _shapeFactory.DisplayTemplate(
                TemplateName: _graphShapeTemplateName,
                Model: model,
                Prefix: null);
        }

        protected virtual bool TryGetGraph(
            IContent searchForm,
            out IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph,
            IMindSettings settings = null,
             Func<IContentQuery<ContentItem>, IContentQuery<ContentItem>> queryModifier = null)
        {
            var searchFormPart = searchForm.As<SearchFormPart>();

            if (searchFormPart.TermsArray.Length == 0)
            {
                graph = null;
                return false;
            }

            var searched = _associativyServices.NodeManager.GetMany(searchFormPart.TermsArray);

            if (searched.Count() != searchFormPart.TermsArray.Length) // Some nodes were not found
            {
                graph = null;
                return false;
            }

            graph = _mind.MakeAssociations(searched, settings, queryModifier);

            return !graph.IsVerticesEmpty;
        }

        protected virtual IMindSettings MakeDefaultMindSettings()
        {
            return new MindSettings()
            {
                ZoomLevel = _setup.MaxZoomLevel,
                MaxZoomLevel = _setup.MaxZoomLevel
            };
        }
    }
}
