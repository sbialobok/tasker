using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;
using TaskList.ServiceContracts;

namespace TaskList.MemServices
{
    /// <summary>
    /// Singleton class that holds the in memory cache of the tasks and has hooks to listen for updates.
    /// TODO: replace with actual db
    /// </summary>
    public class TaskService : ITaskService
    {
        Dictionary<string, Dictionary<int, TaskItem>> _tasks;
        int _runningTaskId;

        public TaskService()
        {
            _tasks = new Dictionary<string, Dictionary<int, TaskItem>>();
            _runningTaskId = 1;
        }

        public void AddTask(TaskItem task)
        {
            Dictionary<int, TaskItem> teamtasks;
            if (!_tasks.TryGetValue(task.TeamName, out teamtasks))
            {
                teamtasks = new Dictionary<int, TaskItem>();
                _tasks[task.TeamName] = teamtasks;
            }

            task.Id = _runningTaskId++;
            teamtasks[task.Id] = task;
        }

        public void UpdateTask(TaskItem task, string team)
        {
            //???
        }

        public void DeleteTask(int id, string team)
        {
            Dictionary<int, TaskItem> teamtasks;
            if (!_tasks.TryGetValue(team, out teamtasks))
            {
                throw new ArgumentException("Cannot delete task. Team " + team + " does not exist");
            }
            teamtasks.Remove(id);
        }

        public List<TaskItem> GetTeamTasks(string teamName)
        {
            Dictionary<int, TaskItem> teamTasks;
            if (!_tasks.TryGetValue(teamName, out teamTasks))
            {
                teamTasks = new Dictionary<int, TaskItem>();
                _tasks[teamName] = teamTasks;
            }
            return teamTasks.Values.ToList();
        }
    }
}
