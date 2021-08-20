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
        //private static VestaGamesEntities db = new VestaGamesEntities();
        public enum Esito : int
        {
            OK = 1,
            KO = 0
        }
        public (Esito, List<Utente>, string) listaUtenti(Utente ut)
        {
            
            List<Utente> listaUser = new List<Utente>();
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //controllo che chi fa la richiesta abbia le credenziali di amministratore
                if (ut.email == "admin@admin.admin")
                {
                    var record = db.utenti.OrderBy(utente => utente.email);
                    //converto il tipo del record in una lista di Utente
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
                VestaGamesEntities db = new VestaGamesEntities();
                //prendo la prima riga contenente la email, progettualmente è sempre univoca
                utenti utentedb = db.utenti.Where(utente => utente.email == ut.email).First();
                //passo i dati dall'oggetto Utente alla riga e salvo i cambiamenti
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

        public (Esito, string) eliminaUtente(Utente ut)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //prendo la prima riga che coincide con la mail, la rimuovo e salvo
                utenti utentedb = db.utenti.Where(utente => utente.email == ut.email).First();
                db.utenti.Remove(utentedb);
                db.SaveChanges();
                Console.WriteLine("eliminaUtente: Esito OK Stringa esito: " + OK);
                return (Esito.OK, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "eliminaUtente: " + ex.Message);
                return (Esito.KO,ex.Message);
            }
        }

        public (Esito, Utente, string) aggiungiUtente(Utente ut)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //controllo che la mail non sia già presente per vincolo progettuale
                if (db.utenti.Where(utente => utente.email == ut.email).Any())
                {
                    throw new Exception("utente già presente");
                }
                //converto la data in long
                long datanascita = long.Parse(ut.nascita.ToString("yyyyMMdd"));
                //creo la nuova riga da inserire, la inserisco e salvo
                utenti nuovoutente = new utenti()
                {
                    email = ut.email,
                    password = ut.password,
                    nome = ut.nome,
                    cognome = ut.cognome,
                    nascita = datanascita,
                    indirizzo = ut.indirizzo,
                    portafoglio = ut.portafoglio
                };
                db.utenti.Add(nuovoutente);
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
                VestaGamesEntities db = new VestaGamesEntities();
                //la possibilità di accedere ai dati è vincolata all'amministratore quindi controllo che abbia le credenziali
                if (ut.email == "admin@admin.admin")
                {
                    var record = db.commesso.OrderBy(commesso => commesso.codice_commesso);
                    //converto il tipo del record in lista di Commesso
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
                VestaGamesEntities db = new VestaGamesEntities();
                //ricerco per chiave primaria
                commesso commessodb = db.commesso.Where(commesso => commesso.codice_commesso == comm.codice_commesso).First();
                //inserisco i nuovi valori nella riga e salvo
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

        public (Esito, string) eliminaCommesso(Commesso comm)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //ricerca per chiave primaria, rimuovo e salvo
                commesso commessodb = db.commesso.Where(commesso => commesso.codice_commesso == comm.codice_commesso).First();
                db.commesso.Remove(commessodb);
                db.SaveChanges();
                Console.WriteLine("eliminaCommesso: Esito OK Stringa esito: " + OK);
                return (Esito.OK, OK);
            }catch(System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                Console.WriteLine("Eccezione: " + "eliminaCommesso: "+ "Questo dato ha un riferimento ad una transazione esistente, cancellare prima quella " + ex.Message);
                return (Esito.KO, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().ToString());
                Console.WriteLine("Eccezione: " + "eliminaCommesso: " + ex.Message);
                return (Esito.KO, ex.Message);
            }
        }

        public (Esito,Commesso, string) aggiungiCommesso(Commesso comm)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //creo la nuova riga, inserisco nel db e salvo
                commesso nuovocommesso = new commesso()
                {
                    nome = comm.nome,
                    cognome = comm.cognome
                };
                db.commesso.Add(nuovocommesso);
                db.SaveChanges();
                Console.WriteLine("aggiungiCommesso: Esito OK Stringa esito: " + OK);
                return (Esito.OK,comm, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "aggiungiCommesso: " + ex.Message);
                return (Esito.KO,comm, ex.Message);
            }
        }

        public (Esito, List<Prodotto>, string) listaProdotti(Utente ut)
        {
            
            List<Prodotto> prodotti = new List<Prodotto>();
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //per accedere a queste info serve essere admin
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
                VestaGamesEntities db = new VestaGamesEntities();
                //cerco per chiave primaria e copio incollo i dati dell'oggetto dentro la riga
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

        public (Esito, string) eliminaProdotto(Prodotto prd)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //ricerco per chiave primaria, rimuovo la riga e salvo
                prodotto prodottodb = db.prodotto.Where(prodotto => prodotto.codice_prodotto == prd.codice_prodotto).First();
                db.prodotto.Remove(prodottodb);
                db.SaveChanges();
                Console.WriteLine("eliminaProdotto: Esito OK Stringa esito: " + OK);
                return (Esito.OK, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "eliminaProdotto: " + ex.Message);
                return (Esito.KO, ex.Message);
            }
        }

        public (Esito, Prodotto, string) aggiungiProdotto(Prodotto prd)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //conversione date time in long
                long data = long.Parse(prd.data_uscita.ToString("yyyyMMdd"));
                //prendo i dati del prodotto e li inserisco
                prodotto nuovoprodotto = new prodotto()
                {
                    titolo = prd.titolo,
                    genere = prd.genere,
                    producer=prd.producer,
                    data_uscita=data,
                    prezzo=prd.prezzo,
                    quantità=prd.quantità,
                    img= prd.img
                };
                //aggiungo la riga al db e salvo
                db.prodotto.Add(nuovoprodotto);
                db.SaveChanges();
                Console.WriteLine("aggiungiProdotto: Esito OK Stringa esito: " + OK);
                return (Esito.OK, prd, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "aggiungiProdotto: " + ex.Message);
                return (Esito.KO, prd, ex.Message);
            }
        }

        public (Esito, List<Transazione>, string) listaTransazioni(Utente ut)
        {
            
            List<Transazione> listaTransazioni = new List<Transazione>();
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                // dati accessibili solo dall'admin
                if (ut.email == "admin@admin.admin")
                {
                    var record = db.transazioni.OrderBy(transazioni => transazioni.codice_transazione);
                    // conversione record in lista di Transazione con copia-incolla dalla riga all'oggetto
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
                VestaGamesEntities db = new VestaGamesEntities();
                //ricerca per chiave primaria, copio i dati nuovi nella riga e salvo
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

        public (Esito, string) eliminaTransazione(Transazione trn)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                //ricerca per chiave primaria, rimuovo la riga e salvo
                transazioni transazionidb = db.transazioni.Where(transazione => transazione.codice_transazione == trn.codice_transazione).First();
                db.transazioni.Remove(transazionidb);
                db.SaveChanges();
                Console.WriteLine("eliminaTransazione: Esito OK Stringa esito: " + OK);
                return (Esito.OK, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "eliminaTransazione: " + ex.Message);
                return (Esito.KO, ex.Message);
            }
        }

        public (Esito, Transazione, string) aggiungiTransazione(Transazione trn)
        {
            
            try
            {
                VestaGamesEntities db = new VestaGamesEntities();
                transazioni nuovatransazione = new transazioni()
                {
                    codice_commesso = trn.codice_commesso,
                    codice_prodotto = trn.codice_prodotto,
                    email = trn.email,
                    data = trn.data,
                    prezzo = trn.prezzo,
                    
                };
                db.transazioni.Add(nuovatransazione);
                db.SaveChanges();
                Console.WriteLine("aggiungiTransazione: Esito OK Stringa esito: " + OK);
                return (Esito.OK, trn, OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eccezione: " + "aggiungitransazione: " + ex.Message);
                return (Esito.KO, trn, ex.Message);
            }
        }
    }
}
