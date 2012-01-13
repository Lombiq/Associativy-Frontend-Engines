using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.FrontendEngines.Engines.JIT
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("JITEngine").SetUrl("Engines/JIT/JIT.custom.min.js");
            manifest.DefineScript("JIT").SetUrl("Engines/JIT/JITDrawer.js").SetDependencies(new string[] { "jQueryUI", "JITEngine" });

            manifest.DefineStyle("JIT").SetUrl("Engines/JIT/associativy-jit.css");
        }
    }
}
