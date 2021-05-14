using server.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
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
        void Dowork1(string arg);

        [OperationContract]
        (Esito, Utente, string) Login(Utente ut);

        [OperationContract]
        (Esito, Utente, string) Registrazione(Utente ut);

        [OperationContract]
        (Esito, List<Prodotto>, string) getProdotti();
    }
}
