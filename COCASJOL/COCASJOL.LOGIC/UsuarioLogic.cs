﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace COCASJOL.LOGIC
{
    public class UsuarioLogic
    {
        public UsuarioLogic() { }

        #region Select

        public IQueryable GetUsuarios()
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();

                return db.usuarios;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarUsuario(string USR_USERNAME,
            string USR_NOMBRE,
            string USR_APELLIDO,
            int USR_CEDULA,
            string USR_CORREO,
            string USR_PUESTO,
            string USR_PASSWORD,
            string CREADO_POR,
            string FECHA_CREACION,
            string MODIFICADO_POR,
            string FECHA_MODIFICACION)
        {
            colinasEntities db = null;
            try
            {
                usuario user = new usuario();
                user.USR_USERNAME = USR_USERNAME;
                user.USR_NOMBRE = USR_NOMBRE;
                user.USR_APELLIDO = USR_APELLIDO;
                user.USR_CEDULA = USR_CEDULA;
                user.USR_CORREO = USR_CORREO;
                user.USR_PUESTO = USR_PUESTO;
                user.USR_PASSWORD = USR_PASSWORD;
                user.CREADO_POR = CREADO_POR;
                user.FECHA_CREACION = DateTime.Today;
                user.MODIFICADO_POR = CREADO_POR;
                user.FECHA_MODIFICACION = user.FECHA_CREACION;

                db = new colinasEntities();
                db.usuarios.AddObject(user);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                if (db != null)
                    db.Dispose();
                throw;
            }
        }

        #endregion

        #region Update

        public void ActualizarUsuario(string USR_USERNAME,
            string USR_NOMBRE,
            string USR_APELLIDO,
            int USR_CEDULA,
            string USR_CORREO,
            string USR_PUESTO,
            string USR_PASSWORD,
            string CREADO_POR,
            string FECHA_CREACION,
            string MODIFICADO_POR,
            string FECHA_MODIFICACION)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();

                var query = from usr in db.usuarios
                            where usr.USR_USERNAME == USR_USERNAME
                            select usr;

                usuario user = query.First();

                user.USR_USERNAME = USR_USERNAME;
                user.USR_NOMBRE = USR_NOMBRE;
                user.USR_APELLIDO = USR_APELLIDO;
                user.USR_CEDULA = USR_CEDULA;
                user.USR_CORREO = USR_CORREO;
                user.USR_PUESTO = USR_PUESTO;
                user.USR_PASSWORD = USR_PASSWORD;
                user.CREADO_POR = CREADO_POR;
                user.FECHA_CREACION = DateTime.Parse(FECHA_CREACION);
                user.MODIFICADO_POR = MODIFICADO_POR;
                user.FECHA_MODIFICACION = DateTime.Now;

                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                if (db != null)
                    db.Dispose();
                throw;
            }
        }

        #endregion

        #region Delete

        public void EliminarUsuario(string USR_USERNAME)
        {
            colinasEntities db = null;
            try
            {
                db = new colinasEntities();

                var query = from usr in db.usuarios
                            where usr.USR_USERNAME == USR_USERNAME
                            select usr;

                usuario user = query.First();

                db.DeleteObject(user);

                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                if (db != null)
                    db.Dispose();
                throw;
            }
        }

        #endregion
    }
}
