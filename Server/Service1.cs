﻿using server.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        const string OK = "OK";
        private TecnichedisvilEntities db = new TecnichedisvilEntities();
        public enum Esito: int
        {
            OK = 1,
            KO = 0
        }
        public const string CONNECTIONSTRING = "Data Source=LAPTOP-VMPHGDB0\\TECNICHESVIL;Initial Catalog=tecnichedisvil;Integrated Security=True";
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

            
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection())
            //    {
            //        conn.ConnectionString = CONNECTIONSTRING;
            //        conn.Open();

            //        SqlCommand command = new SqlCommand("SELECT email, password FROM dbo.dati_anagrafici WHERE email='" + ut.email + "'and password='" + ut.password + "'", conn);
            //        var resultSet = command.ExecuteReader();
            //        if (resultSet.HasRows)
            //        {
            //            Console.WriteLine(ut.email);
            //            Console.WriteLine(ut.password);
            //            conn.Close();
            //            return true;
            //        }
            //        conn.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

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

            
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection())
            //    {
            //        conn.ConnectionString = CONNECTIONSTRING;
            //        conn.Open();
            //        long datanascita = long.Parse(ut.nascita.ToString("yyyyMMdd"));
            //        ut.portafoglio = 0;
            //        SqlCommand command = new SqlCommand("INSERT INTO dbo.dati_anagrafici (cognome, nome, nascita, password, email, indirizzo, portafoglio) values " +
            //            "('" + ut.cognome + "','" + ut.nome + "','" + datanascita + "','" + ut.password + "','" + ut.email + "', '" + ut.indirizzo + "','" + ut.portafoglio + "')", conn);
            //        var prova = command.ExecuteNonQuery();
            //        if (prova != 0)
            //        {
            //            Console.WriteLine("registrazione effettuata");
            //            conn.Close();
            //            return true;
            //        }

            //        else
            //        {
            //            Console.WriteLine("registrazione fallita");
            //            conn.Close();
            //            return false;
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
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
