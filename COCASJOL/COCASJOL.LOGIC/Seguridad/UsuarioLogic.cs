using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Seguridad
{
    public class UsuarioLogic
    {
        public UsuarioLogic() { }

        #region Select

        public IQueryable GetUsuarios()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.usuarios.MergeOption = MergeOption.NoTracking;//optimizacion

                    return db.usuarios;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<usuario> GetUsuarios
            (string USR_USERNAME,
            string USR_NOMBRE,
            string USR_SEGUNDO_NOMBRE,
            string USR_APELLIDO,
            string USR_SEGUNDO_APELLIDO,
            string USR_CEDULA,
            string USR_CORREO,
            string USR_PUESTO,
            string USR_PASSWORD,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.usuarios.MergeOption = MergeOption.NoTracking;//optimizacion

                    var query = from usr in db.usuarios
                                where
                                (string.IsNullOrEmpty(USR_USERNAME) ? true : usr.USR_USERNAME.Contains(USR_USERNAME)) &&
                                (string.IsNullOrEmpty(USR_NOMBRE) ? true : usr.USR_NOMBRE.Contains(USR_NOMBRE)) &&
                                (string.IsNullOrEmpty(USR_SEGUNDO_NOMBRE) ? true : usr.USR_SEGUNDO_NOMBRE.Contains(USR_SEGUNDO_NOMBRE)) &&
                                (string.IsNullOrEmpty(USR_APELLIDO) ? true : usr.USR_APELLIDO.Contains(USR_APELLIDO)) &&
                                (string.IsNullOrEmpty(USR_SEGUNDO_APELLIDO) ? true : usr.USR_SEGUNDO_APELLIDO.Contains(USR_SEGUNDO_APELLIDO)) &&
                                (string.IsNullOrEmpty(USR_CEDULA) ? true : usr.USR_CEDULA.Equals(USR_CEDULA)) &&
                                (string.IsNullOrEmpty(USR_CORREO) ? true : usr.USR_CORREO.Contains(USR_CORREO)) &&
                                (string.IsNullOrEmpty(USR_PUESTO) ? true : usr.USR_PUESTO.Contains(USR_PUESTO)) &&
                                (string.IsNullOrEmpty(USR_PASSWORD) ? true : usr.USR_PASSWORD.Contains(USR_PASSWORD)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : usr.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : usr.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : usr.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : usr.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select usr;

                    return query.ToList<usuario>();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<rol> GetRoles(string USR_USERNAME, int ROL_ID, string ROL_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.roles.MergeOption = MergeOption.NoTracking;//optimizacion

                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    var query = from r in user.roles
                                select r;

                    var filter = from rls in query
                                 where
                                 (ROL_ID.Equals(0) ? true : rls.ROL_ID.Equals(ROL_ID)) &&
                                 (string.IsNullOrEmpty(ROL_NOMBRE) ? true : rls.ROL_NOMBRE.Contains(ROL_NOMBRE))
                                 select rls;

                    return filter.ToList<rol>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<rol> GetRolesNoDeUsuario(string USR_USERNAME, int ROL_ID, string ROL_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.roles.MergeOption = MergeOption.NoTracking;//optimizacion

                    var query = db.GetRolesNoDeUsuario(USR_USERNAME);

                    var filter = from rls in query
                                 where
                                 (ROL_ID.Equals(0) ? true : rls.ROL_ID.Equals(ROL_ID)) &&
                                 (string.IsNullOrEmpty(ROL_NOMBRE) ? true : rls.ROL_NOMBRE.Contains(ROL_NOMBRE))
                                 select rls;

                    return filter.ToList<rol>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public usuario GetUsuario(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.usuarios.MergeOption = MergeOption.NoTracking;

                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    return user;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<privilegio> GetPrivilegiosDeUsuario(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.privilegios.MergeOption = MergeOption.NoTracking;

                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    var query = from r in user.roles
                                from pr in r.privilegios
                                select pr;

                    return query.ToList<privilegio>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public List<privilegio> GetPrivilegiosNoDeUsuario(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.privilegios.MergeOption = MergeOption.NoTracking;

                    var query = db.GetPrivilegiosNoDeUsuario(USR_USERNAME);

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

        public void InsertarUsuario
            (string USR_USERNAME,
            string USR_NOMBRE,
            string USR_SEGUNDO_NOMBRE,
            string USR_APELLIDO,
            string USR_SEGUNDO_APELLIDO,
            string USR_CEDULA,
            string USR_CORREO,
            string USR_PUESTO,
            string USR_PASSWORD,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {            
            try
            {
                using (var db = new colinasEntities())
                {
                    usuario user = new usuario();
                    user.USR_USERNAME = USR_USERNAME;
                    user.USR_NOMBRE = USR_NOMBRE;
                    user.USR_SEGUNDO_NOMBRE = USR_SEGUNDO_NOMBRE;
                    user.USR_APELLIDO = USR_APELLIDO;
                    user.USR_SEGUNDO_APELLIDO = USR_SEGUNDO_APELLIDO;
                    user.USR_CEDULA = USR_CEDULA;
                    user.USR_CORREO = USR_CORREO;
                    user.USR_PUESTO = USR_PUESTO;
                    user.USR_PASSWORD = USR_PASSWORD;
                    user.CREADO_POR = CREADO_POR;
                    user.FECHA_CREACION = DateTime.Today;
                    user.MODIFICADO_POR = CREADO_POR;
                    user.FECHA_MODIFICACION = user.FECHA_CREACION;

                    db.usuarios.AddObject(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void InsertarRoles(string USR_USERNAME, List<int> roles)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    foreach (int rol_id in roles)
                    {
                        var r = db.GetObjectByKey(new EntityKey("colinasEntities.roles", "ROL_ID", rol_id));

                        rol rolEntity = (rol)r;
                        user.roles.Add(rolEntity);
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

        public void ActualizarUsuario
            (string USR_USERNAME,
            string USR_NOMBRE,
            string USR_SEGUNDO_NOMBRE,
            string USR_APELLIDO,
            string USR_SEGUNDO_APELLIDO,
            string USR_CEDULA,
            string USR_CORREO,
            string USR_PUESTO,
            string USR_PASSWORD,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    user.USR_USERNAME = USR_USERNAME;
                    user.USR_NOMBRE = USR_NOMBRE;
                    user.USR_SEGUNDO_NOMBRE = USR_SEGUNDO_NOMBRE;
                    user.USR_APELLIDO = USR_APELLIDO;
                    user.USR_SEGUNDO_APELLIDO = USR_SEGUNDO_APELLIDO;
                    user.USR_CEDULA = USR_CEDULA;
                    user.USR_CORREO = USR_CORREO;
                    user.USR_PUESTO = USR_PUESTO;
                    user.USR_PASSWORD = USR_PASSWORD;
                    user.CREADO_POR = CREADO_POR;
                    user.FECHA_CREACION = FECHA_CREACION;
                    user.MODIFICADO_POR = MODIFICADO_POR;
                    user.FECHA_MODIFICACION = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ActualizarClave(string USR_USERNAME, string USR_PASSWORD, string MODIFICADO_POR)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    user.USR_PASSWORD = USR_PASSWORD;
                    user.MODIFICADO_POR = MODIFICADO_POR;
                    user.FECHA_MODIFICACION = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ActualizarUsuario
            (string USR_USERNAME,
            string USR_NOMBRE,
            string USR_SEGUNDO_NOMBRE,
            string USR_APELLIDO,
            string USR_SEGUNDO_APELLIDO,
            string USR_CORREO,
            string MODIFICADO_POR)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    user.USR_USERNAME = USR_USERNAME;
                    user.USR_NOMBRE = USR_NOMBRE;
                    user.USR_SEGUNDO_NOMBRE = USR_SEGUNDO_NOMBRE;
                    user.USR_APELLIDO = USR_APELLIDO;
                    user.USR_SEGUNDO_APELLIDO = USR_SEGUNDO_APELLIDO;
                    user.USR_CORREO = USR_CORREO;
                    user.MODIFICADO_POR = MODIFICADO_POR;
                    user.FECHA_MODIFICACION = DateTime.Now;

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

        public void EliminarUsuario(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    db.DeleteObject(user);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarRoles(string USR_USERNAME, List<int> roles)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    foreach(int rol_id in roles)
                    {
                        var r = db.GetObjectByKey(new EntityKey("colinasEntities.roles", "ROL_ID", rol_id));

                        rol rolEntity = (rol)r;
                        user.roles.Remove(rolEntity);
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

        #region Methods

        public bool Autenticar(string USR_USERNAME, string USR_PASSWORD)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.usuarios.MergeOption = MergeOption.NoTracking;

                    Object u = null;
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    if (db.TryGetObjectByKey(k, out u))
                    {
                        usuario usr = (usuario)u;
                        if (usr.USR_PASSWORD.CompareTo(USR_PASSWORD) == 0)
                            return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UsuarioExiste(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.usuarios.MergeOption = MergeOption.NoTracking;

                    Object u = null;
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    if (db.TryGetObjectByKey(k, out u))
                        return true;

                    return false;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public bool CedulaExiste(string USR_CEDULA)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.usuarios.MergeOption = MergeOption.NoTracking;

                    var query = from u in db.usuarios
                                where u.USR_CEDULA == USR_CEDULA
                                select u;
                    if (query.Count() > 0)
                        return true;
                    else
                        return false;
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
