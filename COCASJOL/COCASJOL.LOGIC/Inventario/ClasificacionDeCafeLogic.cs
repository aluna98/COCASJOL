using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Inventario
{
    /// <summary>
    /// Clase con logica de Clasificación de Café
    /// </summary>
    public class ClasificacionDeCafeLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(ClasificacionDeCafeLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClasificacionDeCafeLogic() { }

        #region Select 

        /// <summary>
        /// Obtiene todas las clasificaciones de café.
        /// </summary>
        /// <returns>Lista de todas las clasificaciones de café.</returns>
        public List<clasificacion_cafe> GetClasificacionesDeCafe()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.clasificaciones_cafe.MergeOption = MergeOption.NoTracking;

                    return db.clasificaciones_cafe.OrderBy(cc => cc.CLASIFICACIONES_CAFE_NOMBRE).ToList<clasificacion_cafe>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener clasificaciones de cafe.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las clasificaciones de café.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="CLASIFICACIONES_CAFE_DESCRIPCION"></param>
        /// <param name="CLASIFICACIONES_CAFE_CATACION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de todas las clasificaciones de café.</returns>
        public List<clasificacion_cafe> GetClasificacionesDeCafe
            (int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            string CLASIFICACIONES_CAFE_DESCRIPCION,
            bool CLASIFICACIONES_CAFE_CATACION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.clasificaciones_cafe.MergeOption = MergeOption.NoTracking;

                    var query = from clasifcafe in db.clasificaciones_cafe
                                where
                                (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : clasifcafe.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (string.IsNullOrEmpty(CLASIFICACIONES_CAFE_NOMBRE) ? true : clasifcafe.CLASIFICACIONES_CAFE_NOMBRE.Contains(CLASIFICACIONES_CAFE_NOMBRE)) &&
                                (string.IsNullOrEmpty(CLASIFICACIONES_CAFE_DESCRIPCION) ? true : clasifcafe.CLASIFICACIONES_CAFE_DESCRIPCION.Contains(CLASIFICACIONES_CAFE_DESCRIPCION)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : clasifcafe.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : clasifcafe.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : clasifcafe.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : clasifcafe.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select clasifcafe;

                    return query.OrderBy(cc => cc.CLASIFICACIONES_CAFE_NOMBRE).OrderByDescending(cc => cc.FECHA_MODIFICACION).ToList<clasificacion_cafe>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener clasificaciones de cafe.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserta la clasificación de café.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="CLASIFICACIONES_CAFE_DESCRIPCION"></param>
        /// <param name="CLASIFICACIONES_CAFE_CATACION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        public void InsertarClasificacionDeCafe
            (int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            string CLASIFICACIONES_CAFE_DESCRIPCION,
            bool CLASIFICACIONES_CAFE_CATACION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    clasificacion_cafe coffeeClassif = new clasificacion_cafe();

                    coffeeClassif.CLASIFICACIONES_CAFE_NOMBRE = CLASIFICACIONES_CAFE_NOMBRE;
                    coffeeClassif.CLASIFICACIONES_CAFE_DESCRIPCION = CLASIFICACIONES_CAFE_DESCRIPCION;
                    coffeeClassif.CLASIFICACIONES_CAFE_CATACION = CLASIFICACIONES_CAFE_CATACION;
                    coffeeClassif.CREADO_POR = CREADO_POR;
                    coffeeClassif.FECHA_CREACION = DateTime.Today;
                    coffeeClassif.MODIFICADO_POR = CREADO_POR;
                    coffeeClassif.FECHA_MODIFICACION = coffeeClassif.FECHA_CREACION;

                    db.clasificaciones_cafe.AddObject(coffeeClassif);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar clasificacion de cafe.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza la clasificación de café.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="CLASIFICACIONES_CAFE_DESCRIPCION"></param>
        /// <param name="CLASIFICACIONES_CAFE_CATACION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        public void ActualizarClasificacionDeCafe
            (int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            string CLASIFICACIONES_CAFE_DESCRIPCION,
            bool CLASIFICACIONES_CAFE_CATACION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.clasificaciones_cafe", "CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID);

                    var tp = db.GetObjectByKey(k);

                    clasificacion_cafe coffeeClassif = (clasificacion_cafe)tp;

                    coffeeClassif.CLASIFICACIONES_CAFE_NOMBRE = CLASIFICACIONES_CAFE_NOMBRE;
                    coffeeClassif.CLASIFICACIONES_CAFE_DESCRIPCION = CLASIFICACIONES_CAFE_DESCRIPCION;
                    coffeeClassif.CLASIFICACIONES_CAFE_CATACION = CLASIFICACIONES_CAFE_CATACION;
                    coffeeClassif.MODIFICADO_POR = MODIFICADO_POR;
                    coffeeClassif.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar clasificacion de cafe.", ex);
                throw;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Elimina la clasificación de café.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        public void EliminarClasificacionDeCafe(int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.clasificaciones_cafe", "CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID);

                    var tp = db.GetObjectByKey(k);

                    clasificacion_cafe coffeeClassif = (clasificacion_cafe)tp;

                    db.DeleteObject(coffeeClassif);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar clasificacion de cafe.", ex);
                throw;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Devuelve si la clasificación de café especificada debe pasar por catación
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <returns>Verdaro si debe pasar a catación o falso si no es necesario.</returns>
        public bool ClasificacionDeCafePasaPorCatacion(int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.clasificaciones_cafe", "CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID);

                    var tp = db.GetObjectByKey(k);

                    clasificacion_cafe coffeeClassif = (clasificacion_cafe)tp;

                    return coffeeClassif.CLASIFICACIONES_CAFE_CATACION;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al comprobar si clasificacion de cafe debe pasar por catacion.", ex);
                throw;
            }
        }

        #endregion
    }
}
