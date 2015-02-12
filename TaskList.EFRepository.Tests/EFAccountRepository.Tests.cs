using System;
using System.Linq;
using System.Data.Entity;
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
        private AccountRepository _repository;
        private IQueryable<EFData.User> _mockusers;

        [TestInitialize]
        public void Setup()
        {
            _context = Substitute.For<TaskListDataContext>();
            _dbusers = Substitute.For<DbSet<EFData.User>, IQueryable<EFData.User>>();

            _context.Users.Returns(_dbusers);
            _repository = new AccountRepository(_context);

        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
