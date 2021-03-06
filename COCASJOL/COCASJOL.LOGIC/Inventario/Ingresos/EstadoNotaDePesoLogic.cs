﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    /// <summary>
    /// Clase con logica de Estado de Nota de Peso
    /// </summary>
    public class EstadoNotaDePesoLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(EstadoNotaDePesoLogic).Name);

        public static string PREFIJO_PRIVILEGIO = "DATA_NOTASEN";

        public static string PREFIJO_PLANTILLA = "NOTAS";

        /// <summary>
        /// Constructor.
        /// </summary>
        public EstadoNotaDePesoLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todos los estados de nota de peso.
        /// </summary>
        /// <returns>Lista de estados de nota de peso.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    return db.estados_nota_de_peso.Include("estados_detalles").ToList<estado_nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados activos.
        /// </summary>
        /// <returns></returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePesoActivos()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from esn in db.estados_nota_de_peso.Include("estados_detalles")
                                where esn.ESTADOS_NOTA_ESTADO == true
                                select esn;

                    return query.ToList<estado_nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los estados de nota de peso sin asignar.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <returns>Lista de estados de nota de peso sin asignar.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePesoSinAsignar(int ESTADOS_NOTA_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var lista = db.GetEstadosDeNotaDePesoSinAsignar(ESTADOS_NOTA_ID).ToList<estado_nota_de_peso>();

                    return lista;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso sin asignar.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los estados de nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="ESTADOS_NOTA_SIGUIENTE"></param>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        /// <param name="ESTADOS_NOTA_NOMBRE"></param>
        /// <param name="ESTADOS_NOTA_DESCRIPCION"></param>
        /// <param name="ESTADOS_NOTA_ESTADO"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de estados de nota de peso.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePeso
            (int ESTADOS_NOTA_ID,
            Int32? ESTADOS_NOTA_SIGUIENTE,
            string ESTADOS_NOTA_LLAVE,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
            bool ESTADOS_NOTA_ESTADO,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from estadosn in db.estados_nota_de_peso.Include("estados_nota_de_peso_siguiente").Include("estados_detalles")
                                where
                                (ESTADOS_NOTA_ID.Equals(0) ? true : estadosn.ESTADOS_NOTA_ID.Equals(ESTADOS_NOTA_ID)) &&
                                (ESTADOS_NOTA_SIGUIENTE == null ? true : estadosn.ESTADOS_NOTA_SIGUIENTE == ESTADOS_NOTA_SIGUIENTE) &&
                                (string.IsNullOrEmpty(ESTADOS_NOTA_NOMBRE) ? true : estadosn.ESTADOS_NOTA_NOMBRE.Contains(ESTADOS_NOTA_NOMBRE)) &&
                                (string.IsNullOrEmpty(ESTADOS_NOTA_LLAVE) ? true : estadosn.ESTADOS_NOTA_LLAVE.Contains(ESTADOS_NOTA_LLAVE)) &&
                                (string.IsNullOrEmpty(ESTADOS_NOTA_DESCRIPCION) ? true : estadosn.ESTADOS_NOTA_DESCRIPCION.Contains(ESTADOS_NOTA_DESCRIPCION)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : estadosn.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : estadosn.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : estadosn.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : estadosn.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select estadosn;

                    return query.ToList<estado_nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados iniciales de notas de peso.
        /// </summary>
        /// <returns></returns>
        public List<estado_nota_de_peso> GetEstadosIniciales()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var estadoSiguiente = (from es in db.estados_nota_de_peso
                                           where es.ESTADOS_NOTA_SIGUIENTE != null
                                           select (int)es.ESTADOS_NOTA_SIGUIENTE);

                    var query = from esn in db.estados_nota_de_peso.Include("estados_detalles")
                                where !estadoSiguiente.Contains(esn.ESTADOS_NOTA_ID) &&
                                esn.ESTADOS_NOTA_SIGUIENTE != null &&
                                esn.ESTADOS_NOTA_ESTADO == true
                                select esn;

                    return query.ToList<estado_nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados iniciales de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el estado de nota de peso específico.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <returns>Estado de nota de peso especificado.</returns>
        public estado_nota_de_peso GetEstadoNotaDePeso(int ESTADOS_NOTA_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from esn in db.estados_nota_de_peso
                                where esn.ESTADOS_NOTA_ID == ESTADOS_NOTA_ID
                                select esn;

                    return query.FirstOrDefault<estado_nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estado de nota de peso.", ex);
                throw;
            }
        }
        
        /// <summary>
        /// Obtiene el estado de nota de peso específico.
        /// </summary>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        /// <returns>Estado de nota de peso especificado.</returns>
        public estado_nota_de_peso GetEstadoNotaDePeso(string ESTADOS_NOTA_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from en in db.estados_nota_de_peso
                                where en.ESTADOS_NOTA_LLAVE == ESTADOS_NOTA_LLAVE
                                select en;

                    return query.FirstOrDefault<estado_nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserta el estado de nota de peso con su detalle. Crea privilegio y notificacion para nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_SIGUIENTE"></param>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        /// <param name="ESTADOS_NOTA_NOMBRE"></param>
        /// <param name="ESTADOS_NOTA_DESCRIPCION"></param>
        /// <param name="ESTADOS_NOTA_ES_CATACION"></param>
        /// <param name="ESTADOS_NOTA_ESTADO"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_FECHA"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_ESTADO"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_SOCIO_ID"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE"></param>
        /// <param name="ESTADOS_DETALLE_SHOW_INFO_SOCIO"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_DETALLE"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_TARA"></param>
        /// <param name="ESTADOS_DETALLE_SHOW_DESCUENTOS"></param>
        /// <param name="ESTADOS_DETALLE_SHOW_TOTAL"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN"></param>
        /// <param name="PLANTILLAS_MENSAJE"></param>
        public void InsertarEstadoNotaDePeso
            (int? ESTADOS_NOTA_SIGUIENTE,
            string ESTADOS_NOTA_LLAVE,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
            bool ESTADOS_NOTA_ES_CATACION,
            bool ESTADOS_NOTA_ESTADO,
            string CREADO_POR,
            bool ESTADOS_DETALLE_ENABLE_FECHA,
            int ESTADOS_DETALLE_ENABLE_ESTADO,
            int ESTADOS_DETALLE_ENABLE_SOCIO_ID,
            bool ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE,
            bool ESTADOS_DETALLE_SHOW_INFO_SOCIO,
            bool ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA,
            bool ESTADOS_DETALLE_ENABLE_DETALLE,
            bool ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS,
            bool ESTADOS_DETALLE_ENABLE_TARA,
            bool ESTADOS_DETALLE_SHOW_DESCUENTOS,
            bool ESTADOS_DETALLE_SHOW_TOTAL,
            bool ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN,
            bool ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN,
            string PLANTILLAS_MENSAJE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new System.Transactions.TransactionScope())
                    {
                        estado_nota_de_peso noteStatus = new estado_nota_de_peso();

                        noteStatus.ESTADOS_NOTA_SIGUIENTE = ESTADOS_NOTA_SIGUIENTE == 0 ? null : ESTADOS_NOTA_SIGUIENTE;
                        noteStatus.ESTADOS_NOTA_LLAVE = ESTADOS_NOTA_LLAVE;
                        noteStatus.ESTADOS_NOTA_NOMBRE = ESTADOS_NOTA_NOMBRE;
                        noteStatus.ESTADOS_NOTA_DESCRIPCION = ESTADOS_NOTA_DESCRIPCION;
                        noteStatus.ESTADOS_NOTA_ES_CATACION = ESTADOS_NOTA_ES_CATACION;
                        noteStatus.ESTADOS_NOTA_ESTADO = ESTADOS_NOTA_ESTADO;
                        noteStatus.CREADO_POR = noteStatus.MODIFICADO_POR = CREADO_POR;
                        noteStatus.FECHA_CREACION = DateTime.Today;
                        noteStatus.FECHA_MODIFICACION = noteStatus.FECHA_CREACION;

                        db.estados_nota_de_peso.AddObject(noteStatus);

                        /*--------------------Crear detalle--------------------*/
                        estado_detalle detalle = new estado_detalle();
                        detalle.ESTADOS_NOTA_ID = noteStatus.ESTADOS_NOTA_ID;
                        detalle.ESTADOS_DETALLE_ENABLE_FECHA = ESTADOS_DETALLE_ENABLE_FECHA;
                        detalle.ESTADOS_DETALLE_ENABLE_ESTADO = ESTADOS_DETALLE_ENABLE_ESTADO;
                        detalle.ESTADOS_DETALLE_ENABLE_SOCIO_ID = ESTADOS_DETALLE_ENABLE_SOCIO_ID;
                        detalle.ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE = ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE;
                        detalle.ESTADOS_DETALLE_SHOW_INFO_SOCIO = ESTADOS_DETALLE_SHOW_INFO_SOCIO;
                        detalle.ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA = ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA;
                        detalle.ESTADOS_DETALLE_ENABLE_DETALLE = ESTADOS_DETALLE_ENABLE_DETALLE;
                        detalle.ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS = ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS;
                        detalle.ESTADOS_DETALLE_ENABLE_TARA = ESTADOS_DETALLE_ENABLE_TARA;
                        detalle.ESTADOS_DETALLE_SHOW_DESCUENTOS = ESTADOS_DETALLE_SHOW_DESCUENTOS;
                        detalle.ESTADOS_DETALLE_SHOW_TOTAL = ESTADOS_DETALLE_SHOW_TOTAL;
                        detalle.ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN = ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN;
                        detalle.ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN = ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN;

                        db.estados_detalles.AddObject(detalle);

                        /*--------------------Crear privilegio--------------------*/
                        privilegio notePrivilege = new privilegio();
                        
                        notePrivilege.PRIV_LLAVE = EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + ESTADOS_NOTA_LLAVE;
                        notePrivilege.PRIV_NOMBRE = "Notas de Peso " + ESTADOS_NOTA_NOMBRE;
                        notePrivilege.PRIV_DESCRIPCION = "Acceso a nivel de datos. " + ESTADOS_NOTA_DESCRIPCION;
                        notePrivilege.CREADO_POR = notePrivilege.MODIFICADO_POR = CREADO_POR;
                        notePrivilege.FECHA_CREACION = DateTime.Today;
                        notePrivilege.FECHA_MODIFICACION = notePrivilege.FECHA_CREACION;

                        db.privilegios.AddObject(notePrivilege);

                        /*--------------------Crear plantilla de notificacion--------------------*/
                        plantilla_notificacion noteTemplate = new plantilla_notificacion();

                        noteTemplate.PLANTILLAS_LLAVE = EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + ESTADOS_NOTA_LLAVE;
                        noteTemplate.PLANTILLAS_NOMBRE = "Notas de Peso " + ESTADOS_NOTA_NOMBRE;
                        noteTemplate.PLANTILLAS_ASUNTO = "Notas de Peso " + ESTADOS_NOTA_NOMBRE;
                        noteTemplate.PLANTILLAS_MENSAJE = PLANTILLAS_MENSAJE;
                        noteTemplate.CREADO_POR = noteTemplate.MODIFICADO_POR = CREADO_POR;
                        noteTemplate.FECHA_CREACION = DateTime.Today;
                        noteTemplate.FECHA_MODIFICACION = noteTemplate.FECHA_CREACION;

                        db.plantillas_notificaciones.AddObject(noteTemplate);

                        db.SaveChanges();

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza estado de nota de peso con su detalle. Actualiza privilegio y notificacion.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="ESTADOS_NOTA_SIGUIENTE"></param>
        /// <param name="ESTADOS_NOTA_NOMBRE"></param>
        /// <param name="ESTADOS_NOTA_DESCRIPCION"></param>
        /// <param name="ESTADOS_NOTA_ES_CATACION"></param>
        /// <param name="ESTADOS_NOTA_ESTADO"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_FECHA"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_ESTADO"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_SOCIO_ID"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE"></param>
        /// <param name="ESTADOS_DETALLE_SHOW_INFO_SOCIO"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_DETALLE"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_TARA"></param>
        /// <param name="ESTADOS_DETALLE_SHOW_DESCUENTOS"></param>
        /// <param name="ESTADOS_DETALLE_SHOW_TOTAL"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN"></param>
        /// <param name="ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN"></param>
        /// <param name="PLANTILLAS_MENSAJE"></param>
        public void ActualizarEstadoNotaDePeso
            (int ESTADOS_NOTA_ID,
            int? ESTADOS_NOTA_SIGUIENTE,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
            bool ESTADOS_NOTA_ES_CATACION,
            bool ESTADOS_NOTA_ESTADO,
            string MODIFICADO_POR,
            bool ESTADOS_DETALLE_ENABLE_FECHA,
            int ESTADOS_DETALLE_ENABLE_ESTADO,
            int ESTADOS_DETALLE_ENABLE_SOCIO_ID,
            bool ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE,
            bool ESTADOS_DETALLE_SHOW_INFO_SOCIO,
            bool ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA,
            bool ESTADOS_DETALLE_ENABLE_DETALLE,
            bool ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS,
            bool ESTADOS_DETALLE_ENABLE_TARA,
            bool ESTADOS_DETALLE_SHOW_DESCUENTOS,
            bool ESTADOS_DETALLE_SHOW_TOTAL,
            bool ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN,
            bool ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN,
            string PLANTILLAS_MENSAJE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new System.Transactions.TransactionScope())
                    {
                        EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                        var esn = db.GetObjectByKey(k);

                        estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                        noteStatus.ESTADOS_NOTA_SIGUIENTE = ESTADOS_NOTA_SIGUIENTE == 0 ? null : ESTADOS_NOTA_SIGUIENTE;
                        noteStatus.ESTADOS_NOTA_NOMBRE = ESTADOS_NOTA_NOMBRE;
                        noteStatus.ESTADOS_NOTA_DESCRIPCION = ESTADOS_NOTA_DESCRIPCION;
                        noteStatus.ESTADOS_NOTA_ES_CATACION = ESTADOS_NOTA_ES_CATACION;
                        noteStatus.ESTADOS_NOTA_ESTADO = ESTADOS_NOTA_ESTADO;
                        noteStatus.MODIFICADO_POR = MODIFICADO_POR;
                        noteStatus.FECHA_MODIFICACION = DateTime.Today;

                        /*--------------------Actualizar detalle--------------------*/
                        estado_detalle detalle = noteStatus.estados_detalles;
                        detalle.ESTADOS_DETALLE_ENABLE_FECHA = ESTADOS_DETALLE_ENABLE_FECHA;
                        detalle.ESTADOS_DETALLE_ENABLE_ESTADO = ESTADOS_DETALLE_ENABLE_ESTADO;
                        detalle.ESTADOS_DETALLE_ENABLE_SOCIO_ID = ESTADOS_DETALLE_ENABLE_SOCIO_ID;
                        detalle.ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE = ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE;
                        detalle.ESTADOS_DETALLE_SHOW_INFO_SOCIO = ESTADOS_DETALLE_SHOW_INFO_SOCIO;
                        detalle.ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA = ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA;
                        detalle.ESTADOS_DETALLE_ENABLE_DETALLE = ESTADOS_DETALLE_ENABLE_DETALLE;
                        detalle.ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS = ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS;
                        detalle.ESTADOS_DETALLE_ENABLE_TARA = ESTADOS_DETALLE_ENABLE_TARA;
                        detalle.ESTADOS_DETALLE_SHOW_DESCUENTOS = ESTADOS_DETALLE_SHOW_DESCUENTOS;
                        detalle.ESTADOS_DETALLE_SHOW_TOTAL = ESTADOS_DETALLE_SHOW_TOTAL;
                        detalle.ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN = ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN;
                        detalle.ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN = ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN;


                        /*--------------------Actualizar privilegio--------------------*/
                        string PRIV_LLAVE = EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + noteStatus.ESTADOS_NOTA_LLAVE;

                        var queryPrivilegio = from p in db.privilegios
                                              where p.PRIV_LLAVE == PRIV_LLAVE
                                              select p;

                        privilegio notePrivilege = (privilegio)queryPrivilegio.FirstOrDefault();

                        if (notePrivilege != null)
                        {
                            notePrivilege.PRIV_NOMBRE = "Notas de Peso " + ESTADOS_NOTA_NOMBRE;
                            notePrivilege.PRIV_DESCRIPCION = "Acceso a nivel de datos. " + ESTADOS_NOTA_DESCRIPCION;
                            notePrivilege.MODIFICADO_POR = MODIFICADO_POR;
                            notePrivilege.FECHA_MODIFICACION = DateTime.Today;
                        }


                        /*--------------------Actualizar plantilla de notificacion--------------------*/
                        string PLANTILLAS_LLAVE = EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + noteStatus.ESTADOS_NOTA_LLAVE;

                        db.plantillas_notificaciones.MergeOption = MergeOption.NoTracking;

                        Object pl = null;
                        EntityKey kpl = new EntityKey("colinasEntities.plantillas_notificaciones", "PLANTILLAS_LLAVE", PLANTILLAS_LLAVE);

                        if (db.TryGetObjectByKey(kpl, out pl))
                        {

                            plantilla_notificacion noteTemplate = (plantilla_notificacion)pl;

                            noteTemplate.PLANTILLAS_NOMBRE = "Notas de Peso " + ESTADOS_NOTA_NOMBRE;
                            noteTemplate.PLANTILLAS_ASUNTO = "Notas de Peso " + ESTADOS_NOTA_NOMBRE;
                            noteTemplate.PLANTILLAS_MENSAJE = PLANTILLAS_MENSAJE;
                            noteTemplate.MODIFICADO_POR = MODIFICADO_POR;
                            noteTemplate.FECHA_MODIFICACION = DateTime.Today;
                        }

                        db.SaveChanges();

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Elimina estados de nota de peso con su detalle. Elimina el privilegio y notificacion si existen.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        public void EliminarEstadoNotaDePeso(int ESTADOS_NOTA_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    using (var scope1 = new System.Transactions.TransactionScope())
                    {
                        EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                        var esn = db.GetObjectByKey(k);

                        estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                        db.DeleteObject(noteStatus);

                        /*--------------------Eliminar privilegio--------------------*/
                        string PRIV_LLAVE = EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + noteStatus.ESTADOS_NOTA_LLAVE;

                        var queryPrivilegio = from p in db.privilegios
                                    where p.PRIV_LLAVE == PRIV_LLAVE
                                    select p;
                        
                        privilegio priv = (privilegio)queryPrivilegio.FirstOrDefault();

                        if (priv != null)
                            db.DeleteObject(priv);


                        /*--------------------Eliminar plantilla de notificacion--------------------*/
                        string PLANTILLAS_LLAVE = EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + noteStatus.ESTADOS_NOTA_LLAVE;

                        db.plantillas_notificaciones.MergeOption = MergeOption.NoTracking;

                        Object pl = null;
                        EntityKey kpl = new EntityKey("colinasEntities.plantillas_notificaciones", "PLANTILLAS_LLAVE", PLANTILLAS_LLAVE);

                        if (db.TryGetObjectByKey(kpl, out pl))
                        {
                            plantilla_notificacion plNotif = (plantilla_notificacion)pl;

                            db.DeleteObject(plNotif);
                        }

                        db.SaveChanges();

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Status Update

        /// <summary>
        /// Activa el estado de nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="MODIFICADO_POR"></param>
        public void ActivarEstado(int ESTADOS_NOTA_ID, string MODIFICADO_POR)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                    var esn = db.GetObjectByKey(k);

                    estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                    noteStatus.ESTADOS_NOTA_ESTADO = true;
                    noteStatus.MODIFICADO_POR = MODIFICADO_POR;
                    noteStatus.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al activar estado de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Desactiva el estado de nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="MODIFICADO_POR"></param>
        public void DesactivarEstado(int ESTADOS_NOTA_ID, string MODIFICADO_POR)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                    var esn = db.GetObjectByKey(k);

                    estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                    noteStatus.ESTADOS_NOTA_ESTADO = false;
                    noteStatus.MODIFICADO_POR = MODIFICADO_POR;
                    noteStatus.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al desactivar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Valores para tipos de datasource de estados de notas de peso.
        /// </summary>
        public enum HabilitarEstadosEnNotasDePeso
        {
            Desactivado = 0, ActivadoEnOrden = 1, ActivadoLibre = 2
        }

        /// <summary>
        /// Obtiene todos los tipos de datasource de estados de nota de peso.
        /// </summary>
        /// <returns></returns>
        public static List<object> GetHabilitarEstados()
        {
            try
            {
                List<object> lst = new List<object>
                {
                    new { Text = HabilitarEstadosEnNotasDePeso.Desactivado.ToString(), Value = (int)HabilitarEstadosEnNotasDePeso.Desactivado },
                    new { Text = HabilitarEstadosEnNotasDePeso.ActivadoEnOrden.ToString(), Value = (int)HabilitarEstadosEnNotasDePeso.ActivadoEnOrden },
                    new { Text = HabilitarEstadosEnNotasDePeso.ActivadoLibre.ToString(), Value = (int)HabilitarEstadosEnNotasDePeso.ActivadoLibre }
                };

                return lst;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados para habilitar estado de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el código de estado de nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        /// <returns>Código de estado de nota de peso.</returns>
        public int GetIdEstadoNotaDePeso(string ESTADOS_NOTA_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from en in db.estados_nota_de_peso
                                where en.ESTADOS_NOTA_LLAVE == ESTADOS_NOTA_LLAVE
                                select en;

                    return query.First<estado_nota_de_peso>().ESTADOS_NOTA_ID;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener id de estado de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Verifica la existencia del estado de nota de peso con la llave específica.
        /// </summary>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        /// <returns>True si existe el estado. False si no existe el estado.</returns>
        public bool EstadoDeNotaDePesoExiste(string ESTADOS_NOTA_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.usuarios.MergeOption = MergeOption.NoTracking;

                    var query = from esn in db.estados_nota_de_peso
                                where esn.ESTADOS_NOTA_LLAVE == ESTADOS_NOTA_LLAVE
                                select esn;

                    if (query.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al intentar verificar estado de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Verifica si el estado de nota de peso esta siendo utilizado en notas de peso. Si hay notas de peso en el estado especificado.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <returns>True si hay notas de peso en estado. False si no hay notas de peso en estado.</returns>
        public bool EstadoDeNotaDePesoTieneReferencias(int ESTADOS_NOTA_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                    var esn = db.GetObjectByKey(k);

                    estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                    if (noteStatus.notas_de_peso.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al intentar verificar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion
    }
}
