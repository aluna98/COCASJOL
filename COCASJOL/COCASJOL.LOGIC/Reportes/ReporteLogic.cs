using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COCASJOL.LOGIC.Reportes
{
    public class ReporteLogic
    {
        public ReporteLogic() { }

        public List<nota_de_peso> GetNotasDePeso(int NOTAS_ID = 0)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from n in db.notas_de_peso.Include("socios").Include("clasificaciones_cafe").Include("estados_nota_de_peso")
                                where NOTAS_ID == 0 ? true : n.NOTAS_ID.Equals(NOTAS_ID)
                                select n;

                    return query.ToList<nota_de_peso>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<nota_detalle> GetNotasDetalle(int NOTAS_ID = 0)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from nd in db.notas_de_peso.Include("notas_detalles")
                                where NOTAS_ID == 0 ? true : nd.NOTAS_ID.Equals(NOTAS_ID)
                                select nd;

                    return query.First<nota_de_peso>().notas_detalles.ToList<nota_detalle>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<liquidacion> GetHojasDeLiquidacion(int LIQUIDACIONES_ID = 0)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from l in db.liquidaciones.Include("socios").Include("clasificaciones_cafe")
                                where LIQUIDACIONES_ID == 0 ? true : l.LIQUIDACIONES_ID.Equals(LIQUIDACIONES_ID)
                                select l;

                    return query.ToList<liquidacion>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<socio> GetSocios(string SOCIOS_ID = "")
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from s in db.socios.Include("socios_generales").Include("socios_produccion")
                                where string.IsNullOrEmpty(SOCIOS_ID) ? true : s.SOCIOS_ID == SOCIOS_ID
                                select s;

                    return query.ToList<socio>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<beneficiario_x_socio> GetBeneficiariosDeSocio(string SOCIOS_ID = "")
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from bs in db.socios.Include("beneficiario_x_socio")
                                where string.IsNullOrEmpty(SOCIOS_ID) ? true : bs.SOCIOS_ID == SOCIOS_ID
                                select bs;

                    return query.First<socio>().beneficiario_x_socio.ToList<beneficiario_x_socio>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
