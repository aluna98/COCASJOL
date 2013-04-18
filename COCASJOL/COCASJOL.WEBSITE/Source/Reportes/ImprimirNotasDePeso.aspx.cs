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

using log4net;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ImprimirNotasDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        int NOTAS_ID = 0;

        private static ILog log = LogManager.GetLogger(typeof(ImprimirNotasDePeso).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    string strNOTAS_ID = Request.QueryString["NOTAS_ID"];

                    this.NOTAS_ID = string.IsNullOrEmpty(strNOTAS_ID) ? 0 : Convert.ToInt32(strNOTAS_ID);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar reporte de notas de peso.", ex);
                throw;
            }
        }

        protected void ReportViewer1_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                ReporteLogic rpt = new ReporteLogic();

                List<nota_detalle> detallesLst = rpt.GetNotasDetalle(NOTAS_ID);

                ReportDataSource dataSource = new ReportDataSource("NotasDetalleDataSet", detallesLst);
                e.DataSources.Add(dataSource);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar subreporte de detalles de notas de peso.", ex);
                throw;
            }
        }
    }
}