using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Associativy.Frontends.Engines.Graphviz
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // OpenLayers
            manifest.DefineScript("AssociativyFrontends_OpenLayers").SetUrl("Engines/Graphviz/OpenLayers-2.11/OpenLayers.js").SetVersion("2.11");
            manifest.DefineScript("AssociativyFrontends_MapQuery").SetUrl("Engines/Graphviz/MapQuery-0.1/src/jquery.mapquery.core.js").SetVersion("0.1").SetDependencies(new string[] { "AssociativyFrontends_OpenLayers", "jQuery" });
            manifest.DefineScript("AssociativyFrontends_MapQuery.ImageLayer").SetUrl("Engines/Graphviz/jquery.mapquery.imageLayer.js").SetDependencies(new string[] { "AssociativyFrontends_MapQuery" });
            
            // Mapz
            manifest.DefineScript("AssociativyFrontends_Mousewheel").SetUrl("Engines/Graphviz/jquery.mousewheel.js").SetDependencies(new string[] { "jQuery" });
            manifest.DefineScript("AssociativyFrontends_Mapz").SetUrl("Engines/Graphviz/jquery.mapz.js").SetDependencies(new string[] { "jQueryUI", "AssociativyFrontends_Mousewheel" });
            manifest.DefineStyle("AssociativyFrontends_Mapz").SetUrl("Engines/Graphviz/associativy-graphviz-mapz.css");

            // Mapbox
            manifest.DefineScript("AssociativyFrontends_Mapz").SetUrl("Engines/Graphviz/jquery.mapbox.js").SetDependencies(new string[] { "jQuery", "AssociativyFrontends_Mousewheel" });
            manifest.DefineStyle("AssociativyFrontends_Mapz").SetUrl("Engines/Graphviz/associativy-graphviz-mapbox.css");

            //manifest.DefineScript("AssociativyFrontends_Graphviz").SetDependencies(new string[] { "AssociativyFrontends_Mapz" });
        }
    }
}
