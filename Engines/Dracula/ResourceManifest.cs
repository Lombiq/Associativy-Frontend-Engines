using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("Raphael").SetUrl("Engines/Dracula/raphael-min.js");
            manifest.DefineScript("DraculaGraffle").SetUrl("Engines/Dracula/dracula_graffle.js");
            manifest.DefineScript("Dracula").SetUrl("Engines/Dracula/dracula_graph.js").SetDependencies(new string[] { "jQuery", "Raphael", "DraculaGraffle" });
        }
    }
}
