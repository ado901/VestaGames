using Sito.Models;
using Sito.ServiceReference2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sito.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admi
        private static ServiceReference1.Utente admin = new ServiceReference1.Utente();
         public static ServiceReference2.IadminClient wcf = new ServiceReference2.IadminClient();
        public ActionResult Listautenti()
        {
            ServiceReference2.Utente ut = new ServiceReference2.Utente();
            ut.email = admin.email;
            var listautenti = wcf.listaUtenti(ut);
            var model = new List<Utente>();
            foreach (var item in listautenti.Item2)
            {
                model.Add(item);
            }
            return View("utenti",model);
        }

        

        public ActionResult Index()
        {
            admin = (ServiceReference1.Utente)Session["utenteAttivo"];
            return View("home");
        }
    }
}