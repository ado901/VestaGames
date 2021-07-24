using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Classi
{
    public class Commesso
    {
        public long codice_commesso { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }

        public void parseCommesso(commesso rigadb)
        {
            try
            {
                this.codice_commesso = rigadb.codice_commesso;
                this.nome = rigadb.nome;
                this.cognome = rigadb.cognome;
            }
            catch (Exception ex)
            {
                throw new Exception("metodo parseCommesso: " + ex.Message);
            }
        }
    }

    
}
