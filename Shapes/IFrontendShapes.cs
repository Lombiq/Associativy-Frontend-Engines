using Orchard;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Shapes
{
    public interface IFrontendShapes : IDependency
    {
        dynamic SearchBoxShape(IContent searchForm);
        dynamic SearchResultShape(dynamic searchBoxShape, dynamic graphShape);
        dynamic AssociationsNotFoundShape(IContent searchForm);
    }
}
