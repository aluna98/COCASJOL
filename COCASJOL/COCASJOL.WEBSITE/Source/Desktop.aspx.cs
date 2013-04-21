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

using COCASJOL.LOGIC.Reportes;

using System.Reflection;
using log4net;

using COCASJOL.LOGIC.Configuracion;

namespace COCASJOL.WEBSITE
{
    public partial class Desktop : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(Desktop).Name);

        private ConfiguracionDeSistemaLogic configuracionLogic;

        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
                RemoveObjects();
            }
            catch (System.Threading.ThreadAbortException tex)
            {
                log.Warn("Error de terminacion de hilo al inicializar pagina desktop. Nota: Este error pudo ser causado por Response.Redirect.", tex);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al inicializar pagina desktop.", ex);
                throw;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    configuracionLogic = new ConfiguracionDeSistemaLogic(this.docConfiguracion);
                    this.ConsolidadoFechaInicialTxt.Value = configuracionLogic.ConsolidadoInventarioInicioPeriodo;
                    this.ConsolidadoFechaFinalTxt.Value = configuracionLogic.ConsolidadoInventarioFinalPeriodo;

                    ConsolidadoDeInventarioDeCafeLogic consolidadoinventariologic = new ConsolidadoDeInventarioDeCafeLogic();
                    ReporteConsolidadoDeCafeDeSocios reporteConsolidadoDeCafeSocios = consolidadoinventariologic.GetReporteCafeDeSocios(configuracionLogic.ConsolidadoInventarioInicioPeriodo, configuracionLogic.ConsolidadoInventarioFinalPeriodo);
                    this.TotalIngresadoTxt.Text = reporteConsolidadoDeCafeSocios.TotalIngresado.ToString();
                    this.TotalAjustadoTxt.Text = String.Format("({0})", reporteConsolidadoDeCafeSocios.TotalAjustado.ToString());
                    this.TotalCompradoTxt.Text = String.Format("({0})", reporteConsolidadoDeCafeSocios.TotalComprado.ToString());
                    this.TotalDepositoTxt.Text = reporteConsolidadoDeCafeSocios.TotalDeposito.ToString();

                    ReporteConsolidadoDeCafe reporteConsolidadoDeCafe = consolidadoinventariologic.GetReporteCafeCooperativa(configuracionLogic.ConsolidadoInventarioInicioPeriodo, configuracionLogic.ConsolidadoInventarioFinalPeriodo);

                    this.TotalCoopCompradoTxt.Text = reporteConsolidadoDeCafe.TotalComprado.ToString();
                    this.TotalCoopVendidoTxt.Text = String.Format("({0})", reporteConsolidadoDeCafe.TotalVendido.ToString());
                    this.TotalCoopDepositoTxt.Text = reporteConsolidadoDeCafe.TotalDeposito.ToString();


                    this.maximizarVentanasHdn.Checked = configuracionLogic.VentanasMaximizar;

                    this.WebAssemblyTitle.Text = this.WAssemblyTitle;
                    this.WebAssemblyVersion.Text = this.WAssemblyVersion;

                    this.LogicAssemblyTitle.Text = this.LAssemblyTitle;
                    this.LogicAssemblyVersion.Text = this.LAssemblyVersion;
                }

                string loggedUser = Session["username"] as string;
                this.MyDesktop.StartMenu.Title = loggedUser;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina desktop.", ex);
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

                List<COCASJOL.DATAACCESS.privilegio> privs = usuariologic.GetPrivilegiosDeUsuario(loggedUser);

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
                log.Fatal("Error fatal al remover objetos sin acceso.", ex);
                throw;
            }
        }

        protected void Logout_Click(object sender, DirectEventArgs e)
        {
            try
            {
                // Logout from Authenticated Session
                Session.RemoveAll();
                Session.Abandon();
                this.Response.Redirect("~/Default.aspx");
            }
            catch (System.Threading.ThreadAbortException tex)
            {
                log.Warn("Error de terminacion de hilo al salir de pagina desktop. Nota: Este error pudo ser causado por Response.Redirect.", tex);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al salir de pagina desktop.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void InitialCheckForNotifications()
        {
            try
            {
                string loggedUser = Session["username"] as string;
#if DEBUG
                if (loggedUser == "DEVELOPER")
                    return;
#endif
                NotificacionLogic notificacionlogic = new NotificacionLogic();
                List<COCASJOL.DATAACCESS.notificacion> NotificacionesList = notificacionlogic.GetNotificacionesDeUsuario(loggedUser);

                if (NotificacionesList == null)
                    return;

                var query = from n in NotificacionesList
                            where n.NOTIFICACION_ESTADO.Equals((int)EstadosNotificacion.Notificado)
                            select n;

                if (query.Count() > 0)
                {
                    foreach (COCASJOL.DATAACCESS.notificacion notif in query.ToList<COCASJOL.DATAACCESS.notificacion>())
                    {
                        this.ShowPinnedNotification(notif.NOTIFICACION_TITLE, notif.NOTIFICACION_MENSAJE, notif.NOTIFICACION_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar notificaciones inicial.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void CheckForNotifications()
        {
            try
            {
                configuracionLogic = new ConfiguracionDeSistemaLogic(this.docConfiguracion);
                this.ConsolidadoFechaInicialTxt.Value = configuracionLogic.ConsolidadoInventarioInicioPeriodo;
                this.ConsolidadoFechaFinalTxt.Value = configuracionLogic.ConsolidadoInventarioFinalPeriodo;

                //actualizar reporte consolidado de inventario de cafe
                ConsolidadoDeInventarioDeCafeLogic consolidadoinventariologic = new ConsolidadoDeInventarioDeCafeLogic();
                ReporteConsolidadoDeCafeDeSocios reporteConsolidadoDeCafeSocios = consolidadoinventariologic.GetReporteCafeDeSocios(configuracionLogic.ConsolidadoInventarioInicioPeriodo, configuracionLogic.ConsolidadoInventarioFinalPeriodo);
                this.TotalIngresadoTxt.Text = reporteConsolidadoDeCafeSocios.TotalIngresado.ToString();
                this.TotalAjustadoTxt.Text = String.Format("({0})", reporteConsolidadoDeCafeSocios.TotalAjustado.ToString());
                this.TotalCompradoTxt.Text = String.Format("({0})", reporteConsolidadoDeCafeSocios.TotalComprado.ToString());
                this.TotalDepositoTxt.Text = reporteConsolidadoDeCafeSocios.TotalDeposito.ToString();


                ReporteConsolidadoDeCafe reporteConsolidadoDeCafe = consolidadoinventariologic.GetReporteCafeCooperativa(configuracionLogic.ConsolidadoInventarioInicioPeriodo, configuracionLogic.ConsolidadoInventarioFinalPeriodo);

                this.TotalCoopCompradoTxt.Text = reporteConsolidadoDeCafe.TotalComprado.ToString();
                this.TotalCoopVendidoTxt.Text = String.Format("({0})", reporteConsolidadoDeCafe.TotalVendido.ToString());
                this.TotalCoopDepositoTxt.Text = reporteConsolidadoDeCafe.TotalDeposito.ToString();

                //check for notification

                string loggedUser = Session["username"] as string;

#if DEBUG
                if (loggedUser == "DEVELOPER")
                    return;
#endif

                NotificacionLogic notificacionlogic = new NotificacionLogic();
                List<COCASJOL.DATAACCESS.notificacion> NotificacionesList = notificacionlogic.GetNotificacionesDeUsuario(loggedUser);

                if (NotificacionesList == null)
                    return;

                var query = from n in NotificacionesList
                            where n.NOTIFICACION_ESTADO.Equals((int)EstadosNotificacion.Creado)
                            select n;

                if (query.Count() > 0)
                {
                    foreach (COCASJOL.DATAACCESS.notificacion notif in query.ToList<COCASJOL.DATAACCESS.notificacion>())
                    {
                        this.ShowPinnedNotification(notif.NOTIFICACION_TITLE, notif.NOTIFICACION_MENSAJE, notif.NOTIFICACION_ID);
                        notificacionlogic.ActualizarNotificacion(notif.NOTIFICACION_ID, EstadosNotificacion.Notificado);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar notificaciones.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void MarkAsReadNotification(string NOTIFICACION_ID)
        {
            try
            {
                NotificacionLogic notificacionlogic = new NotificacionLogic();
                notificacionlogic.ActualizarNotificacion(Convert.ToInt32(NOTIFICACION_ID) , EstadosNotificacion.Leido);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al marcar como leida la notificacion.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void DeleteReadNotifications()
        {
            try
            {
                string loggedUser = Session["username"] as string;

#if DEBUG
                if (loggedUser == "DEVELOPER")
                    return;
#endif

                NotificacionLogic notificacionlogic = new NotificacionLogic();
                notificacionlogic.EliminarNotificacionesDeUsuario(loggedUser);
                dsReport_Refresh(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar notificaciones leidas.", ex);   
                throw;
            }
        }

        private void ShowPinnedNotification(string title, string message, int NOTIFICACION_ID)
        {
            try
            {
                Ext.Net.Notification PinnedNotification = Ext.Net.Notification.Show(new Ext.Net.NotificationConfig
                {
                    Title = title,
                    Html = message,
                    Pinned = true,
                    ShowPin = true,
                    Width = 300,
                    Tools = new ToolsCollection
                    {
                        new Tool
                        {
                            Type = ToolType.Save, Handler = "DesktopX.markAsReadNotification('" + NOTIFICACION_ID.ToString() + "');", Qtip="Guardar como leída."
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al mostrar notificacion.", ex);
                throw;
            }
        }

        protected void dsReport_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string loggedUser = Session["username"] as string;

#if DEBUG
                if (loggedUser == "DEVELOPER")
                    return;
#endif

                NotificacionLogic notificacionlogic = new NotificacionLogic();
                this.dsReport.DataSource = notificacionlogic.GetNotificacionesDeUsuario(loggedUser);
                this.dsReport.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar bandeja de notificaciones.", ex);
                throw;
            }
        }

        #region About

        public string WAssemblyTitle
        {
            get
            {
                try
                {
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    if (attributes.Length > 0)
                    {
                        AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                        if (titleAttribute.Title != "")
                        {
                            return titleAttribute.Title;
                        }
                    }
                    return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                }
                catch (Exception ex)
                {
                    log.Fatal("Error fatal al obtener informacion de assembly de aplicacion web.", ex);
                    throw;
                }
            }
        }

        public string WAssemblyVersion
        {
            get
            {
                try
                {
                    return Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
                catch (Exception ex)
                {
                    log.Fatal("Error fatal al obtener informacion de assembly de aplicacion web.", ex);
                    throw;
                }
            }
        }

        public string LAssemblyTitle
        {
            get
            {
                try
                {
                    object[] attributes = Assembly.LoadFrom(Server.MapPath("~/bin/COCASJOL.LOGIC.dll")).GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    if (attributes.Length > 0)
                    {
                        AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                        if (titleAttribute.Title != "")
                        {
                            return titleAttribute.Title;
                        }
                    }
                    return System.IO.Path.GetFileNameWithoutExtension(Assembly.LoadFrom(Server.MapPath("~/bin/COCASJOL.LOGIC.dll")).CodeBase);
                }
                catch (Exception ex)
                {
                    log.Fatal("Error fatal al obtener informacion de assembly de logica de aplicacion.", ex);
                    throw;
                }
            }
        }

        public string LAssemblyVersion
        {
            get
            {
                try
                {
                    return Assembly.LoadFrom(Server.MapPath("~/bin/COCASJOL.LOGIC.dll")).GetName().Version.ToString();
                }
                catch (Exception ex)
                {
                    log.Fatal("Error fatal al obtener informacion de assembly de logica de aplicacion.", ex);
                    throw;
                }
            }
        }

        #endregion
    }
}