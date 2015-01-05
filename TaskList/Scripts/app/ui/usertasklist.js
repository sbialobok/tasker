/** @jsx React.DOM */

//Represents a users task list. Will be used to display both the logged in users tasks and the selected teammates tasks
var React = require('react'),
	taskentry = require('./taskentry');

var userTaskList = React.createClass({
	getInitialState: function () {
		return {
			editingTask: -1 //keep track of the state of which task is being edited (if any)
		}
	},
	getDefaultProps: function () {
		return {
			username: 'johndoe',
			tasks:[]
		};
	},
	onTaskClick: function (event, task) {
		//Gets passed into every rendered task.  Will change from review to edit mode
		//TODO: Might want to add some kind of wanting here is another non -1 task is being edited (DO you want to save your changes?)
		this.setState({
			editingTask: task.props.id
		});
	},
	onTaskSave: function (event, task) {
		//TODO Still need to setup updating
		this.setState({
			editingTask: -1
		});
	},
	onAddTask: function (event, task) {
		if(this.props.onAddTask) {
			this.props.onAddTask(task);
		}
	},
	render: function () {
		var tasks = this.props.tasks.map(function (task, index) {
			//We only want tasks that belong to the username passed in or if they are shared.  
			//TODO: Add shared check
			if(task.Owner === this.props.username) {
				//If this task is being edited make sure its rendered in the proper mode
				var mode = this.state.editingTask === task.Id ? 'edit' : 'review';
				return(
					<taskentry
						description = {task.Description}
						duedate = {task.DueDate}
						id = {task.Id}
						owner= {task.Owner}
						mode={mode} 
						onTaskClick={this.onTaskClick}
						onTaskSave={this.onTaskSave}/>
				);
			} else {
				return null;
			}
			
		}.bind(this));

		//Render a blank task entry for the top to add new stuff. TODO: hide for the user that isn't logged in
		//Render all of the currently existing tasks below the new task entry bar
		return(
			<div className='tasklist'>
				<div className='taskbody'>
					<taskentry
						id = {-1}
						mode={'new'}
						onTaskSave={this.onAddTask}/>
					{tasks}
				</div>
			</div>
		);
	}
}); 
	
module.exports = userTaskList;