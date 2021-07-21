using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "admin" nel codice e nel file di configurazione contemporaneamente.
    public class admin : Iadmin
    {
        const string OK = "OK";
        private static VestaGamesEntities db = new VestaGamesEntities();
        public enum Esito : int
        {
            OK = 1,
            KO = 0
        }
        public void DoWork()
        {
        }
        public List<Utente> listaUtenti(Utente ut)
        {
            if (ut.email=="admin@admin.admin")
            {
                List<utenti> listautenti = db.utenti.ToList();
            }
            return null;
        }
    }
}
