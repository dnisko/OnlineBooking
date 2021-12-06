using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineBooking.baza;

namespace OnlineBooking.Pages.Korisink
{
    public partial class PreviewItem : System.Web.UI.Page
    {
        public string EmbedSrc
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                return;

            if (!int.TryParse(Request.QueryString["id"], out var id))
                return;

            using (var service = new onlinebooking())
            {
                var ns = service.NastanInfo(id);
                lblNasov.Text = ns.Naziv;
                imgSlika.Src = $"~/Sliki.ashx?fileName={ns.Slika}";
                lblSopis.Text = ns.Sopis;
                //  EmbedSrc = "";
                // String t = "//www.youtube.com/v/Z7b37l_B4vI?hl=en_US&amp;version=3";
                //lbl.Text = ns.Video;
                sajtHL.Text = ns.Sajt;
                sajtHL.NavigateUrl = ns.Sajt;
                var t = ns.Video;

                myObject.Text = "<object width=\"420\" height=\"315\"><param name=\"movie\" value=\""

                                + t + "\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" " +
                                "value=\"always\"></param><embed src=\""

                                + t + "\" type=\"application/x-shockwave-flash\" width=\"420\" height=\"315\" allowscriptaccess=\"always\" " +
                                "allowfullscreen=\"true\"></embed></object>";

            }
        }
    }
}