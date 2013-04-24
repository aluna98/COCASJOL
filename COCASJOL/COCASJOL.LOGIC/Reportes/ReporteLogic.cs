using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Reportes
{
    /// <summary>
    /// Clase con logica de Reportes
    /// </summary>
    public class ReporteLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(ReporteLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReporteLogic() { }

        /// <summary>
        /// Obtiene notas de peso.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <returns>Lista de notas de peso.</returns>
        public List<nota_de_peso> GetNotasDePeso(int NOTAS_ID = 0)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from n in db.notas_de_peso.Include("socios").Include("clasificaciones_cafe").Include("estados_nota_de_peso")
                                where NOTAS_ID == 0 ? true : n.NOTAS_ID.Equals(NOTAS_ID)
                                select n;

                    return query.ToList<nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los detalles de notas de peso.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <returns>Lista de detalles de notas de peso.</returns>
        public List<nota_detalle> GetNotasDetalle(int NOTAS_ID = 0)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from nd in db.notas_de_peso.Include("notas_detalles")
                                where NOTAS_ID == 0 ? true : nd.NOTAS_ID.Equals(NOTAS_ID)
                                select nd;

                    nota_de_peso nota = query.FirstOrDefault<nota_de_peso>();

                    return nota == null ? new List<nota_detalle>() :  nota.notas_detalles.ToList<nota_detalle>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener detalles de notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene hojas de liquidación.
        /// </summary>
        /// <param name="LIQUIDACIONES_ID"></param>
        /// <returns>Lista de hojas de liquidación.</returns>
        public List<liquidacion> GetHojasDeLiquidacion(int LIQUIDACIONES_ID = 0)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from l in db.liquidaciones.Include("socios").Include("clasificaciones_cafe")
                                where LIQUIDACIONES_ID == 0 ? true : l.LIQUIDACIONES_ID.Equals(LIQUIDACIONES_ID)
                                select l;

                    return query.ToList<liquidacion>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener hojas de liquidacion.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene socios.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>Lista de socios.</returns>
        public List<socio> GetSocios(string SOCIOS_ID = "")
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from s in db.socios.Include("socios_generales").Include("socios_produccion")
                                where s.SOCIOS_ID == SOCIOS_ID
                                select s;

                    return query.ToList<socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener socios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene beneficiarios de socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>Lista de beneficiarios de socio.</returns>
        public List<beneficiario_x_socio> GetBeneficiariosDeSocio(string SOCIOS_ID = "")
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from bs in db.socios.Include("beneficiario_x_socio")
                                where string.IsNullOrEmpty(SOCIOS_ID) ? true : bs.SOCIOS_ID == SOCIOS_ID
                                select bs;

                    socio soc = query.FirstOrDefault<socio>();

                    return soc == null ? new List<beneficiario_x_socio>() :  soc.beneficiario_x_socio.ToList<beneficiario_x_socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener beneficiarios de socios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene solicitudes de prestamo.
        /// </summary>
        /// <param name="SOLICITUDES_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <returns>Lista de solicitudes de prestamo.</returns>
        public List<solicitud_prestamo> GetSolicitudesDePrestamo(int SOLICITUDES_ID = 0 , string SOCIOS_ID = "")
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from sp in db.solicitudes_prestamos.Include("prestamos").Include("socios").Include("socios.socios_generales").Include("socios.socios_produccion")
                                where
                                (SOLICITUDES_ID == 0 ? true : sp.SOLICITUDES_ID.Equals(SOLICITUDES_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : sp.SOCIOS_ID == SOCIOS_ID)
                                select sp;

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
        /// Obtiene referencias de solicitudes de prestamo.
        /// </summary>
        /// <param name="SOLICITUDES_ID"></param>
        /// <param name="REFERENCIAS_TIPO"></param>
        /// <returns>Lista de referencias de solicitudes de prestamo.</returns>
        public List<referencia_x_solicitud> GetReferenciasXSolicitud(int SOLICITUDES_ID = 0, string REFERENCIAS_TIPO = "")
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.solicitudes_prestamos", "SOLICITUDES_ID", SOLICITUDES_ID);

                    Object solic = null;

                    if (db.TryGetObjectByKey(k, out solic))
                    {
                        solicitud_prestamo solicitud = (solicitud_prestamo)solic;

                        var query = from rs in solicitud.referencias_x_solicitudes
                                    where string.IsNullOrEmpty(REFERENCIAS_TIPO) ? true : rs.REFERENCIAS_TIPO == REFERENCIAS_TIPO
                                    select rs;

                        return query.ToList<referencia_x_solicitud>();
                    }
                    else
                        return new List<referencia_x_solicitud>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener referencias por solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene avales de solicitudes de prestamo.
        /// </summary>
        /// <param name="SOLICITUDES_ID"></param>
        /// <returns>Lista de avales de solicitudes de prestamo.</returns>
        public List<aval_x_solicitud> GetAvalesXSolicitud(int SOLICITUDES_ID = 0)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from s in db.solicitudes_prestamos.Include("avales_x_solicitud").Include("avales_x_solicitud.socios").Include("avales_x_solicitud.socios.socios_generales").Include("avales_x_solicitud.socios.socios_produccion")
                                where s.SOLICITUDES_ID == SOLICITUDES_ID
                                select s;

                    solicitud_prestamo solicitud = query.FirstOrDefault();

                    if (solicitud != null)
                    {
                        return solicitud.avales_x_solicitud.ToList<aval_x_solicitud>();
                    }
                    else
                        return new List<aval_x_solicitud>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener avales por solicitud de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el reporte movimientos de inventario de café de socios.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="DOCUMENTO_TIPO"></param>
        /// <param name="FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <returns>Reporte movimientos de inventario de café de socios</returns>
        public List<reporte_movimientos_de_inventario_de_cafe_de_socios> GetMovimientosInventarioDeCafeDeSocios
            (string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string DOCUMENTO_TIPO,
            string FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            string CREADO_POR,
            DateTime FECHA_CREACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from mov in db.reporte_movimientos_de_inventario_de_cafe_de_socios
                                where
                                (FECHA_DESDE == default(DateTime) ? true : mov.FECHA >= FECHA_DESDE) &&
                                (FECHA_HASTA == default(DateTime) ? true : mov.FECHA <= FECHA_HASTA) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : mov.SOCIOS_ID == SOCIOS_ID) &&
                                (CLASIFICACIONES_CAFE_ID == 0 ? true : mov.CLASIFICACIONES_CAFE_ID == CLASIFICACIONES_CAFE_ID) &&
                                (string.IsNullOrEmpty(DOCUMENTO_TIPO) ? true : mov.DOCUMENTO_TIPO == DOCUMENTO_TIPO) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : mov.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : mov.FECHA_CREACION == FECHA_CREACION)
                                select mov;

                    return query.ToList<reporte_movimientos_de_inventario_de_cafe_de_socios>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener el reporte de movimientos de inventario de cafe de socios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el reporte movimientos de inventario de café de cooperativa.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="DOCUMENTO_TIPO"></param>
        /// <param name="FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <returns>Reporte movimientos de inventario de café de cooperativa.</returns>
        public List<reporte_movimientos_de_inventario_de_cafe_de_cooperativa> GetMovimientosInventarioDeCafeDeCooperativa
            (int CLASIFICACIONES_CAFE_ID,
            string DOCUMENTO_TIPO,
            string FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            string CREADO_POR,
            DateTime FECHA_CREACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from mov in db.reporte_movimientos_de_inventario_de_cafe_de_cooperativa
                                where
                                (FECHA_DESDE == default(DateTime) ? true : mov.FECHA >= FECHA_DESDE) &&
                                (FECHA_HASTA == default(DateTime) ? true : mov.FECHA <= FECHA_HASTA) &&
                                (CLASIFICACIONES_CAFE_ID == 0 ? true : mov.CLASIFICACIONES_CAFE_ID == CLASIFICACIONES_CAFE_ID) &&
                                (string.IsNullOrEmpty(DOCUMENTO_TIPO) ? true : mov.DOCUMENTO_TIPO == DOCUMENTO_TIPO) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : mov.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : mov.FECHA_CREACION == FECHA_CREACION)
                                select mov;

                    return query.ToList<reporte_movimientos_de_inventario_de_cafe_de_cooperativa>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener el reporte de movimientos de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene las solicitudes de prestamo por socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="PRESTAMOS_ID"></param>
        /// <returns>Solicitudes de prestamo por socio.</returns>
        public List<solicitud_prestamo> GetPrestamosXSocio(string SOCIOS_ID, int PRESTAMOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from s in db.solicitudes_prestamos.Include("socios").Include("prestamos")
                                where
                                s.SOLICITUD_ESTADO == "APROBADA" &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : s.SOCIOS_ID == SOCIOS_ID) &&
                                (PRESTAMOS_ID == 0 ? true : s.PRESTAMOS_ID == PRESTAMOS_ID)
                                select s;

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
        /// Obtiene el reporte detalle de aportaciones por socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="DOCUMENTO_TIPO"></param>
        /// <param name="FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <returns>Reporte detalle de aportaciones por socio.</returns>
        public List<reporte_detalle_de_aportaciones_por_socio> GetDetalleAportacionesPorSocio
            (string SOCIOS_ID,
            string DOCUMENTO_TIPO,
            string FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            string CREADO_POR,
            DateTime FECHA_CREACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from mov in db.reporte_detalle_de_aportaciones_por_socio
                                where
                                (FECHA_DESDE == default(DateTime) ? true : mov.FECHA >= FECHA_DESDE) &&
                                (FECHA_HASTA == default(DateTime) ? true : mov.FECHA <= FECHA_HASTA) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : mov.SOCIOS_ID == SOCIOS_ID) &&
                                (string.IsNullOrEmpty(DOCUMENTO_TIPO) ? true : mov.DOCUMENTO_TIPO == DOCUMENTO_TIPO) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : mov.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : mov.FECHA_CREACION == FECHA_CREACION)
                                select mov;

                    return query.ToList<reporte_detalle_de_aportaciones_por_socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener el reporte detalle aportaciones por socio.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene reporte de notas de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <returns>Reporte de notas de peso.</returns>
        public List<reporte_notas_de_peso> GetDetalleNotasDePeso
            (int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from np in db.reporte_notas_de_peso
                                where
                                (FECHA_DESDE == default(DateTime) ? true : np.NOTAS_FECHA >= FECHA_DESDE) &&
                                (FECHA_HASTA == default(DateTime) ? true : np.NOTAS_FECHA <= FECHA_HASTA) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : np.SOCIOS_ID == SOCIOS_ID) &&
                                (CLASIFICACIONES_CAFE_ID == 0 ? true : np.CLASIFICACIONES_CAFE_ID == CLASIFICACIONES_CAFE_ID) &&
                                (ESTADOS_NOTA_ID == 0 ? true : np.ESTADOS_NOTA_ID == ESTADOS_NOTA_ID)
                                select np;

                    return query.ToList<reporte_notas_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener el reporte detalle de notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el reporte hojas de liquidación
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <returns>Reporte hojas de liquidación</returns>
        public List<reporte_hojas_de_liquidacion> GetDetalleHojasDeLiquidacion
            (string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from hl in db.reporte_hojas_de_liquidacion
                                where
                                (FECHA_DESDE == default(DateTime) ? true : hl.LIQUIDACIONES_FECHA >= FECHA_DESDE) &&
                                (FECHA_HASTA == default(DateTime) ? true : hl.LIQUIDACIONES_FECHA <= FECHA_HASTA) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : hl.SOCIOS_ID == SOCIOS_ID) &&
                                (CLASIFICACIONES_CAFE_ID == 0 ? true : hl.CLASIFICACIONES_CAFE_ID == CLASIFICACIONES_CAFE_ID)
                                select hl;

                    return query.ToList<reporte_hojas_de_liquidacion>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener el reporte detalle de hojas de liquidacion.", ex);
                throw;
            }
        }
    
    }
}
