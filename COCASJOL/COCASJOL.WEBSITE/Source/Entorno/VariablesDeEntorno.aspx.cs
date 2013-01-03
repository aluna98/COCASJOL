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
                    this.VariablesEntornoGridP.RemoveProperty("tmp");//remove temp propertygrid parameter
                    Variables_Refresh(null, null);
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

        private void Variables_Refresh(object sender, DirectEventArgs e)
        {
            try
            {
                VariablesDeEntornoLogic varenvslogic = new VariablesDeEntornoLogic();
                var variables = varenvslogic.GetVariablesDeEntorno();

                foreach (variable_de_entorno varenv in variables)
                {
                    this.VariablesEntornoGridP.AddProperty(new PropertyGridParameter
                    {
                        Name = varenv.VARIABLES_LLAVE,
                        Value = varenv.VARIABLES_VALOR,
                        DisplayName = varenv.VARIABLES_NOMBRE,
                        Editor =
                            {
                                new TextField
                                {
                                    AllowBlank = false,
                                    MsgTarget = MessageTarget.Qtip
                                }
                            }
                    });
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void GuardarVariablesBtn_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string loggeduser = LoggedUserHdn.Text;

                string paramsVars = this.VariablesEntornoGridP.Source.ToJsonObject();
                var VariablesDeEntorno = Ext.Net.JSON.Deserialize<Dictionary<string, string>>(paramsVars);

                VariablesDeEntornoLogic varenvlogic = new VariablesDeEntornoLogic();
                varenvlogic.ActualizarVariablesDeEntorno(VariablesDeEntorno, loggeduser);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}