using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Reportes
{
    public class MovimientosDeInventarioDeCafeLogic
    {
        public MovimientosDeInventarioDeCafeLogic() { }

        #region Select

        public List<reporte_movimientos_de_inventario_de_cafe> GetMovimientosDeInventarioDeCafeLogic()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.reporte_movimientos_de_inventario_de_cafe.ToList<reporte_movimientos_de_inventario_de_cafe>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<reporte_movimientos_de_inventario_de_cafe> GetMovimientosDeInventarioDeCafeLogic
            (int TRANSACCION_NUMERO,
            DateTime FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            string SOCIOS_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            string DESCRIPCION,
            decimal ENTRADAS_CANTIDAD,
            decimal SALIDAS_CANTIDAD,
            decimal SALIDAS_COSTO,
            decimal SALIDAS_MONTO,
            decimal INVENTARIO_ENTRADAS_CANTIDAD,
            decimal INVENTARIO_SALIDAS_SALDO)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from mov in db.reporte_movimientos_de_inventario_de_cafe
                                where
                                (TRANSACCION_NUMERO.Equals(0) ? true : mov.TRANSACCION_NUMERO.Equals(TRANSACCION_NUMERO)) &&
                                (FECHA_DESDE == default(DateTime) ? true : mov.FECHA >= FECHA_DESDE) &&
                                (FECHA_HASTA == default(DateTime) ? true : mov.FECHA <= FECHA_HASTA) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : mov.SOCIOS_ID == SOCIOS_ID) &&
                                (string.IsNullOrEmpty(CLASIFICACIONES_CAFE_NOMBRE) ? true : mov.CLASIFICACIONES_CAFE_NOMBRE == CLASIFICACIONES_CAFE_NOMBRE) &&
                                (string.IsNullOrEmpty(DESCRIPCION) ? true : mov.DOCUMENTO_TIPO == DESCRIPCION) &&
                                (ENTRADAS_CANTIDAD.Equals(-1) ? true : mov.ENTRADAS_CANTIDAD == ENTRADAS_CANTIDAD) &&
                                (SALIDAS_CANTIDAD.Equals(-1) ? true : mov.SALIDAS_CANTIDAD == SALIDAS_CANTIDAD) &&
                                (SALIDAS_COSTO.Equals(-1) ? true : mov.SALIDAS_COSTO == SALIDAS_COSTO) &&
                                (SALIDAS_MONTO.Equals(-1) ? true : mov.SALIDAS_MONTO == SALIDAS_MONTO) &&
                                (INVENTARIO_ENTRADAS_CANTIDAD.Equals(-1) ? true : mov.INVENTARIO_ENTRADAS_CANTIDAD == INVENTARIO_ENTRADAS_CANTIDAD) &&
                                (INVENTARIO_SALIDAS_SALDO.Equals(-1) ? true : mov.INVENTARIO_SALIDAS_SALDO == INVENTARIO_SALIDAS_SALDO)
                                select mov;

                    return query.ToList<reporte_movimientos_de_inventario_de_cafe>();
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
