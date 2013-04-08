using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COCASJOL.WEBSITE
{
    public partial class Error : System.Web.UI.Page
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Error).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string title = "Error Fatal", message = "Ocurrio un error inesperado.";

                string fromPage = Request.QueryString["aspxerrorpath"];
                string urlReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "";

                string currentPage = this.Session["CurrentPage"] as string;
                System.Text.StringBuilder errorMessage = new System.Text.StringBuilder();
                errorMessage.AppendFormat("{0}. Error fatal inesperado. URL Actual: (aspxerrorpath) = {1} - Anterior: (UrlReferrer) = {2} - (session) = {3}", title, fromPage, urlReferrer, currentPage);

                log.Fatal(errorMessage.ToString());

                Ext.Net.X.Msg.Show(new Ext.Net.MessageBoxConfig
                {
                    Title = title,
                    Message = message,
                    Buttons = Ext.Net.MessageBox.Button.OK,
                    Handler = "window.location = '" + fromPage + "'"
                });
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina custom de errores.", ex);
            }
        }
    }
}