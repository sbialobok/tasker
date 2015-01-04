/** @jsx React.DOM */

var React = require('react');

var header = React.createClass({
	render: function () {
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
				<button className='logout'>Logout</button>
			</div>
		);
	}
});
	
module.exports = header;