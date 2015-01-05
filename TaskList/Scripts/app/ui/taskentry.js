/** @jsx React.DOM */

//Represents a single task to be rendered.
var React = require('react');

var task = React.createClass({
	//if the task has passed all requirements for the text input
	validTask: false,
	//the values of the text input fields after being parsed
	editedTask: {
		description: [],
		shares: [],
		duedate: []
	},
	getDefaultProps: function () {
		return {
			description: 'Enter task description',
			duedate: '',
			id: 0,
			owner: 'johndoe',
			shared: [],
			mode: 'review'
		};
	},
	parseTask: function (event) {
		var text = event.target.value;

		//This implies that the TODO needs to come first with the description.  I think thats what the spec stated.
		this.editedTask.description = text.match(/TODO:[\s\S][^@^]*/g);
		this.editedTask.shares = text.match(/@[\w]*/g);
		this.editedTask.duedate = text.match(/\^[\d\/]*/g);

		if(this.editedTask.description && this.editedTask.description.length) {
			//TODO: Check valid user and check valid date
			this.validTask = true;
		} else {
			this.validTasl = false;
		}
	},
	handleTaskClick: function (event) {
		//Handles the click on a task. Calls passed in handler.
		if(this.props.mode === 'review' && this.props.onTaskClick) {
			this.props.onTaskClick(event, this);
		}
	},
	handleTaskSave: function (event) {
		//Checks if the entered text passed all checks before saving the task
		if(this.validTask) {
			if(this.props.onTaskSave) {
				this.props.onTaskSave(event, this);
			}
		} else {
			alert('Invalid task format.  All tasks must start with "TODO:", shares must begin with an "@" then the users name (@Bob) and the duedate must start with ^.');
		}
	},
	renderEdit: function () {
		//Render an input and button for edit mode.
		//TODO: Add handler for keyboard enter press
		return (
			<div className='taskedit'>
				<input type="text" className="taskentry" onChange={this.parseTask} value={this.props.description} />
				<button className="submit" onClick={this.handleTaskSave}>{this.props.mode === 'new' ? 'Add' : 'Save'}</button>
			</div>
		);
	},
	renderReview: function () {
		//Review mode which also handles clicks to go into edit mode
		return (
			<div className='taskreview' onClick={this.handleTaskClick}>
				<label className='taskowner'>{this.props.owner}</label>
				<label className='taskdescription'>{this.props.description}</label>
				<label className='taskduedate'>{this.props.duedate}</label>
			</div>
		);
	},
	render: function () {
		var meat = this.props.mode === 'review' ? this.renderReview() : this.renderEdit();

		return (
			<div className='task'>
				{meat}
			</div>
		);
	}
}); 
	
module.exports = task;