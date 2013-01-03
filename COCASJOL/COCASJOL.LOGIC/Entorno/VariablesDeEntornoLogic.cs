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

        #region Update

        public void ActualizarVariablesDeEntorno(Dictionary<string, string> VariablesDeEntorno, string MODIFICADO_POR)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    DateTime tday = DateTime.Today;

                    foreach (KeyValuePair<string, string> Variable in VariablesDeEntorno)
                    {
                        EntityKey k = new EntityKey("colinasEntities.variables_de_entorno", "VARIABLES_LLAVE", Variable.Key);

                        var env = db.GetObjectByKey(k);

                        variable_de_entorno environmentVariables = (variable_de_entorno)env;

                        environmentVariables.VARIABLES_VALOR = Variable.Value;
                        environmentVariables.MODIFICADO_POR = MODIFICADO_POR;
                        environmentVariables.FECHA_MODIFICACION = tday;
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
