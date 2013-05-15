using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Utiles;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    /// <summary>
    /// Clase con logica de Nota de Peso en Área de Catación
    /// </summary>
    public class NotaDePesoEnCatacionLogic : NotaDePesoLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(NotaDePesoEnCatacionLogic).Name);

        /// <summary>
        /// Constructor. Inicializa código de estado en area de catación.
        /// </summary>
        public NotaDePesoEnCatacionLogic() : base("CATACION") { }

        #region Select

        /// <summary>
        /// Obtiene todas las notas de peso de socios activos. (overrided)
        /// </summary>
        /// <returns>Lista de notas de peso de socios activos.</returns>
        public override List<nota_de_peso> GetNotasDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from n in db.notas_de_peso.Include("notas_de_peso").Include("socios").Include("clasificaciones_cafe")
                                where n.socios.SOCIOS_ESTATUS >= 1 &&
                                (n.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID)
                                select n;

                    return query.OrderBy(n => n.SOCIOS_ID).ToList<nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener Notas de Peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados de la nota de peso. overrided.
        /// </summary>
        /// <returns>Lista estados de la nota de peso.</returns>
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

                    List<estado_nota_de_peso> estadolist = GetEstadosSiguiente(padre);

                    return estadolist;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /*
         *                  -----Flujo-----
         * 
         * modificar Inventario de Café    
         *      cambiar clasificacion de café a la clasificación actual
         *      verificar si hubo cambio de estado
         *          cambiar estado a nuevo estado
         *          notificar a usuarios
         * 
         */
        /// <summary>
        /// Actualiza la nota de peso (overrided). Guarda la nota de peso y envía las respectivas notificaciones.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="NOTAS_FECHA"></param>
        /// <param name="NOTAS_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_DEFECTO"></param>
        /// <param name="NOTAS_PORCENTAJE_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_SUMA"></param>
        /// <param name="NOTAS_PESO_TARA"></param>
        /// <param name="NOTAS_SACOS_RETENIDOS"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="Detalles"></param>
        /// <param name="NOTA_PORCENTAJEHUMEDADMIN"></param>
        /// <param name="NOTA_TRANSPORTECOOP"></param>
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

                        /* --------Modificar Inventario de Café Actual-------- */
                        // cambiar clasificacion de café a la clasificación actual
                        note.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        note.socios.socios_produccion.CLASIFICACIONES_CAFE_ID = note.CLASIFICACIONES_CAFE_ID; 

                        // verificar si hubo cambio de estado
                        if (ESTADOS_NOTA_ID != this.ESTADOS_NOTA_ID)
                        {
                            // cambiar estado a nuevo estado
                            note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;

                            // notificar a usuarios
                            //this.NotificarUsuarios("NOTASADMINISTRACION", "MANT_NOTASPESO", note, db);
                            string ESTADO_NOTA_LLAVE = note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE;
                            this.NotificarUsuarios(EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + ESTADO_NOTA_LLAVE, EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + ESTADO_NOTA_LLAVE, note, db);
                        }

                        note.MODIFICADO_POR = MODIFICADO_POR;
                        note.FECHA_MODIFICACION = DateTime.Today;

                        db.SaveChanges();

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar nota de peso.", ex);
                throw;
            }
        }

        #endregion
    }
}
