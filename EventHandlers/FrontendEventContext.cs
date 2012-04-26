using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;

namespace Associativy.Frontends.EventHandlers
{
    public class FrontendEventContext : FrontendContext
    {
        public IContent Page { get; private set; }

        public FrontendEventContext(IContent page, IEngineContext engineContext, IGraphContext graphContext)
            : base(engineContext, graphContext)
        {
            Page = page;
        }
    }
}