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
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                select solicitud;
                  
                    return query.ToList<solicitud_prestamo>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

            public List<socio> getSocios()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from socios in db.socios.Include("socios_generales").Include("socios_produccion")
                                select socios;
                    return query.ToList<socio>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

            public socio getSocio(string socioid)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from socios in db.socios.Include("socios_generales").Include("socios_produccion")
                                where socios.SOCIOS_ID == socioid
                                select socios;
                    return query.First();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

            public solicitud_prestamo getSolicitud(int id)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                where solicitud.SOLICITUDES_ID == id
                                select solicitud;
                    return query.First();
                }
            }
            catch (Exception)
            {
                throw;
            }
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

        #region Update
        public void EditarSolicitud(int idsolicitud, 
            decimal monto, int interes, string plazo, string pago, 
            string destino, string cargo, decimal promedio3, 
            decimal prodact, string norte, string sur, 
            string este, string oeste, int carro, int agua, 
            int luz, int casa, int beneficio, string otros, 
            string calificacion, string modificadopor)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                var query = from solicitud in db.solicitudes_prestamos
                            where idsolicitud == solicitud.SOLICITUDES_ID
                            select solicitud;
                solicitud_prestamo sol = query.First();
                sol.SOLICITUDES_MONTO = monto;
                sol.SOLICITUDES_INTERES = interes;
                sol.SOLICITUDES_PLAZO = DateTime.Parse(plazo);
                sol.SOLICITUDES_PAGO = pago;
                sol.SOLICITUDES_DESTINO = destino;
                sol.SOLICITUDES_CARGO = cargo;
                sol.SOLICITUDES_PROMEDIO3 = promedio3;
                sol.SOLICITUDES_PRODUCCIONACT = prodact;
                sol.SOLICITUDES_NORTE = norte;
                sol.SOLICITUDES_SUR = sur;
                sol.SOLICITUDES_ESTE = este;
                sol.SOLICITUDES_OESTE = oeste;
                sol.SOLICITUDES_VEHICULO = (sbyte)carro;
                sol.SOLICITUDES_AGUA = (sbyte)agua;
                sol.SOLICITUDES_ENEE = (sbyte)luz;
                sol.SOLICITUDES_CASA = (sbyte)casa;
                sol.SOLICITUDES_BENEFICIO = (sbyte)beneficio;
                sol.SOLICITUD_OTROSCULTIVOS = otros;
                sol.SOLICITUD_CALIFICACION = calificacion;
                sol.MODIFICADO_POR = modificadopor;
                sol.FECHA_MODIFICACION = DateTime.Today;
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception e)
            {
                if(db != null){
                    db.Dispose();
                }
                throw;
            }
        }
        #endregion

        #region Metodos

        public socio_produccion getProduccion(string id)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    var query = from carnet in db.socios_produccion
                                where carnet.SOCIOS_ID == id
                                select carnet;
                    return query.First(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string getIHCAFE(string id){
            using (var db = new colinasEntities())
            {

                var query = from carnet in db.socios_generales
                            where carnet.SOCIOS_ID == id
                            select carnet;

                return query.First().GENERAL_CARNET_IHCAFE; 
            }
        }

        #endregion
    }
}
