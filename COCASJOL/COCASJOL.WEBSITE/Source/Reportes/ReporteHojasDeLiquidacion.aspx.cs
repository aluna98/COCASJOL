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
    public partial class ReporteHojasDeLiquidacion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        int LIQUIDACIONES_ID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string strLIQUIDACIONES_ID = Request.QueryString["LIQUIDACIONES_ID"];

                this.LIQUIDACIONES_ID = string.IsNullOrEmpty(strLIQUIDACIONES_ID) ? 0 : Convert.ToInt32(strLIQUIDACIONES_ID);
            }
        }

        protected void ObjectDataSource2_OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true; DateTime.Now.ToString("dd dddd MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-HN"));
        }
    }
}