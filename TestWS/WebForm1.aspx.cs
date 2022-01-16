using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OBContext list = new OBContext();

            var nas = (from n in list.nastans//.ToList()

                      join ko in list.komintents//.ToList()
                      on n.fk_id_komintent equals ko.id_komintent

                      join k in list.kartis//.ToList()
                      on n.id_nastan equals k.n_id_nastan

                      join o in list.objekts//.ToList()
                      on n.o_id_objekt equals o.id_objekt
                      //orderby

                      //    k.n_id_nastan,
                      //    n.naziv,
                      //    ko.opis,
                      //    o.ime,
                      //    n.vreme,
                      //    o.grad,
                      //    n.data

                      select new
                      {
                          Naziv = n.naziv,
                          Opis = ko.opis,
                          Grad = o.grad,
                          Vreme = n.vreme
                      }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            //DataTable dt = nas.CopyToDataTable();
            //dataSet.Tables.Add(dt);
            ////Setup the table columns.
            //foreach (DataTable table in dataSet.Tables)
            //{
            //    foreach (DataRow row in table.Rows)
            //    {
            //        foreach (object item in row.ItemArray)
            //        {
            //            Label1.Text += "<br/>" + item.ToString();
            //            // read item
            //        }
            //    }
            //}
            DataTable dt = new DataTable();
            dt.Columns.Add("Grad");
            dt.Columns.Add("Naziv");
            dt.Columns.Add("Opis");
            dt.Columns.Add("Vreme");
            foreach (var lista in nas)
            {
                Label1.Text += "<br/>" + lista.ToString();
                //DataRow row = dataSet.Tables[0].NewRow();
                dt.Rows.Add(lista.Grad, lista.Naziv, lista.Opis, lista.Vreme);
                //row["Naziv"] = lista.Naziv;
                //row["Opis"] = lista.Opis;

                //row["Vreme"] = lista.Vreme;

                //dataSet.Tables[0].Rows.Add(row);
            }
            dataSet.Tables.Add(dt);
            //List<string> PL = new List<string>();
            //DataSet ds = new DataSet();

            //var list1 = nas.ToList();
            //PL.Add(list1.ToString());
            //Label1.Text = "pocinja";

            ////foreach (var o in PL)
            ////{
            ////    Label1.Text = "<br/>" + o.ToString();

            ////}
            ////Label1.Text += "<br/> foreach";
            ////DataTable dt = new DataTable();
            //ds.Tables.Add(new DataTable());
            ////DataRow row;
            //foreach (var lista in nas)
            //{

            //    DataRow row = ds.Tables[0].NewRow();
            //    row["Naziv"] = lista.Naziv;
            //    row["Opis"] = lista.Opis;
            //    row["Grad"] = lista.Grad;
            //    row["Vreme"] = lista.Vreme;
            //    ds.Tables[0].Rows.Add(row);
            //    //row = dt.NewRow();
            //    //row["Naziv"] = 
            //    //dt.Rows.Add(lista);
            //    //PL.Add(lista.ToString());
            //    Label1.Text += "<br/>"+ lista.ToString();
            //}
            ////Label1.Text = list1.ToString();
            ////ds = list1;
        }
    }
}