using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Orchard;

namespace Associativy.Frontends.Models
{
    public interface IFrontendEngineSetup : IDependency
    {
        Func<IContentQuery<ContentItem>, IContentQuery<ContentItem>> GraphQueryModifier { get; }

        /// <summary>
        /// Upper bound of the graph zoom levels (lower bound is always zero).
        /// </summary>
        int MaxZoomLevel { get; }
    }
}
