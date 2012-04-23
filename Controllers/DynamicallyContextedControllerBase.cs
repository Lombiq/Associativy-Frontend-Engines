using System;
using Associativy.Frontends.Services;
using Associativy.GraphDiscovery;
using Associativy.Services;
using Orchard.Environment.Extensions;

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