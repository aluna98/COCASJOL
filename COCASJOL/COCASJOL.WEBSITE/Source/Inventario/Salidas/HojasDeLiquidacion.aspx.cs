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
using COCASJOL.LOGIC.Socios;

using log4net;

namespace COCASJOL.WEBSITE.Source.Inventario.Salidas
{
    public partial class HojasDeLiquidacion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(HojasDeLiquidacion).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    this.CantidadDeAportacionExtraordParaCoopHdn.Text = this.Variables["HOJAS_APORTACIONEXTRACANT"];
                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;//necesario actualizarlo siempre, para el tracking correcto
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de hojas de liquidacion.", ex);
                throw;
            }
        }

        protected void HojasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic configLogic = new COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic(this.docConfiguracion);
                    if (configLogic.VentanasCargarDatos == true)
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar hojas de liquidacion.", ex);
                throw;
            }
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar cantidad de libras de inventario de cafe de socio.", ex);
                throw;
            }
        }

        #endregion

        [DirectMethod(RethrowException=true)]
        public void GetCantidadDeInventarioDeCafeDeSocio()
        {
            try
            {
                string SOCIOS_ID = this.AddSociosIdTxt.Text;
                string TxtCLASIFICACIONES_CAFE_ID = this.AddClasificacionCafeCmb.Text;

                int CLASIFICACIONES_CAFE_ID = string.IsNullOrEmpty(TxtCLASIFICACIONES_CAFE_ID) ? 0 : Convert.ToInt32(TxtCLASIFICACIONES_CAFE_ID);

                if (string.IsNullOrEmpty(SOCIOS_ID) || CLASIFICACIONES_CAFE_ID == 0)
                    return;

                InventarioDeCafeLogic inventarioliquidacionlogic = new InventarioDeCafeLogic();
                decimal inventarioSocio = inventarioliquidacionlogic.GetInventarioDeCafeDeSocio(SOCIOS_ID, CLASIFICACIONES_CAFE_ID);
                this.AddInventarioCafeTxt.Value = inventarioSocio;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener cantidad de inventario de cafe de socio.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void LoadCapitalizacionXRetencion()
        {
            try
            {
                this.AddCapitalizacionXRetencionTxt.Text = this.Variables["HOJAS_CAPITALIZACIONRETN"];
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar capitalizacion por retencion de variables de entorno.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void LoadCuotaDeIngresoYGastosAdmn()
        {
            try
            {
                string socios_id = this.AddSociosIdTxt.Text;

                if (SociosLogic.EsNuevo(socios_id) == true)
                {
                    this.AddCuotaIngresoTxt.Text = this.Variables["HOJAS_CUOTAINGRESO"];
                    this.AddGastosAdminTxt.Text = this.Variables["HOJAS_GASTOADMINISTRACION"];
                }
                else
                {
                    this.AddCuotaIngresoTxt.Text = (0).ToString();
                    this.AddGastosAdminTxt.Text = (0).ToString();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar cuota de ingreso y gastos de administracion de variables de entorno.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void LoadAportacionesOrdinariaYExtraordinaria()
        {
            try
            {
                string socios_id = this.AddSociosIdTxt.Text;

                if (SociosLogic.DebePagarAportacionOrdinaria(socios_id))
                    this.AddAportacionOrdinariaTxt.Text = this.Variables["HOJAS_APORTACIONORD"];
                else
                    this.AddAportacionOrdinariaTxt.Text = (0).ToString();



                if (SociosLogic.DebePagarAportacionExtraordinaria(socios_id))
                    this.AddAportacionExtraOrdinariaTxt.Text = this.Variables["HOJAS_APORTACIONEXTRAORD"];
                else
                    this.AddAportacionExtraOrdinariaTxt.Text = (0).ToString();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar aportaciones ordinaria y extraordinaria de variables de entorno.", ex);
                throw;
            }
        }

        private static object lockObj = new object();

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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar reporte consolidado de inventario de cafe.", ex);
                throw;
            }
        }
    }
}