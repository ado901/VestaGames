using server.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        const string OK = "OK";
        private static VestaGamesEntities db = new VestaGamesEntities();
        public enum Esito: int
        {
            OK = 1,
            KO = 0
        }
        public string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static void popolateTable(string titolo, string genere, string producer, long data_uscita, double prezzo, int quantità, string filepath)
        {
            Console.WriteLine("inserimento Record in corso...");
            try
            {
                prodotto videogioco = new prodotto()
                {
                    titolo = titolo,
                    genere = genere,
                    producer = producer,
                    data_uscita = data_uscita,
                    prezzo = prezzo,
                    quantità = quantità,
                    img = filepath

                };
                db.prodotto.Add(videogioco);
                db.SaveChanges();
                Console.WriteLine("Inserimento completato");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
            }
            
        }
        public void DoWork()
        {
            Console.WriteLine("CLIENT CONNESSO AL SERVER ");
        }

        public void Dowork1(string arg)
        {
            Console.WriteLine(arg);

        }
        //login con entity framework
        public (Esito,Utente, string) Login(Utente ut)

        {
            Console.WriteLine(ut.email + " "+  ut.password);
            
                try
                {
                   
                    utenti utente = db.utenti.Where((x) => x.email == ut.email && x.password == ut.password).First();
                    //se l'utente è loggato popolo l'oggetto utente ut con i dati dal database
                    ut.loginEffettuato(utente);
                    Console.WriteLine("Utente " + ut.nome + " " + ut.cognome + ", benvenuto");
                Console.WriteLine("Login utente: Esito OK Stringa esito: " + OK);
                return (Esito.OK, ut, OK);
                }
                catch (Exception ex)
                {
                Console.WriteLine("Login utente: Esito KO Stringa esito: " + ex.Message);
                return (Esito.KO, ut, ex.Message);
                }


        }
        //registrazione con entity framework, lavoro con la classe Utente
        public (Esito, Utente,string) Registrazione(Utente ut)
        {
                try
                {
                    long datanascita = long.Parse(ut.nascita.ToString("yyyyMMdd"));
                    utenti utente = new utenti()
                    {
                        email = ut.email,
                        password = ut.password,
                        nome = ut.nome,
                        cognome = ut.cognome,
                        nascita = datanascita,
                        indirizzo = ut.indirizzo,
                        portafoglio = 0
                    };

                    //controllo se la mail è già presente nel sistema

                    if (db.utenti.Where((x) => x.email == ut.email).Any())
                    {
                        throw new Exception("Email già presente nel sistema");
                    }
                    
                    db.utenti.Add(utente);
                    db.SaveChanges();

                    //se l'utente è registrato e loggato popolo l'oggetto utente ut con i dati dal database
                    ut.loginEffettuato(utente);
                    Console.WriteLine("Utente " + ut.nome + " " + ut.cognome + ", benvenuto");
                    Console.WriteLine("Registrazione utente: Esito OK Stringa esito: " + OK);
                return (Esito.OK, ut, OK);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Registrazione utente: Esito KO Stringa esito: " + ex.Message);
                    return (Esito.KO, ut, ex.Message);
                    
                }
        }

        public (Esito, List<Prodotto>, string) getProdotti()
        {
            List<Prodotto> prodotti = new List<Prodotto>();
            try
            {
                
                var record = db.prodotto.OrderBy(product => product.titolo);

                //copia-incolla da entity framework a oggetto prodotto con popolamento della lista 

                foreach (prodotto product in record)
                {
                    Prodotto prd = new Prodotto();
                    prd.parseProduct(product);
                    prodotti.Add(prd);
                }
                Console.WriteLine("get Prodotti: Esito OK Stringa esito: " + OK);
                return (Esito.OK, prodotti, OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine("get Prodotti: Esito KO Stringa esito: " + ex.Message);
                return (Esito.KO, prodotti, ex.Message);
            }

        }

        //questa funzione usa la reflection, prende il nome della proprietà della classe "Utente" e "utenti(Entityframework)" e cambia il valore di quella proprietà
        public (Esito, Utente, string) modificaUtente(Utente ut, string field, string emailnuova=null)
        {
            try
            {
                //cerchiamo l'utente nel db con la chiave primaria email
                utenti utentedb = db.utenti.Where(riga => riga.email == ut.email).First();

                //nel caso sia da cambiare la email controlliamo che sia valida la nuova e non ci sia già un utente con la stessa mail
                //LUCO NON METTERE LA EMAIL NUOVA NELL'OGGETTO UTENTE MA PASSALO COME PARAMETRO FACOLTATIVO emailnuova. IL RESTO METTILO SUBITO NELL'OGGETTO
                if(field == GetMemberName((Utente c) => c.email))
                {
                    if (!IsValidEmail(emailnuova) || db.utenti.Where(riga => riga.email == emailnuova).Any())
                    {
                        throw new Exception("campo email non valido o email già presente");
                    }
                    ut.email = emailnuova;
                }
                //reflection: modifichiamo il valore della proprietà chiamata field (es: email, password, nome, cognome etc...)
                PropertyInfo prop = utentedb.GetType().GetProperty(field, BindingFlags.Public | BindingFlags.Instance);
                //controllo che il campo esista e sia settabile
                if (null != prop && prop.CanWrite)
                {
                    if (field == "nascita")
                    {
                        utentedb.nascita= long.Parse(ut.nascita.ToString("yyyyMMdd"));
                    }
                    else { prop.SetValue(utentedb, ut.GetType().GetProperty(field).GetValue(ut, null), null); }
                    
                }
                else
                {
                    throw new Exception("Errore nel settare il campo nel db");
                }
                db.SaveChanges();
                Console.WriteLine("modifica Utente: Esito OK Stringa esito: " + OK);
                return (Esito.OK, ut, OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine("modifica Utente: Esito KO Stringa esito: " + ex.Message);
                return (Esito.KO, ut, ex.Message);
            }
            
        }

        public (Esito, Utente,Prodotto, string) compraProdotto(Prodotto prod, Utente ut)
        {
            try
            {
                if (ut.portafoglio< prod.prezzo)
                {
                    throw new Exception("prezzo prodotto maggiore della disponibilità portafoglio");
                }
                //transazione sottraggo di 1 la quantità del prodotto nel db e sottraggo dal portafoglio il prezzo del prodotto
                using(var transaction= db.Database.BeginTransaction())
                {
                    try
                    {
                        utenti utentedb = db.utenti.Where(riga => riga.email == ut.email).First();
                        prodotto prodottodb= db.prodotto.Where(riga => riga.codice_prodotto == prod.codice_prodotto).First();
                        if (utentedb.portafoglio!= ut.portafoglio || prodottodb.prezzo!= prod.prezzo)
                        {
                            throw new Exception("prezzo o portafoglio non combaciano con quelli in db");
                        }
                        utentedb.portafoglio -= prodottodb.prezzo;
                        ut.portafoglio -= prodottodb.prezzo;
                        db.SaveChanges();
                        prodottodb.quantità -= 1;
                        prod.quantità -= 1;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Transazione non andata a buon fine: " + ex.Message);
                    }
                }

                Console.WriteLine("Compra Prodotto: Esito OK Stringa esito: " + OK);
                return (Esito.OK, ut, prod, OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Compra Prodotto: Esito KO Stringa esito: " + ex.Message);
                return (Esito.KO, ut, prod, ex.Message);
            }
            
            
        }

    }
}
