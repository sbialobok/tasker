/** @jsx React.DOM */

//Handles rendering all of the header information
var React = require('react');

var header = React.createClass({
	onLogoutClick: function (event, ui) {
		if(this.props.onLogout) {
			this.props.onLogout();
		}
	},
	render: function () {
		//Thinking of doing a table + list of users to meet the requirement.  Might look messy
		return(
			<div className='header'>
				<label className='username'>{this.props.username}</label>
				<table className='teamstats'>
					<thead>
						<tr className='header'>
							<th>User</th>
							<th>Tasks</th>
							<th>Shared Tasks</th>
							<th>Non-shared Tasks</th>
						</tr>
					</thead>
				</table>
				<button className='logout' onClick={this.onLogoutClick}>Logout</button>
			</div>
		);
	}
});
	
module.exports = header;