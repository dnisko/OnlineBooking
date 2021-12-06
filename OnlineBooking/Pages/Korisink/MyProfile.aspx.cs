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
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblpromena.Text = "";
            lblpromena.ForeColor = Color.Green;
            if (!IsPostBack)
            {
                var user = new baza.onlinebooking();
                var ds = user.lista_na_korisnici_po_id_DS(Int32.Parse(Session["id_korisnik"].ToString()));
                if (ds.Tables[0].Rows.Count == 1)
                {
                    var ime = (ds.Tables[0].Rows[0]["ime"]).ToString();
                    var prezime = (ds.Tables[0].Rows[0]["prezime"]).ToString();
                    var email = (ds.Tables[0].Rows[0]["email"]).ToString();
                    var username = (ds.Tables[0].Rows[0]["username"]).ToString();
                    var pass = (ds.Tables[0].Rows[0]["pass"]).ToString();
                    var id_user = Int32.Parse((ds.Tables[0].Rows[0]["id_klient"]).ToString());

                    Session["id_korisnik"] = id_user;
                    Session["korisnik"] = ime;
                    Session["prezime"] = prezime;
                    Session["email"] = email;
                    Session["user"] = username;
                    Session["pass"] = pass;
                }

                txtPime.Text = Session["korisnik"].ToString();
                txtPprezime.Text = Session["prezime"].ToString();
                txtPemail.Text = Session["email"].ToString();
                txtPuser.Text = Session["user"].ToString();
                txtPpass.Text = Session["pass"].ToString();
            }
            //baza.onlinebooking promeni = new baza.onlinebooking();
            //promeni.Promeni(txtPime.Text, txtPprezime.Text, txtPemail.Text, txtPuser.Text, 
            //                txtPpass.Text, Int32.Parse(Session["id_korisnik"].ToString()));

            var klient = new baza.onlinebooking();
            var ds1 = klient.pregled_karti_korisnik_profile(Int32.Parse(Session["id_korisnik"].ToString()));
            DataList1.DataSource = ds1;
            foreach (DataTable table in ds1.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var ci = new CultureInfo("mk-MK");
                    var dt = Convert.ToDateTime(dr["datum_prodazba"].ToString());


                    dr["datum_prodazba"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                }
            }
            DataList1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var promena = new baza.onlinebooking();

            //promena.Promeni(txtPime.Text, txtPprezime.Text, txtPemail.Text, txtPuser.Text,
            //      txtPpass.Text, Int32.Parse(Session["id_korisnik"].ToString()));
            var odgovor = promena.Promeni(txtPime.Text, txtPprezime.Text, txtPemail.Text, txtPuser.Text,
                    txtPpass.Text, Int32.Parse(Session["id_korisnik"].ToString()));
            lblpromena.Text = odgovor;
            if (lblpromena.Text == "OK")
            {
                lblpromena.Text = "Успешна промена на профилот.";
            }
            else
            {
                lblpromena.ForeColor = Color.Red;
                if ((lblpromena.Text == "NO") || (lblpromena.Text == "pole"))
                {
                    lblpromena.Text = "Се случи грешка, обиди се повторно.";
                }
            }
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {

            }
        }
    }
}