using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Prestamos
{
    /// <summary>
    /// Clase con logica de Tipos de Prestamo
    /// </summary>
    public class TiposPrestamoLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(TiposPrestamoLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public TiposPrestamoLogic() { }

        #region select
        /// <summary>
        /// Obtiene todos los tipos de prestamo.
        /// </summary>
        /// <returns>Lista de tipos de prestamo.</returns>
        public List<prestamo> getData()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from prestamos in db.prestamos
                                select prestamos;
                    return query.ToList<prestamo>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener prestamos aprobados.", ex);
                throw;
            }
        }
        #endregion

        #region update

        /// <summary>
        /// Actualiza el tipo de prestamo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="cantmax"></param>
        /// <param name="interes"></param>
        /// <param name="modificadopor"></param>
        public void ActualizarPrestamos(int id, string nombre, string descripcion, int cantmax, int interes, string modificadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from p in db.prestamos
                                where p.PRESTAMOS_ID == id
                                select p;
                    prestamo editar = query.First();
                    editar.PRESTAMOS_NOMBRE = nombre;
                    editar.PRESTAMOS_DESCRIPCION = descripcion;
                    editar.PRESTAMOS_CANT_MAXIMA = cantmax;
                    editar.PRESTAMOS_INTERES = interes;
                    db.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar prestamo.", ex);
                throw;
            }
        }
        #endregion

        #region insert

        /// <summary>
        /// Inserta el tipo de prestamo.
        /// </summary>
        /// <param name="prestamoid"></param>
        /// <param name="nombre"></param>
        /// <param name="descrip"></param>
        /// <param name="max"></param>
        /// <param name="interes"></param>
        /// <param name="creadopor"></param>
        public void InsertarPrestamo(int prestamoid, string nombre, string descrip, int max, int interes, string creadopor)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    prestamo nuevo = new prestamo();

                    nuevo.PRESTAMOS_NOMBRE = nombre;
                    nuevo.PRESTAMOS_DESCRIPCION = descrip;
                    nuevo.PRESTAMOS_CANT_MAXIMA = max;
                    nuevo.PRESTAMOS_INTERES = interes;
                    nuevo.CREADO_POR = creadopor;
                    nuevo.MODIFICADO_POR = creadopor;
                    nuevo.FECHA_CREACION = DateTime.Today;
                    nuevo.FECHA_MODIFICACION = DateTime.Today;
                    db.prestamos.AddObject(nuevo);
                    db.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar prestamo.", ex);
                throw;
            }
        }
        #endregion

        #region delete

        /// <summary>
        /// Eliminar el tipo de prestamo.
        /// </summary>
        /// <param name="prestamoid"></param>
        public void EliminarPrestamo(int prestamoid)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from p in db.prestamos
                                where p.PRESTAMOS_ID == prestamoid
                                select p;

                    prestamo prest = query.FirstOrDefault();

                    if (prest != null)
                    {
                        db.prestamos.DeleteObject(prest);
                    }
                    db.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar prestamo.", ex);
                throw;
            }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Verifica si existe el tipo de prestamo.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>True si existe el tipo de prestamo, False si es lo contrario.</returns>
        public bool ExistePrestamo(string nombre)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    bool found = false;
                    var query = from p in db.prestamos
                                where p.PRESTAMOS_NOMBRE == nombre
                                select p;
                    List<prestamo> lista = query.ToList<prestamo>();
                    if (lista.Count == 0)
                    {
                        found = true;
                    }
                    return found; 
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al comprabar existencia de prestamo.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el porcentaje de intereses asignado al tipo de prestamo.
        /// </summary>
        /// <param name="prestamo"></param>
        /// <returns>Porcentaje de intereses asignado al tipo de prestamo.</returns>
        public int Intereses(int prestamo)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from p in db.prestamos
                                where p.PRESTAMOS_ID == prestamo
                                select p;

                    return query.First().PRESTAMOS_INTERES;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener la cantidad de intereses de prestamo.", ex);
                throw;
            }
        }
        #endregion
    }
}
