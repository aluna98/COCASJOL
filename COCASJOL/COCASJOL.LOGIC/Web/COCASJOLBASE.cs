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
using COCASJOL.LOGIC.Entorno;

using log4net;

namespace COCASJOL.LOGIC.Web
{
    public class COCASJOLBASE : System.Web.UI.Page
    {
        private static ILog log = LogManager.GetLogger(typeof(COCASJOLBASE).Name);

        private string pagename;
        private XmlDocument docPrivilegios;
        private XmlDocument docEntorno;
        private static object lockObj = new object();

        public COCASJOLBASE()
        {
            try
            {
                base.Init += new EventHandler(this.COCASJOLBASE_Init);
                base.Error += new EventHandler(this.COCASJOL_Error);

                //Get Derived Class Name
                pagename = this.GetType().BaseType.Name;

                //Get Cached Privilegios
                string strPrivilegesPath = System.Configuration.ConfigurationManager.AppSettings.Get("privilegesXML");
                var varPrivsXML = HttpContext.Current.Cache.Get("privilegesXML");

                if (varPrivsXML == null)
                {
                    lock (lockObj)
                    {
                        docPrivilegios = new XmlDocument();
                        docPrivilegios.Load(Server.MapPath(strPrivilegesPath));

                        HttpContext.Current.Cache.Insert("privilegesXML", docPrivilegios,
                            new System.Web.Caching.CacheDependency(Server.MapPath(strPrivilegesPath)));
                    }
                }
                else
                    docPrivilegios = varPrivsXML as XmlDocument;


                //Get Cached Entorno
                string strEnvironmentPath = System.Configuration.ConfigurationManager.AppSettings.Get("variablesDeEntornoXML");
                var varEnvsXML = HttpContext.Current.Cache.Get("variablesDeEntornoXML");

                if (varEnvsXML == null)
                {
                    lock (lockObj)
                    {
                        docEntorno = new XmlDocument();
                        docEntorno.Load(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("variablesDeEntornoXML")));

                        HttpContext.Current.Cache.Insert("variablesDeEntornoXML", docEntorno,
                            new System.Web.Caching.CacheDependency(Server.MapPath(strEnvironmentPath)));
                    }
                }
                else
                    docEntorno = varEnvsXML as XmlDocument;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al inicializar constructor de COCASJOLBASE.", ex);
                throw;
            }
        }

        protected void COCASJOLBASE_Init(object sender, EventArgs e)
        {
            try
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

                this.ValidarCredenciales();

                Dictionary<string, string> variables = this.GetVariables();
                this.ValidarVariables(variables);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al inicializar pagina COCASJOLBASE.", ex);
                throw;
            }
        }

        protected void COCASJOL_Error(object sender, EventArgs e)
        {
            Exception ex = base.Server.GetLastError();
            log.Fatal("Error de aplicación COCASJOLBASE.", ex);
        }

        protected void ValidarCredenciales()
        {
            try
            {
                string loggedUser = Session["username"] as string;
#if DEBUG
                if (loggedUser == "DEVELOPER")
                    return;
#endif
                if (pagename == "Desktop")
                    return;

                XmlNode node = docPrivilegios.SelectSingleNode("privilegios/privilege[page[contains(text(), '" + pagename + "')]]");

                if (node == null)
                {
                    Response.Redirect("~/NoAccess.aspx");
                }

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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar credenciales.", ex);
                throw;
            }
        }

        public Dictionary<string, string> GetVariables()
        {

            Dictionary<string, string> variablesDictionary = null;
            try
            {
                variablesDictionary = new Dictionary<string, string>();

                XmlNodeList pagesNode = docEntorno.SelectNodes("paginas/pagina[Name[contains(text(), '" + pagename + "')]]");

                VariablesDeEntornoLogic variablesLogic = new VariablesDeEntornoLogic();

                foreach (XmlNode pageNode in pagesNode)
                {
                    XmlNodeList variablesNode = pageNode.SelectNodes("variables/variable");

                    foreach (XmlNode variableNode in variablesNode)
                    {
                        XmlNode keyNode = variableNode.SelectSingleNode("key");

                        string key = keyNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                        variable_de_entorno varenv = variablesLogic.GetVariableDeEntorno(key);

                        variablesDictionary[key] = varenv.VARIABLES_VALOR;
                    }
                }

                return variablesDictionary;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener variables de entorno.", ex);
                throw;
            }
        }

        public bool ValidarVariables(Dictionary<string, string> Variables)
        {
            try
            {
                XmlNodeList pagesNode = docEntorno.SelectNodes("paginas/pagina[Name[contains(text(), '" + pagename + "')]]");

                foreach (XmlNode pageNode in pagesNode)
                {
                    XmlNodeList variablesNode = pageNode.SelectNodes("variables/variable");

                    foreach (XmlNode variableNode in variablesNode)
                    {
                        XmlNode keyNode = variableNode.SelectSingleNode("key");
                        XmlNode typeNode = variableNode.SelectSingleNode("type");

                        string key = keyNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                        string type = typeNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                        string VARIABLES_VALOR;
                        if (!Variables.TryGetValue(key, out VARIABLES_VALOR))
                        {
                            ShowPinnedNotification("Variables de Entorno", "La variable de entorno \"" + key + "\" no existe.");
                            return false;
                        }


                        if (type == "decimal")
                        {
                            decimal decResult;
                            if (!decimal.TryParse(VARIABLES_VALOR, out decResult))
                            {
                                ShowPinnedNotification("Variables de Entorno", "El tipo de la variable de entorno \"" + key + "\" es incorrecto. Debe ser un numero decimal.");
                                return false;
                            }
                        }
                        else if (type == "int")
                        {
                            int nResult;
                            if (!int.TryParse(VARIABLES_VALOR, out nResult))
                            {
                                ShowPinnedNotification("Variables de Entorno", "El tipo de la variable de entorno \"" + key + "\" es incorrecto. Debe ser un numero entero.");
                                return false;
                            }
                        }
                        else if (type == "bool")
                        {
                            bool bResult;
                            if (!bool.TryParse(VARIABLES_VALOR, out bResult))
                            {
                                ShowPinnedNotification("Variables de Entorno", "El tipo de la variable de entorno \"" + key + "\" es incorrecto. Debe ser un valor booleano(0-1).");
                                return false;
                            }
                        }
                        else if (type == "string" || type == "char")
                        {
                            continue;
                        }
                        else
                        {
                            ShowPinnedNotification("Variables de Entorno", "El tipo de la variable de entorno \"" + key + "\" es incorrecto.");
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar variables de entorno.", ex);
                throw;
            }
        }

        protected virtual void ShowPinnedNotification(string title, string message)
        {
            try
            {
                Ext.Net.Notification.Show(new Ext.Net.NotificationConfig
                {
                    Title = title,
                    Html = message,
                    ShowPin = true,
                    Pinned = true
                });
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al mostrar notificacion.", ex);
                throw;
            }
        }
    }
}
