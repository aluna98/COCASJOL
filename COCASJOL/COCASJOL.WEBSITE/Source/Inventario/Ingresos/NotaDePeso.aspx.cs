using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    public partial class NotaDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !X.IsAjaxRequest )
            {
                stSocios.DataSource = new COCASJOL.LOGIC.Socios.SociosLogic().getData().Select( r => new { SOCIOS_ID = r.SOCIOS_ID, SOCIOS_NOMBRE = String.Format( "{0} {1}", r.SOCIOS_PRIMER_NOMBRE, r.SOCIOS_PRIMER_APELLIDO ) } );
                stSocios.DataBind();
            }

        }
    }
}