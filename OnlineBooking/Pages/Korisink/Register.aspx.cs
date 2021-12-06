using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Korisink
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblregister.Text = "Полињата со * се задолжителни.";
            regodgovor.Text = "";
            regodgovor.ForeColor = Color.Red;

            if (Session["korisnik"] != null)
            {
                Response.Redirect("~/Pages/Korisink/index.aspx");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var reg = new baza.onlinebooking();
            var odgovor = reg.Register(rUser.Text, rPass.Text, rIme.Text, rPrezime.Text, rEmail.Text);
            regodgovor.Text = odgovor;
            /*if (odgovor == "OK")
            {
                regodgovor.ForeColor = Color.Green;
                regodgovor.Text = "Успешна регистрација! Добредојдовте. Ќе бидете префрлени на страната за логирање.";
                Response.AddHeader("REFRESH", "3;URL=login.aspx");
            }
            else
            {*/
                switch (odgovor)
                {
                    case "OK":
                        regodgovor.ForeColor = Color.Green;
                        regodgovor.Text = "Успешна регистрација! Добредојдовте. Ќе бидете префрлени на страната за логирање.";
                        Response.AddHeader("REFRESH", "3;URL=login.aspx");
                        break;
                    case "user&email":
                        regodgovor.Text = "Корисничкото име и email-от се зафатени, одберете други.";
                        break;
                    case "user":
                        regodgovor.Text = "Корисничкото име е зафатено, одберете друг.";
                        break;
                    case "email":
                        regodgovor.Text = "Еmail-от е зафатен, одберете друг.";
                        break;
                    default:
                        regodgovor.Text = "Се случи грешка";
                        break;
                }
            //}
        }
    }
}