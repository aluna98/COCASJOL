using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Entorno;
using COCASJOL.LOGIC.Web;

using log4net;
 
namespace COCASJOL.WEBSITE.Source.Entorno
{
    public partial class VariablesDeEntorno : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(VariablesDeEntorno).Name);

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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de variables de entorno.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void GuardarVariablesBtn_Click(string paramsVars)
        {
            try
            {
                string loggeduser = LoggedUserHdn.Text;

                var VariablesDeEntorno = Ext.Net.JSON.Deserialize<Dictionary<string, string>[]>(paramsVars);

                Dictionary<string, string> variables = new Dictionary<string, string>();

                foreach (Dictionary<string, string> validarVars in VariablesDeEntorno)
                {
                    variables.Add(validarVars["VARIABLES_LLAVE"], validarVars["VARIABLES_VALOR"]);
                }

                if (this.ValidarTodasVariables(variables))
                {

                    VariablesDeEntornoLogic varenvlogic = new VariablesDeEntornoLogic();
                    varenvlogic.ActualizarVariablesDeEntorno(VariablesDeEntorno, loggeduser);
                }
                else
                {
                    Ext.Net.ResourceManager.AjaxSuccess = false;
                    Ext.Net.ResourceManager.AjaxErrorMessage = "Ocurrio un error al validar las variables.";
                }

            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al guardar variables de entorno.", ex);
                throw;
            }
        }
    }
}