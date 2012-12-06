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
        public void InsertarSolicitud(string idsocio, decimal monto,
            int interes, string plazo, string pago, string destino, int idprestamo, string cargo, decimal promedio3,
            decimal produccion, string norte, string sur, string oeste, string este, int vehiculo, int agua,
            int enee, int casa, int beneficio, string cultivos, string calificacion, string creadopor)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                solicitud_prestamo solicitud = new solicitud_prestamo();

                solicitud.SOCIOS_ID = idsocio;
                solicitud.SOLICITUDES_MONTO = monto;
                solicitud.SOLICITUDES_INTERES = interes;
                solicitud.SOLICITUDES_PLAZO = DateTime.Parse(plazo);
                solicitud.SOLICITUDES_PAGO = pago;
                solicitud.SOLICITUDES_DESTINO = destino;
                solicitud.PRESTAMOS_ID = idprestamo;
                solicitud.SOLICITUDES_CARGO = cargo;
                solicitud.SOLICITUDES_PROMEDIO3 = promedio3;
                solicitud.SOLICITUDES_PRODUCCIONACT = produccion;
                solicitud.SOLICITUDES_NORTE = norte;
                solicitud.SOLICITUDES_SUR = sur;
                solicitud.SOLICITUDES_ESTE = este;
                solicitud.SOLICITUDES_OESTE = oeste;
                solicitud.SOLICITUDES_VEHICULO = (sbyte)vehiculo;
                solicitud.SOLICITUDES_AGUA = (sbyte)agua;
                solicitud.SOLICITUDES_ENEE = (sbyte)enee;
                solicitud.SOLICITUDES_CASA = (sbyte)casa;
                solicitud.SOLICITUDES_BENEFICIO = (sbyte)beneficio;
                solicitud.SOLICITUD_OTROSCULTIVOS = cultivos;
                solicitud.SOLICITUD_CALIFICACION = calificacion;
                solicitud.CREADO_POR = creadopor;
                solicitud.FECHA_CREACION = DateTime.Today;
                solicitud.MODIFICADO_POR = creadopor;
                solicitud.FECHA_MODIFICACION = DateTime.Today;
                db.solicitudes_prestamos.AddObject(solicitud);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception e)
            {
                if (db != null) { db.Dispose(); }
                throw;
            }
        }
        #endregion
    }
}
