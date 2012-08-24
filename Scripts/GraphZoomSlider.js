﻿(function ($) {
    $.extend(true, {
        associativy: {
            graphZoomSlider: {
                init: function (maxZoomLevel) {
                    $("#associativy-graph-zoom-slider").slider({
		                orientation: "vertical",
		                range: "min",
		                min: 0,
		                max: maxZoomLevel,
		                value: 0,
		                stop: function(event, ui) {
                            $(document).triggerHandler({
                                type: "stop.AssociativyGraphZoomSlider",
                                event: event,
                                ui: ui
                            });
                            $("#associativy-graph-zoom-slider-value").text(ui.value);
		                }
	                });
                    $("#associativy-graph-zoom-slider-value").text($("#associativy-graph-zoom-slider").slider("value"));
                }
            }
        }
    });
})(jQuery);