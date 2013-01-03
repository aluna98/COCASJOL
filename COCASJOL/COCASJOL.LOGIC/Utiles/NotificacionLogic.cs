using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COCASJOL.LOGIC.Utiles
{
    public class NotificacionLogic
    {
        public NotificacionLogic() { }

        #region Select

        public List<Notificacion> GetNotificaciones()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Insert



        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }

    public enum EstadosNotificacion
    {
        Creado,
        Notificado,
        Leido
    }

    public class Notificacion
    {
        #region Propiedades

        string _UserName; 
        string _Title;
        string _Message;
        EstadosNotificacion _Estado;

        #endregion

        #region Constructores

        public Notificacion(string UserName, string Title, string Message, EstadosNotificacion Estado)
        {
            try
            {
                this._UserName = UserName;
                this._Title = Title;
                this._Message = Message;
                this._Estado = Estado;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Gets y Sets

        public string UserName
        {
            get { return this._UserName; }
            set { this._UserName = value; }
        }

        public string Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }

        public string Message
        {
            get { return this._Message; }
            set { this._Message = value; }
        }

        public EstadosNotificacion Estado
        {
            get { return this._Estado; }
            set { this._Estado = value; }
        }

        #endregion
    }
}
