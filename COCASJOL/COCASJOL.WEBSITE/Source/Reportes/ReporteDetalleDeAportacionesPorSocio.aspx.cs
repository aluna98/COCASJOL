using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Reportes;

using Ext.Net;
using log4net;
using Microsoft.Reporting.WebForms;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ReporteDetalleDeAportacionesPorSocio : COCASJOL.LOGIC.Web.COCASJOLREPORT
    {
        private static ILog log = LogManager.GetLogger(typeof(ReporteDetalleDeAportacionesPorSocio).Name);

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
                log.Fatal("Error fatal al cargar reporte detalle de aportaciones por socio.", ex);
                throw;
            }
        }

        protected void Report_Execution(object sender, DirectEventArgs e)
        {
            string formatoSalida = "";
            try
            {
                ReporteLogic reporteLogic = new ReporteLogic();

                List<reporte_detalle_de_aportaciones_por_socio> ReporteDetalleAportacionesXSocioLst = reporteLogic.GetDetalleAportacionesPorSocio
                    (this.f_SOCIOS_ID.Text,
                    this.f_DESCRIPCION.Text,
                    this.f_FECHA.Text,
                    this.f_DATE_FROM.SelectedDate,
                    this.f_DATE_TO.SelectedDate,
                    this.f_CREADO_POR.Text,
                    this.f_FECHA_CREACION.SelectedDate);

                ReportDataSource datasourceDetalleAportacionesXSocio = new ReportDataSource("DetalleAportacionesPorSocioDataSet", ReporteDetalleAportacionesXSocioLst);

                ReportParameterCollection reportParamCollection = new ReportParameterCollection();
                reportParamCollection.Add(new ReportParameter("parGroupBySocios", this.g_SOCIOS_ID.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByDescripcion", this.g_DESCRIPCION.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByFecha", this.g_FECHA.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByCreadoPor", this.g_CREADO_POR.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByFechaCreacion", this.g_FECHA_CREACION.Checked.ToString()));

                formatoSalida = this.f_SALIDA_FORMATO.Text;

                string rdlPath = "~/resources/rdlcs/ReporteDetalleDeAportacionesPorSocio.rdlc";

                this.CreateFileOutput("ReporteDetalleAportacionesPorSocio", formatoSalida, rdlPath, datasourceDetalleAportacionesXSocio, reportParamCollection);
            }
            catch (Exception ex)
            {
                log.Fatal(string.Format("Error fatal al generar reporte. Formato de salida: {0}", formatoSalida), ex);
                throw;
            }
        }
    }
}