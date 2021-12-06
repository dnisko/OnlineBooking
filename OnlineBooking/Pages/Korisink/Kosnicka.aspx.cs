using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Korisink
{
    public partial class Kosnicka : System.Web.UI.Page
    {
        baza.onlinebooking b;
        decimal grdTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label1.Visible = false;
            if (!IsPostBack)
            {
                b = new baza.onlinebooking();
                DataSet ds = b.kosnicka(Int32.Parse(Session["id_korisnik"].ToString()));
                GridView1.DataSource = ds.Tables["kosnicka"];
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        CultureInfo ci = new CultureInfo("mk-MK");
                        DateTime dt = Convert.ToDateTime(dr["vreme"].ToString());


                        dr["vreme"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                    }
                }
                GridView1.DataBind();
                //Label1.Text = DateTime.Today.ToString("yyyy/MM/dd");
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "kosnicka")
            {
                int id_karti = Int32.Parse(e.CommandArgument.ToString());
                int id_klient = Int32.Parse(Session["id_korisnik"].ToString());
                b = new baza.onlinebooking();
                string odgovor = b.brisi_kosnicka_eden(id_karti, id_klient);
                if (odgovor == "OK")
                {
                    Response.Redirect("~/Kosnicka.aspx");
                }
                else
                {
                    Label1.Visible = true;
                    Label1.ForeColor = Color.Red;
                    Label1.Text = "Грешка, обиди се повторно";
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal rowTotal = Convert.ToDecimal
                            (DataBinder.Eval(e.Row.DataItem, "cena"));
                grdTotal = grdTotal + rowTotal;
                //Label1.Text += " grdTotal = " + grdTotal;
                //Label1.Text += " rowTotal = " + rowTotal;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblTotal");
                lbl.Text = grdTotal.ToString();
                if (GridView1.Rows.Count == 1)
                {
                    HyperLink hLkupi = (HyperLink)e.Row.FindControl("hLkupi");
                    hLkupi.Text = "Купи карта";
                }
                if (GridView1.Rows.Count > 1)
                {
                    HyperLink hLkupi = (HyperLink)e.Row.FindControl("hLkupi");
                    hLkupi.Text = "Купи карти";
                }
                //Label1.Text += "lbltotal = " + lbl.Text;
            }

        }
    }
}