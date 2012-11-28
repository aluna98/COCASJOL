using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Productos
{
    public class TipoDeProductoLogic
    {
        public TipoDeProductoLogic() { }

        #region Select

        public IQueryable GetTiposDeProducto()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.tipos_productos.MergeOption = MergeOption.NoTracking;

                    return db.tipos_productos;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<tipo_producto> GetTiposDeProducto
            (int TIPOS_PROD_ID,
            string TIPOS_PROD_NOMBRE,
            string TIPOS_PROD_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.tipos_productos.MergeOption = MergeOption.NoTracking;

                    var query = from tprods in db.tipos_productos
                                where
                                (TIPOS_PROD_ID.Equals(0) ? true : tprods.TIPOS_PROD_ID.Equals(TIPOS_PROD_ID)) &&
                                (string.IsNullOrEmpty(TIPOS_PROD_NOMBRE) ? true : tprods.TIPOS_PROD_NOMBRE.Contains(TIPOS_PROD_NOMBRE)) &&
                                (string.IsNullOrEmpty(TIPOS_PROD_DESCRIPCION) ? true : tprods.TIPOS_PROD_DESCRIPCION.Contains(TIPOS_PROD_DESCRIPCION)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : tprods.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : tprods.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : tprods.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : tprods.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select tprods;

                    return query.ToList<tipo_producto>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarTipoDeProducto
            (int TIPOS_PROD_ID,
            string TIPOS_PROD_NOMBRE,
            string TIPOS_PROD_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    tipo_producto productType = new tipo_producto();

                    productType.TIPOS_PROD_ID = TIPOS_PROD_ID;
                    productType.TIPOS_PROD_NOMBRE = TIPOS_PROD_NOMBRE;
                    productType.TIPOS_PROD_DESCRIPCION = TIPOS_PROD_DESCRIPCION;
                    productType.CREADO_POR = CREADO_POR;
                    productType.FECHA_CREACION = DateTime.Today;
                    productType.MODIFICADO_POR = CREADO_POR;
                    productType.FECHA_MODIFICACION = productType.FECHA_CREACION;

                    db.tipos_productos.AddObject(productType);
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

        public void ActualizarTipoDeProducto
            (int TIPOS_PROD_ID,
            string TIPOS_PROD_NOMBRE,
            string TIPOS_PROD_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.tipos_productos", "TIPOS_PROD_ID", TIPOS_PROD_ID);

                    var tp = db.GetObjectByKey(k);

                    tipo_producto productType = (tipo_producto)tp;

                    productType.TIPOS_PROD_NOMBRE = TIPOS_PROD_NOMBRE;
                    productType.TIPOS_PROD_DESCRIPCION = TIPOS_PROD_DESCRIPCION;
                    productType.CREADO_POR = CREADO_POR;
                    productType.FECHA_CREACION = DateTime.Today;
                    productType.MODIFICADO_POR = CREADO_POR;
                    productType.FECHA_MODIFICACION = productType.FECHA_CREACION;

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

        public void EliminarTipoDeProducto(int TIPOS_PROD_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.tipos_productos", "TIPOS_PROD_ID", TIPOS_PROD_ID);

                    var tp = db.GetObjectByKey(k);

                    tipo_producto productType = (tipo_producto)tp;

                    db.DeleteObject(productType);

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
