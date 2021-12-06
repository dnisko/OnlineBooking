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
    public partial class karti : System.Web.UI.Page
    {
        baza.onlinebooking b;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                b = new baza.onlinebooking();
                DataSet ds = b.Tabela_nastan();
                ddn1.DataSource = ds.Tables["nastani"];
                ddn1.DataTextField = "naziv";
                ddn1.DataValueField = "id_nastan";
                ddn1.DataBind();

                ddn2.DataSource = ds.Tables["nastani"];
                ddn2.DataTextField = "naziv";
                ddn2.DataValueField = "id_nastan";
                ddn2.DataBind();

                lblodgovor.Text = "";
            }
        }
        protected void btnEdna_Click(object sender, EventArgs e)
        {
            b = new baza.onlinebooking();
            int cena;
            if (int.TryParse(txtcena1.Text, out cena))
            {
                string odgovor = b.Vnesi_karta(ddn1.SelectedIndex, ddz.SelectedValue.ToString(),
                     int.Parse(ddz.SelectedValue.ToString()), int.Parse(ddm.SelectedValue.ToString()),
                     cena);
                if (odgovor == "OK")
                {
                    lblodgovor.ForeColor = Color.Green;
                    lblodgovor.Text = "Успешно ја внесовте картата";
                }
            }
            else
            {
                lblodgovor.ForeColor = Color.Red;
                lblodgovor.Text = "Внесете валидна цена.";
            }
        }
        protected void btnSite_Click(object sender, EventArgs e)
        {
            b = new baza.onlinebooking();
            //int cena;
            string odgovor;
            string zona = "Зона ";
            //bool textIsNumeric = true;
            try
            {
                int cena = Convert.ToInt32(txtcena2.Text);
                //}
                /*catch
                {
               //     textIsNumeric = false;
                }*/
                // if (textIsNumeric=true)
                //{
                for (int i = 1; i <= 5; i++)//ZONA
                {
                    for (int j = 1; j <= 5; j++)//RED
                    {
                        for (int z = 1; z <= 5; z++)//MESTO
                        {
                            odgovor = b.Vnesi_karta(ddn2.SelectedIndex, zona + i, j, z, cena);
                            if (odgovor == "OK")
                            {
                                lblodgovor.ForeColor = Color.Green;
                                lblodgovor.Text = "Успешно ги внесовте картите";
                            }
                            else
                            {
                                lblodgovor.ForeColor = Color.Red;
                                lblodgovor.Text += "Грешка!!!";
                            }
                        }
                    }
                }
            }
            catch
            {
                lblodgovor.ForeColor = Color.Red;
                lblodgovor.Text = "Внесете валидна цена.";
                //     textIsNumeric = false;
            }
            /*else
            {

            }*/
        }
    }
}