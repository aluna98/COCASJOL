using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.LOGIC.Inventario;

namespace COCASJOL.LOGIC.Aportaciones
{
    public class AportacionLogic
    {
        public AportacionLogic() { }

        #region Select

        public List<reporte_total_aportaciones_por_socio> GetAportaciones()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from a in db.reporte_total_aportaciones_por_socio
                                join s in db.socios on a.SOCIOS_ID equals s.SOCIOS_ID
                                where s.SOCIOS_ESTATUS >= 1
                                select a;

                    return query.OrderBy(a => a.SOCIOS_ID).ToList<reporte_total_aportaciones_por_socio>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<reporte_total_aportaciones_por_socio> GetAportaciones
            ( string SOCIOS_ID,
              string SOCIOS_NOMBRE_COMPLETO,
             decimal APORTACIONES_ORDINARIA_SALDO,
             decimal APORTACIONES_EXTRAORDINARIA_SALDO,
             decimal APORTACIONES_CAPITALIZACION_RETENCION_SALDO,
             decimal APORTACIONES_INTERESES_S_APORTACION_SALDO,
             decimal APORTACIONES_EXCEDENTE_PERIODO_SALDO,
             decimal APORTACIONES_SALDO_TOTAL,
              string CREADO_POR,
            DateTime FECHA_CREACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from ap in db.reporte_total_aportaciones_por_socio
                                join s in db.socios 
                                on ap.SOCIOS_ID equals s.SOCIOS_ID
                                where
                                (s.SOCIOS_ESTATUS >= 1) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : ap.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_NOMBRE_COMPLETO) ? true : ap.SOCIOS_NOMBRE_COMPLETO.Contains(SOCIOS_NOMBRE_COMPLETO)) &&
                                (APORTACIONES_SALDO_TOTAL == -1 ? true : ap.APORTACIONES_SALDO_TOTAL.Equals(APORTACIONES_SALDO_TOTAL)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : ap.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : ap.FECHA_CREACION == FECHA_CREACION)
                                select ap;

                    return query.OrderBy(a => a.SOCIOS_ID).OrderByDescending(a => a.FECHA_CREACION).ToList<reporte_total_aportaciones_por_socio>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public reporte_total_aportaciones_por_socio GetAportacionesXSocio(string SOCIOS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from ap in db.reporte_total_aportaciones_por_socio
                                join s in db.socios
                                on ap.SOCIOS_ID equals s.SOCIOS_ID
                                where ap.SOCIOS_ID == SOCIOS_ID
                                select ap;

                    return query.FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    
        #region insert

        public void InsertarTransaccionAportacionesDeSocio(liquidacion HojaDeLiquidacion,colinasEntities db)
        {
            try
            {
                reporte_total_aportaciones_por_socio asocInventory = this.GetAportacionesXSocio(HojaDeLiquidacion.SOCIOS_ID);

                decimal saldo_aportaciones_ordinaria = asocInventory == null ? 0 : asocInventory.APORTACIONES_ORDINARIA_SALDO;
                decimal saldo_aportaciones_extraordinaria = asocInventory == null ? 0 : asocInventory.APORTACIONES_EXTRAORDINARIA_SALDO;
                decimal saldo_aportaciones_capitalizacion_retencion = asocInventory == null ? 0 : asocInventory.APORTACIONES_CAPITALIZACION_RETENCION_SALDO;
                decimal saldo_aportaciones_intereses_aportaciones = asocInventory == null ? 0 : asocInventory.APORTACIONES_INTERESES_S_APORTACION_SALDO;
                decimal saldo_aportaciones_excedente_periodo = asocInventory == null ? 0 : asocInventory.APORTACIONES_EXCEDENTE_PERIODO_SALDO;

                decimal saldo_aportaciones_total = asocInventory == null ? 0 : asocInventory.APORTACIONES_SALDO_TOTAL;

                decimal liquidacion_aportacion_extraordinaria = (HojaDeLiquidacion.LIQUIDACIONES_D_APORTACION_EXTRAORD_COOP == true ? HojaDeLiquidacion.LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA : 0);

                aportacion_socio aportacionDeSocio = new aportacion_socio();
                
                aportacionDeSocio.SOCIOS_ID = HojaDeLiquidacion.SOCIOS_ID;
                aportacionDeSocio.DOCUMENTO_ID = HojaDeLiquidacion.LIQUIDACIONES_ID;
                aportacionDeSocio.DOCUMENTO_TIPO = "ENTRADA";//Las hojas de liquidaciones son tomadas como entradas para las aportaciones

                aportacionDeSocio.APORTACIONES_ORDINARIA_SALDO = saldo_aportaciones_ordinaria + HojaDeLiquidacion.LIQUIDACIONES_D_APORTACION_ORDINARIO;
                aportacionDeSocio.APORTACIONES_EXTRAORDINARIA_SALDO = saldo_aportaciones_extraordinaria + liquidacion_aportacion_extraordinaria;
                aportacionDeSocio.APORTACIONES_CAPITALIZACION_RETENCION_SALDO = saldo_aportaciones_capitalizacion_retencion + HojaDeLiquidacion.LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD;
                aportacionDeSocio.APORTACIONES_INTERESES_S_APORTACION_SALDO = saldo_aportaciones_intereses_aportaciones + HojaDeLiquidacion.LIQUIDACIONES_D_INTERESES_S_APORTACIONES;
                aportacionDeSocio.APORTACIONES_EXCEDENTE_PERIODO_SALDO = saldo_aportaciones_excedente_periodo + HojaDeLiquidacion.LIQUIDACIONES_D_EXCEDENTE_PERIODO;

                aportacionDeSocio.APORTACIONES_CANTIDAD = Convert.ToDecimal
                    (HojaDeLiquidacion.LIQUIDACIONES_D_APORTACION_ORDINARIO +
                    liquidacion_aportacion_extraordinaria +
                    HojaDeLiquidacion.LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD +
                    HojaDeLiquidacion.LIQUIDACIONES_D_INTERESES_S_APORTACIONES +
                    HojaDeLiquidacion.LIQUIDACIONES_D_EXCEDENTE_PERIODO);

                aportacionDeSocio.APORTACIONES_SALDO_TOTAL = saldo_aportaciones_total + aportacionDeSocio.APORTACIONES_CANTIDAD;                    

                aportacionDeSocio.CREADO_POR = HojaDeLiquidacion.CREADO_POR;
                aportacionDeSocio.FECHA_CREACION = HojaDeLiquidacion.FECHA_CREACION;

                db.aportaciones_socio.AddObject(aportacionDeSocio);

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertarTransaccionAportacionesDeSocio(retiro_aportaciones RetiroDeAportaciones, colinasEntities db)
        {
            try
            {
                reporte_total_aportaciones_por_socio asocInventory = this.GetAportacionesXSocio(RetiroDeAportaciones.SOCIOS_ID);

                decimal saldo_aportaciones_ordinaria = asocInventory == null ? 0 : asocInventory.APORTACIONES_ORDINARIA_SALDO;
                decimal saldo_aportaciones_extraordinaria = asocInventory == null ? 0 : asocInventory.APORTACIONES_EXTRAORDINARIA_SALDO;
                decimal saldo_aportaciones_capitalizacion_retencion = asocInventory == null ? 0 : asocInventory.APORTACIONES_CAPITALIZACION_RETENCION_SALDO;
                decimal saldo_aportaciones_intereses_aportaciones = asocInventory == null ? 0 : asocInventory.APORTACIONES_INTERESES_S_APORTACION_SALDO;
                decimal saldo_aportaciones_excedente_periodo = asocInventory == null ? 0 : asocInventory.APORTACIONES_EXCEDENTE_PERIODO_SALDO;

                decimal saldo_aportaciones_total = asocInventory == null ? 0 : asocInventory.APORTACIONES_SALDO_TOTAL;

                decimal liquidacion_aportacion_extraordinaria = RetiroDeAportaciones.RETIROS_AP_EXTRAORDINARIA;

                aportacion_socio aportacionDeSocio = new aportacion_socio();

                aportacionDeSocio.SOCIOS_ID = RetiroDeAportaciones.SOCIOS_ID;
                aportacionDeSocio.DOCUMENTO_ID = RetiroDeAportaciones.RETIROS_AP_ID;
                aportacionDeSocio.DOCUMENTO_TIPO = "SALIDA";//Los retiros de aportaciones son tomadas como salidas para las aportaciones

                aportacionDeSocio.APORTACIONES_ORDINARIA_SALDO = saldo_aportaciones_ordinaria - RetiroDeAportaciones.RETIROS_AP_ORDINARIA;
                aportacionDeSocio.APORTACIONES_EXTRAORDINARIA_SALDO = saldo_aportaciones_extraordinaria - liquidacion_aportacion_extraordinaria;
                aportacionDeSocio.APORTACIONES_CAPITALIZACION_RETENCION_SALDO = saldo_aportaciones_capitalizacion_retencion - RetiroDeAportaciones.RETIROS_AP_CAPITALIZACION_RETENCION;
                aportacionDeSocio.APORTACIONES_INTERESES_S_APORTACION_SALDO = saldo_aportaciones_intereses_aportaciones + RetiroDeAportaciones.RETIROS_AP_INTERESES_S_APORTACION;
                aportacionDeSocio.APORTACIONES_EXCEDENTE_PERIODO_SALDO = saldo_aportaciones_excedente_periodo + RetiroDeAportaciones.RETIROS_AP_EXCEDENTE_PERIODO;

                aportacionDeSocio.APORTACIONES_CANTIDAD = - RetiroDeAportaciones.RETIROS_AP_TOTAL_RETIRADO;
                aportacionDeSocio.APORTACIONES_SALDO_TOTAL = saldo_aportaciones_total + aportacionDeSocio.APORTACIONES_CANTIDAD;

                aportacionDeSocio.CREADO_POR = RetiroDeAportaciones.CREADO_POR;
                aportacionDeSocio.FECHA_CREACION = RetiroDeAportaciones.FECHA_CREACION;

                db.aportaciones_socio.AddObject(aportacionDeSocio);

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
