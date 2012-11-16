using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

namespace COCASJOL.WEBSITE
{
    public partial class ExpiredSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExtNet.Msg.Show(new MessageBoxConfig
            {
                Title = "Error",
                Message = "Su sesión ha expirado.",
                Buttons = MessageBox.Button.OK,
                Handler = "window.parent.location = 'Default.aspx'"
            });
        }
    }
}