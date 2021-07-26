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

        

       public ActionResult AggiungiUtente()
        {
            
            

            var model = new aUtenteModificato();
           
            return View("Utentinew", model);
        }

        [HttpPost]
        public ActionResult AggiungiUtente1(aUtenteModificato model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    
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

        public ActionResult dettagliUtente()
        {

            try
            {
                string email = (string)Session["email"];
                Session["email"] = null;
                ServiceReference2.Utente ut = new ServiceReference2.Utente();
                ut.email = admin.email;
                var listautenti = wcf.listaUtenti(ut);
                if (listautenti.Item1 == adminEsito.KO)
                {
                    throw new Exception(listautenti.Item3);
                }
                Session["listautenti"] = listautenti.Item2;
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


        public ActionResult Index()
        {
            admin = (ServiceReference1.Utente)Session["utenteAttivo"];
            return View("home");
        }


    }
}