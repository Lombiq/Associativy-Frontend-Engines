using Associativy.Controllers;
using Associativy.Frontends.Services;
using Associativy.Services;

namespace Associativy.Frontends.Controllers
{
    public abstract class FrontendControllerBase : AssociativyControllerBase
    {
        protected readonly IFrontendServices _frontendServices;
        protected readonly IFrontendContextAccessor _frontendContextAccessor;

        protected FrontendControllerBase(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices)
            : base(associativyServices)
        {
            _frontendServices = frontendServices;
            _frontendContextAccessor = frontendServices.FrontendContextAccessor;
        }
    }
}