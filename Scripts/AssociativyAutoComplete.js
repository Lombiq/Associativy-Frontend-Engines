(function ($) {
    $.extend(true, {
        associativy: {
            autoComplete: {
                labels: function (textboxId, fetchUrl) {
                    if (textboxId == null)
                        textboxId = 'terms';

                    function split(val) {
                        return val.split(/,\s*/);
                    }
                    function extractLast(term) {
                        return split(term).pop();
                    }

                    var textBox = $('#' + textboxId);

                    textBox.bind('keydown', function (event) {
                        // don't navigate away from the field on tab when selecting an item
                        if (event.keyCode === $.ui.keyCode.TAB &&
                $(this).data('autocomplete').menu.active) {
                            event.preventDefault();
                        }
                    }).autocomplete({
                        source: function (request, response) {
                            $.getJSON(fetchUrl, {
                                LabelSnippet: extractLast(request.term)
                            }, response);
                        },
                        appendTo: textBox.parent(),
                        search: function () {
                            // custom minLength
                            var term = extractLast(this.value);
                            if (term.length < 2) {
                                return false;
                            }
                        },
                        focus: function () {
                            // prevent value inserted on focus
                            return false;
                        },
                        select: function (event, ui) {
                            var terms = split(this.value);
                            // remove the current input
                            terms.pop();
                            // add the selected item
                            terms.push(ui.item.value);
                            // add placeholder to get the comma-and-space at the end
                            terms.push('');
                            this.value = terms.join(', ');
                            return false;
                        }
                    });
                }
            }
        }
    });
})(jQuery);