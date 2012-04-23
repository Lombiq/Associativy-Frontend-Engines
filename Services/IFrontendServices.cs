using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;

namespace Associativy.Frontends.Services
{
    public interface IFrontendServices : IDependency
    {
        IFrontendContextAccessor FrontendContextAccessor { get; }
    }
}
