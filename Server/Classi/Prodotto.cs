using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Classi
{
    class Prodotto
    {
        public long codice_prodotto { get; set; }
        public string titolo { get; set; }
        public string genere { get; set; }
        public string producer { get; set; }
        public int quantità { get; set; }
        public System.DateTime data_uscita { get; set; }
        public double prezzo { get; set; }
    }
}
