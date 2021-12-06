using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Admin
{
    public partial class pocetnaadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["korisnik"] != null) || (Session["admin"] == null))
            {
                Response.Redirect("~/Pages/Korisnik/index.aspx");
            }
        }
    }
}