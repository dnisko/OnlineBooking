using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OnlineBooking.Pages.Korisink
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //int klient = Int32.Parse(Session["id_korisnik"].ToString());

        //This code write only for getting data from datatable on the basis of id.
        //and pass it to th Generatereport method.
        //Here my data source is my datatable but you can also use other datasource.
        int karta = 0;
        int klient = 0;
        //string barCode=null;
        //string odgovor = null;
        if ((Int32.TryParse(Request.QueryString["karta"], out karta))
            && (Int32.TryParse(Request.QueryString["klient"], out klient))
            && (Request.QueryString["bar"] != null))
        {
            baza.onlinebooking service = new baza.onlinebooking();
            DataSet ds = service.pregled_kupena_karta(klient, karta);

            string barCode = Request.QueryString["bar"];
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                plBarCode.Controls.Add(imgBarCode);
            }
            /*ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");
            kupena_karta dsCustomers = GetData("select top 20 * from customers");
            ReportDataSource datasource = new ReportDataSource("Customers", dsCustomers.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);*/
        }
    }

    /*private kupena_karta GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;

                sda.SelectCommand = cmd;
                using (Customers dsCustomers = new Customers())
                {
                    sda.Fill(dsCustomers, "DataTable1");
                    return dsCustomers;
                }
            }
        }
    }
    private void Generatereport(DataTable dt)
    {
        ReportViewer1.SizeToReportContent = true;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportDataSource _rsource = new ReportDataSource("DataSet1", dt);
        ReportViewer1.LocalReport.DataSources.Add(_rsource);
        ReportViewer1.LocalReport.Refresh();

    }*/
}