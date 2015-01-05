/** @jsx React.DOM */

//UI that builds the main tasks page
var React = require('react'),
	taskheader = require('./header'),
	tasklist = require('./usertasklist'),
	taskmanager = require('../modules/taskmanager');

var main = React.createClass({
	onAddTask: function (task) {
		taskmanager.AddTask(this.props.teamname, this.props.username, task.editedTask.description[0], task.editedTask.duedate[0]);
	},
	getDefaultProps: function () {
		return {
			username: 'johndoe',
			teamname: 'team',
			tasks: []
		};
	},
	render: function () {
		//Ideally two tasks list will be rendered here.  One for the logged in user and one for the currently selected tab (if there is one)
		return(
			<div className='tasks'>
				<taskheader username={this.props.username}/>
				<div className='taskbody'>
					<tasklist onAddTask={this.onAddTask} username={this.props.username} tasks={this.props.tasks}/>
				</div>
			</div>
		);
	}
}); 
	
module.exports = main;