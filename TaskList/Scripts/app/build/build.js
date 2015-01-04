var Task =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;
/******/
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "/app/build/";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	var signalr = __webpack_require__(1);
	
	module.exports = function () {
		//make contact
		signalr('blah', function(data) {
			console.log(data);
		});
	}();

/***/ },
/* 1 */
/***/ function(module, exports, __webpack_require__) {

	//Functions used to connect to a service that uses signalr for duplex communication
	//NOTE: there is an expectation that the signalr
	
	var $ = __webpack_require__(2);
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
		connection.start();
	};

/***/ },
/* 2 */
/***/ function(module, exports, __webpack_require__) {

	module.exports = jQuery;

/***/ }
/******/ ])
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vd2VicGFjay9ib290c3RyYXAgNDY2YTE4ZmU0OTk4N2U1OTY4NzMiLCJ3ZWJwYWNrOi8vLy4vdGFzay5qcyIsIndlYnBhY2s6Ly8vLi9tb2R1bGVzL3NpZ25hbGNvbm5lY3Rpb24uanMiLCJ3ZWJwYWNrOi8vL2V4dGVybmFsIFwialF1ZXJ5XCIiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7QUFBQTtBQUNBOztBQUVBO0FBQ0E7O0FBRUE7QUFDQTtBQUNBOztBQUVBO0FBQ0E7QUFDQSx1QkFBZTtBQUNmO0FBQ0E7QUFDQTs7QUFFQTtBQUNBOztBQUVBO0FBQ0E7O0FBRUE7QUFDQTtBQUNBOzs7QUFHQTtBQUNBOztBQUVBO0FBQ0E7O0FBRUE7QUFDQTs7QUFFQTtBQUNBLHdDOzs7Ozs7O0FDdENBOztBQUVBO0FBQ0E7QUFDQTtBQUNBO0FBQ0EsR0FBRTtBQUNGLEVBQUMsRzs7Ozs7O0FDUEQ7QUFDQTs7QUFFQTtBQUNBO0FBQ0E7O0FBRUE7QUFDQSw0RDtBQUNBOztBQUVBO0FBQ0E7QUFDQTtBQUNBLEdBQUU7QUFDRjtBQUNBO0FBQ0EsR0FBRTtBQUNGO0FBQ0EsRzs7Ozs7O0FDbkJBLHlCIiwic291cmNlc0NvbnRlbnQiOlsiIFx0Ly8gVGhlIG1vZHVsZSBjYWNoZVxuIFx0dmFyIGluc3RhbGxlZE1vZHVsZXMgPSB7fTtcblxuIFx0Ly8gVGhlIHJlcXVpcmUgZnVuY3Rpb25cbiBcdGZ1bmN0aW9uIF9fd2VicGFja19yZXF1aXJlX18obW9kdWxlSWQpIHtcblxuIFx0XHQvLyBDaGVjayBpZiBtb2R1bGUgaXMgaW4gY2FjaGVcbiBcdFx0aWYoaW5zdGFsbGVkTW9kdWxlc1ttb2R1bGVJZF0pXG4gXHRcdFx0cmV0dXJuIGluc3RhbGxlZE1vZHVsZXNbbW9kdWxlSWRdLmV4cG9ydHM7XG5cbiBcdFx0Ly8gQ3JlYXRlIGEgbmV3IG1vZHVsZSAoYW5kIHB1dCBpdCBpbnRvIHRoZSBjYWNoZSlcbiBcdFx0dmFyIG1vZHVsZSA9IGluc3RhbGxlZE1vZHVsZXNbbW9kdWxlSWRdID0ge1xuIFx0XHRcdGV4cG9ydHM6IHt9LFxuIFx0XHRcdGlkOiBtb2R1bGVJZCxcbiBcdFx0XHRsb2FkZWQ6IGZhbHNlXG4gXHRcdH07XG5cbiBcdFx0Ly8gRXhlY3V0ZSB0aGUgbW9kdWxlIGZ1bmN0aW9uXG4gXHRcdG1vZHVsZXNbbW9kdWxlSWRdLmNhbGwobW9kdWxlLmV4cG9ydHMsIG1vZHVsZSwgbW9kdWxlLmV4cG9ydHMsIF9fd2VicGFja19yZXF1aXJlX18pO1xuXG4gXHRcdC8vIEZsYWcgdGhlIG1vZHVsZSBhcyBsb2FkZWRcbiBcdFx0bW9kdWxlLmxvYWRlZCA9IHRydWU7XG5cbiBcdFx0Ly8gUmV0dXJuIHRoZSBleHBvcnRzIG9mIHRoZSBtb2R1bGVcbiBcdFx0cmV0dXJuIG1vZHVsZS5leHBvcnRzO1xuIFx0fVxuXG5cbiBcdC8vIGV4cG9zZSB0aGUgbW9kdWxlcyBvYmplY3QgKF9fd2VicGFja19tb2R1bGVzX18pXG4gXHRfX3dlYnBhY2tfcmVxdWlyZV9fLm0gPSBtb2R1bGVzO1xuXG4gXHQvLyBleHBvc2UgdGhlIG1vZHVsZSBjYWNoZVxuIFx0X193ZWJwYWNrX3JlcXVpcmVfXy5jID0gaW5zdGFsbGVkTW9kdWxlcztcblxuIFx0Ly8gX193ZWJwYWNrX3B1YmxpY19wYXRoX19cbiBcdF9fd2VicGFja19yZXF1aXJlX18ucCA9IFwiL2FwcC9idWlsZC9cIjtcblxuIFx0Ly8gTG9hZCBlbnRyeSBtb2R1bGUgYW5kIHJldHVybiBleHBvcnRzXG4gXHRyZXR1cm4gX193ZWJwYWNrX3JlcXVpcmVfXygwKTtcblxuXG4vKiogV0VCUEFDSyBGT09URVIgKipcbiAqKiB3ZWJwYWNrL2Jvb3RzdHJhcCA0NjZhMThmZTQ5OTg3ZTU5Njg3M1xuICoqLyIsInZhciBzaWduYWxyID0gcmVxdWlyZSgnLi9tb2R1bGVzL3NpZ25hbGNvbm5lY3Rpb24nKTtcclxuXHJcbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gKCkge1xyXG5cdC8vbWFrZSBjb250YWN0XHJcblx0c2lnbmFscignYmxhaCcsIGZ1bmN0aW9uKGRhdGEpIHtcclxuXHRcdGNvbnNvbGUubG9nKGRhdGEpO1xyXG5cdH0pO1xyXG59KCk7XG5cblxuLyoqKioqKioqKioqKioqKioqXG4gKiogV0VCUEFDSyBGT09URVJcbiAqKiAuL3Rhc2suanNcbiAqKiBtb2R1bGUgaWQgPSAwXG4gKiogbW9kdWxlIGNodW5rcyA9IDBcbiAqKi8iLCIvL0Z1bmN0aW9ucyB1c2VkIHRvIGNvbm5lY3QgdG8gYSBzZXJ2aWNlIHRoYXQgdXNlcyBzaWduYWxyIGZvciBkdXBsZXggY29tbXVuaWNhdGlvblxyXG4vL05PVEU6IHRoZXJlIGlzIGFuIGV4cGVjdGF0aW9uIHRoYXQgdGhlIHNpZ25hbHJcclxuXHJcbnZhciAkID0gcmVxdWlyZSgnanF1ZXJ5Jyk7XHJcbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gKHVybCwgaGFuZGxlcikge1xyXG5cdHRoaXMuY29ubmVjdGlvbiA9IG51bGw7XHJcblxyXG5cdGlmKCEkLmNvbm5lY3Rpb24pIHtcclxuXHRcdHRocm93ICdNaXNzaW5nIHNpZ25hbHIgcGx1Z2luLiBDYW5ub3QgY3JlYXRlIGNvbm5lY3Rpb24nOyBcclxuXHR9XHJcblxyXG5cdGNvbm5lY3Rpb24gPSAkLmNvbm5lY3Rpb24oJy91cGRhdGVzJyk7XHJcblx0Y29ubmVjdGlvbi5yZWNlaXZlZChmdW5jdGlvbihkYXRhKSB7XHJcblx0XHRjb25zb2xlLmxvZyhkYXRhKTtcclxuXHR9KTtcclxuXHRjb25uZWN0aW9uLmVycm9yKGZ1bmN0aW9uKGVycm9yKXtcclxuXHRcdGNvbnNvbGUud2FybihlcnJvcik7XHJcblx0fSk7XHJcblx0Y29ubmVjdGlvbi5zdGFydCgpO1xyXG59O1xuXG5cbi8qKioqKioqKioqKioqKioqKlxuICoqIFdFQlBBQ0sgRk9PVEVSXG4gKiogLi9tb2R1bGVzL3NpZ25hbGNvbm5lY3Rpb24uanNcbiAqKiBtb2R1bGUgaWQgPSAxXG4gKiogbW9kdWxlIGNodW5rcyA9IDBcbiAqKi8iLCJtb2R1bGUuZXhwb3J0cyA9IGpRdWVyeTtcblxuXG4vKioqKioqKioqKioqKioqKipcbiAqKiBXRUJQQUNLIEZPT1RFUlxuICoqIGV4dGVybmFsIFwialF1ZXJ5XCJcbiAqKiBtb2R1bGUgaWQgPSAyXG4gKiogbW9kdWxlIGNodW5rcyA9IDBcbiAqKi8iXSwic291cmNlUm9vdCI6IiIsImZpbGUiOiJidWlsZC5qcyJ9