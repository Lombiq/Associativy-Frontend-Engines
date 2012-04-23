using System.Web.Mvc;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Controllers
{
    public interface IDynamicallyContextedController : IController
    {
        IGraphContext GraphContext { get; }
    }
}