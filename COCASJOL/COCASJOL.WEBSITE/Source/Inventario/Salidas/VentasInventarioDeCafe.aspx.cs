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
using COCASJOL.LOGIC.Inventario;
using COCASJOL.LOGIC.Inventario.Salidas;

using log4net;

namespace COCASJOL.WEBSITE.Source.Inventario.Salidas
{
    public partial class VentasInventarioDeCafe : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(VentasInventarioDeCafe).Name);

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
                log.Fatal("Error fatal al cargar pagina de ventas de inventario de cafe.", ex);
                throw;
            }
        }

        protected void VentaInventarioDeCafeDs_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        [DirectMethod(RethrowException = true)]
        public void GetCantidadDeInventarioDeCafe()
        {
            try
            {
                string TxtCLASIFICACIONES_CAFE_ID = this.AddClasificacionCafeCmb.Text;

                int CLASIFICACIONES_CAFE_ID = string.IsNullOrEmpty(TxtCLASIFICACIONES_CAFE_ID) ? 0 : Convert.ToInt32(TxtCLASIFICACIONES_CAFE_ID);

                if (CLASIFICACIONES_CAFE_ID == 0)
                    return;

                InventarioDeCafeLogic inventarioliquidacionlogic = new InventarioDeCafeLogic();
                decimal inventario = inventarioliquidacionlogic.GetInventarioDeCafe(CLASIFICACIONES_CAFE_ID);
                this.AddInventarioDeCafeCantidadTxt.Value = inventario;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener cantidad de inventario de cafe.", ex);
                throw;
            }
        }
    }
}