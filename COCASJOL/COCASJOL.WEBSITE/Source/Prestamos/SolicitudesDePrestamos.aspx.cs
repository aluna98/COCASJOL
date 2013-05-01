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

using log4net;

namespace COCASJOL.WEBSITE.Source.Prestamos
{
    public partial class SolicitudesDePrestamos : COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(SolicitudesDePrestamos).Name);

        string Socioid;
        int Solicitudid;
        public bool confirm;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.RM1.DirectMethodNamespace = "DirectX";
                SociosSt_Reload(null, null);
                if (!X.IsAjaxRequest)
                {
                    COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic configLogic = new COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic(this.docConfiguracion);
                    if (configLogic.VentanasCargarDatos == true)
                    {
                        this.SolicitudesSt_Reload(null, null);
                    }
                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de solicitudes de prestamos.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void SeleccionarInteres()
        {
           try
            {
                TiposPrestamoLogic prestamo = new TiposPrestamoLogic();
                int interes = prestamo.Intereses(Convert.ToInt32(AddTipoDeProdIdCmb.Value));
                AddInteresTxt.Text = Convert.ToString(interes);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar intereses de prestamos.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoConfirmEnable()
        {
			try{
				RowSelectionModel sm = SolicitudesGriP.SelectionModel.Primary as RowSelectionModel;
				int id = Convert.ToInt32(sm.SelectedRow.RecordID);
				SolicitudesLogic logica = new SolicitudesLogic();
				string estado = logica.getEstado(id);
				if (estado != "RECHAZADA")
				{
					X.Msg.Confirm("Solicitud de Prestamo", "Desea aprobar la solicitud?", new MessageBoxButtonsConfig
					{
						Yes = new MessageBoxButtonConfig
						{
							Handler = "DirectX.DoYesEnable()",
							Text = "Aceptar"
						},
						No = new MessageBoxButtonConfig
						{
							Handler = "DirectX.DoNo()",
							Text = "Cancelar"
						}
					}).Show();
				}
				else
				{
					X.Msg.Alert("ERROR", "La solicitud ya ha sido rechazada").Show();
				}
			}catch(Exception ex){
				log.Fatal("Error fatal al intentar aprobar una solicitud.", ex);
                throw;
			}
        }

        [DirectMethod(RethrowException=true)]
        public void DoConfirmDisable()
        {
			try{
				X.Msg.Confirm("Message", "Desea rechazar la solicitud?", new MessageBoxButtonsConfig
				{
					Yes = new MessageBoxButtonConfig
					{
						Handler = "DirectX.DoYesDisable()",
						Text = "Aceptar"
					},
					No = new MessageBoxButtonConfig
					{
						Handler = "DirectX.DoNo()",
						Text = "Cancelar"
					}
				}).Show();
			}
			catch(Exception ex)
            {
                log.Fatal("Error fatal al intentar rechazar la solicitud", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoYesDisable()
        {
			try{
				confirm = true;
				SolicitudSt_Disable(null, null);
			
			}
			catch(Exception ex)
            {
                log.Fatal("Error fatal al rechazar solicitud", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoYesEnable()
        {
			try{
				confirm = true;
				SolicitudSt_Enable(null, null);
			}
			catch(Exception ex)
            {
                log.Fatal("Error fatal al aprobar solicitud", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void RefrescarSocio(string id)
        {
           try
            {
                Socioid = id;
                Socios_Refresh(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar socios.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void RefrescarAvales(int id)
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                StoreAvales.DataSource = logica.getAvales(id);
                StoreAvales.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar avales.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void refreshTabReferencias()
        {
            try
            {
                RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar tab de referencias.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void refreshTabAvales()
        {
            try
            {
                RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar tab de avales.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void SetIdSolicitud(int id)
        {
            try
            {
                Solicitudid = id;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al configurar id de solicitud de prestamo.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void RefrescarReferencias(int id)
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                StoreReferencias.DataSource = logica.getReferencia(id);
                StoreReferencias.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar referencias.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar aval.", ex);
                throw;
            }
            
        }

        [DirectMethod(RethrowException=true)]
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar referencia.", ex);
                throw;
            }

        }

        [DirectMethod(RethrowException=true)]
        public void SetDatosAval()
        {
            try
            {
                SociosLogic logica = new SociosLogic();
                AportacionLogic aportacion = new AportacionLogic();
                if (EditAvalId.Text != "")
                {
                    SolicitudesLogic solicitud = new SolicitudesLogic();
                    EditAvalAntiguedad.Text = logica.Antiguedad(EditAvalId.Text);
                    string avalNombreCompleto = solicitud.getAvalNombreCompleto(EditAvalId.Text);
                    EditAvalNombre.Text = avalNombreCompleto;
                    EditAvalAportaciones.Text = aportacion.GetSaldoTotalAportacionesXSocio(EditAvalId.Text).ToString();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al configurar datos de aval.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar aval.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void ActualizarReferencias(){
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                    logica.EditarReferencia(EditIdRef.Text, Convert.ToInt32(EditIdSolicitud.Text), EditNombreRef.Text, EditTelRef.Text, EditTipoRef.Text, LoggedUserHdn.Text);
                    this.EditarReferenciaWin.Hide();
                    RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
                    X.Msg.Alert("Referencia", "La referencia ha sido modificada satisfactoriamente").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar referencia.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void EliminarAvales()
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                logica.EliminarAval(EditAvalId.Text, Convert.ToInt32(EditIdSolicitud.Text));
                RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
                X.Msg.Alert("Avales", "El Aval ha sido eliminado satisfactoriamente").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar aval.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void EliminarReferencias()
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                logica.EliminarReferencia(EditIdRef.Text, Convert.ToInt32(EditIdSolicitud.Text));
                RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
                X.Msg.Alert("Referencia", "La referencia ha sido eliminada satisfactoriamente").Show();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar referencia.", ex);
                throw;
            }
        }

        protected void AddFecha_Change(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                DateTime Fecha = DateTime.Parse(this.AddPlazoTxt.Text);
                DateTime Ahora = DateTime.Now;
                UsuarioLogic usuarioLogic = new UsuarioLogic();
                if (Ahora.CompareTo(Fecha)<1)
                {
                    e.Success = false;
                    e.ErrorMessage = "La Fecha debe ser posterior a la actual.";
                }
                else
                    e.Success = true;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al validar la fecha de plazo para la solicitud de prestamo.", ex);
                throw;
            }
        }

        protected void Referencias_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar referencias.", ex);
                throw;
            }
        }

        protected void Avales_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar avales.", ex);
                throw;
            }
		}

        protected void SolicitudesSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
			try
            {
				SolicitudesLogic prestamo = new SolicitudesLogic();
				var store1 = this.SolicitudesGriP.GetStore();
				store1.DataSource = prestamo.getViewSolicitud();
				store1.DataBind();
			}
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar solicitudes de prestamos.", ex);
                throw;
            }
        }

        protected void SolicitudesSt_Update(object sender, DirectEventArgs e)
        {
            try
            {
                SolicitudesLogic logica = new SolicitudesLogic();
                int id = Convert.ToInt32(EditIdSolicitud.Value.ToString());
                decimal monto = Decimal.Parse(EditMontoTxt.Value.ToString());
                int interes = Convert.ToInt32(EditInteres.Value.ToString());
                decimal promedio3 = 0;
                if(EditPromedio != null)
                    promedio3 = Decimal.Parse(EditPromedio.Text);
                decimal promact = 0;
                if(EditProd != null)
                    promact = Decimal.Parse(EditProd.Text);
                logica.EditarSolicitud(id, monto, interes, EditPlazo.Text, EditPagoTxt.Text, 
                    EditDestinoTxt.Text, EditCargoTxt.Text, promedio3, promact, EditNorteTxt.Text, EditSurTxt.Text,
                    EditEsteTxt.Text, EditOesteTxt.Text, EditCarro.Checked ? 1 : 0, EditAgua.Checked ? 1 : 0, 
                    EditLuz.Checked ? 1 : 0, EditCasa.Checked ? 1 : 0, EditBeneficio.Checked ? 1 : 0, EditOtrosTxt.Text,
                    EditCalifCmb.Text, LoggedUserHdn.Text);
                EditarSolicitudWin.Hide();
                X.Msg.Alert("Solicitud de Prestamo", "La solicitud se ha modificado satisfactoriamente.").Show();
                
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar de solicitud de prestamo.", ex);
                throw;
            }
        }

        protected void SociosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                SolicitudesLogic solicitud = new SolicitudesLogic();
                ComboBoxSt.DataSource = solicitud.getSocios();
                ComboBoxSt.DataBind();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar socios.", ex);
                throw;
            }
        }

        protected void SolicitudesSt_Insert(object sender, DirectEventArgs e)
        {
            try
            {

                SolicitudesLogic logica = new SolicitudesLogic();
                int vehiculo = 0, agua = 0, luz = 0, beneficio = 0, casa = 0;
                string norte, sur, oeste, este, otros, calificacion;
                norte = sur = oeste = este = otros = calificacion= "";
                if (AddVehiculo != null && AddAgua != null && AddENEE != null && AddCasa != null && AddBeneficio != null)
                {
                    if (AddVehiculo.Checked) { vehiculo = 1; }
                    if (AddAgua.Checked) { agua = 1; }
                    if (AddENEE.Checked) { luz = 1; }
                    if (AddCasa.Checked) { casa = 1; }
                    if (AddBeneficio.Checked) { beneficio = 1; }
                }
                int tipoPrestamo = Convert.ToInt32(AddTipoDeProdIdCmb.Value.ToString());
                decimal monto = Decimal.Parse(AddMontoTxt.Value.ToString());
                int interes = Convert.ToInt32(AddInteresTxt.Value.ToString());
                decimal promedio=0;
                if (AddPromedioTxt.Text!="")
                     promedio = Decimal.Parse(AddPromedioTxt.Text);
                decimal promact = 0;
                if (AddPromActTxt.Text!="")
                    promact = Decimal.Parse(AddPromActTxt.Text);
                if (AddNorteTxt!= null && AddSurTxt!= null && AddOesteTxt!= null && AddEsteTxt!= null)
                {
                    norte = AddNorteTxt.Text;
                    sur = AddSurTxt.Text;
                    oeste = AddOesteTxt.Text;
                    este = AddEsteTxt.Text;
                }
                if (AddOtrosTxt != null && AddCalificacion != null)
                {
                    otros = AddOtrosTxt.Text;
                    calificacion = AddCalificacion.Text;
                }
                logica.InsertarSolicitud(cbSociosId.Text, monto, interes, AddPlazoTxt.Text,
                    AddPagoTxt.Text, AddDestinoTxt.Text, tipoPrestamo, AddCargoTxt.Text, promedio,
                    promact, norte, sur, oeste, este, vehiculo, agua, luz, casa, beneficio,
                    otros, calificacion, LoggedUserHdn.Text);
                NuevaSolicitudWin.Hide();
                X.Msg.Alert("Solicitud de Prestamo", "La solicitud se ha creado satisfactoriamente.").Show();
                
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar de solicitud de prestamo.", ex);
                throw;
            }
        }

        protected void Socios_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
			try
            {
				SolicitudesLogic solicitud = new SolicitudesLogic();
                COCASJOL.DATAACCESS.socio socio = solicitud.getSocio(Socioid);
                COCASJOL.DATAACCESS.socio_produccion produccion = solicitud.getProduccion(Socioid);
				EditSocioid.Text = socio.SOCIOS_ID;
				EditNombre.Text = socio.SOCIOS_PRIMER_NOMBRE + " " + socio.SOCIOS_SEGUNDO_NOMBRE + " " + socio.SOCIOS_PRIMER_APELLIDO + " " + socio.SOCIOS_SEGUNDO_APELLIDO;
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
			catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar socios.", ex);
                throw;
            }
        }

        protected void SolicitudSt_Disable(object sender, EventArgs e)
        {
			try{
				RowSelectionModel sm = SolicitudesGriP.SelectionModel.Primary as RowSelectionModel;
				int id = Convert.ToInt32(sm.SelectedRow.RecordID);
				if (confirm)
				{
					SolicitudesLogic solicitud = new SolicitudesLogic();
					solicitud.DenegarSolicitud(id);
					confirm = false;
				}
				SolicitudesSt_Reload(null, null);
			}
			catch(Exception ex)
            {
                log.Fatal("Error fatal al rechazar la solicitud.", ex);
                throw;
            }
        }

        protected void SolicitudSt_Enable(object sender, EventArgs e)
        {
			try{
				RowSelectionModel sm = SolicitudesGriP.SelectionModel.Primary as RowSelectionModel;
				int id = Convert.ToInt32(sm.SelectedRow.RecordID);
				if (confirm)
				{
					SolicitudesLogic solicitudes = new SolicitudesLogic();
					solicitudes.AprobarSolicitud(id);
					confirm = false;
				}
				SolicitudesSt_Reload(null, null);
                X.Msg.Alert("Solicitud de Prestamo", "Prestamo Aprobado Exitosamente.").Show();
			}
			catch(Exception ex)
            {
                log.Fatal("Error fatal al aprobar la solicitud.", ex);
                throw;
            }
        }
    }
}