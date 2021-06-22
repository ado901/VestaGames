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
        public Sito.ServiceReference1.Utente ut { get; private set; }
        public string Nome
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
        public string Cognome
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
        public string Email
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Nascita
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
    }
}