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

namespace COCASJOL.Website.Source.Inventario.Ingresos
{
    public partial class NotasDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    string loggedUsr = Session["username"] as string;
                    this.LoggedUserHdn.Text = loggedUsr;
                    //this.ValidarCredenciales("MANT_TIPOPRODS");
                }
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        protected void NotasDePesoDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }
    }
}