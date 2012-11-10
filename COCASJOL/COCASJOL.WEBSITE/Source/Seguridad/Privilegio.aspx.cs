using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;
using COCASJOL.LOGIC;

namespace COCASJOL.WEBSITE.Source.Seguridad
{
    public partial class Privilegio : System.Web.UI.Page
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