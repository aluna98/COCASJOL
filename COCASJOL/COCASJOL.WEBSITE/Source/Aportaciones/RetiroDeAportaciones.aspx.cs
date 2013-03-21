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
    public partial class RetiroDeAportaciones : COCASJOL.LOGIC.Web.COCASJOLBASE
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

        protected void RetiroAportacionesDs_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        [DirectMethod(RethrowException = true)]
        public void RetirarBtn_Click()
        {
            try
            {
                string logged_user = Session["username"] as string;

                RetiroAportacionLogic retirologic = new RetiroAportacionLogic();
                retirologic.InsertarRetiroDeAportaciones(this.AddSociosIdTxt.Text,
                        this.AddFechaRetiroTxt.SelectedDate,
                        Convert.ToDecimal(this.AddRetiroAportacionOrdinariaSaldoTxt.Text),
                        Convert.ToDecimal(this.AddRetiroAportacionExtraordinariaSaldoTxt.Text),
                        Convert.ToDecimal(this.AddRetiroAportacionCapRetencionSaldoTxt.Text),
                        Convert.ToDecimal(this.AddRetiroAportacionInteresesSAportacionesSaldoTxt.Text),
                        Convert.ToDecimal(this.AddRetiroAportacionExcedentePeriodoSaldoTxt.Text),
                        logged_user, DateTime.Today, logged_user, DateTime.Today);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}