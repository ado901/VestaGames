using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Sito.Models.Admin
{
    public class aUtenteModificato
    {
        public aUtenteModificato()
        {
            this.ut = new ServiceReference2.Utente();
        }
        public Sito.ServiceReference2.Utente ut { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail non valida")]
        public string Email { get; set; }

        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Nascita { get; set; } = default(DateTime);
        [Required]
        public string Password { get; set; }
        [Required]
        public double? Portafoglio { get; set; }
        [Required]
        public string Indirizzo { get; set; }
        public int id { get; set; }

        public void update()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                if (this.ut.password != this.Password || this.Password =="\u2022\u2022\u2022\u2022\u2022")
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
                        
                    }
            this.ut.nome = this.Nome;        
            this.ut.cognome = this.Cognome; 
            if (this.Nascita != default(DateTime))
            {
                this.ut.nascita = this.Nascita;
            }
            
            this.ut.indirizzo = this.Indirizzo;
            this.ut.portafoglio = this.Portafoglio;
            this.ut.email = this.Email;
            this.ut.id = this.id;

                    

            

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
            this.id = this.ut.id;
        }

    }
}