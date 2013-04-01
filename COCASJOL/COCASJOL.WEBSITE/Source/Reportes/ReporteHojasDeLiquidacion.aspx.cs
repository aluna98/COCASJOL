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
    public partial class ReporteHojasDeLiquidacion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(ReporteHojasDeLiquidacion).Name);

        int LIQUIDACIONES_ID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    string strLIQUIDACIONES_ID = Request.QueryString["LIQUIDACIONES_ID"];

                    this.LIQUIDACIONES_ID = string.IsNullOrEmpty(strLIQUIDACIONES_ID) ? 0 : Convert.ToInt32(strLIQUIDACIONES_ID);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar reporte de hojas de liquidacion.", ex);
                throw;
            }
        }
    }
}