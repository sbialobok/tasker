/** @jsx React.DOM */

var React = require('react'),
	taskheader = require('./header');

var main = React.createClass({
	getDefaultProps: function () {
		return {
			username: 'johndoe'
		};
	},
	render: function () {
		return(
			<div className='tasks'>
				<taskheader username={this.props.username}/>
				<div className='taskbody'>
				</div>
			</div>
		);
	}
}); 
	
module.exports = main;