using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;
using Associativy.Controllers;
using Associativy.Services;
using System.Web.Routing;
using Associativy.Frontends.Services;

namespace Associativy.Frontends.Controllers
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class DynamicallyContextedControllerBase : FrontendControllerBase, IDynamicallyContextedController
    {
        private IGraphContext _graphContext;
        public IGraphContext GraphContext
        {
            get
            {
                if (_graphContext == null)
                {
                    _graphContext = _frontendServices.FrontendContextAccessor.GetCurrentGraphContext();
                    if (_graphContext == null)
                    {
                        throw new InvalidOperationException("The graph context was not set for the current request.");
                    }
                }

                return _graphContext;
            }
        }

        protected DynamicallyContextedControllerBase(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices)
            : base(associativyServices, frontendServices)
        {
        }
    }
}