using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Web;
using log4net;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class MovimientosDeInventarioDeCafe : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(MovimientosDeInventarioDeCafe).Name);

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
                log.Fatal("Error fatal al cargar reporte de movimientos de inventario de cafe de socios.", ex);
                throw;
            }
        }

        protected void MovimientoDeInventarioCafeDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //if (!this.IsPostBack)
            //    e.Cancel = true;
        }
    }
}