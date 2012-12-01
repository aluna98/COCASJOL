using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Prestamos
{
    public class SolicitudesLogic
    {
        public SolicitudesLogic() { }

    #region Select
        public List<solicitud_prestamo> getData()
        {
            colinasEntities db = new colinasEntities();
            var query = from solicitud in db.solicitudes_prestamos
                        select solicitud;
            return query.ToList<solicitud_prestamo>();
        }
    #endregion
    }
}
