using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC.Inventario.Ingresos;
using COCASJOL.LOGIC.Web;

using log4net;

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class EstadosNotaDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(EstadosNotaDePeso).Name);

        protected void Page_Load(object sender, EventArgs e)
        { 
            try
            {
                if (!X.IsAjaxRequest)
                {
                    this.plantillaPrefixHdn.Text = EstadoNotaDePesoLogic.PREFIJO_PLANTILLA;
                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de estados de nota de peso.", ex);
                throw;
            }
        }

        #region Validacion

        protected void AddLlaveTxt_Change(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string llave = this.AddLlaveTxt.Text;

                EstadoNotaDePesoLogic estadonotalogic = new EstadoNotaDePesoLogic();

                if (estadonotalogic.EstadoDeNotaDePesoExiste(llave))
                {
                    e.Success = false;
                    e.ErrorMessage = "La llave de estado de nota de peso ingresada ya existe.";
                    return;
                }
                else
                    e.Success = true;

                COCASJOL.LOGIC.Seguridad.PrivilegioLogic privilegiologic = new LOGIC.Seguridad.PrivilegioLogic();

                if (privilegiologic.PrivilegioExiste(EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + llave))
                {
                    e.Success = false;
                    e.ErrorMessage = "La llave de privilegio para estado de nota de peso ya existe. Es necesario que cambie la llave.";
                    return;
                }
                else
                    e.Success = true;


                COCASJOL.LOGIC.Utiles.PlantillaLogic plantillalogic = new LOGIC.Utiles.PlantillaLogic();

                if (plantillalogic.PlantillaDeNotifiacionExiste(EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + llave))
                {
                    e.Success = false;
                    e.ErrorMessage = "La llave de plantilla de notificacíon para estado de nota de peso ya existe. Es necesario que cambie la llave.";
                    return;
                }
                else
                    e.Success = true;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar existencia de estado de nota de peso y privilegio asociado.", ex);
                throw;
            }
        }

        #endregion

        protected void FormatKeysSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                COCASJOL.LOGIC.Utiles.PlantillaLogic plantillalogic = new COCASJOL.LOGIC.Utiles.PlantillaLogic();

                this.FormatKeysSt.DataSource = plantillalogic.GetFormatKeys("");
                this.FormatKeysSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar llaves de formato para plantilla de notificacion.", ex);
                throw;
            }
        }

        protected void EstadosNotaDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
                log.Fatal("Error fatal al cargar estados de nota de peso.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void AddEstadosNotaPadreSt_Refresh()
        {
            try
            {
                this.EstadosNotaSiguienteSt_Refresh(this.AddSiguienteIdCmb);
                this.AddSiguienteIdCmb.Value = this.AddSiguienteIdCmb.Value;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso para insertar.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void EditEstadosNotaPadreSt_Refresh()
        {
            try
            {
                this.EstadosNotaSiguienteSt_Refresh(this.EditSiguienteIdCmb);
                this.EditSiguienteIdCmb.Value = this.EditSiguienteIdCmb.Value;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso para modificar.", ex);
                throw;
            }
        }

        private void EstadosNotaSiguienteSt_Refresh(Ext.Net.ComboBox siguienteIdCmb)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();

                int siguiente = siguienteIdCmb.Text == "" ? 0 : Convert.ToInt32(siguienteIdCmb.Text);

                this.EstadosNotaPadreSt.DataSource = estadologic.GetEstadosNotaDePesoSinAsignar(siguiente);
                this.EstadosNotaPadreSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void LoadPlantilla()
        {
            try
            {
                COCASJOL.LOGIC.Utiles.PlantillaLogic plantillalogic = new LOGIC.Utiles.PlantillaLogic();
                COCASJOL.DATAACCESS.plantilla_notificacion pl = plantillalogic.GetPlantilla(EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + this.EditLlaveTxt.Text);

                this.EditMensajeTxt.Text = pl == null ? "" : pl.PLANTILLAS_MENSAJE;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar plantilla de notificacion de estado de nota de peso.", ex);
                throw;
            }
        }

        #region Insertar

        [DirectMethod(RethrowException = true)]
        public void AddGuardarBtn_Click()
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                estadologic.InsertarEstadoNotaDePeso
                    (string.IsNullOrEmpty(this.AddSiguienteIdCmb.Text) ? 0 : Convert.ToInt32(this.AddSiguienteIdCmb.Text),
                    this.AddLlaveTxt.Text,
                    this.AddNombreTxt.Text,
                    this.AddDescripcionTxt.Text,
                    this.AddEsCatacionChk.Checked,
                    this.AddCreatedByTxt.Text,
                    this.AddEnableFechaChk.Checked,
                    this.AddEnableEstadoChk.Checked,
                    this.AddEnableSocioIdChk.Checked,
                    this.AddEnableClasificacionDeCafeChk.Checked,
                    this.AddShowInformacionSocioChk.Checked,
                    this.AddEnableFormaDeEntregaChk.Checked,
                    this.AddEnableDetalleChk.Checked,
                    this.AddEnableSacosRetenidosChk.Checked,
                    this.AddShowDescuentosChk.Checked,
                    this.AddShowTotalesChk.Checked,
                    this.AddEnableRegistrarChk.Checked,
                    this.AddEnableImprimirChk.Checked,
                    this.AddMensajeTxt.Text);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al guardar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Actualizar

        [DirectMethod(RethrowException = true)]
        public void EditGuardarBtn_Click()
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                estadologic.ActualizarEstadoNotaDePeso
                    (Convert.ToInt32(this.EditIdTxt.Text),
                    string.IsNullOrEmpty(this.EditSiguienteIdCmb.Text) ? 0 : Convert.ToInt32(this.EditSiguienteIdCmb.Text),
                    this.EditNombreTxt.Text,
                    this.EditDescripcionTxt.Text,
                    this.EditEsCatacionChk.Checked,
                    this.EditModifiedByTxt.Text,
                    this.EditEnableFechaChk.Checked,
                    this.EditEnableEstadoChk.Checked,
                    this.EditEnableSocioIdChk.Checked,
                    this.EditEnableClasificacionDeCafeChk.Checked,
                    this.EditShowInformacionSocioChk.Checked,
                    this.EditEnableFormaDeEntregaChk.Checked,
                    this.EditEnableDetalleChk.Checked,
                    this.EditEnableSacosRetenidosChk.Checked,
                    this.EditShowDescuentosChk.Checked,
                    this.EditShowTotalesChk.Checked,
                    this.EditEnableRegistrarChk.Checked,
                    this.EditEnableImprimirChk.Checked,
                    this.EditMensajeTxt.Text);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Eliminar

        [DirectMethod(RethrowException = true)]
        public void EliminarBtn_Click(int ESTADOS_NOTA_ID)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                estadologic.EliminarEstadoNotaDePeso(ESTADOS_NOTA_ID);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

    }
}