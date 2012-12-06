using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Prestamos
{
    public class SolicitudesLogic
    {
        public SolicitudesLogic() { } 

    #region Select
        public List<solicitud_prestamo> getData()
        {
            colinasEntities db = new colinasEntities();
            var query = from solicitud in db.solicitudes_prestamos
                        select solicitud;
            return query.ToList<solicitud_prestamo>();
        }

        public List<socio> getSocios()
        {
            colinasEntities db = new colinasEntities();
            var query = from socios in db.socios
                        select socios;
            return query.ToList<socio>();
        }
    #endregion

        #region insert
        public void InsertarSolicitud(string idsocio, int idsolicitud, decimal monto,
            int interes, string plazo, string destino, int idprestamo, string cargo, decimal promedio3,
            decimal produccion, string norte, string sur, string oeste, string este, int vehiculo, int agua,
            int enee, int casa, int beneficio, string cultivos, string calificacion)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                solicitud_prestamo solicitud = new solicitud_prestamo();

                solicitud.SOCIOS_ID = idsocio;
                solicitud.SOLICITUDES_ID = idsolicitud;
                solicitud.SOLICITUDES_MONTO = monto;
                solicitud.SOLICITUDES_INTERES = (sbyte)interes;
                solicitud.SOLICITUDES_PLAZO = DateTime.Parse(plazo);
                solicitud.SOLICITUDES_DESTINO = destino;
                solicitud.PRESTAMOS_ID = idprestamo;
                solicitud.SOLICITUDES_CARGO = cargo;
                solicitud.SOLICITUDES_PROMEDIO3 = promedio3;

            }
            catch (Exception e)
            {

            }
        }
        #endregion
    }
}
