
module.exports = {
	// cookie handling functions
	get_cookie: function(name) {
	    var search = name + "=";
	    var returnvalue = "";
	    if (document.cookie.length > 0) {
	        var offset = document.cookie.indexOf(search);
	        if (offset != -1) {
	            offset += search.length
	            var end = document.cookie.indexOf(";", offset);
	            if (end == -1) end = document.cookie.length;
	            returnvalue = unescape(document.cookie.substring(offset, end));
	        }
	    }
	    return returnvalue;
	},
	set_cookie: function(cookie_name, cookie_value, cookie_life, cookie_path) {
	    var category = '' + getParameterByName('category');
	    if (category.length > 0) {
	        var qpath = cookie_value.split("?");
	        cookie_value = qpath[0] + '?category=' + category;
	    }
	    var today = new Date();
	    var expiry = new Date(today.getTime() + cookie_life * 24 * 60 * 60 * 1000);
	    if (cookie_value != null && cookie_value != "") {
	        var cookie_string = cookie_name + "=" + escape(cookie_value);
	        
	        if (cookie_life && cookie_life != '0') {
	            cookie_string += "; expires=" + expiry.toGMTString();
	        }
	        
	        if (cookie_path) {
	            cookie_string += "; path=" + cookie_path;
	        }
	        document.cookie = cookie_string;
	    }
	},
	delete_cookie: function(name, path, domain) {
	    if (get_cookie(name)) document.cookie = name + "=" +
	        ((path) ? ";path=" + path : "") +
	        ((domain) ? ";domain=" + domain : "") +
	        ";expires=Thu, 01-Jan-1970 00:00:01 GMT";
	}
}