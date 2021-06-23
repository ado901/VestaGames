using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;

namespace Sito.Models
{
    public class UtenteModificato
    {
        public UtenteModificato()
        {
            this.ut = new ServiceReference1.Utente();
        }
        public Sito.ServiceReference1.Utente ut { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Nascita { get; set; }

        public string Password { get; set; }

        public double? Portafoglio { get; set; }

        public string Indirizzo { get; set; }

        public void update(string field)
        {
            switch (field)
            {
                case "password":
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        // ComputeHash - returns byte array  
                        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(this.Password));

                        // Convert byte array to a string   
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            builder.Append(bytes[i].ToString("x2"));
                        }
                        this.ut.password = builder.ToString();
                    }
                    break;

                case "nome":
                    this.ut.nome = this.Nome;
                    break;

                case "cognome":
                    this.ut.cognome = this.Cognome;
                    break;

                case "nascita":
                    this.ut.nascita = this.Nascita;
                    break;
                case "indirizzo":
                    this.ut.indirizzo = this.Indirizzo;
                    break;
                case "portafoglio":
                    this.ut.portafoglio = this.Portafoglio;
                    break;
                
            }

           }
        public void parse()
        {
            this.Password = this.ut.password;
            this.Email = this.ut.email;
            this.Nome = this.ut.nome;
            this.Cognome = this.ut.cognome;
            this.Indirizzo = this.ut.indirizzo;
            this.Nascita = this.ut.nascita;
            this.Portafoglio = this.ut.portafoglio;
        }

    }
}