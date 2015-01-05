using System;
using System.Collections.Generic;
using TaskList.BizModels;

namespace TaskList.Manager
{
    public interface ITaskManager
    {
        void AddTask(TaskItem task);
        void DeleteTask(uint id, string team);
        void UpdateTask(TaskItem task, string team);
        List<TaskItem> GetTeamTasks(string teamName);
    }
}
