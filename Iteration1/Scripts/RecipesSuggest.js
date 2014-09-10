/// <reference path="jquery-1.4.1-vsdoc.js" />
/// <reference path="Auxiliary.js" />


$(function ()
{
    var MIN_LENGTH = 1;
    var DELAY = 300;


    var input = $("#recipessuggest").autocomplete(
    {
        minLength: MIN_LENGTH,
        delay: DELAY,

        focus: function (event, ui)
        {
            return false;
        },

        select: function (event, ui)
        {
            input.val(ui.item.value);

            return false;
        },

        source: function (request, response)
        {
            $.ajax(
            {
                url: Auxiliary.RELATIVE_PATH + "Opskrifter/Navn/" + input.val() + ".json",
                success: function (data)
                {
                    response($.map(data, function (item)
                    {
                        return {
                            "label": item.Name,
                            "value": item.Name
                        };
                    }));
                }
            });
        }
    })
    .click(function ()
    {
        this.select();
    });
});