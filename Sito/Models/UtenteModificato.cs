using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sito.Models
{
    public class UtenteModificato
    {
        public UtenteModificato()
        {
            this.ut = new ServiceReference1.Utente();
        }
        public Sito.ServiceReference1.Utente ut { get; set; }
        public string Nome
        {
            get
            {
                return this.ut.nome;
            }
        }
        public string Cognome
        {
            get
            {
                return this.ut.cognome;
            }

        }
        public string Email
        {
            get
            {
                return this.ut.email;
            }

        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Nascita
        {
            get
            {
                return this.ut.nascita;
            }

        }

        public string Password
        {
            get
            {
                return this.ut.password;
            }
        }

        public double? Portafoglio
        {
            get
            {
                return this.ut.portafoglio;
            }
        }

        public string Indirizzo
        {
            get
            {
                return this.ut.indirizzo;
            }
        }

        public void update()
        {

            
            this.ut.password = this.Password;
            this.ut.nome = this.Nome;
            this.ut.cognome = this.Cognome;
            this.ut.nascita = this.Nascita;
            this.ut.indirizzo = this.Indirizzo;
            this.ut.portafoglio = this.Portafoglio;

            this.ut.portafoglio = this.Portafoglio;

        }
    }
}