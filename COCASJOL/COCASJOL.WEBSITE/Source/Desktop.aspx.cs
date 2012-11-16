using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC.Web;

namespace COCASJOL.WEBSITE
{
    public partial class Desktop : COCASJOLBASE //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //this.MyDesktop.Wallpaper = "../Images/desktop.jpg";
                this.MyDesktop.Wallpaper = "../Images/background1.jpg";
            }
        }

        protected void Logout_Click(object sender, DirectEventArgs e)
        {
            // Logout from Authenticated Session
            Session.Abandon();
            this.Response.Redirect("~/Default.aspx");
        }
    }
}