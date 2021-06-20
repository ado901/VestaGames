using Sito.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
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
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    this.ut.password= builder.ToString();
                }


            }
        }
    }
}