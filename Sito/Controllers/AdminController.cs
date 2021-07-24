using Sito.Models;
using Sito.Models.Admin;
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
            Session["listautenti"] = listautenti.Item2;
            var model = new List<Utente>();
            foreach (var item in listautenti.Item2)
            {
                model.Add(item);
            }
            return View("utenti",model);
        }
        public ActionResult EditUtente(string button)
        {
            Session["modifica"] = button;
            Utente[] listautenti = (Utente[])Session["listautenti"];
            
            var model = new aUtenteModificato();
            model.ut = listautenti.Where(utente => utente.email == button).First();
            model.parse();
            Session["tmp"] = model.ut;
            return View("UtentiEdit", model);
        }

        [HttpPost]
        public ActionResult EditUtente1(aUtenteModificato model)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    model.ut = (Utente)Session["tmp"];
                    Session["tmp"] = null;
                    model.update();
                    var result = wcf.modificaUtente(model.ut);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("UtentiEdit");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("utenti");
            }
            
            

        }

        public ActionResult Listacommessi()
        {
            var model = new List<Commesso>();
            try
            {
                ServiceReference2.Utente ut = new ServiceReference2.Utente();
                ut.email = admin.email;
                var listacommessi = wcf.listaCommessi(ut);
                Session["listacommessi"] = listacommessi.Item2;
                
                foreach (var item in listacommessi.Item2)
                {
                    model.Add(item);
                }

                return View("commessi", model);
            }
            catch(Exception ex)
            {
                
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");
            }
            
            
        }
        public ActionResult EditCommesso(string button)
        {
            try
            {
                Session["modifica"] = button;
                Commesso[] listacommessi = (Commesso[])Session["listacommessi"];

                var model = new CommessiAdmin();
                model.commesso = listacommessi.Where(commesso => commesso.codice_commesso.ToString() == button).First();
                model.parse();
                Session["tmp"] = model.commesso;
                return View("commessiEdit", model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");
            }
            
        }

        [HttpPost]
        public ActionResult EditCommesso1(CommessiAdmin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.commesso = (Commesso)Session["tmp"];
                    Session["tmp"] = null;
                    model.update();
                    var result = wcf.modificaCommesso(model.commesso);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("commessiEdit");
                }


            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("commessi");
            }
            
            

        }

        public ActionResult Listaprodotti()
        {
            var model = new List<Prodotto>();
            try
            {
                ServiceReference2.Utente ut = new ServiceReference2.Utente();
                ut.email = admin.email;
                var listaprodotti = wcf.listaProdotti(ut);
                if (listaprodotti.Item1 == adminEsito.KO)
                {
                    throw new HttpException(listaprodotti.Item3);
                }
                Session["listaprodotti"] = listaprodotti.Item2;
                foreach (var item in listaprodotti.Item2)
                {
                    model.Add(item);
                }

                return View("prodotti", model);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Errore", ex.Message);
                return View("home");
            }


        }

        public ActionResult EditProdotto(string button)
        {
            try
            {
                Session["modifica"] = button;
                Prodotto[] listaprodotti = (Prodotto[])Session["listaprodotti"];

                var model = new ProdottiAdmin();
                model.prd = listaprodotti.Where(commesso => commesso.codice_prodotto.ToString() == button).First();
                model.parse();
                Session["tmp"] = model.prd;
                return View("prodottiEdit", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");
            }

        }

        [HttpPost]
        public ActionResult EditProdotto1(ProdottiAdmin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.prd = (Prodotto)Session["tmp"];
                    Session["tmp"] = null;
                    model.update();
                    var result = wcf.modificaProdotto(model.prd);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("prodottiEdit");
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("prodottiEdit");
            }



        }

        public ActionResult Listatransazioni()
        {
            var model = new List<Transazione>();
            try
            {
                ServiceReference2.Utente ut = new ServiceReference2.Utente();
                ut.email = admin.email;
                var listatransazioni = wcf.listaTransazioni(ut);
                if (listatransazioni.Item1 == adminEsito.KO)
                {
                    throw new HttpException(listatransazioni.Item3);
                }
                Session["listatransazioni"] = listatransazioni.Item2;

                foreach (var item in listatransazioni.Item2)
                {
                    model.Add(item);
                }

                return View("transazioni", model);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Errore", ex.Message);
                return View("home");
            }


        }

        public ActionResult EditTransazione(string button)
        {
            try
            {
                Session["modifica"] = button;
                Transazione[] listatransazioni = (Transazione[])Session["listatransazioni"];

                var model = new TransazioniAdmin();
                model.transazione = listatransazioni.Where(transazione => transazione.codice_transazione.ToString() == button).First();
                model.parse();
                Session["tmp"] = model.transazione;
                return View("transazioniEdit", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");
            }

        }

        [HttpPost]
        public ActionResult EditTransazione1(TransazioniAdmin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.transazione = (Transazione)Session["tmp"];
                    Session["tmp"] = null;
                    model.update();
                    var result = wcf.modificaTransazione(model.transazione);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("transazioniEdit");
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("transazioniEdit");
            }
        }


            public ActionResult Index()
        {
            admin = (ServiceReference1.Utente)Session["utenteAttivo"];
            return View("home");
        }


    }
}