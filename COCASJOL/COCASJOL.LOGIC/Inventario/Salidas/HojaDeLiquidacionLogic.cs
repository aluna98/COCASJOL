using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Inventario.Salidas
{
    public class HojaDeLiquidacionLogic
    {
        public HojaDeLiquidacionLogic() { }

        #region Select

        public List<liquidacion> GetHojasDeLiquidacion()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.liquidaciones.MergeOption = MergeOption.NoTracking;

                    return db.liquidaciones.Include("socios").Include("clasificaciones_cafe").ToList<liquidacion>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<liquidacion> GetHojasDeLiquidacion
            (int LIQUIDACIONES_ID,
            string SOCIOS_ID,
            DateTime LIQUIDACIONES_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            int CLASIFICACIONES_CAFE_ID,
            decimal LIQUIDACIONES_TOTAL_LIBRAS,
            decimal LIQUIDACIONES_PRECIO_LIBRAS,
            decimal LIQUIDACIONES_VALOR_TOTAL,
            decimal LIQUIDACIONES_D_CUOTA_INGRESO,
            decimal LIQUIDACIONES_D_GASTOS_ADMIN,
            decimal LIQUIDACIONES_D_APORTACION_ORDINARIO,
            decimal LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA,
            decimal LIQUIDACIONES_D_CUOTA_ADMIN,
            int LIQUIDACIONES_D_CAPITALIZACION_RETENCION,
            decimal LIQUIDACIONES_D_SERVICIO_SECADO_CAFE,
            decimal LIQUIDACIONES_D_INTERESES_S_APORTACIONES,
            decimal LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE,
            decimal LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO,
            decimal LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO,
            decimal LIQUIDACIONES_D_PRESTAMO_PRENDARIO,
            decimal LIQUIDACIONES_D_CUENTAS_X_COBRAR,
            decimal LIQUIDACIONES_D_INTERESES_X_COBRAR,
            decimal LIQUIDACIONES_D_RETENCION_X_TORREFACCION,
            decimal LIQUIDACIONES_D_OTRAS_DEDUCCIONES,
            decimal LIQUIDACIONES_D_TOTAL_DEDUCCIONES,
            decimal LIQUIDACIONES_D_AF_SOCIO,
            decimal LIQUIDACIONES_D_TOTAL,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.liquidaciones.MergeOption = MergeOption.NoTracking;

                    var query = from hojaliq in db.liquidaciones.Include("socios").Include("clasificaciones_cafe")
                                where
                                (LIQUIDACIONES_ID.Equals(0) ? true : hojaliq.LIQUIDACIONES_ID.Equals(LIQUIDACIONES_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : hojaliq.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (default(DateTime) == FECHA_DESDE ? true : hojaliq.LIQUIDACIONES_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA ? true : hojaliq.LIQUIDACIONES_FECHA <= FECHA_HASTA) &&
                                (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : hojaliq.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (LIQUIDACIONES_TOTAL_LIBRAS.Equals(-1) ? true : hojaliq.LIQUIDACIONES_TOTAL_LIBRAS.Equals(LIQUIDACIONES_TOTAL_LIBRAS)) &&
                                (LIQUIDACIONES_PRECIO_LIBRAS.Equals(-1) ? true : hojaliq.LIQUIDACIONES_PRECIO_LIBRAS.Equals(LIQUIDACIONES_PRECIO_LIBRAS)) &&
                                (LIQUIDACIONES_VALOR_TOTAL.Equals(-1) ? true : hojaliq.LIQUIDACIONES_VALOR_TOTAL.Equals(LIQUIDACIONES_VALOR_TOTAL)) &&
                                (LIQUIDACIONES_D_CUOTA_INGRESO.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_CUOTA_INGRESO.Equals(LIQUIDACIONES_D_CUOTA_INGRESO)) &&
                                (LIQUIDACIONES_D_GASTOS_ADMIN.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_GASTOS_ADMIN.Equals(LIQUIDACIONES_D_GASTOS_ADMIN)) &&
                                (LIQUIDACIONES_D_APORTACION_ORDINARIO.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_APORTACION_ORDINARIO.Equals(LIQUIDACIONES_D_APORTACION_ORDINARIO)) &&
                                (LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA.Equals(LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA)) &&
                                (LIQUIDACIONES_D_CUOTA_ADMIN.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_CUOTA_ADMIN.Equals(LIQUIDACIONES_D_CUOTA_ADMIN)) &&
                                (LIQUIDACIONES_D_CAPITALIZACION_RETENCION.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_CAPITALIZACION_RETENCION.Equals(LIQUIDACIONES_D_CAPITALIZACION_RETENCION)) &&
                                (LIQUIDACIONES_D_SERVICIO_SECADO_CAFE.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_SERVICIO_SECADO_CAFE.Equals(LIQUIDACIONES_D_SERVICIO_SECADO_CAFE)) &&
                                (LIQUIDACIONES_D_INTERESES_S_APORTACIONES.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_INTERESES_S_APORTACIONES.Equals(LIQUIDACIONES_D_INTERESES_S_APORTACIONES)) &&
                                (LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE.Equals(LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE)) &&
                                (LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO.Equals(LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO)) &&
                                (LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO.Equals(LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO)) &&
                                (LIQUIDACIONES_D_PRESTAMO_PRENDARIO.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_PRESTAMO_PRENDARIO.Equals(LIQUIDACIONES_D_PRESTAMO_PRENDARIO)) &&
                                (LIQUIDACIONES_D_CUENTAS_X_COBRAR.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_CUENTAS_X_COBRAR.Equals(LIQUIDACIONES_D_CUENTAS_X_COBRAR)) &&
                                (LIQUIDACIONES_D_INTERESES_X_COBRAR.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_INTERESES_X_COBRAR.Equals(LIQUIDACIONES_D_INTERESES_X_COBRAR)) &&
                                (LIQUIDACIONES_D_RETENCION_X_TORREFACCION.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_RETENCION_X_TORREFACCION.Equals(LIQUIDACIONES_D_RETENCION_X_TORREFACCION)) &&
                                (LIQUIDACIONES_D_OTRAS_DEDUCCIONES.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_OTRAS_DEDUCCIONES.Equals(LIQUIDACIONES_D_OTRAS_DEDUCCIONES)) &&
                                (LIQUIDACIONES_D_TOTAL_DEDUCCIONES.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_TOTAL_DEDUCCIONES.Equals(LIQUIDACIONES_D_TOTAL_DEDUCCIONES)) &&
                                (LIQUIDACIONES_D_AF_SOCIO.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_AF_SOCIO.Equals(LIQUIDACIONES_D_AF_SOCIO)) &&
                                (LIQUIDACIONES_D_TOTAL.Equals(-1) ? true : hojaliq.LIQUIDACIONES_D_TOTAL.Equals(LIQUIDACIONES_D_TOTAL)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : hojaliq.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : hojaliq.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : hojaliq.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : hojaliq.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select hojaliq;

                    return query.ToList<liquidacion>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarHojaDeLiquidacion
            (string SOCIOS_ID,
            DateTime LIQUIDACIONES_FECHA,
            int CLASIFICACIONES_CAFE_ID,
            decimal LIQUIDACIONES_TOTAL_LIBRAS,
            decimal LIQUIDACIONES_PRECIO_LIBRAS,
            decimal LIQUIDACIONES_VALOR_TOTAL,
            decimal LIQUIDACIONES_D_CUOTA_INGRESO,
            decimal LIQUIDACIONES_D_GASTOS_ADMIN,
            decimal LIQUIDACIONES_D_APORTACION_ORDINARIO,
            decimal LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA,
            decimal LIQUIDACIONES_D_CUOTA_ADMIN,
            int LIQUIDACIONES_D_CAPITALIZACION_RETENCION,
            decimal LIQUIDACIONES_D_SERVICIO_SECADO_CAFE,
            decimal LIQUIDACIONES_D_INTERESES_S_APORTACIONES,
            decimal LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE,
            decimal LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO,
            decimal LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO,
            decimal LIQUIDACIONES_D_PRESTAMO_PRENDARIO,
            decimal LIQUIDACIONES_D_CUENTAS_X_COBRAR,
            decimal LIQUIDACIONES_D_INTERESES_X_COBRAR,
            decimal LIQUIDACIONES_D_RETENCION_X_TORREFACCION,
            decimal LIQUIDACIONES_D_OTRAS_DEDUCCIONES,
            decimal LIQUIDACIONES_D_TOTAL_DEDUCCIONES,
            decimal LIQUIDACIONES_D_AF_SOCIO,
            decimal LIQUIDACIONES_D_TOTAL,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
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
                    hojaliquidacion.LIQUIDACIONES_D_CUOTA_ADMIN = LIQUIDACIONES_D_CUOTA_ADMIN;
                    hojaliquidacion.LIQUIDACIONES_D_CAPITALIZACION_RETENCION = LIQUIDACIONES_D_CAPITALIZACION_RETENCION;
                    hojaliquidacion.LIQUIDACIONES_D_SERVICIO_SECADO_CAFE = LIQUIDACIONES_D_SERVICIO_SECADO_CAFE;
                    hojaliquidacion.LIQUIDACIONES_D_INTERESES_S_APORTACIONES = LIQUIDACIONES_D_INTERESES_S_APORTACIONES;
                    hojaliquidacion.LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE = LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE;
                    hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO = LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO;
                    hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO = LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO;
                    hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_PRENDARIO = LIQUIDACIONES_D_PRESTAMO_PRENDARIO;
                    hojaliquidacion.LIQUIDACIONES_D_CUENTAS_X_COBRAR = LIQUIDACIONES_D_CUENTAS_X_COBRAR;
                    hojaliquidacion.LIQUIDACIONES_D_INTERESES_X_COBRAR = LIQUIDACIONES_D_INTERESES_X_COBRAR;
                    hojaliquidacion.LIQUIDACIONES_D_RETENCION_X_TORREFACCION = LIQUIDACIONES_D_RETENCION_X_TORREFACCION;
                    hojaliquidacion.LIQUIDACIONES_D_OTRAS_DEDUCCIONES = LIQUIDACIONES_D_OTRAS_DEDUCCIONES;
                    hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES = LIQUIDACIONES_D_TOTAL_DEDUCCIONES;
                    hojaliquidacion.LIQUIDACIONES_D_AF_SOCIO = LIQUIDACIONES_D_AF_SOCIO;
                    hojaliquidacion.LIQUIDACIONES_D_TOTAL = LIQUIDACIONES_D_TOTAL;
                    hojaliquidacion.CREADO_POR = CREADO_POR;
                    hojaliquidacion.FECHA_CREACION = DateTime.Today;
                    hojaliquidacion.MODIFICADO_POR = CREADO_POR;
                    hojaliquidacion.FECHA_MODIFICACION = hojaliquidacion.FECHA_CREACION;

                    db.liquidaciones.AddObject(hojaliquidacion);
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

        public void ActualizarHojaDeLiquidacion
            (int LIQUIDACIONES_ID,
            string SOCIOS_ID,
            DateTime LIQUIDACIONES_FECHA,
            int CLASIFICACIONES_CAFE_ID,
            decimal LIQUIDACIONES_TOTAL_LIBRAS,
            decimal LIQUIDACIONES_PRECIO_LIBRAS,
            decimal LIQUIDACIONES_VALOR_TOTAL,
            decimal LIQUIDACIONES_D_CUOTA_INGRESO,
            decimal LIQUIDACIONES_D_GASTOS_ADMIN,
            decimal LIQUIDACIONES_D_APORTACION_ORDINARIO,
            decimal LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA,
            decimal LIQUIDACIONES_D_CUOTA_ADMIN,
            int LIQUIDACIONES_D_CAPITALIZACION_RETENCION,
            decimal LIQUIDACIONES_D_SERVICIO_SECADO_CAFE,
            decimal LIQUIDACIONES_D_INTERESES_S_APORTACIONES,
            decimal LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE,
            decimal LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO,
            decimal LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO,
            decimal LIQUIDACIONES_D_PRESTAMO_PRENDARIO,
            decimal LIQUIDACIONES_D_CUENTAS_X_COBRAR,
            decimal LIQUIDACIONES_D_INTERESES_X_COBRAR,
            decimal LIQUIDACIONES_D_RETENCION_X_TORREFACCION,
            decimal LIQUIDACIONES_D_OTRAS_DEDUCCIONES,
            decimal LIQUIDACIONES_D_TOTAL_DEDUCCIONES,
            decimal LIQUIDACIONES_D_AF_SOCIO,
            decimal LIQUIDACIONES_D_TOTAL,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.liquidaciones", "LIQUIDACIONES_ID", LIQUIDACIONES_ID);

                    var l = db.GetObjectByKey(k);

                    liquidacion hojaliquidacion = (liquidacion)l;

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
                    hojaliquidacion.LIQUIDACIONES_D_CUOTA_ADMIN = LIQUIDACIONES_D_CUOTA_ADMIN;
                    hojaliquidacion.LIQUIDACIONES_D_CAPITALIZACION_RETENCION = LIQUIDACIONES_D_CAPITALIZACION_RETENCION;
                    hojaliquidacion.LIQUIDACIONES_D_SERVICIO_SECADO_CAFE = LIQUIDACIONES_D_SERVICIO_SECADO_CAFE;
                    hojaliquidacion.LIQUIDACIONES_D_INTERESES_S_APORTACIONES = LIQUIDACIONES_D_INTERESES_S_APORTACIONES;
                    hojaliquidacion.LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE = LIQUIDACIONES_D_EXCEDENTE_X_RENDIMIENTO_CAFE;
                    hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO = LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO;
                    hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO = LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO;
                    hojaliquidacion.LIQUIDACIONES_D_PRESTAMO_PRENDARIO = LIQUIDACIONES_D_PRESTAMO_PRENDARIO;
                    hojaliquidacion.LIQUIDACIONES_D_CUENTAS_X_COBRAR = LIQUIDACIONES_D_CUENTAS_X_COBRAR;
                    hojaliquidacion.LIQUIDACIONES_D_INTERESES_X_COBRAR = LIQUIDACIONES_D_INTERESES_X_COBRAR;
                    hojaliquidacion.LIQUIDACIONES_D_RETENCION_X_TORREFACCION = LIQUIDACIONES_D_RETENCION_X_TORREFACCION;
                    hojaliquidacion.LIQUIDACIONES_D_OTRAS_DEDUCCIONES = LIQUIDACIONES_D_OTRAS_DEDUCCIONES;
                    hojaliquidacion.LIQUIDACIONES_D_TOTAL_DEDUCCIONES = LIQUIDACIONES_D_TOTAL_DEDUCCIONES;
                    hojaliquidacion.LIQUIDACIONES_D_AF_SOCIO = LIQUIDACIONES_D_AF_SOCIO;
                    hojaliquidacion.LIQUIDACIONES_D_TOTAL = LIQUIDACIONES_D_TOTAL;
                    hojaliquidacion.MODIFICADO_POR = CREADO_POR;
                    hojaliquidacion.FECHA_MODIFICACION = DateTime.Today;

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

        public void EliminarHojaDeLiquidacion(int LIQUIDACIONES_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.liquidaciones", "LIQUIDACIONES_ID", LIQUIDACIONES_ID);

                    var l = db.GetObjectByKey(k);

                    liquidacion hojaliquidacion = (liquidacion)l;

                    db.DeleteObject(hojaliquidacion);
                    db.SaveChanges();
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
