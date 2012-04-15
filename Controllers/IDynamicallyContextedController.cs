using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Controllers
{
    public interface IDynamicallyContextedController : IController
    {
        IGraphContext GraphContext { get; }
    }
}
