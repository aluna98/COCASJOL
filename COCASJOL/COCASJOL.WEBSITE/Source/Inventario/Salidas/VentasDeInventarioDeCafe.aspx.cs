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

namespace COCASJOL.WEBSITE.Source.Inventario.Salidas
{
    public partial class VentasDeInventarioDeCafe : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
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
            catch (Exception)
            {
                //log
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
                string SOCIOS_ID = this.AddSociosIdTxt.Text;
                string TxtCLASIFICACIONES_CAFE_ID = this.AddClasificacionCafeCmb.Text;

                int CLASIFICACIONES_CAFE_ID = string.IsNullOrEmpty(TxtCLASIFICACIONES_CAFE_ID) ? 0 : Convert.ToInt32(TxtCLASIFICACIONES_CAFE_ID);

                if (string.IsNullOrEmpty(SOCIOS_ID) || CLASIFICACIONES_CAFE_ID == 0)
                    return;

                InventarioDeCafeLogic inventarioliquidacionlogic = new InventarioDeCafeLogic();
                decimal inventarioSocio = inventarioliquidacionlogic.GetInventarioDeCafe(SOCIOS_ID, CLASIFICACIONES_CAFE_ID);
                this.AddInventarioDeCafeCantidadTxt.Value = inventarioSocio;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}