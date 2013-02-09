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

        public List<reporte_total_inventario_de_cafe_por_socio> GetInventarioDeCafe()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.reporte_total_inventario_de_cafe_por_socio.ToList<reporte_total_inventario_de_cafe_por_socio>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<reporte_total_inventario_de_cafe_por_socio> GetInventarioDeCafe
            (string SOCIOS_ID,
            string SOCIOS_NOMBRE_COMPLETO,
            string CLASIFICACIONES_CAFE_NOMBRE,
            decimal INVENTARIO_ENTRADAS_CANTIDAD,
            decimal INVENTARIO_SALIDAS_SALDO,
            string CREADO_POR,
            DateTime FECHA_CREACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from invCafeSocio in db.reporte_total_inventario_de_cafe_por_socio
                                where
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : invCafeSocio.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_NOMBRE_COMPLETO) ? true : invCafeSocio.SOCIOS_NOMBRE_COMPLETO.Contains(SOCIOS_NOMBRE_COMPLETO)) &&
                                (string.IsNullOrEmpty(CLASIFICACIONES_CAFE_NOMBRE) ? true : invCafeSocio.CLASIFICACIONES_CAFE_NOMBRE.Contains(CLASIFICACIONES_CAFE_NOMBRE)) &&
                                (INVENTARIO_ENTRADAS_CANTIDAD.Equals(-1) ? true : invCafeSocio.INVENTARIO_ENTRADAS_CANTIDAD.Equals(INVENTARIO_ENTRADAS_CANTIDAD)) &&
                                (INVENTARIO_SALIDAS_SALDO.Equals(-1) ? true : invCafeSocio.INVENTARIO_SALIDAS_SALDO.Equals(INVENTARIO_SALIDAS_SALDO)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : invCafeSocio.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : invCafeSocio.FECHA_CREACION == FECHA_CREACION)
                                select invCafeSocio;

                    return query.OrderByDescending(ic => ic.FECHA_CREACION).ToList<reporte_total_inventario_de_cafe_por_socio>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetInventarioDeCafe(string SOCIOS_ID, int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from inv in db.reporte_total_inventario_de_cafe_por_socio
                                where inv.SOCIOS_ID == SOCIOS_ID && inv.CLASIFICACIONES_CAFE_ID == CLASIFICACIONES_CAFE_ID
                                select inv;

                    reporte_total_inventario_de_cafe_por_socio asocInventory = query.FirstOrDefault<reporte_total_inventario_de_cafe_por_socio>();

                    if (asocInventory != null)
                        return asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                    else
                        return 0;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Insert

        //Notas de Peso
        public int InsertarTransaccionInventarioDeCafeDeSocio(nota_de_peso NotaDePeso, colinasEntities db)
        {
            try
            {
                var query = (from inv in db.reporte_total_inventario_de_cafe_por_socio
                             where 
                             (inv.SOCIOS_ID == NotaDePeso.SOCIOS_ID) && 
                             (inv.CLASIFICACIONES_CAFE_ID == NotaDePeso.CLASIFICACIONES_CAFE_ID)
                             select inv).FirstOrDefault();

                decimal cantidad_en_inventario = query == null ? 0 : query.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario = query == null ? 0 : query.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafe = new inventario_cafe_de_socio();
                inventarioDeCafe = new inventario_cafe_de_socio();

                inventarioDeCafe.SOCIOS_ID = NotaDePeso.SOCIOS_ID;
                inventarioDeCafe.CLASIFICACIONES_CAFE_ID = NotaDePeso.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_ID = NotaDePeso.NOTAS_ID;
                inventarioDeCafe.DOCUMENTO_TIPO = "ENTRADA";
                
                inventarioDeCafe.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario + NotaDePeso.NOTAS_PESO_TOTAL_RECIBIDO;
                inventarioDeCafe.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario;

                inventarioDeCafe.CREADO_POR = NotaDePeso.CREADO_POR;
                inventarioDeCafe.FECHA_CREACION = Convert.ToDateTime(NotaDePeso.FECHA_MODIFICACION);

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafe);

                db.SaveChanges();

                return inventarioDeCafe.TRANSACCION_NUMERO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Hojas de Liquidacion
        public void InsertarTransaccionInventarioDeCafeDeSocio(liquidacion HojaDeLiquidacion, colinasEntities db)
        {
            try
            {
                var query = (from inv in db.inventario_cafe_de_socio
                             where inv.SOCIOS_ID == HojaDeLiquidacion.SOCIOS_ID && inv.CLASIFICACIONES_CAFE_ID == HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID
                             select inv).OrderByDescending(i => i.TRANSACCION_NUMERO).FirstOrDefault();

                decimal cantidad_en_inventario = query == null ? 0 : query.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario = query == null ? 0 : query.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafe = new inventario_cafe_de_socio();
                inventarioDeCafe = new inventario_cafe_de_socio();

                inventarioDeCafe.SOCIOS_ID = HojaDeLiquidacion.SOCIOS_ID;
                inventarioDeCafe.CLASIFICACIONES_CAFE_ID = HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_ID = HojaDeLiquidacion.LIQUIDACIONES_ID;
                inventarioDeCafe.DOCUMENTO_TIPO = "SALIDA";

                inventarioDeCafe.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario - HojaDeLiquidacion.LIQUIDACIONES_TOTAL_LIBRAS;
                inventarioDeCafe.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario + HojaDeLiquidacion.LIQUIDACIONES_VALOR_TOTAL;

                inventarioDeCafe.CREADO_POR = HojaDeLiquidacion.CREADO_POR;
                inventarioDeCafe.FECHA_CREACION = HojaDeLiquidacion.FECHA_CREACION;

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafe);

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
