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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authentication(Dipendente d)
        {
            try { 
                SqlConnection con = Shared.GetConnectionToDB();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("Username", d.Username);
                cmd.Parameters.AddWithValue("Psw", d.Password);
                cmd.CommandText = "SELECT * FROM Dipendente WHERE Username = @Username AND Psw = @Psw";
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(d.Username, false);
                    return Redirect(FormsAuthentication.DefaultUrl);
                }
            } catch (Exception ex)
            {
              
            }
            return View();
        }

        public ActionResult Disauthentication()
        {
            FormsAuthentication.SignOut();
            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ".ASPXAUTH");
            //cookie.Expires = DateTime.Now.AddYears(-1);
            //Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }
    }
}