﻿using server.Classi;
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
        void DoWork();
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
        (Esito, Commesso, string) modificaCommesso(Commesso comm);
        [OperationContract]
        (Esito, Prodotto, string) modificaProdotto(Prodotto prd);
    }
}
