using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Engines;
using Orchard.ContentManagement;
using Associativy.GraphDiscovery;
using Orchard.Security;

namespace Associativy.Frontends.EventHandlers
{
    public class FrontendAuthorizationEventContext : FrontendEventContext
    {
        public IUser User { get; private set; }
        public bool Granted { get; set; }

        public FrontendAuthorizationEventContext(IUser user, IContent page, IEngineContext engineContext, IGraphContext graphContext)
            : base(page, engineContext, graphContext)
        {
            User = user;
            Granted = false;
        }
    }
}