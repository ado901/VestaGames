//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sito.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Utente", Namespace="http://schemas.datacontract.org/2004/07/server")]
    [System.SerializableAttribute()]
    public partial class Utente : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string cognomeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string emailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string indirizzoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime nascitaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nomeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string passwordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> portafoglioField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string cognome {
            get {
                return this.cognomeField;
            }
            set {
                if ((object.ReferenceEquals(this.cognomeField, value) != true)) {
                    this.cognomeField = value;
                    this.RaisePropertyChanged("cognome");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string email {
            get {
                return this.emailField;
            }
            set {
                if ((object.ReferenceEquals(this.emailField, value) != true)) {
                    this.emailField = value;
                    this.RaisePropertyChanged("email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int id {
            get {
                return this.idField;
            }
            set {
                if ((this.idField.Equals(value) != true)) {
                    this.idField = value;
                    this.RaisePropertyChanged("id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string indirizzo {
            get {
                return this.indirizzoField;
            }
            set {
                if ((object.ReferenceEquals(this.indirizzoField, value) != true)) {
                    this.indirizzoField = value;
                    this.RaisePropertyChanged("indirizzo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime nascita {
            get {
                return this.nascitaField;
            }
            set {
                if ((this.nascitaField.Equals(value) != true)) {
                    this.nascitaField = value;
                    this.RaisePropertyChanged("nascita");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nome {
            get {
                return this.nomeField;
            }
            set {
                if ((object.ReferenceEquals(this.nomeField, value) != true)) {
                    this.nomeField = value;
                    this.RaisePropertyChanged("nome");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                if ((object.ReferenceEquals(this.passwordField, value) != true)) {
                    this.passwordField = value;
                    this.RaisePropertyChanged("password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> portafoglio {
            get {
                return this.portafoglioField;
            }
            set {
                if ((this.portafoglioField.Equals(value) != true)) {
                    this.portafoglioField = value;
                    this.RaisePropertyChanged("portafoglio");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Service1.Esito", Namespace="http://schemas.datacontract.org/2004/07/server")]
    public enum Service1Esito : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OK = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        KO = 0,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Prodotto", Namespace="http://schemas.datacontract.org/2004/07/server.Classi")]
    [System.SerializableAttribute()]
    public partial class Prodotto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long codice_prodottoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime data_uscitaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string genereField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string imgField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double prezzoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string producerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int quantitàField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string titoloField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long codice_prodotto {
            get {
                return this.codice_prodottoField;
            }
            set {
                if ((this.codice_prodottoField.Equals(value) != true)) {
                    this.codice_prodottoField = value;
                    this.RaisePropertyChanged("codice_prodotto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime data_uscita {
            get {
                return this.data_uscitaField;
            }
            set {
                if ((this.data_uscitaField.Equals(value) != true)) {
                    this.data_uscitaField = value;
                    this.RaisePropertyChanged("data_uscita");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string genere {
            get {
                return this.genereField;
            }
            set {
                if ((object.ReferenceEquals(this.genereField, value) != true)) {
                    this.genereField = value;
                    this.RaisePropertyChanged("genere");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string img {
            get {
                return this.imgField;
            }
            set {
                if ((object.ReferenceEquals(this.imgField, value) != true)) {
                    this.imgField = value;
                    this.RaisePropertyChanged("img");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double prezzo {
            get {
                return this.prezzoField;
            }
            set {
                if ((this.prezzoField.Equals(value) != true)) {
                    this.prezzoField = value;
                    this.RaisePropertyChanged("prezzo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string producer {
            get {
                return this.producerField;
            }
            set {
                if ((object.ReferenceEquals(this.producerField, value) != true)) {
                    this.producerField = value;
                    this.RaisePropertyChanged("producer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int quantità {
            get {
                return this.quantitàField;
            }
            set {
                if ((this.quantitàField.Equals(value) != true)) {
                    this.quantitàField = value;
                    this.RaisePropertyChanged("quantità");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string titolo {
            get {
                return this.titoloField;
            }
            set {
                if ((object.ReferenceEquals(this.titoloField, value) != true)) {
                    this.titoloField = value;
                    this.RaisePropertyChanged("titolo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DoWork", ReplyAction="http://tempuri.org/IService1/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DoWork", ReplyAction="http://tempuri.org/IService1/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Login", ReplyAction="http://tempuri.org/IService1/LoginResponse")]
        System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string> Login(Sito.ServiceReference1.Utente ut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Login", ReplyAction="http://tempuri.org/IService1/LoginResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string>> LoginAsync(Sito.ServiceReference1.Utente ut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Registrazione", ReplyAction="http://tempuri.org/IService1/RegistrazioneResponse")]
        System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string> Registrazione(Sito.ServiceReference1.Utente ut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Registrazione", ReplyAction="http://tempuri.org/IService1/RegistrazioneResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string>> RegistrazioneAsync(Sito.ServiceReference1.Utente ut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getProdotti", ReplyAction="http://tempuri.org/IService1/getProdottiResponse")]
        System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Prodotto[], string> getProdotti();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getProdotti", ReplyAction="http://tempuri.org/IService1/getProdottiResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Prodotto[], string>> getProdottiAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/modificaUtente", ReplyAction="http://tempuri.org/IService1/modificaUtenteResponse")]
        System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string> modificaUtente(Sito.ServiceReference1.Utente ut, string field, string emailnuova);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/modificaUtente", ReplyAction="http://tempuri.org/IService1/modificaUtenteResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string>> modificaUtenteAsync(Sito.ServiceReference1.Utente ut, string field, string emailnuova);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/compraProdotto", ReplyAction="http://tempuri.org/IService1/compraProdottoResponse")]
        System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, Sito.ServiceReference1.Prodotto, string> compraProdotto(Sito.ServiceReference1.Prodotto prod, Sito.ServiceReference1.Utente ut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/compraProdotto", ReplyAction="http://tempuri.org/IService1/compraProdottoResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, Sito.ServiceReference1.Prodotto, string>> compraProdottoAsync(Sito.ServiceReference1.Prodotto prod, Sito.ServiceReference1.Utente ut);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Sito.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Sito.ServiceReference1.IService1>, Sito.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string> Login(Sito.ServiceReference1.Utente ut) {
            return base.Channel.Login(ut);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string>> LoginAsync(Sito.ServiceReference1.Utente ut) {
            return base.Channel.LoginAsync(ut);
        }
        
        public System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string> Registrazione(Sito.ServiceReference1.Utente ut) {
            return base.Channel.Registrazione(ut);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string>> RegistrazioneAsync(Sito.ServiceReference1.Utente ut) {
            return base.Channel.RegistrazioneAsync(ut);
        }
        
        public System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Prodotto[], string> getProdotti() {
            return base.Channel.getProdotti();
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Prodotto[], string>> getProdottiAsync() {
            return base.Channel.getProdottiAsync();
        }
        
        public System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string> modificaUtente(Sito.ServiceReference1.Utente ut, string field, string emailnuova) {
            return base.Channel.modificaUtente(ut, field, emailnuova);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, string>> modificaUtenteAsync(Sito.ServiceReference1.Utente ut, string field, string emailnuova) {
            return base.Channel.modificaUtenteAsync(ut, field, emailnuova);
        }
        
        public System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, Sito.ServiceReference1.Prodotto, string> compraProdotto(Sito.ServiceReference1.Prodotto prod, Sito.ServiceReference1.Utente ut) {
            return base.Channel.compraProdotto(prod, ut);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<Sito.ServiceReference1.Service1Esito, Sito.ServiceReference1.Utente, Sito.ServiceReference1.Prodotto, string>> compraProdottoAsync(Sito.ServiceReference1.Prodotto prod, Sito.ServiceReference1.Utente ut) {
            return base.Channel.compraProdottoAsync(prod, ut);
        }
    }
}
