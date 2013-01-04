using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Utiles
{
    public enum EstadosNotificacion
    {
        Creado = 0,
        Notificado = 1,
        Leido = 2
    }

    public class NotificacionLogic
    {
        public NotificacionLogic() { }

        #region Select

        public List<notificacion> GetNotificaciones()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.notificaciones.ToList<notificacion>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<notificacion> GetNotificacionesDeUsuario(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    return user.notificaciones.ToList<notificacion>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<notificacion> GetNotificacionesDeUsuarioCreado(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    var query = from n in user.notificaciones
                                where EstadosNotificacion.Creado.CompareTo(n.NOTIFICACION_ESTADO) == 0
                                select n;

                    return query.ToList<notificacion>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<notificacion> GetNotificacionesDeUsuarioNotificado(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    var query = from n in user.notificaciones
                                where EstadosNotificacion.Notificado.CompareTo(n.NOTIFICACION_ESTADO) == 0
                                select n;

                    return query.ToList<notificacion>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<notificacion> GetNotificacionesDeUsuarioLeido(string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.usuarios", "USR_USERNAME", USR_USERNAME);

                    var u = db.GetObjectByKey(k);

                    usuario user = (usuario)u;

                    var query = from n in user.notificaciones
                                where EstadosNotificacion.Leido.CompareTo(n.NOTIFICACION_ESTADO) == 0
                                select n;

                    return query.ToList<notificacion>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Insert

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

                    db.notificaciones.AddObject(notification);

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Update

        public void ActualizarNotificacion(int NOTIFICACION_ID, EstadosNotificacion NOTIFICACION_ESTADO)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notifiaciones", "NOTIFICACION_ID", NOTIFICACION_ID);

                    var n = db.GetObjectByKey(k);

                    notificacion notification = (notificacion)n;

                    notification.NOTIFICACION_ESTADO = (int)NOTIFICACION_ESTADO;

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Delete
        #endregion
    }
}
