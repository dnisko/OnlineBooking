using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Admin
{
    public partial class korisnici : System.Web.UI.Page
    {
        baza.onlinebooking b;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["korisnik"] != null) || (Session["admin"] == null))
            {
                Response.Redirect("~/Pages/Korisnik/index.aspx");
            }
            lblpregled.Visible = false;
            Kpanel.Visible = false;
            Npanel.Visible = false;
            if (Request.QueryString["korisnik"] != null)
            {
                DataList2.Visible = false;

                Kpanel.Visible = true;
                Npanel.Visible = false;

                lblpregled.Visible = true;
                lblpregled.Text = "Купени карти од корисници";
            }
            if (Request.QueryString["nastan"] != null)
            {
                DataList1.Visible = false;

                Npanel.Visible = true;
                Kpanel.Visible = false;

                lblpregled.Visible = true;
                lblpregled.Text = "Купени карти за настани";
            }
            lblErr.Visible = false;
            if (!IsPostBack)
            {
                b = new baza.onlinebooking();
                DataSet ds = b.pregled_karti_korisnici();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    DataList1.DataSource = ds;
                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            CultureInfo ci = new CultureInfo("mk-MK");
                            DateTime dt = Convert.ToDateTime(dr["datum_prodazba"].ToString());


                            dr["datum_prodazba"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                        }
                        DataList1.DataBind();
                    }
                }

            }
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            b = new baza.onlinebooking();
            DataSet ds1 = b.lista_na_korisnici_po_username_DS(txtKorisnik.Text);
            if (ds1.Tables[0].Rows.Count != 0)
            {
                string user = txtKorisnik.Text;
                if (user != null)
                {
                    b = new baza.onlinebooking();
                    DataSet ds = b.pregled_karti_korisnik(user);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataList1.DataSource = ds;
                        foreach (DataTable table in ds.Tables)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                CultureInfo ci = new CultureInfo("mk-MK");
                                DateTime dt = Convert.ToDateTime(dr["datum_prodazba"].ToString());

                                dr["datum_prodazba"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                            }
                            DataList1.DataBind();
                        }
                    }
                }

            }
            else
            {
                lblErr.Visible = true;
                lblErr.Text = "Корисникот не постои.";
            }
        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            b = new baza.onlinebooking();
            DataSet ds = b.pregled_karti_korisnici();
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataList1.DataSource = ds;
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        CultureInfo ci = new CultureInfo("mk-MK");
                        DateTime dt = Convert.ToDateTime(dr["datum_prodazba"].ToString());


                        dr["datum_prodazba"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                    }
                    DataList1.DataBind();
                }
            }
        }
        protected void Button11_Click(object sender, EventArgs e)
        {
            b = new baza.onlinebooking();
            DataSet ds = b.lista_na_kupeni_karti_za_nastani_po_naziv(txtNastan.Text);

            DataList2.DataSource = ds;
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    CultureInfo ci = new CultureInfo("mk-MK");
                    DateTime dt = Convert.ToDateTime(dr["datum_prodazba"].ToString());


                    dr["datum_prodazba"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                }
                DataList2.DataBind();
            }
            DataList2.Visible = true;
        }
        protected void Button12_Click(object sender, EventArgs e)
        {
            b = new baza.onlinebooking();
            DataSet ds = b.lista_na_kupeni_karti_za_nastani();

            DataList2.DataSource = ds;
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    CultureInfo ci = new CultureInfo("mk-MK");
                    DateTime dt = Convert.ToDateTime(dr["datum_prodazba"].ToString());


                    dr["datum_prodazba"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                }
                DataList2.DataBind();
            }
            DataList2.Visible = true;
        }
    }
}