using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBooking.Pages.Korisink
{
    /// <summary>
    /// Summary description for Sliki
    /// </summary>
    public class Sliki : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //instantiate your webservice
            var ws = new baza.onlinebooking();
            //invoke GetImage method passing file name as param
            var binImage = ws.GetImageFile(context.Request["fileName"]);

            if (binImage.Length == 1)
            {
                //File Not found by the web services
                //Maybe you want to display an alternate image here...
            }
            else
            {
                //file found, return it
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(binImage);
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