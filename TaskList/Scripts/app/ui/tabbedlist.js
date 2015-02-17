/** @jsx React.DOM */

var React = require('react'),
	taskentry = require('./taskentry');

var tabbedTasks = React.createClass({
	getDefaultProps: function () {
		return {
			users: [],
			tasks:[]
		};
	},
	getInitialState: function () {
		return {
			currentuser: ''
		};
	},
	setUser: function (event, ui) {
		var name = event.currentTarget.getAttribute('data-name');
		this.setState({
			currentuser: name 
		});
	},
	componentDidMount: function () {
		if(this.props.users) {
			//Loop through users until we find one who isn't the signed in user, default them as the selected tab
			for(var i in this.props.users) {
				var user = this.props.users[i];
				if(user.Name !== this.props.username) {
					this.setState({
						currentuser: user.Name
					});
					break;
				}
			}
		}
	},
	render: function () {
		var tasks = this.props.tasks.map(function (task, index) {
			//We only want tasks that belong to the username passed in or if they are shared.  
			//TODO: Add shared check
			if(task.Owner === this.state.currentuser) {
				//All nonuser tasks in review mode
				var mode = 'review';
				return(
					<taskentry
						key = {task.Id}
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

		var users = this.props.users.map(function (user, index) {
			if(user.Name !== this.props.username) {
				return(
					<li className={this.state.currentuser === user.Name ? "selected" : ''}>
						<a data-name={user.Name} onClick={this.setUser}> {user.Name} </a>
					</li>
				);
			} else {
				return null;
			}
		}.bind(this));

		//Render a blank task entry for the top to add new stuff. TODO: hide for the user that isn't logged in
		//Render all of the currently existing tasks below the new task entry bar
		return(
			<div className="tabbedList">
				<ul>
					{users}
				</ul>
				<div className='tasklist'>
					{tasks}
				</div>
			</div>
		);
	}
}); 
	
module.exports = tabbedTasks;