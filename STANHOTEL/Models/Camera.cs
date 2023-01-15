using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STANHOTEL.Models
{
    public class Camera
    {
        public int ID { get; set; }
        [Display (Name = "Numero Camera")]
        public int Numero { get; set; }

        [Display(Name = "La camera è gia occupata?")]
        public bool Occupata { get; set; }

        [Display(Name = "Tipologia Camera")]
        public int ID_TipologiaCamera { get; set; }

        // --------------------------------------------
        [Display(Name = "Tipologia Camera")]
        public string Tipo { get; set; }
        [DataType(DataType.Currency)]
        public double Costo { get; set; }

        public static List<Camera> Camere = new List<Camera>();

    }
}