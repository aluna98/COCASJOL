using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COCASJOL.WEBSITE
{
    public partial class ExpiredSession : System.Web.UI.Page
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(ExpiredSession).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string title = "Error Sesión Expirada", message = "Su sesión ha expirado.", actualPage = Request.QueryString["aspxerrorpath"];

                string currentPage = this.Session["CurrentPage"] as string;
                System.Text.StringBuilder errorMessage = new System.Text.StringBuilder();
                errorMessage.AppendFormat("{0}. La sesión de usuario expiró. URL: (aspxerrorpath) = {1} - (session) = {2}.", title, actualPage, currentPage);

                log.Warn(errorMessage.ToString());

                Session.RemoveAll();
                Session.Abandon();

                Ext.Net.X.Msg.Show(new Ext.Net.MessageBoxConfig
                {
                    Closable = false,
                    Title = title,
                    Message = message,
                    Buttons = Ext.Net.MessageBox.Button.OK,
                    Handler = "window.parent.location = 'Default.aspx'"
                });
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de sesion expirada.", ex);
            }
        }
    }
}