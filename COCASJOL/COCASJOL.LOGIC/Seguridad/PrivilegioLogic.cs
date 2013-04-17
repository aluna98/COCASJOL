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
    /// Clase con logica de Privilegio
    /// </summary>
    public class PrivilegioLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(PrivilegioLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public PrivilegioLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todos los privilegios.
        /// </summary>
        /// <returns>Lista de Privilegios.</returns>
        public List<privilegio> GetPrivilegios()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.privilegios.MergeOption = MergeOption.NoTracking;

                    return db.privilegios.OrderBy(p => p.PRIV_LLAVE).ToList<privilegio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener privilegios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los privilegios.
        /// </summary>
        /// <param name="PRIV_ID"></param>
        /// <param name="PRIV_NOMBRE"></param>
        /// <param name="PRIV_DESCRIPCION"></param>
        /// <param name="PRIV_LLAVE"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de Privilegios.</returns>
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
                    db.privilegios.MergeOption = MergeOption.NoTracking;

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

                    return query.OrderBy(p => p.PRIV_LLAVE).ToList<privilegio>();
                }

            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener privilegios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los usuarios con privilegio específico.
        /// </summary>
        /// <param name="PRIV_LLAVE"></param>
        /// <returns>Lista de usuarios con privilegio específico.</returns>
        public List<usuario> GetUsuariosWithPrivilege(string PRIV_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from p in db.privilegios
                                from r in p.roles
                                from u in r.usuarios
                                where p.PRIV_LLAVE == PRIV_LLAVE
                                select u;

                    return query.ToList<usuario>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener usuarios con el privilegio especifico.", ex);
                throw;
            }
        }

        #endregion
    }
}
