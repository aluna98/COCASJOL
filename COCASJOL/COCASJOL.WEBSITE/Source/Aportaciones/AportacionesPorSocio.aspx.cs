using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Aportaciones;

using log4net;
using Microsoft.Reporting.WebForms;

namespace COCASJOL.WEBSITE.Source.Aportaciones
{
    public partial class AportacionesPorSocio : COCASJOL.LOGIC.Web.COCASJOLREPORT
    {
        private static ILog log = LogManager.GetLogger(typeof(AportacionesPorSocio).Name);

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
                log.Fatal("Error fatal al cargar pagina de aportaciones por socio.", ex);
                throw;
            }
        }

        protected void AportacionesDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
                log.Fatal("Error fatal al cargar aportaciones por socio.", ex);
                throw;
            }
        }

        protected void Export_PDFBtn_Click(object sender, DirectEventArgs e)
        {
            string formatoSalida = "";
            try
            {
                COCASJOL.LOGIC.Aportaciones.AportacionLogic reporteLogic = new COCASJOL.LOGIC.Aportaciones.AportacionLogic();           

                List<COCASJOL.DATAACCESS.reporte_total_aportaciones_por_socio> ReporteAportacionesDeSociosLst = reporteLogic.GetAportaciones
                    (this.f_SOCIOS_ID.Text,
                    this.f_SOCIOS_NOMBRE_COMPLETO.Text,
                    string.IsNullOrEmpty(this.f_APORTACIONES_SALDO_TOTAL.Text) ? -1 : Convert.ToInt32(this.f_APORTACIONES_SALDO_TOTAL.Text),
                    "",
                    default(DateTime));

                ReportDataSource datasourceAportacionesSocios = new ReportDataSource("ResumenAportacionesPorSocioDataSet", ReporteAportacionesDeSociosLst);

                ReportParameterCollection reportParamCollection = new ReportParameterCollection();

                formatoSalida = "PDF";

                string rdlPath = "~/resources/rdlcs/ReportResumenAportacionesPorSocio.rdlc";

                this.CreateFileOutput("ReporteResumenAportacionesDeSocios", formatoSalida, rdlPath, datasourceAportacionesSocios, reportParamCollection);
            }
            catch (Exception ex)
            {
                log.Fatal(string.Format("Error fatal al generar reporte. Formato de salida: {0}", formatoSalida), ex);
                throw;
            }
        }

        protected void Export_ExcelBtn_Click(object sender, DirectEventArgs e)
        {
            string formatoSalida = "";
            try
            {
                COCASJOL.LOGIC.Aportaciones.AportacionLogic reporteLogic = new COCASJOL.LOGIC.Aportaciones.AportacionLogic();

                List<COCASJOL.DATAACCESS.reporte_total_aportaciones_por_socio> ReporteAportacionesDeSociosLst = reporteLogic.GetAportaciones
                    (this.f_SOCIOS_ID.Text,
                    this.f_SOCIOS_NOMBRE_COMPLETO.Text,
                    string.IsNullOrEmpty(this.f_APORTACIONES_SALDO_TOTAL.Text) ? -1 : Convert.ToInt32(this.f_APORTACIONES_SALDO_TOTAL.Text),
                    "",
                    default(DateTime));

                ReportDataSource datasourceAportacionesSocios = new ReportDataSource("ResumenAportacionesPorSocioDataSet", ReporteAportacionesDeSociosLst);

                ReportParameterCollection reportParamCollection = new ReportParameterCollection();

                formatoSalida = "EXCEL";

                string rdlPath = "~/resources/rdlcs/ReportResumenAportacionesPorSocio.rdlc";

                this.CreateFileOutput("ReporteResumenAportacionesDeSocios", formatoSalida, rdlPath, datasourceAportacionesSocios, reportParamCollection);
            }
            catch (Exception ex)
            {
                log.Fatal(string.Format("Error fatal al generar reporte. Formato de salida: {0}", formatoSalida), ex);
                throw;
            }
        }
    }
}