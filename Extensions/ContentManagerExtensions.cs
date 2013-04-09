using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;

namespace Piedone.HelpfulLibraries.Contents.DynamicPages
{
    public static class ContentManagerExtensions
    {
        public static dynamic BuildFrontendPageDisplay(this IContentManager contentManager, IContent page, IGraphContext graph)
        {
            var shape = contentManager.BuildDisplay(page, graph.Name);
            shape.Metadata.Wrappers.Add("PageWrapper_" + page.ContentItem.ContentType.Replace('.', '_'));
            return shape;
        }
    }
}