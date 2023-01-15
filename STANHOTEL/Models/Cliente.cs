using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STANHOTEL.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string CodFiscale { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Cellulare { get; set; }

        public static List<Cliente> Clienti = new List<Cliente>();
    }
}