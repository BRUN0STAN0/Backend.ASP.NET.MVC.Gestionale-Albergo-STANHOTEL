using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STANHOTEL.Models
{
    public class Servizio
    {
        public string Descrizione { get; set; }
        public static List<Servizio> Servizi = new List<Servizio>();
    }
}