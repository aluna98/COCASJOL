using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Seguridad
{
    public class RolLogic
    {
        public RolLogic() { }

        #region Select

        public IQueryable GetRoles()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.roles;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<rol> GetRoles
            (int ROL_ID,
            string ROL_NOMBRE,
            string ROL_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from rls in db.roles
                                where
                                (ROL_ID.Equals(0) ? true : rls.ROL_ID.Equals(ROL_ID)) &&
                                (string.IsNullOrEmpty(ROL_NOMBRE) ? true : rls.ROL_NOMBRE.Contains(ROL_NOMBRE)) &&
                                (string.IsNullOrEmpty(ROL_DESCRIPCION) ? true : rls.ROL_DESCRIPCION.Contains(ROL_DESCRIPCION)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : rls.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : rls.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : rls.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : rls.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select rls;

                    return query.ToList<rol>();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<privilegio> GetPrivilegios(int ROL_ID, int PRIV_ID, string PRIV_NOMBRE, string PRIV_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);

                    var r = db.GetObjectByKey(k);

                    rol role = (rol)r;

                    var filter = from privs in role.privilegios
                                 where
                                 (PRIV_ID.Equals(0) ? true : privs.PRIV_ID.Equals(PRIV_ID)) &&
                                 (string.IsNullOrEmpty(PRIV_NOMBRE) ? true : privs.PRIV_NOMBRE.Contains(PRIV_NOMBRE)) &&
                                 (string.IsNullOrEmpty(PRIV_LLAVE) ? true : privs.PRIV_LLAVE.Contains(PRIV_LLAVE))
                                 select privs;

                    return filter.ToList<privilegio>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<privilegio> GetPrivilegiosNoDeRol(int ROL_ID, int PRIV_ID, string PRIV_NOMBRE, string PRIV_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = db.GetPrivilegiosNoDeRol(ROL_ID);

                    var filter = from privs in query
                                 where
                                 (PRIV_ID.Equals(0) ? true : privs.PRIV_ID.Equals(PRIV_ID)) &&
                                 (string.IsNullOrEmpty(PRIV_NOMBRE) ? true : privs.PRIV_NOMBRE.Contains(PRIV_NOMBRE)) &&
                                 (string.IsNullOrEmpty(PRIV_LLAVE) ? true : privs.PRIV_LLAVE.Contains(PRIV_LLAVE))
                                 select privs;

                    return filter.ToList<privilegio>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarRol
            (int ROL_ID,
            string ROL_NOMBRE,
            string ROL_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    rol role = new rol();
                    role.ROL_ID = ROL_ID;
                    role.ROL_NOMBRE = ROL_NOMBRE;
                    role.ROL_DESCRIPCION = ROL_DESCRIPCION;
                    role.CREADO_POR = CREADO_POR;
                    role.FECHA_CREACION = DateTime.Today;
                    role.MODIFICADO_POR = CREADO_POR;
                    role.FECHA_MODIFICACION = role.FECHA_CREACION;

                    db.roles.AddObject(role);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InsertarPrivilegios(int ROL_ID, List<int> lprivilegios)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);

                    var r = db.GetObjectByKey(k);

                    rol role = (rol)r;

                    foreach (int priv_id in lprivilegios)
                    {
                        var p = db.GetObjectByKey(new EntityKey("colinasEntities.privilegios", "PRIV_ID", priv_id));

                        privilegio privilegioEntity = (privilegio)p;
                        role.privilegios.Add(privilegioEntity);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Update

        public void ActualizarRol
            (int ROL_ID,
            string ROL_NOMBRE,
            string ROL_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);

                    var r = db.GetObjectByKey(k);

                    rol role = (rol)r;

                    role.ROL_ID = ROL_ID;
                    role.ROL_NOMBRE = ROL_NOMBRE;
                    role.ROL_DESCRIPCION = ROL_DESCRIPCION;
                    role.CREADO_POR = CREADO_POR;
                    role.FECHA_CREACION = FECHA_CREACION;
                    role.MODIFICADO_POR = MODIFICADO_POR;
                    role.FECHA_MODIFICACION = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Delete

        public void EliminarRol(int ROL_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);

                    var r = db.GetObjectByKey(k);

                    rol role = (rol)r;

                    db.DeleteObject(role);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarPrivilegios(int ROL_ID, List<int> lprivilegios)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);

                    var r = db.GetObjectByKey(k);

                    rol role = (rol)r;

                    foreach (int priv_id in lprivilegios)
                    {
                        var p = db.GetObjectByKey(new EntityKey("colinasEntities.privilegios", "PRIV_ID", priv_id));

                        privilegio privilegioEntity = (privilegio)p;
                        role.privilegios.Remove(privilegioEntity);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
