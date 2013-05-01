using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Entorno
{
    /// <summary>
    /// Clase con logica de Variables de Entorno
    /// </summary>
    public class VariablesDeEntornoLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(VariablesDeEntornoLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public VariablesDeEntornoLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todas las variables de entorno.
        /// </summary>
        /// <returns>Lista de variables de entorno.</returns>
        public List<variable_de_entorno> GetVariablesDeEntorno()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    return db.variables_de_entorno.OrderBy(v => v.VARIABLES_NOMBRE).ToList<variable_de_entorno>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener variables de entorno.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las variables de entorno.
        /// </summary>
        /// <param name="VARIABLES_LLAVE"></param>
        /// <param name="VARIABLES_NOMBRE"></param>
        /// <param name="VARIABLES_DESCRIPCION"></param>
        /// <param name="VARIABLES_VALOR"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de variables de entorno.</returns>
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

                    return db.variables_de_entorno.OrderBy(v => v.VARIABLES_NOMBRE).ToList<variable_de_entorno>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener variables de entorno.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene variable de entorno específica.
        /// </summary>
        /// <param name="VARIABLES_LLAVE"></param>
        /// <returns>Variable de entorno.</returns>
        public variable_de_entorno GetVariableDeEntorno(string VARIABLES_LLAVE, colinasEntities db)
        {
            try
            {
                //using (var db = new colinasEntities())
                //{
                    db.variables_de_entorno.MergeOption = MergeOption.NoTracking;

                    EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", VARIABLES_LLAVE);

                    var env = db.GetObjectByKey(k);

                    variable_de_entorno environmentVariable = (variable_de_entorno)env;

                    return environmentVariable;
                //}
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener variable de entorno.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza todas las variables de entorno.
        /// </summary>
        /// <param name="VariablesDeEntorno"></param>
        /// <param name="MODIFICADO_POR"></param>
        public void ActualizarVariablesDeEntorno(Dictionary<string, string>[] VariablesDeEntorno, string MODIFICADO_POR)
        {
            try
            {
                List<string> variablesActualizadas = new List<string>();
                using (var db = new colinasEntities())
                {
                    DateTime tday = DateTime.Today;

                    foreach (Dictionary<string, string> VariableDeEntorno in VariablesDeEntorno)
                    {
                        EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", VariableDeEntorno["VARIABLES_LLAVE"]);

                        var env = db.GetObjectByKey(k);

                        variable_de_entorno environmentVariables = (variable_de_entorno)env;

                        string valor = VariableDeEntorno["VARIABLES_VALOR"];

                        if (environmentVariables.VARIABLES_VALOR != valor)
                        {
                            environmentVariables.VARIABLES_VALOR = valor;
                            environmentVariables.MODIFICADO_POR = MODIFICADO_POR;
                            environmentVariables.FECHA_MODIFICACION = tday;

                            variablesActualizadas.Add(environmentVariables.VARIABLES_LLAVE);
                        }
                    }

                    db.SaveChanges();
                }
                
                Utiles.PlantillaLogic plantillalogic = new Utiles.PlantillaLogic();
                plantilla_notificacion pl = plantillalogic.GetPlantilla("VARIABLESACTUALIZADAS");
                Utiles.NotificacionLogic notificacionlogic = new Utiles.NotificacionLogic();

                foreach (string varKey in variablesActualizadas)
                {
                    notificacionlogic.NotifyUsers("", Utiles.EstadosNotificacion.Creado, pl.PLANTILLAS_ASUNTO, pl.PLANTILLAS_MENSAJE, varKey);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar variables de entorno.", ex);
                throw;
            }
        }

        #endregion
    }
}
