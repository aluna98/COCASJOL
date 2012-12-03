using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class EstadoNotaDePesoLogic
    {
        public EstadoNotaDePesoLogic() { }

        #region Select

        public List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    return db.estados_nota_de_peso.ToList<estado_nota_de_peso>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<estado_nota_de_peso> GetEstadosNotaDePeso
            (int ESTADOS_NOTA_ID,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from estadosn in db.estados_nota_de_peso
                                where
                                (ESTADOS_NOTA_ID.Equals(0) ? true : estadosn.ESTADOS_NOTA_ID.Equals(ESTADOS_NOTA_ID)) &&
                                (string.IsNullOrEmpty(ESTADOS_NOTA_NOMBRE) ? true : estadosn.ESTADOS_NOTA_NOMBRE.Contains(ESTADOS_NOTA_NOMBRE)) &&
                                (string.IsNullOrEmpty(ESTADOS_NOTA_DESCRIPCION) ? true : estadosn.ESTADOS_NOTA_DESCRIPCION.Contains(ESTADOS_NOTA_DESCRIPCION)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : estadosn.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : estadosn.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : estadosn.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : estadosn.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select estadosn;

                    return query.ToList<estado_nota_de_peso>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarEstadoNotaDePeso
            (int ESTADOS_NOTA_ID,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    estado_nota_de_peso noteStatus = new estado_nota_de_peso();

                    noteStatus.ESTADOS_NOTA_NOMBRE = ESTADOS_NOTA_NOMBRE;
                    noteStatus.ESTADOS_NOTA_DESCRIPCION = ESTADOS_NOTA_DESCRIPCION;
                    noteStatus.CREADO_POR = CREADO_POR;
                    noteStatus.FECHA_CREACION = DateTime.Today;
                    noteStatus.MODIFICADO_POR = CREADO_POR;
                    noteStatus.FECHA_MODIFICACION = noteStatus.FECHA_CREACION;

                    db.estados_nota_de_peso.AddObject(noteStatus);
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

        public void ActualizarEstadoNotaDePeso
            (int ESTADOS_NOTA_ID,
            string ESTADOS_NOTA_NOMBRE,
            string ESTADOS_NOTA_DESCRIPCION,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                    var esn = db.GetObjectByKey(k);

                    estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                    noteStatus.ESTADOS_NOTA_NOMBRE = ESTADOS_NOTA_NOMBRE;
                    noteStatus.ESTADOS_NOTA_DESCRIPCION = ESTADOS_NOTA_DESCRIPCION;
                    noteStatus.MODIFICADO_POR = MODIFICADO_POR;
                    noteStatus.FECHA_MODIFICACION = DateTime.Today;

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

        public void EliminarEstadoNotaDePeso(int ESTADOS_NOTA_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.estados_nota_de_peso", "ESTADOS_NOTA_ID", ESTADOS_NOTA_ID);

                    var esn = db.GetObjectByKey(k);

                    estado_nota_de_peso noteStatus = (estado_nota_de_peso)esn;

                    db.DeleteObject(noteStatus);

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

        public bool NombreDeEstadoNotaDePesoExiste(string ESTADOS_NOTA_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from esn in db.estados_nota_de_peso
                                where esn.ESTADOS_NOTA_NOMBRE == ESTADOS_NOTA_NOMBRE
                                select esn;

                    if (query.Count() > 0)
                        return true;
                    else
                        return false;
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
