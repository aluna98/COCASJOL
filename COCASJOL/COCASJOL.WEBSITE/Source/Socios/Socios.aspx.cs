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

using COCASJOL.LOGIC.Socios;
using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Seguridad;

using log4net;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace COCASJOL.WEBSITE.Socios
{
    public partial class Socios : COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(Socios).Name);

        public string id; 
        public bool confirm;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                confirm = false;
                this.ResourceManager1.DirectMethodNamespace = "CompanyX";

                if (!X.IsAjaxRequest)
                {
                    COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic configLogic = new COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic(this.docConfiguracion);
                    if (configLogic.VentanasCargarDatos == true)
                    {
                        this.SociosSt_Reload(null, null);
                    }

                    this.ImportarSociosBtn.Hidden = !configLogic.SociosImportacion;
                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de socios.", ex);
                throw;
            }
        }

        private static object lockObj = new object();

        protected void UploadClick(object sender, DirectEventArgs e)
        {
            try
            {
                if (this.FileUploadField1.HasFile)
                {
                    string loggedUsr = Session["username"] as string;

                    COCASJOL.LOGIC.Utiles.ImportarExcelLogic excelImport = new LOGIC.Utiles.ImportarExcelLogic();

                    string extension = Path.GetExtension(this.FileUploadField1.FileName);

                    string uploadSavePath = System.Configuration.ConfigurationManager.AppSettings.Get("uploadSavePath");
                    string uploadNameSocios = System.Configuration.ConfigurationManager.AppSettings.Get("uploadNameSocios");

                    string savePath = Server.MapPath(uploadSavePath) + uploadNameSocios + extension;

                    string msg = "";

                    lock (lockObj)
                    {
                        this.FileUploadField1.PostedFile.SaveAs(savePath);
                         msg = excelImport.SociosCargarDatos(savePath, loggedUsr);
                    }
                    this.BasicForm.Reset();
                    X.Msg.Alert("Importar Socios", msg).Show();
                    this.SociosSt_Reload(null, null);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al guardar archivo subido.", ex);
                throw;
            }
        }

        protected void GetImportTemplateClick(object sender, DirectEventArgs e)
        {
            try
            {
                string uploadNameSocios = System.Configuration.ConfigurationManager.AppSettings.Get("uploadNameSocios");
                this.CreateExcel(uploadNameSocios);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener plantilla de importacion de socios.", ex);
                throw;
            }
        }

        private void CreateExcel(string fileName)
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
                viewer.LocalReport.ReportPath = Server.MapPath("~/resources/rdlcs/Socios.rdlc");

                List<socio> socios = new List<socio>();

                ReportDataSource datasourceSocios = new ReportDataSource("SociosDataSet", socios);

                viewer.LocalReport.DataSources.Add(datasourceSocios);


                byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


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

        [DirectMethod(RethrowException=true)]
        public void DoConfirmDisable()
        {
            try
            {
                X.Msg.Confirm("Message", "Desea deshabilitar el socio?", new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            Handler = "CompanyX.DoYesDisable()",
                            Text = "Aceptar"
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Handler = "CompanyX.DoNo()",
                            Text = "Cancelar"
                        }
                    }).Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al mostrar mensaje de confirmacion de deshabilitacion de socio.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoConfirmEnable()
        {
            try
            {
                X.Msg.Confirm("Message", "Desea habilitar el socio?", new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            Handler = "CompanyX.DoYesEnable()",
                            Text = "Aceptar"
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Handler = "CompanyX.DoNo()",
                            Text = "Cancelar"
                        }
                    }).Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al mostrar mensaje de confirmacion de habilitacion de socio.", ex);
                throw;
            }

        }

        [DirectMethod(RethrowException=true)]
        public void RefrescarBeneficiarios()
        {
            try
            {
                Beneficiarios_Refresh(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar beneficiarios.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void RefreshGrid()
        {
            try
            {
                SociosSt_Reload(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar socios.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void ActualizarBeneficiarioBtn_Click()
        {
            try
            {
                SociosLogic socios = new SociosLogic();
                if (socios.CienPorciento(this.EditsocioIdTxt.Text, Convert.ToInt32(this.AddBenefPorcentaje.Text)))
                {
                    socios.ActualizarBeneficiario(this.EditsocioIdTxt.Text, this.EditBenefId.Text,
                        this.EditBenefNombre.Text, this.EditBenefParentezco.Text, this.EditBenefNacimiento.Text,
                        this.EditBenLugarNac.Text, this.EditBenefPorcentaje.Text);
                    EditarBeneficiarioWin.Hide();
                    Beneficiarios_Refresh(null, null);
                    X.Msg.Alert("Beneficiario", "Beneficiario Actualizado Correctamente.").Show();
                }
                else
                {
                    X.Msg.Alert("Beneficiario", "ERROR: El porcentaje excede del 100% del capital.").Show();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar beneficiario.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void CienPorciento()
        {
            try
            {
                SociosLogic logica = new SociosLogic();
                bool answer = logica.Igual100(this.EditsocioIdTxt.Text);
                if (answer)
                {
                    this.NuevoBeneficiarioWin.Show();
                }
                else
                {
                    X.Msg.Alert("Alerta", "No se puede asignar mas del 100% de las aportaciones").Show();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar porcentaje de beneficiarios.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void AgregarBeneficiarioBtn_Click()
        {
            try
            {
                SociosLogic socios = new SociosLogic();
                if (socios.BuscarId(this.EditsocioIdTxt.Text, this.AddBenefID.Text))
                {
                    if (socios.CienPorciento(this.EditsocioIdTxt.Text, Convert.ToInt32(this.AddBenefPorcentaje.Text)))
                    {
                        socios.InsertarBeneficiario(this.EditsocioIdTxt.Text, this.AddBenefID.Text,
                            this.AddBenefNombre.Text, this.AddBenefParentezco.Text, this.AddBenefFechaNacimiento.Text,
                            this.AddBenefLugarNac.Text, this.AddBenefPorcentaje.Text);
                        bool answer = socios.Igual100(EditsocioIdTxt.Text);
                        if (!answer)
                        {
                            this.NuevoBeneficiarioWin.Hide();
                        }
                        this.NuevoBeneficiarioForm.Reset();
                        Beneficiarios_Refresh(null, null);
                        X.Msg.Alert("Beneficiario", "Beneficiario Agregado Correctamente.").Show();
                    }
                    else
                    {
                        X.Msg.Alert("Beneficiario", "ERROR: El porcentaje excede del 100% del capital.").Show();
                    }
                }
                else
                {
                    X.Msg.Alert("Beneficiario", "ERROR: El beneficiario ya existe.").Show();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al agregar beneficiario.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void EliminarBeneficiarioBtn_Click()
        {

            try
            {
                SociosLogic socios = new SociosLogic();
                socios.EliminarBeneficiario(this.EditsocioIdTxt.Text, this.EditBenefId.Text);
                X.Msg.Alert("Beneficiario", "Beneficiario Eliminado Correctamente.").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar beneficiario.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoYesDisable()
        {
            try
            {
                confirm = true;
                SociosSt_Disable(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al confirmar deshabilitacion y refrescar socios.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoYesEnable()
        {
            try
            {
                confirm = true;
                SociosSt_Enable(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al confirmar habilitacion y refrescar socios.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoNo() { }

        [DirectMethod(RethrowException=true)]
        public void Error100()
        {
            try
            {
                SociosLogic logica = new SociosLogic();
                bool answer = logica.Igual100(this.EditsocioIdTxt.Text);
                if (answer)
                    X.Msg.Alert("Beneficiario", "Advertencia: Se le informa que no tiene asignado el 100% de sus aportaciones").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar porcentaje de beneficiarios.", ex);
                throw;
            }
        }

        protected void Beneficiarios_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string id = this.EditsocioIdTxt.Text;
                SociosLogic socio = new SociosLogic();
                this.StoreBeneficiarios.DataSource = socio.getBenefxSocio(id);
                this.StoreBeneficiarios.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar beneficiarios.", ex);
                throw;
            }
        }

        protected void SociosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                SociosLogic soc = new SociosLogic();
                var store1 = this.SociosGridP.GetStore();
                store1.DataSource = soc.getData();
                store1.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar socios.", ex);
                throw;
            }
        }

        protected void SociosSt_Update(object sender, DirectEventArgs e)
        {
            try
            {
                SociosLogic soc = new SociosLogic();
                int anual = Convert.ToInt32(e.ExtraParams["PRODUCCION_ANUAL"]);
                int manzanas = Convert.ToInt32(e.ExtraParams["PRODUCCION_MANZANAS_CULTIVADAS"]);
                soc.ActualizarSocio(e.ExtraParams["SOCIOS_ID"],
                    e.ExtraParams["SOCIOS_PRIMER_NOMBRE"],
                    e.ExtraParams["SOCIOS_SEGUNDO_NOMBRE"],
                    e.ExtraParams["SOCIOS_PRIMER_APELLIDO"],
                    e.ExtraParams["SOCIOS_SEGUNDO_APELLIDO"],
                    e.ExtraParams["SOCIOS_RESIDENCIA"],
                    e.ExtraParams["SOCIOS_ESTADO_CIVIL"],
                    e.ExtraParams["SOCIOS_LUGAR_DE_NACIMIENTO"],
                    e.ExtraParams["SOCIOS_FECHA_DE_NACIMIENTO"],
                    e.ExtraParams["SOCIOS_NIVEL_EDUCATIVO"],
                    e.ExtraParams["SOCIOS_IDENTIDAD"],
                    e.ExtraParams["SOCIOS_PROFESION"],
                    e.ExtraParams["SOCIOS_RTN"],
                    e.ExtraParams["SOCIOS_TELEFONO"],
                    e.ExtraParams["SOCIOS_LUGAR_DE_EMISION"],
                    e.ExtraParams["SOCIOS_FECHA_DE_EMISION"],
                    e.ExtraParams["GENERAL_CARNET_IHCAFE"],
                    e.ExtraParams["GENERAL_ORGANIZACION_SECUNDARIA"],
                    e.ExtraParams["GENERAL_NUMERO_CARNET"],
                    e.ExtraParams["GENERAL_EMPRESA_LABORA"],
                    e.ExtraParams["GENERAL_EMPRESA_CARGO"],
                    e.ExtraParams["GENERAL_EMPRESA_DIRECCION"],
                    e.ExtraParams["GENERAL_EMPRESA_TELEFONO"],
                    e.ExtraParams["GENERAL_SEGURO"],
                    e.ExtraParams["GENERAL_FECHA_ACEPTACION"],
                    e.ExtraParams["PRODUCCION_UBICACION_FINCA"],
                    e.ExtraParams["PRODUCCION_AREA"],
                    e.ExtraParams["PRODUCCION_VARIEDAD"],
                    e.ExtraParams["PRODUCCION_ALTURA"],
                    e.ExtraParams["PRODUCCION_DISTANCIA"],
                    anual,
                    e.ExtraParams["PRODUCCION_BENEFICIO_PROPIO"],
                    e.ExtraParams["PRODUCCION_ANALISIS_SUELO"],
                    e.ExtraParams["PRODUCCION_TIPO_INSUMOS"],
                    manzanas, this.LoggedUserHdn.Text
                    );
                this.EditarSocioWin.Hide();
                SociosSt_Reload(null, null);
                SociosGridP.GetView().Refresh(false);
                SociosSt.DataSource = soc.getData();
                SociosSt.DataBind();
                X.Msg.Alert("Socio", "Socio Actualizado Exitosamente.").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar socio.", ex);
                throw;
            }
        }

        protected void SociosSt_Insert(object sender, DirectEventArgs e)
        {
            try
            {
                SociosLogic soc = new SociosLogic();
                int anual = 0;
                if (AddAnualTxt.Text != null && AddAnualTxt.Text != "")
                    anual = Convert.ToInt32(AddAnualTxt.Text);
                int manzanas = 0;
                if (AddManzanaTxt.Text != null && AddManzanaTxt.Text != "")
                    manzanas = Convert.ToInt32(AddManzanaTxt.Text);
                soc.InsertarSocio(e.ExtraParams["SOCIOS_ID"],
                    e.ExtraParams["SOCIOS_PRIMER_NOMBRE"],
                    e.ExtraParams["SOCIOS_SEGUNDO_NOMBRE"],
                    e.ExtraParams["SOCIOS_PRIMER_APELLIDO"],
                    e.ExtraParams["SOCIOS_SEGUNDO_APELLIDO"],
                    e.ExtraParams["SOCIOS_RESIDENCIA"],
                    e.ExtraParams["SOCIOS_ESTADO_CIVIL"],
                    e.ExtraParams["SOCIOS_LUGAR_DE_NACIMIENTO"],
                    e.ExtraParams["SOCIOS_FECHA_DE_NACIMIENTO"],
                    e.ExtraParams["SOCIOS_NIVEL_EDUCATIVO"],
                    e.ExtraParams["SOCIOS_IDENTIDAD"],
                    e.ExtraParams["SOCIOS_PROFESION"],
                    e.ExtraParams["SOCIOS_RTN"],
                    e.ExtraParams["SOCIOS_TELEFONO"],
                    e.ExtraParams["SOCIOS_LUGAR_DE_EMISION"],
                    e.ExtraParams["SOCIOS_FECHA_DE_EMISION"],
                    AddCarnetIHCAFETxt.Text,
                    AddOrganizacionSecunTxt.Text,
                    AddNumCarnetTxt.Text,
                    AddEmpresaTxt.Text,
                    AddCargoTxt.Text,
                    AddDireccionTxt.Text,
                    AddEmpresaTelefonoTxt.Text,
                    AddSeguroTxt.Text,
                    AddFechaAceptacionTxt.Text,
                    AddUbicacionTxt.Text,
                    AddAreaTxt.Text,
                    AddVariedadTxt.Text,
                    AddAlturaTxt.Text,
                    AddDistanciaTxt.Text,
                    anual,
                    AddBeneficioTxt.Text,
                    AddAnalisisSueloTxt.Text,
                    AddTipoInsumosTxt.Text,
                    manzanas, this.LoggedUserHdn.Text
                    );
                NuevoSocioForm.Reset();
                SociosGridP.GetView().Refresh(false);
                this.NuevoSocioWin.Hide();
                SociosSt_Reload(null, null);
                X.Msg.Alert("Socio", "Socio Agregado Exitosamente.").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar socio.", ex);
                throw;
            }
        }

        protected void SociosSt_Remove(object sender, DirectEventArgs e)
        {
            try
            {
                SociosLogic socios = new SociosLogic();
                socios.EliminarSocio(e.ExtraParams["SOCIOS_ID"]);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar socio.", ex);
                throw;
            }
        }

        protected void SociosSt_Disable(object sender, EventArgs e)
        {
            try
            {
                RowSelectionModel sm = SociosGridP.SelectionModel.Primary as RowSelectionModel;
                this.id = sm.SelectedRow.RecordID;
                if (confirm)
                {
                    SociosLogic socios = new SociosLogic();
                    socios.DeshabilitarSocio(id);
                    confirm = false;
                }
                SociosSt_Reload(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al deshabilitar socio.", ex);
                throw;
            }
        }

        protected void SociosSt_Enable(object sender, EventArgs e)
        {

            try
            {
                RowSelectionModel sm = SociosGridP.SelectionModel.Primary as RowSelectionModel;
                string id = sm.SelectedRow.RecordID;
                if (confirm)
                {
                    SociosLogic socios = new SociosLogic();
                    socios.HabilitarSocio(id);
                    confirm = false;
                }
                SociosSt_Reload(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al habilitar socio.", ex);
                throw;
            }
         }


    }
}