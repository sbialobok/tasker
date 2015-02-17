
//Handles a client side cache of the tasks and interfaces with the task service
var service = require('../services/taskservice');

module.exports = function () {
	var cache = {},
		team = null,
		owner = null;

	function LoadTeamTasks (teamName) {
		var processed = $.Deferred();
		var promise = service.GetTasks(teamName);
		promise.done(function(data) {
			try {
				var taskarray = ProcessTasks(data);
				processed.resolve(taskarray);
			} catch(err) {
				processed.fail(err);
			}
		});
		return processed;
	}

	function ProcessTasks (data) {
		var retval = [];
		//Expecting an array of objects
		if(!(data instanceof Array)) {
			throw 'Cannot process tasks.  Unexpected data returned';
		}
		for (var i = 0; i < data.length; i++ ) {
			var task = ProcessTask(data[i]);
			retval.push(task);
		}
		return retval;
	}

	function ProcessTask(data) {
		var task = data;
		var duedate = new Date(parseInt(task.DueDate.substr(6)));
		var month = duedate.getMonth();
		month++;
		task.DueDate = (month.toString() + '/' + duedate.getDate().toString() + '/' + duedate.getFullYear().toString());
		cache[task.Id] = task;
		return task;
	}

	function SignalHandler (data) {
		//TODO: this will handle task related signals from server and make updates to the cache.
	}

	function GetTasks() {
		return cache;
	}

	function GetTask(taskId) {
		var processed = $.Deferred();
		var promise = service.GetTask(taskId);
		promise.done(function(task) {
			try {
				var ptask = ProcessTask(task);
				processed.resolve(ptask); 
			} catch(err) {
				processed.fail(err);
			}
		});

		return processed;
	}

	function AddTask(teamname, owner, description, duedate) {
		return service.AddTask(teamname, owner, description, duedate);
	}

	function UpdateTask(taskid, description, duedate) {
		return service.UpdateTask(taskid, description, duedate);
	}

	return {
		LoadTeamTasks: LoadTeamTasks,
		GetTasks: GetTasks,
		AddTask: AddTask,
		UpdateTask: UpdateTask,
		SignalHandler: SignalHandler
	}
}();