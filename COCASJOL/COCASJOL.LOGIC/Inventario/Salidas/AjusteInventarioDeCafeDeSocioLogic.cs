using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

namespace COCASJOL.LOGIC.Inventario.Salidas
{
    public class AjusteInventarioDeCafeDeSocioLogic
    {
        public AjusteInventarioDeCafeDeSocioLogic() { }

        #region Select

        public List<ajuste_inventario_cafe_x_socio> GetAjustesDeInventarioDeCafe()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.ajustes_inventario_cafe_x_socio.ToList<ajuste_inventario_cafe_x_socio>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<ajuste_inventario_cafe_x_socio> GetAjustesDeInventarioDeCafe
            (int AJUSTES_INV_CAFE_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime AJUSTES_INV_CAFE_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            decimal AJUSTES_INV_CAFE_CANTIDAD_LIBRAS,
            decimal AJUSTES_INV_CAFE_PRECIO_LIBRAS,
            decimal AJUSTES_INV_CAFE_SALDO_TOTAL,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from v in db.ajustes_inventario_cafe_x_socio.Include("socios").Include("clasificaciones_cafe")
                                where
                                (AJUSTES_INV_CAFE_ID == 0 ? true : v.AJUSTES_INV_CAFE_ID.Equals(AJUSTES_INV_CAFE_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : v.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (CLASIFICACIONES_CAFE_ID == 0 ? true : v.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&

                                (default(DateTime) == FECHA_DESDE ? true : v.AJUSTES_INV_CAFE_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA ? true : v.AJUSTES_INV_CAFE_FECHA <= FECHA_HASTA) &&

                                (AJUSTES_INV_CAFE_CANTIDAD_LIBRAS == -1 ? true : v.AJUSTES_INV_CAFE_PRECIO_LIBRAS.Equals(AJUSTES_INV_CAFE_CANTIDAD_LIBRAS)) &&
                                (AJUSTES_INV_CAFE_PRECIO_LIBRAS == -1 ? true : v.AJUSTES_INV_CAFE_PRECIO_LIBRAS.Equals(AJUSTES_INV_CAFE_PRECIO_LIBRAS)) &&
                                (AJUSTES_INV_CAFE_SALDO_TOTAL == -1 ? true : v.AJUSTES_INV_CAFE_SALDO_TOTAL.Equals(AJUSTES_INV_CAFE_SALDO_TOTAL)) &&

                                (string.IsNullOrEmpty(CREADO_POR) ? true : v.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : v.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : v.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : v.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select v;

                    return query.ToList<ajuste_inventario_cafe_x_socio>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarAjusteDeInventarioDeCafe
            (int AJUSTES_INV_CAFE_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime AJUSTES_INV_CAFE_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            decimal AJUSTES_INV_CAFE_CANTIDAD_LIBRAS,
            decimal AJUSTES_INV_CAFE_PRECIO_LIBRAS,
            decimal AJUSTES_INV_CAFE_SALDO_TOTAL,
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
                        ajuste_inventario_cafe_x_socio ajuste_cafe = new ajuste_inventario_cafe_x_socio();

                        ajuste_cafe.SOCIOS_ID = SOCIOS_ID;
                        ajuste_cafe.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        ajuste_cafe.AJUSTES_INV_CAFE_FECHA = AJUSTES_INV_CAFE_FECHA;
                        ajuste_cafe.AJUSTES_INV_CAFE_CANTIDAD_LIBRAS = AJUSTES_INV_CAFE_CANTIDAD_LIBRAS;
                        ajuste_cafe.AJUSTES_INV_CAFE_PRECIO_LIBRAS = AJUSTES_INV_CAFE_PRECIO_LIBRAS;

                        ajuste_cafe.AJUSTES_INV_CAFE_SALDO_TOTAL = AJUSTES_INV_CAFE_CANTIDAD_LIBRAS * AJUSTES_INV_CAFE_PRECIO_LIBRAS;

                        ajuste_cafe.CREADO_POR = ajuste_cafe.MODIFICADO_POR = CREADO_POR;
                        ajuste_cafe.FECHA_CREACION = DateTime.Today;
                        ajuste_cafe.FECHA_MODIFICACION = ajuste_cafe.FECHA_CREACION;

                        db.ajustes_inventario_cafe_x_socio.AddObject(ajuste_cafe);

                        db.SaveChanges();

                        InventarioDeCafeLogic inventariodecafelogic = new InventarioDeCafeLogic();
                        inventariodecafelogic.InsertarTransaccionInventarioDeCafeDeSocio(ajuste_cafe, db);

                        db.SaveChanges();

                        scope1.Complete();
                    }
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
