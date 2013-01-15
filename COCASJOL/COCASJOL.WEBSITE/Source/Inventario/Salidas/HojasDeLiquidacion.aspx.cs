using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Inventario.Salidas;

namespace COCASJOL.WEBSITE.Source.Inventario.Salidas
{
    public partial class HojasDeLiquidacion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {

                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;//necesario actualizarlo siempre, para el tracking correcto
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void HojasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        private static object lockObj = new object();

        [DirectMethod(RethrowException=true)]
        public void UpdateReporteConsolidadoDeInventarioDeCafe()
        {
            try
            {
                lock (lockObj)
                {
                    COCASJOL.LOGIC.Reportes.ConsolidadoDeInventarioDeCafeLogic consolidadoinventariologic = new LOGIC.Reportes.ConsolidadoDeInventarioDeCafeLogic();
                    Application["ReporteConsolidadoDeCafe"] = consolidadoinventariologic.GetReporte();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}