using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Inventario
{
    public class ClasificacionDeCafeLogic
    {
        public ClasificacionDeCafeLogic() { }

        #region Select 

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
            catch (Exception)
            {

                throw;
            }
        }

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
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Insert

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
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Update

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
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Delete

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
                throw;
            }
        }

        #endregion

        #region Methods

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
                throw;
            }
        }

        public bool NombreDeClasificacionDeCafeExiste(string CLASIFICACIONES_CAFE_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.clasificaciones_cafe.MergeOption = MergeOption.NoTracking;

                    var query = from tp in db.clasificaciones_cafe
                                where tp.CLASIFICACIONES_CAFE_NOMBRE == CLASIFICACIONES_CAFE_NOMBRE
                                select tp;

                    if (query.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool NombreDeClasificacionDeCafeExiste(int CLASIFICACIONES_CAFE_ID, string CLASIFICACIONES_CAFE_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.clasificaciones_cafe.MergeOption = MergeOption.NoTracking;

                    var query = from tp in db.clasificaciones_cafe
                                where tp.CLASIFICACIONES_CAFE_NOMBRE == CLASIFICACIONES_CAFE_NOMBRE && tp.CLASIFICACIONES_CAFE_ID != CLASIFICACIONES_CAFE_ID
                                select tp;

                    if (query.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
