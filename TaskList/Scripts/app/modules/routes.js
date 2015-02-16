
var crossroads = require('crossroads'),
	hasher = require('hasher'),
	utils = require('./utils'),
	page = require('../ui/page'),
	taskmanager = require('./taskmanager'),
	signalr = require('./signalconnection');

//Handles the routing between the login page and the task page.
module.exports = function () {
	var cookiekey = 'tasker';

	if(!crossroads) {
		throw 'Error initializing routes.  Crossroads not found';
	}
	if(!hasher) {
		throw 'Error initializing routes.  Hasher not found';
	}

	function onLogin(teamname, username) {
		//Sets the cookie then moves to the tasks page
		var val = teamname + '|' + username;
		utils.set_cookie(cookiekey, val);
		hasher.setHash('tasks');
	}

	crossroads.addRoute('login', function() {
		//Use the presence of a cookie to determine if the user is logged in or not.
		var cookie = utils.get_cookie(cookiekey);
		if(cookie) {
			hasher.setHash('tasks');
		}
		page.renderLogin(onLogin);
	});
	crossroads.addRoute('tasks', function() {
		var cookie = utils.get_cookie(cookiekey);
		if(!cookie) {
			hasher.setHash('login');
			return;
		}
		var names = cookie.split('|');

		//start listening on the websocket
		signalr(cookie, taskmanager.SignalHandler);

		//pull in tasks
		var promise = taskmanager.LoadTeamTasks(names[0]); 
		
		//Once tasks are done, push data to ui
		promise.done(function(tasks){
			page.renderTasks(names[0], names[1], tasks);
		});
	});

	//TODO: just have the logout button set the hash to logout and we can run this
	crossroads.addRoute('logout', function() {
		var val = utils.delete_cookie(cookiekey);
		hasher.setHash('login');
	});

	//Make sure that crossroads parses any changes to the hash
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