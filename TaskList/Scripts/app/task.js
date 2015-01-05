//Main entry point to the taskr app right now.  Currently just sets up the routes.
var routes = require('./modules/routes');
module.exports = function () {
	routes();
}();