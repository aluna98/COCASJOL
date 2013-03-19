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

        public List<aportacion_socio> GetAportaciones()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from a in db.aportaciones_socio
                                where a.socios.SOCIOS_ESTATUS >= 1
                                select a;

                    return query.OrderBy(a => a.SOCIOS_ID).ToList<aportacion_socio>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<reporte_total_aportaciones_por_socio> GetAportaciones
            ( string SOCIOS_ID,
             decimal APORTACIONES_SALDO,
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
                                (APORTACIONES_SALDO == 0 ? true : ap.APORTACIONES_SALDO.Equals(APORTACIONES_SALDO)) &&
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

        public void InsertarTransaccionAportacionesDeSocio(liquidacion HojaDeLiquidacion, bool aumentar_aportacion_extraord,colinasEntities db)
        {
            try
            {
                reporte_total_aportaciones_por_socio asocInventory = this.GetAportacionesXSocio(HojaDeLiquidacion.SOCIOS_ID);

                decimal saldo_aportaciones = asocInventory == null ? 0 : asocInventory.APORTACIONES_SALDO;

                aportacion_socio aportacionDeSocio = new aportacion_socio();
                
                aportacionDeSocio.SOCIOS_ID = HojaDeLiquidacion.SOCIOS_ID;
                aportacionDeSocio.DOCUMENTO_ID = HojaDeLiquidacion.LIQUIDACIONES_ID;
                aportacionDeSocio.DOCUMENTO_TIPO = "ENTRADA";//Las hojas de liquidaciones son tomadas como entradas para las aportaciones

                aportacionDeSocio.APORTACIONES_SALDO = saldo_aportaciones + 
                    Convert.ToDecimal
                        (HojaDeLiquidacion.LIQUIDACIONES_D_APORTACION_ORDINARIO + 
                        (aumentar_aportacion_extraord == true ? HojaDeLiquidacion.LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA : 0) +
                        HojaDeLiquidacion.LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD +
                        HojaDeLiquidacion.LIQUIDACIONES_D_INTERESES_S_APORTACIONES + //Pendiente confirmar
                        HojaDeLiquidacion.LIQUIDACIONES_D_EXCEDENTE_PERIODO);

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

        #endregion

    }
}
