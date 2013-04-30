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
    public partial class NotasDePesoEnAdministracion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(NotasDePesoEnAdministracion).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                    this.EstadoIdHdn.Text = estadologic.GetIdEstadoNotaDePeso("ADMINISTRACION").ToString();
                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;//necesario actualizarlo siempre, para el tracking correcto
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de notas de peso en administracion.", ex);
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
                log.Fatal("Error fatal al cargar notas de peso en administracion.", ex);
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

                NotaDePesoEnCatacionLogic notadepesologic = new NotaDePesoEnCatacionLogic();

                this.EditNotaDetalleSt.DataSource = notadepesologic.GetDetalleNotaDePeso(Convert.ToInt32(notaId));
                this.EditNotaDetalleSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener detalles de nota de peso en administracion.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void EditNotaDePeso_Click()
        {
            try
            {
                string loggedUser = this.LoggedUserHdn.Text;

                NotaDePesoEnAdministracionLogic notadepesologic = new NotaDePesoEnAdministracionLogic();

                notadepesologic.ActualizarNotaDePeso
                    (Convert.ToInt32(this.EditNotaIdTxt.Text),
                    Convert.ToInt32(this.EditEstadoNotaCmb.Text),
                    loggedUser);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar nota de peso en administracion.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void RegisterNotaDePeso_Click()
        {
            try
            {
                string loggedUser = this.LoggedUserHdn.Text;

                NotaDePesoEnAdministracionLogic notadepesologic = new NotaDePesoEnAdministracionLogic();

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
    }
}