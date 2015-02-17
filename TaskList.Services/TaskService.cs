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

        /// <summary>
        /// Adds a task 
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(TaskItem task)
        {
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                var user = work.AccountRepository.GetUser(task.TeamName, task.Owner.Name);
                task.Owner = user;
                work.TaskRepository.Add(task);
                work.Save();
            }
        }

        public void DeleteTask(int id, string team)
        {
            throw new NotImplementedException();
        }

        public TaskItem GetTask(int id)
        {
            TaskItem retval;
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                retval = work.TaskRepository.GetTask(id);
            }

            return retval;
        }

        /// <summary>
        /// Update the task
        /// </summary>
        /// <param name="task"></param>
        public void UpdateTask(TaskItem task)
        {
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                work.TaskRepository.Update(task);
                work.Save();
            }
        }

        /// <summary>
        /// Gets list of tasks based on a team name
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public List<TaskItem> GetTeamTasks(string teamName)
        {
            List<TaskItem> result = null;
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                var users = work.AccountRepository.GetUsersByTeam(teamName);
                result = work.TaskRepository.GetTasksByUserID(users.Select(u => u.Id).ToArray());
            }

            return result;
        }
    }
}
