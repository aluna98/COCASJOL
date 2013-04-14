using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Reportes;
using Microsoft.Reporting.WebForms;

using System.Globalization;
using log4net;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ImprimirSolicitudDePrestamo : System.Web.UI.Page
    {
        private static ILog log = LogManager.GetLogger(typeof(ImprimirSolicitudDePrestamo).Name);

        int SOLICITUDES_ID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    string strSOLICITUDES_ID = Request.QueryString["SOLICITUDES_ID"];

                    this.SOLICITUDES_ID = string.IsNullOrEmpty(strSOLICITUDES_ID) ? 0 : Convert.ToInt32(strSOLICITUDES_ID);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de reporte de solicitudes de prestamo.", ex);
                throw;
            }
        }

        protected void ReportViewer1_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                ReporteLogic rpt = new ReporteLogic();

                List<referencia_x_solicitud> referenciasPersonalesLst = rpt.GetReferenciasXSolicitud(SOLICITUDES_ID, "Personal");
                List<referencia_x_solicitud> refrenciasComercialesLst = rpt.GetReferenciasXSolicitud(SOLICITUDES_ID, "Comercial");
                List<aval_x_solicitud> avaleslst = rpt.GetAvalesXSolicitud(SOLICITUDES_ID);

                ReportDataSource dataSourceReferenciasPersonales = new ReportDataSource("ReferenciasPersonalesDataSet", referenciasPersonalesLst);
                ReportDataSource dataSourceReferenciasComerciales = new ReportDataSource("ReferenciasComercialesDataSet", refrenciasComercialesLst);
                ReportDataSource dataSourceAvales = new ReportDataSource("AvalesDataSet", avaleslst);

                e.DataSources.Add(dataSourceReferenciasPersonales);
                e.DataSources.Add(dataSourceReferenciasComerciales);
                e.DataSources.Add(dataSourceAvales);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar subreporte de referencias, avales de solicitud de prestamo.", ex);
                throw;
            }
        }
    }
}