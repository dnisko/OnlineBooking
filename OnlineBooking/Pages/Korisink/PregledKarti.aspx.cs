using OnlineBooking.baza;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Int32;

namespace OnlineBooking.Pages.Korisink
{
    public partial class PregledKarti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        ddMesto.Visible = false;
        mesto.Visible = false;
        ddRed.Visible = false;
        red.Visible = false;
        lblCena.Visible = false;
        btnKosnicka.Visible = false;
        lblOdgovor.Visible = false;

        if (Request.QueryString["id"] != null)
        {
            if (!TryParse(Request.QueryString["id"], out var id))
                return;

            using (var service = new onlinebooking())
            {
                var ds = service.lista_na_karti_detalno_DS(id);

                lblNastan.Text = ds.Tables["nastani"].Rows[0]["naziv"].ToString();

                var ds1 = service.lista_na_zona(id);
                ddZona.DataSource = ds1.Tables["zona"];
                ddZona.DataTextField = "zona";
                //ddZona.DataValueField = "id_karti";
                ddZona.DataBind();
            }
        }
        else
        {
            Response.Redirect("~/Pages/Korisink/Pregled.aspx");
        }
    }
    protected void DdZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddZona.SelectedValue != "0")
        {
            lblOdgovor.Visible = false;
            ddMesto.Visible = true;
            mesto.Visible = true;
            if (!TryParse(Request.QueryString["id"], out var id))
                return;
            using (var service = new onlinebooking())
            {
                var zona = ddZona.SelectedValue;
                var ds = service.lista_na_mesto(id, zona);

                ddMesto.DataSource = ds.Tables["mesto"];
                ddMesto.DataTextField = "mesto";
                ddMesto.DataBind();
            }
        }
        else
        {
            lblOdgovor.Visible = true;
            lblOdgovor.ForeColor = Color.Red;
            lblOdgovor.Text = "Одберете зона од понудените";
        }
    }

    protected void DdMesto_SelectedIndexChanged(object sender, EventArgs e)
    {
        var mesto = Parse(ddMesto.SelectedValue);
        var zona = ddZona.SelectedValue;
        if (ddMesto.SelectedValue != "0")
        {
            lblOdgovor.Visible = false;
            ddRed.Visible = true;
            red.Visible = true;
            if (!TryParse(Request.QueryString["id"], out var id))
                return;

            using (var service = new onlinebooking())
            {
                var ds = service.lista_na_red(id, zona, mesto);

                ddRed.DataSource = ds.Tables["red"];
                ddRed.DataTextField = "red";
                ddRed.DataBind();
            }
        }
        else
        {
            lblOdgovor.Visible = true;
            lblOdgovor.ForeColor = Color.Red;
            lblOdgovor.Text = "Одберете место од понудените";
        }
    }


    protected void DdRed_SelectedIndexChanged(object sender, EventArgs e)
    {
        var zona = ddZona.SelectedValue;
        var mesto = Parse(ddMesto.SelectedValue);
        var red = Parse(ddRed.SelectedValue);

        if (ddZona.SelectedValue != "0")
        {
            lblOdgovor.Visible = false;
            lblCena.Visible = true;

            if (!TryParse(Request.QueryString["id"], out var id))
                return;
            using (var service = new onlinebooking())
            {
                var ds = service.lista_na_cena(id, zona, mesto, red);

                if (ds.Tables["cena"].Rows[0]["cena"] != null)
                {
                    lblCena.Text = "Цена&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                   + ds.Tables["cena"].Rows[0]["cena"].ToString() + " денари.";

                    btnKosnicka.Visible = true;
                }
                else
                {
                    lblCena.Text = "Цена&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                   + "нема цена за картата, почекајте да се наведе";
                }
            }
        }
        else
        {
            lblOdgovor.Visible = true;
            lblOdgovor.ForeColor = Color.Red;
            lblOdgovor.Text = "Одберете ред од понудените";
        }
    }
    protected void BtnOtkazi_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Korisink/Pregled.aspx");
    }
    protected void BtnKosnicka_Click(object sender, EventArgs e)
    {
        if (Session["korisnik"] == null)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), 
                "popup", 
                "alert('Морате да се најавите на системот !');" +
                "window.location='login.aspx';", true);
        }
        else
        {
            var red = Parse(ddRed.SelectedValue);
            var zona = ddZona.SelectedValue;
            var mesto = Parse(ddMesto.SelectedValue);
            using (var service = new onlinebooking())
            {
                if (!TryParse(Request.QueryString["id"], out var id))
                    return;

                var ds = service.lista_na_cena(id, zona, mesto, red);

                var karta = Parse(ds.Tables["cena"].Rows[0]["id_karti"].ToString());
                var klient = Parse(Session["id_korisnik"].ToString());
                var odgovor = service.vnesi_vo_kosnicka(karta, klient);

                switch (odgovor)
                {
                    case "OK":
                        lblOdgovor.Visible = true;
                        lblOdgovor.ForeColor = Color.Green;
                        lblOdgovor.Text = "Успешно ја додадовте картата во вашата кошничка!";
                        break;
                    case "IMA":
                        lblOdgovor.Visible = true;
                        lblOdgovor.ForeColor = Color.Red;
                        lblOdgovor.Text = "Картата е веќе во вашата кошничка.";
                        break;
                    default:
                        lblOdgovor.Visible = true;
                        lblOdgovor.ForeColor = Color.Red;
                        lblOdgovor.Text = "Се случи грешка, обидете се повторно.";
                        break;
                }
            }
        }
    }
    }
}