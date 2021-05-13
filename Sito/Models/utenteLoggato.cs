using Sito.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Sito.Models
{
    public class utenteLoggato
    {
        public utenteLoggato()
        {
            this.ut = new Utente();
        }
        public Utente ut { get; private set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail non valida")]
        public string email
        {
            get
            {
                return this.ut.email;
            }


            set
            {
                this.ut.email = value;

            }
        }
        [Required]
        public string password
        {
            get
            {
                return this.ut.password;
            }


            set
            {
                byte[] data = Encoding.ASCII.GetBytes(value);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                this.ut.password = Encoding.ASCII.GetString(data);


            }
        }
    }
}