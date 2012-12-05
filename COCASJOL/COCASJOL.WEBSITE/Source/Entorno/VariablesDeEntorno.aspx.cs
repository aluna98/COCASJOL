using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC.Entorno;
using COCASJOL.LOGIC.Web;

namespace COCASJOL.WEBSITE.Source.Entorno
{
    public partial class VariablesDeEntorno : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    string loggedUsr = Session["username"] as string;
                    this.LoggedUserHdn.Text = loggedUsr;

                    this.ValidarCredenciales(typeof(VariablesDeEntorno).Name);
                }
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void VariablesEntornoDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        #region Validaciones

        protected void AddLlaveTxt_Validate(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string llaveDeVariablesDeEntorno = this.AddLlaveTxt.Text;

                VariablesDeEntornoLogic VariablesDeEntornologic = new VariablesDeEntornoLogic();

                if (VariablesDeEntornologic.LlaveDeVariableDeEntornoExiste(llaveDeVariablesDeEntorno))
                {
                    e.Success = false;
                    e.ErrorMessage = "La llave de la variable de entorno ingresada ya existe.";
                }
                else
                    e.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void AddNombreTxt_Validate(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string nombreDeVariablesDeEntorno = this.AddNombreTxt.Text;

                VariablesDeEntornoLogic VariablesDeEntornologic = new VariablesDeEntornoLogic();

                if (VariablesDeEntornologic.NombreDeVariableDeEntornoExiste(nombreDeVariablesDeEntorno))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de de la variable de entorno ingresada ya existe.";
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
                string nombreDeVariablesDeEntorno = this.EditNombreTxt.Text;

                VariablesDeEntornoLogic VariablesDeEntornologic = new VariablesDeEntornoLogic();

                if (VariablesDeEntornologic.NombreDeVariableDeEntornoExiste(nombreDeVariablesDeEntorno))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de de la variable de entorno ingresada ya existe.";
                }
                else
                    e.Success = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}