using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Inventario
{
    public class InventarioDeCafeLogic
    {
        public InventarioDeCafeLogic() { }
         
        #region Select

        public List<inventario_cafe_de_socio> GetInventarioDeCafe()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.inventario_cafe_de_socio.MergeOption = MergeOption.NoTracking;

                    return db.inventario_cafe_de_socio.Include("socios").Include("clasificaciones_cafe").OrderBy(ic => ic.SOCIOS_ID).ToList<inventario_cafe_de_socio>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<inventario_cafe_de_socio> GetInventarioDeCafe
            (string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            decimal INVENTARIO_CANTIDAD,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.inventario_cafe_de_socio.MergeOption = MergeOption.NoTracking;

                    var query = from invCafeSocio in db.inventario_cafe_de_socio.Include("socios").Include("clasificaciones_cafe")
                                where
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : invCafeSocio.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : invCafeSocio.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (INVENTARIO_CANTIDAD.Equals(-1) ? true : invCafeSocio.INVENTARIO_CANTIDAD.Equals(INVENTARIO_CANTIDAD)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : invCafeSocio.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : invCafeSocio.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : invCafeSocio.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : invCafeSocio.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select invCafeSocio;

                    return query.OrderBy(ic => ic.SOCIOS_ID).OrderByDescending(ic => ic.FECHA_MODIFICACION).ToList<inventario_cafe_de_socio>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarInventarioDeCafe
            (string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            decimal INVENTARIO_CANTIDAD,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    inventario_cafe_de_socio asocInventory = new inventario_cafe_de_socio();

                    asocInventory.SOCIOS_ID = SOCIOS_ID;
                    asocInventory.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                    asocInventory.INVENTARIO_CANTIDAD = INVENTARIO_CANTIDAD;
                    asocInventory.CREADO_POR = CREADO_POR;
                    asocInventory.FECHA_CREACION = DateTime.Today;
                    asocInventory.MODIFICADO_POR = CREADO_POR;
                    asocInventory.FECHA_MODIFICACION = asocInventory.FECHA_CREACION;

                    db.inventario_cafe_de_socio.AddObject(asocInventory);
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

        public void ActualizarInventarioDeCafe
            (string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            decimal INVENTARIO_CANTIDAD,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k = new EntityKey("colinasEntities.inventario_cafe_de_socio", entityKeyValues);

                    var invCafSoc = db.GetObjectByKey(k);

                    inventario_cafe_de_socio asocInventory = (inventario_cafe_de_socio)invCafSoc;

                    asocInventory.INVENTARIO_CANTIDAD = INVENTARIO_CANTIDAD;
                    asocInventory.MODIFICADO_POR = MODIFICADO_POR;
                    asocInventory.FECHA_MODIFICACION = DateTime.Today;

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

        public void EliminarInventarioDeCafe(string SOCIOS_ID, int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k = new EntityKey("colinasEntities.inventario_cafe_de_socio", entityKeyValues);

                    var invCafSoc = db.GetObjectByKey(k);

                    inventario_cafe_de_socio asocInventory = (inventario_cafe_de_socio)invCafSoc;

                    db.DeleteObject(asocInventory);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
