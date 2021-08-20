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
        public static ServiceReference1.Service1Client wcf;

        //unico modo più semplice che mi viene in mente per gestire le eccezioni nell'apertura della connessione al wcf
        public void connessionewcf() 
        {
            wcf = new Service1Client();
        
        }
        //pagina dove mostro un prodotto casuale tra quelli nel catalogo
        [HandleError]
        public ActionResult Index()
        {
            try
            {
                
                connessionewcf();
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
            
            

            return View();
        }

        //rilascio la memoria usata per le Session
        public ActionResult Logout()
        {
            Session["utenteAttivo"] = null;
            Session.Abandon();

            return RedirectToAction("Index");
        }
        //questa action e view ritornano le info di contatto nostre
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //si rifa al model UtenteRegistrato
        public ActionResult Registrazione()
        {

            return View();
        }
        
        //una volta compilato i dati si controlla se il model è valido, se lo è si chiama il wcf per inserire il record
        //si salva in una session l'utente che si è loggato e si setta a 0 il portafoglio (l'utente potrà farlo successivamente nella modifica)
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
                        utente.ut.portafoglio = 0;
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
        //restituisce il nome del parametro dell'oggetto
        public string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

        // qui si dividono le strade tra admin e utente, se il login va a buon fine l'utente normale torna alla homepage, l'admin nel pannello apposito
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

        // pagina dove l'utente può vedere i propri dati e modificarli
        public ActionResult DatiUtente()
        {
            try {
                //passo al model i dati dell'utente loggato in sessione
                var model = new UtenteModificato();
                model.ut = (Utente)Session["utenteAttivo"];
                model.parse();

                return View("DatiUtente", model);

            } catch (Exception ex) {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Index");
            }
            
        }
        // in base al valore del bottone premuto la view Edit decide che form mostrare
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

        //questa è complicata
        [HttpPost]
        public ActionResult Edit( UtenteModificato utente)
        {
            utente.ut = (Utente)Session["utenteAttivo"];
            try
            {
                if (ModelState.IsValid) {
                    //se il campo da cambiare è la email viene chiamato il wcf col campo facoltativo nuovaemail e il secondo parametro "email"
                    if ((string)Session["modifica"] == GetMemberName((Utente c) => c.email))
                    {
                        var result = wcf.modificaUtente(utente.ut, (string)Session["modifica"], utente.Email);
                        if (result.Item1 == Service1Esito.OK)
                        {
                            Session["utenteAttivo"] = result.Item2;
                        }
                        else throw new Exception(result.Item3);
                    }
                    else
                    {
                        //altrimenti chiamo il wcf con il campo facoltativo null e il secondo parametro come indicazione di quale campo modificare
                        utente.update((string)Session["modifica"]);
                        var result = wcf.modificaUtente(utente.ut, (string)Session["modifica"], null);
                        if (result.Item1 == Service1Esito.OK)
                        {
                            Session["utenteAttivo"] = result.Item2;
                        }
                        else throw new Exception(result.Item3);
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
        //pagina dove visualizzo la lista dei prodotti
        public ActionResult Prodotti(string searchName)
        {
            try
            {
                var model = new List<Prodotto>();
                var result = wcf.getProdotti();
                if (result.Item1 == Service1Esito.OK)
                {
                    foreach (var item in result.Item2)
                    {
                        model.Add(item);
                    }
                    if (!String.IsNullOrEmpty(searchName))
                    {
                        //se la barra di ricerca è stata riempita filtro la lista ottenuta
                        searchName = searchName.ToLower();
                        model = model.Where(c => c.titolo.ToLower().Contains(searchName) || c.genere.ToLower().Contains(searchName) || c.producer.ToLower().Contains(searchName)).ToList();
                    }

                    Session["listaprodotti"] = result.Item2;
                    return View(model);
                }
                else throw new Exception(result.Item3);
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Index");
            }
            
        }
        // pagina dove mostro in primo piano il prodotto che l'utente ha scelto
        public ActionResult compra(long id)
        {
            try
            {
                Prodotto[] listaprodotti = (Prodotto[])Session["listaprodotti"];
                Prodotto prodotto = listaprodotti.Where(p => p.codice_prodotto == id).First();
                var model = new ProdottoModel();
                //associo il prodotto trovato nella matrice al model e gli passo i dati con il parse()
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
        //l'utente ha confermato l'acquisto
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
                //controllo che si possa comprare
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