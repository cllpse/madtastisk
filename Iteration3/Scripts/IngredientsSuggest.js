/// <reference path="jquery-1.4.1-vsdoc.js" />
/// <reference path="Auxiliary.js" />


$(function ()
{
    var MIN_LENGTH = 1;
    var DELAY = 300;


    function extractLastIngredient(s)
    {
        var sanitizedInput = s.replace(Auxiliary.REGEX_SPACES, "");
        var lastGroupIndex = sanitizedInput.lastIndexOf(",");
        var ingredient;


        Auxiliary.REGEX_INGREDIENTS.lastIndex = 0;


        if (lastGroupIndex === -1)
        {
            ingredient = Auxiliary.REGEX_INGREDIENTS.exec(sanitizedInput) || [];
        }
        else
        {
            ingredient = Auxiliary.REGEX_INGREDIENTS.exec(sanitizedInput.substr(lastGroupIndex + 1, sanitizedInput.length - 1)) || [];
        }


        return {
            name: ingredient[1] || "",
            amount: ingredient[3] || "",
            unit: ingredient[4] || Auxiliary.SUGGEST_QUERY_ALL_INGREDIENTS
        };
    }


    var input = $("#ingredientssuggest").autocomplete(
    {
        minLength: MIN_LENGTH,
        delay: DELAY,

        focus: function (event, ui)
        {
            return false;
        },

        select: function (event, ui)
        {
            var s = input.val();
            var ingredient = extractLastIngredient(s);
            var lastGroupIndex = s.lastIndexOf(",");
            var r = s.substr(0, lastGroupIndex + 1);


            if (lastGroupIndex !== -1) r += " ";

            if (ingredient.amount.length > 0)
            {
                r += ingredient.name + " ";
                r += ingredient.amount + " ";
                r += ui.item.value + ", ";
            }
            else if (ingredient.name.length > 0)
            {
                r += ui.item.value + " ";
            }


            input.val(r);


            return false;
        },

        source: function (request, response)
        {
            var ingredient = extractLastIngredient(input.val());
            var url = "";
            var dataPoint = "";


            if (ingredient.name.length > 0)
            {
                if (ingredient.amount.length > 0)
                {
                    url = Auxiliary.RELATIVE_PATH + "Opskrifter/IngrediensEnhed/" + ingredient.unit + ".json";
                    dataPoint = "Unit";
                }
                else
                {
                    if (ingredient.name.length < MIN_LENGTH) return false;

                    url = Auxiliary.RELATIVE_PATH + "Opskrifter/Ingrediens/" + ingredient.name + ".json";
                    dataPoint = "Name";
                }


                $.ajax(
                {
                    url: url,
                    success: function (data)
                    {
                        response($.map(data, function (item)
                        {
                            return {
                                "label": item[dataPoint],
                                "value": item[dataPoint]
                            };
                        }));
                    }
                });
            }
        }
    });
});