using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Korisink
{
    public partial class Korisik : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLinkLogIn.Visible = true;
            HyperLinkReg.Visible = true;

            HyperLinkLogOut.Visible = false;
            HyperLinkProfile.Visible = false;

            ImageButton1.Visible = false;

            if ((Session["korisnik"] == null) && (Session["admin"] == null))
            {
                welcomelbl.Text = "Добредојдовте, ";
            }
            if (Session["korisnik"] != null)
            {
                welcomelbl.Text = "Добредојде " + Session["korisnik"].ToString() + " ";
                HyperLinkLogIn.Visible = false;
                HyperLinkReg.Visible = false;

                HyperLinkLogOut.Visible = true;
                HyperLinkProfile.Visible = true;

                ImageButton1.Visible = true;
            }
            if (Session["admin"] != null)
            {
                Response.Redirect("~/Admin/pocetnaadmin.aspx");
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Kosnicka.aspx");
        }
    }
}