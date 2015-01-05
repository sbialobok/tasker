using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;

namespace TaskList.Manager
{
    /// <summary>
    /// Singleton class that holds the in memory cache of the tasks and has hooks to listen for updates.
    /// TODO: replace with actual db
    /// </summary>
    public class TaskManager : ITaskManager
    {
        Dictionary<string, Dictionary<uint, TaskItem>> _tasks;
        uint _runningTaskId;

        public TaskManager()
        {
            _tasks = new Dictionary<string, Dictionary<uint, TaskItem>>();
            _runningTaskId = 1;
        }

        public void AddTask(TaskItem task)
        {
            Dictionary<uint, TaskItem> teamtasks;
            if (!_tasks.TryGetValue(task.TeamName, out teamtasks))
            {
                teamtasks = new Dictionary<uint, TaskItem>();
                _tasks[task.TeamName] = teamtasks;
            }

            task.Id = _runningTaskId++;
            teamtasks[task.Id] = task;
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
                teamTasks = new Dictionary<uint, TaskItem>();
                _tasks[teamName] = teamTasks;
            }
            return teamTasks.Values.ToList();
        }
    }
}
