using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

using TaskList.EFData;
using TaskList.BizModels;
using TaskList.EFRepository;

namespace TaskList.EFRepository.Tests
{
    [TestClass]
    public class EFAccountRepositoryTests
    {
        private TaskListDataContext _context;
        private DbSet<EFData.User> _dbusers;
        private DbSet<EFData.Team> _dbteams;
        private AccountRepository _repository;
        private IQueryable<EFData.User> _mockusers;
        private IQueryable<EFData.Team> _mockteams;

        [TestInitialize]
        public void Setup()
        {
            _context = Substitute.For<TaskListDataContext>();
            _dbusers = Substitute.For<DbSet<EFData.User>, IQueryable<EFData.User>>();
            _dbteams = Substitute.For<DbSet<EFData.Team>, IQueryable<EFData.Team>>();

            _context.Users.Returns(_dbusers);
            _context.Teams.Returns(_dbteams);
            _repository = new AccountRepository(_context);

            _mockteams = new List<EFData.Team>
            {
                new EFData.Team{ ID=1, Name="Team1"},
                new EFData.Team{ ID=2, Name="Team2"},
                new EFData.Team{ ID=3, Name="Team3"}
            }.AsQueryable();

            _mockusers = new List<EFData.User>
            {
                new EFData.User{ ID=1, Name="User1", TeamID=1},
                new EFData.User{ ID=2, Name="User2", TeamID=1},
                new EFData.User{ ID=3, Name="User3", TeamID=2},
                new EFData.User{ ID=4, Name="User4", TeamID=2},
                new EFData.User{ ID=5, Name="User5", TeamID=3}
            }.AsQueryable();

            ((IQueryable<EFData.Team>)_dbteams).Provider.Returns(_mockteams.Provider);
            ((IQueryable<EFData.Team>)_dbteams).Expression.Returns(_mockteams.Expression);
            ((IQueryable<EFData.Team>)_dbteams).ElementType.Returns(_mockteams.ElementType);
            ((IQueryable<EFData.Team>)_dbteams).GetEnumerator().Returns(_mockteams.GetEnumerator());

            ((IQueryable<EFData.User>)_dbusers).Provider.Returns(_mockusers.Provider);
            ((IQueryable<EFData.User>)_dbusers).Expression.Returns(_mockusers.Expression);
            ((IQueryable<EFData.User>)_dbusers).ElementType.Returns(_mockusers.ElementType);
            ((IQueryable<EFData.User>)_dbusers).GetEnumerator().Returns(_mockusers.GetEnumerator());
        }

        #region Teams
        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_AddTeam_WillCallAddOnDBSet()
        {
            _repository.AddTeam("NewTeam");
            _dbteams.Received().Add(Arg.Any<EFData.Team>());
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_AddTeam_WillSetNameProperly()
        {
            _repository.AddTeam("NewTeam");
            _dbteams.Received().Add(Arg.Is<EFData.Team>(t =>
                t.Name == "NewTeam"));
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_GetTeam_WillReturnNull_IfTeamDoesNotExist()
        {
            Assert.IsNull(_repository.GetTeam("NewTeam"));
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_GetTeam_WillReturnAppropriateTeam_IfNameExists()
        {
            var expected = _mockteams.ElementAt(0);
            var team = _repository.GetTeam(expected.Name);
            Assert.AreEqual(expected.ID, team.Id);
        }
        #endregion Teams

        #region Users
        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_AddUser_WillCallAddOnDBSet()
        {
            _repository.AddUser("user", "Team1");
            _dbusers.Received().Add(Arg.Any<EFData.User>());
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_AddUser_WillSetNameProperly()
        {
            var name = "user";
            _repository.AddUser(name, "Team1");
            _dbusers.Received().Add(Arg.Is<EFData.User>(u =>
                u.Name == name));
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_AddUser_WillSetTeamIDProperly()
        {
            var team = _mockteams.ElementAt(0);
            _repository.AddUser("user", team.Name);
            _dbusers.Received().Add(Arg.Is<EFData.User>(u =>
                u.TeamID == team.ID));
        }

        [TestMethod, TestCategory("AccountRepository"), ExpectedException(typeof(InvalidOperationException))]
        public void EFAccountRepository_AddUser_WillThrowException_IfTeamDoesNotExist()
        {
            _repository.AddUser("user", "invalidteam");
            Assert.Fail("Shouldn't reach here due to exception");
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_GetUser_WillReturnNull_IfTeamDoesNotExist()
        {
            var result = _repository.GetUser("invalidteam", "User1");
            Assert.IsNull(result);
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_GetUser_WillReturnNull_IfUserDoesNotExist()
        {
            var result = _repository.GetUser("Team1", "invalidUser");
            Assert.IsNull(result);
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_GetUser_WillReturnAppropriateUser()
        {
            var user = _mockusers.ElementAt(0);
            var team = _mockteams.ElementAt(0);
            var result = _repository.GetUser(team.Name, user.Name);
            Assert.AreEqual(user.ID, result.Id);
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_GetUsersByTeam_WillReturnNull_IfTeamDoesNotExist()
        {
            var result = _repository.GetUsersByTeam("invalidteam");
            Assert.IsNull(result);
        }

        [TestMethod, TestCategory("AccountRepository")]
        public void EFAccountRepository_GetUsersByTeam_WillReturnProperNumberOfUsers()
        {
            var team = _mockteams.ElementAt(0);
            var result = _repository.GetUsersByTeam(team.Name);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(u => u.Team.Id == team.ID));
        }
        #endregion Users
    }
}
