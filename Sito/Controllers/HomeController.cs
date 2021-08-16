using Sito.Models;
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
        [HandleError]
        public ActionResult Index()
        {
            try
            {
                var ciao = ViewBag.Message;

                var model = new List<Prodotto>();
                foreach (var item in wcf.getProdotti().Item2)
                {
                    model.Add(item);
                }
                ViewBag.listaProd = model;
            }
            catch (Exception ex) {
                return View("Error");
            }
            //ERRORE - quando viene chiamato questo actionresult da altri actionresult non passa i dati della lista prodotti
            

            return View();
        }

        public ActionResult Logout()
        {
            Session["utenteAttivo"] = null;
            Session.Abandon();

            return RedirectToAction("Index");
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
            try
            {
                if (ModelState.IsValid)
                {
                    var result = wcf.Registrazione(utente.ut);
                    if (result.Item1 == Service1Esito.OK)
                    {
                        Session["utenteAttivo"] = utente.ut;
                        return RedirectToAction("Index");
                    }
                    else throw new Exception(result.Item3);

                }
                else
                {
                    throw new HttpException("Qualcosa è andato storto nella registrazione");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("LogOnError", ex.Message);
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
                try
                {
                    var result = wcf.Login(utente.ut);
                    if (result.Item1 == Service1Esito.OK)
                    {

                        Session["utenteAttivo"] = result.Item2;

                        if (result.Item2.email == "admin@admin.admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new HttpException("password or email incorrect");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LogOnError", ex.Message);
                }
                

                
            }
            return View();
        }


        public ActionResult DatiUtente()
        {
            try {

                var model = new UtenteModificato();
                model.ut = (Utente)Session["utenteAttivo"];
                model.parse();

                return View("DatiUtente", model);

            } catch (Exception ex) {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Edit1(string button)
        {
            try {
                Session["modifica"] = button;

                var model = new UtenteModificato();
                model.ut = (ServiceReference1.Utente)Session["utenteAttivo"];
                model.parse();

                return View("Edit", model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("DatiUtente");
            }
            
        }
        [HttpPost]
        public ActionResult Edit( UtenteModificato utente)
        {
            utente.ut = (Utente)Session["utenteAttivo"];
            try
            {
                if (ModelState.IsValid) {

                    if ((string)Session["modifica"] == GetMemberName((Utente c) => c.email))
                    {
                        var result = wcf.modificaUtente(utente.ut, (string)Session["modifica"], utente.Email);
                        if (result.Item1 == Service1Esito.OK)
                        {
                            Session["utenteAttivo"] = result.Item2;
                        }
                    }
                    else
                    {
                        utente.update((string)Session["modifica"]);
                        var result = wcf.modificaUtente(utente.ut, (string)Session["modifica"], null);
                        if (result.Item1 == Service1Esito.OK)
                        {
                            Session["utenteAttivo"] = result.Item2;
                        }
                    }
                    Session["modifica"] = null;
                    return RedirectToAction("DatiUtente");


                } else { return View("Edit", utente); }
                
            }
            catch(Exception ex)
            {
                var model = new UtenteModificato();
                model.ut = utente.ut;
                model.parse();
                ModelState.AddModelError("Errore", ex.Message);
                return View("Edit", model);
            }
           
        }

        public ActionResult Prodotti(string searchName)
        {
            try
            {
                var model = new List<Prodotto>();
                var result = wcf.getProdotti();
                foreach (var item in result.Item2)
                {
                    model.Add(item);
                }
                if (!String.IsNullOrEmpty(searchName))
                {
                    searchName = searchName.ToLower();
                    model = model.Where(c => c.titolo.ToLower().Contains(searchName)|| c.genere.ToLower().Contains(searchName) || c.producer.ToLower().Contains(searchName) ).ToList();
                }

                Session["listaprodotti"] = result.Item2;
                return View(model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult compra(long id)
        {
            try
            {
                Prodotto[] listaprodotti = (Prodotto[])Session["listaprodotti"];
                Prodotto prodotto = listaprodotti.Where(p => p.codice_prodotto == id).First();
                var model = new ProdottoModel();
                model.prd = prodotto;
                model.parse();
                return View("prodottodettagli", model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Prodotti");
            }
            

        }
        [HttpPost]
        public ActionResult compra1(long id, ProdottoModel model)
        {
            try
            {
                Prodotto[] listaprodotti = (Prodotto[])Session["listaprodotti"];
                Prodotto prodotto = listaprodotti.Where(p => p.codice_prodotto == id).First();
                model.prd = prodotto;
                model.parse();
                if (Session["utenteAttivo"]== null)
                {
                    throw new Exception("devi effettuare il login prima di acquistare");
                }
                Utente ut = (Utente)Session["utenteAttivo"];
                /*var model = new ProdottoModel();
                model.prd = prodotto;
                model.parse();*/
                if (ut.portafoglio < prodotto.prezzo)
                {
                    throw new Exception("Denaro non sufficiente");
                }
                var result = wcf.compraProdotto(prodotto, ut);
                if (result.Item1 == Service1Esito.KO)
                {
                    throw new Exception(result.Item4);
                }
                Session["acquistocompletato"] = "Acquisto completato";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("prodottodettagli",model);
            }


        }

    }
}