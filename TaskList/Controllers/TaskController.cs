using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TaskList.Models;
using TaskList.BizModels;
using TaskList.Mapping;
using TaskList.ServiceContracts;

namespace TaskList.Controllers
{
    /// <summary>
    /// Handles all CRUD operation dealing with tasks.
    /// </summary>
    public class TaskController : Controller
    {
        ITaskService _taskService;
        IAccountService _accountService;
        public TaskController(ITaskService tservice, IAccountService aservice)
        {
            _taskService = tservice;
            _accountService = aservice;
        }
        
        public JsonResult GetTasks(string teamName)
        {
            var tasks = _taskService.GetTeamTasks(teamName).Select(t => t.ToModel());
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        public void AddTask(Task task)
        {
            var team = _accountService.GetTeam(task.TeamName);
            var owner = _accountService.GetUser(task.TeamName, task.Owner);
            _taskService.AddTask(task.ToBiz(team, owner));
        }
    }
}
