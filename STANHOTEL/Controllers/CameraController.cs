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
    public class CameraController : Controller
    {
        // GET: Camera
        public ActionResult Index()
        {
            Camera.Camere.Clear();
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Camera JOIN TipologiaCamera ON Camera.ID_TipologiaCamera = TipologiaCamera.ID_TipologiaCamera";
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Camera c = new Camera();
                        c.ID = Convert.ToInt32(reader["ID_Camera"]);
                        c.Numero = Convert.ToInt32(reader["Numero"]);
                        c.ID_TipologiaCamera = Convert.ToInt32(reader["ID_TipologiaCamera"]);
                        c.Tipo = reader["Tipo"].ToString();
                        c.Costo = Convert.ToDouble(reader["Costo"]);
                        Camera.Camere.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(Camera.Camere);
        }
        public ActionResult Create()
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM TipologiaCamera";
                SqlDataReader reader = cmd.ExecuteReader();

                List<SelectListItem> ListaTipologieCamere = new List<SelectListItem>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        SelectListItem tipologiaCamera = new SelectListItem();
                        tipologiaCamera.Value = reader["ID_TipologiaCamera"].ToString();
                        tipologiaCamera.Text = reader["Tipo"] + " [Costo:" + reader["Costo"]+"]";
                        ListaTipologieCamere.Add(tipologiaCamera);
                    }
                }
                ViewBag.DDL_TipologiaCamere = ListaTipologieCamere;
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Camera c)
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("Numero", c.Numero);
                cmd.Parameters.AddWithValue("ID_TipologiaCamera", c.ID_TipologiaCamera);
                cmd.CommandText = "INSERT INTO Camera VALUES (@Numero, @ID_TipologiaCamera)";
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

        public ActionResult Delete(int id)
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("ID_Camera", id);
                cmd.CommandText = "DELETE FROM Camera WHERE ID_Camera = @ID_Camera";
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
            Camera c = new Camera();
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("ID_Camera", id);
                cmd.CommandText = "SELECT * FROM Camera JOIN TipologiaCamera ON Camera.ID_TipologiaCamera = TipologiaCamera.ID_TipologiaCamera WHERE ID_Camera = @ID_Camera";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c.ID = Convert.ToInt32(reader["ID_Camera"]);
                        c.Occupata = Convert.ToBoolean(reader["Occupata"]);
                        c.Numero = Convert.ToInt32(reader["Numero"]);
                        c.ID_TipologiaCamera = Convert.ToInt32(reader["ID_TipologiaCamera"]);
                        c.Tipo = reader["Tipo"].ToString();
                        c.Costo = Convert.ToDouble(reader["Costo"]);

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
            Camera.Camere.Clear();
            Camera c = new Camera();

            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("ID_Camera", id);
                cmd.CommandText = "SELECT * FROM Camera WHERE ID_Camera = @ID_Camera";
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c.ID = Convert.ToInt32(reader["ID_Camera"]);
                        c.Numero = Convert.ToInt32(reader["Numero"]);
                        c.Occupata = Convert.ToBoolean(reader["Occupata"]);
                        Camera.Camere.Add(c);
                    }
                    
                }

                SqlConnection con2 = Shared.GetConnectionToDB();
                con2.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con2;
                cmd2.CommandText = "SELECT * FROM TipologiaCamera";
                SqlDataReader reader2 = cmd2.ExecuteReader();

                List<SelectListItem> ListaTipologieCamere = new List<SelectListItem>();

                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {

                        SelectListItem tipologiaCamera = new SelectListItem();
                        tipologiaCamera.Value = reader2["ID_TipologiaCamera"].ToString();
                        tipologiaCamera.Text = reader2["Tipo"] + " [Costo:" + reader2["Costo"] + "]";
                        ListaTipologieCamere.Add(tipologiaCamera);
                    }
                }
                ViewBag.DDL_TipologiaCamere = ListaTipologieCamere;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult Edit(Camera c)
        {
            try
            {
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("ID_Camera", c.ID);
                cmd.Parameters.AddWithValue("Occupata", c.Occupata);
                cmd.Parameters.AddWithValue("ID_TipologiaCamera", c.ID_TipologiaCamera);
                cmd.CommandText = "UPDATE Camera SET Occupata = @Occupata, ID_TipologiaCamera = @ID_TipologiaCamera WHERE ID_Camera = @ID_Camera";
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