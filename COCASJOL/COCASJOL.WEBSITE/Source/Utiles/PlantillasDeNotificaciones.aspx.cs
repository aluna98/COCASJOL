using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Utiles;
using COCASJOL.LOGIC.Web;

namespace COCASJOL.WEBSITE.Source.Utiles
{
    public partial class PlantillasDeNotificaciones : COCASJOL.LOGIC.Web.COCASJOLBASE
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

        protected void PlantillaDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        protected void FormatKeysSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string formatKey = this.EditLlaveTxt.Text;
                PlantillaLogic plantillalogic = new PlantillaLogic();

                this.FormatKeysSt.DataSource = plantillalogic.GetFormatKeys(formatKey);
                this.FormatKeysSt.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}