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

        protected void ExportToExcelBtn_Click(object sender, DirectEventArgs e)
        {
            try
            {
                this.CreateFileOutput("MovimientosDeInventarioDeCafeDeSocios", "Excel");
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al exportar reporte Excel.", ex);
                throw;
            }
        }

        protected void ExportToPDFBtn_Click(object sender, DirectEventArgs e)
        {
            try
            {
                this.CreateFileOutput("MovimientosDeInventarioDeCafeDeSocios", "PDF");
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al exportar reporte PDF.", ex);
                throw;
            }
        }

        private void CreateFileOutput(string fileName, string format)
        {
            try
            {
                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/resources/rdlcs/ReporteMovimientosDeInventarioDeCafeDeSocios.rdlc");

                ReportParameter param = new ReportParameter();
                param = new ReportParameter("parReportType", "m"); // m-> default, s->socio, c->clasificacion cafe, d->descripcion, f->fecha, cp->creado por, fc->fecha creacion

                viewer.LocalReport.SetParameters(param);

                ReporteLogic reporteLogic = new ReporteLogic();

                List<reporte_movimientos_de_inventario_de_cafe> MovimientosDeInventarioDeCafeDeSociosLst = reporteLogic.GetMovimientosDeInventarioDeCafeDeSocio
                    (0, default(DateTime), default(DateTime), default(DateTime), "", "", "", -1, -1, -1, -1, -1, -1, "", default(DateTime));

                ReportDataSource datasourceMovimientoInventarioCafeSocios = new ReportDataSource("MovimientosDeInventarioDeCafeDeSociosDataSet", MovimientosDeInventarioDeCafeDeSociosLst);

                viewer.LocalReport.DataSources.Add(datasourceMovimientoInventarioCafeSocios);

                byte[] bytes = viewer.LocalReport.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);


                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
                Response.BinaryWrite(bytes); // create the file
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener plantilla de importacion de socios.", ex);
                throw;
            }
        }
    }
}