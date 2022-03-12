using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WebService.Classes;

namespace WebService
{
    /// <summary>
    /// Summary description for onlinebooking
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Onlinebooking : System.Web.Services.WebService
    {
        public readonly string Konekcija;
        public Onlinebooking()
        {
            Konekcija = "Provider=SQLOLEDB;Data Source=VIKI-LAPTOP\\SQLEXPRESS;Initial Catalog=onlinebooking;Trusted_Connection=Yes;";// - OleDb
            
            //"Data Source=Dinka-PC;Initial Catalog=onlinebooking;Trusted_Connection=Yes;";// - Sql
            //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("onlinebooking.mdb");// - Access
            //Uncomment the following line if using designed components 
            //InitializeComponent();
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

        /*[WebMethod]
        public byte[] GetBarcode(string strCodetext, string strSymbology)
        {
            // Initialize BarCodeBuilder
            BarCodeBuilder builder = new BarCodeBuilder();
            // Set codetext
            builder.CodeText = strCodetext;
            // Set barcode symbology
            builder.SymbologyType = (Symbology)Enum.Parse(typeof(Symbology), strSymbology, true);

            // Create and save the barcode image to memory stream
            MemoryStream imgStream = new MemoryStream();
            builder.Save(imgStream, ImageFormat.Png);

            // Return the barcode image as a byte array
            return imgStream.ToArray();
        }*/

        [WebMethod]
        public DataSet Tabela_nastan()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select * from nastan";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");

            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet login(string k, string l)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;

            var sql = "select * from klient where username =? and pass=? ";

            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(sql, CNN);
            CMD.Parameters.AddWithValue("p1", k);
            CMD.Parameters.AddWithValue("p2", l);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "logiran");

            CNN.Close();
            return ds;
        }
        //################################ INSERT ###############################
        [WebMethod]
        public string Register(string user, string password, string ime, string prezime, string email)
        {
            return Vnesi(user, password, ime, prezime, email);
        }

        public string Vnesi(string us, string pa, string im, string pr, string em)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string k = null;

            string sql1 = "select * from klient where username = ?";// and email = ?";
            OleDbCommand CMD1 = new OleDbCommand(sql1, CNN);
            CMD1.Parameters.AddWithValue("p1", us);
            CMD1.Connection.Open();
            OleDbDataAdapter adapter1 = new OleDbDataAdapter(CMD1);
            DataSet dsUser = new DataSet();
            adapter1.Fill(dsUser, "user");
            CMD1.Connection.Close();

            string sql2 = "select * from klient where email = ?";//username = ?";
            OleDbCommand CMD2 = new OleDbCommand(sql2, CNN);
            CMD2.Parameters.AddWithValue("p1", em);
            CMD2.Connection.Open();
            OleDbDataAdapter adapter2 = new OleDbDataAdapter(CMD2);
            DataSet dsEmail = new DataSet();
            adapter2.Fill(dsEmail, "email");
            CMD2.Connection.Close();
            //CMD2.Connection.Open();

            if ((dsUser.Tables[0].Rows.Count == 0) & (dsEmail.Tables[0].Rows.Count == 0))
            {
                string sql_tekst = "insert into klient (ime, prezime, email, username, pass) ";
                sql_tekst += "values (?, ?, ?, ?, ?)";
                OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
                CNN.Open();
                komanda.Parameters.AddWithValue("p1", im);
                komanda.Parameters.AddWithValue("p2", pr);
                komanda.Parameters.AddWithValue("p3", em);
                komanda.Parameters.AddWithValue("p4", us);
                komanda.Parameters.AddWithValue("p5", pa);

                int broj = komanda.ExecuteNonQuery();
                if (broj > 0)
                {
                    k = "OK";
                }
                else
                {
                    k = "NO";
                }
            }
            else if ((dsUser.Tables[0].Rows.Count > 0) & (dsEmail.Tables[0].Rows.Count > 0))
            {
                k = "user&email";
            }
            else if (dsUser.Tables[0].Rows.Count > 0)
            {
                k = "user";
            }
            else if (dsEmail.Tables[0].Rows.Count > 0)
            {
                k = "email";
            }
            CNN.Close();
            return k;
        }

        [WebMethod]
        public string vnesi_nastan(string naziv, int opis, int objekt, string slika, string data, string cas)
        {
            return Vnesi_nastan(naziv, opis, objekt, slika, data, cas);
        }

        public string Vnesi_nastan(string na, int op, int ob, string sl, string da, string ca)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "insert into nastan (naziv, o_id_objekt, fk_id_komintent, vreme, slika, cas) ";
            sql_tekst += "values (?, ?, ?, ?, ?, ?)";
            CNN.Open();

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            komanda.Parameters.AddWithValue("p1", na);
            komanda.Parameters.AddWithValue("p2", ob);
            komanda.Parameters.AddWithValue("p3", op);
            komanda.Parameters.AddWithValue("p4", da);
            komanda.Parameters.AddWithValue("p5", sl);
            komanda.Parameters.Add(new OleDbParameter("@vreme", OleDbType.Date));

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "OK";
            }
            else
            {
                odgovor = "NO";
            }
            CNN.Close();

            return odgovor;
        }

        [WebMethod]
        public string Vnesi_admin(string user, string pass, string ime, string prez, string email)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string k = null;

            string sql1 = "select * from klient where username = ?";// and email = ?";
            OleDbCommand CMD1 = new OleDbCommand(sql1, CNN);
            CMD1.Parameters.AddWithValue("p1", user);
            CMD1.Connection.Open();
            OleDbDataAdapter adapter1 = new OleDbDataAdapter(CMD1);
            DataSet dsUser = new DataSet();
            adapter1.Fill(dsUser, "user");
            CMD1.Connection.Close();

            string sql2 = "select * from klient where email = ?";//username = ?";
            OleDbCommand CMD2 = new OleDbCommand(sql2, CNN);
            CMD2.Parameters.AddWithValue("p1", email);
            CMD2.Connection.Open();
            OleDbDataAdapter adapter2 = new OleDbDataAdapter(CMD2);
            DataSet dsEmail = new DataSet();
            adapter2.Fill(dsEmail, "email");
            CMD2.Connection.Close();
            //CMD2.Connection.Open();

            if ((dsUser.Tables[0].Rows.Count == 0) & (dsEmail.Tables[0].Rows.Count == 0))
            {
                string sql_tekst = "insert into klient (ime, prezime, email, username, pass, isadmin) ";
                sql_tekst += "values (?, ?, ?, ?, ?, 1)";
                OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
                CNN.Open();
                komanda.Parameters.AddWithValue("p1", ime);
                komanda.Parameters.AddWithValue("p2", prez);
                komanda.Parameters.AddWithValue("p3", email);
                komanda.Parameters.AddWithValue("p4", user);
                komanda.Parameters.AddWithValue("p5", pass);

                int broj = komanda.ExecuteNonQuery();
                if (broj > 0)
                {
                    k = "OK";
                }
                else
                {
                    k = "NO";
                }
            }
            else if ((dsUser.Tables[0].Rows.Count > 0) & (dsEmail.Tables[0].Rows.Count > 0))
            {
                k = "user&email";
            }
            else if (dsUser.Tables[0].Rows.Count > 0)
            {
                k = "user";
            }
            else if (dsEmail.Tables[0].Rows.Count > 0)
            {
                k = "email";
            }
            CNN.Close();
            return k;
        }

        [WebMethod]
        public string vnesi_opis(string opis)
        {
            return Vnesi_opis(opis);
        }

        public string Vnesi_opis(string o)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "insert into komintent (opis) values (?)";

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            CNN.Open();
            komanda.Parameters.AddWithValue("p1", o);

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "Описот е внесен";
            }
            else
            {
                odgovor = "Се случи грешка, обиди се повторно";
            }
            CNN.Close();
            return odgovor;
        }

        [WebMethod]
        public string vnesi_objekt(string ime, string grad)
        {
            return Vnesi_objekt(ime, grad);
        }

        public string Vnesi_objekt(string i, string g)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "insert into objekt (ime, grad) ";
            sql_tekst += "values (?, ?)";

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            CNN.Open();
            komanda.Parameters.AddWithValue("p1", i);
            komanda.Parameters.AddWithValue("p2", g);

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "Објектот е внесен";
            }
            else
            {
                odgovor = "Се случи грешка, обиди се повторно";
            }

            CNN.Close();
            return odgovor;
        }

        [WebMethod]
        public string vnesi_vo_kosnicka(int id_karta, int id_klient)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);

            string sql1 = "select * from kosnicka where k_id_klient = ? and k_id_karti = ?";// and email = ?";
            OleDbCommand CMD1 = new OleDbCommand(sql1, CNN);

            CMD1.Parameters.AddWithValue("p1", id_klient);
            CMD1.Parameters.AddWithValue("p2", id_karta);
            CMD1.Connection.Open();

            OleDbDataAdapter adapter1 = new OleDbDataAdapter(CMD1);
            DataSet dsKosnicka = new DataSet();
            adapter1.Fill(dsKosnicka, "user");
            CMD1.Connection.Close();

            string odgovor = null;

            if (dsKosnicka.Tables[0].Rows.Count == 0)
            {
                string sql_tekst = "insert into kosnicka (k_id_klient, k_id_karti) ";
                sql_tekst += "values (?, ?)";

                OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
                CNN.Open();
                komanda.Parameters.AddWithValue("p1", id_klient);
                komanda.Parameters.AddWithValue("p2", id_karta);

                int broj = komanda.ExecuteNonQuery();
                if (broj > 0)
                {
                    odgovor = "OK";
                }
                else
                {
                    odgovor = "NO";
                }
            }
            else
            {
                odgovor = "IMA";
            }
            CNN.Close();
            return odgovor;
        }

        [WebMethod]
        public string kupi_karta(int id_karta, int id_klient, string data)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "insert into prodazba (id_klient, id_karti, datum_prodazba) ";
            sql_tekst += "values (?, ?, ?)";

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            CNN.Open();
            komanda.Parameters.AddWithValue("p1", id_klient);
            komanda.Parameters.AddWithValue("p2", id_karta);
            komanda.Parameters.AddWithValue("p3", data);

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "OK";
            }
            else
            {
                odgovor = "NO";
            }

            CNN.Close();
            return odgovor;
        }

        [WebMethod]
        public string Vnesi_karta(int nastan, string zona, int red, int mesto, int cena)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "insert into karti (n_id_nastan, zona, red, mesto, cena, lager)";
            sql_tekst += " values (?, ?, ?, ?, ?, 1)";

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            CNN.Open();
            komanda.Parameters.AddWithValue("p1", nastan);
            komanda.Parameters.AddWithValue("p2", zona);
            komanda.Parameters.AddWithValue("p3", red);
            komanda.Parameters.AddWithValue("p4", mesto);
            komanda.Parameters.AddWithValue("p5", cena);

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "OK";
            }
            else
            {
                odgovor = "NO";
            }
            CNN.Close();
            return odgovor;
        }
        //################################ UPDATE ###############################
        [WebMethod]
        public string napravi_admin(int id)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string odgovor = null;
            string sql = "update klient set isadmin=1 where id_klient=?";
            string sql1 = "select username from klient where id_klient=?";
            CNN = new OleDbConnection(Konekcija);
            CNN.Open();

            CMD = new OleDbCommand(sql, CNN);
            CMD.Parameters.AddWithValue("p1", id);
            int broj = CMD.ExecuteNonQuery();
            if (broj > 0)
            {
                OleDbCommand CMD1 = null;
                CMD1 = new OleDbCommand(sql1, CNN);
                CMD1.Parameters.AddWithValue("p1", id);
                OleDbDataReader rsuser = CMD1.ExecuteReader();
                rsuser.Read();
                string user = rsuser["username"].ToString();
                odgovor = "Успешно го направивте корисникот со username: " + user + " во администратор";

                rsuser.Close();
            }
            else
            {
                odgovor = "Се случи грешка, обиди се повторно";
            }
            CNN.Close();

            return odgovor;
        }
        [WebMethod]
        public string Promeni(string Ime, string Prezime, string email, string User, string Pass, int Id)
        {
            return Promeni1(Ime, Prezime, email, User, Pass, Id);
        }

        public string Promeni1(string n_ime, string n_prezime, string n_email, string n_user, string n_pass, int id)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = "";
            if ((n_ime.Length > 0) && (n_prezime.Length > 0) && (n_email.Length > 0) && (n_user.Length > 0) && (n_pass.Length > 0) && (id > 0))
            {
                string sql = "Update klient set ime=?, prezime=?, email=?, username=?, pass=? where id_klient=?";
                OleDbCommand komanda = new OleDbCommand(sql, CNN);
                komanda.Parameters.AddWithValue("p1", n_ime);
                komanda.Parameters.AddWithValue("p2", n_prezime);
                komanda.Parameters.AddWithValue("p3", n_email);
                komanda.Parameters.AddWithValue("p4", n_user);
                komanda.Parameters.AddWithValue("p5", n_pass);
                komanda.Parameters.AddWithValue("p6", id);
                CNN.Open();
                int broj = komanda.ExecuteNonQuery();
                if (broj > 0)
                {
                    odgovor = "OK";
                }
                else
                {
                    odgovor = "NO";
                }
                CNN.Close();
            }
            else
            {
                odgovor = "pole";
            }

            return odgovor;
        }

        [WebMethod]
        public string Promeni_korisnik(string Ime, string Prezime, string email, string User, string Pass, string Username)
        {
            return Promeni_k(Ime, Prezime, email, User, Pass, Username);
        }

        public string Promeni_k(string n_ime, string n_prezime, string n_email, string n_user, string n_pass, string n_username)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = "";
            if ((n_ime.Length > 0) && (n_prezime.Length > 0) && (n_email.Length > 0) && (n_user.Length > 0) && (n_pass.Length > 0))
            {
                string sql = "Update klient set ime=?, prezime=?, email=?, username=?, pass=? where username=?";
                OleDbCommand komanda = new OleDbCommand(sql, CNN);
                komanda.Parameters.AddWithValue("p1", n_ime);
                komanda.Parameters.AddWithValue("p2", n_prezime);
                komanda.Parameters.AddWithValue("p3", n_email);
                komanda.Parameters.AddWithValue("p4", n_user);
                komanda.Parameters.AddWithValue("p5", n_pass);
                komanda.Parameters.AddWithValue("p6", n_username);
                CNN.Open();
                int broj = komanda.ExecuteNonQuery();
                if (broj > 0)
                {
                    odgovor = "Успешна промена";
                }
                else
                {
                    odgovor = "Се случи грешка";
                }
                CNN.Close();
            }
            else
            {
                odgovor = "Сите полиња се задолжителни";
            }

            return odgovor;
        }

        [WebMethod]
        public string Promeni_nastan(int opis, int objekt, string naziv, string vreme, int id)
        {
            return Promeni_n(opis, objekt, naziv, vreme, id);
        }

        public string Promeni_n(int n_opis, int n_objekt, string n_naziv, string n_vreme, int n_id)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = "";
            if ((n_opis > 0) && (n_objekt > 0) && (n_naziv.Length > 0) && (n_vreme.Length > 0) && (n_id > 0))
            {
                string SQL = "update nastan set fk_id_komintent=?, o_id_objekt=?, naziv=?, vreme=?";
                SQL += " where id_nastan=?";
                OleDbCommand komanda = new OleDbCommand(SQL, CNN);
                komanda.Parameters.AddWithValue("p1", n_opis);
                komanda.Parameters.AddWithValue("p2", n_objekt);
                komanda.Parameters.AddWithValue("p3", n_naziv);
                komanda.Parameters.AddWithValue("p4", n_vreme);
                komanda.Parameters.AddWithValue("p5", n_id);

                CNN.Open();
                int broj = komanda.ExecuteNonQuery();
                if (broj > 0)
                {
                    odgovor = "Успешна промена";
                }
                else
                {
                    odgovor = "Се случи грешка";
                }
                CNN.Close();
            }
            else
            {
                odgovor = "Сите полиња се задолжителни";
            }

            return odgovor;
        }
        //################################ DELETE ###############################

        [WebMethod]
        public string brisi_kosnicka_eden(int k_id_karti, int k_id_klient)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "delete from kosnicka where k_id_karti = ? and k_id_klient = ?";

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            CNN.Open();
            komanda.Parameters.AddWithValue("p1", k_id_karti);
            komanda.Parameters.AddWithValue("p2", k_id_klient);

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "OK";
            }
            else
            {
                odgovor = "NO";
            }
            CNN.Close();
            return odgovor;
        }

        [WebMethod]
        public string brisi_klient(int id_klient)
        {
            return Brisi_klient(id_klient);
        }

        public string Brisi_klient(int id)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "delete from klient where id_klient=?";

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            CNN.Open();
            komanda.Parameters.AddWithValue("p1", id);

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "OK";
            }
            else
            {
                odgovor = "Се случи грешка, обиди се повторно";
            }
            CNN.Close();
            return odgovor;
        }

        [WebMethod]
        public string brisi_nastan(int id_nastan)
        {
            return Brisi_nastan(id_nastan);
        }

        public string Brisi_nastan(int id)
        {
            OleDbConnection CNN = null;
            CNN = new OleDbConnection(Konekcija);
            string odgovor = null;
            string sql_tekst = "delete from nastan where id_nastan=?";

            OleDbCommand komanda = new OleDbCommand(sql_tekst, CNN);
            CNN.Open();
            komanda.Parameters.AddWithValue("p1", id);

            int broj = komanda.ExecuteNonQuery();
            if (broj > 0)
            {
                odgovor = "OK";
            }
            else
            {
                odgovor = "Се случи грешка, обиди се повторно";
            }
            CNN.Close();
            return odgovor;
        }
        //################################ SELECT ###############################

        [WebMethod]
        public DataSet vkupno(int id)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            CNN = new OleDbConnection(Konekcija);

            string sql = "select distinct username, SUM(cena) as vkupno from klient, karti, kosnicka, nastan";
            sql += " where id_karti=k_id_karti and id_klient=k_id_klient";
            sql += " and id_nastan=n_id_nastan and id_klient = ?";
            sql += " group by username";

            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "vkupno");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet kosnicka(int id)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            CNN = new OleDbConnection(Konekcija);

            string sql = "select distinct * from klient, karti, kosnicka, nastan";
            sql += " where id_karti=k_id_karti and id_klient=k_id_klient";
            sql += " and id_nastan=n_id_nastan and id_klient = ?";

            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "kosnicka");
            CNN.Close();
            return ds;

        }

        [WebMethod]
        public DataSet pregled_karti_korisnik_profile(int id)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            CNN = new OleDbConnection(Konekcija);

            string sql = "select distinct * from prodazba, karti, klient, nastan ";
            sql += "where karti.id_karti=prodazba.id_karti and klient.id_klient=prodazba.id_klient and id_nastan=n_id_nastan ";
            sql += "and klient.id_klient = ?";// like '%' + ? + '%'";

            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "karti");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet pregled_karti_korisnik(string user)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            CNN = new OleDbConnection(Konekcija);

            string sql = "select distinct * from prodazba, karti, klient, nastan ";
            sql += "where karti.id_karti=prodazba.id_karti and klient.id_klient=prodazba.id_klient and id_nastan=n_id_nastan ";
            sql += "and klient.id_klient = prodazba.id_klient and username like '%' + ? + '%'";

            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", user);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "karti");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet pregled_karti_korisnici()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            CNN = new OleDbConnection(Konekcija);

            string sql = "select distinct * from prodazba, karti, klient, nastan ";
            sql += "where karti.id_karti=prodazba.id_karti and klient.id_klient=prodazba.id_klient and id_nastan=n_id_nastan ";
            sql += "and klient.id_klient = prodazba.id_klient";

            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "karti");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet pregled_kupena_karta(int klient, int karta)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            CNN = new OleDbConnection(Konekcija);

            string sql = "select distinct * from prodazba, karti, klient, nastan ";
            sql += "where karti.id_karti=prodazba.id_karti and id_nastan=n_id_nastan ";
            sql += "and klient.id_klient = prodazba.id_klient and prodazba.id_klient = ? and prodazba.id_karti = ?";

            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", klient);
            CMD.Parameters.AddWithValue("p2", karta);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "karti");
            CNN.Close();
            return ds;
        }


        [WebMethod]
        public DataSet Opis_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string sql = "SELECT * from komintent";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "opis");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet Objekt_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string sql = "SELECT * from objekt";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(sql, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "objekt");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_korisnici_po_username_DS(string username)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select *  from klient where username LIKE '%' + ? + '%'";
            CNN = new OleDbConnection(Konekcija);//id_klient, ime, prezime, username, isadmin
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", username);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "klienti");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_korisnici_po_id_DS(int id)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select *  from klient where id_klient = ?";
            CNN = new OleDbConnection(Konekcija);//id_klient, ime, prezime, username, isadmin
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "user");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_korisnici_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select * from klient";
            CNN = new OleDbConnection(Konekcija);//id_klient, ime, prezime, username, isadmin
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "klienti1");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_admini_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select * from klient where isadmin=1";
            CNN = new OleDbConnection(Konekcija);//id_klient, ime, prezime, username, isadmin
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "klienti1");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_top5_nastani_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select top 5 * from nastan";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_nastani_za_nastan()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select * from nastan order by vreme asc";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_karti_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;

            string SQL = "select n_id_nastan, data, naziv, opis, ime, grad, vreme, sum(lager) as lager";
            SQL += " from karti, nastan, objekt, komintent where";
            SQL += " id_nastan = n_id_nastan and id_objekt = o_id_objekt and id_komintent = fk_id_komintent";
            SQL += " and lager is not null";
            SQL += " group by n_id_nastan, naziv, opis, ime, vreme, grad, data";
            SQL += " order by vreme desc";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_karti_detalno_DS(int id)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;

            string SQL = "select id_karti, naziv, n_id_nastan, red, mesto, zona from karti, nastan";
            SQL += " where n_id_nastan=id_nastan and n_id_nastan=?";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);


            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_zona(int id)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;

            string SQL = "select distinct zona from karti";
            SQL += " where n_id_nastan=?";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);


            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "zona");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_mesto(int id, string zona)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;

            string SQL = "select distinct zona, mesto from karti";
            SQL += " where n_id_nastan=? and zona=?";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);
            CMD.Parameters.AddWithValue("p2", zona);


            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "mesto");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_red(int id, string zona, int mesto)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;

            string SQL = "select distinct zona, mesto, red from karti";
            SQL += " where n_id_nastan=? and zona=? and mesto =?";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);
            CMD.Parameters.AddWithValue("p2", zona);
            CMD.Parameters.AddWithValue("p3", mesto);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "red");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_cena(int id, string zona, int mesto, int red)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;

            string SQL = "select distinct id_karti, cena from karti";
            SQL += " where n_id_nastan=? and zona=? and mesto =? and red=?";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", id);
            CMD.Parameters.AddWithValue("p2", zona);
            CMD.Parameters.AddWithValue("p3", mesto);
            CMD.Parameters.AddWithValue("p4", red);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "cena");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_nastani_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select * from nastan, objekt, komintent where";
            SQL += "  id_objekt = o_id_objekt and id_komintent = fk_id_komintent";//id_nastan = n_id_nastan and
            SQL += " order by vreme desc";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_kupeni_karti_za_nastani_po_naziv(string naziv)
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            /*string SQL = "select distinct * from nastan, objekt, komintent, karti, prodazba, klient";
            SQL += " where id_objekt = o_id_objekt and prodazba.id_karti = karti.id_karti and prodazba.id_klient = klient.id_klient";
            SQL += " and id_komintent = fk_id_komintent and id_nastan = n_id_nastan and naziv like '%' + ? + '%'";
            SQL += " order by vreme asc";*/
            string SQL = "select distinct * from nastan, klient, objekt, komintent, karti, prodazba";
            SQL += " where id_objekt = o_id_objekt and id_komintent = fk_id_komintent and id_nastan = n_id_nastan";
            SQL += " and prodazba.id_karti = karti.id_karti and prodazba.id_klient = klient.id_klient ";
            SQL += " and (naziv like '%'+?+'%' or naziv like '%'+?+'%')";//naziv like '%' + ? + '%'";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();
            CMD.Parameters.AddWithValue("p1", naziv);
            CMD.Parameters.AddWithValue("p2", naziv);

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public DataSet lista_na_kupeni_karti_za_nastani()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            /*string SQL = "select distinct * from nastan, objekt, komintent, karti, prodazba, klient";
            SQL += " where id_objekt = o_id_objekt and prodazba.id_karti = karti.id_karti and prodazba.id_klient = klient.id_klient";
            SQL += " and id_komintent = fk_id_komintent and id_nastan = n_id_nastan and naziv like '%' + ? + '%'";
            SQL += " order by vreme asc";*/
            string SQL = "select distinct * from nastan, klient, objekt, komintent, karti, prodazba";
            SQL += " where id_objekt = o_id_objekt and id_komintent = fk_id_komintent and id_nastan = n_id_nastan";
            SQL += " and prodazba.id_karti = karti.id_karti and prodazba.id_klient = klient.id_klient ";
            CNN = new OleDbConnection(Konekcija);
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "nastani");
            CNN.Close();
            return ds;
        }

        [WebMethod]
        public Nastan NastanInfo(int id)
        {
            Nastan ns = null;
            string sql;
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

        [WebMethod] //asd
        public DataSet data_DS()
        {
            OleDbConnection CNN = null;
            OleDbCommand CMD = null;
            string SQL = "select vreme from nastan where id_nastan=1";
            CNN = new OleDbConnection(Konekcija);//id_klient, ime, prezime, username, isadmin
            CMD = new OleDbCommand(SQL, CNN);
            CMD.Connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(CMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "data");
            CNN.Close();
            return ds;
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
        /*
        [WebMethod]
        public List<nastan> NastanAll()
        {
            //###############-Add new-#######################################################
            List<nastan> listnastani = new List<nastan>();
            using (onlinebookingEntities ctx = new onlinebookingEntities())
            {
                nastan novNastan = new nastan();
                novNastan.cas = "Dino";
                ctx.AddTonastans(novNastan);
                ctx.SaveChanges();
                listnastani = ctx.nastans.ToList();

            }
            return listnastani;
        }

        [WebMethod]
        public nastan NastanAllo(int Id)
        {
            //############PRIMERI KAKO DA RABOTIS OSNOVNI C.R.U.D. metodi.#########
            //###############-Get item-#######################################################
            using (onlinebookingEntities ctx = new onlinebookingEntities())
            {
                nastan item = ctx.nastans.Where(c => c.id_nastan == Id).FirstOrDefault();
                if (item != null)
                    return item;
            }
            return null;

            //###############-Update-#######################################################
            using (onlinebookingEntities ctx = new onlinebookingEntities())
            {
                nastan item = ctx.nastans.Where(c => c.id_nastan == Id).FirstOrDefault();
                item.naziv = "Sto sakas";
                ctx.SaveChanges();
            }

            //###############-Delete-#######################################################
            using (onlinebookingEntities ctx = new onlinebookingEntities())
            {
                nastan item = ctx.nastans.Where(c => c.id_nastan == Id).FirstOrDefault();
                if (item != null)
                {
                    ctx.DeleteObject(item);
                    ctx.SaveChanges();
                }
            }

        }
        */
    }
}