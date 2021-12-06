using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Korisink
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] != null)
            {
                Response.Redirect("~/Pages/Korisink/index.aspx");
            }
            lblPoraka.Text = "";
            HyperLink1.Visible = false;
        }
        protected void LogInbtn_Click(object sender, EventArgs e)
        {
            var login = new baza.onlinebooking();
            var ds = login.login(usertxt.Text, passtxt.Text);

            if (ds.Tables[0].Rows.Count == 1)
            {
                var ime = (ds.Tables[0].Rows[0]["ime"]).ToString();
                var prezime = (ds.Tables[0].Rows[0]["prezime"]).ToString();
                var email = (ds.Tables[0].Rows[0]["email"]).ToString();
                var user = (ds.Tables[0].Rows[0]["username"]).ToString();
                var pass = (ds.Tables[0].Rows[0]["pass"]).ToString();
                var id_user = int.Parse((ds.Tables[0].Rows[0]["id_klient"]).ToString());
                var isAdmin = Convert.ToBoolean(ds.Tables[0].Rows[0]["isadmin"]);

                if (isAdmin)
                {
                    Session["id_admin"] = id_user;
                    Session["admin"] = ime;

                    Session["prezime"] = prezime;
                    Session["email"] = email;
                    Session["user"] = user;
                    Session["pass"] = pass;

                    Response.Redirect("~/Pages/Admin/pocetnaadmin.aspx");
                }
                else
                {
                    Session["id_korisnik"] = id_user;
                    Session["korisnik"] = ime;

                    Session["prezime"] = prezime;
                    Session["email"] = email;
                    Session["user"] = user;
                    Session["pass"] = pass;

                    Response.Redirect("~/Pages/Korisink/index.aspx");
                }
            }
            else
            {
                lblPoraka.Text = "Погрешна најава, обидете се повторно или регистрирајте се ";
                HyperLink1.Visible = true;
            }
        }
        protected void LogInCanclebtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Korisink/index.aspx");
        }
    }
}