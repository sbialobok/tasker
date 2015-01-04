var signalr = require('./modules/signalconnection');

module.exports = function () {
	//make contact
	signalr('blah', function(data) {
		console.log(data);
	});
}();