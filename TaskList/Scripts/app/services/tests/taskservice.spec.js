var taskService = require('../taskservice');

describe('Task service', function () {
	describe('GetTasks', function () {
		var teamname = 'teamname',
			request = undefined;

		beforeEach(function() {
			jasmine.Ajax.install();
			taskService.GetTasks(teamname);
			request = jasmine.Ajax.requests.mostRecent();
		});
	
		it('should call correct url', function () {
			expect(request.url).toBe('/task/gettasks?teamName=' + teamname);
		});

		it('should make GET request', function () {
			expect(request.method).toBe('GET');
		});
	});

	describe('AddTask', function () {
		var task = {
			owner: 'owner',
			teamname: 'teamname',
			desc: 'desc',
			duedate: 'duedate'
		},
		data = undefined;

		beforeEach(function () {
			jasmine.Ajax.install();
			taskService.AddTask(task.teamname, task.owner, task.desc, task.duedate);
			request = jasmine.Ajax.requests.mostRecent();
			data = request.data();
		});

		afterEach(function () {
			data = undefined;
		})

		it('should call correct url', function () {
			expect(request.url).toBe('/task/addtask');
		});

		it('should make POST request', function () {
			expect(request.method).toBe('POST');
		});

		it('should pass owner as data', function () {
			expect(data.Owner).toBe(task.owner);
		});

		it('should pass team name as data', function () {
			expect(data.TeamName).toBe(task.teamname);
		});

		it('should pass description as data', function () {
			expect(data.Description).toBe(task.desc);
		});

		it('should pass duedate as data', function () {
			expect(data.DueDate).toBe(task.duedate);
		});
	})
});