using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("AssociativyAutoComplete").SetUrl("AssociativyAutoComplete.js").SetDependencies("jQueryUI_Autocomplete");
            manifest.DefineScript("GraphZoomSlider").SetDependencies(new string[] { "jQueryUI_Slider" });

            manifest.DefineStyle("GraphZoomSlider").SetUrl("associativy-graph-zoom-slider.css").SetDependencies(new string[] { "jQueryUI_Orchard" });
        }
    }
}
