using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Productos
{
    public class ProductoLogic
    {
        public ProductoLogic() { }

        #region Select

        public IQueryable GetProductos()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.productos.MergeOption = MergeOption.NoTracking;

                    return db.productos;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<producto> GetProductos
            (int PRODUCTOS_ID,
            int TIPOS_PROD_ID,
            string TIPOS_PROD_NOMBRE,
            string PRODUCTOS_NOMBRE,
            string PRODUCTOS_DESCRIPCION,
            int PRODUCTOS_CANTIDAD_MIN,
            int PRODUCTOS_EXISTENCIA,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.productos.MergeOption = MergeOption.NoTracking;

                    var query = from prods in db.productos.Include("tipos_productos")
                                where
                                (PRODUCTOS_ID.Equals(-1) ? true : prods.PRODUCTOS_ID.Equals(PRODUCTOS_ID)) &&
                                (TIPOS_PROD_ID.Equals(-1) ? true : prods.TIPOS_PROD_ID.Equals(TIPOS_PROD_ID)) &&
                                (string.IsNullOrEmpty(PRODUCTOS_NOMBRE) ? true : prods.PRODUCTOS_NOMBRE.Contains(PRODUCTOS_NOMBRE)) &&
                                (string.IsNullOrEmpty(PRODUCTOS_DESCRIPCION) ? true : prods.PRODUCTOS_DESCRIPCION.Contains(PRODUCTOS_DESCRIPCION)) &&
                                (PRODUCTOS_CANTIDAD_MIN.Equals(-1) ? true : prods.PRODUCTOS_CANTIDAD_MIN.Equals(PRODUCTOS_CANTIDAD_MIN)) &&
                                (PRODUCTOS_EXISTENCIA.Equals(-1) ? true : prods.PRODUCTOS_EXISTENCIA.Equals(PRODUCTOS_EXISTENCIA)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : prods.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : prods.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : prods.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : prods.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select prods;

                    return query.ToList<producto>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarProducto
            (int PRODUCTOS_ID,
            int TIPOS_PROD_ID,
            string TIPOS_PROD_NOMBRE,
            string PRODUCTOS_NOMBRE,
            string PRODUCTOS_DESCRIPCION,
            int PRODUCTOS_CANTIDAD_MIN,
            int PRODUCTOS_EXISTENCIA,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    producto product = new producto();

                    product.TIPOS_PROD_ID = TIPOS_PROD_ID;
                    product.PRODUCTOS_NOMBRE = PRODUCTOS_NOMBRE;
                    product.PRODUCTOS_DESCRIPCION = PRODUCTOS_DESCRIPCION;
                    product.PRODUCTOS_CANTIDAD_MIN = PRODUCTOS_CANTIDAD_MIN;
                    product.PRODUCTOS_EXISTENCIA = PRODUCTOS_EXISTENCIA;
                    product.CREADO_POR = CREADO_POR;
                    product.FECHA_CREACION = DateTime.Today;
                    product.MODIFICADO_POR = CREADO_POR;
                    product.FECHA_MODIFICACION = product.FECHA_CREACION;

                    db.productos.AddObject(product);
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

        public void ActualizarProducto
            (int PRODUCTOS_ID,
            int TIPOS_PROD_ID,
            string TIPOS_PROD_NOMBRE,
            string PRODUCTOS_NOMBRE,
            string PRODUCTOS_DESCRIPCION,
            int PRODUCTOS_CANTIDAD_MIN,
            int PRODUCTOS_EXISTENCIA,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.productos", "PRODUCTOS_ID", PRODUCTOS_ID);

                    var p = db.GetObjectByKey(k);

                    producto product = (producto)p;

                    product.TIPOS_PROD_ID = TIPOS_PROD_ID;
                    product.PRODUCTOS_NOMBRE = PRODUCTOS_NOMBRE;
                    product.PRODUCTOS_DESCRIPCION = PRODUCTOS_DESCRIPCION;
                    product.PRODUCTOS_CANTIDAD_MIN = PRODUCTOS_CANTIDAD_MIN;
                    product.PRODUCTOS_EXISTENCIA = PRODUCTOS_EXISTENCIA;
                    product.MODIFICADO_POR = MODIFICADO_POR;
                    product.FECHA_MODIFICACION = DateTime.Today;

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

        public void EliminarProducto(int PRODUCTOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.productos", "PRODUCTOS_ID", PRODUCTOS_ID);

                    var p = db.GetObjectByKey(k);

                    producto product = (producto)p;

                    db.DeleteObject(product);

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

        public bool NombreDeProductoExiste(string PRODUCTOS_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.productos.MergeOption = MergeOption.NoTracking;

                    var query = from p in db.productos
                                where p.PRODUCTOS_NOMBRE == PRODUCTOS_NOMBRE
                                select p;

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
