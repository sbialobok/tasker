/** @jsx React.DOM */

//Handles the rendering of the two basic pages for the app. Login and tasks
var React = require('react'),
	login = require('./login'),
	mainui = require('./main');

var page = function () {
	var mountNode = 'app'; //This is the id of the div tag that the index page renders
	function renderLogin(onLogin) {
		React.renderComponent(
			<login onLogin={onLogin} />,
			document.getElementById(mountNode)
		);
	};

	function renderTasks(teamname, username, tasks) {
		React.renderComponent(
			<mainui teamname={teamname} username={username} tasks={tasks}/>,
			document.getElementById(mountNode)
		);
	};

	return {
		renderLogin: renderLogin,
		renderTasks: renderTasks
	};
}();

module.exports = page;