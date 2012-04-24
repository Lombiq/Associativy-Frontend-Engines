using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.Frontends.Engines.JIT
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("JITEngine").SetUrl("Engines/JIT/JIT.custom.min.js");
            manifest.DefineScript("JIT").SetUrl("Engines/JIT/JITDrawer.js").SetDependencies(new string[] { "jQueryUI_Slider", "JITEngine" });

            manifest.DefineStyle("JIT").SetUrl("Engines/JIT/associativy-jit.css").SetDependencies(new string[] { "jQueryUI_Orchard" });
        }
    }
}
