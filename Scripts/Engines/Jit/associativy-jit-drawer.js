﻿(function ($) {
    $.extend(true, {
        associativy: {
            jit: {
                draw: function (canvasId, json) {
                    var labelType, useGradients, nativeTextSupport, animate;

                    var ua = navigator.userAgent;
                    var iStuff = ua.match(/iPhone/i) || ua.match(/iPad/i);
                    var typeOfCanvas = typeof HTMLCanvasElement;
                    var nativeCanvasSupport = (typeOfCanvas == "object" || typeOfCanvas == "function");
                    var textSupport = nativeCanvasSupport && (typeof document.createElement("canvas").getContext("2d").fillText == "function");

                    //I'm setting this based on the fact that ExCanvas provides text support for IE
                    //and that as of today iPhone/iPad current text support is lame
                    labelType = (!nativeCanvasSupport || (textSupport && !iStuff)) ? "Native" : "HTML";
                    nativeTextSupport = labelType == "Native";
                    useGradients = nativeCanvasSupport;
                    animate = !(iStuff || !nativeCanvasSupport);

                    labelType = "HTML"; // This may need revision

                    var graphSetup = {
                        //id of the visualization container
                        injectInto: canvasId,
                        //Enable zooming and panning
                        //by scrolling and DnD
                        Navigation: {
                            enable: true,
                            type: "Native",
                            //Enable panning events only if we're dragging the empty
                            //canvas (and not a node).
                            panning: "avoid nodes",
                            zooming: 50 //zoom speed. higher is more sensible
                        },
                        // Change node and edge styles such as
                        // color and width.
                        // These properties are also set per node
                        // with dollar prefixed data-properties in the
                        // JSON structure.
                        Node: {
                            overridable: true,
                            dim: 18,
                            type: "circle"
                        },
                        Edge: {
                            overridable: true,
                            color: "#23A4FF",
                            lineWidth: 0.6
                        },
                        //Native canvas text styling
                        Label: {
                            type: labelType, //Native or HTML; onCreateLabel() only runs with HTML
                            size: 16,
                            style: "bold",
                            color: "000"
                        },
                        //Add Tips
                        // This would be only useful with the total connection count (vs the displayed connection count) shown.
                        //                        Tips: {
                        //                            enable: true,
                        //                            onShow: function (tip, node) {
                        //                                //count connections
                        //                                var count = 0;
                        //                                node.eachAdjacency(function () { count++; });
                        //                                //display node info in tooltip
                        //                                tip.innerHTML = "<div class=\"tip-title\">" + node.name + "</div>" + "<div class=\"tip-text\"><b>connections:</b> " + count + "</div>";
                        //                            }
                        //                        },
                        // Add node events
                        Events: {
                            enable: true,
                            type: "Native",
                            //Change cursor style when hovering a node
                            onMouseEnter: function () {
                                layout.canvas.getElement().style.cursor = "move";
                            },
                            onMouseLeave: function () {
                                layout.canvas.getElement().style.cursor = "";
                            },
                            //Update node positions when dragged
                            onDragMove: function (node, eventInfo, e) {
                                var pos = eventInfo.getPos();
                                node.pos.setc(pos.x, pos.y);
                                layout.plot();
                            },
                            //Implement the same handler for touchscreens
                            onTouchMove: function (node, eventInfo, e) {
                                $jit.util.event.stop(e); //stop default touchmove event
                                this.onDragMove(node, eventInfo, e);
                            },
                            //Add also a click handler to nodes
                            onClick: function (node) {
                                if (!node) return;

                                //set final styles  
                                layout.graph.eachNode(function (n) {
                                    if (n.id != node.id) delete n.selected;
                                    n.setData('dim', 18, 'end');
                                    n.eachAdjacency(function (adj) {
                                        adj.setDataset('end', {
                                            lineWidth: 0.4,
                                            color: '#23a4ff'
                                        });
                                    });
                                });
                                if (!node.selected) {
                                    node.selected = true;
                                    node.setData('dim', 29, 'end');
                                    node.eachAdjacency(function (adj) {
                                        adj.setDataset('end', {
                                            lineWidth: 3,
                                            color: '#36acfb'
                                        });
                                    });
                                } else {
                                    delete node.selected;
                                }
                                //trigger animation to final styles  
                                layout.fx.animate({
                                    modes: ['node-property:dim', 'edge-property:lineWidth:color'],
                                    duration: 500
                                });
                            }
                        },
                        //Number of iterations for the FD algorithm
                        iterations: 200,
                        //Edge length
                        levelDistance: 230,
                        // This method is only triggered  
                        // on label creation and only for DOM labels (not native canvas ones).  
                        onCreateLabel: function (label, node) {
                            if (!node.data["url"]) {
                                label.innerHTML = node.name;
                                return;
                            }

                            label.innerHTML = "<a href='" + node.data["url"] + "'>" + node.name + "</a>";
                        },
                        // Change node styles when DOM labels are placed
                        // or moved.
                        onPlaceLabel: function (domElement, node) {
                            var style = domElement.style;
                            var left = parseInt(style.left);
                            var top = parseInt(style.top);
                            var w = domElement.offsetWidth;
                            style.left = (left - w / 2) + "px";
                            style.top = (top + 10) + "px";
                            style.display = "";
                        }
                    };

                    $(document).triggerHandler({
                        type: "graphSetup.AssociativyJit",
                        graphSetup: graphSetup
                    });

                    var layout = new $jit.ForceDirected(graphSetup);

                    this.redraw(layout, json);

                    return layout;
                },

                redraw: function (layout, json) {
                    // load JSON data.
                    layout.loadJSON(json);

                    $(document).triggerHandler({
                        type: "graphLoaded.AssociativyJit",
                        layout: layout
                    });

                    // compute positions incrementally and animate.
                    layout.computeIncremental({
                        iter: 40,
                        property: "end",
                        onStep: function (percent) {
                            // percent% done
                        },
                        onComplete: function () {
                            layout.animate({
                                modes: ["linear"],
                                transition: $jit.Trans.Elastic.easeOut,
                                duration: 500,
                                onComplete: function () {
                                    $(document).triggerHandler({
                                        type: "layoutComputed.AssociativyJit",
                                        layout: layout
                                    });
                                }
                            });
                        }
                    });
                },

                showLoader: function () {
                    if ($("#associativy-jit-loader").length > 0) return;
                    $("<div id='associativy-jit-loader'></div>").prependTo("#associativy-jit-associations-canvas");
                },

                hideLoader: function () {
                    var loader = $("#associativy-jit-loader");
                    if (loader.length = 0) return;
                    loader.remove();
                }
            }
        }
    });
})(jQuery);