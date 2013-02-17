using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineContext : IEngineContext
    {
        public string EngineName { get; private set; }

        public EngineContext(string engineName)
        {
            EngineName = engineName;
        }
    }
}