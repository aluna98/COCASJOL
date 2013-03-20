using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

namespace COCASJOL.LOGIC.Aportaciones
{
    public class RetiroAportacionLogic
    {
        public RetiroAportacionLogic() { }

        #region Select

        public List<retiro_aportaciones> GetRetirosDeAportaciones()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from ra in db.retiros_aportaciones
                                select ra;

                    return query.OrderBy(ra => ra.SOCIOS_ID).ToList<retiro_aportaciones>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarRetiroDeAportaciones
            (string SOCIOS_ID,
             decimal RETIROS_AP_ORDINARIA,
             decimal RETIROS_AP_EXTRAORDINARIA,
            decimal RETIROS_AP_CAPITALIZACION_RETENCION,
            decimal RETIROS_AP_INTERESES_S_APORTACION,
            decimal RETIROS_AP_EXCEDENTE_PERIODO,
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
                        retiro_aportaciones retiro_aportacion = new retiro_aportaciones();

                        retiro_aportacion.SOCIOS_ID = SOCIOS_ID;
                        retiro_aportacion.RETIROS_AP_FECHA = DateTime.Today;

                        retiro_aportacion.RETIROS_AP_ORDINARIA = RETIROS_AP_ORDINARIA;
                        retiro_aportacion.RETIROS_AP_EXTRAORDINARIA = RETIROS_AP_EXTRAORDINARIA;
                        retiro_aportacion.RETIROS_AP_CAPITALIZACION_RETENCION = RETIROS_AP_CAPITALIZACION_RETENCION;
                        retiro_aportacion.RETIROS_AP_INTERESES_S_APORTACION = RETIROS_AP_INTERESES_S_APORTACION;
                        retiro_aportacion.RETIROS_AP_EXCEDENTE_PERIODO = RETIROS_AP_INTERESES_S_APORTACION;

                        retiro_aportacion.RETIROS_AP_TOTAL_RETIRADO =
                            RETIROS_AP_ORDINARIA +
                            RETIROS_AP_EXTRAORDINARIA +
                            RETIROS_AP_CAPITALIZACION_RETENCION +
                            RETIROS_AP_INTERESES_S_APORTACION +
                            RETIROS_AP_INTERESES_S_APORTACION;

                        retiro_aportacion.CREADO_POR = retiro_aportacion.MODIFICADO_POR = CREADO_POR;
                        retiro_aportacion.FECHA_CREACION = retiro_aportacion.RETIROS_AP_FECHA;
                        retiro_aportacion.FECHA_MODIFICACION = retiro_aportacion.RETIROS_AP_FECHA;

                        db.retiros_aportaciones.AddObject(retiro_aportacion);

                        db.SaveChanges();

                        AportacionLogic aportacionlogic = new AportacionLogic();
                        aportacionlogic.InsertarTransaccionAportacionesDeSocio(retiro_aportacion, db);
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
