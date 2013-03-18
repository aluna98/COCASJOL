using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
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

            public List<solicitud_prestamo> getViewSolicitud()
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                    where solicitud.SOLICITUD_ESTADO == "PENDIENTE" || solicitud.SOLICITUD_ESTADO == "RECHAZADA"
                                    select solicitud;

                        return query.ToList<solicitud_prestamo>();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public List<solicitud_prestamo> getViewPrestamo()
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from solicitud in db.solicitudes_prestamos.Include("socios")
                                    where solicitud.SOLICITUD_ESTADO == "APROBADAS" || solicitud.SOLICITUD_ESTADO == "FINALIZADAS"
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
                                where socios.SOCIOS_ESTATUS == 1
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

            public List<referencia_x_solicitud> getReferencia(int id)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from referencia in db.referencias_x_solicitudes.Include("solicitudes_prestamos")
                                    where referencia.SOLICITUDES_ID == id
                                    select referencia;
                        return query.ToList<referencia_x_solicitud>();
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

            }

            public List<aval_x_solicitud> getAvales(int id)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        List<aval_x_solicitud> lista = new List<aval_x_solicitud>();
                        var query = from aval in db.avales_x_solicitud.Include("socios").Include("solicitudes_prestamos")
                                    where aval.SOLICITUDES_ID == id
                                    select aval;
                        lista = query.ToList<aval_x_solicitud>();
                        return lista;
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            public socio getAval(string id)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        socio lista = new socio();
                        var query = from soc in db.socios
                                    where soc.SOCIOS_ID == id
                                    select soc;
                        lista = query.First();
                        return lista;
                    }
                }
                catch (Exception e)
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
                solicitud.SOLICITUD_ESTADO = "PENDIENTE";
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

            public void InsertarReferencia(string id, int solicitudid, string nombre, string telefono, string tipo, string creadopor)
        {
            
            try
            {
                using (var db = new colinasEntities())
                {
                    referencia_x_solicitud referencia = new referencia_x_solicitud();
                    referencia.SOLICITUDES_ID = solicitudid;
                    referencia.REFERENCIAS_ID = id;
                    referencia.REFERENCIAS_NOMBRE = nombre;
                    referencia.REFERENCIAS_TELEFONO = telefono;
                    referencia.REFERENCIAS_TIPO = tipo;
                    referencia.CREADO_POR = referencia.MODIFICADO_POR = creadopor;
                    referencia.FECHA_CREACION = DateTime.Today;
                    referencia.FECHA_MODIFICACION = DateTime.Today;
                    db.referencias_x_solicitudes.AddObject(referencia);
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

            public void InsertarAval(string id, int solicitudid, string calificacion, string creadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    aval_x_solicitud aval = new aval_x_solicitud();
                    aval.AVALES_CALIFICACION = calificacion;
                    aval.SOCIOS_ID = id;
                    aval.SOLICITUDES_ID = solicitudid;
                    aval.CREADO_POR = creadopor;
                    aval.FECHA_CREACION = DateTime.Today;
                    aval.MODIFICADO_POR = creadopor;
                    aval.FECHA_MODIFICACION = DateTime.Today;
                    db.avales_x_solicitud.AddObject(aval);
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
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
                string calificacion, string estado, string modificadopor)
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
                    sol.SOLICITUD_ESTADO = estado;
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

            public void EditarReferencia(string id, int solicitudid, string nombre, string telefono, string tipo, string modificadopor)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from referencia in db.referencias_x_solicitudes
                                    where referencia.SOLICITUDES_ID == solicitudid && referencia.REFERENCIAS_ID == id
                                    select referencia;

                        referencia_x_solicitud nueva =  query.First();
                        nueva.REFERENCIAS_NOMBRE = nombre;
                        nueva.REFERENCIAS_TELEFONO = telefono;
                        nueva.REFERENCIAS_TIPO = tipo;
                        nueva.MODIFICADO_POR = modificadopor;
                        nueva.FECHA_MODIFICACION = DateTime.Today;
                        db.SaveChanges();
                        db.Dispose();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public void EditarAval(string id, int solicitudid, string calificacion, string modificadopor)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from aval2 in db.avales_x_solicitud
                                    where aval2.SOLICITUDES_ID == solicitudid && aval2.SOCIOS_ID == id
                                    select aval2;

                        aval_x_solicitud aval = query.First();
                        aval.AVALES_CALIFICACION = calificacion;
                        aval.MODIFICADO_POR = modificadopor;
                        aval.FECHA_MODIFICACION = DateTime.Today;
                        db.SaveChanges();
                        db.Dispose();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        #endregion

        #region Delete

            public void EliminarReferencia(string ReferenciaId, int SolicitudId)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from referencia in db.referencias_x_solicitudes
                                    where referencia.REFERENCIAS_ID == ReferenciaId && referencia.SOLICITUDES_ID == SolicitudId
                                    select referencia;
                        referencia_x_solicitud nueva = query.First();
                        db.referencias_x_solicitudes.DeleteObject(nueva);
                        db.SaveChanges();
                        db.Dispose();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public void EliminarAval(string id, int solicitudid)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from aval in db.avales_x_solicitud
                                    where aval.SOLICITUDES_ID == solicitudid && aval.SOCIOS_ID == id
                                    select aval;

                        aval_x_solicitud eliminar = query.First();
                        db.avales_x_solicitud.DeleteObject(eliminar);
                        db.SaveChanges();
                        db.Dispose();
                    }
                }
                catch (Exception)
                {
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

            public bool BuscarId(int SOLICITUDES_ID, string REFERENCIAS_ID)
            {
                colinasEntities db = null;
                try
                {
                    db = new colinasEntities();
                    List<referencia_x_solicitud> lista;
                    var query = from nuevo in db.referencias_x_solicitudes
                                where nuevo.SOLICITUDES_ID == SOLICITUDES_ID && nuevo.REFERENCIAS_ID == REFERENCIAS_ID
                                select nuevo;

                    lista = query.ToList<referencia_x_solicitud>();

                    if (lista.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    if (db != null)
                    {
                        db.Dispose();
                    }
                    throw;
                }
            }

            public bool BuscarAval(string SOCIOS_ID)
            {
                try
                {
                    using (var db = new colinasEntities())
                    {
                        var query = from aval in db.avales_x_solicitud.Include("solicitudes_prestamos")
                                    where aval.SOCIOS_ID == SOCIOS_ID
                                    select aval;
                        List<aval_x_solicitud> lista = query.ToList<aval_x_solicitud>();
                        if (lista.Count() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            public static void getSociosDBISAM()
            {
                try
                {
                    string queryString = "select * from scategoria";
                    OdbcCommand command = new OdbcCommand(queryString);

                    string connectionString = "PROVIDER=MSDASQL;DSN=MYDBISAM";

                    using (OdbcConnection connection = new OdbcConnection(connectionString))
                    {
                        command.Connection = connection;
                        connection.Open();

                        OdbcDataReader reader = command.ExecuteReader();

                        int fCount = reader.FieldCount;

                        string rec = "";

                        while (reader.Read())
                        {
                            for (int i = 0; i < fCount; i++)
                            {
                                rec += " " + (reader.GetValue(i).ToString());
                            }

                            rec += "\n";
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
