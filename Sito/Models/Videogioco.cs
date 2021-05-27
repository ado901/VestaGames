using Sito.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sito.Models
{
    public class Videogioco
    {
        public Prodotto prod { get; }
        public Videogioco()
        {
            this.prod = new Prodotto();
        }

        public string Titolo => this.prod.titolo;
        public string Produttore => this.prod.producer;
        public string Genere => this.prod.genere;
        public double Prezzo => this.prod.prezzo;




    }
}