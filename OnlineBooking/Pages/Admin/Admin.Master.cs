using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblPorakaPromena.Visible = false;
            }

            if ((Session["korisnik"] == null) && (Session["admin"] == null))
            {
                Response.Redirect("~/Pages/Korisink/index.aspx");
            }

            if (Session["korisnik"] != null)
            {
                Response.Redirect("~/Pages/Korisink/index.aspx");
            }

            if (Session["admin"] == null) return;
            Label1.Text = "Добредојде админ " + Session["admin"].ToString();

            LinkButton3.Visible = true;
            LinkButton4.Visible = true;
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Korisink/index.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            var promena = new baza.onlinebooking();

            if (Session["admin"] != null)
            {
                var odgovor = promena.Promeni(txtPime.Text, txtPprezime.Text, txtPemail.Text, txtPuser.Text,
                    txtPpass.Text, Int32.Parse(Session["id_admin"].ToString()));

                if (odgovor == "OK")
                {
                    lblPorakaPromena.Visible = true;
                    lblPorakaPromena.ForeColor = Color.Green;
                    lblPorakaPromena.Text = "Успешна промена";
                }
                else if (odgovor == "NO")
                {
                    lblPorakaPromena.Visible = true;
                    lblPorakaPromena.ForeColor = Color.Red;
                    lblPorakaPromena.Text = "Се случи грешка, обиди се повторно";
                }
                else if (odgovor == "pole")
                {
                    lblPorakaPromena.Visible = true;
                    lblPorakaPromena.ForeColor = Color.Red;
                    lblPorakaPromena.Text = "Сите полиња се задолжителни";
                }
            }
            else
            {
                lblPorakaPromena.Visible = true;
                lblPorakaPromena.ForeColor = Color.Red;
                lblPorakaPromena.Text = "Автоматски бевте одлогирани, ќе бидете префрлени кон страната за најава.";
                Response.AddHeader("REFRESH", "3;URL=login.aspx");
            }
        }

        protected void BtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Pages/Korisink/index.aspx");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            txtPime.Text = Session["admin"].ToString();
            txtPprezime.Text = Session["prezime"].ToString();
            txtPemail.Text = Session["email"].ToString();
            txtPuser.Text = Session["user"].ToString();
            txtPpass.Text = Session["pass"].ToString();

            LoggedPanel.Visible = true;
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["admin"] = null;
            Session["korisnik"] = null;
            Response.Redirect("~/Pages/Korisink/index.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (Session["admin"] != null)
            {
                txtPime.Text = Session["admin"].ToString();
                txtPprezime.Text = Session["prezime"].ToString();
                txtPemail.Text = Session["email"].ToString();
                txtPuser.Text = Session["user"].ToString();
                txtPpass.Text = Session["pass"].ToString();

                LoggedPanel.Visible = true;
            }
            else
            {
                Response.Redirect("~/Pages/Korisink/index.aspx");
            }
        }
    }
}