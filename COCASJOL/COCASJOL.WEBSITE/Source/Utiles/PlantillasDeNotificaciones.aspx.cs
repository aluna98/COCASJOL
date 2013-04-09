using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Utiles;
using COCASJOL.LOGIC.Web;

using log4net;

namespace COCASJOL.WEBSITE.Source.Utiles
{
    public partial class PlantillasDeNotificaciones : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(PlantillasDeNotificaciones).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {

                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de plantillas de notificaciones.", ex);
                throw;
            }
        }

        protected void PlantillaDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic configLogic = new COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic(this.docConfiguracion);
                    if (configLogic.VentanasCargarDatos == true)
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar plantillas de notificaciones.", ex);
                throw;
            }
        }

        protected void FormatKeysSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string formatKey = this.EditLlaveTxt.Text;
                PlantillaLogic plantillalogic = new PlantillaLogic();

                this.FormatKeysSt.DataSource = plantillalogic.GetFormatKeys(formatKey);
                this.FormatKeysSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar llaves de formato para plantilla de notificacion.", ex);
                throw;
            }
        }
    }
}