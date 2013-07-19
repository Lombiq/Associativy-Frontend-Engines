using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Associativy.Models.Services;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Models
{
    public interface IEngineConfigurationAspect : IContent
    {
        IGraphDescriptor GraphDescriptor { get; set; }
        IEngineContext EngineContext { get; set; }
        IMindSettings MindSettings { get; set; }
        GraphSettings GraphSettings { get; set; }
    }
}
