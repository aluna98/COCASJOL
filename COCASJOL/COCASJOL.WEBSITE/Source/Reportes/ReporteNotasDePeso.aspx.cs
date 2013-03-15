﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Reportes;
using Microsoft.Reporting.WebForms;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ReporteNotasDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        int NOTAS_ID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string strNOTAS_ID = Request.QueryString["NOTAS_ID"];

                 this.NOTAS_ID = string.IsNullOrEmpty(strNOTAS_ID) ? 0 : Convert.ToInt32(strNOTAS_ID);
            }
        }

        protected void ObjectDataSource2_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        protected void ReportViewer1_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                ReporteLogic rpt = new ReporteLogic();

                List<nota_detalle> detallesLst = rpt.GetNotasDetalle(NOTAS_ID);

                ReportDataSource dataSource = new ReportDataSource("DataSet2", detallesLst);
                e.DataSources.Add(dataSource);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}