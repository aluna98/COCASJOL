using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Aportaciones
{
    /// <summary>
    /// Clase con logica de Retiro de Aportaciones de Socios
    /// </summary>
    public class RetiroAportacionLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(RetiroAportacionLogic).Name);

        /// <summary>
        /// Constructor
        /// </summary>
        public RetiroAportacionLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todos los retiros de aportaciones por socio.
        /// </summary>
        /// <returns>Lista de retiros de aportaciones por Socio.</returns>
        public List<retiro_aportaciones> GetRetirosDeAportaciones()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from ra in db.retiros_aportaciones
                                select ra;

                    return query.OrderBy(ra => ra.SOCIOS_ID).ToList<retiro_aportaciones>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener retiros de aportaciones.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los retiros de aportaciones por socio.
        /// </summary>
        /// <param name="RETIROS_AP_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="RETIROS_AP_FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="RETIROS_AP_ORDINARIA"></param>
        /// <param name="RETIROS_AP_EXTRAORDINARIA"></param>
        /// <param name="RETIROS_AP_CAPITALIZACION_RETENCION"></param>
        /// <param name="RETIROS_AP_INTERESES_S_APORTACION"></param>
        /// <param name="RETIROS_AP_EXCEDENTE_PERIODO"></param>
        /// <param name="RETIROS_AP_TOTAL_RETIRADO"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de retiros de aportaciones por Socio.</returns>
        public List<retiro_aportaciones> GetRetirosDeAportaciones
            (int RETIROS_AP_ID,
            string SOCIOS_ID,
            DateTime RETIROS_AP_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            decimal RETIROS_AP_ORDINARIA,
            decimal RETIROS_AP_EXTRAORDINARIA,
            decimal RETIROS_AP_CAPITALIZACION_RETENCION,
            decimal RETIROS_AP_INTERESES_S_APORTACION,
            decimal RETIROS_AP_EXCEDENTE_PERIODO,
            decimal RETIROS_AP_TOTAL_RETIRADO,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from rp in db.retiros_aportaciones.Include("socios")
                                where
                                (rp.socios.SOCIOS_ESTATUS >= 1) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : rp.SOCIOS_ID.Contains(SOCIOS_ID)) &&

                                (default(DateTime) == FECHA_DESDE ? true : rp.RETIROS_AP_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA ? true : rp.RETIROS_AP_FECHA <= FECHA_HASTA) &&

                                (RETIROS_AP_TOTAL_RETIRADO == -1 ? true : rp.RETIROS_AP_TOTAL_RETIRADO.Equals(RETIROS_AP_TOTAL_RETIRADO)) &&
                                (string.IsNullOrEmpty(CREADO_POR) ? true : rp.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : rp.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : rp.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : rp.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select rp;

                    return query.OrderBy(rp => rp.SOCIOS_ID).OrderByDescending(rp => rp.FECHA_CREACION).ToList<retiro_aportaciones>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener retiros de aportaciones.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserta el retiro de aportacion.
        /// </summary>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="RETIROS_AP_FECHA"></param>
        /// <param name="RETIROS_AP_ORDINARIA"></param>
        /// <param name="RETIROS_AP_EXTRAORDINARIA"></param>
        /// <param name="RETIROS_AP_CAPITALIZACION_RETENCION"></param>
        /// <param name="RETIROS_AP_INTERESES_S_APORTACION"></param>
        /// <param name="RETIROS_AP_EXCEDENTE_PERIODO"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        public void InsertarRetiroDeAportaciones
            (string SOCIOS_ID,
            DateTime RETIROS_AP_FECHA,
             decimal RETIROS_AP_ORDINARIA,
             decimal RETIROS_AP_EXTRAORDINARIA,
            decimal RETIROS_AP_CAPITALIZACION_RETENCION,
            decimal RETIROS_AP_INTERESES_S_APORTACION,
            decimal RETIROS_AP_EXCEDENTE_PERIODO,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        retiro_aportaciones retiro_aportacion = new retiro_aportaciones();

                        retiro_aportacion.SOCIOS_ID = SOCIOS_ID;
                        retiro_aportacion.RETIROS_AP_FECHA = RETIROS_AP_FECHA;

                        retiro_aportacion.RETIROS_AP_ORDINARIA = RETIROS_AP_ORDINARIA;
                        retiro_aportacion.RETIROS_AP_EXTRAORDINARIA = RETIROS_AP_EXTRAORDINARIA;
                        retiro_aportacion.RETIROS_AP_CAPITALIZACION_RETENCION = RETIROS_AP_CAPITALIZACION_RETENCION;
                        retiro_aportacion.RETIROS_AP_INTERESES_S_APORTACION = RETIROS_AP_INTERESES_S_APORTACION;
                        retiro_aportacion.RETIROS_AP_EXCEDENTE_PERIODO = RETIROS_AP_INTERESES_S_APORTACION;

                        retiro_aportacion.RETIROS_AP_TOTAL_RETIRADO =
                            RETIROS_AP_ORDINARIA +
                            RETIROS_AP_EXTRAORDINARIA +
                            RETIROS_AP_CAPITALIZACION_RETENCION +
                            RETIROS_AP_INTERESES_S_APORTACION +
                            RETIROS_AP_INTERESES_S_APORTACION;

                        retiro_aportacion.CREADO_POR = retiro_aportacion.MODIFICADO_POR = CREADO_POR;
                        retiro_aportacion.FECHA_CREACION = DateTime.Today;
                        retiro_aportacion.FECHA_MODIFICACION = retiro_aportacion.FECHA_CREACION;

                        db.retiros_aportaciones.AddObject(retiro_aportacion);

                        db.SaveChanges();

                        AportacionLogic aportacionlogic = new AportacionLogic();
                        aportacionlogic.InsertarTransaccionAportacionesDeSocio(retiro_aportacion, db);

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar retiro de aportaciones.", ex);
                throw;
            }
        }

        #endregion
    }
}
