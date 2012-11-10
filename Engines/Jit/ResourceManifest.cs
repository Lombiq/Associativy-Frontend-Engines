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

            manifest.DefineScript("AssociativyFrontends_JitEngine").SetUrl("Engines/Jit/Jit.custom.min.js");
            manifest.DefineScript("AssociativyFrontends_Jit").SetUrl("Engines/Jit/associativy-jit-drawer.js").SetDependencies(new string[] { "jQueryUI_Slider", "AssociativyFrontends_JitEngine" });

            manifest.DefineStyle("AssociativyFrontends_Jit").SetUrl("Engines/Jit/associativy-jit.css").SetDependencies(new string[] { "jQueryUI_Orchard" });
        }
    }
}
