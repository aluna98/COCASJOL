using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Prestamos;
using COCASJOL.LOGIC.Socios;
using COCASJOL.LOGIC.Aportaciones;

using Ext.Net;
using log4net;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ReportePrestamosPorSocio : COCASJOLBASE
    {

        private static ILog log = LogManager.GetLogger(typeof(ReportePrestamosPorSocio).Name);

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                this.RM1.DirectMethodNamespace = "DirectX";
                SociosSt_Reload(null, null);
                if (!X.IsAjaxRequest)
                {

                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de solicitudes de prestamos.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void RefrescarReporte()
        {
            try
            {
                SolicitudesSt_Reload(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar nota de peso en pesaje.", ex);
                throw;
            }
        }

        protected void NombreSocio(object sender, DirectEventArgs e)
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                socio nuevo = logica.getSocio(AddSociosIdTxt.Text);
                AddNombreTxt.Text = nuevo.SOCIOS_PRIMER_NOMBRE + " " + nuevo.SOCIOS_PRIMER_APELLIDO;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar Solicitudes por Socio.", ex);
                throw;
            }
        }

        protected void Reporte_Refresh(object sender, DirectEventArgs e)
        {
            try
            {
                SolicitudesSt_Reload(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar Solicitudes por Socio.", ex);
                throw;
            }
        }

        protected void SociosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                SolicitudesLogic solicitud = new SolicitudesLogic();
                ComboBoxSt.DataSource = solicitud.getSocios();
                ComboBoxSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar socios.", ex);
                throw;
            }
        }

        protected void SolicitudesSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                SolicitudesLogic prestamo = new SolicitudesLogic();
                var store1 = this.PrestamosGrid.GetStore();
                List<solicitud_prestamo> Resultado; 
                if (AddSociosIdTxt.Text != "")
                {
                    if (EditCmbPrestamo.Text != "")
                        Resultado = prestamo.getViewPrestamosXSocioXPrestamo(AddSociosIdTxt.Text, Convert.ToInt32(EditCmbPrestamo.Text));
                    else
                        Resultado = prestamo.getViewPrestamosXSocio(AddSociosIdTxt.Text);
                }
                else
                {
                    if (EditCmbPrestamo.Text != "")
                    {
                        Resultado = prestamo.getViewPrestamosXTipoPrestamo(Convert.ToInt32(EditCmbPrestamo.Text));
                    }
                    else
                    {
                        Resultado = prestamo.getViewPrestamosXSocio();
                    }
                }
                Decimal Monto = Decimal.Parse(Resultado.Select(c => c.SOLICITUDES_MONTO).Sum().ToString());
                MontoTotal.Text = Monto.ToString();
                store1.DataSource = Resultado;
                store1.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("error fatal al cargar solicitudes de prestamos.", ex);
                throw;
            }
        }
    }
}