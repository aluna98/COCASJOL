using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using COCASJOL.LOGIC.Inventario.Ingresos;

namespace COCASJOL.Website.Source.Inventario.Ingresos
{
    public partial class MantenimientoNotaDePeso : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !X.IsAjaxRequest )
            {
                //stSocios.DataSource = NotaDePesoLogic.GetSocios();
                //stSocios.DataBind();

                //this.ValidarCredenciales("MANT_NOTASPESO");
            }
        }

        protected void btnGuardar_OnClick( object sender, DirectEventArgs e )
        {
            //var detalle = JSON.Deserialize<Dictionary<string, string>[]>( e.ExtraParams[ "DETALLE" ] );
            //NotaDePesoLogic.SaveNotaDePeso( SOCIOS_ID.Value.ToString(), (DateTime)FECHA.Value, 1, Convert.ToDecimal( txtDescuentos.Value ), Convert.ToDecimal( txtPorcentajeHumedad.Value ), detalle );
        }

        [DirectMethodAttribute( RethrowException = true )]
        protected void ApplyFilter()
        {
            //IEnumerable<object> notasDePeso =  COCASJOL.LOGIC.Inventario.Ingresos.NotaDePesoLogic.GetNotasDePeso( SOCIO_ID: f_SOCIO_ID.Text, SOCIO_NOMBRE: f_SOCIO_NOMBRE.Text,
            //NOTA_ID: f_NOTA_ID.Text, NOTA_TIPO_CAFE: f_NOTA_TIPO_CAFE.Text, NOTA_PORCENTAJE_DEFECTO: f_NOTA_PORCENTAJE_DEFECTO.Text, NOTA_CARRO_PROPIO: f_NOTA_CARRO_PROPIO.Text );
            //stNotasPeso.DataSource = notasDePeso;
            
            //stNotasPeso.DataBind();
        }

        protected void stNotasPeso_RefreshData( object sender, StoreRefreshDataEventArgs e )
        {
            ApplyFilter();
        }

    }
}