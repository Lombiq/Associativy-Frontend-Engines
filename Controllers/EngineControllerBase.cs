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
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using QuickGraph;
using System.Diagnostics;
using Associativy.Frontends.Models;
using Associativy.GraphDiscovery;
using Associativy.Frontends.ConfigurationDiscovery;
using Associativy.Frontends.Engines;
using System.Web.Routing;

namespace Associativy.Frontends.Controllers
{
    /// <summary>
    /// Base class for frontend engine controllers
    /// </summary>
    [Themed]
    [OrchardFeature("Associativy.Frontends")]
    public abstract class EngineControllerBase<TConfigurationDescriptor> : DynamicallyContextedControllerBase, IUpdateModel
        where TConfigurationDescriptor : ConfigurationDescriptor, new()
    {
        protected readonly IOrchardServices _orchardServices;
        protected readonly IContentManager _contentManager;
        protected readonly IConfigurationManager _configurationManager;

        abstract protected IEngineContext EngineContext { get; }

        private TConfigurationDescriptor _configurationDescriptor;
        protected virtual TConfigurationDescriptor ConfigurationDescriptor
        {
            get
            {
                if (_configurationDescriptor == null)
                {
                    _configurationDescriptor = _configurationManager.FindConfiguration<TConfigurationDescriptor>(EngineContext, GraphContext);
                }

                return _configurationDescriptor;
            }
        }

        public Localizer T { get; set; }

        protected EngineControllerBase(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IConfigurationManager configurationManager)
            : base(associativyServices)
        {
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _configurationManager = configurationManager;

            T = NullLocalizer.Instance;
        }

        public virtual ActionResult ShowWholeGraph()
        {
            _orchardServices.WorkContext.Layout.Title = T("The whole graph").ToString();

            var page = NewPage("ShowWholeGraph");

            LoadGraphPart(page, (settings) => _mind.GetAllAssociations(GraphContext, settings));

            return new ShapeResult(this, BuildDisplay(page));
        }

        public virtual ActionResult ShowAssociations()
        {
            var page = NewPage("ShowAssociations");

            _contentManager.UpdateEditor(page, this);

            if (ModelState.IsValid)
            {
                _orchardServices.WorkContext.Layout.Title = T("Associations for {0}", page.As<AssociativySearchFormPart>().Labels).ToString();

                LoadGraphPart(page, (settings) => RetrieveSearchedGraph(page, settings));

                return new ShapeResult(
                    this,
                    BuildDisplay(page));
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

        protected virtual dynamic BuildDisplay(IContent page)
        {
            return _contentManager.BuildDisplay(page, GraphContext.GraphName);
        }

        protected virtual IContent NewPage(string pageName)
        {
            var page = _contentManager.New(EngineContext.EngineName + pageName);

            var engineCommonPart = page.As<EngineCommonPart>();
            engineCommonPart.ConfigurationDescriptor = ConfigurationDescriptor;
            engineCommonPart.GraphContext = GraphContext;
            engineCommonPart.EngineContext = EngineContext;

            return page;
        }

        protected virtual IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> RetrieveSearchedGraph(
            IContent page,
            IMindSettings settings = null)
        {
            var searchFormPart = page.As<AssociativySearchFormPart>();

            if (searchFormPart.LabelsArray.Length == 0)
            {
                return _associativyServices.GraphService.GraphFactory();
            }

            var searched = _associativyServices.NodeManager.GetMany(GraphContext, searchFormPart.LabelsArray);

            if (searched.Count() != searchFormPart.LabelsArray.Length) // Some nodes were not found
            {
                return _associativyServices.GraphService.GraphFactory();
            }

            if (settings == null)
            {
                settings = ConfigurationDescriptor.MakeDefaultMindSettings();
            }

            return _mind.MakeAssociations(GraphContext, searched, settings);
        }

        protected virtual void LoadGraphPart(IContent page, Func<IMindSettings, IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>>> retrieveGraph)
        {
            var graphPart = page.As<GraphPart>();
            var settings = ConfigurationDescriptor.MakeDefaultMindSettings();
            graphPart.Graph = retrieveGraph(settings);

            graphPart.ZoomLevelCountField.Loader(() =>
            {
                settings.ZoomLevel = ConfigurationDescriptor.MaxZoomLevel;
                return _associativyServices.GraphService.CalculateZoomLevelCount(retrieveGraph(settings), ConfigurationDescriptor.MaxZoomLevel);
            });
        }
    }
}
