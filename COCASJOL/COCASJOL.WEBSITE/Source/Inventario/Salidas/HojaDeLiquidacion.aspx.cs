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
using COCASJOL.LOGIC.Inventario.Salidas;

namespace COCASJOL.WEBSITE.Source.Inventario.Salidas
{
    public partial class HojaDeLiquidacion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            try
            {
                if (!X.IsAjaxRequest)
                {

                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;//necesario actualizarlo siempre, para el tracking correcto
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void HojasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        [DirectMethod(RethrowException = true, ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "EditarNotasFormP")]
        public void EditNotaDePeso_Click(string Detalles)
        {
            try
            {
                Dictionary<string, string> variables = this.GetVariables();
                if (!this.ValidarVariables(variables))
                    return;

                string loggedUser = this.LoggedUserHdn.Text;

                var detalles = JSON.Deserialize<Dictionary<string, string>[]>(Detalles);

                //NotaDePesoEnCatacionLogic notadepesologic = new NotaDePesoEnCatacionLogic();


                //string pDefecto = this.EditPorcentajeDefectoTxt.Text.Replace("%", "");
                //string pHumedad = this.EditPorcentajeHumedadTxt.Text.Replace("%", "");

                //notadepesologic.ActualizarNotaDePeso
                //    (Convert.ToInt32(this.EditNotaIdTxt.Text),
                //    Convert.ToInt32(this.EditEstadoNotaCmb.Text),
                //    this.EditSociosIdTxt.Text,
                //    Convert.ToInt32(this.EditClasificacionCafeCmb.Text),
                //    this.EditFechaNotaTxt.SelectedDate,
                //    this.EditCooperativaRadio.Value == null ? false : Convert.ToBoolean(this.EditCooperativaRadio.Value),
                //    Convert.ToDecimal(pDefecto),
                //    Convert.ToDecimal(pHumedad),
                //    Convert.ToDecimal(this.EditSumaPesoBrutoTxt.Text),
                //    Convert.ToDecimal(this.EditTaraTxt.Text),
                //    Convert.ToInt32(this.EditSacosRetenidosTxt.Text),
                //    loggedUser,
                //    detalles,
                //    variables);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}