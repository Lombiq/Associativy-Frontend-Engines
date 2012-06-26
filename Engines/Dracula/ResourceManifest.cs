using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.Frontends.Engines.Dracula
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("AssociativyFrontends_Raphael").SetUrl("Engines/Dracula/raphael-min.js");
            manifest.DefineScript("AssociativyFrontends_DraculaGraffle").SetUrl("Engines/Dracula/dracula_graffle.js");
            manifest.DefineScript("AssociativyFrontends_Dracula").SetUrl("Engines/Dracula/dracula_graph.js").SetDependencies(new string[] { "jQuery", "AssociativyFrontends_Raphael", "AssociativyFrontends_DraculaGraffle" });

            manifest.DefineStyle("AssociativyFrontends_Dracula").SetUrl("Engines/Dracula/associativy-dracula.css");
        }
    }
}
