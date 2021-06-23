﻿using Sito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sito.ServiceReference1;
using System.Linq.Expressions;

namespace Sito.Controllers
{
    
    public class HomeController : Controller
    {
        public static ServiceReference1.Service1Client wcf = new ServiceReference1.Service1Client();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            ViewBag.Message = "Your contact page.";

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
                if (result.Item1== Service1Esito.OK)
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
        public string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
        [HttpPost]
        public ActionResult Login(utenteLoggato utente)
        {
            if (ModelState.IsValid)
            {
                var result = wcf.Login(utente.ut);
                if (result.Item1 == Service1Esito.OK)
                {
                    Session["utenteAttivo"] = result.Item2;
                    return View("Index");
                }

                
            }
            return View();
        }
        [HttpPost]
        public ActionResult UtenteAttivo(utenteLoggato utente)
        {
            return View();
        }


        public ActionResult DatiUtente()
        {
            var model = new UtenteModificato();
            model.ut = (Utente)Session["utenteAttivo"];
            model.parse();

            return View("DatiUtente", model);
        }

        public ActionResult Edit1(string button)
        {
            Session["modifica"] = button;

            var model = new UtenteModificato();
            model.ut = (ServiceReference1.Utente)Session["utenteAttivo"];
            model.parse();

            return View("Edit",model);
        }
        [HttpPost]
        public ActionResult Edit( UtenteModificato utente)
        {
            utente.ut = (Utente)Session["utenteAttivo"];
            if ((string)Session["modifica"] == GetMemberName((Utente c) => c.email))
            {
                var result = wcf.modificaUtente(utente.ut, (string)Session["modifica"], utente.Email);
                if (result.Item1== Service1Esito.OK)
                {
                    Session["utenteAttivo"] = result.Item2;
                }
            }
            else
            {
                utente.update((string)Session["modifica"]);
                var result = wcf.modificaUtente(utente.ut, (string)Session["modifica"],null);
                if (result.Item1 == Service1Esito.OK)
                {
                    Session["utenteAttivo"] = result.Item2;
                }
            }
            Session["modifica"] = null;
            return View("Index");
        }

        public ActionResult Prodotti()
        {
            var model = new List<Prodotto>();
            foreach (var item in wcf.getProdotti().Item2)
            {
                model.Add(item);
            }

            return View(model);
        }


        public ActionResult Nome(UtenteModificato utente)
        {
            if (ModelState.IsValid)
            {
                wcf.modificaUtente(utente.ut, "nome", null);
            }
            return View("Index");
        }
    }
}