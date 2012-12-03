using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC.Inventario.Ingresos;
using COCASJOL.LOGIC.Web;

namespace COCASJOL.Website.Source.Inventario.Ingresos
{
    public partial class EstadosNotaDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    string loggedUsr = Session["username"] as string;
                    this.LoggedUserHdn.Text = loggedUsr;
                    //this.ValidarCredenciales("MANT_TIPOPRODS");
                }
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

        protected void AddNombreTxt_Validate(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string nombreDeEstadoNotaDePeso = this.AddNombreTxt.Text;

                EstadoNotaDePesoLogic estadoNotaDePesologic = new EstadoNotaDePesoLogic();

                if (estadoNotaDePesologic.NombreDeEstadoNotaDePesoExiste(nombreDeEstadoNotaDePeso))
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

        protected void EditNombreTxt_Validate(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string nombreDeEstadoNotaDePeso = this.EditNombreTxt.Text;

                EstadoNotaDePesoLogic estadoNotaDePesologic = new EstadoNotaDePesoLogic();

                if (estadoNotaDePesologic.NombreDeEstadoNotaDePesoExiste(nombreDeEstadoNotaDePeso))
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