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
                    return db.notificaciones.Where(n => n.NOTIFICACION_ESTADO != (int)EstadosNotificacion.Leido).ToList<notificacion>();
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
                    notification.NOTIFICACION_FECHA = DateTime.Now;

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
                    EntityKey k = new EntityKey("colinasEntities.notificaciones", "NOTIFICACION_ID", NOTIFICACION_ID);

                    Object n = null;

                    //var n = db.GetObjectByKey(k);

                    if (db.TryGetObjectByKey(k, out n))
                    {

                        notificacion notification = (notificacion)n;

                        notification.NOTIFICACION_ESTADO = (int)NOTIFICACION_ESTADO;

                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Delete

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
            catch (Exception)
            {

                throw;
            }
        }

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
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion
    }
}
