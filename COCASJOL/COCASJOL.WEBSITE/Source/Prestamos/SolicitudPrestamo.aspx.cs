using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COCASJOL.LOGIC;
using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Web;
using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Prestamos;

namespace COCASJOL.WEBSITE.Source.Prestamos
{
    public partial class SolicitudPrestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SociosSt_Reload(null, null);
            if (!X.IsAjaxRequest)
            {
                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
        }

        protected void SolicitudesSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            SolicitudesLogic prestamo = new SolicitudesLogic();
            var store1 = this.SolicitudesGriP.GetStore();
            store1.DataSource = prestamo.getData();
            store1.DataBind();
        }

        protected void SociosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            SolicitudesLogic solicitud = new SolicitudesLogic();
            ComboBoxSt.DataSource = solicitud.getSocios();
            ComboBoxSt.DataBind();
        }

        protected void SolicitudesSt_Insert(object sender, DirectEventArgs e)
        {
            SolicitudesLogic logica = new SolicitudesLogic();
            int vehiculo=0, agua=0, luz=0, beneficio=0, casa=0;
            if (AddVehiculo.Checked) { vehiculo = 1; }
            if (AddAgua.Checked) { agua = 1; }
            if (AddENEE.Checked) { luz = 1; }
            if(AddCasa.Checked) { casa = 1;}
            if (AddBeneficio.Checked) { beneficio = 1; }
            logica.InsertarSolicitud(cbSociosId.Text, Decimal.Parse(AddMontoTxt.Text), Convert.ToInt32(AddInteresTxt.Text), AddPlazoTxt.Text,
                AddPagoTxt.Text, AddDestinoTxt.Text, (int)AddTipoDeProdIdCmb.Value, AddCargoTxt.Text, Decimal.Parse(AddPromedioTxt.Text),
                Decimal.Parse(AddPromActTxt.Text), AddNorteTxt.Text, AddSurTxt.Text, AddOesteTxt.Text, AddEsteTxt.Text, vehiculo, agua, luz, casa, beneficio,
                AddOtrosTxt.Text, AddCalificacion.Text, LoggedUserHdn.Text);
            X.Msg.Alert("Solicitudes de Prestamos", "La solicitud se ha creado satisfactoriamente.").Show();
            NuevaSolicitudWin.Hide();
        }


    }
}