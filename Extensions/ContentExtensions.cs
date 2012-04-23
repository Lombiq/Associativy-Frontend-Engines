using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Extensions
{
    [OrchardFeature("Associativy.Frontends")]
    public static class ContentExtensions
    {
        public static bool IsPage(this IContent page, string pageName)
        {
            return page.ContentItem.ContentType.EndsWith(pageName);
        }
    }
}