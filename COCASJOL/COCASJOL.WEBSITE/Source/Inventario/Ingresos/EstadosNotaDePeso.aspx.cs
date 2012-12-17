using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC.Inventario.Ingresos;
using COCASJOL.LOGIC.Web;

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class EstadosNotaDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
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
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void EstadosNotaDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        protected void EditNombreTxt_Validate(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string nombreDeEstadoNotaDePeso = this.EditNombreTxt.Text;
                int EstadoId = Convert.ToInt32(this.EditIdTxt.Text);

                EstadoNotaDePesoLogic estadoNotaDePesologic = new EstadoNotaDePesoLogic();

                if (estadoNotaDePesologic.NombreDeEstadoNotaDePesoExiste(EstadoId, nombreDeEstadoNotaDePeso))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de estado de nota de peso ingresado ya existe.";
                }
                else
                    e.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}