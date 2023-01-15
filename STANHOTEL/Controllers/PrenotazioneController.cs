using STANHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STANHOTEL.Controllers
{
    [Authorize]
    public class PrenotazioneController : Controller
    {
        // GET: Prenotazione
        
        public ActionResult Index()
        {
            Prenotazione.Prenotazioni.Clear();
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Prenotazione " +
                    "JOIN Cliente on Prenotazione.ID_Cliente = Cliente.ID_Cliente " +
                    "JOIN Pensione on Prenotazione.ID_Pensione = Pensione.ID_Pensione " +
                    "JOIN Camera on Prenotazione.ID_Camera = Camera.ID_Camera " +
                    "JOIN TipologiaCamera on TipologiaCamera.ID_TipologiaCamera = Camera.ID_TipologiaCamera";
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prenotazione p = new Prenotazione();
                        p.Id = Convert.ToInt32(reader["ID_Prenotazione"]);
                        p.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        p.DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]);
                        p.DataFineSoggiorno = Convert.ToDateTime(Convert.ToDateTime(reader["DataFineSoggiorno"]).ToString("D"));
                        p.Acconto = Convert.ToDouble(reader["Acconto"]);
                        p.Cliente = reader["Nome"] + " " + reader["Cognome"];
                        p.Camera = "Numero " + reader["Numero"] + " - " + reader["TipoCamera"] + " [costo " + Convert.ToInt32(reader["CostoCamera"]) + "€ ]";
                        p.Pensione = reader["TipoPensione"] + " [costo " + Convert.ToInt32(reader["CostoPensione"]) + "€ ]";
                        p.CostoTotaleEsclusiServizi = Convert.ToInt32(reader["CostoPensione"]) + Convert.ToInt32(reader["CostoCamera"]);
                        Prenotazione.Prenotazioni.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(Prenotazione.Prenotazioni);
        }
    }
}