using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;

namespace TestWS
{
    /// <summary>
    /// Summary description for OB
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OB : System.Web.Services.WebService
    {
        OBContext context = new OBContext();
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                //will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        [WebMethod]
        public void HelloWorld()
        {
            HttpContext.Current.Response.Write ("Hello World");
        }

        [WebMethod]
        public List<nastan> Test1()
        {
            using (OBContext oBContext = new OBContext())
            {
                var list = from nastan in oBContext.nastans select nastan;
                return list.ToList();
            }
        }

        [WebMethod]
        public DataSet Tabela_nastan()
        {
            context = new OBContext();

            var nas = (from nastan in context.nastans

                       join komintent in context.komintents
                       on nastan.fk_id_komintent equals komintent.id_komintent

                       join objekt in context.objekts
                       on nastan.o_id_objekt equals objekt.id_objekt

                       select new
                       {
                           Naziv = nastan.naziv,
                           Opis = komintent.opis,
                           Grad = objekt.grad,
                           Vreme = nastan.vreme
                       }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = new DataTable();

            dt = LINQToDataTable(nas);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod(Description = "Барај по корисничко име", MessageName ="test")]
        public DataSet lista_na_korisnici_po_username_DS(string username)
        {
            context = new OBContext();
            var nas = from klient in context.klients
                      where klient.username.Contains(username)
                      select new { Ime = klient.ime, Username = klient.username };

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(nas);
            dataSet.Tables.Add(dt);
            return dataSet;
        }
    }
}
