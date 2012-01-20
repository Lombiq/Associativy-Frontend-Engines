using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;
using Associativy.Controllers;
using Associativy.Services;
using System.Web.Routing;

namespace Associativy.FrontendEngines.Controllers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public abstract class DynamicallyContextedControllerBase : AssociativyControllerBase
    {
        public IGraphContext GraphContext { get; set; }

        protected DynamicallyContextedControllerBase(IAssociativyServices associativyServices)
            : base(associativyServices)
        {
        }

        protected override void Initialize(RequestContext requestContext)
        {
            var dataTokens = requestContext.RouteData.DataTokens;
            if (dataTokens != null && dataTokens.ContainsKey("GraphContext"))
            {
                GraphContext = dataTokens["GraphContext"] as IGraphContext;
            }
            else throw new InvalidOperationException("The graph context to use should be fed into the route's DataTokens dictionary with key \"GraphContext\".");

            base.Initialize(requestContext);
        }
    }
}