using server.Classi;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;
using static server.Service1;

namespace server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        (Esito, Utente, string) Login(Utente ut);

        [OperationContract]
        (Esito, Utente, string) Registrazione(Utente ut);

        [OperationContract]
        (Esito, List<Prodotto>, string) getProdotti();

        [OperationContract]
        (Esito, Utente, string) modificaUtente(Utente ut, string field, string emailnuova = null);

        

        [OperationContract]
        (Esito, Utente, Prodotto, string) compraProdotto(Prodotto prod, Utente ut);
    }
}
