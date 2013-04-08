using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COCASJOL.WEBSITE
{
    public partial class NoAccess : System.Web.UI.Page
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(NoAccess).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string title = "Error de Acceso", 
                    message = "Usted no tiene los accesos necesarios para este recurso, para obtener acceso contactese con el administrador del sistema.", 
                    actualPage = Request.QueryString["aspxerrorpath"],
                    urlReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "";

                string currentPage = this.Session["CurrentPage"] as string;
                System.Text.StringBuilder errorMessage = new System.Text.StringBuilder();
                errorMessage.AppendFormat("{0}. Se intento acceder a una pagina a la cual no tienen el acceso necesario. URL : (aspxerrorpath) = {1} - (UrlReferrer) = {2} - (session) = {3}", title, actualPage, urlReferrer, currentPage);

                log.Warn(errorMessage.ToString());

                Ext.Net.X.Msg.Show(new Ext.Net.MessageBoxConfig
                {
                    Title = title,
                    Message = message,
                    Closable = false
                });
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de errores de accesos.", ex);
            }
        }
    }
}