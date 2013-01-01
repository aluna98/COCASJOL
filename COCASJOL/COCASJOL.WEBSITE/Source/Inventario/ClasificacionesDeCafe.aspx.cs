using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC.Inventario;
using COCASJOL.LOGIC.Web; 

namespace COCASJOL.WEBSITE.Source.Inventario
{
    public partial class ClasificacionesDeCafe : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {

                }
                string loggedUsr = Session["username"] as string;//para tracking (auditoria)
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void ClasificacionesCafeDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        protected void AddNombreTxt_Validate(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string nombreDeClasificacionDeCafe = this.AddNombreTxt.Text;

                ClasificacionDeCafeLogic clasificacionDeCafelogic = new ClasificacionDeCafeLogic();

                if (clasificacionDeCafelogic.NombreDeClasificacionDeCafeExiste(nombreDeClasificacionDeCafe))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de la clasificación de café ingresada ya existe.";
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
                int idDeClasificacionDeCafe = Convert.ToInt32(this.EditIdTxt.Value);
                string nombreDeClasificacionDeCafe = this.EditNombreTxt.Text;

                ClasificacionDeCafeLogic clasificacionDeCafelogic = new ClasificacionDeCafeLogic();

                if (clasificacionDeCafelogic.NombreDeClasificacionDeCafeExiste(idDeClasificacionDeCafe, nombreDeClasificacionDeCafe))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de la clasificación de café ingresada ya existe.";
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