using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Seguridad
{
    /// <summary>
    /// Clase con logica de Rol
    /// </summary>
    public class RolLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(RolLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public RolLogic() { }
         
        #region Select

        /// <summary>
        /// Obtiene todos los roles.
        /// </summary>
        /// <returns>Lista de roles.</returns>
        public List<rol> GetRoles()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.roles.MergeOption = MergeOption.NoTracking;

                    return db.roles.OrderBy(r => r.ROL_NOMBRE).ToList<rol>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener roles.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los roles.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <param name="ROL_NOMBRE"></param>
        /// <param name="ROL_DESCRIPCION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de roles.</returns>
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
                    db.roles.MergeOption = MergeOption.NoTracking;

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

                    return query.OrderBy(r => r.ROL_NOMBRE).OrderByDescending(r => r.FECHA_MODIFICACION).ToList<rol>();
                }

            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener roles.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el rol específico.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <returns>Rol.</returns>
        public rol GetRol(int ROL_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);

                    var r = db.GetObjectByKey(k);

                    rol role = (rol)r;

                    return role;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener rol.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene privilegios de rol.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <param name="PRIV_ID"></param>
        /// <param name="PRIV_NOMBRE"></param>
        /// <param name="PRIV_LLAVE"></param>
        /// <returns>Lista de privilegios de rol.</returns>
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

                    return filter.OrderBy(p => p.PRIV_LLAVE).ToList<privilegio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener privilegios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene privilegios no de rol.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <param name="PRIV_ID"></param>
        /// <param name="PRIV_NOMBRE"></param>
        /// <param name="PRIV_LLAVE"></param>
        /// <returns>Lista de privilegios no de rol.</returns>
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

                    return filter.OrderBy(p => p.PRIV_LLAVE).ToList<privilegio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener privilegios no de rol.", ex);
                throw;
            }
        }

        #endregion

        #region Insert
        /// <summary>
        /// Inserta el rol.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <param name="ROL_NOMBRE"></param>
        /// <param name="ROL_DESCRIPCION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
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
                log.Fatal("Error fatal al insertar rol.", ex);
                throw;
            }
        }

        /// <summary>
        /// Inserta privilegios al rol.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <param name="lprivilegios"></param>
        /// <param name="MODIFICADO_POR"></param>
        public void InsertarPrivilegios(int ROL_ID, List<int> lprivilegios, string MODIFICADO_POR)
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

                    role.MODIFICADO_POR = MODIFICADO_POR;
                    role.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar privilegios.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza el rol.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <param name="ROL_NOMBRE"></param>
        /// <param name="ROL_DESCRIPCION"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
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

                    role.ROL_NOMBRE = ROL_NOMBRE;
                    role.ROL_DESCRIPCION = ROL_DESCRIPCION;
                    role.CREADO_POR = CREADO_POR;
                    role.FECHA_CREACION = FECHA_CREACION;
                    role.MODIFICADO_POR = MODIFICADO_POR;
                    role.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar rol.", ex);
                throw;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Eliminar el rol.
        /// </summary>
        /// <param name="ROL_ID"></param>
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
                log.Fatal("Error fatal al eliminar rol.", ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar privilegios de rol.
        /// </summary>
        /// <param name="ROL_ID"></param>
        /// <param name="lprivilegios"></param>
        /// <param name="MODIFICADO_POR"></param>
        public void EliminarPrivilegios(int ROL_ID, List<int> lprivilegios, string MODIFICADO_POR)
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

                    role.MODIFICADO_POR = MODIFICADO_POR;
                    role.FECHA_MODIFICACION = DateTime.Today;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar privilegios.", ex);
                throw;
            }
        }

        #endregion
    }
}
