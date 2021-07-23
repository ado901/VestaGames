using server.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "admin" nel codice e nel file di configurazione contemporaneamente.
    public class admin : Iadmin
    {
        const string OK = "OK";
        private static VestaGamesEntities db = new VestaGamesEntities();
        public enum Esito : int
        {
            OK = 1,
            KO = 0
        }
        public void DoWork()
        {
        }
        public (Esito, List<Utente>, string) listaUtenti(Utente ut)
        {
            List<Utente> listaUser = new List<Utente>();
            try
            {
                
                if (ut.email == "admin@admin.admin")
                {
                    var record = db.utenti.OrderBy(utente => utente.email);

                    foreach (utenti utente in record)
                    {
                        Utente usr = new Utente();
                        usr.loginEffettuato(utente);
                        listaUser.Add(usr);
                    }
                    Console.WriteLine("ListaUtenti: Esito OK Stringa esito: " + OK);
                    return (Esito.OK, listaUser, OK);
                }
                else { throw new Exception("chiamata senza permessi"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "ListaUtenti: " + ex.Message);
                return (Esito.KO, listaUser, "ListaUtenti: "+ ex.Message);
            }
            
        }

        public (Esito, Utente, string) modificaUtente(Utente ut)
        {
            try
            {
                utenti utentedb = db.utenti.Where(utente => utente.id == ut.id).First();
                utentedb.parseUtente(ut);
                db.SaveChanges();
                return (Esito.OK, ut, OK);
            }
            catch (Exception ex)
            {
                return (Esito.KO, ut, ex.Message);
            }
        }

        public (Esito, List<Commesso>, string) listaCommessi(Utente ut)
        {
            List<Commesso> listaCommessi = new List<Commesso>();
            try
            {
               
                if (ut.email == "admin@admin.admin")
                {
                    var record = db.commesso.OrderBy(commesso => commesso.codice_commesso);

                    foreach (commesso commesso in record)
                    {
                        Commesso cmmss = new Commesso();
                        cmmss.parseCommesso(commesso);
                        listaCommessi.Add(cmmss);
                    }
                    Console.WriteLine("ListaCommessi: Esito OK Stringa esito: " + OK);
                    return (Esito.OK, listaCommessi, OK);
                }
                else { throw new Exception("chiamata senza permessi"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "ListaCommessi: " + ex.Message);
                return (Esito.KO, listaCommessi, "ListaCommessi: " + ex.Message);
            }

        }

        public (Esito, List<Prodotto>, string) listaProdotti(Utente ut)
        {
            List<Prodotto> prodotti = new List<Prodotto>();
            try
            {
                if (ut.email == "admin@admin.admin")
                {
                    var record = db.prodotto.OrderBy(product => product.titolo);

                    //copia-incolla da entity framework a oggetto prodotto con popolamento della lista 

                    foreach (prodotto product in record)
                    {
                        Prodotto prd = new Prodotto();
                        prd.parseProduct(product);
                        prodotti.Add(prd);
                    }
                    Console.WriteLine("ListaProdotti: Esito OK Stringa esito: " + OK);
                    return (Esito.OK, prodotti, OK);
                }
                else { throw new Exception("chiamata senza permessi"); }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("ListaProdotti: Esito KO Stringa esito: " + ex.Message);
                return (Esito.KO, prodotti, ex.Message);
            }

        }

        public (Esito, List<Transazione>, string) listaTransazioni(Utente ut)
        {
            List<Transazione> listaTransazioni = new List<Transazione>();
            try
            {
                
                if (ut.email == "admin@admin.admin")
                {
                    var record = db.transazioni.OrderBy(transazioni => transazioni.codice_transazione);

                    foreach (transazioni transazione in record)
                    {
                        Transazione trnsctn = new Transazione();
                        trnsctn.parseTransazione(transazione);
                        listaTransazioni.Add(trnsctn);
                    }
                    Console.WriteLine("ListaTransazioni: Esito OK Stringa esito: " + OK);
                    return (Esito.OK, listaTransazioni, OK);
                }
                else { throw new Exception("chiamata senza permessi"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "listaTransazioni: " + ex.Message);
                return (Esito.KO, listaTransazioni, "listaTransazioni: " + ex.Message);
            }

        }
    }
}
