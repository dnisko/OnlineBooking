using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Korisink
{
    public partial class Pregled : System.Web.UI.Page
    {
        private baza.onlinebooking _b;
        protected void Page_Load(object sender, EventArgs e)
        {
            _b = new baza.onlinebooking();
            var ds = _b.lista_na_karti_DS();
            GridView1.DataSource = ds.Tables["nastani"];
            foreach (DataTable table in ds.Tables)
            {
                var ci = new CultureInfo("mk-MK");
                foreach (DataRow dr in table.Rows)
                {
                    var dt = Convert.ToDateTime(dr["vreme"].ToString());
                    var sega = DateTime.Today.Date;
                    if (dt < sega)
                    {

                    }
                    else if (dt > sega)
                    {
                        dr["vreme"] = dt.ToString("dddd, dd-MMMM-yyyy", ci);
                    }
                }
            }
            GridView1.DataBind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var ci = new CultureInfo("mk-MK");
            var dt = DateTime.Now;
            dt.ToString("dddd, dd-MMMM-yyyy", ci);

            if ((e.Row.Cells[5].Text) == "Дата")
            {
                e.Row.Visible = true;
            }
            else if ((e.Row.Cells[5].Text) == "&nbsp;")
            {
                e.Row.Visible = false;
            }
            else if (Regex.IsMatch(e.Row.Cells[5].Text, @"^\d+"))
            {
                if (DateTime.Parse((e.Row.Cells[5].Text))
                    <= DateTime.Parse(dt.ToString("yyyy-M-d")))
                {
                    e.Row.Visible = false;
                }
            }
        }
    }
}