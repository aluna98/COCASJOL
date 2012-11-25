using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using COCASJOL.LOGIC.Inventario.Ingresos;

namespace COCASJOL.WEBSITE.Source.Inventario.Ingresos
{
    
    public partial class NotaDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !X.IsAjaxRequest )
            {
                stSocios.DataSource = NotaDePesoLogic.GetSocios();
                stSocios.DataBind();
            }

        }


         protected void btnGuardar_OnClick( object sender, DirectEventArgs e )
        {
            var detalle = JSON.Deserialize < Dictionary<string, string>[]>( e.ExtraParams[ "DETALLE" ] );
            NotaDePesoLogic.SaveNotaDePeso( SOCIOS_ID.Value.ToString(), (DateTime)FECHA.Value, 1, Convert.ToDecimal( txtDescuentos.Value ), Convert.ToDecimal( txtPorcentajeHumedad.Value ), detalle );
        }
    }
}