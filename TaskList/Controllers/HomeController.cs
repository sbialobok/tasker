using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TaskList.ServiceContracts;

namespace TaskList.Controllers
{
    /// <summary>
    /// Right now handles logging in and returns our base html that we mount the react ui to.  Thats about it. 
    /// </summary>
    public class HomeController : Controller
    {
        IAccountService _accountService;
        public HomeController(IAccountService service)
        {
            _accountService = service;
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public void Login(string team, string user)
        {
            _accountService.LoginUser(team, user);
        }

    }
}
