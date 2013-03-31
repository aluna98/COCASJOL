using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Salidas
{
    public class VentaInventarioDeCafeLogic
    {
        private static ILog log = LogManager.GetLogger(typeof(VentaInventarioDeCafeLogic).Name);

        public VentaInventarioDeCafeLogic() { }

        #region Select

        public List<venta_inventario_cafe> GetVentasDeInventarioDeCafe()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.ventas_inventario_cafe.ToList<venta_inventario_cafe>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener ventas de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        public List<venta_inventario_cafe> GetVentasDeInventarioDeCafe
            (int VENTAS_INV_CAFE_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime VENTAS_INV_CAFE_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            decimal VENTAS_INV_CAFE_CANTIDAD_LIBRAS,
            decimal VENTAS_INV_CAFE_PRECIO_LIBRAS,
            decimal VENTAS_INV_CAFE_SALDO_TOTAL,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from v in db.ventas_inventario_cafe.Include("clasificaciones_cafe")
                                where
                                (VENTAS_INV_CAFE_ID == 0 ? true : v.VENTAS_INV_CAFE_ID.Equals(VENTAS_INV_CAFE_ID)) &&
                                (CLASIFICACIONES_CAFE_ID == 0 ? true : v.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&

                                (default(DateTime) == FECHA_DESDE ? true : v.VENTAS_INV_CAFE_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA ? true : v.VENTAS_INV_CAFE_FECHA <= FECHA_HASTA) &&

                                (VENTAS_INV_CAFE_CANTIDAD_LIBRAS == -1 ? true : v.VENTAS_INV_CAFE_PRECIO_LIBRAS.Equals(VENTAS_INV_CAFE_PRECIO_LIBRAS)) &&
                                (VENTAS_INV_CAFE_PRECIO_LIBRAS == -1 ? true : v.VENTAS_INV_CAFE_PRECIO_LIBRAS.Equals(VENTAS_INV_CAFE_PRECIO_LIBRAS)) &&
                                (VENTAS_INV_CAFE_SALDO_TOTAL == -1 ? true : v.VENTAS_INV_CAFE_SALDO_TOTAL.Equals(VENTAS_INV_CAFE_SALDO_TOTAL)) &&

                                (string.IsNullOrEmpty(CREADO_POR) ? true : v.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : v.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : v.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : v.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select v;

                    return query.ToList<venta_inventario_cafe>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener ventas de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarVentaDeInventarioDeCafe
            (int VENTAS_INV_CAFE_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime VENTAS_INV_CAFE_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            decimal VENTAS_INV_CAFE_CANTIDAD_LIBRAS,
            decimal VENTAS_INV_CAFE_PRECIO_LIBRAS,
            decimal VENTAS_INV_CAFE_SALDO_TOTAL,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        venta_inventario_cafe venta_cafe = new venta_inventario_cafe();

                        venta_cafe.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        venta_cafe.VENTAS_INV_CAFE_FECHA = VENTAS_INV_CAFE_FECHA;
                        venta_cafe.VENTAS_INV_CAFE_CANTIDAD_LIBRAS = VENTAS_INV_CAFE_CANTIDAD_LIBRAS;
                        venta_cafe.VENTAS_INV_CAFE_PRECIO_LIBRAS = VENTAS_INV_CAFE_PRECIO_LIBRAS;

                        venta_cafe.VENTAS_INV_CAFE_SALDO_TOTAL = VENTAS_INV_CAFE_CANTIDAD_LIBRAS * VENTAS_INV_CAFE_PRECIO_LIBRAS;

                        venta_cafe.CREADO_POR = venta_cafe.MODIFICADO_POR = CREADO_POR;
                        venta_cafe.FECHA_CREACION = DateTime.Today;
                        venta_cafe.FECHA_MODIFICACION = venta_cafe.FECHA_CREACION;

                        db.ventas_inventario_cafe.AddObject(venta_cafe);

                        db.SaveChanges();

                        InventarioDeCafeLogic inventariodecafelogic = new InventarioDeCafeLogic();
                        inventariodecafelogic.InsertarTransaccionInventarioDeCafe(venta_cafe, db);

                        db.SaveChanges();

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar venta de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        #endregion
    }
}
