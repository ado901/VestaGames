using Sito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sito.Controllers
{
    
    public class HomeController : Controller
    {
        public static ServiceReference1.Service1Client wcf = new ServiceReference1.Service1Client();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registrazione()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Registrazione(utenteRegistrato utente)
        {
            if (ModelState.IsValid)
            {
                var result= wcf.Registrazione(utente.ut);
                if (result.Item1)
                {
                    Session["utenteAttivo"] = utente.ut;
                    return View("Index");
                }
                
            }
            return View();
        }

        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(utenteLoggato utente)
        {
            if (ModelState.IsValid)
            {
                var result = wcf.Login(utente.ut);
                if (result.Item1)
                {
                    Session["utenteAttivo"] = utente.ut.email;
                    return View("Index");
                }

                
            }
            return View();
        }
    }
}