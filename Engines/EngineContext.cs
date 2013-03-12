
namespace Associativy.Frontends.Engines
{
    public class EngineContext : IEngineContext
    {
        public string EngineName { get; private set; }

        public EngineContext(string engineName)
        {
            EngineName = engineName;
        }
    }
}