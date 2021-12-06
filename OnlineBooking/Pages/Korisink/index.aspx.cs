using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Korisink
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var b = new baza.onlinebooking();

            var ds = b.lista_na_nastani_za_nastan();
            reNastani.DataSource = ds;
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var ci = new CultureInfo("mk-MK");
                    var dt = Convert.ToDateTime(dr["vreme"].ToString());


                    dr["vreme"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                }
            }
            reNastani.DataBind();
        }
    }
}