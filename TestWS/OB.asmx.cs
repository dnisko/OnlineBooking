using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using TestWS.Classes;

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
        readonly string Konekcija = "Provider=SQLOLEDB;Data Source=VIKI-LAPTOP\\SQLEXPRESS;Initial Catalog=onlinebooking;Trusted_Connection=Yes;";// - OleDb

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
        public string UploadFile(byte[] f, string fileName)
        {
            try
            {
                // instance a memory stream and pass the 
                // byte array to its constructor 
                MemoryStream ms = new MemoryStream(f);
                // instance a filestream pointing to the 
                // storage folder, use the original file name 
                // to name the resulting file 
                FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath
                            ("~/sliki/") + fileName, FileMode.Create);

                // write the memory stream containing the original 
                // file as a byte array to the filestream 
                ms.WriteTo(fs);
                // clean up 
                ms.Close();
                fs.Close();
                fs.Dispose();
                // return OK if we made it this far 
                return "OK";
            }
            catch (Exception ex)
            {
                // return the error message if the operation fails 
                return ex.Message.ToString();
            }
        }

        [WebMethod, Description("Get image content")]
        public byte[] GetImageFile(string fileName)
        {
            if (System.IO.File.Exists
            (System.Web.Hosting.HostingEnvironment.MapPath
                ("~/sliki/") + fileName))
            {
                return System.IO.File.ReadAllBytes(
                  System.Web.Hosting.HostingEnvironment.MapPath
                    ("~/sliki/") + fileName);
            }
            else
            {
                return new byte[] { 0 };
            }
        }


        //###################### SELECT ###########################
        [WebMethod]
        public DataSet Login(string username, string pass)
        {
            context = new OBContext();

            var query = from user in context.klients
                        where user.username == username &&
                              user.pass == pass
                        select user;

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;

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

        [WebMethod]
        public List<nastan> Test(int id, string name)
        {
            using (OBContext oBContext = new OBContext())
            {
                var list = from nastan in oBContext.nastans
                           where nastan.id_nastan == id ||
                                 nastan.naziv == name
                           select nastan;
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
        public DataSet Lista_na_karti_DS()
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
        public DataSet Vkupno(int id)
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

        [WebMethod(Description = "Барај по корисничко име", MessageName = "test")]
        public DataSet Lista_na_korisnici_po_username_DS(string username)
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

        [WebMethod]
        public DataSet Kosnicka(int id)
        {
            context = new OBContext();

            var query = from nastan in context.nastans
                        join karti in context.kartis
                        on nastan.id_nastan equals karti.n_id_nastan

                        join kosnicka in context.kosnickas
                        on karti.id_karti equals kosnicka.fk_id_karti

                        join klient in context.klients
                        on kosnicka.fk_id_klient equals klient.id_klient

                        where klient.id_klient == id

                        select new { nastan, karti, kosnicka, klient };

            //var query = (from nastan in context.nastans
            //              join karti in context.kartis
            //              on nastan.id_nastan equals karti.n_id_nastan

            //              from klient in context.klients

            //              where klient.id_klient == id

            //              select new
            //              {
            //                  Username = klient.username,
            //                  Vkupno = query1.Sum()
            //              }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Pregled_karti_korisnik_profile(int id)
        {
            context = new OBContext();

            var query = (from nastan in context.nastans
                         join karti in context.kartis
                         on nastan.id_nastan equals karti.n_id_nastan

                         join prodazba in context.prodazbas
                         on karti.id_karti equals prodazba.id_karti

                         join klient in context.klients
                         on prodazba.id_klient equals klient.id_klient

                         where klient.id_klient == id

                         select new
                         {
                             nastan,
                             karti,
                             prodazba,
                             klient
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;

            //string sql = "select distinct * from prodazba, karti, klient, nastan ";
            //sql += "where karti.id_karti=prodazba.id_karti and klient.id_klient=prodazba.id_klient and id_nastan=n_id_nastan ";
            //sql += "and klient.id_klient = ?";// like '%' + ? + '%'";
        }

        [WebMethod]
        public DataSet Pregled_karti_korisnik(string user)
        {
            context = new OBContext();

            var query = (from nastan in context.nastans
                         join karti in context.kartis
                         on nastan.id_nastan equals karti.n_id_nastan

                         join prodazba in context.prodazbas
                         on karti.id_karti equals prodazba.id_karti

                         join klient in context.klients
                         on prodazba.id_klient equals klient.id_klient

                         where klient.username == user

                         select new
                         {
                             nastan,
                             karti,
                             prodazba,
                             klient
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Pregled_karti_korisnici()
        {
            context = new OBContext();

            var query = (from nastan in context.nastans
                         join karti in context.kartis
                         on nastan.id_nastan equals karti.n_id_nastan

                         join prodazba in context.prodazbas
                         on karti.id_karti equals prodazba.id_karti

                         join klient in context.klients
                         on prodazba.id_klient equals klient.id_klient

                         select new
                         {
                             nastan,
                             karti,
                             prodazba,
                             klient
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Pregled_kupena_karta(int id_kli, int id_kar)
        {
            context = new OBContext();

            var query = (from nastan in context.nastans
                         join karti in context.kartis
                         on nastan.id_nastan equals karti.n_id_nastan

                         join prodazba in context.prodazbas
                         on karti.id_karti equals prodazba.id_karti

                         join klient in context.klients
                         on prodazba.id_klient equals klient.id_klient

                         where klient.id_klient == id_kli && karti.id_karti == id_kar

                         select new
                         {
                             nastan,
                             karti,
                             prodazba,
                             klient
                         }).Distinct();


            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Opis_DS()
        {
            context = new OBContext();

            var query = from komintent in context.komintents
                        select komintent;

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Objekt_DS()
        {
            context = new OBContext();

            var query = from objekt in context.objekts
                        select objekt;

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        //[WebMethod]
        //public DataSet lista_na_korisnici_po_username_DS(string username)
        //{
        //    context = new OBContext();

        //    var query = from klient in context.klients
        //                where klient.username.Contains(username)
        //                select klient;


        //    DataSet dataSet = new DataSet("myDataSet");
        //    DataTable dt = LINQToDataTable(query);
        //    dataSet.Tables.Add(dt);
        //    return dataSet;
        //}

        [WebMethod]
        public DataSet Lista_na_korisnici_po_id_DS(int id)
        {
            context = new OBContext();

            var query = from klient in context.klients
                        where klient.id_klient == id
                        select klient;

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_korisnici_DS()
        {
            context = new OBContext();

            //var query = from klient in context.klients
            //            select klient;

            var query = context.klients.Select(x => x);

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_admini_DS()
        {
            context = new OBContext();

            var query = from klient in context.klients
                        where klient.isadmin
                        select klient;

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_top5_nastani_DS()
        {
            context = new OBContext();

            var query = (from nastan in context.nastans
                         select nastan).Take(5);

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_nastani_za_nastan()
        {
            context = new OBContext();

            var query = from nastan in context.nastans
                        orderby nastan.vreme ascending
                        select nastan;

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_zona(int id)
        {
            context = new OBContext();

            var query = (from karti in context.kartis
                         where karti.n_id_nastan == id
                         select karti.zona).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_mesto(int id, string zona)
        {
            context = new OBContext();

            var query = (from karti in context.kartis
                         where karti.n_id_nastan == id &&
                               karti.zona == zona
                         select new
                         {
                             karti.zona,
                             karti.mesto
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_red(int id, string zona, string mesto)
        {
            context = new OBContext();

            var query = (from karti in context.kartis
                         where karti.n_id_nastan == id &&
                               karti.zona == zona &&
                               karti.mesto == mesto
                         select new
                         {
                             karti.zona,
                             karti.mesto,
                             karti.red
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_cena(int id, string zona, string mesto, string red)
        {
            context = new OBContext();

            var query = (from karti in context.kartis
                         where karti.n_id_nastan == id &&
                               karti.zona == zona &&
                               karti.mesto == mesto &&
                               karti.red == red
                         select new
                         {
                             karti.id_karti,
                             karti.cena
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_nastani_DS()
        {
            context = new OBContext();

            var query = from objekt in context.objekts
                        join nastan in context.nastans
                        on objekt.id_objekt equals nastan.o_id_objekt

                        join komintent in context.komintents
                        on nastan.fk_id_komintent equals komintent.id_komintent

                        orderby nastan.vreme descending

                        select new
                        {
                            nastan,
                            objekt,
                            komintent
                        };

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_kupeni_karti_za_nastani_po_naziv(string naziv)
        {
            context = new OBContext();
            var query = (from objekt in context.objekts
                         join nastan in context.nastans
                         on objekt.id_objekt equals nastan.o_id_objekt

                         join komintent in context.komintents
                         on nastan.o_id_objekt equals komintent.id_komintent

                         join karti in context.kartis
                         on nastan.id_nastan equals karti.n_id_nastan

                         join prodazba in context.prodazbas
                         on karti.id_karti equals prodazba.id_karti

                         join klient in context.klients
                         on prodazba.id_klient equals klient.id_klient

                         where nastan.naziv.Contains(naziv)

                         select new
                         {
                             nastan,
                             klient,
                             objekt,
                             komintent,
                             karti,
                             prodazba
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public DataSet Lista_na_kupeni_karti_za_nastani()
        {
            context = new OBContext();
            var query = (from objekt in context.objekts
                         join nastan in context.nastans
                         on objekt.id_objekt equals nastan.o_id_objekt

                         join komintent in context.komintents
                         on nastan.o_id_objekt equals komintent.id_komintent

                         join karti in context.kartis
                         on nastan.id_nastan equals karti.n_id_nastan

                         join prodazba in context.prodazbas
                         on karti.id_karti equals prodazba.id_karti

                         join klient in context.klients
                         on prodazba.id_klient equals klient.id_klient

                         select new
                         {
                             nastan,
                             klient,
                             objekt,
                             komintent,
                             karti,
                             prodazba
                         }).Distinct();

            DataSet dataSet = new DataSet("myDataSet");
            DataTable dt = LINQToDataTable(query);
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        [WebMethod]
        public Nastan NastanInfo(int id)
        {
            Nastan ns = null;
            context = new OBContext();
            ns = new Nastan();
            var query = from nastan in context.nastans
                        where nastan.id_nastan == id
                        select nastan;
            //{

            //ID = ns.Id == nastan.id_nastan,
            //naziv = ns.Naziv == nastan.naziv,
            //vreme = ns.Vreme == nastan.vreme,
            //slika = ns.Slika == nastan.slika,
            //cas = ns.Cas == nastan.cas,
            //kratok_opis = ns.Kopis == nastan.kratok_opis,
            //sirok_opis = ns.Sopis == nastan.sirok_opis,
            //sajt = ns.Sajt == nastan.sajt,
            //video = ns.Video == nastan.video

            //ID = nastan.id_nastan,
            //naziv = nastan.naziv,
            //vreme = nastan.vreme,
            //slika = nastan.slika,
            //cas = nastan.cas,
            //kratok_opis = nastan.kratok_opis,
            //sirok_opis = nastan.sirok_opis,
            //sajt = nastan.sajt,
            //video = nastan.video
            //};
            ns.Id = (from q in query select q.id_nastan).First();
            ns.Naziv = (from q in query select q.naziv).First();
            ns.Vreme = (from q in query select q.vreme).First();
            ns.Slika = (from q in query select q.slika).First();
            ns.Cas = (from q in query select q.cas).First();
            ns.Kopis = (from q in query select q.kratok_opis).First();
            ns.Sopis = (from q in query select q.sirok_opis).First();
            ns.Sajt = (from q in query select q.sajt).First();
            ns.Video = (from q in query select q.video).First();
            return ns;
        }
        [WebMethod]
        public Nastan NastanInfoStar(int id)
        {
            Nastan ns = null;
            //context = new OBContext();
            string sql;
            //ns = new Nastan();
            //var query = from nastan in context.nastans
            //            where nastan.id_nastan == id
            //            select new
            //            {

            //                //ID = ns.Id == nastan.id_nastan,
            //                //naziv = ns.Naziv == nastan.naziv,
            //                //vreme = ns.Vreme == nastan.vreme,
            //                //slika = ns.Slika == nastan.slika,
            //                //cas = ns.Cas == nastan.cas,
            //                //kratok_opis = ns.Kopis == nastan.kratok_opis,
            //                //sirok_opis = ns.Sopis == nastan.sirok_opis,
            //                //sajt = ns.Sajt == nastan.sajt,
            //                //video = ns.Video == nastan.video

            //                ID = nastan.id_nastan,
            //                naziv = nastan.naziv,
            //                vreme = nastan.vreme,
            //                slika = nastan.slika,
            //                cas = nastan.cas,
            //                kratok_opis = nastan.kratok_opis,
            //                sirok_opis = nastan.sirok_opis,
            //                sajt = nastan.sajt,
            //                video = nastan.video
            //            };
            ////ns = new Nastan();
            //ns.Id = Int32.Parse(query.Select(x => x.ID).ToString());
            //ns.Naziv = query.Select(x => x.naziv).ToString();
            //ns.Slika = query.Select(x => x.slika).ToString();
            //ns.Cas = query.Select(x => x.cas).ToString();
            //ns.Kopis = query.Select(x => x.kratok_opis).ToString();
            //ns.Sopis = query.Select(x => x.sirok_opis).ToString();
            //ns.Sajt = query.Select(x => x.sajt).ToString();
            //ns.Video = query.Select(x => x.video).ToString();
            //return ns;
            using (OleDbConnection CNN = new OleDbConnection(Konekcija))
            {
                sql = "select id_nastan, naziv, vreme, slika, cas, kratok_opis, sirok_opis, sajt, video";
                sql += " from nastan where id_nastan={0}";
                OleDbCommand komanda = new OleDbCommand(string.Format(sql, id), CNN);
                CNN.Open();
                OleDbDataReader reader = komanda.ExecuteReader();
                if (reader.Read())
                {
                    ns = new Nastan();
                    ns.Id = reader.GetInt32(0);
                    ns.Naziv = reader.GetString(1);
                    ns.Vreme = reader.GetString(2);
                    ns.Slika = reader.GetString(3);
                    ns.Cas = reader.GetString(4);
                    ns.Kopis = reader.GetString(5);
                    ns.Sopis = reader.GetString(6);
                    ns.Sajt = reader.GetString(7);
                    ns.Video = reader.GetString(8);
                }
            }
            return ns;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetCustomers(string prefix)
        {
            List<string> customers = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(Konekcija))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.CommandText = "select * from klient where " +
                    "ime like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}", sdr["ime"], sdr["id_klient"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }

        //###################### UPDATE ###########################

        [WebMethod]
        public string Napravi_admin(int id)
        {
            context = new OBContext();

            var result = context.klients.Where(x => x.id_klient == id).FirstOrDefault();
            switch (result.isadmin)
            {
                case true:
                    return "Already an admin";
                default:
                    result.isadmin = true;
                    context.SaveChanges();
                    return "OK";
            }
            //return result.isadmin == 1 ? "OK" : "Already an admin";
        }

        [WebMethod]
        public string Promeni(string Ime, string Prezime, string Email, string User, string Pass, int Id)
        {
            context = new OBContext();

            var result = context.klients.Where(x => x.id_klient == Id || x.username == User).FirstOrDefault();

            if (result == null)
            {
                return "Null";
            }
            else
            {

                result.ime = Ime;
                result.prezime = Prezime;
                result.email = Email;
                result.username = User;
                result.pass = Pass;

                context.SaveChanges();
                return "OK";
            }

        }

        public string Promeni_nastan(int opis, int objekt, string naziv, string vreme, int id)
        {
            context = new OBContext();

            var result = context.nastans.Where(x => x.id_nastan == id).FirstOrDefault();

            if (result == null)
            {
                return "Null";
            }
            else
            {

                result.fk_id_komintent = opis;
                result.o_id_objekt = objekt;
                result.naziv = naziv;
                result.vreme = vreme;

                context.SaveChanges();
                return "OK";
            }
        }

        //###################### DELETE ###########################

        [WebMethod]
        public string Brisi_kosnicka_eden(int fk_id_karti, int fk_id_klient)
        {
            context = new OBContext();

            var result = context.kosnickas.Where(x => x.fk_id_karti == fk_id_karti &&
                                                      x.fk_id_klient == fk_id_klient).FirstOrDefault();

            if (result == null)
            {
                return "Null";
            }
            else
            {
                context.kosnickas.Remove(result);
                context.SaveChanges();

                return "OK";
            }
        }

        [WebMethod]
        public string Brisi_klient(int id)
        {
            context = new OBContext();

            var result = context.klients.Where(x => x.id_klient == id).FirstOrDefault();

            if (result == null)
            {
                return "Null";
            }
            else
            {
                context.klients.Remove(result);
                context.SaveChanges();

                return "OK";
            }
        }

        [WebMethod]
        public string Brisi_nastan(int id)
        {
            context = new OBContext();

            var result = context.nastans.Where(x => x.id_nastan == id).FirstOrDefault();

            if (result == null)
            {
                return "Null";
            }
            else
            {
                context.nastans.Remove(result);
                context.SaveChanges();

                return "OK";
            }
        }
        //###################### INSERT ###########################

        [WebMethod]
        public string Register(string user, string password, string ime, string prezime, string email)
        {
            context = new OBContext();

            var Resultusername = from klient in context.klients
                                 where klient.username == user
                                 select klient;

            var ResultEmail = from klient in context.klients
                              where klient.email == email
                              select klient;

            if (Resultusername.Count() > 0 | ResultEmail.Count() > 0)
            {
                if (Resultusername.Count() > 0 & ResultEmail.Count() > 0)
                {
                    return "username and email taken";
                }

                if (Resultusername.Count() > 0)
                {
                    return "username taken";
                }

                if (ResultEmail.Count() > 0)
                {
                    return "email taken";
                }
                return "aosjhd";

            }
            else
            {

                klient klt = new klient();

                klt.username = user;
                klt.pass = password;
                klt.ime = ime;
                klt.prezime = prezime;
                klt.email = email;


                var result = context.klients.Add(klt);

                if (result == null)
                {
                    return "ERROR";
                }
                else
                {
                    context.SaveChanges();
                    return "OK";
                }

            }
        }

        [WebMethod]
        public string Vnesi_nastan(string naziv, int objekt, int opis, string data, string slika, string cas)
        {
            context = new OBContext();
            nastan nas = new nastan();
            nas.naziv = naziv;
            nas.o_id_objekt = objekt;
            nas.fk_id_komintent = opis;
            nas.data = DateTime.Parse(data);
            nas.slika = slika;
            nas.cas = cas;
            var result = context.nastans.Add(nas);

            if (result == null)
            {
                return "ERROR";
            }
            else
            {
                context.SaveChanges();
                return "OK";
            }
        }

        [WebMethod]// nema potreba. Vnesuvas user i samo MakeAdmin...
        public string Vnesi_admin(string user, string pass, string ime, string prez, string email)
        {
            context = new OBContext();

            var Resultusername = from klient in context.klients
                                 where klient.username == user
                                 select klient;

            var ResultEmail = from klient in context.klients
                              where klient.email == email
                              select klient;
            if (Resultusername.Count() > 0 | ResultEmail.Count() > 0)
            {
                if (Resultusername.Count() > 0 & ResultEmail.Count() > 0)
                {
                    return "username and email taken";
                }

                if (Resultusername.Count() > 0)
                {
                    return "username taken";
                }

                if (ResultEmail.Count() > 0)
                {
                    return "email taken";
                }
                return "aosjhd";

            }
            else
            {

                klient x = new klient();

                x.username = user;
                x.pass = pass;
                x.ime = ime;
                x.prezime = prez;
                x.email = email;
                x.isadmin = true;


                var result = context.klients.Add(x);

                if (result == null)
                {
                    return "ERROR";
                }
                else
                {
                    context.SaveChanges();
                    return "OK";
                }

            }
        }

        [WebMethod]
        public string Vnesi_opis(string opis)
        {
            komintent x = new komintent();
            x.opis = opis;

            var result = context.komintents.Add(x);

            if (result == null)
            {
                return "ERROR";
            }
            else
            {
                context.SaveChanges();
                return "OK";
            }
        }

        [WebMethod]
        public string Vnesi_objekt(string ime, string grad)
        {
            objekt x = new objekt();
            x.ime = ime;
            x.grad = grad;

            var result = context.objekts.Add(x);

            if (result == null)
            {
                return "ERROR";
            }
            else
            {
                context.SaveChanges();
                return "OK";
            }
        }

        [WebMethod]
        public string Vnesi_vo_kosnicka(int id_karta, int id_klient)
        {
            kosnicka x = new kosnicka();
            x.fk_id_karti = id_karta;
            x.fk_id_klient = id_klient;

            var result = context.kosnickas.Add(x);
            if (result == null)
            {
                return "ERROR";
            }
            else
            {
                context.SaveChanges();
                return "OK";
            }
        }

        [WebMethod]
        public string Kupi_karta(int id_karta, int id_klient, string data)
        {
            prodazba x = new prodazba();
            x.id_karti = id_karta;
            x.id_klient = id_klient;
            x.datum_prodazba = data;

            var result = context.prodazbas.Add(x);
            if (result == null)
            {
                return "ERROR";
            }
            else
            {
                context.SaveChanges();
                return "OK";
            }
        }

        [WebMethod]
        public string Vnesi_karta(int nastan, string zona, int red, int mesto, int cena)
        {
            karti x = new karti();
            x.n_id_nastan = nastan;
            x.zona = zona;
            x.red = red.ToString();
            x.mesto = mesto.ToString();
            x.cena = cena;

            var result = context.kartis.Add(x);
            if (result == null)
            {
                return "ERROR";
            }
            else
            {
                context.SaveChanges();
                return "OK";
            }
        }
    }
}

