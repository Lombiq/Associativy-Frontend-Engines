using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.FrontendEngines
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineScript("AssociativyAutoComplete").SetUrl("AssociativyAutoComplete.js").SetDependencies("jQueryUI_Autocomplete");
        }
    }
}
