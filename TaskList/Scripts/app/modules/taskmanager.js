
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
			var task = data[i];
			cache[task.Id] = task;
			retval.push(task);
		}
		return retval;
	}

	function SignalHandler (data) {
		//TODO: this will handle task related signals from server and make updates to the cache.
	}

	function GetTasks() {
		return cache;
	}

	function AddTask(teamname, owner, description, duedate) {
		service.AddTask(teamname, owner, description, duedate);
	}

	return {
		LoadTeamTasks: LoadTeamTasks,
		GetTasks: GetTasks,
		AddTask: AddTask,
		SignalHandler: SignalHandler
	}
}();