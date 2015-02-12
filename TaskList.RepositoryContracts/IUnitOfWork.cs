using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.RepositoryContracts
{
    public interface IWorkFactory
    {
        IUnitOfWork GetWorkUnit();
    }

    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository TaskRepository { get; }
        IAccountRepository AccountRepository { get; }
        void Save();
    }
}
