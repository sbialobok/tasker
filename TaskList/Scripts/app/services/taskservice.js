var $ = require('jquery');
module.exports = {
	GetTasks: function (teamname) {
		//Returns a deferred object for getting the teams requests
		
		return $.ajax({
			url: '/task/gettasks',
			data: {
				teamName: teamname,
			},
			error: function (error) {
				console.warn('Error getting team tasks. ' + error);
			}
		});
	}
};