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

        protected void EstadosNotaPadreSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();

                int siguiente = this.EditPadreIdCmb.Text == "" ? 0 : Convert.ToInt32(this.EditPadreIdCmb.Text);

                this.EstadosNotaPadreSt.DataSource = estadologic.GetEstadosNotaDePesoSinAsignar(siguiente);
                this.EstadosNotaPadreSt.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}