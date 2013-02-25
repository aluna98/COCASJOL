using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Utiles;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class NotaDePesoEnCatacionLogic : NotaDePesoLogic
    {
        public NotaDePesoEnCatacionLogic() : base("CATACION") { }

        #region Select

        public override List<nota_de_peso> GetNotasDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from n in db.notas_de_peso.Include("notas_de_peso").Include("socios").Include("clasificaciones_cafe")
                                where n.socios.SOCIOS_ESTATUS == 1 &&
                                (n.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID)
                                select n;

                    return query.OrderBy(n => n.SOCIOS_ID).ToList<nota_de_peso>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EstadoNotaDePesoLogic estadoslogic = new EstadoNotaDePesoLogic();

                    var queryPadre = from enp in db.estados_nota_de_peso
                                     where enp.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID
                                     select enp;

                    estado_nota_de_peso padre = queryPadre.First();

                    List<estado_nota_de_peso> estadolist = GetHijos(padre);

                    return estadolist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Update

        /*
         *                  -----Flujo-----
         * 
         * --------Modificar Inventario de Café--------
         * verificar si hubo cambio de clasificación
         *      --------Modificar Inventario de Café Anterior--------
         *      intentar obtener inventario de café anterior y modificarlo
         *      
         *      cambiar clasificacion de café a la clasificación actual
         *      verificar si hubo cambio de estado
         *          cambiar estado a nuevo estado
         *          notificar a usuarios
         * 
         */

        public override void ActualizarNotaDePeso
            (int NOTAS_ID,
            int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            Boolean NOTAS_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_DEFECTO,
            decimal NOTAS_PORCENTAJE_HUMEDAD,
            decimal NOTAS_PESO_SUMA,
            decimal NOTAS_PESO_TARA,
            int NOTAS_SACOS_RETENIDOS,
            string MODIFICADO_POR,
            Dictionary<string, string>[] Detalles,
            decimal NOTA_PORCENTAJEHUMEDADMIN,
            decimal NOTA_TRANSPORTECOOP)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                        var n = db.GetObjectByKey(k);

                        nota_de_peso note = (nota_de_peso)n;

                        /* --------Modificar Inventario de Café-------- */
                        // verificar si hubo cambio de clasificación
                        if (note.CLASIFICACIONES_CAFE_ID != CLASIFICACIONES_CAFE_ID)
                        {
                            /* --------Modificar Inventario de Café Actual-------- */
                            // cambiar clasificacion de café a la clasificación actual
                            note.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        }

                        // verificar si hubo cambio de estado
                        if (ESTADOS_NOTA_ID != this.ESTADOS_NOTA_ID)
                        {
                            // cambiar estado a nuevo estado
                            note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;

                            // notificar a usuarios
                            //this.NotificarUsuarios("NOTASADMINISTRACION", "MANT_NOTASPESO", note, db);
                            string ESTADO_NOTA_LLAVE = note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE;
                            this.NotificarUsuarios("NOTAS" + ESTADO_NOTA_LLAVE, "MANT_NOTASPESOEN" + ESTADO_NOTA_LLAVE, note, db);
                        }

                        note.MODIFICADO_POR = MODIFICADO_POR;
                        note.FECHA_MODIFICACION = DateTime.Today;

                        db.SaveChanges();

                        scope1.Complete();
                    }
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
