using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Manager;

namespace TaskList.Controllers
{
    /// <summary>
    /// Handles all CRUD operation dealing with tasks.
    /// </summary>
    public class TaskController : Controller
    {
        ITaskManager _manager;
        public TaskController(ITaskManager manager)
        {
            _manager = manager;
        }

        public JsonResult GetTasks(string teamName)
        {
            var tasks = _manager.GetTeamTasks(teamName);
            return Json(tasks);
        }

    }
}
