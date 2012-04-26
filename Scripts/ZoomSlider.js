(function ($) {
    $.extend(true, {
        associativy: {
            zoomslider: {
                init: function (containerSelector, maxZoomLevel, onStop) {
                    $(containerSelector).slider({
		                orientation: "vertical",
		                range: "min",
		                min: 0,
		                max: maxZoomLevel,
		                value: 0,
		                stop: function(event, ui) {
		                    onStop(ui.value);
                            $("#associativy-jit-zoom-slider-value").text(ui.value);
		                }
	                });
                }
            }
        }
    });
})(jQuery);