using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskList.EFData;
using TaskList.RepositoryContracts;

namespace TaskList.EFRepository
{
    /// <summary>
    /// Return an EFUnitOfWork to be used by services
    /// </summary>
    public class WorkFactory : IWorkFactory
    {
        public IUnitOfWork GetWorkUnit()
        {
            return new UnitOfWork();
        }
    }

    /// <summary>
    /// Wraps creation and disposing of the db context for repository actions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private TaskListDataContext _context = new TaskListDataContext();

        private ITaskRepository _taskRepository;
        public ITaskRepository TaskRepository
        {
            get
            {
                if (_taskRepository == null)
                {
                    _taskRepository = new TaskRepository(_context);
                }
                return _taskRepository;
            }
        }

        private IAccountRepository _accountRepository;
        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_context);

                return _accountRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
