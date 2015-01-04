using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Manager
{
    /// <summary>
    /// Singleton class that holds the in memory cache of the tasks and has hooks to listen for updates.
    /// </summary>
    public class TaskManager
    {
        Dictionary<string, Dictionary<uint, TaskItem>> tasks;

        public TaskManager()
        {
            tasks = new Dictionary<string, Dictionary<uint, TaskItem>>();
        }

        public void AddTask(TaskItem task, string team)
        {
            Dictionary<uint, TaskItem> teamtasks;
            if (!tasks.TryGetValue(team, out teamtasks))
            {
                teamtasks = new Dictionary<uint, TaskItem>();
                tasks[team] = teamtasks;
            }

            teamtasks[task.Id] = task;

            //Where do we propagate to db?
        }

        public void UpdateTask(TaskItem task, string team)
        {
            //???
        }

        public void DeleteTask(uint id, string team)
        {
            Dictionary<uint, TaskItem> teamtasks;
            if (!tasks.TryGetValue(team, out teamtasks))
            {
                throw new ArgumentException("Cannot delete task. Team " + team + " does not exist");
            }
            teamtasks.Remove(id);
        }
    }
}
