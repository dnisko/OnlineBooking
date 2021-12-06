using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Korisink
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            if (Session["korisnik"] == null)
            {
                lblporaka.Text = "Успешно се одлогиравте, ве очекуваме повторно.";
                Response.AddHeader("REFRESH", "3;URL=index.aspx");
            }
            else
            {
                lblporaka.Text = "Се случи грешка, обидете се повторно.";
            }
        }
    }
}