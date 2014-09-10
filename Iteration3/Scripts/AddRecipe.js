$(function ()
{
    var INGREDIENTS_SUGGEST_ID = "ingredientssuggest";


    //show:null,stack:true,title:"",width:300,zIndex:1000}

    function highlight(context)
    {
        var parent = $(context).parent();
        var info = $(".info", parent);
        var text = $("input:text", parent);

        info.stop(true);
        text.stop(true);

        info.addClass("highlight", 400, function ()
        {
            info.removeClass("highlight", 4000);
        });

        text.addClass("highlight", 400, function ()
        {
            text.removeClass("highlight", 4000);
        });
    }


    var ingredientssuggest = $("#" + INGREDIENTS_SUGGEST_ID).focus(function ()
    {
        form.data("submitaction", INGREDIENTS_SUGGEST_ID);
    })
    .blur(function ()
    {
        form.data("submitaction", "");
    })
    .focus(function ()
    {
        highlight(this);

        form.data("submitaction", INGREDIENTS_SUGGEST_ID);

        return false;
    });



    var ingredientsContainer = $("#ingredients");

    var form = $("form").submit(function ()
    {
        if (form.data("submitaction") != INGREDIENTS_SUGGEST_ID)
        {
            var innerHTML = "<div><h1>Navn</h1> " + $("#RecipeName").val() + "<h1>Tilberedning</h1><p>" + $("#RecipePreperation").val().replace(/\n/g, "<br />") + "</p></div><div><h1>Ingredienser</h1>";

            $(".addingredient").each(function ()
            {
                innerHTML += "<p>" + this.value + "</p>";
            });

            $("<div>", { id: "modal", innerHTML: innerHTML }).dialog(
            {

                title: "Sådan ser din opskrift ud",
                closeOnEscape: false,
                resizable: false,
                draggable: false,
                modal: true,
                height: 400,
                width: 960,
                closeText: "Tilbage",
                buttons: {
                    "Jeg er tilfreds. Gem min opskrift": function ()
                    {
                        form.data("allowsubmit", "allowsubmit");

                        form.submit();
                    },

                    "Tilbage": function ()
                    {
                        $("#modal").remove();
                    }
                }
            });
        }


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
                            "class": "addingredient",
                            value: a[i]
                        }))
                        .append($("<img>",
                        {
                            src: Auxiliary.RELATIVE_PATH + "../Content/Images/remove.png",
                            alt: "Fjern ingrediens",
                            title: "Fjern ingrediens"
                        })
                        .click(function ()
                        {
                            li.remove();
                        })
                        .tipTip(
                        {
                            fadeIn: 0,
                            fadeOut: 0,
                            maxWidth: "220px"
                        }));

                        $("li:last", ingredientsContainer).after(li);
                    } ($("li", ingredientsContainer).length));
                }
            }

            ingredientssuggest.val("");

            ingredientssuggest.focus();

            return false;
        }

        $("#RecipeAdd").click(function ()
        {
            form.data("submitaction", "");
        });

        if (form.data("allowsubmit") != "allowsubmit") return false;
    });


    $("input:button").click(function ()
    {
        form.data("submitaction", INGREDIENTS_SUGGEST_ID);

        form.submit();
    });


    $("input:submit, input:button").button();
});