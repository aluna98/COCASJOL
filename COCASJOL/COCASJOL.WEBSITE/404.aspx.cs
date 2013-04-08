using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COCASJOL.WEBSITE
{
    public partial class _404 : System.Web.UI.Page
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(_404).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string title = "Error 404",
                    message = "La pagina a la que usted quiere acceder no existe.",
                    fromPage = Request.QueryString["aspxerrorpath"],
                    urlReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "";

                log.WarnFormat("{0}. La pagina a la que se quiere acceder no existe. URL: (aspxerrorpath) = {1} - (UrlReferrer) = {2} .", title, fromPage, urlReferrer);

                Ext.Net.X.Msg.Show(new Ext.Net.MessageBoxConfig
                {
                    Title = title,
                    Message = message,
                    Closable = false,
                    Buttons = Ext.Net.MessageBox.Button.OK,
                    Handler = "window.parent.location = 'Default.aspx'"
                });
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de Errores 404.", ex);
            }
        }
    }
}