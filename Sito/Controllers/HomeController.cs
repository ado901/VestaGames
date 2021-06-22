﻿using Sito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sito.ServiceReference1;

namespace Sito.Controllers
{
    
    public class HomeController : Controller
    {
        public static ServiceReference1.Service1Client wcf = new ServiceReference1.Service1Client();
        public ActionResult Index()
        {
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
                    Session["utenteAttivo"] = utente.ut.email;
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


        public ActionResult DatiUtente(utenteLoggato utente)
        {
            var model = new UtenteModificato();
            model.ut = (Utente)Session["utenteAttivo"];

            return View("DatiUtente", model);
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
    }
}