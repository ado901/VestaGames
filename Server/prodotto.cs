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
    
    public partial class prodotto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prodotto()
        {
            this.transazioni = new HashSet<transazioni>();
        }
    
        public long codice_prodotto { get; set; }
        public string titolo { get; set; }
        public string genere { get; set; }
        public string producer { get; set; }
        public int quantità { get; set; }
        public long data_uscita { get; set; }
        public double prezzo { get; set; }
        public string img { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transazioni> transazioni { get; set; }

        public void parse(Prodotto prd)
        {
            this.codice_prodotto = prd.codice_prodotto;
            this.titolo = prd.titolo;
            this.genere = prd.genere;
            this.producer = prd.producer;
            this.quantità = prd.quantità;
            long datauscita = long.Parse(prd.data_uscita.ToString("yyyyMMdd"));
            this.data_uscita =datauscita;
            this.prezzo = prd.prezzo;
            this.img = prd.img;


        }
    }
}
