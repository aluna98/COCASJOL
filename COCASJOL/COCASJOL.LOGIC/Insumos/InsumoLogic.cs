using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Insumos
{
    public class InsumoLogic
    {
        public InsumoLogic() { }

        #region Select

        public IQueryable GetInsumos()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.privilegios;
                }
            }
            catch (Exception ex)
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
}
