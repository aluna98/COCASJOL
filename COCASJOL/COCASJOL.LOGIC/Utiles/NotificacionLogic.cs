using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Utiles
{
    /// <summary>
    /// Estados de Notificación
    /// </summary>
    public enum EstadosNotificacion
    {
        /// <summary>
        /// Estado Creada. Se muestran durante la sesión.
        /// </summary>
        Creado = 0,
        /// <summary>
        /// Estado Notificando. Se muestran al inicio de sesión.
        /// </summary>
        Notificado = 1,
        /// <summary>
        /// Estado Leído. Las notificaciones ya no se muestran en el escritorio.
        /// </summary>
        Leido = 2
    }

    /// <summary>
    /// Clase con logica de Notificación
    /// </summary>
    public class NotificacionLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(NotificacionLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotificacionLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todas las notificaciones.
        /// </summary>
        /// <returns>Lista de notificaciones.</returns>
        public List<notificacion> GetNotificaciones()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.notificaciones.Where(n => n.NOTIFICACION_ESTADO != (int)EstadosNotificacion.Leido).OrderBy(nt => nt.NOTIFICACION_FECHA).ToList<notificacion>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notificaciones.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las notificaciones de usuario.
        /// </summary>
        /// <param name="USR_USERNAME"></param>
        /// <returns>Lista de notificaciones.</returns>
        public List<notificacion> GetNotificacionesDeUsuario(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    return user.notificaciones.OrderByDescending(nt => nt.NOTIFICACION_FECHA).ToList<notificacion>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notificaciones de usuario.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserta la notificación.
        /// </summary>
        /// <param name="USR_USERNAME"></param>
        /// <param name="TITLE"></param>
        /// <param name="MENSAJE"></param>
        public void InsertarNotificacion(string USR_USERNAME, string TITLE, string MENSAJE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    notificacion notification = new notificacion();

                    notification.NOTIFICACION_ESTADO = (int)EstadosNotificacion.Creado;
                    notification.NOTIFICACION_TITLE = TITLE;
                    notification.NOTIFICACION_MENSAJE = MENSAJE;
                    notification.USR_USERNAME = USR_USERNAME;
                    notification.NOTIFICACION_FECHA = DateTime.Now;

                    db.notificaciones.AddObject(notification);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar notificacion.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza la notificación.
        /// </summary>
        /// <param name="NOTIFICACION_ID"></param>
        /// <param name="NOTIFICACION_ESTADO"></param>
        public void ActualizarNotificacion(int NOTIFICACION_ID, EstadosNotificacion NOTIFICACION_ESTADO)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notificaciones", "NOTIFICACION_ID", NOTIFICACION_ID);

                    Object n = null;

                    if (db.TryGetObjectByKey(k, out n))
                    {

                        notificacion notification = (notificacion)n;

                        notification.NOTIFICACION_ESTADO = (int)NOTIFICACION_ESTADO;

                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar notificacion.", ex);
                throw;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Elimina todas las notificaciones del usuario.
        /// </summary>
        /// <param name="USR_USERNAME"></param>
        public void EliminarNotificacionesDeUsuario(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    var query = from n in user.notificaciones
                                where n.NOTIFICACION_ESTADO == (int)EstadosNotificacion.Leido
                                select n;

                    foreach (notificacion notif in query.ToList<notificacion>())
                        db.notificaciones.DeleteObject(notif);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar todas las notificaciones de usuario.", ex);
                throw;
            }
        }

        /// <summary>
        /// Elimina todas las notifiaciones leidas.
        /// </summary>
        public void EliminarNotificacionesLeidas()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from n in db.notificaciones
                                where n.NOTIFICACION_ESTADO == (int)EstadosNotificacion.Leido
                                select n;

                    foreach (notificacion notification in query.ToList<notificacion>())
                    {
                        db.notificaciones.DeleteObject(notification);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notificaciones leidas de usuario.", ex);
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Notifica a todos los usuarios con privilegio específico.
        /// </summary>
        /// <param name="PRIVS_LLAVE"></param>
        /// <param name="estado"></param>
        /// <param name="titulo"></param>
        /// <param name="mensaje"></param>
        /// <param name="mensajeParams"></param>
        public void NotifyUsers(string PRIVS_LLAVE, EstadosNotificacion estado, string titulo, string mensaje, params object[] mensajeParams)
        {
            try
            {
                
                List<usuario> usuarios = null;

                if (string.IsNullOrEmpty(PRIVS_LLAVE))
                {
                    Seguridad.UsuarioLogic usuariologic = new Seguridad.UsuarioLogic();
                    usuarios = usuariologic.GetUsuarios();
                } else {
                    Seguridad.PrivilegioLogic privilegiologic = new Seguridad.PrivilegioLogic();
                    usuarios = privilegiologic.GetUsuariosWithPrivilege(PRIVS_LLAVE);
                }

                StringBuilder mensajeBuilder = new StringBuilder();
                string mensajeFormateado = mensajeBuilder.AppendFormat(mensaje, mensajeParams).ToString();

                using (var db = new colinasEntities())
                {
                    foreach (usuario usr in usuarios)
                    {
                        notificacion notification = new notificacion();
                        notification.NOTIFICACION_ESTADO = (int)estado;
                        notification.USR_USERNAME = usr.USR_USERNAME;
                        notification.NOTIFICACION_TITLE = titulo; //"Notas de Peso en Catación";
                        notification.NOTIFICACION_MENSAJE = mensajeFormateado ;  //"Ya tiene disponible la nota de peso #" + note.NOTAS_ID + ".";
                        notification.NOTIFICACION_FECHA = DateTime.Now;

                        db.notificaciones.AddObject(notification);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al notificar usuarios.", ex);
                throw;
            }
        }
    }
}
