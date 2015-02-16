using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

using TaskList.EFData;
using TaskList.BizModels;

namespace TaskList.EFRepository.Tests
{
    [TestClass]
    public class EFTaskRepositoryTests
    {
        private TaskListDataContext _context;
        private DbSet<Task> _dbtasks;
        private TaskRepository _repository;
        private TaskItem _task;
        private IQueryable<Task> _mocktasks;
        private List<EFData.User> _mockusers;
        private List<EFData.Team> _mockteams;

        [TestInitialize]
        public void Setup()
        {
            _context = Substitute.For<TaskListDataContext>();
            _dbtasks = Substitute.For<DbSet<Task>, IQueryable<Task>>();

            _context.Tasks.Returns(_dbtasks);
            _repository = new TaskRepository(_context);
            _task = new TaskItem()
            {
                Id = 1,
                Owner = new TaskList.BizModels.User()
                {
                    Id = 2
                },
                Description = "description",
                DueDate = new DateTime(2000, 1, 1)
            };

            _mockteams = new List<EFData.Team>{
                new EFData.Team { ID=1, Name="Team1"},
                new EFData.Team { ID=2, Name="Team2"}
            };

            _mockusers = new List<EFData.User>{
                new EFData.User { ID=1, Name="User1", Team = _mockteams.ElementAt(0)},
                new EFData.User { ID=2, Name="User2", Team = _mockteams.ElementAt(0)},
                new EFData.User { ID=3, Name="User3", Team = _mockteams.ElementAt(1)},
            };

            //For use when testing query statements
            _mocktasks = new List<Task>
            {
                new Task { ID = 1, UserID = 1, Text = "text", User = _mockusers.ElementAt(0)},
                new Task { ID = 2, UserID = 1, User = _mockusers.ElementAt(0) },
                new Task { ID = 3, UserID = 2, User = _mockusers.ElementAt(1) },
                new Task { ID = 4, UserID = 3, User = _mockusers.ElementAt(2) },
                new Task { ID = 5, UserID = 3, User = _mockusers.ElementAt(2) },
            }.AsQueryable();
        }

        private void SetupQuery()
        {
            ((IQueryable<Task>)_dbtasks).Provider.Returns(_mocktasks.Provider);
            ((IQueryable<Task>)_dbtasks).Expression.Returns(_mocktasks.Expression);
            ((IQueryable<Task>)_dbtasks).ElementType.Returns(_mocktasks.ElementType);
            ((IQueryable<Task>)_dbtasks).GetEnumerator().Returns(_mocktasks.GetEnumerator());
        }

        #region Add
        [TestMethod, TestCategory("TaskRepository")]
        public void EFTaskRepository_Add_WillCallAddOnDBSet()
        {
            _repository.Add(_task);
            _dbtasks.Received().Add(Arg.Any<Task>());
        }

        [TestMethod, TestCategory("TaskRepository")]
        public void EFTaskRepository_Add_WillMapToEFObjectCorrectly()
        {
            _repository.Add(_task);
            _dbtasks.Received().Add(Arg.Is<Task>(x =>
                x.ID == _task.Id &&
                x.UserID  == _task.Owner.Id &&
                x.Text == _task.Description &&
                x.Date == _task.DueDate));
        }

        [TestMethod, TestCategory("TaskRepository"), ExpectedException(typeof(ArgumentNullException))]
        public void EFTaskRepository_Add_WithNullTask_WillThrowException()
        {
            _repository.Add(null);
            Assert.Fail("Shouldn't reach here due to exception");
        }
        #endregion Add

        #region GetTask
        [TestMethod, TestCategory("TaskRepository")]
        public void EFTaskRepository_GetTask_WillCallFindDBOnSet()
        {
            _dbtasks.Find(0).Returns(x => new Task
            {
                User = new EFData.User
                {
                    Team = new EFData.Team()
                }
            });
            _repository.GetTask(0);
            _dbtasks.Received().Find(Arg.Is(0));
        }

        [TestMethod, TestCategory("TaskRepository")]
        public void EFTaskRepository_GetTask_WillMapToBizObjectCorrectly()
        {
            var id = 0;
            var userid = 1;
            var text = "text";
            var teamName = "teamname";
            var date = new DateTime(2000,1,1);

            _dbtasks.Find(0).Returns(x => 
                new Task()
                {
                    ID = id,
                    User = new EFData.User {
                        ID = userid,
                        Team = new EFData.Team {
                            Name = teamName
                        }
                    },
                    Text = text,
                    Date = date
                }
            );
            var result = _repository.GetTask(0);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(userid, result.Owner.Id);
            Assert.AreEqual(text, result.Description);
            Assert.AreEqual(date, result.DueDate);
            Assert.AreEqual(teamName, result.TeamName);
        }

        [TestMethod, TestCategory("TaskRepository"), ExpectedException(typeof(ArgumentNullException))]
        public void EFTaskRepository_GetTask_WithInvalidId_WillThrowException()
        {
            _dbtasks.Find(0).Returns(x => null);
            _repository.GetTask(0);
            Assert.Fail("Shouldn't reach here due to exception");
        }
        #endregion GetTask

        #region GetTaskByUserID
        [TestMethod, TestCategory("TaskRepository")]
        public void EFTaskRepository_GetTaskByUserID_WillFilterTasksByUserIDs()
        {
            SetupQuery();
            var result = _repository.GetTasksByUserID(new int[] { 1, 2, 3 });
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod, TestCategory("TaskRepository")]
        public void EFTaskRepository_GetTaskByUserID_WillCovertFilteredResultsToBizObjects()
        {
            SetupQuery();
            var result = _repository.GetTasksByUserID(new int[] { 1 });
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(t => t.Owner.Id == 1));
        }
        #endregion GetTaskByUserID
    }
}
