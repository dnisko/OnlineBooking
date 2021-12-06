using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBooking.Pages.Admin
{
    /// <summary>
    /// Summary description for proba
    /// </summary>
    public class proba : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            baza.onlinebooking ws = new baza.onlinebooking();

            string[] klienti = ws.GetCustomers(context.Request["korisnik"]);

            if (klienti.Length == 1)
            { 

            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Hello World");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}