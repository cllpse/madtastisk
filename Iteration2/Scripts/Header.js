$(function ()
{
    function setSelected(that)
    {
        if (that.className == "selected") $("a", that).addClass("ui-state-active");
    }


    var cache = $("#header li").mouseout(function ()
    {
        setSelected(this);
    });

    cache.parent().buttonset();

    cache.each(function ()
    {
        setSelected(this);
    });
});