/** @jsx React.DOM */

//Handles rendering of the login page
var React = require('react'),
	accountService = require('../services/accountservice');

var login = React.createClass({
	getInitialState: function () {
		return {
			validTeamName: false,
			validUserName: false,
			userName: '',
			teamName: ''
		};
	},
	validateName: function (name) {
		//TODO: replace with actual regex match to confirm it meets specs
		return true;
	},
	validateTeamName: function (event) {
		this.setState({
			validTeamName: this.validateName(event.target.value),
			teamName: event.target.value
		});
	},
	validateUserName: function (event) {
		this.setState({
			validUserName: this.validateName(event.target.value),
			userName: event.target.value
		});
	},
	submit: function (event) {
		var promise = accountService.LoginUser(this.state.teamName, this.state.userName);
		promise.done(function() {
			if(this.props.onLogin) {
				this.props.onLogin(this.state.teamName, this.state.userName);
			}
		}.bind(this));
	},
	render: function () {
		return(
			<div className='login'>
				<label htmlFor="teamname">Team Name</label>
				<input type="text" className="teamname" onBlur={this.validateTeamName} required />
				<label htmlFor="username">User Name</label>
				<input type="text" className="username" onBlur={this.validateUserName} required />
				<button className="submit" onClick={this.submit} disabled={(!this.state.validTeamName || !this.state.validUserName) ? 'disable' : ''}>Submit</button>
			</div>
		);
	}
});
	
module.exports = login;