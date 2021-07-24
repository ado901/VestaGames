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
                Console.WriteLine("modificaUtente: Esito OK Stringa esito: " + OK);
                return (Esito.OK, ut, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "modificaUtenti: " + ex.Message);
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

        public (Esito, Commesso, string) modificaCommesso(Commesso comm)
        {
            try
            {
                commesso commessodb = db.commesso.Where(commesso => commesso.codice_commesso == comm.codice_commesso).First();
                commessodb.parseCommesso(comm);
                db.SaveChanges();
                Console.WriteLine("modificaCommesso: Esito OK Stringa esito: " + OK);
                return (Esito.OK, comm, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "modificaCommesso: " + ex.Message);
                return (Esito.KO, comm, ex.Message);
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

        public (Esito, Prodotto, string) modificaProdotto(Prodotto prd)
        {
            try
            {
                prodotto prodottodb = db.prodotto.Where(prodotto => prodotto.codice_prodotto == prd.codice_prodotto).First();
                prodottodb.parse(prd);
                db.SaveChanges();
                Console.WriteLine("modificaProdotto: Esito OK Stringa esito: " + OK);
                return (Esito.OK, prd, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "modificaProdotto: " + ex.Message);
                return (Esito.KO, prd, ex.Message);
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

        public (Esito, Transazione, string) modificaTransazione(Transazione trn)
        {
            try
            {
                transazioni transazionedb = db.transazioni.Where(transazione => transazione.codice_transazione == trn.codice_transazione).First();
                transazionedb.parse(trn);
                db.SaveChanges();
                Console.WriteLine("modificaTransazione: Esito OK Stringa esito: " + OK);
                return (Esito.OK, trn, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "modificaTransazione: " + ex.Message);
                return (Esito.KO, trn, ex.Message);
            }
        }
    }
}
