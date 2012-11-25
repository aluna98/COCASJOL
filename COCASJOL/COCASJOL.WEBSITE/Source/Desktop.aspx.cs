using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Seguridad;
using System.Xml;
using System.Data;
using System.Data.Objects;

namespace COCASJOL.WEBSITE
{
    public partial class Desktop : COCASJOLBASE //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    string loggedUser = Session["username"] as string;
                    //this.MyDesktop.Wallpaper = "../resources/images/desktop.jpg";
                    this.MyDesktop.Wallpaper = "../resources/images/background1.jpg";
                    this.MyDesktop.StartMenu.Title = loggedUser;

                    this.HideObjects();
                    //this.ShowObjects();
                }
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
        }

        private void HideObjects()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/resources/xml/Privilegios.xml"));

                XmlNodeList nodes = doc.SelectNodes("privilegios/privilege");

                foreach (XmlNode node in nodes)
                {
                    XmlNode moduleNode = node.SelectSingleNode("module");
                    string module = moduleNode.InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                    DesktopShortcuts listDS = this.MyDesktop.Shortcuts;

                    foreach (DesktopShortcut ds in listDS)
                    {
                        if (ds.ModuleID == module)
                            X.Js.Call("hideShortcut", new object[] { module });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void RemoveObjects()
        {
            try
            {
                string loggedUser = Session["username"] as string;

                UsuarioLogic usuariologic = new UsuarioLogic();

                List<privilegio> privs = usuariologic.GetAllPrivileges(loggedUser);

                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/resources/xml/Privilegios.xml"));

                XmlNodeList nodes = doc.SelectNodes("privilegios/privilege");


                foreach (privilegio p in privs)
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

                        if (p.PRIV_LLAVE == key)
                        {
                            DesktopShortcuts listDS = this.MyDesktop.Shortcuts;
                            DesktopModulesCollection listDM = this.MyDesktop.Modules;
                            ItemsCollection<Component> listIC = this.MyDesktop.StartMenu.Items;

                            foreach (DesktopShortcut ds in listDS)
                            {
                                if (ds.ModuleID == module)
                                    X.Js.Call("hideShortcut", new object[] { module });
                            }

                            foreach (DesktopModule dm in listDM)
                            {
                                if (dm.ModuleID == module)
                                {
                                    listDM.Remove(dm);
                                }
                            }


                            foreach (Component item in listIC)
                            {
                                if (item is Ext.Net.MenuItem)
                                {
                                    Ext.Net.MenuItem menuItem = (Ext.Net.MenuItem)item;

                                    if (menuItem.Menu.Count > 0)
                                    {
                                        MenuCollection menu = menuItem.Menu;

                                        foreach (Component itm in menu.Primary.Items)
                                        {
                                            if (itm.ID == menuitem)
                                                menu.Primary.Items.Remove(itm);
                                        }
                                    }
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
    }
}