using server.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;

namespace server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        const string OK = "OK";
        private static TecnichedisvilEntities db = new TecnichedisvilEntities();
        public enum Esito: int
        {
            OK = 1,
            KO = 0
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
            
            
                try
                {
                   
                    utenti utente = db.utenti.Where((x) => x.email == ut.email && x.password == ut.password).First();
                    //se l'utente è loggato popolo l'oggetto utente ut con i dati dal database
                    ut.loginEffettuato(utente);
                    Console.WriteLine("Utente " + ut.nome + " " + ut.cognome + ", benvenuto");

                    return (Esito.OK, ut, OK);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("login fallito: " + ex.Message);
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
                    return (Esito.OK, ut, OK);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("registrazione fallita: " + ex.Message);
                    return (Esito.KO, ut, ex.Message);
                    
                }
        }

        //se possibile, mettimi anche un metodo che restituisca solo la lista (luco104)
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

                return (Esito.OK, prodotti, OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Record dei prodotti fallito: " + ex.Message);
                return (Esito.KO, prodotti, ex.Message);
            }

        }
    }
}
