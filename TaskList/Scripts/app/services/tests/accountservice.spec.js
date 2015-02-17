var accountService = require('../accountservice');

describe('Account service', function () {
	beforeEach(function() {
		jasmine.Ajax.install();
	});
	it('Will log error if there is an issue with the request', function () {
		spyOn(console, 'warn');
		accountService.LoginUser('team', 'user');
		var request = jasmine.Ajax.requests.mostRecent();
		request.respondWith({
			status: 500,
			responseText: 'Error'
		});

		expect(console.warn).toHaveBeenCalled();
	});
});