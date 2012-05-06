using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.Frontends.Engines.Jit
{
    [OrchardFeature("Associativy.Frontends.Jit")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("JitEngine").SetUrl("Engines/Jit/Jit.custom.min.js");
            manifest.DefineScript("Jit").SetUrl("Engines/Jit/JitDrawer.js").SetDependencies(new string[] { "jQueryUI_Slider", "JitEngine" });

            manifest.DefineStyle("Jit").SetUrl("Engines/Jit/associativy-jit.css").SetDependencies(new string[] { "jQueryUI_Orchard" });
        }
    }
}
