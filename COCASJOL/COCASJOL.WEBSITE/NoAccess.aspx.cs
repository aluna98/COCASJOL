using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

namespace COCASJOL.Website
{
    public partial class NoAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExtNet.Msg.Show(new MessageBoxConfig
            {
                Title = "Sin Accesos",
                Message = "Usted no tiene los accesos necesarios para este recurso, para obtener acceso contactese con el administrador del sistema.",
                Buttons = MessageBox.Button.OK
            });
        }
    }
}