using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TaskList.Manager;
using TaskList.Models;
using TaskList.BizModels;
using TaskList.Mapping;

namespace TaskList.Controllers
{
    /// <summary>
    /// Handles all CRUD operation dealing with tasks.
    /// </summary>
    public class TaskController : Controller
    {
        ITaskManager _manager;
        IAccountManager _accountManager;
        public TaskController(ITaskManager manager, IAccountManager accountManager)
        {
            _manager = manager;
            _accountManager = accountManager;
        }
        
        public JsonResult GetTasks(string teamName)
        {
            var tasks = _manager.GetTeamTasks(teamName).Select(t => t.ToModel());
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        public void AddTask(Task task)
        {
            var team = _accountManager.GetTeam(task.TeamName);
            var owner = _accountManager.GetUser(task.TeamName, task.Owner);
            _manager.AddTask(task.ToBiz(team, owner));
        }
    }
}
