/** @jsx React.DOM */

var React = require('react'),
	login = require('./login'),
	mainui = require('./main');

var page = function () {
	var mountNode = 'app';
	function renderLogin(onLogin) {
		React.renderComponent(
			<login onLogin={onLogin} />,
			document.getElementById(mountNode)
		);
	};

	function renderTasks() {
		React.renderComponent(
			<mainui />,
			document.getElementById(mountNode)
		);
	};

	return {
		renderLogin: renderLogin,
		renderTasks: renderTasks
	};
}();

module.exports = page;