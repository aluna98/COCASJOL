using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class NotaDePesoEnAdministracionLogic : NotaDePesoLogic
    {

        public NotaDePesoEnAdministracionLogic() : base("ADMINISTRACION") { }

        public override List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EstadoNotaDePesoLogic estadoslogic = new EstadoNotaDePesoLogic();

                    var queryPadre = from enp in db.estados_nota_de_peso.Include("estados_nota_de_peso_hijos")
                                     where enp.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID
                                     select enp;

                    estado_nota_de_peso padre = queryPadre.First();
                    List<estado_nota_de_peso> estadolist = this.GetHijos(padre);

                    return estadolist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
