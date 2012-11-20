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

namespace COCASJOL.Website.Socios
{
    public partial class Socios : COCASJOLBASE
    {
        public string id;
        public bool confirm;

        protected void Page_Load(object sender, EventArgs e)
        {
            confirm = false;

            this.ResourceManager1.DirectMethodNamespace = "CompanyX";

            if (!X.IsAjaxRequest)
            {
                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
        }

        [DirectMethod]
        public void DoConfirmDisable()
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

        [DirectMethod]
        public void DoConfirmEnable()
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

        [DirectMethod]
        public void RefrescarBeneficiarios()
        {
            Beneficiarios_Refresh(null, null);
        }

        [DirectMethod]
        public void RefreshGrid()
        {
            SociosSt_Reload(null, null);
        }

        [DirectMethod]
        public void ActualizarBeneficiarioBtn_Click()
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

        [DirectMethod]
        public void AgregarBeneficiarioBtn_Click()
        {
            SociosLogic socios = new SociosLogic();
            if (socios.BuscarId(this.EditsocioIdTxt.Text, this.AddBenefID.Text))
            {
                if (socios.CienPorciento(this.EditsocioIdTxt.Text, Convert.ToInt32(this.AddBenefPorcentaje.Text)))
                {
                    socios.InsertarBeneficiario(this.EditsocioIdTxt.Text, this.AddBenefID.Text,
                        this.AddBenefNombre.Text, this.AddBenefParentezco.Text, this.AddBenefFechaNacimiento.Text,
                        this.AddBenefLugarNac.Text, this.AddBenefPorcentaje.Text);
                    this.NuevoBeneficiarioWin.Hide();
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

        [DirectMethod]
        public void EliminarBeneficiarioBtn_Click()
        {
             
            SociosLogic socios = new SociosLogic();
            socios.EliminarBeneficiario(this.EditsocioIdTxt.Text, this.EditBenefId.Text);
            X.Msg.Alert("Beneficiario", "Beneficiario Eliminado Correctamente.").Show();
        }
        [DirectMethod]
        public void DoYesDisable()
        {
            confirm = true;
            SociosSt_Disable(null, null);
        }

        [DirectMethod]
        public void DoYesEnable()
        {
            confirm = true;
            SociosSt_Enable(null, null);
        }

        [DirectMethod]
        public void DoNo() { }


        protected void Beneficiarios_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            string id = this.EditsocioIdTxt.Text;
            SociosLogic socio = new SociosLogic();
            this.StoreBeneficiarios.DataSource = socio.getBenefxSocio(id);
            this.StoreBeneficiarios.DataBind();
        }

        protected void SociosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            SociosLogic soc = new SociosLogic();
            var store1 = this.SociosGridP.GetStore();
            store1.DataSource = soc.getData();
            store1.DataBind();
        }

        protected void SociosSt_Update(object sender, DirectEventArgs e)
        {
            SociosLogic soc = new SociosLogic();
            int identidad = Convert.ToInt32(e.ExtraParams["SOCIOS_IDENTIDAD"]);
            int RTN = Convert.ToInt32(e.ExtraParams["SOCIOS_RTN"]);
            int IHCAFE = Convert.ToInt32(e.ExtraParams["GENERAL_CARNET_IHCAFE"]);
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
                identidad,
                e.ExtraParams["SOCIOS_PROFESION"],
                RTN,
                e.ExtraParams["SOCIOS_TELEFONO"],
                e.ExtraParams["SOCIOS_LUGAR_DE_EMISION"],
                e.ExtraParams["SOCIOS_FECHA_DE_EMISION"],
                IHCAFE,
                e.ExtraParams["GENERAL_ORGANIZACION_SECUNDARIA"],
                e.ExtraParams["GENERAL_NUMERO_CARNET"],
                e.ExtraParams["GENERAL_EMPRESA_LABORA"],
                e.ExtraParams["GENERAL_EMPRESA_CARGO"],
                e.ExtraParams["GENERAL_EMPRESA_DIRECCION"],
                e.ExtraParams["GENERAL_EMPRESA_TELEFONO"],
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

        protected void SociosSt_Insert(object sender, DirectEventArgs e)
        {

            SociosLogic soc = new SociosLogic();
            int identidad = Convert.ToInt32(e.ExtraParams["SOCIOS_IDENTIDAD"]);
            int RTN = Convert.ToInt32(e.ExtraParams["SOCIOS_RTN"]);
            int IHCAFE = 0;
            if (AddCarnetIHCAFETxt.Text != null && AddCarnetIHCAFETxt.Text != "")
                IHCAFE = Convert.ToInt32(AddCarnetIHCAFETxt.Text);
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
                identidad,
                e.ExtraParams["SOCIOS_PROFESION"],
                RTN,
                e.ExtraParams["SOCIOS_TELEFONO"],
                e.ExtraParams["SOCIOS_LUGAR_DE_EMISION"],
                e.ExtraParams["SOCIOS_FECHA_DE_EMISION"],
                IHCAFE,
                AddOrganizacionSecunTxt.Text,
                AddNumCarnetTxt.Text,
                AddEmpresaTxt.Text,
                AddCargoTxt.Text,
                AddDireccionTxt.Text,
                AddEmpresaTelefonoTxt.Text,
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

        protected void SociosSt_Remove(object sender, DirectEventArgs e)
        {
            SociosLogic socios = new SociosLogic();
            socios.EliminarUsuario(e.ExtraParams["SOCIOS_ID"]);
            
        }

        protected void SociosSt_Disable(object sender, EventArgs e)
        {
            RowSelectionModel sm = SociosGridP.SelectionModel.Primary as RowSelectionModel;
            this.id = sm.SelectedRow.RecordID;
            if (confirm)
            {
                SociosLogic socios = new SociosLogic();
                socios.DeshabilitarUsuario(id);
                confirm = false;
            }
            SociosSt_Reload(null, null);
        }

        protected void SociosSt_Enable(object sender, EventArgs e)
        {
            
            RowSelectionModel sm = SociosGridP.SelectionModel.Primary as RowSelectionModel;
            string id = sm.SelectedRow.RecordID;
            if (confirm)
            {
                SociosLogic socios = new SociosLogic();
                socios.HabilitarUsuario(id);
                confirm = false;
            }
            SociosSt_Reload(null, null);
         }
    }
}