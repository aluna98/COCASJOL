using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Productos;
using COCASJOL.LOGIC.Web;

namespace COCASJOL.WEBSITE.Source.Productos
{
    public partial class TiposDeProductos : COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    string loggedUsr = Session["username"] as string;
                    this.LoggedUserHdn.Text = loggedUsr;
                    
                    this.ValidarCredenciales(typeof(TiposDeProductos).Name);
                }
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void TipoDeProductoDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        protected void AddNombreTxt_Validate(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string nombreDeTipoDeProducto = this.AddNombreTxt.Text;

                TipoDeProductoLogic tipoDeProductologic = new TipoDeProductoLogic();

                if (tipoDeProductologic.NombreDeTipoDeProductoExiste(nombreDeTipoDeProducto))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de tipo de producto ingresado ya existe.";
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
                string nombreDeTipoDeProducto = this.EditNombreTxt.Text;

                TipoDeProductoLogic tipoDeProductologic = new TipoDeProductoLogic();

                if (tipoDeProductologic.NombreDeTipoDeProductoExiste(nombreDeTipoDeProducto))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de tipo de producto ingresado ya existe.";
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