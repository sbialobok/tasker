/** @jsx React.DOM */

//Represents a single task to be rendered.
var React = require('react');

var task = React.createClass({
	//the values of the text input fields after being parsed
	editedTask: {
		description: undefined,
		shares: [],
		duedate: undefined
	},
	getInitialState: function () {
		return {
			validTask: false
		};
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
		var text = event.target.value,
			date = undefined;
			parseddate = undefined,
			desc = undefined;

		//This implies that the TODO needs to come first with the description.  I think thats what the spec stated.
		desc = text.match(/TODO:[\s\S][^@^]*/g);
		this.editedTask.shares = text.match(/@[\w]*/g);
		date = text.match(/\^[\d\/]*/g);

		if(date && date.length) {
			date = date[0].replace('^','');
			parseddate = Date.parse(date);
		}

		if(desc && desc.length) {
			desc = desc[0].replace('TODO:','')
		}
		
		if(desc && date && !isNaN(parseddate)) {

			this.editedTask.duedate = date;
			this.editedTask.description = desc;

			//TODO: Check valid user and check valid date
			this.setState({
				validTask: true
			});
		} else {
			this.setState({
				validTask: false
			});
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
		if(this.state.validTask) {
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
		var val = this.props.description;
		if(val !== "Enter task description") {
			val = 'TODO:' + val + '^' + this.props.duedate;
		}
		return (
			<div className='taskedit'>
				<input id="description" type="text" className="taskentry" onChange={this.parseTask} defaultValue={val}/>
				<button className="submit" onClick={this.handleTaskSave}>{this.props.mode === 'new' ? 'Add' : 'Save'}</button>
			</div>
		);
	},
	renderReview: function () {
		//Review mode which also handles clicks to go into edit mode
		return (
			<div className='taskreview' onDoubleClick={this.handleTaskClick}>
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