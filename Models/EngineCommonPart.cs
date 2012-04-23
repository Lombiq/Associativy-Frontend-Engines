using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Associativy.Models.Mind;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Models
{
    /// <summary>
    /// All engine pages should contain this part or another part implementing the aspect
    /// </summary>
    [OrchardFeature("Associativy.Frontends")]
    public class EngineCommonPart : ContentPart, IEngineConfigurationAspect
    {
        public IGraphContext GraphContext { get; set; }
        public IEngineContext EngineContext { get; set; }
        public IMindSettings MindSettings { get; set; }
    }
}