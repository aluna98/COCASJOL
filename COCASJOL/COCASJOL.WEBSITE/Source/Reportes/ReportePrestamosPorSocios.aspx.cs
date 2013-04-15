using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Reportes;

using Ext.Net;
using log4net;
using Microsoft.Reporting.WebForms;

namespace COCASJOL.WEBSITE.Source.Reportes
{
    public partial class ReportePrestamosPorSocios : COCASJOL.LOGIC.Web.COCASJOLREPORT
    {

        private static ILog log = LogManager.GetLogger(typeof(ReportePrestamosPorSocios).Name);

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
                log.Fatal("Error fatal al cargar pagina de solicitudes de prestamos.", ex);
                throw;
            }
        }

        protected override void Report_Execution(object sender, DirectEventArgs e)
        {
            string formatoSalida = "";
            try
            {
                ReporteLogic reporteLogic = new ReporteLogic();

                int prestamos_id = string.IsNullOrEmpty(this.f_PRESTAMOS_ID.Text) ? 0 : Convert.ToInt32(this.f_PRESTAMOS_ID.Text);

                List<solicitud_prestamo> ReportePrestamosXSociosLst = reporteLogic.GetPrestamosXSocio
                    (this.f_SOCIOS_ID.Text,
                    prestamos_id);

                ReportDataSource datasourceMovimientoInventarioCafeSocios = new ReportDataSource("PrestamosXSocioDataSet", ReportePrestamosXSociosLst);


                ReportParameterCollection reportParamCollection = new ReportParameterCollection();
                reportParamCollection.Add(new ReportParameter("parGroupBySocios", this.g_SOCIOS_ID.Checked.ToString()));
                reportParamCollection.Add(new ReportParameter("parGroupByPrestamo", this.g_PRESTAMOS_ID.Checked.ToString()));

                formatoSalida = this.f_SALIDA_FORMATO.Text;

                string rdlPath = "~/resources/rdlcs/ReportePrestamosPorSocios.rdlc";

                this.CreateFileOutput("ReportePrestamosPorSocios", formatoSalida, rdlPath, datasourceMovimientoInventarioCafeSocios, reportParamCollection);
            }
            catch (Exception ex)
            {
                log.Fatal(string.Format("Error fatal al generar reporte. Formato de salida: {0}", formatoSalida), ex);
                throw;
            }
        }

        //protected void SolicitudesSt_Reload(object sender, StoreRefreshDataEventArgs e)
        //{
        //    try
        //    {
        //        SolicitudesLogic prestamo = new SolicitudesLogic();
        //        var store1 = this.PrestamosGrid.GetStore();
        //        List<solicitud_prestamo> Resultado; 
        //        if (AddSociosIdTxt.Text != "")
        //        {
        //            if (EditCmbPrestamo.Text != "")
        //                Resultado = prestamo.getViewPrestamosXSocioXPrestamo(AddSociosIdTxt.Text, Convert.ToInt32(EditCmbPrestamo.Text));
        //            else
        //                Resultado = prestamo.getViewPrestamosXSocio(AddSociosIdTxt.Text);
        //        }
        //        else
        //        {
        //            if (EditCmbPrestamo.Text != "")
        //            {
        //                Resultado = prestamo.getViewPrestamosXTipoPrestamo(Convert.ToInt32(EditCmbPrestamo.Text));
        //            }
        //            else
        //            {
        //                Resultado = prestamo.getViewPrestamosXSocio();
        //            }
        //        }
        //        Decimal Monto = Decimal.Parse(Resultado.Select(c => c.SOLICITUDES_MONTO).Sum().ToString());
        //        MontoTotal.Text = Monto.ToString();
        //        store1.DataSource = Resultado;
        //        store1.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal("error fatal al cargar solicitudes de prestamos.", ex);
        //        throw;
        //    }
        //}
    }
}