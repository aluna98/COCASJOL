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
using COCASJOL.LOGIC.Aportaciones;

namespace COCASJOL.WEBSITE.Source.Prestamos
{
    public partial class SolicitudesDePrestamos : COCASJOLBASE
    {
        string Socioid;
        int Solicitudid;

        protected void Page_Load(object sender, EventArgs e)
        {
            SolicitudesLogic.getSociosDBISAM();
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

        [DirectMethod]
        public void RefrescarAvales(int id)
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                StoreAvales.DataSource = logica.getAvales(id);
                StoreAvales.DataBind();
            }
            catch (Exception e)
            {

            }
        }

        [DirectMethod]
        public void refreshTabAvales()
        {
            try
            {
                RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
            }
            catch (Exception)
            {
            }
        }

        [DirectMethod]
        public void SetIdSolicitud(int id)
        {
            try
            {
                Solicitudid = id;
            }
            catch (Exception)
            {
            }
        }

        [DirectMethod]
        public void RefrescarReferencias(int id)
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                StoreReferencias.DataSource = logica.getReferencia(id);
                StoreReferencias.DataBind();
            }
            catch (Exception e)
            {

            }
        }

        [DirectMethod]
        public void InsertarAvales()
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                if (logica.BuscarAval(CodigoAval.Text))
                {
                    X.Msg.Alert("Avales", "ERROR: El Aval ya ha sido asignado en prestamos anteriormente").Show();
                }
                else
                {
                    if (CodigoAval.Text == EditSocioid.Text)
                    {
                        X.Msg.Alert("Avales", "ERROR: El solicitante del prestamo y el aval no pueden ser iguales").Show();
                    }
                    else
                    {
                        logica.InsertarAval(CodigoAval.Text, Convert.ToInt32(EditIdSolicitud.Text), AvalCalificacion.Text, LoggedUserHdn.Text);
                        this.NuevoAvalWin.Hide();
                        RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
                        X.Msg.Alert("Avales", "El aval ha sido agregado satisfactoriamente").Show();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [DirectMethod]
        public void InsertarReferencias()
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                if (logica.BuscarId(Convert.ToInt32(EditIdSolicitud.Text), AddReferenciaId.Text))
                {
                    logica.InsertarReferencia(AddReferenciaId.Text, Convert.ToInt32(EditIdSolicitud.Text), AddReferenciaNombre.Text, AddReferenciaTel.Text, AddReferenciaTipo.Value.ToString(), LoggedUserHdn.Text);
                    this.NuevaReferenciaWin.Hide();
                    RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
                    X.Msg.Alert("Referencia", "La referencia ha sido creada satisfactoriamente").Show();
                }
                else
                {
                    X.Msg.Alert("Referencia", "La referencia ya existe, imposible crear duplicados").Show();
                }
            }
            catch (Exception)
            {
               throw;
            }

        }

        [DirectMethod]
        public void SetDatosAval()
        {
            SociosLogic logica = new SociosLogic();
            AportacionLogic aportacion = new AportacionLogic();
            if (EditAvalId.Text != "")
            {
                SolicitudesLogic solicitud = new SolicitudesLogic();
                EditAvalAntiguedad.Text = logica.Antiguedad(EditAvalId.Text);
                socio aval = solicitud.getAval(EditAvalId.Text);
                EditAvalNombre.Text = aval.SOCIOS_PRIMER_NOMBRE + " " + aval.SOCIOS_PRIMER_APELLIDO;
                EditAvalAportaciones.Text = aportacion.GetAportacionesXSocio(EditAvalId.Text).APORTACIONES_SALDO.ToString();
            }
        }

        [DirectMethod]
        public void ActualizarAvales()
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                logica.EditarAval(EditAvalId.Text, Convert.ToInt32(EditIdSolicitud.Text), EditarCalificacionAval.Text, LoggedUserHdn.Text);
                this.EditarAvalWin.Hide();
                RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
                X.Msg.Alert("Avales", "El aval ha sido modificado satisfactoriamente").Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [DirectMethod]
        public void ActualizarReferencias(){
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                    logica.EditarReferencia(EditIdRef.Text, Convert.ToInt32(EditIdSolicitud.Text), EditNombreRef.Text, EditTelRef.Text, EditTipoRef.Text, LoggedUserHdn.Text);
                    this.EditarReferenciaWin.Hide();
                    RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
                    X.Msg.Alert("Referencia", "La referencia ha sido modificada satisfactoriamente").Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [DirectMethod]
        public void EliminarAvales()
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                logica.EliminarAval(EditAvalId.Text, Convert.ToInt32(EditIdSolicitud.Text));
                RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
                X.Msg.Alert("Avales", "El Aval ha sido eliminado satisfactoriamente").Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [DirectMethod]
        public void EliminarReferencias()
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                logica.EliminarReferencia(EditIdRef.Text, Convert.ToInt32(EditIdSolicitud.Text));
                RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
                X.Msg.Alert("Referencia", "La referencia ha sido eliminada satisfactoriamente").Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Referencias_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
            
        }

        protected void Avales_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
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
                    EditCalifCmb.Text, EditCmbEstado.Text, LoggedUserHdn.Text);
                EditarSolicitudWin.Hide();
                X.Msg.Alert("Solicitudes de Prestamos", "La solicitud se ha modificado satisfactoriamente.").Show();
                
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
                NuevaSolicitudWin.Hide();
                X.Msg.Alert("Solicitudes de Prestamos", "La solicitud se ha creado satisfactoriamente.").Show();
                
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