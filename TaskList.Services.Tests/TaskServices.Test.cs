using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using TaskList.RepositoryContracts;
using TaskList.Services;
using TaskList.BizModels;

namespace TaskList.Services.Tests
{
    [TestClass]
    public class TaskServicesTest
    {
        IWorkFactory _factory;
        IUnitOfWork _work;
        ITaskRepository _taskrepo;
        TaskService _service;
        TaskItem _task;

        [TestInitialize]
        public void Setup()
        {
            _factory = Substitute.For<IWorkFactory>();
            _work = Substitute.For<IUnitOfWork, IDisposable>();
            _taskrepo = Substitute.For<ITaskRepository>();

            _work.TaskRepository.Returns(_taskrepo);
            _factory.GetWorkUnit().Returns(_work);
            _service = new TaskService(_factory);
            _task = new TaskItem()
            {
                Id = 1,
                Owner = new User()
                {
                    Id = 2
                },
                Description = "description",
                DueDate = new DateTime(2000, 1, 1)
            };
        }

        #region Add Tests
        [TestMethod, TestCategory("TaskService")]
        public void TaskService_AddingTask_ShouldCallTaskRepoAdd()
        {
            _service.AddTask(_task);
            _taskrepo.Received().Add(_task);
        }

        [TestMethod, TestCategory("TaskService")]
        public void TaskService_AddingTask_CallSaveOnWorkUnit()
        {
            _service.AddTask(_task);
            _work.Received().Save();
        }

        //Make sure that we have our unit of work wrapper in an using statement and properly disposed
        [TestMethod, TestCategory("TaskService"), ExpectedException(typeof(Exception))]
        public void TaskService_AddingTask_WillDisposeWorkUnit_OnException()
        {
            _taskrepo.When(x => x.Add(_task)).Do(x => { throw new Exception(); });
            _service.AddTask(_task);
            ((IDisposable)_work).Received().Dispose();
        }
        #endregion Add Tests
    }
}
