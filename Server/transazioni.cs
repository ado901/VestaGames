//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace server
{
    using server.Classi;
    using System;
    using System.Collections.Generic;
    
    public partial class transazioni
    {
        public long codice_transazione { get; set; }
        public long codice_prodotto { get; set; }
        public long codice_commesso { get; set; }
        public string email { get; set; }
        public double prezzo { get; set; }
        public System.DateTime data { get; set; }
    
        public virtual commesso commesso { get; set; }
        public virtual prodotto prodotto { get; set; }

        public void parse(Transazione trn)
        {
            this.codice_transazione = trn.codice_transazione;
            this.codice_prodotto = trn.codice_prodotto;
            this.codice_commesso = trn.codice_commesso;
            this.email = trn.email;
            this.prezzo = trn.prezzo;
            this.data = trn.data;


        }
    }
}
