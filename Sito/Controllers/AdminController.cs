using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sito.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult listautenti()
        {
            return View("home");
        }

        public ActionResult Index()
        {
            return View("home");
        }
    }
}