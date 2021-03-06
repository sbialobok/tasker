//Interfaces with tasklist server for account related actions
var $ = require('jquery')
module.exports = {
	LoginUser: function (teamname, username) {
		//Returns a deferred object for a login request
		
		return $.ajax({
			url: '/home/login',
			data: {
				team: teamname,
				user: username
			},
			error: function (error) {
				console.warn('Error logging in. ' + error);
			}
		});
	},
	GetUsers: function (teamname) {

		return $.ajax({
			url: '/home/getusers',
			data: {
				team: teamname
			},
			error: function (error) {
				console.warn('Error logging in. ' + error);
			}
		});
	}
};