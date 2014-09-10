var Auxiliary = {
    REGEX_SPACES: /\s+/g,
    REGEX_COMMAS: /,+/g,
    REGEX_INGREDIENTS: /([a-zæøå]+)((\d+)([a-zæøå]+)?)?,?/g,
    SUGGEST_QUERY_ALL_INGREDIENTS: "SUGGEST_QUERY_ALL_INGREDIENTS",
    RELATIVE_PATH: "",
    
    // createCookie, readCookie and eraseCookie by Peter-Paul Koch
    createCookie: function (name,value,days)
    {
	    if (days) {
		    var date = new Date();
		    date.setTime(date.getTime()+(days*24*60*60*1000));
		    var expires = "; expires="+date.toGMTString();
	    }
	    else var expires = "";
	    document.cookie = name+"="+value+expires+"; path=/";
    },

    readCookie: function (name)
    {
	    var nameEQ = name + "=";
	    var ca = document.cookie.split(';');
	    for(var i=0;i < ca.length;i++) {
		    var c = ca[i];
		    while (c.charAt(0)==' ') c = c.substring(1,c.length);
		    if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	    }
	    return undefined;
    },

    eraseCookie: function (name)
    {
	    createCookie(name,"",-1);
    }
};