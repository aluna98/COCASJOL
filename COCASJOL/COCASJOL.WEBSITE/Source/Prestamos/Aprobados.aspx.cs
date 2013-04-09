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
    public partial class Aprobados : COCASJOLBASE
    {
		private static ILog log = LogManager.GetLogger(typeof(SolicitudesDePrestamos).Name);

        string Socioid;
        int Solicitudid;
        bool confirm;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SolicitudesLogic.getSociosDBISAM();
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
                log.Fatal("Error fatal al cargar pagina de prestamos aprobados.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void RefrescarSocio(string id)
        {
			try{
				Socioid = id;
				Socios_Refresh(null, null);
			}
			catch (Exception ex)
            {
                log.Fatal("Error fatal al refrescar socio.", ex);
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
                log.Fatal("Error fatal al refrescar avales.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void DoConfirmEnable()
        {
			try{
				X.Msg.Confirm("Message", "Desea finalizar la solicitud?", new MessageBoxButtonsConfig
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
			catch (Exception ex)
            {
                log.Fatal("Error fatal al confirmar la finalizacion del prestamo.", ex);
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
			catch (Exception ex)
            {
                log.Fatal("Error fatal al confirmar finalizacion de prestamo.", ex);
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
                log.Fatal("Error fatal al cargar tab de avales.", ex);
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
                log.Fatal("Error fatal al refrescar avales.", ex);
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
                log.Fatal("Error fatal al asignar el id del prestamo.", ex);
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
                log.Fatal("Error fatal al refrescar socio.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void SetDatosAval()
        {
			try{
				SociosLogic logica = new SociosLogic();
				AportacionLogic aportacion = new AportacionLogic();
				if (EditAvalId.Text != "")
				{
					SolicitudesLogic solicitud = new SolicitudesLogic();
					EditAvalAntiguedad.Text = logica.Antiguedad(EditAvalId.Text);
					socio aval = solicitud.getAval(EditAvalId.Text);
					EditAvalNombre.Text = aval.SOCIOS_PRIMER_NOMBRE + " " + aval.SOCIOS_PRIMER_APELLIDO;
					EditAvalAportaciones.Text = aportacion.GetAportacionesXSocio(EditAvalId.Text).APORTACIONES_SALDO_TOTAL.ToString();
				}
			}
			catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar datos del aval.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void CerrarPrestamo()
        {
            try
            {
                this.EditarSolicitudWin.Hide();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cerrar ventana de prestamo.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void CerrarAvales()
        {
            try
            {
                this.EditarAvalWin.Hide();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cerrar ventana de aval.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void CerrarReferencias(){
            try
            {
                this.EditarReferenciaWin.Hide();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cerrar ventana de referencia.", ex);
                throw;
            }
        }

        protected void Referencias_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
			try{
				RefrescarReferencias(Convert.ToInt32(EditIdSolicitud.Text));
            }
			catch (Exception ex)
            {
                log.Fatal("Error fatal al refrescar referencias.", ex);
                throw;
            }
        }

        protected void Avales_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
			try{
				RefrescarAvales(Convert.ToInt32(EditIdSolicitud.Text));
			}
			catch (Exception ex)
            {
                log.Fatal("Error fatal al refrescar avales.", ex);
                throw;
            }
		}

        protected void SolicitudesSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
			try{
				SolicitudesLogic prestamo = new SolicitudesLogic();
				var store1 = this.SolicitudesGriP.GetStore();
				store1.DataSource = prestamo.getViewPrestamo();
				store1.DataBind();
			}
			catch (Exception ex)
            {
                log.Fatal("Error fatal al refrescar prestamos.", ex);
                throw;
            }
		}

        protected void SociosSt_Reload(object sender, StoreRefreshDataEventArgs e)
        {
			try{
				SolicitudesLogic solicitud = new SolicitudesLogic();
				ComboBoxSt.DataSource = solicitud.getSocios();
				ComboBoxSt.DataBind();
			}
			catch (Exception ex)
            {
                log.Fatal("Error fatal al refrescar socio al ComboBox.", ex);
                throw;
            }
		}

        protected void Socios_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
			try{
				SolicitudesLogic solicitud = new SolicitudesLogic();
				socio socio = solicitud.getSocio(Socioid);
				socio_produccion produccion = solicitud.getProduccion(Socioid);
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
                log.Fatal("Error fatal al refrescar socio.", ex);
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
					solicitudes.FinalizarSolicitud(id);
					confirm = false;
				}
				SolicitudesSt_Reload(null, null);
			}
			catch (Exception ex)
            {
                log.Fatal("Error fatal al finalizar prestamo.", ex);
                throw;
            }
        }
    }
}