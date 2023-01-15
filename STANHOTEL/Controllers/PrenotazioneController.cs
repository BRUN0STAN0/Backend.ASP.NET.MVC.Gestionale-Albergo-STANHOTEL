using STANHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                        




                        SqlConnection con2 = Shared.GetConnectionToDB();
                        con2.Open();
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = con2;
                        cmd2.Parameters.AddWithValue("ID_Prenotazione", p.Id);
                        cmd2.CommandText = "SELECT * FROM Servizio JOIN Prenotazione ON Servizio.ID_Prenotazione = Prenotazione.ID_Prenotazione JOIN TipologiaServizio ON Servizio.ID_TipologiaServizio = TipologiaServizio.ID_TipologiaServizio WHERE Prenotazione.ID_Prenotazione = @ID_Prenotazione";
                        SqlDataReader reader2 = cmd2.ExecuteReader();

                        p.Servizi.Clear();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                Servizio s = new Servizio();
                                s.Descrizione = reader2["Descrizione"].ToString();
                                p.Servizi.Add(s);
                            }
                        }

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
        public ActionResult Create()
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Cliente";
                SqlDataReader reader = cmd.ExecuteReader();

                List<SelectListItem> ListaClienti = new List<SelectListItem>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        SelectListItem Cliente = new SelectListItem();
                        Cliente.Text = reader["Nome"] + " " + reader["Cognome"];
                        Cliente.Value = reader["ID_Cliente"].ToString();
                        ListaClienti.Add(Cliente);
                    }
                }
                ViewBag.DDL_ListaClienti = ListaClienti;




                SqlConnection con2 = Shared.GetConnectionToDB();
                con2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandText = "SELECT * FROM Camera JOIN TipologiaCamera ON Camera.ID_TipologiaCamera = TipologiaCamera.ID_TipologiaCamera";
                SqlDataReader reader2 = cmd2.ExecuteReader();

                List<SelectListItem> ListaCamere = new List<SelectListItem>();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {

                        SelectListItem Camera = new SelectListItem();
                        Camera.Value = reader2["ID_Camera"].ToString();
                        Camera.Text = reader2["Numero"] + " " + reader2["TipoCamera"] + " [costo " + reader2["CostoCamera"] + "]";
                        ListaCamere.Add(Camera);
                    }
                }
                ViewBag.DDL_ListaCamere = ListaCamere;



                SqlConnection con3 = Shared.GetConnectionToDB();
                con3.Open();
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con3;
                cmd3.CommandText = "SELECT * FROM Pensione";
                SqlDataReader reader3 = cmd3.ExecuteReader();

                List<SelectListItem> ListaPensioni = new List<SelectListItem>();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {

                        SelectListItem Pensione = new SelectListItem();
                        Pensione.Value = reader3["ID_Pensione"].ToString();
                        Pensione.Text = reader3["TipoPensione"] + " [costo " + reader3["CostoPensione"] + "]";
                        ListaPensioni.Add(Pensione);
                    }
                }
                ViewBag.DDL_ListaPensioni = ListaPensioni;



            } catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Prenotazione p)
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("DataPrenotazione", p.DataPrenotazione);
                cmd.Parameters.AddWithValue("DataInizioSoggiorno", p.DataInizioSoggiorno);
                cmd.Parameters.AddWithValue("DataFineSoggiorno", p.DataFineSoggiorno);
                cmd.Parameters.AddWithValue("Acconto", p.Acconto);
                cmd.Parameters.AddWithValue("ID_Cliente", p.ID_Cliente);
                cmd.Parameters.AddWithValue("ID_Camera", p.ID_Camera);
                cmd.Parameters.AddWithValue("ID_Pensione", p.ID_Pensione);
                cmd.CommandText = "INSERT INTO Prenotazione VALUES (@DataPrenotazione, @DataInizioSoggiorno, @DataFineSoggiorno, @Acconto, @ID_Cliente, @ID_Camera, @ID_Pensione)";
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Prenotazione pre = new Prenotazione();
                        pre.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        pre.DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]);
                        pre.DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]);
                        pre.Acconto = Convert.ToDouble(reader["Acconto"]);
                        pre.ID_Cliente = Convert.ToInt32(reader["ID_Cliente"]);
                        pre.ID_Camera = Convert.ToInt32(reader["ID_Camera"]);
                        pre.ID_Pensione = Convert.ToInt32(reader["ID_Pensione"]);
                        Prenotazione.Prenotazioni.Add(pre);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("ID_Prenotazione", id);
                cmd.CommandText = "DELETE FROM Prenotazione WHERE ID_Prenotazione = @ID_Prenotazione";
                int row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}