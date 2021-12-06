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
    public partial class Naracka : System.Web.UI.Page
    {
        baza.onlinebooking _b;
        private decimal _grdTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Pages/Korisink/index.aspx");
            }

            if (IsPostBack)
                return;

            _b = new baza.onlinebooking();
            var ds = _b.kosnicka(Parse(Session["id_korisnik"].ToString()));
            GridView1.DataSource = ds.Tables["kosnicka"];
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var ci = new CultureInfo("mk-MK");
                    var dt = Convert.ToDateTime(dr["vreme"].ToString());


                    dr["vreme"] = dt.ToString("dddd, dd-MMMM-yyyy", ci);
                }
            }

            GridView1.DataBind();

            lblinfo.Font.Bold = true;
            lblinfo.Font.Size = 12;
            lblinfo.ForeColor = Color.Red;
            lblinfo.Text = "Ве молиме користете вистински податоци!";

            lblZad.Font.Size = 9;
            lblZad.ForeColor = Color.Red;
            lblZad.Text = "Полињата со * се задолжителни";

            lblOdgovor.Visible = false;
            lblOdgovor.Text = "";
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "kosnicka") 
                return;

            var idKarti = Parse(e.CommandArgument.ToString());
            var idKlient = Parse(Session["id_korisnik"].ToString());
            _b = new baza.onlinebooking();
            var odgovor = _b.brisi_kosnicka_eden(idKarti, idKlient);
            if (odgovor == "OK")
            {
                Response.Redirect("~/Pages/Korisink/Kosnicka.aspx");
            }
            else
            {
                lblOdgovor.Visible = true;
                lblOdgovor.ForeColor = Color.Red;
                lblOdgovor.Text = "Грешка, обиди се повторно";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var rowTotal = Convert.ToDecimal
                (DataBinder.Eval(e.Row.DataItem, "cena"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _grdTotal += rowTotal;
            }

            var lbl = (Label) e.Row.FindControl("lblTotal");

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                lbl.Text = _grdTotal.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void BtnOtkaz_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Korisink/index.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Session["korisnik"] == null)
            {
                Response.Redirect("~/Pages/Korisink/index.aspx");
            }

            var data = DateTime.Today.ToString("yyyy/MM/dd");
            if (txtIme.Text != null && txtPrezime.Text != null)
            {
                lblOdgovor.Visible = true;

                _b = new baza.onlinebooking();
                var ds = _b.kosnicka(Parse(Session["id_korisnik"].ToString()));

                var j = GridView1.Rows.Count;

                for (var i = 0; i < j; i++)
                {
                    var karta = Parse((ds.Tables[0].Rows[i]["k_id_karti"]).ToString());
                    var klient = Parse(Session["id_korisnik"].ToString());

                    _b = new baza.onlinebooking();
                    var odgovor = _b.kupi_karta(karta, klient, data);

                    if (odgovor == "OK")
                    {
                        lblOdgovor.Text += "Успешнo ја купивте картата за настанот " +
                                           GridView1.Rows[((i + 1) - j) * (-1)].Cells[0].Text + "<br/>----<br/>";

                        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                        var random = new Random();
                        var result = new string(
                            Enumerable.Repeat(chars, 8)
                                .Select(s => s[random.Next(s.Length)])
                                .ToArray());
                        Response.Redirect(string.Format("~/NaplataUspesno.aspx?bar=" + 
                                                        result + "&klient=" + 
                                                        klient + "&karta=" + karta));
                    }
                    else
                    {
                        lblOdgovor.ForeColor = Color.Red;
                        lblOdgovor.Text += "Се случи грешка! Картата под реден број " +
                                           GridView1.Rows[((i + 1) - j) * (-1)].Cells[0].Text
                                           + " не е купена. Обиди се повторно ---- <br/>";
                    }

                    //lblOdgovor.Text += GridView1.Rows[((i + 1) - j) * (-1)].Cells[0].Text + " <br />";
                    //lblOdgovor.Text += ((i + 1) - j) * (-1) + " <br />-----<br/>";
                    //lblOdgovor.Text += "Karta - " + karta + "<br />" + "Klient - " + klient + "<br /> ---------- <br />";
                }

            }
        }

    }
}