$(function ()
{
    var INGREDIENTS_SUGGEST_ID = "ingredientssuggest";


    var ingredientssuggest = $("#" + INGREDIENTS_SUGGEST_ID).focus(function ()
    {
        form.data("submitaction", INGREDIENTS_SUGGEST_ID);
    })
    .blur(function ()
    {
        form.data("submitaction", "");
    });


    var ingredientsContainer = $("#ingredients");

    var form = $("form").submit(function ()
    {
        if (form.data("submitaction") == INGREDIENTS_SUGGEST_ID)
        {
            var a = ingredientssuggest.val().split(",");
            var aLength = a.length;

            for (var i = 0; i < aLength; i++)
            {
                if (a[i].length > 0)
                {
                    (function (l)
                    {
                        var li = $("<li>").wrapInner($("<input>",
                        {
                            type: "text",
                            id: "addingredient" + l,
                            name: "addingredient" + l,
                            value: a[i]
                        }))
                        .append($("<img>",
                        {
                            src: Auxiliary.RELATIVE_PATH + "../Content/Images/remove.png",
                            alt: "Fjern ingrediens"
                        })
                        .click(function ()
                        {
                            li.remove();
                        }));

                        $("li:last", ingredientsContainer).after(li);
                    } ($("li", ingredientsContainer).length));
                }
            }

            ingredientssuggest.val("");

            ingredientssuggest.focus();

            return false;
        }
    });


    $("input:button").click(function ()
    {
        form.data("submitaction", INGREDIENTS_SUGGEST_ID);

        form.submit();
    });


    $("input:submit, input:button").button();
});