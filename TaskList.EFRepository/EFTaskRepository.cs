using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskList.BizModels;
using TaskList.EFData;
using TaskList.RepositoryContracts;

namespace TaskList.EFRepository
{
    /// <summary>
    /// Class responsibilities handle mapping from the biz task -> ef object task and making context calls
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        /// <summary>
        /// Context to be used to handle db insteractions
        /// </summary>
        private TaskListDataContext _context;
        public TaskRepository(TaskListDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Calls add on the context after converting the biz item to the ef object.
        /// </summary>
        /// <param name="task">The task to save to the database</param>
        public void Add(TaskItem task)
        {
            _context.Tasks.Add(ToEFTask(task));
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="task"></param>
        public void Update(TaskItem task)
        {
            var updated = ToEFTask(task);
            _context.Tasks.Attach(updated);
            var entry = _context.Entry(updated);
            entry.Property(e => e.Text).IsModified = true;
            entry.Property(e => e.Date).IsModified = true;
        }

        /// <summary>
        /// Returns a task based on its ID
        /// </summary>
        /// <param name="id">ID of the task to retrieve</param>
        /// <returns>TaskItem that matches the ID passed in</returns>
        public TaskItem GetTask(int id)
        {
            //TODO: Do we want to return null if we can't find the item?
            return ToTaskItem(_context.Tasks.Find(id));
        }

        /// <summary>
        /// Returns a list of tasks based on an array of task id's
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<TaskItem> GetTasksByUserID(int[] userids)
        {
            var result = from t in _context.Tasks
                         where userids.Contains(t.UserID)
                         select t;

            return result.ToList().Select( t => ToTaskItem(t)).ToList();
        }
        
        /// <summary>
        /// Maps the business object to the ef object
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        protected virtual TaskList.EFData.Task ToEFTask(TaskItem task)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            var eftask = new TaskList.EFData.Task()
            {
                ID = task.Id,
                UserID = task.Owner.Id,
                Text = task.Description,
                Date = task.DueDate
            };
            return eftask;
        }

        /// <summary>
        /// Maps the ef object back to our business object
        /// </summary>
        /// <param name="task">The ef object</param>
        /// <returns>A new taskitem object created from the ef object</returns>
        protected virtual TaskItem ToTaskItem(EFData.Task task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            var owner = AccountRepository.ToBizUser(task.User);
            var taskItem = new TaskItem()
            {
                Id = task.ID,
                Owner = owner,
                TeamName = task.User.Team.Name,
                Description = task.Text,
                DueDate = task.Date
            };

            return taskItem;
        }
    }
}
