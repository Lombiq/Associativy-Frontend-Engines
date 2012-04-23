using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associativy.Frontends.Services
{
    public class FrontendServices : IFrontendServices
    {
        private readonly IFrontendContextAccessor _frontendContextAccessor;

        public IFrontendContextAccessor FrontendContextAccessor
        {
            get { return _frontendContextAccessor; }
        }

        public FrontendServices(
            IFrontendContextAccessor frontendContextAccessor)
        {
            _frontendContextAccessor = frontendContextAccessor;
        }
    }
}