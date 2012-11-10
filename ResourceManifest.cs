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

            manifest.DefineScript("AssociativyFrontends_AutoComplete").SetUrl("associativy-auto-complete.js").SetDependencies("jQueryUI_Autocomplete");
            manifest.DefineScript("AssociativyFrontends_GraphZoomSlider").SetUrl("associativy-graph-zoom-slider.js").SetDependencies(new string[] { "jQueryUI_Slider" });

            manifest.DefineStyle("AssociativyFrontends_GraphZoomSlider").SetUrl("associativy-graph-zoom-slider.css").SetDependencies(new string[] { "jQueryUI_Orchard" });
        }
    }
}
