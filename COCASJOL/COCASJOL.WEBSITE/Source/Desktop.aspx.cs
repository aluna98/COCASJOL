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
                string loggedUser = Session["username"] as string;
                //this.MyDesktop.Wallpaper = "../resources/images/desktop.jpg";
                this.MyDesktop.Wallpaper = "../resources/images/background1.jpg";
                this.MyDesktop.StartMenu.Title = loggedUser;
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