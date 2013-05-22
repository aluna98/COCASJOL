using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Salidas
{
    /// <summary>
    /// Clase con logica de Ajuste Inventario de Cafe de Socios
    /// </summary>
    public class AjusteInventarioDeCafeDeSocioLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(AjusteInventarioDeCafeDeSocioLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public AjusteInventarioDeCafeDeSocioLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todos los ajustes de inventario de café de socios.
        /// </summary>
        /// <returns>Lista de ajustes de inventario de café de socios.</returns>
        public List<ajuste_inventario_cafe_x_socio> GetAjustesDeInventarioDeCafeDeSocio()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    return db.ajustes_inventario_cafe_x_socio.ToList<ajuste_inventario_cafe_x_socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener ajustes de inventario de cafe de socios.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los ajustes de inventario de café de socios.
        /// </summary>
        /// <param name="AJUSTES_INV_CAFE_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="AJUSTES_INV_CAFE_FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="AJUSTES_INV_CAFE_CANTIDAD_LIBRAS"></param>
        /// <param name="AJUSTES_INV_CAFE_PRECIO_LIBRAS"></param>
        /// <param name="AJUSTES_INV_CAFE_SALDO_TOTAL"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <returns>Lista de ajustes de inventario de café de socios.</returns>
        public List<ajuste_inventario_cafe_x_socio> GetAjustesDeInventarioDeCafeDeSocio
            (int AJUSTES_INV_CAFE_ID,
            string SOCIOS_ID,
            string SOCIOS_PRIMER_NOMBRE,
            string SOCIOS_SEGUNDO_NOMBRE,
            string SOCIOS_PRIMER_APELLIDO,
            string SOCIOS_SEGUNDO_APELLIDO,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime AJUSTES_INV_CAFE_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            decimal AJUSTES_INV_CAFE_CANTIDAD_LIBRAS,
            decimal AJUSTES_INV_CAFE_PRECIO_LIBRAS,
            decimal AJUSTES_INV_CAFE_SALDO_TOTAL,
            string CREADO_POR,
            DateTime FECHA_CREACION,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from v in db.ajustes_inventario_cafe_x_socio.Include("socios").Include("clasificaciones_cafe")
                                where
                                (AJUSTES_INV_CAFE_ID == 0 ? true : v.AJUSTES_INV_CAFE_ID.Equals(AJUSTES_INV_CAFE_ID)) &&
                                (string.IsNullOrEmpty(SOCIOS_ID) ? true : v.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                (CLASIFICACIONES_CAFE_ID == 0 ? true : v.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&

                                (default(DateTime) == FECHA_DESDE ? true : v.AJUSTES_INV_CAFE_FECHA >= FECHA_DESDE) &&
                                (default(DateTime) == FECHA_HASTA ? true : v.AJUSTES_INV_CAFE_FECHA <= FECHA_HASTA) &&

                                (AJUSTES_INV_CAFE_CANTIDAD_LIBRAS == -1 ? true : v.AJUSTES_INV_CAFE_PRECIO_LIBRAS.Equals(AJUSTES_INV_CAFE_CANTIDAD_LIBRAS)) &&
                                (AJUSTES_INV_CAFE_PRECIO_LIBRAS == -1 ? true : v.AJUSTES_INV_CAFE_PRECIO_LIBRAS.Equals(AJUSTES_INV_CAFE_PRECIO_LIBRAS)) &&
                                (AJUSTES_INV_CAFE_SALDO_TOTAL == -1 ? true : v.AJUSTES_INV_CAFE_SALDO_TOTAL.Equals(AJUSTES_INV_CAFE_SALDO_TOTAL)) &&

                                (string.IsNullOrEmpty(CREADO_POR) ? true : v.CREADO_POR.Contains(CREADO_POR)) &&
                                (default(DateTime) == FECHA_CREACION ? true : v.FECHA_CREACION == FECHA_CREACION) &&
                                (string.IsNullOrEmpty(MODIFICADO_POR) ? true : v.MODIFICADO_POR.Contains(MODIFICADO_POR)) &&
                                (default(DateTime) == FECHA_MODIFICACION ? true : v.FECHA_MODIFICACION == FECHA_MODIFICACION)
                                select v;

                    return query.ToList<ajuste_inventario_cafe_x_socio>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener ajustes de inventario de cafe de socios.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserta el ajuste de inventario de café de socio.
        /// </summary>
        /// <param name="AJUSTES_INV_CAFE_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_NOMBRE"></param>
        /// <param name="AJUSTES_INV_CAFE_FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="AJUSTES_INV_CAFE_CANTIDAD_LIBRAS"></param>
        /// <param name="AJUSTES_INV_CAFE_PRECIO_LIBRAS"></param>
        /// <param name="AJUSTES_INV_CAFE_SALDO_TOTAL"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        public void InsertarAjusteDeInventarioDeCafeDeSocio
            (int AJUSTES_INV_CAFE_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime AJUSTES_INV_CAFE_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            decimal AJUSTES_INV_CAFE_CANTIDAD_LIBRAS,
            decimal AJUSTES_INV_CAFE_PRECIO_LIBRAS,
            decimal AJUSTES_INV_CAFE_SALDO_TOTAL,
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
                        ajuste_inventario_cafe_x_socio ajuste_cafe = new ajuste_inventario_cafe_x_socio();

                        ajuste_cafe.SOCIOS_ID = SOCIOS_ID;
                        ajuste_cafe.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        ajuste_cafe.AJUSTES_INV_CAFE_FECHA = AJUSTES_INV_CAFE_FECHA;
                        ajuste_cafe.AJUSTES_INV_CAFE_CANTIDAD_LIBRAS = AJUSTES_INV_CAFE_CANTIDAD_LIBRAS;
                        ajuste_cafe.AJUSTES_INV_CAFE_PRECIO_LIBRAS = AJUSTES_INV_CAFE_PRECIO_LIBRAS;

                        ajuste_cafe.AJUSTES_INV_CAFE_SALDO_TOTAL = AJUSTES_INV_CAFE_CANTIDAD_LIBRAS * AJUSTES_INV_CAFE_PRECIO_LIBRAS;

                        ajuste_cafe.CREADO_POR = ajuste_cafe.MODIFICADO_POR = CREADO_POR;
                        ajuste_cafe.FECHA_CREACION = DateTime.Today;
                        ajuste_cafe.FECHA_MODIFICACION = ajuste_cafe.FECHA_CREACION;

                        db.ajustes_inventario_cafe_x_socio.AddObject(ajuste_cafe);

                        db.SaveChanges();

                        InventarioDeCafeLogic inventariodecafelogic = new InventarioDeCafeLogic();
                        inventariodecafelogic.InsertarTransaccionInventarioDeCafeDeSocio(ajuste_cafe, db);

                        db.SaveChanges();

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar ajuste de inventario de cafe de socio.", ex);
                throw;
            }
        }

        #endregion
    }
}
