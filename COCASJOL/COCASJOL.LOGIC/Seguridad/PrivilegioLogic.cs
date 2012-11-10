using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using System.Xml.Linq;

namespace COCASJOL.LOGIC.Security
{
    public class PrivilegioLogic
    {
        public PrivilegioLogic() { }

        #region Select

        public IQueryable GetPrivilegios()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.privilegios;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<privilegio> GetPrivilegios
            (int PRIV_ID,
            string PRIV_NOMBRE,
            string PRIV_DESCRIPCION,
            string PRIV_LLAVE,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from privs in db.privilegios
                                where
                                (PRIV_ID.Equals(0) ? true : privs.PRIV_ID.Equals(PRIV_ID)) &&
                                (string.IsNullOrEmpty(PRIV_NOMBRE) ? true : privs.PRIV_NOMBRE.Contains(PRIV_NOMBRE)) &&
                                (string.IsNullOrEmpty(PRIV_DESCRIPCION) ? true : privs.PRIV_DESCRIPCION.Contains(PRIV_DESCRIPCION)) &&
                                (string.IsNullOrEmpty(PRIV_LLAVE) ? true : privs.PRIV_LLAVE.Contains(PRIV_LLAVE)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : privs.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : privs.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : privs.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : privs.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select privs;

                    return query.ToList<privilegio>();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Insert

        /*
        public void InsertarPrivilegio
            (int PRIV_ID,
            string PRIV_NOMBRE,
            string PRIV_DESCRIPCION,
            string PRIV_LLAVE,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    privilegio priv = new privilegio();
                    priv.PRIV_ID = PRIV_ID;
                    priv.PRIV_NOMBRE = PRIV_NOMBRE;
                    priv.PRIV_DESCRIPCION = PRIV_DESCRIPCION;
                    priv.PRIV_LLAVE = PRIV_LLAVE;
                    priv.CREADO_POR = CREADO_POR;
                    priv.FECHA_CREACION = DateTime.Today;
                    priv.MODIFICADO_POR = CREADO_POR;
                    priv.FECHA_MODIFICACION = priv.FECHA_CREACION;

                    db.privilegios.AddObject(priv);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        */
        #endregion

        #region Update

        /*
        public void ActualizarPrivilegio
            (int PRIV_ID,
            string PRIV_NOMBRE,
            string PRIV_DESCRIPCION,
            string PRIV_LLAVE,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    var query = from pr in db.privilegios
                                where pr.PRIV_ID == PRIV_ID
                                select pr;

                    privilegio priv = query.First();

                    priv.PRIV_ID = PRIV_ID;
                    priv.PRIV_NOMBRE = PRIV_NOMBRE;
                    priv.PRIV_DESCRIPCION = PRIV_DESCRIPCION;
                    priv.PRIV_LLAVE = PRIV_LLAVE;
                    priv.CREADO_POR = CREADO_POR;
                    priv.FECHA_CREACION = FECHA_CREACION;
                    priv.MODIFICADO_POR = MODIFICADO_POR;
                    priv.FECHA_MODIFICACION = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        */
        #endregion

        #region Delete

        /*
        public void EliminarPrivilegio(int PRIV_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    var query = from pr in db.privilegios
                                where pr.PRIV_ID == PRIV_ID
                                select pr;

                    privilegio priv = query.First();

                    db.DeleteObject(priv);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        */
        #endregion
    }
}
