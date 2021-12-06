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
    public partial class Nastani : System.Web.UI.Page
    {
        private baza.onlinebooking _b;
        protected void Page_Load(object sender, EventArgs e)
        {
            _b = new baza.onlinebooking();
            var ds = _b.lista_na_nastani_za_nastan();
            DataList1.DataSource = ds;
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var ci = new CultureInfo("mk-MK");
                    var dt = Convert.ToDateTime(dr["vreme"].ToString());


                    dr["vreme"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                }
            }
            DataList1.DataBind();
        }
    }
}