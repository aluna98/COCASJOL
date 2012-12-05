using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Xml;

using System.Data;
using System.Data.Objects;

using COCASJOL.LOGIC.Seguridad;

namespace COCASJOL.LOGIC.Web
{
    public class COCASJOLBASE : Page
    {
        public COCASJOLBASE()
        {
            base.Init += new EventHandler(this.COCASJOLBASE_Init);
            base.Error += new EventHandler(this.COCASJOL_Error);
        }

        protected void COCASJOLBASE_Init(object sender, EventArgs e)
        {
            this.Session["CurrentPage"] = HttpContext.Current.Request.FilePath;

#if DEBUG
            if (Session["username"] == null)
                Session["username"] = "DEVELOPER";
#endif
            string username = Session["username"] as string;

            if (!this.Session["CurrentPage"].ToString().Contains("Default.aspx") && 
                string.IsNullOrEmpty(username))
            {
                base.Response.Redirect("~/ExpiredSession.aspx");
            }
        }

        protected void COCASJOL_Error(object sender, EventArgs e)
        {
            Exception ex = base.Server.GetLastError();
            //log error
        }

        protected void ValidarCredenciales(string pagename)
        {
            try
            {
                string loggedUser = Session["username"] as string;
#if DEBUG
                if (loggedUser == "DEVELOPER")
                    return;
#endif

                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("privilegesXML")));

                XmlNode node = doc.SelectSingleNode("privilegios/privilege[page[contains(text(), '" + pagename + "')]]");
                XmlNode keyNode = node.SelectSingleNode("key");
                string key = keyNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                UsuarioLogic usuariologic = new UsuarioLogic();
                List<privilegio> privs = usuariologic.GetPrivilegiosDeUsuario(loggedUser);

                foreach (privilegio p in privs)
                {
                    if (p.PRIV_LLAVE == key)
                        return;
                }

                base.Response.Redirect("~/NoAccess.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, string> GetVariables(string pagename)
        {
            Dictionary<string, string> variablesDictionary = null;
            try
            {
                variablesDictionary = new Dictionary<string, string>();

                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("variablesDeEntornoXML")));
                XmlNodeList pagesNode = doc.SelectNodes("paginas/pagina[Name[contains(text(), '" + pagename + "')]]");

                foreach (XmlNode pageNode in pagesNode)
                {
                    XmlNodeList variablesNode = pageNode.SelectNodes("variables/variable");

                    foreach (XmlNode variableNode in variablesNode)
                    {
                        XmlNode keyNode = variableNode.SelectSingleNode("key");
                        XmlNode mapsNode = variableNode.SelectSingleNode("maps");

                        string key = keyNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                        string maps = mapsNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                        variablesDictionary[maps] = key;
                    }
                }

                return variablesDictionary;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
