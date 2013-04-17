using System;
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

                    return db.estados_nota_de_peso.ToList<estado_nota_de_peso>();
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
        /// <param name="ESTADOS_NOTA_SIGUIENTE_NOMBRE"></param>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        /// <param name="ESTADOS_NOTA_NOMBRE"></param>
        /// <param name="ESTADOS_NOTA_DESCRIPCION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de estados de nota de peso.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePeso
            (int ESTADOS_NOTA_ID,
            Int32? ESTADOS_NOTA_SIGUIENTE,
            string ESTADOS_NOTA_SIGUIENTE_NOMBRE,
            string ESTADOS_NOTA_LLAVE,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
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

                    var query = from estadosn in db.estados_nota_de_peso.Include("estados_nota_de_peso_siguiente")
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
        /// Obtiene todos los estados de nota de peso para notas en area de pesaje.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <returns>Lista de estados de nota de peso.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePesoEnPesaje(int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                List<estado_nota_de_peso> estadosList = null;

                ClasificacionDeCafeLogic clasificaciondecafelogic = new ClasificacionDeCafeLogic();
                bool catacionFlag = clasificaciondecafelogic.ClasificacionDeCafePasaPorCatacion(CLASIFICACIONES_CAFE_ID);
                
                estadosList = GetEstadosNotaDePeso();

                if (catacionFlag == true)
                    estadosList = estadosList.Where(esn => esn.ESTADOS_NOTA_LLAVE == "CATACION" || esn.ESTADOS_NOTA_LLAVE == "PESAJE").ToList<estado_nota_de_peso>();
                else
                    estadosList = estadosList.Where(esn => esn.ESTADOS_NOTA_LLAVE != "CATACION").ToList<estado_nota_de_peso>();

                return estadosList;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso en area de pesaje.", ex);
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

        #region Update

        /// <summary>
        /// Actualiza estado de nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="ESTADOS_NOTA_SIGUIENTE"></param>
        /// <param name="ESTADOS_NOTA_SIGUIENTE_NOMBRE"></param>
        /// <param name="ESTADOS_NOTA_LLAVE"></param>
        /// <param name="ESTADOS_NOTA_NOMBRE"></param>
        /// <param name="ESTADOS_NOTA_DESCRIPCION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        public void ActualizarEstadoNotaDePeso
            (int ESTADOS_NOTA_ID,
            int? ESTADOS_NOTA_SIGUIENTE,
            string ESTADOS_NOTA_SIGUIENTE_NOMBRE,
            string ESTADOS_NOTA_LLAVE,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                    var esn = db.GetObjectByKey(k);

                    estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                    noteStatus.ESTADOS_NOTA_SIGUIENTE = ESTADOS_NOTA_SIGUIENTE;
                    noteStatus.ESTADOS_NOTA_NOMBRE = ESTADOS_NOTA_NOMBRE;
                    noteStatus.ESTADOS_NOTA_DESCRIPCION = ESTADOS_NOTA_DESCRIPCION;
                    noteStatus.MODIFICADO_POR = MODIFICADO_POR;
                    noteStatus.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar estado de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Methods

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

        #endregion
    }
}
