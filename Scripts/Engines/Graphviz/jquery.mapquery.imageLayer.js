$.extend($.MapQuery.Layer.types, {
    image: function (options) {
        //delete options["type"];
        return {
            layer: new OpenLayers.Layer.Image(options["name"], options["url"], options["extent"], options["size"], options["options"]),
            options: options
        };
    }
});