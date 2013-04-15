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
    public class ReporteLogic
    {
        private static ILog log = LogManager.GetLogger(typeof(ReporteLogic).Name);

        public ReporteLogic() { }

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

        public List<solicitud_prestamo> GetSolicitudesDePrestamo(int SOLICITUDES_ID = 0 , string SOCIOS_ID = "")
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from sp in db.solicitudes_prestamos.Include("prestamos").Include("socios")
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

        public List<aval_x_solicitud> GetAvalesXSolicitud(int SOLICITUDES_ID = 0)
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

        public List<reporte_movimientos_de_inventario_de_cafe_de_socios> GetMovimientosInventarioDeCafeDeSocios
            (string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string DESCRIPCION,
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
                                (string.IsNullOrEmpty(DESCRIPCION) ? true : mov.DOCUMENTO_TIPO == DESCRIPCION) &&
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

        public List<reporte_movimientos_de_inventario_de_cafe_de_cooperativa> GetMovimientosInventarioDeCafeDeCooperativa
            (int CLASIFICACIONES_CAFE_ID,
            string DESCRIPCION,
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
                                (string.IsNullOrEmpty(DESCRIPCION) ? true : mov.DOCUMENTO_TIPO == DESCRIPCION) &&
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
    }
}
