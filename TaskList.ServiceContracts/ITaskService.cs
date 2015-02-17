using System;
using System.Collections.Generic;
using TaskList.BizModels;

namespace TaskList.ServiceContracts
{
    public interface ITaskService
    {
        void AddTask(TaskItem task);
        void DeleteTask(int id, string team);
        void UpdateTask(TaskItem task);
        TaskItem GetTask(int id);
        List<TaskItem> GetTeamTasks(string teamName);
    }
}
