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
    public class TaskManager : ITaskManager
    {
        Dictionary<string, Dictionary<uint, TaskItem>> _tasks;

        public TaskManager()
        {
            _tasks = new Dictionary<string, Dictionary<uint, TaskItem>>();
        }

        public void AddTask(TaskItem task, string team)
        {
            Dictionary<uint, TaskItem> teamtasks;
            if (!_tasks.TryGetValue(team, out teamtasks))
            {
                teamtasks = new Dictionary<uint, TaskItem>();
                _tasks[team] = teamtasks;
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
            if (!_tasks.TryGetValue(team, out teamtasks))
            {
                throw new ArgumentException("Cannot delete task. Team " + team + " does not exist");
            }
            teamtasks.Remove(id);
        }

        public List<TaskItem> GetTeamTasks(string teamName)
        {
            Dictionary<uint, TaskItem> teamTasks;
            if (!_tasks.TryGetValue(teamName, out teamTasks))
            {
                throw new ArgumentException("Team " + teamName + " does not exist");
            }
            return teamTasks.Values.ToList();
        }
    }
}
