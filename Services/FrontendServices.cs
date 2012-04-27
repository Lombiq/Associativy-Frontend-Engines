
using Associativy.Frontends.EngineDiscovery;
namespace Associativy.Frontends.Services
{
    public class FrontendServices : IFrontendServices
    {
        private readonly IFrontendContextAccessor _frontendContextAccessor;
        public IFrontendContextAccessor FrontendContextAccessor
        {
            get { return _frontendContextAccessor; }
        }

        private readonly IEngineManager _engineManager;
        public IEngineManager EngineManager
        {
            get { return _engineManager; }
        }

        public FrontendServices(
            IFrontendContextAccessor frontendContextAccessor,
            IEngineManager engineManager)
        {
            _frontendContextAccessor = frontendContextAccessor;
            _engineManager = engineManager;
        }
    }
}