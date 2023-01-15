using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STANHOTEL.Models
{
    public class Prenotazione
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataPrenotazione { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataInizioSoggiorno { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataFineSoggiorno { get; set; }
        [DataType(DataType.Currency)]
        public double Acconto { get; set; }
        public int ID_Cliente { get; set; }
        public string Cliente { get; set; }
        public int ID_Camera { get; set; }
        public string Camera { get; set; }
        public int ID_Pensione { get; set; }
        public string Pensione { get; set; }
        [DataType(DataType.Currency)]

        public double CostoTotaleEsclusiServizi { get; set; }

        public static List<Prenotazione> Prenotazioni = new List<Prenotazione>();

        public List<Servizio> Servizi { get; set; } = new List<Servizio>();
    }
}