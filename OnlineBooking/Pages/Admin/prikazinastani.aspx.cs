using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Admin
{
    public partial class Prikazinastani : System.Web.UI.Page
    {
        baza.onlinebooking b;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["korisnik"] != null) || (Session["admin"] == null))
            {
                Response.Redirect("~/Pages/Korisnik/index.aspx");
            }
            if (!IsPostBack)
            {
                BindgvNastani();
            }
        }

        protected void BindgvNastani()
        {
            b = new baza.onlinebooking();
            DataSet ds = b.lista_na_nastani_DS();
            gvNastani.DataSource = ds;//.Tables["nastani"];

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    CultureInfo ci = new CultureInfo("mk-MK");
                    DateTime dt = Convert.ToDateTime(dr["vreme"].ToString());


                    dr["vreme"] = dt.ToString("dddd, dd-MMMM-yyyy", ci); ;
                }
            }
            gvNastani.DataBind();
        }
        protected void GvNastani_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNastani.EditIndex = e.NewEditIndex;
            BindgvNastani();
        }
        protected void GvNastani_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int nastanid = Convert.ToInt32(gvNastani.DataKeys[e.RowIndex].Value.ToString());
            string naziv_s = gvNastani.DataKeys[e.RowIndex].Values["naziv"].ToString();
            TextBox txtNastan = (TextBox)gvNastani.Rows[e.RowIndex].FindControl("txtNastan");
            DropDownList ddOpis = (DropDownList)gvNastani.Rows[e.RowIndex].FindControl("ddOpis");
            DropDownList ddIme = (DropDownList)gvNastani.Rows[e.RowIndex].FindControl("ddIme");
            TextBox txtVreme = (TextBox)gvNastani.Rows[e.RowIndex].FindControl("txtVreme");

            baza.onlinebooking promeni_nastan = new baza.onlinebooking();

            if ((ddOpis.SelectedIndex > 0) && (ddIme.SelectedIndex > 0))
            {
                lblresult.ForeColor = Color.Green;
                lblresult.Text = promeni_nastan.Promeni_nastan
                                (ddOpis.SelectedIndex, ddIme.SelectedIndex, txtNastan.Text, txtVreme.Text, nastanid);

                if (lblresult.Text == "Успешна промена")
                {
                    BindgvNastani();
                    string naziv_n = gvNastani.DataKeys[e.RowIndex].Values["naziv"].ToString();
                    if (naziv_n != naziv_s)
                    {
                        lblresult.Text += " на настанот " + naziv_s + " во настан со назив: " + naziv_n;
                    }
                    else
                    {
                        lblresult.Text += " на настанот " + naziv_s;
                    }

                    gvNastani.EditIndex = -1;
                    BindgvNastani();
                }
            }
            else
            {
                lblresult.ForeColor = Color.Red;
                lblresult.Text = "Одбери опис и место";
            }
        }
        protected void GvNastani_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNastani.EditIndex = -1;
            BindgvNastani();
        }
        protected void GvNastani_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int nastanid = Convert.ToInt32(gvNastani.DataKeys[e.RowIndex].Values["id_nastan"].ToString());
            string naziv = gvNastani.DataKeys[e.RowIndex].Values["naziv"].ToString();

            baza.onlinebooking brisi = new baza.onlinebooking();
            lblresult.Text = brisi.brisi_nastan(nastanid);

            if (lblresult.Text == "OK")
            {
                BindgvNastani();
                lblresult.ForeColor = Color.Green;
                lblresult.Text = "Успешно го избришавте настанот со назив " + naziv;
            }
            else
            {
                lblresult.ForeColor = Color.Red;
                lblresult.Text = "ГРЕШКА!!! Настанот не е избришан, обиди се повторно";
            }
        }
        protected void GvNastani_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            if ((row.RowType == DataControlRowType.DataRow) && ((row.RowState & DataControlRowState.Edit) > 0))
            {
                DropDownList ddOpis = row.FindControl("ddOpis") as DropDownList;
                if (ddOpis != null)
                {
                    baza.onlinebooking opis = new baza.onlinebooking();
                    DataSet dsopis = opis.Opis_DS();
                    ddOpis.DataSource = dsopis.Tables["opis"];

                    ddOpis.DataTextField = "opis";
                    ddOpis.DataValueField = "id_komintent";
                    ddOpis.DataBind();
                    ddOpis.Items.Insert(0, new ListItem("--Select--", "0"));
                }

                DropDownList ddIme = row.FindControl("ddIme") as DropDownList;
                if (ddIme != null)
                {
                    baza.onlinebooking ime = new baza.onlinebooking();
                    DataSet dsime = ime.Objekt_DS();
                    ddIme.DataSource = dsime.Tables["objekt"];

                    ddIme.DataTextField = "ime";
                    ddIme.DataValueField = "id_objekt";
                    ddIme.DataBind();
                    ddIme.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
        }
    }
}