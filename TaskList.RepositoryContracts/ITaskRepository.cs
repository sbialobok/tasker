using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskList.BizModels;

namespace TaskList.RepositoryContracts
{
    public interface ITaskRepository
    {
        void Add(TaskItem task);
        TaskItem GetTask(int id);
        List<TaskItem> GetTasksByUserID(int[] userids);
    }
}
