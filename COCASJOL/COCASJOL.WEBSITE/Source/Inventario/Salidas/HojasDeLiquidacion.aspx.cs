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
using COCASJOL.LOGIC.Inventario;
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

        #region Validacion

        protected void AddTotalLibrasTxt_Blur(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string cantidadInventario = this.AddInventarioCafeTxt.Text;
                string cantidadLibras = this.AddTotalLibrasTxt.Text;

                int CantidadInventarioDisponible = string.IsNullOrEmpty(cantidadInventario) ? 0 : Convert.ToInt32(cantidadInventario);
                int CantidadLibrasLiquidar = string.IsNullOrEmpty(cantidadLibras) ? 0 : Convert.ToInt32(cantidadLibras);

                if (CantidadLibrasLiquidar <= CantidadInventarioDisponible)
                    e.Success = true;
                else
                {
                    e.Success = false;
                    e.ErrorMessage = "La cantidad sobrepasa el inventario de café disponible por " + (CantidadLibrasLiquidar - CantidadInventarioDisponible) + " libras.";
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        private static object lockObj = new object();

        [DirectMethod(RethrowException=true)]
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
                decimal inventarioSocio = inventarioliquidacionlogic.GetInventarioDeCafe(SOCIOS_ID, CLASIFICACIONES_CAFE_ID, false);
                this.AddInventarioCafeTxt.Value = inventarioSocio;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [DirectMethod]
        public void LoadCapitalizacionXRetencion()
        {
            try
            {
                Dictionary<string, string> variables = this.GetVariables();
                if (!this.ValidarVariables(variables))
                    return;

                this.AddCapitalizacionXRetencionTxt.Text = variables["HOJAS_CAPITALIZACIONRETN"];
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
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