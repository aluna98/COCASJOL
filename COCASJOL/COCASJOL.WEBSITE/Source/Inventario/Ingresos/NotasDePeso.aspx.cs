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
using COCASJOL.LOGIC.Inventario.Ingresos;

using log4net;

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class NotasDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(NotasDePeso).Name);

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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de notas de peso.", ex);
                throw;
            }
        }

        protected void NotasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
                log.Fatal("Error fatal al cargar notas de peso.", ex);
                throw;
            }
        }

        protected void AddEstadosNotaSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                this.AddEstadosNotaSt.DataSource = estadologic.GetEstadosIniciales();
                this.AddEstadosNotaSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso para agregar.", ex);
                throw;
            }
        }

        protected void EditSocioSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string strESTADOS_NOTA_ID_ACTUAL = e.Parameters["ESTADOS_NOTA_ID_ACTUAL"];

                if (string.IsNullOrEmpty(strESTADOS_NOTA_ID_ACTUAL))
                    return;

                NotaDePesoLogic notalogic = new NotaDePesoLogic();

                int ESTADOS_NOTA_ID_ACTUAL = Convert.ToInt32(strESTADOS_NOTA_ID_ACTUAL);

                this.EditSocioSt.DataSource = notalogic.GetSocios(ESTADOS_NOTA_ID_ACTUAL);
                this.EditEstadosNotaSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso para actualizar.", ex);
                throw;
            }
        }

        protected void EditEstadosNotaSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string strESTADOS_NOTA_ID_ACTUAL = e.Parameters["ESTADOS_NOTA_ID_ACTUAL"];

                NotaDePesoLogic notalogic = new NotaDePesoLogic();

                if (string.IsNullOrEmpty(strESTADOS_NOTA_ID_ACTUAL))
                {
                    this.EditEstadosNotaSt.DataSource = notalogic.GetEstadosNotaDePeso();
                }
                else
                {
                    int ESTADOS_NOTA_ID_ACTUAL = Convert.ToInt32(strESTADOS_NOTA_ID_ACTUAL);

                    if (string.IsNullOrEmpty(this.EditClasificacionCafeCmb.Text))
                        this.EditEstadosNotaSt.DataSource = notalogic.GetEstadosNotaDePeso(ESTADOS_NOTA_ID_ACTUAL);
                    else
                        this.EditEstadosNotaSt.DataSource = notalogic.GetEstadosNotaDePeso(ESTADOS_NOTA_ID_ACTUAL, Convert.ToInt32(this.EditClasificacionCafeCmb.Text));
                }

                this.EditEstadosNotaSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso para actualizar.", ex);
                throw;
            }
        }

        protected void EditNotaDetalleSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string notaId = this.EditNotaIdTxt.Text;

                if (string.IsNullOrEmpty(notaId))
                    return;

                NotaDePesoLogic notadepesologic = new NotaDePesoLogic();

                this.EditNotaDetalleSt.DataSource = notadepesologic.GetDetalleNotaDePeso(Convert.ToInt32(notaId));
                this.EditNotaDetalleSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener detalles nota de peso para actualizar.", ex);
                throw;
            }
        }

        protected void NotasSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string loggedUsr = Session["username"] as string;

                NotaDePesoLogic notadepesologic = new NotaDePesoLogic();
                this.NotasSt.DataSource = notadepesologic.GetNotasDePeso
                    (Convert.ToInt32(string.IsNullOrEmpty(this.f_NOTAS_ID.Text) ? "0" : this.f_NOTAS_ID.Text),
                    Convert.ToInt32(string.IsNullOrEmpty(this.f_ESTADOS_NOTA_ID.Text) ? "0" : this.f_ESTADOS_NOTA_ID.Text),
                    this.f_SOCIOS_ID.Text,
                    this.f_SOCIOS_NOMBRE_COMPLETO.Text,
                    Convert.ToInt32(string.IsNullOrEmpty(this.f_CLASIFICACIONES_CAFE_ID.Text) ? "0" : this.f_CLASIFICACIONES_CAFE_ID.Text),
                    default(DateTime),
                    this.f_DATE_FROM.SelectedDate,
                    this.f_DATE_TO.SelectedDate,
                    loggedUsr);
                this.NotasSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar las nota de peso.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void AddNotaDePeso_Click(string Detalles)
        {
            try
            {
                decimal NOTA_PORCENTAJEHUMEDADMIN = Convert.ToDecimal(this.Variables["NOTA_PORCENTAJEHUMEDADMIN"]);
                decimal NOTA_TRANSPORTECOOP = Convert.ToDecimal(this.Variables["NOTA_TRANSPORTECOOP"]);

                string loggedUser = this.LoggedUserHdn.Text;

                var detalles = JSON.Deserialize<Dictionary<string, string>[]>(Detalles);

                NotaDePesoLogic notadepesologic = new NotaDePesoLogic();


                string pDefecto = this.AddPorcentajeDefectoTxt.Text.Replace("%", "");
                string pHumedad = this.AddPorcentajeHumedadTxt.Text.Replace("%", "");

                notadepesologic.InsertarNotaDePeso
                    (Convert.ToInt32(this.AddEstadoNotaCmb.Text),
                    this.AddSociosIdTxt.Text,
                    Convert.ToInt32(this.AddClasificacionCafeCmb.Text),
                    this.AddFechaNotaTxt.SelectedDate,
                    this.AddCooperativaRadio.Value == null ? false : Convert.ToBoolean(this.AddCooperativaRadio.Value),
                    Convert.ToDecimal(pDefecto),
                    Convert.ToDecimal(pHumedad),
                    Convert.ToDecimal(this.AddSumaPesoBrutoTxt.Text),
                    Convert.ToDecimal(this.AddTaraTxt.Text),
                    Convert.ToInt32(this.AddSacosRetenidosTxt.Text),
                    loggedUser,
                    detalles,
                    NOTA_PORCENTAJEHUMEDADMIN,
                    NOTA_TRANSPORTECOOP);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al agregar nota de peso.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void EditNotaDePeso_Click(string Detalles)
        {
            try
            {
                decimal NOTA_PORCENTAJEHUMEDADMIN = Convert.ToDecimal(this.Variables["NOTA_PORCENTAJEHUMEDADMIN"]);
                decimal NOTA_TRANSPORTECOOP = Convert.ToDecimal(this.Variables["NOTA_TRANSPORTECOOP"]);

                string loggedUser = this.LoggedUserHdn.Text;

                var detalles = JSON.Deserialize<Dictionary<string, string>[]>(Detalles);

                NotaDePesoLogic notadepesologic = new NotaDePesoLogic();

                string pDefecto = this.EditPorcentajeDefectoTxt.Text.Replace("%", "");
                string pHumedad = this.EditPorcentajeHumedadTxt.Text.Replace("%", "");

                notadepesologic.ActualizarNotaDePeso
                    (Convert.ToInt32(this.EditNotaIdTxt.Text),
                    Convert.ToInt32(this.EditEstadoNotaCmb.Text),
                    this.EditSociosIdTxt.Text,
                    Convert.ToInt32(this.EditClasificacionCafeCmb.Text),
                    this.EditFechaNotaTxt.SelectedDate,
                    this.EditCooperativaRadio.Value == null ? false : Convert.ToBoolean(this.EditCooperativaRadio.Value),
                    Convert.ToDecimal(pDefecto),
                    Convert.ToDecimal(pHumedad),
                    Convert.ToDecimal(this.EditSumaPesoBrutoTxt.Text),
                    Convert.ToDecimal(this.EditTaraTxt.Text),
                    Convert.ToInt32(this.EditSacosRetenidosTxt.Text),
                    loggedUser,
                    detalles,
                    NOTA_PORCENTAJEHUMEDADMIN,
                    NOTA_TRANSPORTECOOP);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar nota de peso.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void RegisterNotaDePeso_Click()
        {
            try
            {
                string loggedUser = this.LoggedUserHdn.Text;

                NotaDePesoLogic notadepesologic = new NotaDePesoLogic();

                int transactnum = notadepesologic.RegistrarNotaDePeso
                    (Convert.ToInt32(this.EditNotaIdTxt.Text),
                    Convert.ToInt32(this.EditEstadoNotaCmb.Text),
                    loggedUser);

                this.EditRegistrarBtn.Hidden = true;
                this.EditGuardarBtn.Hidden = true;
                this.EditTransaccionNumTxt.Text = transactnum.ToString();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al registrar nota de peso en administracion.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void DeleteNotaDePeso_Click(string strNOTAS_ID)
        {
            try
            {
                if (string.IsNullOrEmpty(strNOTAS_ID) || string.IsNullOrWhiteSpace(strNOTAS_ID))
                    return;

                int NOTAS_ID = Convert.ToInt32(strNOTAS_ID);
                NotaDePesoLogic notadepesologic = new NotaDePesoLogic();
                notadepesologic.EliminarNotaDePeso(NOTAS_ID);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar nota de peso.", ex);
                throw;
            }
        }
    }
}