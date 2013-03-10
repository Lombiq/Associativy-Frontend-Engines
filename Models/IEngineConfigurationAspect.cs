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

    public class GraphSettings
    {
        public int InitialZoomLevel { get; set; }
        public int ZoomLevelCount { get; set; }
        public int MaxConnectionCount { get; set; }

        private static GraphSettings _default = new GraphSettings();
        public static GraphSettings Default { get { return _default; } }


        public GraphSettings()
        {
            InitialZoomLevel = 0;
            ZoomLevelCount = 10;
            MaxConnectionCount = 200;
        }
    }
}
