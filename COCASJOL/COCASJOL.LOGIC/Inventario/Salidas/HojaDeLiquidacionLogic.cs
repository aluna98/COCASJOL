using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC.Aportaciones;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Salidas
{
    /// <summary>
    /// Clase con logica de Hoja de Liquidación
    /// </summary>
    public class HojaDeLiquidacionLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(HojaDeLiquidacionLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public HojaDeLiquidacionLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todas las hojas de liquidación.
        /// </summary>
        /// <returns>Lista de hojas de liquidación.</returns>
        public List<liquidacion> GetHojasDeLiquidacion()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.liquidaciones.MergeOption = MergeOption.NoTracking;

                    var query = from l in db.liquidaciones.Include("socios").Include("clasificaciones_cafe")
                                //where l.socios.SOCIOS_ESTATUS >= 1
                                select l;

                    return query.OrderBy(h => h.SOCIOS_ID).ToList<liquidacion>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener hojas de liquidacion.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las hojas de liquidación.
        /// </summary>
        /// <param name="LIQUIDACIONES_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="SOCIOS_PRIMER_NOMBRE"></param>
        /// <param name="SOCIOS_SEGUNDO_NOMBRE"></param>
        /// <param name="SOCIOS_PRIMER_APELLIDO"></param>
        /// <param name="SOCIOS_SEGUNDO_APELLIDO"></param>
        /// <param name="LIQUIDACIONES_FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="LIQUIDACIONES_TOTAL_LIBRAS"></param>
        /// <param name="LIQUIDACIONES_PRECIO_LIBRAS"></param>
        /// <param name="LIQUIDACIONES_VALOR_TOTAL"></param>
        /// <param name="LIQUIDACIONES_D_CUOTA_INGRESO"></param>
        /// <param name="LIQUIDACIONES_D_GASTOS_ADMIN"></param>
        /// <param name="LIQUIDACIONES_D_APORTACION_ORDINARIO"></param>
        /// <param name="LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA"></param>
        /// <param name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION"></param>
        /// <param name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD"></param>
        /// <param name="LIQUIDACIONES_D_INTERESES_S_APORTACIONES"></param>
        /// <param name="LIQUIDACIONES_D_EXCEDENTE_PERIODO"></param>
        /// <param name="LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO"></param>
        /// <param name="LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO"></param>
        /// <param name="LIQUIDACIONES_D_PRESTAMO_PRENDARIO"></param>
        /// <param name="LIQUIDACIONES_D_CUENTAS_X_COBRAR"></param>
        /// <param name="LIQUIDACIONES_D_INTERESES_X_COBRAR"></param>
        /// <param name="LIQUIDACIONES_D_OTRAS_DEDUCCIONES"></param>
        /// <param name="LIQUIDACIONES_D_TOTAL_DEDUCCIONES"></param>
        /// <param name="LIQUIDACIONES_D_AF_SOCIO"></param>
        /// <param name="LIQUIDACIONES_D_TOTAL"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <param name="SOCIOS_APORTACION_EXTRAORD_COOP_COUNT"></param>
        /// <returns>Lista de hojas de liquidación.</returns>
        public List<liquidacion> GetHojasDeLiquidacion
            (    int LIQUIDACIONES_ID,
              string SOCIOS_ID,
              string SOCIOS_PRIMER_NOMBRE,
              string SOCIOS_SEGUNDO_NOMBRE,
              string SOCIOS_PRIMER_APELLIDO,
              string SOCIOS_SEGUNDO_APELLIDO,
            DateTime LIQUIDACIONES_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
                 int CLASIFICACIONES_CAFE_ID,
              string CLASIFICACIONES_CAFE_NOMBRE,
             decimal LIQUIDACIONES_TOTAL_LIBRAS,
             decimal LIQUIDACIONES_PRECIO_LIBRAS,
             decimal LIQUIDACIONES_VALOR_TOTAL,
             decimal LIQUIDACIONES_D_CUOTA_INGRESO,
             decimal LIQUIDACIONES_D_GASTOS_ADMIN,
             decimal LIQUIDACIONES_D_APORTACION_ORDINARIO,
             decimal LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA,
                 int LIQUIDACIONES_D_CAPITALIZACION_RETENCION,
             decimal LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD,
             decimal LIQUIDACIONES_D_INTERESES_S_APORTACIONES,
             decimal LIQUIDACIONES_D_EXCEDENTE_PERIODO,
             decimal LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO,
             decimal LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO,
             decimal LIQUIDACIONES_D_PRESTAMO_PRENDARIO,
             decimal LIQUIDACIONES_D_CUENTAS_X_COBRAR,
             decimal LIQUIDACIONES_D_INTERESES_X_COBRAR,
             decimal LIQUIDACIONES_D_OTRAS_DEDUCCIONES,
             decimal LIQUIDACIONES_D_TOTAL_DEDUCCIONES,
             decimal LIQUIDACIONES_D_AF_SOCIO,
             decimal LIQUIDACIONES_D_TOTAL,
              string CREADO_POR,
            DateTime FECHA_CREACION,
              string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION,
                 int SOCIOS_APORTACION_EXTRAORD_COOP_COUNT)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.liquidaciones.MergeOption = MergeOption.NoTracking;

                    var prequery = from l in db.liquidaciones.Include("socios").Include("clasificaciones_cafe")
                                   //where l.socios.SOCIOS_ESTATUS >= 1
                                   select l;

                    var query = from hojaliq in prequery
                                where
                                (LIQUIDACIONES_ID.Equals(0)                              ? true : hojaliq.LIQUIDACIONES_ID.Equals(LIQUIDACIONES_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_ID)                         ? true : hojaliq.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_PRIMER_NOMBRE)              ? true : (hojaliq.socios.SOCIOS_PRIMER_NOMBRE + hojaliq.socios.SOCIOS_SEGUNDO_NOMBRE + hojaliq.socios.SOCIOS_PRIMER_APELLIDO + hojaliq.socios.SOCIOS_SEGUNDO_APELLIDO).Contains(SOCIOS_PRIMER_NOMBRE)) &&
                                (default(DateTime) == FECHA_DESDE                        ? true : hojaliq.LIQUIDACIONES_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA                        ? true : hojaliq.LIQUIDACIONES_FECHA <= FECHA_HASTA) &&
                                (CLASIFICACIONES_CAFE_ID.Equals(0)                       ? true : hojaliq.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (LIQUIDACIONES_TOTAL_LIBRAS.Equals(-1)                   ? true : hojaliq.LIQUIDACIONES_TOTAL_LIBRAS.Equals(LIQUIDACIONES_TOTAL_LIBRAS)) &&
                                (LIQUIDACIONES_PRECIO_LIBRAS.Equals(-1)                  ? true : hojaliq.LIQUIDACIONES_PRECIO_LIBRAS.Equals(LIQUIDACIONES_PRECIO_LIBRAS)) &&
                                (LIQUIDACIONES_VALOR_TOTAL.Equals(-1)                    ? true : hojaliq.LIQUIDACIONES_VALOR_TOTAL.Equals(LIQUIDACIONES_VALOR_TOTAL)) &&

                                (LIQUIDACIONES_D_CUOTA_INGRESO == -1                     ? true : hojaliq.LIQUIDACIONES_D_CUOTA_INGRESO == LIQUIDACIONES_D_CUOTA_INGRESO) &&
                                (LIQUIDACIONES_D_GASTOS_ADMIN == -1                      ? true : hojaliq.LIQUIDACIONES_D_GASTOS_ADMIN == LIQUIDACIONES_D_GASTOS_ADMIN) &&
                                (LIQUIDACIONES_D_APORTACION_ORDINARIO == -1              ? true : hojaliq.LIQUIDACIONES_D_APORTACION_ORDINARIO == LIQUIDACIONES_D_APORTACION_ORDINARIO) &&
                                (LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA == -1         ? true : hojaliq.LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA == LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA) &&

                                (LIQUIDACIONES_D_CAPITALIZACION_RETENCION == -1          ? true : hojaliq.LIQUIDACIONES_D_CAPITALIZACION_RETENCION == LIQUIDACIONES_D_CAPITALIZACION_RETENCION) &&
                                (LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD == -1 ? true : hojaliq.LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD == LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD) &&

                                (LIQUIDACIONES_D_INTERESES_S_APORTACIONES == -1          ? true : hojaliq.LIQUIDACIONES_D_INTERESES_S_APORTACIONES == LIQUIDACIONES_D_INTERESES_S_APORTACIONES) &&
                                (LIQUIDACIONES_D_EXCEDENTE_PERIODO == -1                 ? true : hojaliq.LIQUIDACIONES_D_EXCEDENTE_PERIODO == LIQUIDACIONES_D_EXCEDENTE_PERIODO) &&
                                (LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO == -1              ? true : hojaliq.LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO == LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO) &&
                                                                                         
                                (LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO == -1               ? true : hojaliq.LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO == LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO) &&
                                (LIQUIDACIONES_D_PRESTAMO_PRENDARIO == -1                ? true : hojaliq.LIQUIDACIONES_D_PRESTAMO_PRENDARIO == LIQUIDACIONES_D_PRESTAMO_PRENDARIO) &&
                                (LIQUIDACIONES_D_CUENTAS_X_COBRAR == -1                  ? true : hojaliq.LIQUIDACIONES_D_CUENTAS_X_COBRAR == LIQUIDACIONES_D_CUENTAS_X_COBRAR) &&
                                (LIQUIDACIONES_D_INTERESES_X_COBRAR == -1                ? true : hojaliq.LIQUIDACIONES_D_INTERESES_X_COBRAR == LIQUIDACIONES_D_INTERESES_X_COBRAR) &&
                                (LIQUIDACIONES_D_OTRAS_DEDUCCIONES == -1                 ? true : hojaliq.LIQUIDACIONES_D_OTRAS_DEDUCCIONES == LIQUIDACIONES_D_OTRAS_DEDUCCIONES) &&
                                (LIQUIDACIONES_D_TOTAL_DEDUCCIONES == -1                 ? true : hojaliq.LIQUIDACIONES_D_TOTAL_DEDUCCIONES == LIQUIDACIONES_D_TOTAL_DEDUCCIONES) &&
                                (LIQUIDACIONES_D_AF_SOCIO == -1                          ? true : hojaliq.LIQUIDACIONES_D_AF_SOCIO == LIQUIDACIONES_D_AF_SOCIO) &&
                                (LIQUIDACIONES_D_TOTAL == -1                             ? true : hojaliq.LIQUIDACIONES_D_TOTAL == LIQUIDACIONES_D_TOTAL) &&
                                (string.IsNullOrEmpty(CREADO_POR)                        ? true : hojaliq.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION                     ? true : hojaliq.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR)                    ? true : hojaliq.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION                 ? true : hojaliq.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select hojaliq;

                    return query.OrderBy(h => h.SOCIOS_ID).OrderByDescending(h => h.FECHA_MODIFICACION).OrderByDescending(h => h.LIQUIDACIONES_FECHA).ToList<liquidacion>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener hojas de liquidacion.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /*
         *                  -----Flujo-----
         * --------Guardar Hoja de Liquidación--------
         *      guardar datos de hoja de liquidación
         * 
         * --------Cambiar Estado Actual de Socio--------
         * --------Cambiar Estado Aportación ordinaria anual de Socio--------
         * --------Cambiar Estado Aportación extraordinaria anual de Socio--------
         * --------Calcular de deducciones--------
         * --------Modificar Inventario de Café--------
         * --------Modificar Aportaciones de Socio--------
         */
        /// <summary>
        /// Inserta la hoja de liquidación.
        /// </summary>
        /// <param name="LIQUIDACIONES_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="SOCIOS_PRIMER_NOMBRE"></param>
        /// <param name="SOCIOS_SEGUNDO_NOMBRE"></param>
        /// <param name="SOCIOS_PRIMER_APELLIDO"></param>
        /// <param name="SOCIOS_SEGUNDO_APELLIDO"></param>
        /// <param name="LIQUIDACIONES_FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="LIQUIDACIONES_TOTAL_LIBRAS"></param>
        /// <param name="LIQUIDACIONES_PRECIO_LIBRAS"></param>
        /// <param name="LIQUIDACIONES_VALOR_TOTAL"></param>
        /// <param name="LIQUIDACIONES_D_CUOTA_INGRESO"></param>
        /// <param name="LIQUIDACIONES_D_GASTOS_ADMIN"></param>
        /// <param name="LIQUIDACIONES_D_APORTACION_ORDINARIO"></param>
        /// <param name="LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA"></param>
        /// <param name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION"></param>
        /// <param name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD"></param>
        /// <param name="LIQUIDACIONES_D_INTERESES_S_APORTACIONES"></param>
        /// <param name="LIQUIDACIONES_D_EXCEDENTE_PERIODO"></param>
        /// <param name="LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO"></param>
        /// <param name="LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO"></param>
        /// <param name="LIQUIDACIONES_D_PRESTAMO_PRENDARIO"></param>
        /// <param name="LIQUIDACIONES_D_CUENTAS_X_COBRAR"></param>
        /// <param name="LIQUIDACIONES_D_INTERESES_X_COBRAR"></param>
        /// <param name="LIQUIDACIONES_D_OTRAS_DEDUCCIONES"></param>
        /// <param name="LIQUIDACIONES_D_TOTAL_DEDUCCIONES"></param>
        /// <param name="LIQUIDACIONES_D_AF_SOCIO"></param>
        /// <param name="LIQUIDACIONES_D_TOTAL"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <param name="SOCIOS_APORTACION_EXTRAORD_COOP_COUNT"></param>
        public void InsertarHojaDeLiquidacion
            (    int LIQUIDACIONES_ID,
              string SOCIOS_ID,
              string SOCIOS_PRIMER_NOMBRE,
              string SOCIOS_SEGUNDO_NOMBRE,
              string SOCIOS_PRIMER_APELLIDO,
              string SOCIOS_SEGUNDO_APELLIDO,
            DateTime LIQUIDACIONES_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
                 int CLASIFICACIONES_CAFE_ID,
              string CLASIFICACIONES_CAFE_NOMBRE,
             decimal LIQUIDACIONES_TOTAL_LIBRAS,
             decimal LIQUIDACIONES_PRECIO_LIBRAS,
             decimal LIQUIDACIONES_VALOR_TOTAL,
             decimal LIQUIDACIONES_D_CUOTA_INGRESO,
             decimal LIQUIDACIONES_D_GASTOS_ADMIN,
             decimal LIQUIDACIONES_D_APORTACION_ORDINARIO,
             decimal LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA,
                 int LIQUIDACIONES_D_CAPITALIZACION_RETENCION,
             decimal LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD,
             decimal LIQUIDACIONES_D_INTERESES_S_APORTACIONES,
             decimal LIQUIDACIONES_D_EXCEDENTE_PERIODO,
             decimal LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO,
             decimal LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO,
             decimal LIQUIDACIONES_D_PRESTAMO_PRENDARIO,
             decimal LIQUIDACIONES_D_CUENTAS_X_COBRAR,
             decimal LIQUIDACIONES_D_INTERESES_X_COBRAR,
             decimal LIQUIDACIONES_D_OTRAS_DEDUCCIONES,
             decimal LIQUIDACIONES_D_TOTAL_DEDUCCIONES,
             decimal LIQUIDACIONES_D_AF_SOCIO,
             decimal LIQUIDACIONES_D_TOTAL,
              string CREADO_POR,
            DateTime FECHA_CREACION,
              string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION,
                 int SOCIOS_APORTACION_EXTRAORD_COOP_COUNT)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        /* --------Guardar Hoja de Liquidación-------- */
                        // guardar datos de hoja de liquidación
                        liquidacion hojaliquidacion = new liquidacion();

                        hojaliquidacion.SOCIOS_ID = SOCIOS_ID;
                        hojaliquidacion.LIQUIDACIONES_FECHA = LIQUIDACIONES_FECHA;
                        hojaliquidacion.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        hojaliquidacion.LIQUIDACIONES_TOTAL_LIBRAS = LIQUIDACIONES_TOTAL_LIBRAS;
                        hojaliquidacion.LIQUIDACIONES_PRECIO_LIBRAS = LIQUIDACIONES_PRECIO_LIBRAS;
                        hojaliquidacion.LIQUIDACIONES_VALOR_TOTAL = LIQUIDACIONES_VALOR_TOTAL;
                        hojaliquidacion.LIQUIDACIONES_D_CUOTA_INGRESO = LIQUIDACIONES_D_CUOTA_INGRESO;
                        hojaliquidacion.LIQUIDACIONES_D_GASTOS_ADMIN = LIQUIDACIONES_D_GASTOS_ADMIN;
                        hojaliquidacion.LIQUIDACIONES_D_APORTACION_ORDINARIO = LIQUIDACIONES_D_APORTACION_ORDINARIO;
                        hojaliquidacion.LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA = LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA;
                        hojaliquidacion.LIQUIDACIONES_D_CAPITALIZACION_RETENCION = LIQUIDACIONES_D_CAPITALIZACION_RETENCION;
                        hojaliquidacion.LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD = LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD;
                        hojaliquidacion.LIQUIDACIONES_D_INTERESES_S_APORTACIONES = LIQUIDACIONES_D_INTERESES_S_APORTACIONES;
                        hojaliquidacion.LIQUIDACIONES_D_EXCEDENTE_PERIODO = LIQUIDACIONES_D_EXCEDENTE_PERIODO;
                        hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO = LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO;
                        hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO = LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO;
                        hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_PRENDARIO = LIQUIDACIONES_D_PRESTAMO_PRENDARIO;
                        hojaliquidacion.LIQUIDACIONES_D_CUENTAS_X_COBRAR = LIQUIDACIONES_D_CUENTAS_X_COBRAR;
                        hojaliquidacion.LIQUIDACIONES_D_INTERESES_X_COBRAR = LIQUIDACIONES_D_INTERESES_X_COBRAR;
                        hojaliquidacion.LIQUIDACIONES_D_OTRAS_DEDUCCIONES = LIQUIDACIONES_D_OTRAS_DEDUCCIONES;
                        hojaliquidacion.CREADO_POR = CREADO_POR;
                        hojaliquidacion.FECHA_CREACION = DateTime.Today;
                        hojaliquidacion.MODIFICADO_POR = CREADO_POR;
                        hojaliquidacion.FECHA_MODIFICACION = hojaliquidacion.FECHA_CREACION;
                        

                        /* --------Cambiar Estado Actual de Socio-------- */
                        if (LIQUIDACIONES_D_CUOTA_INGRESO != 0)
                            Socios.SociosLogic.PagarGastoDeIngreso(SOCIOS_ID, db);

                        /* --------Cambiar Estado Aportación ordinaria anual de Socio-------- */
                        if (LIQUIDACIONES_D_APORTACION_ORDINARIO != 0)
                            Socios.SociosLogic.PagarAportacionOrdinaria(SOCIOS_ID, db);

                        /* --------Cambiar Estado Aportación extraordinaria anual de Socio-------- */
                        bool aumentar_aportaciones = false;
                        if (LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA != 0)
                            aumentar_aportaciones = Socios.SociosLogic.PagarAportacionExtraordinaria(SOCIOS_ID, SOCIOS_APORTACION_EXTRAORD_COOP_COUNT, db);

                        hojaliquidacion.LIQUIDACIONES_D_APORTACION_EXTRAORD_COOP = aumentar_aportaciones;

                        /* --------Cambiar Estado Aportación intereses sobre aportaciones anual de Socio-------- */
                        if (LIQUIDACIONES_D_INTERESES_S_APORTACIONES != 0)
                            Socios.SociosLogic.PagarAportacionInteresesSobreAportaciones(SOCIOS_ID, db);


                        // Total Deducciones: Sum(toda deduccion)
                        // A/F Socio (Afavor): (Total Valor Producto) - (Total Deducciones)
                        // ---> Total Deducciones >= 0 < Total Valor Producto
                        // Total Valor Deducciones = (A/F Socio) + (Total Deducciones)

                        hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES =
                            LIQUIDACIONES_D_CUOTA_INGRESO +
                            LIQUIDACIONES_D_GASTOS_ADMIN +
                            LIQUIDACIONES_D_APORTACION_ORDINARIO +
                            LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA +
                            LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD +
                            LIQUIDACIONES_D_INTERESES_S_APORTACIONES +
                            LIQUIDACIONES_D_EXCEDENTE_PERIODO +
                            LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO +
                            LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO +
                            LIQUIDACIONES_D_PRESTAMO_PRENDARIO +
                            LIQUIDACIONES_D_CUENTAS_X_COBRAR +
                            LIQUIDACIONES_D_INTERESES_X_COBRAR +
                            LIQUIDACIONES_D_OTRAS_DEDUCCIONES;

                        hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES = hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES > LIQUIDACIONES_VALOR_TOTAL ? LIQUIDACIONES_VALOR_TOTAL : hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES;
                        hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES = hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES < 0 ? 0 : hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES;

                        hojaliquidacion.LIQUIDACIONES_D_AF_SOCIO = LIQUIDACIONES_VALOR_TOTAL - hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES;

                        hojaliquidacion.LIQUIDACIONES_D_TOTAL = hojaliquidacion.LIQUIDACIONES_D_AF_SOCIO + hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES;


                        db.liquidaciones.AddObject(hojaliquidacion);

                        db.SaveChanges();

                        /* --------Modificar Inventario de Café Actual-------- */
                        InventarioDeCafeLogic inventariodecafelogic = new InventarioDeCafeLogic();
                        inventariodecafelogic.InsertarTransaccionInventarioDeCafeDeSocio(hojaliquidacion, db);

                        /* --------Modificar Aportaciones de Socio-------- */
                        AportacionLogic aportacionesDeSocioLogic = new AportacionLogic();
                        aportacionesDeSocioLogic.InsertarTransaccionAportacionesDeSocio(hojaliquidacion, db);

                        db.SaveChanges();

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar hoja de liquidacion.", ex);
                throw;
            }
        }

        #endregion
    }
}
