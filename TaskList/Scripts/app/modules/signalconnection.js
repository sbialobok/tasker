//Functions used to connect to a service that uses signalr for duplex communication
//NOTE: there is an expectation that the signalr plugin script is included outside the app

var $ = require('jquery');
module.exports = function (appid, taskhandler) {
	this.connection = null;

	if(!$.connection) {
		throw 'Missing signalr plugin. Cannot create connection'; 
	}

	connection = $.connection('/updates');
	connection.received(function(data) {
		//If the message is for a new task use passed in task handler
		if(data.type === 'task') {
			taskhandler(data);
		}
	});
	connection.error(function(error){
		console.warn(error);
	});
	//Set connection ID as Team/user so that it can be put into a proper group when connected
	//TODO figure out how to set connection id to have team/user info
	connection.id = appid + '|' + connection.id;
	connection.start();
};