using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Classi
{
    class Transazione
    {
        public long codice_transazione { get; set; }
        public long codice_prodotto { get; set; }
        public long codice_commesso { get; set; }
        public bool prenotazione { get; set; }
        public string email { get; set; }
        public double prezzo { get; set; }
        public System.DateTime data { get; set; }
    }
}
