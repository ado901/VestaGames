using Sito.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sito.Models
{
    public class Prodotto
    {
        public Prodotto prod { get; }
        public Prodotto()
        {
            this.prod = new Prodotto();
        }

        public string Titolo
        {
            get
            {
                return this.prod.Titolo;
            }
        }
        public string Produttore
        {
            get
            {
                return this.prod.Produttore;
            }
        }

        public string Genere
        {
            get
            {
                return this.prod.Genere;
            }
        }

        public double Prezzo
        {
            get
            {
                return this.prod.Prezzo;
            }
        }



    }
}