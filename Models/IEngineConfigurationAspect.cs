using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Associativy.Models.Mind;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Models
{
    public interface IEngineConfigurationAspect : IContent
    {
        IGraphContext GraphContext { get; set; }
        IEngineContext EngineContext { get; set; }
        IMindSettings MindSettings { get; set; }
    }
}
