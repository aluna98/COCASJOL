using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using System.Xml;
using System.Data;
using System.Data.Objects; 

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Utiles;

namespace COCASJOL.WEBSITE
{
    public partial class Desktop : COCASJOLBASE //System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RemoveObjects();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    
                }

                string loggedUser = Session["username"] as string;
                this.MyDesktop.StartMenu.Title = loggedUser;
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
        }

        private void RemoveObjects()
        {
            try
            {                
                string loggedUser = Session["username"] as string;
#if DEBUG
                if (loggedUser == "DEVELOPER")
                    return;
#endif

                UsuarioLogic usuariologic = new UsuarioLogic();

                //List<privilegio> privs = usuariologic.GetPrivilegiosNoDeUsuario(loggedUser);
                List<privilegio> privs = usuariologic.GetPrivilegiosDeUsuario(loggedUser);

                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("privilegesXML")));

                XmlNodeList nodes = doc.SelectNodes("privilegios/privilege");

                DesktopShortcuts listDS = this.MyDesktop.Shortcuts;
                DesktopModulesCollection listDM = this.MyDesktop.Modules;
                ItemsCollection<Component> listIC = this.MyDesktop.StartMenu.Items;

                if (privs.Count == 0)
                {
                    listDS.Clear();
                    listDM.Clear();
                    listIC.Clear();
                }
                else
                {

                    foreach (XmlNode node in nodes)
                    {
                        XmlNode keyNode = node.SelectSingleNode("key");
                        XmlNode moduleNode = node.SelectSingleNode("module");
                        XmlNode shortcutNode = node.SelectSingleNode("shortcut");
                        XmlNode menuitemNode = node.SelectSingleNode("menuitem");

                        string key = keyNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                        string module = moduleNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                        string shortcut = shortcutNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                        string menuitem = menuitemNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                        var query = from p in privs.AsParallel()
                                    where p.PRIV_LLAVE == key
                                    select p;

                        if (query.Count() == 0)
                        {
                            for (int x = 0; x < listDS.Count; x++)
                            {
                                DesktopShortcut ds = listDS.ElementAt(x);

                                if (ds.ShortcutID == shortcut)
                                    listDS.Remove(ds);
                            }

                            for (int x = 0; x < listDM.Count; x++)
                            {
                                DesktopModule dm = listDM.ElementAt(x);

                                if (dm.ModuleID == module)
                                    listDM.Remove(dm);
                            }

                            for (int x = 0; x < listIC.Count; x++)
                            {
                                Component item = listIC.ElementAt(x);

                                if (item is Ext.Net.MenuItem)
                                {
                                    Ext.Net.MenuItem menuItem = (Ext.Net.MenuItem)item;

                                    if (menuItem.Menu.Count > 0)
                                    {
                                        MenuCollection menu = menuItem.Menu;

                                        for (int y = 0; y < menu.Primary.Items.Count; y++)
                                        {
                                            Component itm = menu.Primary.Items.ElementAt(y);
                                            if (itm.ID == menuitem)
                                                menu.Primary.Items.Remove(itm);
                                        }
                                    }

                                    if (menuItem.Menu.Primary.Items.Count == 0)
                                        listIC.Remove(menuItem);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void Logout_Click(object sender, DirectEventArgs e)
        {
            try
            {
                // Logout from Authenticated Session
                Session.Abandon();
                this.Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void CheckForNotifications()
        {
            try
            {
                string loggedUser = Session["username"] as string;
                List<notificacion> NotificacionesList = Application["NotificacionesList"] as List<notificacion>;

                if (NotificacionesList == null)
                    return;

                var query = from n in NotificacionesList
                            where n.USR_USERNAME == loggedUser && (n.NOTIFICACION_ESTADO.Equals(EstadosNotificacion.Creado))
                            select n;

                if (query.Count() > 0)
                {

                    NotificacionLogic notificacionlogic = new NotificacionLogic();
                    foreach (notificacion notif in NotificacionesList)
                    {
                        Notificacion(notif.NOTIFICACION_TITLE, notif.NOTIFICACION_MENSAJE);
                        notificacionlogic.ActualizarNotificacion(notif.NOTIFICACION_ID, EstadosNotificacion.Notificado);
                    }

                    Application["NotificacionesList"] = notificacionlogic.GetNotificaciones();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void Notificacion(string title, string message)
        {
            try
            {
                Ext.Net.Notification.Show(new Ext.Net.NotificationConfig
                {
                    Title = title,
                    Html = message,
                    BringToFront = false,
                    ShowPin = true,
                    Pinned = true
                });
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}