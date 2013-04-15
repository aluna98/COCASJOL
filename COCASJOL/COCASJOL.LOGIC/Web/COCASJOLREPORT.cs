using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;
using Ext.Net;
using Microsoft.Reporting.WebForms;

namespace COCASJOL.LOGIC.Web
{
    public class COCASJOLREPORT: COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(COCASJOLREPORT).Name);

        public COCASJOLREPORT() { }

        protected virtual void Report_Execution(object sender, DirectEventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al ejecutar reporte. Metodo sin implementar.", ex);
                throw;
            }
        }

        protected void CreateFileOutput(string fileName, string format, string RDL_Path, ReportDataSource RptDatasource, ReportParameterCollection RptParams)
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
                viewer.LocalReport.ReportPath = Server.MapPath(RDL_Path);
                viewer.LocalReport.SetParameters(RptParams);
                viewer.LocalReport.DataSources.Add(RptDatasource);

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
                log.Fatal("Error fatal al obtener reporte.", ex);
                throw;
            }
        }
    }
}
