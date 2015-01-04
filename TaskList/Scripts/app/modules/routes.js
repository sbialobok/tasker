
var crossroads = require('crossroads'),
	hasher = require('hasher'),
	utils = require('./utils'),
	page = require('../ui/page'),
	signalr = require('./signalconnection');

module.exports = function () {
	var cookiekey = 'tasker'

	if(!crossroads) {
		throw 'Error initializing routes.  Crossroads not found';
	}
	if(!hasher) {
		throw 'Error initializing routes.  Hasher not found';
	}

	function onLogin() {
		hasher.setHash('tasks');
	}
	crossroads.addRoute('login', function() {
		//Use the presence of a cookie to determine if the user is logged in or not.
		var cookie = utils.get_cookie(cookiekey);
		if(cookie) {
			hasher.setHash('tasks');
			//make contact
			// signalr('blah', function(data) {
			// 	console.log(data);
			// });
		}
		page.renderLogin(onLogin);
	});
	crossroads.addRoute('tasks', function() {
		page.renderTasks();
	});

	hasher.changed.add(function(newhash, oldhash) {
		crossroads.parse(newhash);
	});

	hasher.initialized.add(function(hash) {
		//If there is no current hash set when hash is initialized than default to login
		if(hash === '') {
			hasher.replaceHash('login');
		} else {
            crossroads.parse(hash);
        }	
	});

	hasher.init();
};