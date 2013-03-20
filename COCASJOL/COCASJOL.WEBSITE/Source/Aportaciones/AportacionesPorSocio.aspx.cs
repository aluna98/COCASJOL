using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Aportaciones;

namespace COCASJOL.WEBSITE.Source.Aportaciones
{
    public partial class AportacionesPorSocio : COCASJOL.LOGIC.Web.COCASJOLBASE
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

        protected void AportacionesDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        [DirectMethod(RethrowException=true)]
        public void RetirarBtn_Click()
        {
            try
            {
                string logged_user = Session["username"] as string;

                RetiroAportacionLogic retirologic = new RetiroAportacionLogic();
                retirologic.InsertarRetiroDeAportaciones(this.EditSociosIdTxt.Text,
                        Convert.ToDecimal(this.EditRetiroAportacionOrdinariaSaldoTxt.Text),
                        Convert.ToDecimal(this.EditRetiroAportacionExtraordinariaSaldoTxt.Text),
                        Convert.ToDecimal(this.EditRetiroAportacionCapRetencionSaldoTxt.Text),
                        Convert.ToDecimal(this.EditRetiroAportacionInteresesSAportacionesSaldoTxt.Text),
                        Convert.ToDecimal(this.EditRetiroAportacionExcedentePeriodoSaldoTxt.Text),
                        logged_user, DateTime.Today, logged_user, DateTime.Today);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}