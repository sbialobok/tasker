
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
			error: function (xhr, message, error) {
				console.warn('Error getting team tasks. ' + error);
			}
		});
	},
	GetTask: function (taskid) {

		return $.ajax({
			url: '/task/gettask',
			type: 'GET',
			data: {
				taskId: taskid
			},
			error: function (xhr, message, error) {
				console.warn('Error getting task. ' + message);
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
			data: json,
			contentType: "application/json; charset=utf-8",
			error: function (xhr, message, error) {
				console.warn('Error adding task. ' + message);
			}
		});
	},
	UpdateTask: function (taskid, description, duedate) {

		var data = {
			Id: taskid,
			Description: description,
			DueDate: duedate,
			Shared:[],
			Owner:'',
			TeamName:''
		};

		var json = JSON.stringify(data);
		return $.ajax({
			url: '/task/updatetask',
			type: 'POST',
			data: json,
			contentType: "application/json; charset=utf-8",
			error: function (xhr, message, error) {
				console.warn('Error updating task. ' + message);
			}
		});
	}
};