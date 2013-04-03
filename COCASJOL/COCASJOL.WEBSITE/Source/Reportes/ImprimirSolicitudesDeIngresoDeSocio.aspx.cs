using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Reportes;
using Microsoft.Reporting.WebForms;

using System.Globalization;
using log4net;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ImprimirSolicitudesDeIngresoDeSocio : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(ImprimirSolicitudesDeIngresoDeSocio).Name);

        string SOCIOS_ID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    string strSOCIOS_ID = Request.QueryString["SOCIOS_ID"];

                    this.SOCIOS_ID = string.IsNullOrEmpty(strSOCIOS_ID) ? "" : strSOCIOS_ID;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de reporte de solicitudes de ingreso de socios.", ex);
                throw;
            }
        }

        protected void ReportViewer1_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                ReporteLogic rpt = new ReporteLogic();

                List<beneficiario_x_socio> beneficiariosLst = rpt.GetBeneficiariosDeSocio(SOCIOS_ID);

                ReportDataSource dataSource = new ReportDataSource("BeneficiariosDataSet", beneficiariosLst);
                e.DataSources.Add(dataSource);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar subreporte de beneficiarios de socio.", ex);
                throw;
            }
        }
    }
}