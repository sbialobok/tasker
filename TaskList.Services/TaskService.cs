using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskList.ServiceContracts;
using TaskList.BizModels;
using TaskList.RepositoryContracts;

namespace TaskList.Services
{
    public class TaskService : ITaskService
    {
        private IWorkFactory _workFactory;
        public TaskService(IWorkFactory workfactory)
        {
            _workFactory = workfactory;
        }

        public void AddTask(TaskItem task)
        {
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                work.TaskRepository.Add(task);
                work.Save();
            }
        }

        public void DeleteTask(int id, string team)
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(TaskItem task, string team)
        {
            throw new NotImplementedException();
        }

        public List<TaskItem> GetTeamTasks(string teamName)
        {
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                //work.TaskRepository.Add(task);
                work.Save();
            }
            throw new NotImplementedException();
        }
    }
}
