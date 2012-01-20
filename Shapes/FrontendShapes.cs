using ClaySharp;
using Orchard;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Shapes
{
    [OrchardFeature("Associativy.Frontends")]
    public class FrontendShapes : IFrontendShapes
    {
        protected readonly IOrchardServices _orchardServices;
        protected readonly IContentManager _contentManager;
        protected readonly dynamic _shapeFactory;

        protected readonly dynamic _clayFactory = new ClayFactory();

        #region Shape template settings
        protected virtual string SearchBoxShapeTemplateName
        {
            get { return "SearchBox"; }
        }

        protected virtual string SearchResultShapeTemplateName
        {
            get { return "SearchResult"; }
        }

        protected virtual string AssociationsNotFoundShapeTemplateName
        {
            get { return "NotFound"; }
        }
        #endregion

        public FrontendShapes(
            IOrchardServices orchardServices,
            IShapeFactory shapeFactory)
        {
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _shapeFactory = shapeFactory;
        }

        public virtual dynamic SearchBoxShape(IContent searchForm)
        {
            dynamic searchFormShape = _contentManager.BuildDisplay(searchForm);

            var model = _clayFactory.Model();
            model.SearchFormShape = searchFormShape;

            return _shapeFactory.DisplayTemplate(
                        TemplateName: SearchBoxShapeTemplateName,
                        Model: model,
                        Prefix: null);
        }

        public virtual dynamic SearchResultShape(dynamic searchBoxShape, dynamic graphShape)
        {
            var model = _clayFactory.Model();
            model.SearchBox = searchBoxShape;
            model.Graph = graphShape;

            return _shapeFactory.DisplayTemplate(
                        TemplateName: SearchResultShapeTemplateName,
                        Model: model,
                        Prefix: null);
        }

        public virtual dynamic AssociationsNotFoundShape(IContent searchForm)
        {
            return _shapeFactory.DisplayTemplate(
                        TemplateName: AssociationsNotFoundShapeTemplateName,
                        Model: searchForm,
                        Prefix: null);
        }
    }
}