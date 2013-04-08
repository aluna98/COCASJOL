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
    public partial class NotasDePesoEnCatacion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(NotasDePesoEnCatacion).Name);

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
                log.Fatal("Error fatal al cargar pagina de notas de peso en catacion.", ex);
                throw;
            }
        }

        protected void NotasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
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
                log.Fatal("Error fatal al obtener detalles de nota de peso en catacion.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void EditNotaDePeso_Click(string Detalles)
        {
            try
            {
                string loggedUser = this.LoggedUserHdn.Text;

                var detalles = JSON.Deserialize<Dictionary<string, string>[]>(Detalles);

                NotaDePesoEnCatacionLogic notadepesologic = new NotaDePesoEnCatacionLogic();


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
                    -1, -1);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar nota de peso en catacion.", ex);
                throw;
            }
        }
    }
}