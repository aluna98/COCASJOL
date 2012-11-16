using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;

namespace COCASJOL.LOGIC.Web
{
    public class COCASJOLBASE : Page
    {
        public COCASJOLBASE()
        {
            base.Init += new EventHandler(COCASJOLBASE_Init);
        }

        protected void COCASJOLBASE_Init(object sender, EventArgs e)
        {
            this.Session["CurrentPage"] = HttpContext.Current.Request.FilePath;

#if DEBUG
            if (Session["username"] == null)
                Session["username"] = "DEVELOPER";
#endif
            string username = Session["username"] as string;

            if (!this.Session["CurrentPage"].ToString().Contains("Default.aspx") && 
                string.IsNullOrEmpty(username))
            {
                base.Response.Redirect("~/ExpiredSession.aspx");
            }
        }
    }
}
