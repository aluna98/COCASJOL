using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Prestamos
{
    public class PrestamosLogic
    {
        public PrestamosLogic() { }

        #region select
        public List<prestamo> getData()
        {
            colinasEntities db = new colinasEntities();
            var query = from prestamos in db.prestamos
                        select prestamos;
            return query.ToList<prestamo>();
        }
        #endregion

        #region update
        public void ActualizarPrestamos(int id, string nombre, string descripcion, int cantmax, int interes, string modificadopor)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                var query = from p in db.prestamos
                            where p.PRESTAMOS_ID == id
                            select p;
                prestamo editar = query.First();
                editar.PRESTAMOS_NOMBRE = nombre;
                editar.PRESTAMOS_DESCRIPCION = descripcion;
                editar.PRESTAMOS_CANT_MAXIMA = cantmax;
                editar.PRESTAMOS_INTERES = interes;
                db.SaveChanges();
                db.Dispose();
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
        #endregion

        #region insert
        public void InsertarPrestamo(int prestamoid, string nombre, string descrip, int max, int interes, string creadopor)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
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
                db.Dispose();

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
        #endregion

        #region delete
        public void EliminarPrestamo(int prestamoid)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();
                var query = from p in db.prestamos
                            where p.PRESTAMOS_ID == prestamoid
                            select p;
                if (query.First() != null)
                {
                    db.prestamos.DeleteObject(query.First());
                }
                db.SaveChanges();
                db.Dispose();
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
        #endregion

        #region Metodos

        public bool ExistePrestamo(string nombre)
        {
            bool found = false;
            colinasEntities db = new colinasEntities();
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

        public int Intereses(int prestamo)
        {
            colinasEntities db = new colinasEntities();
            var query = from p in db.prestamos
                        where p.PRESTAMOS_ID == prestamo
                        select p;

            return query.First().PRESTAMOS_INTERES;
        }
        #endregion
    }
}
