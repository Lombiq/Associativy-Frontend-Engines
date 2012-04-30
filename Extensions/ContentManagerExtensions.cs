using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Extensions
{
    [OrchardFeature("Associativy.Frontends")]
    public static class ContentManagerExtensions
    {
        public static ContentItem NewEnginePage(this IContentManager contentManager, IEngineContext engineContext, string pageName)
        {
            return contentManager.New(contentManager.EnginePageId(engineContext, pageName));
        }

        public static string EnginePageId(this IContentManager contentManager, IEngineContext engineContext, string pageName)
        {
            return engineContext.EngineName + pageName;
        }

        public static dynamic BuildEnginePageDisplay(this IContentManager contentManager, IGraphContext graphContext, IContent page, string groupId = "")
        {
            return contentManager.BuildDisplay(page, graphContext.GraphName, groupId);
        }
    }
}