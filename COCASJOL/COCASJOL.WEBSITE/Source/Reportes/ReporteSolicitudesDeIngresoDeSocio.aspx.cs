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

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ReporteSolicitudesDeIngresoDeSocio : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        string SOCIOS_ID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string strSOCIOS_ID = Request.QueryString["SOCIOS_ID"];

                this.SOCIOS_ID = string.IsNullOrEmpty(strSOCIOS_ID) ? "" : strSOCIOS_ID;
            }
        }

        protected void ReportViewer1_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                ReporteLogic rpt = new ReporteLogic();

                List<beneficiario_x_socio> beneficiariosLst = rpt.GetBeneficiariosDeSocio(SOCIOS_ID);

                ReportDataSource dataSource = new ReportDataSource("DataSet2", beneficiariosLst);
                e.DataSources.Add(dataSource);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}