using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Odbc;
using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Prestamos
{
    /// <summary>
    /// Clase con logica de Solicitudes de Prestamo
    /// </summary>
    public class SolicitudesLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(SolicitudesLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public SolicitudesLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todas las solicitudes de prestamo.
        /// </summary>
        /// <returns></returns>
        public List<solicitud_prestamo> getData()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                select solicitud;
                  
                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitudes de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las solicitudes de prestamo pendientes o rechazadas.
        /// </summary>
        /// <returns>Lista de solicitudes de prestamo pendientes o rechazadas.</returns>
        public List<solicitud_prestamo> getViewSolicitud()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                where solicitud.SOLICITUD_ESTADO == "PENDIENTE" || solicitud.SOLICITUD_ESTADO == "RECHAZADA"
                                select solicitud;

                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitudes de prestamo sin aprobar.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las solicitudes de prestamo aprobadas o solicitadas.
        /// </summary>
        /// <returns>Lista de solicitudes de prestamo aprobadas o solicitadas.</returns>
        public List<solicitud_prestamo> getViewPrestamo()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                where solicitud.SOLICITUD_ESTADO == "APROBADA" || solicitud.SOLICITUD_ESTADO == "FINALIZADA"
                                select solicitud;

                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitudes de prestamo aprobadas.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las solicitudes de prestamo por socio aprobadas.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de solicitudes de prestamo por socio aprobadas.</returns>
        public List<solicitud_prestamo> getViewPrestamosXSocio(string id)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios").Include("prestamos")
                                where solicitud.SOLICITUD_ESTADO == "APROBADA" && solicitud.SOCIOS_ID == id
                                select solicitud;

                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitudes de prestamo aprobadas.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las solicitudes de prestamo por socio y prestamo aprobadas.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prestamo"></param>
        /// <returns>Lista de solicitudes de prestamo por socio aprobadas.</returns>
        public List<solicitud_prestamo> getViewPrestamosXSocioXPrestamo(string id, int prestamo)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios").Include("prestamos")
                                where solicitud.SOLICITUD_ESTADO == "APROBADA" && solicitud.SOCIOS_ID == id && solicitud.PRESTAMOS_ID == prestamo
                                select solicitud;

                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitudes de prestamo aprobadas.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las solicitudes de prestamo por prestamo aprobadas.
        /// </summary>
        /// <param name="prestamo"></param>
        /// <returns>Lista de solicitudes de prestamo por prestamo aprobadas.</returns>
        public List<solicitud_prestamo> getViewPrestamosXTipoPrestamo(int prestamo)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios").Include("prestamos")
                                where solicitud.SOLICITUD_ESTADO == "APROBADA" && solicitud.PRESTAMOS_ID == prestamo
                                select solicitud;

                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitudes de prestamo aprobadas.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las solicitudes de prestamo aprobadas.
        /// </summary>
        /// <returns>Lista de solicitudes de prestamo aprobadas.</returns>
        public List<solicitud_prestamo> getViewPrestamosXSocio()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios").Include("prestamos")
                                where solicitud.SOLICITUD_ESTADO == "APROBADA"
                                select solicitud;

                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitudes de prestamo aprobadas.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los socios activos.
        /// </summary>
        /// <returns>Lista de socios activos.</returns>
        public List<socio> getSocios()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from socios in db.socios.Include("socios_generales").Include("socios_produccion")
                                where socios.SOCIOS_ESTATUS >= 1
                                select socios;
                    return query.ToList<socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener socios activos.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene socio
        /// </summary>
        /// <param name="socioid"></param>
        /// <returns>Socio</returns>
        public socio getSocio(string socioid)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from socios in db.socios.Include("socios_generales").Include("socios_produccion")
                                where socios.SOCIOS_ID == socioid
                                select socios;
                    return query.First();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener socio.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Solicitud de prestamo.</returns>
        public solicitud_prestamo getSolicitud(int id)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                where solicitud.SOLICITUDES_ID == id
                                select solicitud;
                    return query.First();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene referencias de solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de referencias de solicitud de prestamo.</returns>
        public List<referencia_x_solicitud> getReferencia(int id)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from referencia in db.referencias_x_solicitudes.Include("solicitudes_prestamos")
                                where referencia.SOLICITUDES_ID == id
                                select referencia;
                    return query.ToList<referencia_x_solicitud>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener referencias de solicitud de prestamo.", ex);
                throw;
            }

        }

        /// <summary>
        /// Obtiene los avales de solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista avales de solicitud de prestamo.</returns>
        public List<aval_x_solicitud> getAvales(int id)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    List<aval_x_solicitud> lista = new List<aval_x_solicitud>();
                    var query = from aval in db.avales_x_solicitud.Include("socios").Include("solicitudes_prestamos")
                                where aval.SOLICITUDES_ID == id
                                select aval;
                    lista = query.ToList<aval_x_solicitud>();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener avales de solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene información de Aval (socio)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Información de Aval (socio)</returns>
        public socio getAval(string id)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    socio lista = new socio();
                    var query = from soc in db.socios
                                where soc.SOCIOS_ID == id
                                select soc;
                    lista = query.First();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener aval.", ex);
                throw;
            }
        }
        #endregion

        #region insert

        /// <summary>
        /// Inserta solicitud de prestamo.
        /// </summary>
        /// <param name="idsocio"></param>
        /// <param name="monto"></param>
        /// <param name="interes"></param>
        /// <param name="plazo"></param>
        /// <param name="pago"></param>
        /// <param name="destino"></param>
        /// <param name="idprestamo"></param>
        /// <param name="cargo"></param>
        /// <param name="promedio3"></param>
        /// <param name="produccion"></param>
        /// <param name="norte"></param>
        /// <param name="sur"></param>
        /// <param name="oeste"></param>
        /// <param name="este"></param>
        /// <param name="vehiculo"></param>
        /// <param name="agua"></param>
        /// <param name="enee"></param>
        /// <param name="casa"></param>
        /// <param name="beneficio"></param>
        /// <param name="cultivos"></param>
        /// <param name="calificacion"></param>
        /// <param name="creadopor"></param>
        public void InsertarSolicitud(string idsocio, decimal monto,
        int interes, string plazo, string pago, string destino, int idprestamo, string cargo, decimal promedio3,
        decimal produccion, string norte, string sur, string oeste, string este, int vehiculo, int agua,
        int enee, int casa, int beneficio, string cultivos, string calificacion, string creadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    solicitud_prestamo solicitud = new solicitud_prestamo();

                    solicitud.SOCIOS_ID = idsocio;
                    solicitud.SOLICITUDES_MONTO = monto;
                    solicitud.SOLICITUDES_INTERES = interes;
                    solicitud.SOLICITUDES_PLAZO = DateTime.Parse(plazo);
                    solicitud.SOLICITUDES_PAGO = pago;
                    solicitud.SOLICITUDES_DESTINO = destino;
                    solicitud.PRESTAMOS_ID = idprestamo;
                    solicitud.SOLICITUDES_CARGO = cargo;
                    solicitud.SOLICITUDES_PROMEDIO3 = promedio3;
                    solicitud.SOLICITUDES_PRODUCCIONACT = produccion;
                    solicitud.SOLICITUDES_NORTE = norte;
                    solicitud.SOLICITUDES_SUR = sur;
                    solicitud.SOLICITUDES_ESTE = este;
                    solicitud.SOLICITUDES_OESTE = oeste;
                    solicitud.SOLICITUDES_VEHICULO = (sbyte)vehiculo;
                    solicitud.SOLICITUDES_AGUA = (sbyte)agua;
                    solicitud.SOLICITUDES_ENEE = (sbyte)enee;
                    solicitud.SOLICITUDES_CASA = (sbyte)casa;
                    solicitud.SOLICITUDES_BENEFICIO = (sbyte)beneficio;
                    solicitud.SOLICITUD_OTROSCULTIVOS = cultivos;
                    solicitud.SOLICITUD_CALIFICACION = calificacion;
                    solicitud.SOLICITUD_ESTADO = "PENDIENTE";
                    solicitud.CREADO_POR = creadopor;
                    solicitud.FECHA_CREACION = DateTime.Today;
                    solicitud.MODIFICADO_POR = creadopor;
                    solicitud.FECHA_MODIFICACION = DateTime.Today;
                    db.solicitudes_prestamos.AddObject(solicitud);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Inserta referencia de solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="solicitudid"></param>
        /// <param name="nombre"></param>
        /// <param name="telefono"></param>
        /// <param name="tipo"></param>
        /// <param name="creadopor"></param>
        public void InsertarReferencia(string id, int solicitudid, string nombre, string telefono, string tipo, string creadopor)
        {

            try
            {
                using (var db = new colinasEntities())
                {
                    referencia_x_solicitud referencia = new referencia_x_solicitud();
                    referencia.SOLICITUDES_ID = solicitudid;
                    referencia.REFERENCIAS_ID = id;
                    referencia.REFERENCIAS_NOMBRE = nombre;
                    referencia.REFERENCIAS_TELEFONO = telefono;
                    referencia.REFERENCIAS_TIPO = tipo;
                    referencia.CREADO_POR = referencia.MODIFICADO_POR = creadopor;
                    referencia.FECHA_CREACION = DateTime.Today;
                    referencia.FECHA_MODIFICACION = DateTime.Today;
                    db.referencias_x_solicitudes.AddObject(referencia);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar referencia de solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Inserta aval de solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="solicitudid"></param>
        /// <param name="calificacion"></param>
        /// <param name="creadopor"></param>
        public void InsertarAval(string id, int solicitudid, string calificacion, string creadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    aval_x_solicitud aval = new aval_x_solicitud();
                    aval.AVALES_CALIFICACION = calificacion;
                    aval.SOCIOS_ID = id;
                    aval.SOLICITUDES_ID = solicitudid;
                    aval.CREADO_POR = creadopor;
                    aval.FECHA_CREACION = DateTime.Today;
                    aval.MODIFICADO_POR = creadopor;
                    aval.FECHA_MODIFICACION = DateTime.Today;
                    db.avales_x_solicitud.AddObject(aval);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar aval solicitud de prestamo.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza solicitud de prestamo.
        /// </summary>
        /// <param name="idsolicitud"></param>
        /// <param name="monto"></param>
        /// <param name="interes"></param>
        /// <param name="plazo"></param>
        /// <param name="pago"></param>
        /// <param name="destino"></param>
        /// <param name="cargo"></param>
        /// <param name="promedio3"></param>
        /// <param name="prodact"></param>
        /// <param name="norte"></param>
        /// <param name="sur"></param>
        /// <param name="este"></param>
        /// <param name="oeste"></param>
        /// <param name="carro"></param>
        /// <param name="agua"></param>
        /// <param name="luz"></param>
        /// <param name="casa"></param>
        /// <param name="beneficio"></param>
        /// <param name="otros"></param>
        /// <param name="calificacion"></param>
        /// <param name="modificadopor"></param>
        public void EditarSolicitud(int idsolicitud,
            decimal monto, int interes, string plazo, string pago,
            string destino, string cargo, decimal promedio3,
            decimal prodact, string norte, string sur,
            string este, string oeste, int carro, int agua,
            int luz, int casa, int beneficio, string otros,
            string calificacion, string modificadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos
                                where idsolicitud == solicitud.SOLICITUDES_ID
                                select solicitud;
                    solicitud_prestamo sol = query.First();
                    sol.SOLICITUDES_MONTO = monto;
                    sol.SOLICITUDES_INTERES = interes;
                    sol.SOLICITUDES_PLAZO = DateTime.Parse(plazo);
                    sol.SOLICITUDES_PAGO = pago;
                    sol.SOLICITUDES_DESTINO = destino;
                    sol.SOLICITUDES_CARGO = cargo;
                    sol.SOLICITUDES_PROMEDIO3 = promedio3;
                    sol.SOLICITUDES_PRODUCCIONACT = prodact;
                    sol.SOLICITUDES_NORTE = norte;
                    sol.SOLICITUDES_SUR = sur;
                    sol.SOLICITUDES_ESTE = este;
                    sol.SOLICITUDES_OESTE = oeste;
                    sol.SOLICITUD_ESTADO = "PENDIENTE";
                    sol.SOLICITUDES_VEHICULO = (sbyte)carro;
                    sol.SOLICITUDES_AGUA = (sbyte)agua;
                    sol.SOLICITUDES_ENEE = (sbyte)luz;
                    sol.SOLICITUDES_CASA = (sbyte)casa;
                    sol.SOLICITUDES_BENEFICIO = (sbyte)beneficio;
                    sol.SOLICITUD_OTROSCULTIVOS = otros;
                    sol.SOLICITUD_CALIFICACION = calificacion;
                    sol.MODIFICADO_POR = modificadopor;
                    sol.FECHA_MODIFICACION = DateTime.Today;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Actualiza referencia de solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="solicitudid"></param>
        /// <param name="nombre"></param>
        /// <param name="telefono"></param>
        /// <param name="tipo"></param>
        /// <param name="modificadopor"></param>
        public void EditarReferencia(string id, int solicitudid, string nombre, string telefono, string tipo, string modificadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from referencia in db.referencias_x_solicitudes
                                where referencia.SOLICITUDES_ID == solicitudid && referencia.REFERENCIAS_ID == id
                                select referencia;

                    referencia_x_solicitud nueva = query.First();
                    nueva.REFERENCIAS_NOMBRE = nombre;
                    nueva.REFERENCIAS_TELEFONO = telefono;
                    nueva.REFERENCIAS_TIPO = tipo;
                    nueva.MODIFICADO_POR = modificadopor;
                    nueva.FECHA_MODIFICACION = DateTime.Today;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar referencia de solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Actualiza aval de solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="solicitudid"></param>
        /// <param name="calificacion"></param>
        /// <param name="modificadopor"></param>
        public void EditarAval(string id, int solicitudid, string calificacion, string modificadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from aval2 in db.avales_x_solicitud
                                where aval2.SOLICITUDES_ID == solicitudid && aval2.SOCIOS_ID == id
                                select aval2;

                    aval_x_solicitud aval = query.First();
                    aval.AVALES_CALIFICACION = calificacion;
                    aval.MODIFICADO_POR = modificadopor;
                    aval.FECHA_MODIFICACION = DateTime.Today;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar aval de solicitud de prestamo.", ex);
                throw;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Elimina referencia de solicitud de prestamo.
        /// </summary>
        /// <param name="ReferenciaId"></param>
        /// <param name="SolicitudId"></param>
        public void EliminarReferencia(string ReferenciaId, int SolicitudId)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from referencia in db.referencias_x_solicitudes
                                where referencia.REFERENCIAS_ID == ReferenciaId && referencia.SOLICITUDES_ID == SolicitudId
                                select referencia;
                    referencia_x_solicitud nueva = query.First();
                    db.referencias_x_solicitudes.DeleteObject(nueva);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar referencia de solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Elimina aval de solicitud de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="solicitudid"></param>
        public void EliminarAval(string id, int solicitudid)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from aval in db.avales_x_solicitud
                                where aval.SOLICITUDES_ID == solicitudid && aval.SOCIOS_ID == id
                                select aval;

                    aval_x_solicitud eliminar = query.First();
                    db.avales_x_solicitud.DeleteObject(eliminar);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar aval de solicitud de prestamo.", ex);
                throw;
            }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Obtiene estado de solicitud de prestamo.
        /// </summary>
        /// <param name="ID_SOLICITUD"></param>
        /// <returns></returns>
        public string getEstado(int ID_SOLICITUD)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.solicitudes_prestamos
                                where soc.SOLICITUDES_ID == ID_SOLICITUD
                                select soc;

                    solicitud_prestamo solicitud = query.First();
                    return solicitud.SOLICITUD_ESTADO;
                }

            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estado.", ex);
                throw;
            }
        }

        /// <summary>
        /// Rechaza solicitud de prestamo.
        /// </summary>
        /// <param name="ID_SOLICITUD"></param>
        public void DenegarSolicitud(int ID_SOLICITUD)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.solicitudes_prestamos
                                where soc.SOLICITUDES_ID == ID_SOLICITUD
                                select soc;

                    solicitud_prestamo solicitud = query.First();
                    solicitud.SOLICITUD_ESTADO = "RECHAZADA";
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al denegar solicitud.", ex);
                throw;
            }
        }

        /// <summary>
        /// Aprueba solicitud de prestamo.
        /// </summary>
        /// <param name="ID_SOLICITUD"></param>
        public void AprobarSolicitud(int ID_SOLICITUD)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.solicitudes_prestamos
                                where soc.SOLICITUDES_ID == ID_SOLICITUD
                                select soc;

                    solicitud_prestamo solicitud = query.First();
                    solicitud.SOLICITUD_ESTADO = "APROBADA";
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al aprobar solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Finaliza solicitud de prestamo.
        /// </summary>
        /// <param name="ID_SOLICITUD"></param>
        public void FinalizarSolicitud(int ID_SOLICITUD)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.solicitudes_prestamos
                                where soc.SOLICITUDES_ID == ID_SOLICITUD
                                select soc;

                    solicitud_prestamo solicitud = query.First();
                    solicitud.SOLICITUD_ESTADO = "FINALIZADA";
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al finalizar solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene información de producción de socio.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Información de produccion de socio.</returns>
        public socio_produccion getProduccion(string id)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    var query = from carnet in db.socios_produccion
                                where carnet.SOCIOS_ID == id
                                select carnet;
                    return query.First();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener produccion de socio.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene código de carnet IHCAFE.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Código de carnet IHCAFE.</returns>
        public string getIHCAFE(string id)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    var query = from carnet in db.socios_generales
                                where carnet.SOCIOS_ID == id
                                select carnet;

                    return query.First().GENERAL_CARNET_IHCAFE;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener IHCAFE de socio.", ex);
                throw;
            }
        }

        /// <summary>
        /// Busca si ya existencia de referencia en solicitudes de prestamo.
        /// </summary>
        /// <param name="SOLICITUDES_ID"></param>
        /// <param name="REFERENCIAS_ID"></param>
        /// <returns>True si existe referencia, False si actualmente no es referencia en otro prestamo.</returns>
        public bool BuscarId(int SOLICITUDES_ID, string REFERENCIAS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    List<referencia_x_solicitud> lista;
                    var query = from nuevo in db.referencias_x_solicitudes
                                where nuevo.SOLICITUDES_ID == SOLICITUDES_ID && nuevo.REFERENCIAS_ID == REFERENCIAS_ID
                                select nuevo;

                    lista = query.ToList<referencia_x_solicitud>();

                    if (lista.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al buscar id de referencia en solicitudes de prestamos.", ex);
                throw;
            }
        }

        /// <summary>
        /// Busca si el socio ya es aval en otra solicitud de prestamo.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>True si el socio ya es aval en otra solicitud de prestamo, False si no lo es.</returns>
        public bool BuscarAval(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from aval in db.avales_x_solicitud.Include("solicitudes_prestamos")
                                where aval.SOCIOS_ID == SOCIOS_ID
                                select aval;
                    List<aval_x_solicitud> lista = query.ToList<aval_x_solicitud>();
                    if (lista.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al buscar aval en solicitudes de prestamos.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el nombre completo de aval.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nombre completo de aval.</returns>
        public string getAvalNombreCompleto(string id)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from soc in db.socios
                                where soc.SOCIOS_ID == id
                                select soc;
                    socio lista = query.First();
                    return lista.SOCIOS_PRIMER_NOMBRE + " " + lista.SOCIOS_PRIMER_APELLIDO;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener aval.", ex);
                throw;
            }
        }

        #endregion
    }
}
