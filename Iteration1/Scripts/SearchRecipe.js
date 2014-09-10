/// <reference path="Auxiliary.js" />


$(function ()
{
    var INGREDIENTS_SUGGEST_ID = "ingredientssuggest";
    var RECIPES_SUGGEST_ID = "recipessuggest";


    var ingredientssuggest = $("#" + INGREDIENTS_SUGGEST_ID);
    var recipessuggest = $("#" + RECIPES_SUGGEST_ID);


    var form = $("form").submit(function ()
    {
        if (form.data("submitaction") == INGREDIENTS_SUGGEST_ID)
        {
            Auxiliary.createCookie(INGREDIENTS_SUGGEST_ID, ingredientssuggest.val(), 1);

            window.location = Auxiliary.RELATIVE_PATH + "Opskrifter/Find/" + ingredientssuggest.val().replace(Auxiliary.REGEX_COMMAS, "-").replace(Auxiliary.REGEX_SPACES, "") + ".html";
        }
        else
        {
            Auxiliary.createCookie(RECIPES_SUGGEST_ID, recipessuggest.val(), 1);

            window.location = Auxiliary.RELATIVE_PATH + "Opskrifter/Vis/" + recipessuggest.val().replace(Auxiliary.REGEX_SPACES, "-") + ".html";
        }


        return false;
    });


    ingredientssuggest
    .val(Auxiliary.readCookie(INGREDIENTS_SUGGEST_ID) || "")
    .focus(function ()
    {
        form.data("submitaction", INGREDIENTS_SUGGEST_ID);
    });


    recipessuggest
    .val(Auxiliary.readCookie(RECIPES_SUGGEST_ID) || "")
    .focus(function ()
    {
        form.data("submitaction", RECIPES_SUGGEST_ID);
    });


    var submitInput = $("<input />", { id: "searchsubmit", name: "searchsubmit", type: "submit", value: "Søg" }).button();

    var tabs = $("#tabs").tabs(
    {
        show: function (event, ui)
        {
            $("input:text", ui.panel).focus().after(submitInput);
        }
    });
});