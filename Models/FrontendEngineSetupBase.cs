using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Core.Routable.Models;
using Orchard.Environment.Extensions;

namespace Associativy.FrontendEngines.Models
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public abstract class FrontendEngineSetupBase : IFrontendEngineSetup
    {
        public virtual Func<IContentQuery<ContentItem>, IContentQuery<ContentItem>> GraphQueryModifier
        {
            get { return (query) => query.Join<RoutePartRecord>(); }
        }

        public virtual int MaxZoomLevel
        {
            get { return 10; }
        }
    }
}