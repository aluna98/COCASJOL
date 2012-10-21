using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

namespace COCASJOL.WEBSITE.Source.Seguridad
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string loggedUsr = "DEVELOPER";
                
#if !DEBUG
                loggedUsr = Session["username"] as string;
#endif
                this.LoggedUserHdn.Text = loggedUsr;
            }
        }
    }
}