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
using COCASJOL.LOGIC.Socios;

namespace COCASJOL.WEBSITE.Source.Prestamos
{
    public partial class SolicitudPrestamo : COCASJOLBASE
    {
        string Socioid;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RM1.DirectMethodNamespace = "DirectX";
            SociosSt_Reload(null, null);
            if (!X.IsAjaxRequest)
            {
                
            }

            string loggedUsr = Session["username"] as string;
            this.LoggedUserHdn.Text = loggedUsr;
        }

        [DirectMethod]
        public void SeleccionarInteres()
        {
            PrestamosLogic prestamo = new PrestamosLogic();
            int interes = prestamo.Intereses(Convert.ToInt32(AddTipoDeProdIdCmb.Value));
            AddInteresTxt.Text = Convert.ToString(interes);
        }

        [DirectMethod]
        public void RefrescarSocio(string id)
        {
            Socioid = id;
            Socios_Refresh(null, null);
        }

        protected void SolicitudesSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            SolicitudesLogic prestamo = new SolicitudesLogic();
            var store1 = this.SolicitudesGriP.GetStore();
            store1.DataSource = prestamo.getData();
            store1.DataBind();
        }

        protected void SolicitudesSt_Update(object sender, DirectEventArgs e)
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                int id = Convert.ToInt32(EditIdSolicitud.Value.ToString());
                decimal monto = Decimal.Parse(EditMontoTxt.Value.ToString());
                int interes = Convert.ToInt32(EditInteres.Value.ToString());
                decimal promedio3 = Decimal.Parse(EditPromedio.Value.ToString());
                decimal promact = Decimal.Parse(EditProd.Value.ToString());
                
                logica.EditarSolicitud(id, monto, interes, EditPlazo.Text, EditPagoTxt.Text, EditDestinoTxt.Text, EditCargoTxt.Text, promedio3, promact, EditNorteTxt.Text, EditSurTxt.Text,
                    EditEsteTxt.Text, EditOesteTxt.Text, EditCarro.Checked ? 1 : 0, EditAgua.Checked ? 1 : 0, EditLuz.Checked ? 1 : 0, EditCasa.Checked ? 1 : 0, EditBeneficio.Checked ? 1 : 0, EditOtrosTxt.Text,
                    EditCalifCmb.Text, LoggedUserHdn.Text);
                X.Msg.Alert("Solicitudes de Prestamos", "La solicitud se ha modificado satisfactoriamente.").Show();
                EditarSolicitudWin.Hide();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void SociosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            SolicitudesLogic solicitud = new SolicitudesLogic();
            ComboBoxSt.DataSource = solicitud.getSocios();
            ComboBoxSt.DataBind();
        }

        protected void SolicitudesSt_Insert(object sender, DirectEventArgs e)
        {
            try
            {

                SolicitudesLogic logica = new SolicitudesLogic();
                int vehiculo = 0, agua = 0, luz = 0, beneficio = 0, casa = 0;
                if (AddVehiculo.Checked) { vehiculo = 1; }
                if (AddAgua.Checked) { agua = 1; }
                if (AddENEE.Checked) { luz = 1; }
                if (AddCasa.Checked) { casa = 1; }
                if (AddBeneficio.Checked) { beneficio = 1; }
                int tipoPrestamo = Convert.ToInt32(AddTipoDeProdIdCmb.Value.ToString());
                decimal monto = Decimal.Parse(AddMontoTxt.Value.ToString());
                int interes = Convert.ToInt32(AddInteresTxt.Value.ToString());
                decimal promedio = Decimal.Parse(AddPromedioTxt.Value.ToString());
                decimal promact = Decimal.Parse(AddPromActTxt.Value.ToString());
                logica.InsertarSolicitud(cbSociosId.Text, monto, interes, AddPlazoTxt.Text,
                    AddPagoTxt.Text, AddDestinoTxt.Text, tipoPrestamo, AddCargoTxt.Text, promedio,
                    promact, AddNorteTxt.Text, AddSurTxt.Text, AddOesteTxt.Text, AddEsteTxt.Text, vehiculo, agua, luz, casa, beneficio,
                    AddOtrosTxt.Text, AddCalificacion.Text, LoggedUserHdn.Text);
                X.Msg.Alert("Solicitudes de Prestamos", "La solicitud se ha creado satisfactoriamente.").Show();
                NuevaSolicitudWin.Hide();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Socios_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            SolicitudesLogic solicitud = new SolicitudesLogic();
            socio socio = solicitud.getSocio(Socioid);
            socio_produccion produccion = solicitud.getProduccion(Socioid);
            EditSocioid.Text = socio.SOCIOS_ID;
            EditPrimerNombre.Text = socio.SOCIOS_PRIMER_NOMBRE;
            Edit2doNombre.Text = socio.SOCIOS_SEGUNDO_NOMBRE;
            Edit1erApellido.Text = socio.SOCIOS_PRIMER_APELLIDO;
            Edit2doApellido.Text = socio.SOCIOS_SEGUNDO_APELLIDO;
            EditIdentidad.Text = socio.SOCIOS_IDENTIDAD;
            EditLugarNcax.Text = socio.SOCIOS_LUGAR_DE_NACIMIENTO;
            EditCarnetIHCAFE.Text = solicitud.getIHCAFE(Socioid);
            EditRTN.Text = socio.SOCIOS_RTN;
            EditEstadoCivil.Text = socio.SOCIOS_ESTADO_CIVIL;
            EditProfesion.Text = socio.SOCIOS_PROFESION;
            EditTelefono.Text = socio.SOCIOS_TELEFONO;
            EditResidencia.Text = socio.SOCIOS_RESIDENCIA;
            EditManzanas.Text = Convert.ToString(produccion.PRODUCCION_MANZANAS_CULTIVADAS);
            EditUbicacion.Text = produccion.PRODUCCION_UBICACION_FINCA;
        }
    }
}