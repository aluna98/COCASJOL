using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

namespace COCASJOL.WEBSITE.Source.Inventario
{
    public partial class InventarioDeCafePorSocio : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.ValidarCredenciales(typeof(InventarioDeCafePorSocio).Name);
            }
        }
    }
}