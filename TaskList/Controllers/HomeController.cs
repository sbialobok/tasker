using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TaskList.Manager;

namespace TaskList.Controllers
{
    /// <summary>
    /// Right now handles logging in and returns our base html that we mount the react ui to.  Thats about it. 
    /// </summary>
    public class HomeController : Controller
    {
        IAccountManager _manager;
        public HomeController(IAccountManager manager)
        {
            _manager = manager;
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public void Login(string team, string user)
        {
            _manager.LoginUser(team, user);
        }

    }
}
