using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Entorno
{
    public class VariablesDeEntornoLogic
    {
        public VariablesDeEntornoLogic() { }

        #region Select  

        public List<variable_de_entorno> GetVariablesDeEntorno()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    return db.variables_de_entorno.ToList<variable_de_entorno>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<variable_de_entorno> GetVariablesDeEntorno
            (string VARIABLES_LLAVE,
            string VARIABLES_NOMBRE,
            string VARIABLES_DESCRIPCION,
            string VARIABLES_VALOR,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    var query = from envars in db.variables_de_entorno
                                where
                                (string.IsNullOrEmpty(VARIABLES_LLAVE) ? true : envars.VARIABLES_LLAVE.Contains(VARIABLES_LLAVE)) &&
                                (string.IsNullOrEmpty(VARIABLES_NOMBRE) ? true : envars.VARIABLES_NOMBRE.Contains(VARIABLES_NOMBRE)) &&
                                (string.IsNullOrEmpty(VARIABLES_DESCRIPCION) ? true : envars.VARIABLES_DESCRIPCION.Contains(VARIABLES_DESCRIPCION)) &&
                                (string.IsNullOrEmpty(VARIABLES_VALOR) ? true : envars.VARIABLES_VALOR.Contains(VARIABLES_VALOR)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : envars.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : envars.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : envars.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : envars.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select envars;

                    return query.ToList<variable_de_entorno>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public variable_de_entorno GetVariableDeEntorno(string VARIABLES_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", VARIABLES_LLAVE);

                    var env = db.GetObjectByKey(k);

                    variable_de_entorno environmentVariable = (variable_de_entorno)env;

                    return environmentVariable;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Insert

        public void InsertarVariableDeEntorno
            (string VARIABLES_LLAVE,
            string VARIABLES_NOMBRE,
            string VARIABLES_DESCRIPCION,
            string VARIABLES_VALOR,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    variable_de_entorno environmentVariables = new variable_de_entorno();

                    environmentVariables.VARIABLES_LLAVE = VARIABLES_LLAVE;
                    environmentVariables.VARIABLES_NOMBRE = VARIABLES_NOMBRE;
                    environmentVariables.VARIABLES_DESCRIPCION = VARIABLES_DESCRIPCION;
                    environmentVariables.VARIABLES_VALOR = VARIABLES_VALOR;
                    environmentVariables.CREADO_POR = CREADO_POR;
                    environmentVariables.FECHA_CREACION = DateTime.Today;
                    environmentVariables.MODIFICADO_POR = CREADO_POR;
                    environmentVariables.FECHA_MODIFICACION = environmentVariables.FECHA_CREACION;

                    db.variables_de_entorno.AddObject(environmentVariables);
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

        public void ActualizarVariableDeEntorno
            (string VARIABLES_LLAVE,
            string VARIABLES_NOMBRE,
            string VARIABLES_DESCRIPCION,
            string VARIABLES_VALOR,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", VARIABLES_LLAVE);

                    var env = db.GetObjectByKey(k);

                    variable_de_entorno environmentVariables = (variable_de_entorno)env;

                    environmentVariables.VARIABLES_NOMBRE = VARIABLES_NOMBRE;
                    environmentVariables.VARIABLES_DESCRIPCION = VARIABLES_DESCRIPCION;
                    environmentVariables.VARIABLES_VALOR = VARIABLES_VALOR;
                    environmentVariables.MODIFICADO_POR = MODIFICADO_POR;
                    environmentVariables.FECHA_MODIFICACION = DateTime.Today;

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

        public void EliminarVariableDeEntorno(string VARIABLES_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {

                    EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", VARIABLES_LLAVE);

                    var env = db.GetObjectByKey(k);

                    variable_de_entorno environmentVariables = (variable_de_entorno)env;

                    db.DeleteObject(environmentVariables);

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

        public bool NombreDeVariableDeEntornoExiste(string VARIABLES_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    var query = from env in db.variables_de_entorno
                                where env.VARIABLES_NOMBRE == VARIABLES_NOMBRE
                                select env;

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

        public bool NombreDeVariableDeEntornoExiste(string VARIABLES_LLAVE, string VARIABLES_NOMBRE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", VARIABLES_LLAVE);

                    var envar = db.GetObjectByKey(k);

                    variable_de_entorno environmentVariables = (variable_de_entorno)envar;

                    if (environmentVariables.VARIABLES_NOMBRE == VARIABLES_NOMBRE)
                        return false;// para que pase la validacion

                    var query = from env in db.variables_de_entorno
                                where env.VARIABLES_NOMBRE == VARIABLES_NOMBRE
                                select env;

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

        public bool LlaveDeVariableDeEntornoExiste(string VARIABLES_LLAVE)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    Object u = null;
                    EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", VARIABLES_LLAVE);

                    if (db.TryGetObjectByKey(k, out u))
                        return true;

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
