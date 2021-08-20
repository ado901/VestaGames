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
        //QUESTO CONTROLLER E' PRESSOCHE' COMPLETAMENTE MODULARE IN OGNI SUA PARTE, PER OGNI TABELLA ABBIAMO 7 FUNZIONI:
        // 1 LISTA, 2 PER LA MODIFICA, 1 PER ELIMINARE, 1 PER I DETTAGLI DI UNA SINGOLA RIGA, 2 PER AGGIUNGERE UNA NUOVA RIGA
        // GET: Admi
        private static ServiceReference1.Utente admin = new ServiceReference1.Utente();
         public static ServiceReference2.IadminClient wcf;

        
        public ActionResult Listautenti(string searchName)
        {
            ServiceReference2.Utente ut = new ServiceReference2.Utente();
            ut.email = admin.email;
            //passo le credenziali di admin al wcf
            var listautenti = wcf.listaUtenti(ut);
            //salvo in una session lla matrice degli utenti
            Session["listautenti"] = listautenti.Item2;
            var model = new List<Utente>();
            //converto la matrice in una lista
            foreach (var item in listautenti.Item2)
            {
                model.Add(item);
            }
            //se la searchbar è stata riempita filtro la lista in base a quello che ha cercato l'admin
            if (!String.IsNullOrEmpty(searchName))
            {
                searchName = searchName.ToLower();
                model = model.Where(c => c.nome.ToLower().Contains(searchName) || c.cognome.ToLower().Contains(searchName) || c.email.ToLower().Contains(searchName) || c.indirizzo.ToLower().Contains(searchName)).ToList();
            }
            return View("utenti",model);
        }
        //controller che fornisce il modello alla view
        public ActionResult EditUtente(string button)
        {
            Session["modifica"] = button;
            //prendo la session salvata nel controller Listautenti
            Utente[] listautenti = (Utente[])Session["listautenti"];
            
            var model = new aUtenteModificato();
            //ricerca tramite email, unica per vincolo progettuale
            model.ut = listautenti.Where(utente => utente.email == button).First();
            //passo i dati dell'utente al model di riferimento
            model.parse();
            //e salvo l'utente in una tmp che servirà nel submit
            Session["tmp"] = model.ut;
            return View("UtentiEdit", model);
        }

        //controller di submit dei dati inseriti
        [HttpPost]
        public ActionResult EditUtente1(aUtenteModificato model)
        {
            try
            {
                // se è valido procedo a chiamare il wcf
                if (ModelState.IsValid)
                {
                    model.ut = (Utente)Session["tmp"];
                    Session["tmp"] = null;
                    //passo i dati dal model all'utente
                    model.update();
                    //chiamo il wcf passando l'utente modificato
                    var result = wcf.modificaUtente(model.ut);
                    if (result.Item1 == adminEsito.KO)
                    {
                        throw new HttpException(result.Item3);
                    }
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

        public ActionResult EliminaUtente(string button)
        {
            try
            {
                Session["modifica"] = button;
                //la session salvata nella funzione Listautenti
                Utente[] listautenti = (Utente[])Session["listautenti"];

                var model = new aUtenteModificato();
                model.ut = listautenti.Where(utente => utente.email == button).First();
                var result = wcf.eliminaUtente(model.ut);
                if (result.Item1 == adminEsito.KO)
                {
                    throw new HttpException(result.Item2);
                }
                return RedirectToAction("Listautenti");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");
                
            }
            
        }

        
        //controller per fornire il model alla view
       public ActionResult AggiungiUtente()
        {
            
            

            var model = new aUtenteModificato();
           
            return View("Utentinew", model);
        }

        //controller per la submit dei dati inseriti
        [HttpPost]
        public ActionResult AggiungiUtente1(aUtenteModificato model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //fornisco i dati all'oggetto Utente dentro il model
                    model.update();
                    
                    var result = wcf.aggiungiUtente(model.ut);
                    if (result.Item1== adminEsito.KO)
                    {
                        throw new HttpException(result.Item3);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Utentinew");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("Utentinew");
            }



        }
        //questa actionresult viene chiamata dalla view delle transazioni
        public ActionResult dettagliUtente()
        {

            try
            {
                string email = (string)Session["email"];
                Session["email"] = null;
                ServiceReference2.Utente ut = new ServiceReference2.Utente();
                ut.email = admin.email;
                //chiamo ancora listautenti per consistenza
                var listautenti = wcf.listaUtenti(ut);
                if (listautenti.Item1 == adminEsito.KO)
                {
                    throw new Exception(listautenti.Item3);
                }
                Session["listautenti"] = listautenti.Item2;
                //passo i dati al model
                Utente utente = listautenti.Item2.Where(prodotto => prodotto.email == email).First();

                var model = new aUtenteModificato();
                model.ut = utente;
                model.parse();
                return View("dettagliUtente", model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Listatransazioni");
            }





        }
        // le prossime ActionResult relative ai commessi usano lo stesso pattern per la parte degli Utenti
        public ActionResult Listacommessi(string searchName)
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
                if (!String.IsNullOrEmpty(searchName))
                {
                    searchName = searchName.ToLower();
                    model = model.Where(c => c.nome.ToLower().Contains(searchName) || c.cognome.ToLower().Contains(searchName)).ToList();
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
                    if (result.Item1 == adminEsito.KO)
                    {
                        throw new HttpException(result.Item3);
                    }
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

        public ActionResult EliminaCommesso(string button)
        {
            try
            {
                Session["modifica"] = button;
                Commesso[] listacommessi = (Commesso[])Session["listacommessi"];

                var model = new CommessiAdmin();
                model.commesso = listacommessi.Where(commesso => commesso.codice_commesso.ToString() == button).First();
                var result = wcf.eliminaCommesso(model.commesso);
                if (result.Item1 == adminEsito.KO)
                {
                    throw new HttpException(result.Item2);
                }
                return RedirectToAction("Listacommessi");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");

            }

        }

        public ActionResult AggiungiCommesso()
        {
            
            

            var model = new CommessiAdmin();
           
            
            return View("Commessinew", model);
        }

        [HttpPost]
        public ActionResult AggiungiCommesso1(CommessiAdmin model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    
                    model.update();
                    var result = wcf.aggiungiCommesso(model.commesso);
                    if (result.Item1 == adminEsito.KO)
                    {
                        throw new HttpException(result.Item3);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Commessinew");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("Commessinew");
            }



        }
        //chiamato dalla view delle transazioni
        public ActionResult dettagliCommesso(long id)
        {

            try
            {
                ServiceReference2.Utente ut = new ServiceReference2.Utente();
                ut.email = admin.email;
                var listacommessi = wcf.listaCommessi(ut);
                if (listacommessi.Item1 == adminEsito.KO)
                {
                    throw new Exception(listacommessi.Item3);
                }
                Session["listacommessi"] = listacommessi.Item2;
                Commesso comm = listacommessi.Item2.Where(prodotto => prodotto.codice_commesso == id).First();

                var model = new CommessiAdmin();
                model.commesso = comm;
                model.parse();
                return View("dettagliCommesso", model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Listatransazioni");
            }





        }
        // stesso pattern degli utenti per queste actionresult
        public ActionResult Listaprodotti(string searchName)
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
                if (!String.IsNullOrEmpty(searchName))
                {
                    searchName = searchName.ToLower();
                    model = model.Where(c => c.titolo.ToLower().Contains(searchName) || c.genere.ToLower().Contains(searchName) || c.producer.ToLower().Contains(searchName)).ToList();
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
                return RedirectToAction("Listaprodotti");
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
                    if (result.Item1 == adminEsito.KO)
                    {
                        throw new HttpException(result.Item3);
                    }
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
        public ActionResult EliminaProdotto(string button)
        {
            try
            {
                Session["modifica"] = button;
                Prodotto[] listaprodotti = (Prodotto[])Session["listaprodotti"];

                var model = new ProdottiAdmin();
                model.prd = listaprodotti.Where(prodotto => prodotto.codice_prodotto.ToString() == button).First();
                var result = wcf.eliminaProdotto(model.prd);
                if (result.Item1 == adminEsito.KO)
                {
                    throw new HttpException(result.Item2);
                }
                return RedirectToAction("Listaprodotti");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");

            }

        }

        public ActionResult AggiungiProdotto()
        {
            
            

            var model = new ProdottiAdmin();
            
            
            return View("Prodottinew", model);
        }

        



        [HttpPost]
        public ActionResult AggiungiProdotto1(ProdottiAdmin model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                   
                    model.update();
                    var result = wcf.aggiungiProdotto(model.prd);
                    if (result.Item1 == adminEsito.KO)
                    {
                        throw new HttpException(result.Item3);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Prodottinew");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("Prodottinew");
            }



        }
        //chiamato dalle transazioni
        public ActionResult dettagliProdotto(long id)
        {

            try
            {
                ServiceReference2.Utente ut = new ServiceReference2.Utente();
                ut.email = admin.email;
                var listaprodotti = wcf.listaProdotti(ut);
                if (listaprodotti.Item1 == adminEsito.KO)
                {
                    throw new Exception(listaprodotti.Item3);
                }
                Session["listaprodotti"] = listaprodotti.Item2;
                Prodotto prd = listaprodotti.Item2.Where(prodotto => prodotto.codice_prodotto == id).First();

                var model = new ProdottiAdmin();
                model.prd = prd;
                model.parse();
                return View("dettagliProdotto", model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return RedirectToAction("Listatransazioni");
            }





        }
        //stesso pattern delle altre senza però la parte "dettagli..."
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

        public ActionResult EliminaTransazione(string button)
        {
            try
            {
                Session["modifica"] = button;
                Transazione[] listatransazioni = (Transazione[])Session["listatransazioni"];

                var model = new TransazioniAdmin();
                model.transazione = listatransazioni.Where(transazione => transazione.codice_transazione.ToString() == button).First();
                var result = wcf.eliminaTransazione(model.transazione);
                if (result.Item1 == adminEsito.KO)
                {
                    throw new HttpException(result.Item2);
                }
                return RedirectToAction("Listatransazioni");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("home");

            }

        }

        public ActionResult AggiungiTransazione()
        {
            

            var model = new TransazioniAdmin();
            return View("Transazioninew", model);
        }

        [HttpPost]
        public ActionResult AggiungiTransazione1(TransazioniAdmin model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    
                    model.update();
                    var result = wcf.aggiungiTransazione(model.transazione);
                    if (result.Item1 == adminEsito.KO)
                    {
                        throw new HttpException(result.Item3);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Transazioninew");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errore", ex.Message);
                return View("Transazioninew");
            }



        }
        public void aperturawcf()
        {
            wcf = new ServiceReference2.IadminClient();
        }
        //home dove ho 4 strade da percorrere, in questa action salvo l'utente admin come variabile globale statica
        public ActionResult Index()
        {
            aperturawcf();
            admin = (ServiceReference1.Utente)Session["utenteAttivo"];
            return View("home");
        }


    }
}