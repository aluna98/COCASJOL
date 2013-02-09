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

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class NotasDePesoEnAdministracion : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
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
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void NotasDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        [DirectMethod(RethrowException = true, ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "EditarNotasFormP")]
        public void EditNotaDePeso_Click(string Detalles)
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
            catch (Exception)
            {
                throw;
            }
        }

        [DirectMethod(RethrowException = true, ShowMask = true, Target = MaskTarget.CustomTarget, CustomTarget = "EditarNotasFormP")]
        public void RegisterNotaDePeso_Click(string Detalles)
        {
            try
            {
                string loggedUser = this.LoggedUserHdn.Text;

                NotaDePesoEnAdministracionLogic notadepesologic = new NotaDePesoEnAdministracionLogic();

                notadepesologic.RegistrarNotaDePeso
                    (Convert.ToInt32(this.EditNotaIdTxt.Text),
                    Convert.ToInt32(this.EditEstadoNotaCmb.Text),
                    loggedUser);

                this.EditRegistrarBtn.Hidden = true;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}