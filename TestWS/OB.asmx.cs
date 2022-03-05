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
        public List<nastan> Tabela_nastan()
        {
            using (OBContext oBContext = new OBContext())
            {
                var list = from nastan in oBContext.nastans select nastan;
                return list.ToList();
            }
        }
        //public class MyGrouping
        //{
        //    int n_id_nastan { get; set; }
        //    DateTime data { get; set; }
        //    string naziv { get; set; }
        //    string ime { get; set; }
        //    string grad { get; set; }
        //    DateTime vreme { get; set; }
        //}

        [WebMethod]
        public DataSet lista_na_karti_DS()
        {
            context = new OBContext();

            var nas = (from nastan in context.nastans

                       join komintent in context.komintents
                       on nastan.fk_id_komintent equals komintent.id_komintent

                       join objekt in context.objekts
                       on nastan.o_id_objekt equals objekt.id_objekt

                       join karti in context.kartis
                       on nastan.id_nastan equals karti.n_id_nastan into eKarti

                       //group karti by new MyGrouping(
                       //{
                       //    karti.n_id_nastan,
                       //    nastan.data,
                       //    nastan.naziv,
                       //    komintent.opis,
                       //    objekt.ime,
                       //    objekt.grad,
                       //    nastan.vreme,
                       //    //karti.lager

                       //}) into g
                       select new
                       {
                           //n_id_nastan = eKarti.Select(x => x.n_id_nastan),
                           Data = nastan.data,
                           Naziv = nastan.naziv,
                           Opis = komintent.opis,
                           Ime = objekt.ime,
                           Grad = objekt.grad,
                           Vreme = nastan.vreme,
                           Lager = eKarti.Sum(x => x.lager)
                       });//.Distinct();
            //string SQL = "select n_id_nastan, data, naziv, opis, ime, grad, vreme, sum(lager) as lager";
            //SQL += " from karti, nastan, objekt, komintent where";
            //SQL += " id_nastan = n_id_nastan and id_objekt = o_id_objekt and id_komintent = fk_id_komintent";
            //SQL += " and lager is not null";
            //SQL += " group by n_id_nastan, naziv, opis, ime, vreme, grad, data";
            //SQL += " order by vreme desc";
            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = new DataTable();

            dt = LINQToDataTable(nas);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet vkupno(int id)
        {
            context = new OBContext();

            var query = from kosnicka in context.kosnickas //into eKarti
                        
                        join klient in context.klients
                        on kosnicka.fk_id_klient equals klient.id_klient
                        
                        join karti in context.kartis
                        on kosnicka.fk_id_karti equals karti.id_karti

                        where klient.id_klient == id

                        select karti.cena;

            var result = (from nastan in context.nastans
                         join karti in context.kartis
                         on nastan.id_nastan equals karti.n_id_nastan
                         
                         from klient in context.klients
                         //join karti in context.kartis
                         //on nastan.id_nastan equals karti.n_id_nastan

                         //join kosnicka in context.kosnickas
                         //on karti.id_karti equals kosnicka.fk_id_karti

                         //join klient in context.klients
                         //on kosnicka.fk_id_klient equals klient.id_klient

                         where klient.id_klient == id

                         select new
                         {
                             Username = klient.username,
                             Vkupno = query.Sum()
                         }).Distinct();

            string sql = "select distinct username, SUM(cena) as vkupno from klient, karti, kosnicka, nastan";
            sql += " where id_karti=k_id_karti and id_klient=k_id_klient";
            sql += " and id_nastan=n_id_nastan and id_klient = ?";
            sql += " group by username";

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = new DataTable();

            dt = LINQToDataTable(result);
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
