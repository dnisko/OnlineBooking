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
    public partial class NaplataUspesno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int klient = Int32.Parse(Session["id_korisnik"].ToString());

            //This code write only for getting data from datatable on the basis of id.
            //and pass it to th Generatereport method.
            //Here my data source is my datatable but you can also use other datasource.
            //string barCode=null;
            //string odgovor = null;

            if (!int.TryParse(Request.QueryString["karta"], out var karta) ||
                (!int.TryParse(Request.QueryString["klient"], out var klient)) ||
                (Request.QueryString["bar"] == null)) return;
            var service = new baza.onlinebooking();
            service.pregled_kupena_karta(klient, karta);

            var barCode = Request.QueryString["bar"];
            var imgBarCode = new System.Web.UI.WebControls.Image();
            using (var bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (var graphics = Graphics.FromImage(bitMap))
                {
                    var oFont = new Font("IDAutomationHC39M", 16);
                    var point = new PointF(2f, 2f);
                    var blackBrush = new SolidBrush(Color.Black);
                    var whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }

                using (var ms = new MemoryStream())
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    var byteImage = ms.ToArray();

                    var base64String = Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + base64String;
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
}