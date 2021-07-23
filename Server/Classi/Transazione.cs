using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Classi
{
    public class Transazione
    {
        public long codice_transazione { get; set; }
        public long codice_prodotto { get; set; }
        public long codice_commesso { get; set; }
        public bool prenotazione { get; set; }
        public string email { get; set; }
        public double prezzo { get; set; }
        public System.DateTime data { get; set; }

        public void parseTransazione(transazioni rigadb)
        {
            try
            {
                this.codice_transazione = rigadb.codice_transazione;
                this.codice_prodotto = rigadb.codice_prodotto;
                this.codice_commesso = rigadb.codice_commesso;
                this.email = rigadb.email;
                this.prezzo = rigadb.prezzo;
                DateTime.TryParseExact(rigadb.data.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
                this.data = dt;
            }
            catch (Exception ex)
            {
                throw new Exception("metodo parseCommesso: " + ex.Message);
            }
        }
    }
}
