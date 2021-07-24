using Sito.ServiceReference2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sito.Models.Admin
{
    public class TransazioniAdmin
    {
        public TransazioniAdmin()
        {
            this.transazione = new Transazione();
        }
        [Required]
        public long codice_transazione { get; set; }
        [Required]
        public long codice_prodotto { get; set; }
        [Required]
        public long codice_commesso { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail non valida")]
        public string email { get; set; }
        [Required]
        public double prezzo { get; set; }
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime data { get; set; }
        public Transazione transazione { get; set; }

        public void parse()
        {
            this.codice_transazione = this.transazione.codice_transazione;
            this.codice_prodotto = this.transazione.codice_prodotto;
            this.codice_commesso = this.transazione.codice_commesso;
            this.email = this.transazione.email;
            this.prezzo = this.transazione.prezzo;
            this.data = this.transazione.data;


        }

        public void update()
        {
            this.transazione.codice_transazione = this.codice_transazione;
            this.transazione.codice_prodotto = this.codice_prodotto;
            this.transazione.codice_commesso = this.codice_commesso;
            this.transazione.email = this.email;
            this.transazione.prezzo = this.prezzo;
            this.transazione.data = this.data;

            

        }
    }
}