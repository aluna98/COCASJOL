using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

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
                                where a.socios.SOCIOS_ESTATUS == 1
                                select a;

                    return query.OrderBy(a => a.SOCIOS_ID).ToList<aportacion_socio>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<aportacion_socio> GetAportaciones
            ( string SOCIOS_ID,
             decimal APORTACIONES_SOLICITUD,
              string CREADO_POR,
            DateTime FECHA_CREACION,
              string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from ap in db.aportaciones_socio
                                where ap.socios.SOCIOS_ESTATUS == 1 &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : ap.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (ap.APORTACIONES_SALDO == APORTACIONES_SOLICITUD) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : ap.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : ap.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : ap.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : ap.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select ap;

                    return query.OrderBy(a => a.SOCIOS_ID).OrderByDescending(a => a.FECHA_MODIFICACION).ToList<aportacion_socio>();
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
