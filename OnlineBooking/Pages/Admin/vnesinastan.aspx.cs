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
    public partial class vnesinastan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["korisnik"] != null) || (Session["admin"] == null))
            {
                Response.Redirect("~/index.aspx");
            }

            lblcal.Visible = false;
            if (!IsPostBack)
            {
                lblSlikaOdg.Visible = false;
                lblSlika.Visible = false;
                ddCas.DataSource = GetTimeIntervals();
                ddCas.DataBind();
                ddCas.SelectedIndex = 24;
                lblOdgovor.Text = "";

                baza.onlinebooking opis = new baza.onlinebooking();

                DataSet DSopis = opis.Opis_DS();
                ddOpis.DataSource = DSopis.Tables["opis"];
                ddOpis.DataTextField = "opis";
                ddOpis.DataValueField = "id_komintent";
                ddOpis.DataBind();

                baza.onlinebooking objekt = new baza.onlinebooking();

                DataSet DSobjekt = objekt.Objekt_DS();
                ddObjekt.DataSource = DSobjekt.Tables["objekt"];
                ddObjekt.DataTextField = "ime";
                ddObjekt.DataValueField = "id_objekt";
                ddObjekt.DataBind();
            }
        }
        public List<string> GetTimeIntervals()
        {
            List<string> timeIntervals = new List<string>();
            TimeSpan startTime = new TimeSpan(0, 0, 0);
            DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
            for (int i = 0; i < 48; i++)
            {
                int minutesToBeAdded = 30 * i;      // Increasing minutes by 30 minutes interval
                TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
                TimeSpan t = startTime.Add(timeToBeAdded);
                DateTime result = startDate + t;
                timeIntervals.Add(result.ToShortTimeString());      // Use Date.ToShortTimeString() method to get the desired format                
            }
            return timeIntervals;
        }
        protected void Button11_Click(object sender, EventArgs e)
        {
            baza.onlinebooking opis = new baza.onlinebooking();

            string odgovor = opis.vnesi_opis(txtVopis.Text);
            lblOdgovor.Text = odgovor;
        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            baza.onlinebooking objekt = new baza.onlinebooking();

            string odgovor = objekt.vnesi_objekt(txtVobjekt.Text, txtVgrad.Text);
            lblOdgovor.Text = odgovor;
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            lblSlika.Text = "";
            string cas = ddCas.SelectedValue;
            if ((txtNaziv.Text != null) && (ddOpis.SelectedValue.Length > 0) && (ddObjekt.SelectedValue.Length > 0))
            {
                baza.onlinebooking nastan = new baza.onlinebooking();


                byte[] file = FileUpload1.FileBytes;
                baza.onlinebooking serv = new baza.onlinebooking();
                lblSlikaOdg.Text = serv.UploadFile(file, FileUpload1.FileName);
                //serv.UploadFile(file, FileUpload1.FileName);
                if (lblSlikaOdg.Text == "OK")
                {
                    lblSlikaOdg.Text = "Успешно закачивте слика.";
                    lblSlika.Text = FileUpload1.FileName;//lblSlika.Text = "Успешно закачивте слика";
                }
                else
                {
                    lblSlikaOdg.Text = "Се случи грешка, обидете се повторно";
                }
                Label16.Text = cas;
                string odgovor = nastan.vnesi_nastan(txtNaziv.Text, ddOpis.SelectedIndex,
                    ddObjekt.SelectedIndex, FileUpload1.FileName,
                    Calendar1.SelectedDate.ToString("yyyy/M/d"), cas);//"yyyy/MM/dd"));

                if (odgovor == "OK")
                {
                    lblOdgovor.Text = "Успешно внесовте настан";
                }
                else
                {
                    lblOdgovor.Text = "Се случи грешка, обиди се повторно";
                }
            }
            else
            {
                lblOdgovor.Text = "Полињата настан, опис и објект се задолжителни.";
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            lblSlika.Text = ddCas.SelectedValue;
            lblcal.Visible = true;
            CultureInfo ci = new CultureInfo("mk-MK");
            //lblcal.Text = "Одбрана дата: " + Calendar1.SelectedDate.ToString("dddd, dd/MMM/yyyy", ci);//ToShortDateString("dd/MM/yyyy");

            /*
    ##########################################################################################
             baza.onlinebooking data = new baza.onlinebooking();
            DataSet ds = data.data_DS();
            string data1 = ds.Tables[0].Rows[0]["vreme"].ToString();
            DateTime dt = Convert.ToDateTime(data1);

            lblcal.Text = dt.ToString("dddd, dd/MMM/yyyy", ci);
    #########################################################################################
             */

        }
    }
}