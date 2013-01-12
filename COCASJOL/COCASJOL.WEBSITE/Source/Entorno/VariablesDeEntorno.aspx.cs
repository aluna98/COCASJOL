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
                    //this.VariablesEntornoGridP.RemoveProperty("tmp");//remove temp propertygrid parameter
                    //Variables_Refresh(null, null);
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

        [DirectMethod(RethrowException=true)]
        public void GuardarVariablesBtn_Click(string paramsVars)
        {
            try
            {
                string loggeduser = LoggedUserHdn.Text;

                var VariablesDeEntorno = Ext.Net.JSON.Deserialize<Dictionary<string, string>[]>(paramsVars);

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