using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class Utente
    {
        [Required]
        public string password { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string cognome { get; set; }
        [Required]
        public DateTime nascita { get; set; }
        [Required]
        public string email { get; set; }
        public string indirizzo { get; set; }
        public double? portafoglio { get; set; }

        //prendo l'entity utente del db e setto tutte le proprietà dell'instanza
        public void loginEffettuato(utenti login)
        {
            try
            {
                this.email = login.email;
                this.password = login.password;
                this.nome = login.nome;
                this.cognome = login.cognome;
                DateTime dt;
                //converto il long di nascita nel formato datetime classico
                if (DateTime.TryParseExact(login.nascita.ToString(), "yyyyMMdd",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None, out dt))
                {
                }
                this.nascita = dt;
                this.indirizzo = login.indirizzo;
                this.portafoglio = login.portafoglio;
                if (this.portafoglio == null)
                {
                    this.portafoglio = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("metodo loginEffettuato: " + ex.Message);
            }


        }

        
    }
}
