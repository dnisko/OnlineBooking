using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Int32;

namespace OnlineBooking.Pages.Korisink
{
    public partial class Kosnicka : System.Web.UI.Page
    {
        baza.onlinebooking b;
        decimal _grdTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label1.Visible = false;
            if (IsPostBack)
                return;
            b = new baza.onlinebooking();
            var ds = b.kosnicka(Parse(Session["id_korisnik"].ToString()));
            GridView1.DataSource = ds.Tables["kosnicka"];
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var ci = new CultureInfo("mk-MK");
                    var dt = Convert.ToDateTime(dr["vreme"].ToString());


                    dr["vreme"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                }
            }
            GridView1.DataBind();
            //Label1.Text = DateTime.Today.ToString("yyyy/MM/dd");
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "kosnicka")
                return;
            var idKarti = Parse(e.CommandArgument.ToString());
            var idKlient = Parse(Session["id_korisnik"].ToString());
            b = new baza.onlinebooking();
            var odgovor = b.brisi_kosnicka_eden(idKarti, idKlient);
            if (odgovor == "OK")
            {
                Response.Redirect("~/Pages/Korisink/Kosnicka.aspx");
            }
            else
            {
                Label1.Visible = true;
                Label1.ForeColor = Color.Red;
                Label1.Text = "Грешка, обиди се повторно";
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rowTotal = Convert.ToDecimal
                            (DataBinder.Eval(e.Row.DataItem, "cena"));
                _grdTotal += rowTotal;
                //Label1.Text += " grdTotal = " + grdTotal;
                //Label1.Text += " rowTotal = " + rowTotal;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var lbl = (Label)e.Row.FindControl("lblTotal");
                lbl.Text = _grdTotal.ToString(CultureInfo.InvariantCulture);
                if (GridView1.Rows.Count == 1)
                {
                    var hLkupi = (HyperLink)e.Row.FindControl("hLkupi");
                    hLkupi.Text = "Купи карта";
                }

                if (GridView1.Rows.Count <= 1)
                    return;
                {
                    var hLkupi = (HyperLink)e.Row.FindControl("hLkupi");
                    hLkupi.Text = "Купи карти";
                }
                //Label1.Text += "lbltotal = " + lbl.Text;
            }

        }
    }
}