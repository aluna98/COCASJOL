using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC.Utiles;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    /// <summary>
    /// Clase con logica de Nota de Peso
    /// </summary>
    public class NotaDePesoLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(NotaDePesoLogic).Name);

        /// <summary>
        /// Código de estado de la nota de peso.
        /// </summary>
        public int ESTADOS_NOTA_ID;

        private bool ALL_SOCIOS = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotaDePesoLogic() { }

        /// <summary>
        /// Constructor. Inicializa el código de estado de la nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        public NotaDePesoLogic(string ESTADOS_NOTA_LLAVE)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                estado_nota_de_peso esn = estadologic.GetEstadoNotaDePeso(ESTADOS_NOTA_LLAVE);
                this.ESTADOS_NOTA_ID = esn == null ? 0 : esn.ESTADOS_NOTA_ID;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al construir NotaDePesoLogic.", ex);
                throw;
            }
        }

        public NotaDePesoLogic(string ESTADOS_NOTA_LLAVE, bool SHOW_ALL_SOCIOS)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                estado_nota_de_peso esn = estadologic.GetEstadoNotaDePeso(ESTADOS_NOTA_LLAVE);
                this.ESTADOS_NOTA_ID = esn == null ? 0 : esn.ESTADOS_NOTA_ID;
                this.ALL_SOCIOS = SHOW_ALL_SOCIOS;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al construir NotaDePesoLogic.", ex);
                throw;
            }
        }

        #region Select

        /// <summary>
        /// Obtiene todas las notas de peso de socios activos.
        /// </summary>
        /// <returns>Lista de notas de peso de socios activos.</returns>
        public virtual List<nota_de_peso> GetNotasDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from n in db.notas_de_peso.Include("notas_de_peso").Include("socios").Include("clasificaciones_cafe")
                                where ALL_SOCIOS == true ? true : n.socios.SOCIOS_ESTATUS >= 1
                                select n;

                    return query.OrderBy(n => n.SOCIOS_ID).ToList<nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los detalles de nota de peso específica.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <returns>Lista de detalles de nota de peso específica.</returns>
        public List<nota_detalle> GetDetalleNotaDePeso(int NOTAS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    return note.notas_detalles.OrderByDescending(nd => nd.DETALLES_PESO).ToList<nota_detalle>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener detalles de notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las notas de peso de socios activos.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="ESTADOS_NOTA_NOMBRE"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="NOTAS_FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="NOTAS_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_DEFECTO"></param>
        /// <param name="NOTAS_PORCENTAJE_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PESO_DEFECTO"></param>
        /// <param name="NOTAS_PESO_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_DESCUENTO"></param>
        /// <param name="NOTAS_PESO_SUMA"></param>
        /// <param name="NOTAS_PESO_TARA"></param>
        /// <param name="NOTAS_PESO_TOTAL_RECIBIDO"></param>
        /// <param name="NOTAS_PESO_TOTAL_RECIBIDO_TEXTO"></param>
        /// <param name="NOTAS_SACOS_RETENIDOS"></param>
        /// <param name="TRANSACCION_NUMERO"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de notas de peso de socios activos.</returns>
        public virtual List<nota_de_peso> GetNotasDePeso
            (    int NOTAS_ID,
                 int ESTADOS_NOTA_ID,
              string ESTADOS_NOTA_NOMBRE,
              string SOCIOS_ID,
                 int CLASIFICACIONES_CAFE_ID,
              string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime NOTAS_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            Boolean? NOTAS_TRANSPORTE_COOPERATIVA,
             decimal NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA,
             decimal NOTAS_PORCENTAJE_DEFECTO,
             decimal NOTAS_PORCENTAJE_HUMEDAD,
             decimal NOTAS_PESO_TRANSPORTE_COOPERATIVA,
             decimal NOTAS_PESO_DEFECTO,
             decimal NOTAS_PESO_HUMEDAD,
             decimal NOTAS_PESO_DESCUENTO,
             decimal NOTAS_PESO_SUMA,
             decimal NOTAS_PESO_TARA,
             decimal NOTAS_PESO_TOTAL_RECIBIDO,
              string NOTAS_PESO_TOTAL_RECIBIDO_TEXTO,
                 int NOTAS_SACOS_RETENIDOS,
                 int TRANSACCION_NUMERO,
              string CREADO_POR,
            DateTime FECHA_CREACION,
              string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    var queryEnPesaje = from notasPesoPesaje in db.notas_de_peso.Include("socios").Include("clasificaciones_cafe").Include("estados_nota_de_peso")
                                        where ALL_SOCIOS == true ? true : notasPesoPesaje.socios.SOCIOS_ESTATUS >= 1 &&
                                        (this.ESTADOS_NOTA_ID.Equals(0) ? true : notasPesoPesaje.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID)
                                        select notasPesoPesaje;

                    var query = from notasPeso in queryEnPesaje
                                where
                                (NOTAS_ID.Equals(0) ? true : notasPeso.NOTAS_ID.Equals(NOTAS_ID)) &&
                                (ESTADOS_NOTA_ID.Equals(0) ? true : notasPeso.ESTADOS_NOTA_ID.Equals(ESTADOS_NOTA_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : notasPeso.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : notasPeso.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (default(DateTime) == FECHA_DESDE ? true : notasPeso.NOTAS_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA ? true : notasPeso.NOTAS_FECHA <= FECHA_HASTA) &&
                                (NOTAS_TRANSPORTE_COOPERATIVA == null ? true : notasPeso.NOTAS_TRANSPORTE_COOPERATIVA == NOTAS_TRANSPORTE_COOPERATIVA) &&
                                (NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA.Equals(-1) ? true : notasPeso.NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA.Equals(NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA)) &&
                                (NOTAS_PORCENTAJE_DEFECTO.Equals(-1) ? true : notasPeso.NOTAS_PORCENTAJE_DEFECTO.Equals(NOTAS_PORCENTAJE_DEFECTO)) &&
                                (NOTAS_PORCENTAJE_HUMEDAD.Equals(-1) ? true : notasPeso.NOTAS_PORCENTAJE_HUMEDAD.Equals(NOTAS_PORCENTAJE_HUMEDAD)) &&
                                (NOTAS_PESO_TRANSPORTE_COOPERATIVA.Equals(-1) ? true : notasPeso.NOTAS_PESO_TRANSPORTE_COOPERATIVA.Equals(NOTAS_PESO_TRANSPORTE_COOPERATIVA)) &&
                                (NOTAS_PESO_DEFECTO.Equals(-1) ? true : notasPeso.NOTAS_PESO_DEFECTO.Equals(NOTAS_PESO_DEFECTO)) &&
                                (NOTAS_PESO_DESCUENTO.Equals(-1) ? true : notasPeso.NOTAS_PESO_DESCUENTO.Equals(NOTAS_PESO_DESCUENTO)) &&
                                (NOTAS_PESO_HUMEDAD.Equals(-1) ? true : notasPeso.NOTAS_PESO_HUMEDAD.Equals(NOTAS_PESO_HUMEDAD)) &&
                                (NOTAS_PESO_TARA.Equals(-1) ? true : notasPeso.NOTAS_PESO_TARA.Equals(NOTAS_PESO_TARA)) &&
                                (NOTAS_PESO_SUMA.Equals(-1) ? true : notasPeso.NOTAS_PESO_SUMA.Equals(NOTAS_PESO_SUMA)) &&
                                (NOTAS_PESO_TOTAL_RECIBIDO.Equals(-1) ? true : notasPeso.NOTAS_PESO_TOTAL_RECIBIDO.Equals(NOTAS_PESO_TOTAL_RECIBIDO)) &&
                                (string.IsNullOrEmpty(NOTAS_PESO_TOTAL_RECIBIDO_TEXTO) ? true : notasPeso.SOCIOS_ID.Contains(NOTAS_PESO_TOTAL_RECIBIDO_TEXTO)) &&
                                (NOTAS_SACOS_RETENIDOS.Equals(-1) ? true : notasPeso.NOTAS_SACOS_RETENIDOS.Equals(NOTAS_SACOS_RETENIDOS)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : notasPeso.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : notasPeso.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : notasPeso.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : notasPeso.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select notasPeso;

                    return query.OrderBy(n => n.SOCIOS_ID).OrderByDescending(n => n.FECHA_MODIFICACION).OrderByDescending(n => n.NOTAS_FECHA).ToList<nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados de la nota de peso.
        /// </summary>
        /// <returns>Lista estados de la nota de peso.</returns>
        public virtual List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                EstadoNotaDePesoLogic estadosnotadepesologic = new EstadoNotaDePesoLogic();
                return estadosnotadepesologic.GetEstadosNotaDePeso();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados de nota de peso siguientes.
        /// </summary>
        /// <param name="padre"></param>
        /// <returns>Lista de estados de nota de peso siguientes.</returns>
        protected List<estado_nota_de_peso> GetEstadosSiguiente(estado_nota_de_peso padre)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from esn in db.estados_nota_de_peso
                                where padre.ESTADOS_NOTA_SIGUIENTE <= esn.ESTADOS_NOTA_ID &&
                                      padre.ESTADOS_NOTA_ID != esn.ESTADOS_NOTA_ID
                                select esn;

                    var hijos = query.ToList<estado_nota_de_peso>();
                    hijos.Add(padre);

                    return hijos;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados hijos de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserta la nota de peso. (Sin Implementar, devuelve excepción. No se debería llamar este metodo.)
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="NOTAS_FECHA"></param>
        /// <param name="NOTAS_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_DEFECTO"></param>
        /// <param name="NOTAS_PORCENTAJE_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_SUMA"></param>
        /// <param name="NOTAS_PESO_TARA"></param>
        /// <param name="NOTAS_SACOS_RETENIDOS"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="Detalles"></param>
        /// <param name="NOTA_PORCENTAJEHUMEDADMIN"></param>
        /// <param name="NOTA_TRANSPORTECOOP"></param>
        public virtual void InsertarNotaDePeso
            (int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            Boolean NOTAS_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_DEFECTO,
            decimal NOTAS_PORCENTAJE_HUMEDAD,
            decimal NOTAS_PESO_SUMA,
            decimal NOTAS_PESO_TARA,
            int NOTAS_SACOS_RETENIDOS,
            string CREADO_POR,
            Dictionary<string, string>[] Detalles,
            decimal NOTA_PORCENTAJEHUMEDADMIN,
            decimal NOTA_TRANSPORTECOOP)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar nota de peso (BASE).", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza la nota de peso. (Sin Implementar, devuelve excepción. No se debería llamar este metodo.)
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="NOTAS_FECHA"></param>
        /// <param name="NOTAS_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_DEFECTO"></param>
        /// <param name="NOTAS_PORCENTAJE_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_SUMA"></param>
        /// <param name="NOTAS_PESO_TARA"></param>
        /// <param name="NOTAS_SACOS_RETENIDOS"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="Detalles"></param>
        /// <param name="NOTA_PORCENTAJEHUMEDADMIN"></param>
        /// <param name="NOTA_TRANSPORTECOOP"></param>
        public virtual void ActualizarNotaDePeso
            (int NOTAS_ID,
            int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            Boolean NOTAS_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_DEFECTO,
            decimal NOTAS_PORCENTAJE_HUMEDAD,
            decimal NOTAS_PESO_SUMA,
            decimal NOTAS_PESO_TARA,
            int NOTAS_SACOS_RETENIDOS,
            string MODIFICADO_POR,
            Dictionary<string, string>[] Detalles,
            decimal NOTA_PORCENTAJEHUMEDADMIN,
            decimal NOTA_TRANSPORTECOOP)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar nota de peso (BASE).", ex);
                throw;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifica usuarios sobre cambio de estado de nota de peso.
        /// </summary>
        /// <param name="PLANTILLAS_LLAVE"></param>
        /// <param name="PRIVS_LLAVE"></param>
        /// <param name="note"></param>
        /// <param name="db"></param>
        protected void NotificarUsuarios(string PLANTILLAS_LLAVE, string PRIVS_LLAVE, nota_de_peso note, colinasEntities db)
        {
            try
            {
                string[] notaid = { note.NOTAS_ID.ToString() };

                PlantillaLogic plantillalogic = new PlantillaLogic();
                plantilla_notificacion pl = plantillalogic.GetPlantilla(PLANTILLAS_LLAVE);

                NotificacionLogic notificacionlogic = new NotificacionLogic();
                notificacionlogic.NotifyUsers(PRIVS_LLAVE, EstadosNotificacion.Creado, pl.PLANTILLAS_ASUNTO, pl.PLANTILLAS_MENSAJE, notaid);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al notificar usuarios.", ex);
                throw;
            }
        }

        #endregion
    }
}
