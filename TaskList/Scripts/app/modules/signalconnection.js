//Functions used to connect to a service that uses signalr for duplex communication
//NOTE: there is an expectation that the signalr

var $ = require('jquery');
module.exports = function (url, handler) {
	this.connection = null;

	if(!$.connection) {
		throw 'Missing signalr plugin. Cannot create connection'; 
	}

	connection = $.connection('/updates');
	connection.received(function(data) {
		console.log(data);
	});
	connection.error(function(error){
		console.warn(error);
	});
	//Set connection ID as Team/user so that it can be put into a proper group when connected
	connection.start();
};