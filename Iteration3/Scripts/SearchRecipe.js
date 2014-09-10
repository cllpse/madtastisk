/// <reference path="Auxiliary.js" />


$(function ()
{
    var INGREDIENTS_SUGGEST_ID = "ingredientssuggest";
    var RECIPES_SUGGEST_ID = "recipessuggest";


    function highlight(context)
    {
        var parent = $(context).parent();
        var info = $(".info", parent);
        var text = $("input:text", parent);

        info.stop(true);
        text.stop(true);

        info.addClass("highlight", 400, function ()
        {
            info.removeClass("highlight", 2000);
        });

        text.addClass("highlight", 400, function ()
        {
            text.removeClass("highlight", 2000);
        });
    }


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

            window.location = Auxiliary.RELATIVE_PATH + "Opskrifter/WildCard/" + recipessuggest.val().replace(Auxiliary.REGEX_SPACES, "-") + ".html";
        }


        return false;
    });


    ingredientssuggest
    .val(Auxiliary.readCookie(INGREDIENTS_SUGGEST_ID) || "")
    .focus(function ()
    {
        highlight(this);

        form.data("submitaction", INGREDIENTS_SUGGEST_ID);

        return false;
    });


    recipessuggest
    .val(Auxiliary.readCookie(RECIPES_SUGGEST_ID) || "")
    .focus(function ()
    {
        highlight(this);

        form.data("submitaction", RECIPES_SUGGEST_ID);

        return false;
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