using STANHOTEL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace STANHOTEL.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente

        public ActionResult Index()
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Cliente";
                SqlDataReader reader = cmd.ExecuteReader();

                Cliente.Clienti.Clear();
                if (reader.HasRows)
                {
                   while (reader.Read())
                    {
                        Cliente c = new Cliente();
                        c.ID = Convert.ToInt32(reader["ID_Cliente"]);
                        c.Cognome = reader["Cognome"].ToString();
                        c.Nome = reader["Nome"].ToString();
                        c.CodFiscale = reader["CodFiscale"].ToString();
                        c.Citta = reader["Citta"].ToString();
                        c.Provincia = reader["Provincia"].ToString();
                        c.Email = reader["Email"].ToString();
                        c.Telefono = reader["Telefono"].ToString();
                        c.Cellulare = reader["Cellulare"].ToString();
                        Cliente.Clienti.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(Cliente.Clienti);
        }
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Cliente c)
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("Cognome", c.Cognome);
                cmd.Parameters.AddWithValue("Nome", c.Nome);
                cmd.Parameters.AddWithValue("CodFiscale", c.CodFiscale);
                cmd.Parameters.AddWithValue("Citta", c.Citta);
                cmd.Parameters.AddWithValue("Provincia", c.Provincia);
                cmd.Parameters.AddWithValue("Email", c.Email);
                cmd.Parameters.AddWithValue("Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("Cellulare", c.Cellulare);
                cmd.CommandText = "INSERT INTO Cliente VALUES (@Cognome, @Nome, @CodFiscale, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";
                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                {
                    ViewBag.Success = "Inserimento effettuato con successo.";
                } else
                {
                    ViewBag.Error = "Nessuna riga interessata";
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
                cmd.Parameters.AddWithValue("ID_Cliente", id);
                cmd.CommandText = "DELETE FROM Cliente WHERE ID_Cliente = @ID_Cliente";
                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                {
                    ViewBag.Success = "Eliminazione effettuata con successo.";
                }
                else
                {
                    ViewBag.Error = "Nessuna riga interessata";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Lista");
        }

        public ActionResult Details(int id)
        {
            Cliente c = new Cliente();
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("ID_Cliente", id);
                cmd.CommandText = "SELECT * FROM Cliente WHERE ID_Cliente = @ID_Cliente";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c.ID = Convert.ToInt32(reader["ID_Cliente"]);
                        c.Cognome = reader["Cognome"].ToString();
                        c.Nome = reader["Nome"].ToString();
                        c.CodFiscale = reader["CodFiscale"].ToString();
                        c.Citta = reader["Citta"].ToString();
                        c.Provincia = reader["Provincia"].ToString();
                        c.Email = reader["Email"].ToString();
                        c.Telefono = reader["Telefono"].ToString();
                        c.Cellulare = reader["Cellulare"].ToString();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(c);
        }

        public ActionResult Edit(int id)
        {
            Cliente.Clienti.Clear();
            Cliente c = new Cliente();
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("ID_Cliente", id);
                cmd.CommandText = "SELECT * FROM Cliente WHERE ID_Cliente = @ID_Cliente";
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while (reader.Read()) { 
                        c.ID = Convert.ToInt32(reader["ID_Cliente"]);
                        c.Cognome = reader["Cognome"].ToString();
                        c.Nome = reader["Nome"].ToString();
                        c.CodFiscale = reader["CodFiscale"].ToString();
                        c.Citta = reader["Citta"].ToString();
                        c.Provincia = reader["Provincia"].ToString();
                        c.Email = reader["Email"].ToString();
                        c.Telefono = reader["Telefono"].ToString();
                        c.Cellulare = reader["Cellulare"].ToString();
                        Cliente.Clienti.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult Edit(Cliente c)
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("Cognome", c.Cognome);
                cmd.Parameters.AddWithValue("Nome", c.Nome);
                cmd.Parameters.AddWithValue("CodFiscale", c.CodFiscale);
                cmd.Parameters.AddWithValue("Citta", c.Citta);
                cmd.Parameters.AddWithValue("Provincia", c.Provincia);
                cmd.Parameters.AddWithValue("Email", c.Email);
                cmd.Parameters.AddWithValue("Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("Cellulare", c.Cellulare);
                cmd.Parameters.AddWithValue("ID_Cliente", c.ID);
                cmd.CommandText = "UPDATE Cliente SET Cognome = @Cognome, Nome = @Nome, CodFiscale = @CodFiscale, Citta = @Citta, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare WHERE ID_Cliente = @ID_Cliente";
                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                {
                    ViewBag.Success = "Inserimento effettuato con successo.";
                }
                else
                {
                    ViewBag.Error = "Nessuna riga interessata";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}