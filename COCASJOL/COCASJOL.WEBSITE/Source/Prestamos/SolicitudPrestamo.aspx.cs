using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COCASJOL.LOGIC;
using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Prestamos;

namespace COCASJOL.Website.Source.Prestamos
{
    public partial class SolicitudPrestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SolicitudesSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            SolicitudesLogic prestamo = new SolicitudesLogic();
            var store1 = this.SolicitudesGriP.GetStore();
            store1.DataSource = prestamo.getData();
            store1.DataBind();
        }
    }
}