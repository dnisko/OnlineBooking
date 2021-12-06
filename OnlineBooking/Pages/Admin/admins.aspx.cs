using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBooking.Pages.Admin
{
    public partial class admins : System.Web.UI.Page
    {
        baza.onlinebooking b;
        protected int vkupno = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblresult.Visible = false;

                BindGridView();
            }
        }
        protected void BindGridView()
        {
            b = new baza.onlinebooking();
            DataSet ds = b.lista_na_korisnici_DS();
            GridView1.DataSource = ds.Tables["klienti1"];
            GridView1.DataBind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.Cells[4].Text.ToString() == "admin")
            {
                e.Row.Cells[4].Visible = false;
            }
            if (e.Row.Cells[4].Text.ToString() == "0")
            {
                e.Row.Visible = false;
            }
            else if (e.Row.Cells[4].Text.ToString() == "1")
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Visible = true;
                vkupno = vkupno + 1;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblVkupno");
                lbl.Text = vkupno.ToString();
            }

        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblresult.Visible = true;
            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["id_klient"].ToString());
            string username = GridView1.DataKeys[e.RowIndex].Values["username"].ToString();

            baza.onlinebooking brisi = new baza.onlinebooking();
            lblresult.Text = brisi.brisi_klient(userid);

            if (lblresult.Text == "OK")
            {
                BindGridView();
                lblresult.ForeColor = Color.Green;
                lblresult.Text = "Успешно го избришавте администраторот со username " + username;
            }
            else
            {
                lblresult.ForeColor = Color.Red;
                lblresult.Text = "ГРЕШКА!!! Администраторот не е избришан, обиди се повторно";
            }
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            b = new baza.onlinebooking();
            string odgovor;
            odgovor = b.Vnesi_admin(txtUser.Text, txtPass.Text, txtIme.Text, txtPrezime.Text, txtEmail.Text);
            if (odgovor == "OK")
            {
                lblodgovor.Visible = true;
                lblodgovor.ForeColor = Color.Green;
                lblodgovor.Text = "Успешно внесовте нов администратор.";
            }
            else if (odgovor == "user")
            {
                lblodgovor.Visible = true;
                lblodgovor.ForeColor = Color.Red;
                lblodgovor.Text = "Usernam-от е зафатен, одберете друг!";
            }
            else if (odgovor == "email")
            {
                lblodgovor.Visible = true;
                lblodgovor.ForeColor = Color.Red;
                lblodgovor.Text = "Email-от е зафатен, одберете друг!";
            }
            else if (odgovor == "user&email")
            {
                lblodgovor.Visible = true;
                lblodgovor.ForeColor = Color.Red;
                lblodgovor.Text = "Email-от и usernam-от се зафатени, одберете други!";
            }
        }
    }
}