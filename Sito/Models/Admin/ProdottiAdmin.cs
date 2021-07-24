using Sito.ServiceReference2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sito.Models.Admin
{
    public class ProdottiAdmin
    {
        public ProdottiAdmin()
        {
            this.prd = new Prodotto();
        }
        public long codice_prodotto { get; set; }
        public string titolo { get; set; }
        public string genere { get; set; }
        public string producer { get; set; }
        public int quantità { get; set; }
        public System.DateTime data_uscita { get; set; }
        public double prezzo { get; set; }
        public string img { get; set; }
        public Prodotto prd { get; set; }

        public void parse()
        {
            this.codice_prodotto = this.prd.codice_prodotto;
            this.titolo = this.prd.titolo;
            this.genere = this.prd.genere;
            this.producer = this.prd.producer;
            this.quantità = this.prd.quantità;
            this.data_uscita = this.prd.data_uscita;
            this.prezzo = this.prd.prezzo;
            this.img = this.prd.img;
            

        }

        public void update()
        {
            this.prd.codice_prodotto = this.codice_prodotto;
            this.prd.genere = this.genere;
            this.prd.titolo = this.titolo;
            this.prd.producer = this.producer;
            this.prd.quantità = this.quantità;
            this.prd.data_uscita = this.data_uscita;

            this.prd.prezzo = this.prezzo;
            this.prd.img = this.img;

        }
    }
}