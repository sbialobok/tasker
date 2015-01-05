
//Interfaces with the tasklist server
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
	},
	AddTask: function (teamname, owner, description, duedate) {
		//Adds a task to the collection

		var task = {
			Owner: owner,
			TeamName: teamname,
			Description: description,
			DueDate: duedate,
			Id: -1,
			Shared: []
		};

		var json = JSON.stringify(task);
		return $.ajax({
			url: '/task/addtask',
			type: 'POST',
			dataType: 'json',
			data: json,
			contentType: "application/json; charset=utf-8",
			error: function (error) {
				console.warn('Error getting team tasks. ' + error);
			}
		});
	}
};