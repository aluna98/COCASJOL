using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

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
                                where n.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID
                                select n;

                    return query.ToList<nota_de_peso>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override List<nota_de_peso> GetNotasDePeso
            (int NOTAS_ID,
            int ESTADOS_NOTA_ID,
            string ESTADOS_NOTA_NOMBRE,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime NOTAS_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            bool? NOTAS_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_DEFECTO,
            decimal NOTAS_PORCENTAJE_HUMEDAD,
            decimal NOTAS_PESO_DEFECTO,
            decimal NOTAS_PESO_HUMEDAD,
            decimal NOTAS_PESO_DESCUENTO,
            decimal NOTAS_PESO_SUMA,
            decimal NOTAS_PESO_TARA,
            decimal NOTAS_PESO_TOTAL_RECIBIDO,
            string NOTAS_PESO_TOTAL_RECIBIDO_TEXTO,
            int NOTAS_SACOS_RETENIDOS,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    var queryEnPesaje = from notasPesoPesaje in db.notas_de_peso.Include("socios").Include("clasificaciones_cafe").Include("estados_nota_de_peso")
                                        where notasPesoPesaje.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID
                                        select notasPesoPesaje;

                    var query = from notasPeso in queryEnPesaje
                                where
                                (NOTAS_ID.Equals(0) ? true : notasPeso.NOTAS_ID.Equals(NOTAS_ID)) &&
                                (ESTADOS_NOTA_ID.Equals(0) ? true : notasPeso.ESTADOS_NOTA_ID.Equals(ESTADOS_NOTA_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : notasPeso.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : notasPeso.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                (default(DateTime) == FECHA_DESDE ? true : notasPeso.NOTAS_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA ? true : notasPeso.NOTAS_FECHA <= FECHA_HASTA) &&
                                (NOTAS_TRANSPORTE_COOPERATIVA == null ? true : notasPeso.NOTAS_TRANSPORTE_COOPERATIVA == NOTAS_TRANSPORTE_COOPERATIVA) &&
                                (NOTAS_PORCENTAJE_DEFECTO.Equals(-1) ? true : notasPeso.NOTAS_PORCENTAJE_DEFECTO.Equals(NOTAS_PORCENTAJE_DEFECTO)) &&
                                (NOTAS_PORCENTAJE_HUMEDAD.Equals(-1) ? true : notasPeso.NOTAS_PORCENTAJE_HUMEDAD.Equals(NOTAS_PORCENTAJE_HUMEDAD)) &&
                                (NOTAS_PESO_DEFECTO.Equals(-1) ? true : notasPeso.NOTAS_PESO_DEFECTO.Equals(NOTAS_PESO_DEFECTO)) &&
                                (NOTAS_PESO_DESCUENTO.Equals(-1) ? true : notasPeso.NOTAS_PESO_DESCUENTO.Equals(NOTAS_PESO_DESCUENTO)) &&
                                (NOTAS_PESO_HUMEDAD.Equals(-1) ? true : notasPeso.NOTAS_PESO_HUMEDAD.Equals(NOTAS_PESO_HUMEDAD)) &&
                                (NOTAS_PESO_TARA.Equals(-1) ? true : notasPeso.NOTAS_PESO_TARA.Equals(NOTAS_PESO_TARA)) &&
                                (NOTAS_PESO_SUMA.Equals(-1) ? true : notasPeso.NOTAS_PESO_SUMA.Equals(NOTAS_PESO_SUMA)) &&
                                (NOTAS_PESO_TOTAL_RECIBIDO.Equals(-1) ? true : notasPeso.NOTAS_PESO_TOTAL_RECIBIDO.Equals(NOTAS_PESO_TOTAL_RECIBIDO)) &&
                                (string.IsNullOrEmpty(NOTAS_PESO_TOTAL_RECIBIDO_TEXTO) ? true : notasPeso.SOCIOS_ID.Contains(NOTAS_PESO_TOTAL_RECIBIDO_TEXTO)) &&
                                (NOTAS_SACOS_RETENIDOS.Equals(-1) ? true : notasPeso.NOTAS_SACOS_RETENIDOS.Equals(NOTAS_SACOS_RETENIDOS)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : notasPeso.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : notasPeso.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : notasPeso.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : notasPeso.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select notasPeso;

                    return query.ToList<nota_de_peso>();
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

                    var queryPadre = from enp in db.estados_nota_de_peso.Include("estados_nota_de_peso_hijos")
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
         *      --------Modificar Inventario de Café Actual--------
         *      cambiar clasificacion de café a la clasificación actual
         *      intentar obtener el inventario de café actual
         *          si hay inventario de café actual modificarlo
         *          si no hay inventario de café actual crearlo
         * 
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
            Dictionary<string, string> Variables)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    /* --------Modificar Inventario de Café-------- */
                    // verificar si hubo cambio de clasificación
                    if (note.CLASIFICACIONES_CAFE_ID != CLASIFICACIONES_CAFE_ID)
                    {
                        /* --------Modificar Inventario de Café Anterior-------- */
                        IEnumerable<KeyValuePair<string, object>> entityKeyValuesInventarioAnterior =
                            new KeyValuePair<string, object>[] {
                                    new KeyValuePair<string, object>("SOCIOS_ID", note.SOCIOS_ID),
                                    new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", note.CLASIFICACIONES_CAFE_ID) 
                                };

                        EntityKey kInventarioAnterior = new EntityKey("colinasEntities.inventario_cafe_de_socio", entityKeyValuesInventarioAnterior);

                        Object invCafSocAnterior = null;

                        // intentar obtener inventario de café anterior y modificarlo
                        if (db.TryGetObjectByKey(kInventarioAnterior, out invCafSocAnterior))
                        {
                            inventario_cafe_de_socio asocInventoryAnterior = (inventario_cafe_de_socio)invCafSocAnterior;
                            asocInventoryAnterior.INVENTARIO_CANTIDAD -= note.NOTAS_PESO_TOTAL_RECIBIDO;
                            asocInventoryAnterior.MODIFICADO_POR = MODIFICADO_POR;
                            asocInventoryAnterior.FECHA_MODIFICACION = DateTime.Now;
                        }

                        /* --------Modificar Inventario de Café Actual-------- */
                        // cambiar clasificacion de café a la clasificación actual
                        note.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;

                        IEnumerable<KeyValuePair<string, object>> entityKeyValuesInventario = 
                            new KeyValuePair<string, object>[] {
                                new KeyValuePair<string, object>("SOCIOS_ID", note.SOCIOS_ID),
                                new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", note.CLASIFICACIONES_CAFE_ID) 
                            };

                        EntityKey kInventario = new EntityKey("colinasEntities.inventario_cafe_de_socio", entityKeyValuesInventario);

                        Object invCafSoc = null;

                        // intentar obtener el inventario de café actual
                        if (db.TryGetObjectByKey(kInventario, out invCafSoc))
                        {
                            // si hay inventario de café actual modificarlo
                            inventario_cafe_de_socio asocInventory = (inventario_cafe_de_socio)invCafSoc;

                            asocInventory.INVENTARIO_CANTIDAD += note.NOTAS_PESO_TOTAL_RECIBIDO;
                            asocInventory.MODIFICADO_POR = MODIFICADO_POR;
                            asocInventory.FECHA_MODIFICACION = DateTime.Now;
                        }
                        else
                        {
                            // si no hay inventario de café actual crearlo
                            inventario_cafe_de_socio asocInventory = new inventario_cafe_de_socio();
                            asocInventory.SOCIOS_ID = note.SOCIOS_ID;
                            asocInventory.CLASIFICACIONES_CAFE_ID = note.CLASIFICACIONES_CAFE_ID;
                            asocInventory.INVENTARIO_CANTIDAD = note.NOTAS_PESO_TOTAL_RECIBIDO;
                            asocInventory.CREADO_POR = asocInventory.MODIFICADO_POR = MODIFICADO_POR;
                            asocInventory.FECHA_CREACION = DateTime.Now;
                            asocInventory.FECHA_MODIFICACION = asocInventory.FECHA_CREACION;

                            db.inventario_cafe_de_socio.AddObject(asocInventory);
                        }
                    }

                    note.MODIFICADO_POR = MODIFICADO_POR;
                    note.FECHA_MODIFICACION = DateTime.Today;

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
