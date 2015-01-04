using System;
using System.Collections.Generic;

namespace TaskList.Manager
{
    public interface ITaskManager
    {
        void AddTask(TaskItem task, string team);
        void DeleteTask(uint id, string team);
        void UpdateTask(TaskItem task, string team);
        List<TaskItem> GetTeamTasks(string teamName);
    }
}
