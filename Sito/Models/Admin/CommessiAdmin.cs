using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sito.Models.Admin
{
    public class CommessiAdmin
    {
        public CommessiAdmin()
        {
            this.commesso = new ServiceReference2.Commesso();
        }
        public ServiceReference2.Commesso commesso { get; set; }
        [Required]
        public long codice_commesso { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string cognome { get; set; }

        public void parse()
        {
            this.codice_commesso = this.commesso.codice_commesso;
            this.nome = this.commesso.nome;
            this.cognome = this.commesso.cognome;
        }

        public void update()
        {
            this.commesso.nome = this.nome;
            this.commesso.cognome = this.cognome;
            this.commesso.codice_commesso = this.codice_commesso;
        }
    }
}