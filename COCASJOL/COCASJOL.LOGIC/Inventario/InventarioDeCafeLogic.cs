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
    /// Clase con logica de Inventario de Café de Socios y Cooperativa.
    /// </summary>
    public class InventarioDeCafeLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(InventarioDeCafeLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public InventarioDeCafeLogic() { }
         
        #region Select

        /// <summary>
        /// Obtiene todos los totales de inventario de café por socio.
        /// </summary>
        /// <returns>Lista de totales de inventario de café por socio.</returns>
        public List<reporte_total_inventario_de_cafe_por_socio> GetInventarioDeCafeDeSocios()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.reporte_total_inventario_de_cafe_por_socio.ToList<reporte_total_inventario_de_cafe_por_socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte total de inventario de cafe por socios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los totales de inventario de café por socio.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="SOCIOS_NOMBRE_COMPLETO"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="INVENTARIO_ENTRADAS_CANTIDAD"></param>
        /// <param name="INVENTARIO_SALIDAS_SALDO"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <returns>Lista de totales de inventario de café por socio.</returns>
        public List<reporte_total_inventario_de_cafe_por_socio> GetInventarioDeCafeDeSocios
            (string SOCIOS_ID,
            string SOCIOS_NOMBRE_COMPLETO,
            int CLASIFICACIONES_CAFE_ID,
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
                                (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : invCafeSocio.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (INVENTARIO_ENTRADAS_CANTIDAD.Equals(-1) ? true : invCafeSocio.INVENTARIO_ENTRADAS_CANTIDAD.Equals(INVENTARIO_ENTRADAS_CANTIDAD)) &&
                                (INVENTARIO_SALIDAS_SALDO.Equals(-1) ? true : invCafeSocio.INVENTARIO_SALIDAS_SALDO.Equals(INVENTARIO_SALIDAS_SALDO)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : invCafeSocio.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : invCafeSocio.FECHA_CREACION == FECHA_CREACION)
                                select invCafeSocio;

                    return query.OrderByDescending(ic => ic.FECHA_CREACION).ToList<reporte_total_inventario_de_cafe_por_socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte total de inventario de cafe por socios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene totales de inventario de café para socio específico.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <returns>Totales de inventario de café para socio específico.</returns>
        public reporte_total_inventario_de_cafe_por_socio GetReporteTotalInventarioDeCafeDeSocio(string SOCIOS_ID, int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k = new EntityKey("colinasEntities.reporte_total_inventario_de_cafe_por_socio", entityKeyValues);

                    Object invCafSoc = null;

                    reporte_total_inventario_de_cafe_por_socio asocInventory = null;

                    if (db.TryGetObjectByKey(k, out invCafSoc))
                        asocInventory = (reporte_total_inventario_de_cafe_por_socio)invCafSoc;
                    else
                        asocInventory = new reporte_total_inventario_de_cafe_por_socio();

                    return asocInventory;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte total de inventario de cafe de socio.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene total de inventario de café para socio específico.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <returns>Total de inventario de café para socio específico.</returns>
        public decimal GetInventarioDeCafeDeSocio(string SOCIOS_ID, int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k = new EntityKey("colinasEntities.reporte_total_inventario_de_cafe_por_socio", entityKeyValues);

                    Object invCafSoc = null;

                    if (db.TryGetObjectByKey(k, out invCafSoc))
                    {
                        reporte_total_inventario_de_cafe_por_socio asocInventory = (reporte_total_inventario_de_cafe_por_socio)invCafSoc;
                        return asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                    }
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener total de inventario de cafe de socio.", ex);
                throw;
            }
        }


        /*                       Inventario Cooperativa                    */
        /// <summary>
        /// Obtiene todos los totales de inventario de café de Cooperativa.
        /// </summary>
        /// <returns>Lista de totales de inventario de café de Cooperativa.</returns>
        public List<reporte_total_inventario_de_cafe> GetInventarioDeCafe()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.reporte_total_inventario_de_cafe.ToList<reporte_total_inventario_de_cafe>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte total de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los totales de inventario de café de Cooperativa.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="INVENTARIO_ENTRADAS_CANTIDAD"></param>
        /// <param name="INVENTARIO_SALIDAS_SALDO"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <returns>Lista de totales de inventario de café de Cooperativa.</returns>
        public List<reporte_total_inventario_de_cafe> GetInventarioDeCafe
            (int CLASIFICACIONES_CAFE_ID,
            decimal INVENTARIO_ENTRADAS_CANTIDAD,
            decimal INVENTARIO_SALIDAS_SALDO,
            string CREADO_POR,
            DateTime FECHA_CREACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from invCafe in db.reporte_total_inventario_de_cafe
                                where
                                (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : invCafe.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (INVENTARIO_ENTRADAS_CANTIDAD.Equals(-1) ? true : invCafe.INVENTARIO_ENTRADAS_CANTIDAD.Equals(INVENTARIO_ENTRADAS_CANTIDAD)) &&
                                (INVENTARIO_SALIDAS_SALDO.Equals(-1) ? true : invCafe.INVENTARIO_SALIDAS_SALDO.Equals(INVENTARIO_SALIDAS_SALDO)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : invCafe.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : invCafe.FECHA_CREACION == FECHA_CREACION)
                                select invCafe;

                    return query.OrderByDescending(ic => ic.FECHA_CREACION).ToList<reporte_total_inventario_de_cafe>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte total de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene totales de inventario de café de cooperativa.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <returns>Totales de inventario de café de cooperativa.</returns>
        public reporte_total_inventario_de_cafe GetReporteTotalInventarioDeCafe(int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.reporte_total_inventario_de_cafe", "CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID);

                    Object invCaf = null;

                    reporte_total_inventario_de_cafe inventory = null;

                    if (db.TryGetObjectByKey(k, out invCaf))
                        inventory = (reporte_total_inventario_de_cafe)invCaf;
                    else
                        inventory = new reporte_total_inventario_de_cafe();

                    return inventory;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte total de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene total de inventario de café de cooperativa.
        /// </summary>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <returns>Total de inventario de café de cooperativa.</returns>
        public decimal GetInventarioDeCafe(int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.reporte_total_inventario_de_cafe", "CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID);

                    Object invCaf = null;

                    if (db.TryGetObjectByKey(k, out invCaf))
                    {
                        reporte_total_inventario_de_cafe inventory = (reporte_total_inventario_de_cafe)invCaf;
                        return inventory.INVENTARIO_ENTRADAS_CANTIDAD;
                    }
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener total de inventario de cafe de cooperativa.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /*                                      Inventario de Socios                                   */


        /// <summary>
        /// Insertar transacción de la nota de peso en la tabla de inventario de café de socios como entrada (Deposito).
        /// </summary>
        /// <param name="NotaDePeso"></param>
        /// <param name="db"></param>
        /// <returns>El numero de transacción asignado a la nota de peso registrada.</returns>
        public int InsertarTransaccionInventarioDeCafeDeSocio(nota_de_peso NotaDePeso, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe_por_socio asocInventory = this.GetReporteTotalInventarioDeCafeDeSocio(NotaDePeso.SOCIOS_ID, NotaDePeso.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario_socio = asocInventory == null ? 0 : asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario_socio = asocInventory == null ? 0 : asocInventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafeDeSocio = new inventario_cafe_de_socio();

                inventarioDeCafeDeSocio.SOCIOS_ID = NotaDePeso.SOCIOS_ID;
                inventarioDeCafeDeSocio.CLASIFICACIONES_CAFE_ID = NotaDePeso.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafeDeSocio.DOCUMENTO_ID = NotaDePeso.NOTAS_ID;
                inventarioDeCafeDeSocio.DOCUMENTO_TIPO = "ENTRADA";

                inventarioDeCafeDeSocio.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario_socio + NotaDePeso.NOTAS_PESO_TOTAL_RECIBIDO;
                inventarioDeCafeDeSocio.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario_socio;

                inventarioDeCafeDeSocio.CREADO_POR = NotaDePeso.CREADO_POR;
                inventarioDeCafeDeSocio.FECHA_CREACION = Convert.ToDateTime(NotaDePeso.FECHA_MODIFICACION);

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafeDeSocio);

                db.SaveChanges();

                return inventarioDeCafeDeSocio.TRANSACCION_NUMERO;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar transaccion de inventario de cafe de socio. Nota de Peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Insertar transacción de la hoja de liquidación en la tabla de inventario de café de socios como salida (Liquidación).
        /// </summary>
        /// <param name="HojaDeLiquidacion"></param>
        /// <param name="db"></param>
        public void InsertarTransaccionInventarioDeCafeDeSocio(liquidacion HojaDeLiquidacion, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe_por_socio asocInventory = this.GetReporteTotalInventarioDeCafeDeSocio(HojaDeLiquidacion.SOCIOS_ID, HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario_socio = asocInventory == null ? 0 : asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario_socio = asocInventory == null ? 0 : asocInventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafeDeSocio = new inventario_cafe_de_socio();

                inventarioDeCafeDeSocio.SOCIOS_ID = HojaDeLiquidacion.SOCIOS_ID;
                inventarioDeCafeDeSocio.CLASIFICACIONES_CAFE_ID = HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafeDeSocio.DOCUMENTO_ID = HojaDeLiquidacion.LIQUIDACIONES_ID;
                inventarioDeCafeDeSocio.DOCUMENTO_TIPO = "SALIDA";

                inventarioDeCafeDeSocio.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario_socio - HojaDeLiquidacion.LIQUIDACIONES_TOTAL_LIBRAS;
                inventarioDeCafeDeSocio.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario_socio + HojaDeLiquidacion.LIQUIDACIONES_VALOR_TOTAL;

                inventarioDeCafeDeSocio.CREADO_POR = HojaDeLiquidacion.CREADO_POR;
                inventarioDeCafeDeSocio.FECHA_CREACION = HojaDeLiquidacion.FECHA_CREACION;

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafeDeSocio);

                db.SaveChanges();

                this.InsertarTransaccionInventarioDeCafe(HojaDeLiquidacion, db);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar transaccion de inventario de cafe de socio. Hoja de Liquidacion.", ex);
                throw;
            }
        }

        /// <summary>
        /// Insertar transacción de la nota de peso en la tabla de inventario de café de socios como ajuste (Ajuste de Inventario).
        /// </summary>
        /// <param name="AjusteDeInventarioDeCafe"></param>
        /// <param name="db"></param>
        public void InsertarTransaccionInventarioDeCafeDeSocio(ajuste_inventario_cafe_x_socio AjusteDeInventarioDeCafe, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe_por_socio asocInventory = this.GetReporteTotalInventarioDeCafeDeSocio(AjusteDeInventarioDeCafe.SOCIOS_ID, AjusteDeInventarioDeCafe.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario_socio = asocInventory == null ? 0 : asocInventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario_socio = asocInventory == null ? 0 : asocInventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe_de_socio inventarioDeCafeDeSocio = new inventario_cafe_de_socio();

                inventarioDeCafeDeSocio.SOCIOS_ID = AjusteDeInventarioDeCafe.SOCIOS_ID;
                inventarioDeCafeDeSocio.CLASIFICACIONES_CAFE_ID = AjusteDeInventarioDeCafe.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafeDeSocio.DOCUMENTO_ID = AjusteDeInventarioDeCafe.AJUSTES_INV_CAFE_ID;
                inventarioDeCafeDeSocio.DOCUMENTO_TIPO = "AJUSTE";

                inventarioDeCafeDeSocio.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario_socio - AjusteDeInventarioDeCafe.AJUSTES_INV_CAFE_CANTIDAD_LIBRAS;
                inventarioDeCafeDeSocio.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario_socio + AjusteDeInventarioDeCafe.AJUSTES_INV_CAFE_SALDO_TOTAL;

                inventarioDeCafeDeSocio.CREADO_POR = AjusteDeInventarioDeCafe.CREADO_POR;
                inventarioDeCafeDeSocio.FECHA_CREACION = AjusteDeInventarioDeCafe.FECHA_CREACION;

                db.inventario_cafe_de_socio.AddObject(inventarioDeCafeDeSocio);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar transaccion de inventario de cafe de socio. Ajuste de Inventario de Cafe.", ex);
                throw;
            }
        }


        /*                                      Inventario de Cooperativa                              */

        /// <summary>
        /// Insertar transacción de la hoja de liquidación en la tabla de inventario de café de la cooperativa como entrada (Compra).
        /// </summary>
        /// <param name="HojaDeLiquidacion"></param>
        /// <param name="db"></param>
        public void InsertarTransaccionInventarioDeCafe(liquidacion HojaDeLiquidacion, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe inventory = this.GetReporteTotalInventarioDeCafe(HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario = inventory == null ? 0 : inventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario = inventory == null ? 0 : inventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe inventarioDeCafe = new inventario_cafe();

                inventarioDeCafe.CLASIFICACIONES_CAFE_ID = HojaDeLiquidacion.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_ID = HojaDeLiquidacion.LIQUIDACIONES_ID;
                inventarioDeCafe.DOCUMENTO_TIPO = "ENTRADA";

                inventarioDeCafe.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario + HojaDeLiquidacion.LIQUIDACIONES_TOTAL_LIBRAS;
                inventarioDeCafe.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario;

                inventarioDeCafe.CREADO_POR = HojaDeLiquidacion.CREADO_POR;
                inventarioDeCafe.FECHA_CREACION = HojaDeLiquidacion.FECHA_CREACION;

                db.inventario_cafe.AddObject(inventarioDeCafe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar transaccion de inventario de cafe. Hoja de Liquidacion.", ex);
                throw;
            }
        }

        /// <summary>
        /// Insertar transacción de la venta de inventario de café en la tabla de inventario de café de la cooperativa como salida (Venta).
        /// </summary>
        /// <param name="VentaDeInventario"></param>
        /// <param name="db"></param>
        public void InsertarTransaccionInventarioDeCafe(venta_inventario_cafe VentaDeInventario, colinasEntities db)
        {
            try
            {
                reporte_total_inventario_de_cafe inventory = this.GetReporteTotalInventarioDeCafe(VentaDeInventario.CLASIFICACIONES_CAFE_ID);

                decimal cantidad_en_inventario = inventory == null ? 0 : inventory.INVENTARIO_ENTRADAS_CANTIDAD;
                decimal salidas_de_inventario = inventory == null ? 0 : inventory.INVENTARIO_SALIDAS_SALDO;

                inventario_cafe inventarioDeCafe = new inventario_cafe();

                inventarioDeCafe.CLASIFICACIONES_CAFE_ID = VentaDeInventario.CLASIFICACIONES_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_ID = VentaDeInventario.VENTAS_INV_CAFE_ID;
                inventarioDeCafe.DOCUMENTO_TIPO = "SALIDA";

                inventarioDeCafe.INVENTARIO_ENTRADAS_CANTIDAD = cantidad_en_inventario - VentaDeInventario.VENTAS_INV_CAFE_CANTIDAD_LIBRAS;
                inventarioDeCafe.INVENTARIO_SALIDAS_SALDO = salidas_de_inventario + VentaDeInventario.VENTAS_INV_CAFE_SALDO_TOTAL;

                inventarioDeCafe.CREADO_POR = VentaDeInventario.CREADO_POR;
                inventarioDeCafe.FECHA_CREACION = VentaDeInventario.FECHA_CREACION;

                db.inventario_cafe.AddObject(inventarioDeCafe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar transaccion de inventario de cafe. Venta de Inventario de Cafe.", ex);
                throw;
            }
        }

        #endregion
    }
}
