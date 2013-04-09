using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC.Inventario.Ingresos;
using COCASJOL.LOGIC.Web;

using log4net;

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class EstadosNotaDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(EstadosNotaDePeso).Name);

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
                log.Fatal("Error fatal al cargar pagina de estados de nota de peso.", ex);
                throw;
            }
        }

        protected void EstadosNotaDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
                log.Fatal("Error fatal al cargar estados de nota de peso.", ex);
                throw;
            }
        }

        protected void EstadosNotaPadreSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();

                int siguiente = this.EditPadreIdCmb.Text == "" ? 0 : Convert.ToInt32(this.EditPadreIdCmb.Text);

                this.EstadosNotaPadreSt.DataSource = estadologic.GetEstadosNotaDePesoSinAsignar(siguiente);
                this.EstadosNotaPadreSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }
    }
}