using Orchard;
using Orchard.ContentManagement;

namespace Associativy.FrontendEngines.Shapes
{
    public interface IFrontendShapes : IDependency
    {
        dynamic SearchBoxShape(IContent searchForm);
        dynamic SearchResultShape(dynamic searchBoxShape, dynamic graphShape);
        dynamic AssociationsNotFoundShape(IContent searchForm);
    }
}
