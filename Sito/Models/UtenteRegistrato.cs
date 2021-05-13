using Sito.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Sito.Models
{
    public class utenteRegistrato
    {
        public utenteRegistrato()
        {
            this.ut = new Utente();
        }
        public Utente ut { get; private set; }
        [Required]
        public string email
        {
            get
            {
                return this.ut.email;
            }


            set
            {
                this.ut.email = value;

            }
        }
        [Required]
        public string password
        {
            get
            {
                return this.ut.password;
            }


            set
            {
                byte[] data = Encoding.ASCII.GetBytes(value);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                this.ut.password = Encoding.ASCII.GetString(data);
                

            }
        }
        [Required]
        public string nome
        {
            get
            {
                return this.ut.nome;
            }


            set
            {
                this.ut.nome = value;

            }
        }
        [Required]
        public string cognome
        {
            get
            {
                return this.ut.cognome;
            }


            set
            {
                this.ut.cognome = value;

            }
        }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime nascita
        {
            get
            {
                return this.ut.nascita;
            }


            set
            {
                this.ut.nascita = value;

            }
        }
        
        public string indirizzo
        {
            get
            {
                return this.ut.indirizzo;
            }


            set
            {
                this.ut.indirizzo = value;

            }
        } 

    }
}