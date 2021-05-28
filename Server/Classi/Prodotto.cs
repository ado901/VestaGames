using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Classi
{
    public class Prodotto
    {
        public long codice_prodotto { get; set; }
        public string titolo { get; set; }
        public string genere { get; set; }
        public string producer { get; set; }
        public int quantità { get; set; }
        public System.DateTime data_uscita { get; set; }
        public double prezzo { get; set; }
        public string img { get; set; }

        public void parseProduct(prodotto rigadb)
        {
            try
            {
                this.titolo = rigadb.titolo;
                this.genere = rigadb.genere;
                this.producer = rigadb.producer;
                this.quantità = rigadb.quantità;
                this.codice_prodotto = rigadb.codice_prodotto;
                DateTime.TryParseExact(rigadb.data_uscita.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
                this.data_uscita = dt;
                this.prezzo = rigadb.prezzo;
                this.img = rigadb.img;
            }
            catch (Exception ex)
            {
                throw new Exception("metodo parseProduct: " + ex.Message);
            }
        }
    }
}
