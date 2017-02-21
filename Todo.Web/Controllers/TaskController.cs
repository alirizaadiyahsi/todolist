using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Todo.Web.Controllers
{
    public class TaskController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}