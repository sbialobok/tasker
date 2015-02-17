/** @jsx React.DOM */

//UI that builds the main tasks page
var React = require('react'),
	taskheader = require('./header'),
	tasklist = require('./usertasklist'),
	tabbedlist = require('./tabbedlist'),
	taskmanager = require('../modules/taskmanager');

var main = React.createClass({
	onAddTask: function (task) {
		taskmanager.AddTask(this.props.teamname, this.props.username, task.editedTask.description, task.editedTask.duedate).done(function() {
			if(this.props.onUpdate) {
				this.props.onUpdate();
			}
		}.bind(this));
	},
	onUpdateTask: function(task) {
		taskmanager.UpdateTask(task.props.id, task.editedTask.description, task.editedTask.duedate).done(function(){
			if(this.props.onUpdate) {
				this.props.onUpdate();
			}
		}.bind(this));
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
				<taskheader username={this.props.username} onLogout={this.props.onLogout}/>
				<div className='taskbody'>
					<tasklist 
						onAddTask={this.onAddTask} 
						onUpdateTask={this.onUpdateTask} 
						username={this.props.username} 
						tasks={this.props.tasks}/>
				</div>
				<tabbedlist 
					username={this.props.username}
					tasks={this.props.tasks}
					users={this.props.users}/>
			</div>
		);
	}
}); 
	
module.exports = main;