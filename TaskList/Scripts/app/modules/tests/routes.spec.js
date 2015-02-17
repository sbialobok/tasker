//Unit tests for the routes file

var rewire = require('rewire'),
	routes = rewire('../routes'),
	helper = require('test/testhelper');

describe('Routes', function () {
	var spies = undefined,
		hasher = undefined,
		crossroads = undefined;

	beforeEach(function () {
		//Mock dependencies for routes.
		spies = {
			utils: jasmine.createSpyObj('utils', ['set_cookie', 'get_cookie', 'delete_cookie']),
			taskmanager: jasmine.createSpyObj('taskmanager', ['SignalHandler', 'LoadTeamTasks']),
			page: jasmine.createSpyObj('page', ['renderLogin', 'renderTasks']),
			signalr: jasmine.createSpy('signalr')
		};

		routes.__set__('utils', spies.utils);
		routes.__set__('taskmanager', spies.taskmanager);
		routes.__set__('page', spies.page);
		routes.__set__('signalr', spies.signalr);

		//Didn't bother mocking hasher or crossroads because the functionality is built on those two dependencies
		hasher = routes.__get__('hasher');
		crossroads = routes.__get__('crossroads');

		//Spy on some hasher calls just to make sure they are being called.
		spyOn(hasher, 'init').and.callThrough();
		spyOn(hasher, 'replaceHash').and.callThrough();
		spyOn(hasher, 'setHash').and.callThrough();
		routes.Init();
	});

	afterEach(function () {
		hasher.setHash('');
		crossroads.resetState();
		routes.Destroy();
	});
	describe('on init', function () {
		it('should initialize hasher', function () {
			expect(hasher.init).toHaveBeenCalled();
		});
		it('should set hash to login', function () {
			expect(hasher.replaceHash).toHaveBeenCalledWith('login');
		});
	});
	
	//We dont need any before each as the login route is the default one, as tested above.
	describe('login route', function () {
		beforeEach(function() {
			spies.taskmanager.LoadTeamTasks.and.returnValue($.Deferred());
			hasher.setHash('');
			crossroads.resetState();
		});
		it('should call get_cookie', function () {
			expect(spies.utils.get_cookie).toHaveBeenCalled();
		});
		it('should set the hash to tasks if the cookie is found', function () {
			spies.utils.get_cookie.and.returnValue('team|user');
			hasher.setHash('login');
			expect(hasher.setHash.calls.argsFor(2)[0]).toBe('tasks');
		});
		it('should render the login screen if the cookie is not found', function () {
			hasher.setHash('login');
			expect(spies.page.renderLogin).toHaveBeenCalled();
		});
	});

	describe('tasks route', function () {
		var promise = undefined;
		beforeEach(function() {
			promise = $.Deferred();
			spies.taskmanager.LoadTeamTasks.and.returnValue(promise);
			hasher.setHash('');
			crossroads.resetState();
		});
		it('should call get_cookie', function () {
			hasher.setHash('tasks');
			expect(spies.utils.get_cookie).toHaveBeenCalled();
		});
		it('should set the hash to login if the cookie is not found', function () {
			hasher.setHash('tasks');
			expect(hasher.setHash.calls.argsFor(2)[0]).toBe('login');
		});
		it('should render the tasks screen if the cookie is found', function () {
			spies.utils.get_cookie.and.returnValue('team|user');
			hasher.setHash('tasks');
			promise.resolve(null);
			expect(spies.page.renderTasks).toHaveBeenCalledWith('team','user', null);
		});
	});

	describe('logout route', function () {
		beforeEach(function() {
			hasher.setHash('');
			crossroads.resetState();
		});

		it('should call delete cookie', function () {
			hasher.setHash('logout');
			expect(spies.utils.delete_cookie).toHaveBeenCalled();
		});

		it('should set hash to login', function () {
			hasher.setHash('logout');
			expect(hasher.setHash.calls.argsFor(2)[0]).toBe('login');
		});
	});
});