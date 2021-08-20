using server.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using static server.admin;

namespace server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "Iadmin" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface Iadmin
    {
        
        [OperationContract]
        (Esito, List<Utente>, string) listaUtenti(Utente ut);
        [OperationContract]
        (Esito, List<Commesso>, string) listaCommessi(Utente ut);
        [OperationContract]
        (Esito, List<Prodotto>, string) listaProdotti(Utente ut);
        [OperationContract]
        (Esito, List<Transazione>, string) listaTransazioni(Utente ut);
        [OperationContract]
        (Esito, Utente, string) modificaUtente(Utente ut);
        [OperationContract]
        (Esito, string) eliminaUtente(Utente ut);
        [OperationContract]
        (Esito, Utente, string) aggiungiUtente(Utente ut);
        [OperationContract]
        (Esito, Commesso, string) modificaCommesso(Commesso comm);
        [OperationContract]
        (Esito, string) eliminaCommesso(Commesso comm);
        [OperationContract]
        (Esito, Commesso, string) aggiungiCommesso(Commesso comm);
        [OperationContract]
        (Esito, Prodotto, string) modificaProdotto(Prodotto prd);
        [OperationContract]
        (Esito, string) eliminaProdotto(Prodotto prd);
        [OperationContract]
        (Esito, Prodotto, string) aggiungiProdotto(Prodotto prd);
        [OperationContract]
        (Esito, Transazione, string) modificaTransazione(Transazione trn);
        [OperationContract]
        (Esito, string) eliminaTransazione(Transazione trn);
        [OperationContract]
        (Esito, Transazione, string) aggiungiTransazione(Transazione trn);

    }
}
