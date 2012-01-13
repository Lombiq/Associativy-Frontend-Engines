using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.FrontendEngines.Engines.Graphviz
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // OpenLayers
            manifest.DefineScript("OpenLayers").SetUrl("Engines/Graphviz/OpenLayers-2.11/OpenLayers.js").SetVersion("2.11");
            manifest.DefineScript("MapQuery").SetUrl("Engines/Graphviz/MapQuery-0.1/src/jquery.mapquery.core.js").SetVersion("0.1").SetDependencies(new string[] { "OpenLayers", "jQuery" });
            manifest.DefineScript("MapQuery.ImageLayer").SetUrl("Engines/Graphviz/jquery.mapquery.imageLayer.js").SetDependencies(new string[] { "MapQuery" });
            
            // Mapz
            manifest.DefineScript("Mousewheel").SetUrl("Engines/Graphviz/jquery.mousewheel.js").SetDependencies(new string[] { "jQuery" });
            manifest.DefineScript("Mapz").SetUrl("Engines/Graphviz/jquery.mapz.js").SetDependencies(new string[] { "jQueryUI", "Mousewheel" });
            manifest.DefineStyle("Mapz").SetUrl("Engines/Graphviz/associativy-graphviz-mapz.css");

            // Mapbox
            manifest.DefineScript("Mapbox").SetUrl("Engines/Graphviz/jquery.mapbox.js").SetDependencies(new string[] { "jQuery", "Mousewheel" });
            manifest.DefineStyle("Mapbox").SetUrl("Engines/Graphviz/associativy-graphviz-mapbox.css");

            //manifest.DefineScript("Graphviz").SetDependencies(new string[] { "Mapbox" });
        }
    }
}
