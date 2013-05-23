using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.DATAACCESS;
using COCASJOL.LOGIC.Utiles;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    /// <summary>
    /// Clase con logica de Nota de Peso
    /// </summary>
    public class NotaDePesoLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(NotaDePesoLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotaDePesoLogic() { }

        #region Select

        /// <summary>
        /// Obtiene todas las notas de peso de socios activos.
        /// </summary>
        /// <returns>Lista de notas de peso de socios activos.</returns>
        public List<nota_de_peso> GetNotasDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    var query = from n in db.notas_de_peso.Include("notas_de_peso").Include("socios").Include("clasificaciones_cafe")
                                where n.estados_nota_de_peso.estados_detalles.ESTADOS_DETALLE_ENABLE_SOCIO_ID == (int)Socios.SociosLogic.HabilitarSocios.MostrarTodos ? true : n.socios.SOCIOS_ESTATUS >= 1
                                select n;

                    return query.OrderBy(n => n.SOCIOS_ID).ToList<nota_de_peso>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los detalles de nota de peso específica.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <returns>Lista de detalles de nota de peso específica.</returns>
        public List<nota_detalle> GetDetalleNotaDePeso(int NOTAS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    return note.notas_detalles.OrderByDescending(nd => nd.DETALLES_PESO).ToList<nota_detalle>();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener detalles de notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las notas de peso.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="SOCIOS_NOMBRE_COMPLETO"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="NOTAS_FECHA"></param>
        /// <param name="FECHA_DESDE"></param>
        /// <param name="FECHA_HASTA"></param>
        /// <param name="LoggedUser"></param>
        /// <returns>Lista de notas de peso.</returns>
        public List<nota_de_peso> GetNotasDePeso
            (int NOTAS_ID,
            int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            string SOCIOS_NOMBRE_COMPLETO,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            string LoggedUser)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    var queryEnPesaje = from notasPesoPesaje in db.notas_de_peso.Include("socios").Include("clasificaciones_cafe").Include("estados_nota_de_peso")
                                        where (notasPesoPesaje.estados_nota_de_peso.estados_detalles.ESTADOS_DETALLE_ENABLE_SOCIO_ID == (int)COCASJOL.LOGIC.Socios.SociosLogic.HabilitarSocios.MostrarTodos ? true : notasPesoPesaje.socios.SOCIOS_ESTATUS >= 1) &&
                                        (string.IsNullOrEmpty(SOCIOS_NOMBRE_COMPLETO) ? true : (notasPesoPesaje.socios.SOCIOS_PRIMER_NOMBRE + notasPesoPesaje.socios.SOCIOS_SEGUNDO_NOMBRE + notasPesoPesaje.socios.SOCIOS_PRIMER_APELLIDO + notasPesoPesaje.socios.SOCIOS_SEGUNDO_APELLIDO).Contains(SOCIOS_NOMBRE_COMPLETO))
                                        select notasPesoPesaje;

                    if (LoggedUser != "DEVELOPER")
                    {
                        COCASJOL.LOGIC.Seguridad.UsuarioLogic usuariologic = new Seguridad.UsuarioLogic();
                        List<privilegio> privilegios = usuariologic.GetPrivilegiosParaNotaDePesoDeUsuario(LoggedUser);

                        List<string> privLlaves = privilegios.Select(p => p.PRIV_LLAVE).ToList<string>();

                        var querySeguridad = from notasSeguridad in queryEnPesaje
                                             where privLlaves.Contains(EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + notasSeguridad.estados_nota_de_peso.ESTADOS_NOTA_LLAVE)
                                             select notasSeguridad;

                        var query = from notasPeso in querySeguridad
                                    where
                                    (NOTAS_ID.Equals(0) ? true : notasPeso.NOTAS_ID.Equals(NOTAS_ID)) &&
                                    (ESTADOS_NOTA_ID.Equals(0) ? true : notasPeso.ESTADOS_NOTA_ID.Equals(ESTADOS_NOTA_ID)) &&
                                    (string.IsNullOrEmpty(SOCIOS_ID) ? true : notasPeso.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                    (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : notasPeso.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                    (default(DateTime) == FECHA_DESDE ? true : notasPeso.NOTAS_FECHA >= FECHA_DESDE) &&
                                    (default(DateTime) == FECHA_HASTA ? true : notasPeso.NOTAS_FECHA <= FECHA_HASTA)
                                    select notasPeso;

                        return query.OrderBy(n => n.SOCIOS_ID).OrderByDescending(n => n.FECHA_MODIFICACION).OrderByDescending(n => n.NOTAS_FECHA).ToList<nota_de_peso>();
                    }
                    else
                    {

                        var query = from notasPeso in queryEnPesaje
                                    where
                                    (NOTAS_ID.Equals(0) ? true : notasPeso.NOTAS_ID.Equals(NOTAS_ID)) &&
                                    (ESTADOS_NOTA_ID.Equals(0) ? true : notasPeso.ESTADOS_NOTA_ID.Equals(ESTADOS_NOTA_ID)) &&
                                    (string.IsNullOrEmpty(SOCIOS_ID) ? true : notasPeso.SOCIOS_ID.Contains(SOCIOS_ID)) &&
                                    (CLASIFICACIONES_CAFE_ID.Equals(0) ? true : notasPeso.CLASIFICACIONES_CAFE_ID.Equals(CLASIFICACIONES_CAFE_ID)) &&
                                    (default(DateTime) == FECHA_DESDE ? true : notasPeso.NOTAS_FECHA >= FECHA_DESDE) &&
                                    (default(DateTime) == FECHA_HASTA ? true : notasPeso.NOTAS_FECHA <= FECHA_HASTA)
                                    select notasPeso;

                        return query.OrderBy(n => n.SOCIOS_ID).OrderByDescending(n => n.FECHA_MODIFICACION).OrderByDescending(n => n.NOTAS_FECHA).ToList<nota_de_peso>();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener notas de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados para la nota de peso.
        /// </summary>
        /// <returns>Lista de estados de nota de peso.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                EstadoNotaDePesoLogic estadosnotadepesologic = new EstadoNotaDePesoLogic();
                return estadosnotadepesologic.GetEstadosNotaDePeso();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados de la nota de peso siguientes.
        /// </summary>
        /// <returns>Lista estados de la nota de peso siguientes.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePeso(int ESTADOS_NOTA_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var querySiguiente = from enp in db.estados_nota_de_peso.Include("estados_detalles")
                                         where enp.ESTADOS_NOTA_ID == ESTADOS_NOTA_ID
                                         select enp;

                    estado_nota_de_peso siguiente = querySiguiente.First();

                    List<estado_nota_de_peso> estadolist = null;

                    switch (siguiente.estados_detalles.ESTADOS_DETALLE_ENABLE_ESTADO)
                    {
                        case (int)EstadoNotaDePesoLogic.HabilitarEstadosEnNotasDePeso.ActivadoEnOrden:
                            estadolist = GetEstadosSiguiente(siguiente, db);
                            break;

                        case (int)EstadoNotaDePesoLogic.HabilitarEstadosEnNotasDePeso.ActivadoLibre:
                            estadolist = db.estados_nota_de_peso.Include("estados_detalles").ToList<estado_nota_de_peso>();
                            break;

                        default:
                            estadolist = new List<estado_nota_de_peso>();
                            break;
                    }

                    return estadolist;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados de la nota de peso siguientes. Segun clasificacion de café.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <returns>Lista estados de la nota de peso siguientes. Segun clasificacion de café.</returns>
        public List<estado_nota_de_peso> GetEstadosNotaDePeso(int ESTADOS_NOTA_ID, int CLASIFICACIONES_CAFE_ID)
        {
            try
            {
                List<estado_nota_de_peso> estadosList = null;

                using (var db = new colinasEntities())
                {
                    var querySiguiente = from enp in db.estados_nota_de_peso.Include("estados_detalles")
                                         where enp.ESTADOS_NOTA_ID == ESTADOS_NOTA_ID
                                         select enp;

                    estado_nota_de_peso siguiente = querySiguiente.First();

                    switch (siguiente.estados_detalles.ESTADOS_DETALLE_ENABLE_ESTADO)
                    {
                        case (int)EstadoNotaDePesoLogic.HabilitarEstadosEnNotasDePeso.ActivadoEnOrden:
                            estadosList = GetEstadosSiguiente(siguiente, db);
                            break;

                        default:
                            estadosList = db.estados_nota_de_peso.Include("estados_detalles").ToList<estado_nota_de_peso>();
                            break;
                    }
                }

                ClasificacionDeCafeLogic clasificaciondecafelogic = new ClasificacionDeCafeLogic();
                bool catacionFlag = clasificaciondecafelogic.ClasificacionDeCafePasaPorCatacion(CLASIFICACIONES_CAFE_ID);

                if (catacionFlag == false)
                    estadosList = estadosList.Where(esn => esn.ESTADOS_NOTA_ES_CATACION == false).ToList<estado_nota_de_peso>();

                return estadosList;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados de nota de peso.", ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los estados de nota de peso siguientes.
        /// </summary>
        /// <param name="siguiente"></param>
        /// <param name="db"></param>
        /// <returns>Lista de estados de nota de peso siguientes.</returns>
        public List<estado_nota_de_peso> GetEstadosSiguiente(estado_nota_de_peso siguiente, colinasEntities db)
        {
            try
            {
                db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                var query = from esn in db.estados_nota_de_peso.Include("estados_detalles")
                            where siguiente.ESTADOS_NOTA_SIGUIENTE == esn.ESTADOS_NOTA_ID &&
                                  siguiente.ESTADOS_NOTA_ID != esn.ESTADOS_NOTA_ID
                            select esn;

                var siguientes = query.ToList<estado_nota_de_peso>();
                siguientes.Add(siguiente);

                return siguientes;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener estados siguientes de nota de peso.", ex);
                throw;
            }
        }
        
        /// <summary>
        /// Obtiene los socios segun estado de nota de peso.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <returns>Los socios segun estado de nota de peso.</returns>
        public List<socio> GetSocios(int ESTADOS_NOTA_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var query = from enp in db.estados_nota_de_peso.Include("estados_detalles")
                                         where enp.ESTADOS_NOTA_ID == ESTADOS_NOTA_ID
                                         select enp;

                    estado_nota_de_peso estado = query.First();

                    Socios.SociosLogic sociologic = new Socios.SociosLogic();

                    List<socio> sociosList = null;

                    switch (estado.estados_detalles.ESTADOS_DETALLE_ENABLE_SOCIO_ID)
                    {
                        case (int)Socios.SociosLogic.HabilitarSocios.MostrarActivos:
                            sociosList = sociologic.getSociosActivos();
                            break;

                        default:
                            sociosList = sociologic.getData();
                            break;
                    }

                    return sociosList;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener socios de nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Insert

        /*
         *                  -----Calculos-----
         * 
         *                     Tara = Peso de los sacos
         *               Peso bruto = Peso total de café
         * 
         *                % Defecto = (Peso de  muestra) / (Peso de gramos malos)
         *    Descuento por Defecto = ((Peso Bruto) - Tara) * (% Defecto)
         * 
         *                % Humedad = Valor devuelto por maquina?
         *    Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
         * 
         * % Transporte Cooperativa = (Indicado por variable de entorno) * (1-0)
         * Descuento por Transporte = ((Peso Bruto) - Tara) * (% Transporte Cooperativa)
         * 
         *                Descuento = (Descuento por Defecto) + (Descuento por Humedad) + (Descuento por Transporte)
         *                    Total = (Peso Bruto) - Tara - Descuento
         * 
         */
        /// <summary>
        /// Inserta la nota de peso I. Esta fase recalcula los montos y totales para guardar la nota de peso.
        /// </summary>
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
        /// <param name="CREADO_POR"></param>
        /// <param name="Detalles"></param>
        /// <param name="NOTA_PORCENTAJEHUMEDADMIN"></param>
        /// <param name="NOTA_TRANSPORTECOOP"></param>
        public void InsertarNotaDePeso
            (int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            Boolean NOTAS_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_DEFECTO,
            decimal NOTAS_PORCENTAJE_HUMEDAD,
            decimal NOTAS_PESO_SUMA,
            decimal NOTAS_PESO_TARA,
            int NOTAS_SACOS_RETENIDOS,
            string CREADO_POR,
            Dictionary<string, string>[] Detalles,
            decimal NOTA_PORCENTAJEHUMEDADMIN,
            decimal NOTA_TRANSPORTECOOP)
        {
            try
            {
                decimal peso_suma = 0;
                foreach (Dictionary<string, string> detalle in Detalles)
                {
                    decimal det_peso = Convert.ToDecimal(detalle["DETALLES_PESO"]);
                    peso_suma += det_peso;
                }

                NOTAS_PESO_SUMA = peso_suma;

                // Descuento por Defecto = ((Peso Bruto) - Tara) * (% Defecto)
                NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO / 100;
                decimal DESCUENTO_POR_DEFECTO = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_DEFECTO);

                // Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
                if (NOTAS_PORCENTAJE_HUMEDAD < NOTA_PORCENTAJEHUMEDADMIN)
                    NOTAS_PORCENTAJE_HUMEDAD = 0;

                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD / 100;

                decimal DESCUENTO_POR_HUMEDAD = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_HUMEDAD);

                // Descuento por Transporte = ((Peso Bruto) - Tara) * (% Transporte Cooperativa)
                decimal NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA = 0;
                decimal NOTAS_PORCENTAJE_TRANSPORTE = 0;
                if (NOTAS_TRANSPORTE_COOPERATIVA == true)
                {
                    NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA = NOTA_TRANSPORTECOOP;
                    NOTAS_PORCENTAJE_TRANSPORTE = NOTA_TRANSPORTECOOP / 100;
                }

                decimal DESCUENTO_POR_TRANSPORTE = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_TRANSPORTE);

                // Descuento = (Descuento por Defecto) + (Descuento por Humedad) + (Descuento por Transporte)
                decimal DESCUENTO = System.Math.Round(DESCUENTO_POR_DEFECTO + DESCUENTO_POR_HUMEDAD + DESCUENTO_POR_TRANSPORTE);

                // Total = (Peso Bruto) - Tara - Descuento
                decimal TOTAL = System.Math.Round(NOTAS_PESO_SUMA - NOTAS_PESO_TARA - DESCUENTO);

                string localization = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasLocalizacion");

                COCASJOL.LOGIC.Utiles.Numalet cq = new COCASJOL.LOGIC.Utiles.Numalet();
                cq.SeparadorDecimalSalida = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasSeparadorDecimalSalida");
                cq.MascaraSalidaDecimal = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasMascaraSalidaDecimal");
                cq.ConvertirDecimales = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasConvertirDecimales"));
                cq.LetraCapital = true;

                //string TOTAL_TEXTO = COCASJOL.LOGIC.Utiles.Numalet.ToCardinal(TOTAL.ToString(), new System.Globalization.CultureInfo(localization));

                string TOTAL_TEXTO = cq.ToCustomCardinal((TOTAL / 100).ToString());

                // Convertir Porcentaje a entero de nuevo para guardar.
                NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO * 100;
                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD * 100;

                InsertarNotaDePeso
                    (ESTADOS_NOTA_ID,
                    SOCIOS_ID,
                    CLASIFICACIONES_CAFE_ID,
                    NOTAS_FECHA,
                    NOTAS_TRANSPORTE_COOPERATIVA,
                    NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA,
                    NOTAS_PORCENTAJE_DEFECTO,
                    NOTAS_PORCENTAJE_HUMEDAD,
                    DESCUENTO_POR_TRANSPORTE,
                    DESCUENTO_POR_DEFECTO,
                    DESCUENTO_POR_HUMEDAD,
                    DESCUENTO,
                    NOTAS_PESO_SUMA,
                    NOTAS_PESO_TARA,
                    TOTAL,
                    TOTAL_TEXTO,
                    NOTAS_SACOS_RETENIDOS,
                    CREADO_POR,
                    DateTime.Today,
                    Detalles);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar nota de peso. Calculos.", ex);
                throw;
            }
        }


        /*
         *                  -----Flujo-----
         *  verificar si hubo cambio de estado
         *  cambiar estado a nuevo estado
         *  notificar a usuarios
         *
         */
        /// <summary>
        /// Inserta la nota de peso II. Esta fase guarda la nota de peso y envía las respectivas notificaciones.
        /// </summary>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="NOTAS_FECHA"></param>
        /// <param name="NOTAS_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_DEFECTO"></param>
        /// <param name="NOTAS_PORCENTAJE_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PESO_DEFECTO"></param>
        /// <param name="NOTAS_PESO_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_DESCUENTO"></param>
        /// <param name="NOTAS_PESO_SUMA"></param>
        /// <param name="NOTAS_PESO_TARA"></param>
        /// <param name="NOTAS_PESO_TOTAL_RECIBIDO"></param>
        /// <param name="NOTAS_PESO_TOTAL_RECIBIDO_TEXTO"></param>
        /// <param name="NOTAS_SACOS_RETENIDOS"></param>
        /// <param name="CREADO_POR"></param>
        /// <param name="FECHA_CREACION"></param>
        /// <param name="Detalles"></param>
        private void InsertarNotaDePeso
            (int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            Boolean NOTAS_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_DEFECTO,
            decimal NOTAS_PORCENTAJE_HUMEDAD,
            decimal NOTAS_PESO_TRANSPORTE_COOPERATIVA,
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
            Dictionary<string, string>[] Detalles)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        nota_de_peso note = new nota_de_peso();

                        note.SOCIOS_ID = SOCIOS_ID;
                        note.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        note.NOTAS_FECHA = NOTAS_FECHA;
                        note.NOTAS_TRANSPORTE_COOPERATIVA = NOTAS_TRANSPORTE_COOPERATIVA;
                        note.NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA = NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA;
                        note.NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO;
                        note.NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD;
                        note.NOTAS_PESO_TRANSPORTE_COOPERATIVA = NOTAS_PESO_TRANSPORTE_COOPERATIVA;
                        note.NOTAS_PESO_DEFECTO = NOTAS_PESO_DEFECTO;
                        note.NOTAS_PESO_HUMEDAD = NOTAS_PESO_HUMEDAD;
                        note.NOTAS_PESO_DESCUENTO = NOTAS_PESO_DESCUENTO;
                        note.NOTAS_PESO_TARA = NOTAS_PESO_TARA;
                        note.NOTAS_PESO_SUMA = NOTAS_PESO_SUMA;
                        note.NOTAS_PESO_TOTAL_RECIBIDO = NOTAS_PESO_TOTAL_RECIBIDO;
                        note.NOTAS_PESO_TOTAL_RECIBIDO_TEXTO = NOTAS_PESO_TOTAL_RECIBIDO_TEXTO;
                        note.NOTAS_SACOS_RETENIDOS = NOTAS_SACOS_RETENIDOS;
                        note.CREADO_POR = CREADO_POR;
                        note.FECHA_CREACION = FECHA_CREACION;
                        note.MODIFICADO_POR = CREADO_POR;
                        note.FECHA_MODIFICACION = FECHA_CREACION;

                        note.notas_detalles.Clear();

                        foreach (Dictionary<string, string> detalle in Detalles)
                            note.notas_detalles.Add(new nota_detalle() { DETALLES_PESO = Convert.ToDecimal(detalle["DETALLES_PESO"]), DETALLES_CANTIDAD_SACOS = Convert.ToInt32(detalle["DETALLES_CANTIDAD_SACOS"]) });

                        db.notas_de_peso.AddObject(note);

                        db.SaveChanges();

                        // verificar si hubo cambio de estado
                        if (note.ESTADOS_NOTA_ID != ESTADOS_NOTA_ID)
                        {
                            note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;
                            // notificar a usuarios
                            //this.NotificarUsuarios("NOTASCATACION", "MANT_NOTASPESOENCATACION", note, db);

                            string ESTADO_NOTA_LLAVE = note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE;
                            this.NotificarUsuarios(EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + ESTADO_NOTA_LLAVE, EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + ESTADO_NOTA_LLAVE, note, db);
                        }

                        scope1.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al insertar nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Update

        /*
         *                  -----Calculos-----
         * 
         *                     Tara = Peso de los sacos
         *               Peso bruto = Peso total de café
         * 
         *                % Defecto = (Peso de  muestra) / (Peso de gramos malos)
         *    Descuento por Defecto = ((Peso Bruto) - Tara) * (% Defecto)
         * 
         *                % Humedad = Valor devuelto por maquina?
         *    Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
         * 
         * % Transporte Cooperativa = (Indicado por variable de entorno) * (1-0)
         * Descuento por Transporte = ((Peso Bruto) - Tara) * (% Transporte Cooperativa)
         * 
         *                Descuento = (Descuento por Defecto) + (Descuento por Humedad) + (Descuento por Transporte)
         *                    Total = (Peso Bruto) - Tara - Descuento
         * 
         */

        /// <summary>
        /// Actualiza la nota de peso I. Esta fase recalcula los montos y totales para guardar la nota de peso.
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
        public void ActualizarNotaDePeso
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
                decimal peso_suma = 0;
                foreach (Dictionary<string, string> detalle in Detalles)
                {
                    decimal det_peso = Convert.ToDecimal(detalle["DETALLES_PESO"]);
                    peso_suma += det_peso;
                }

                NOTAS_PESO_SUMA = peso_suma;

                // Descuento por Defecto = ((Peso Bruto) - Tara) * (% Defecto)
                NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO / 100;
                decimal DESCUENTO_POR_DEFECTO = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_DEFECTO);

                // Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
                if (NOTAS_PORCENTAJE_HUMEDAD < NOTA_PORCENTAJEHUMEDADMIN)
                    NOTAS_PORCENTAJE_HUMEDAD = 0;

                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD / 100;

                decimal DESCUENTO_POR_HUMEDAD = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_HUMEDAD);

                // Descuento por Transporte = ((Peso Bruto) - Tara) * (% Transporte Cooperativa)
                decimal NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA = 0;
                decimal NOTAS_PORCENTAJE_TRANSPORTE = 0;
                if (NOTAS_TRANSPORTE_COOPERATIVA == true)
                {
                    NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA = NOTA_TRANSPORTECOOP;
                    NOTAS_PORCENTAJE_TRANSPORTE = NOTA_TRANSPORTECOOP / 100;
                }

                decimal DESCUENTO_POR_TRANSPORTE = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_TRANSPORTE);

                // Descuento = (Descuento por Defecto) + (Descuento por Humedad) + (Descuento por Transporte)
                decimal DESCUENTO = System.Math.Round(DESCUENTO_POR_DEFECTO + DESCUENTO_POR_HUMEDAD + DESCUENTO_POR_TRANSPORTE);

                // Total = (Peso Bruto) - Tara - Descuento
                decimal TOTAL = System.Math.Round(NOTAS_PESO_SUMA - NOTAS_PESO_TARA - DESCUENTO);

                string localization = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasLocalizacion");

                COCASJOL.LOGIC.Utiles.Numalet cq = new COCASJOL.LOGIC.Utiles.Numalet();
                cq.SeparadorDecimalSalida = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasSeparadorDecimalSalida");
                cq.MascaraSalidaDecimal = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasMascaraSalidaDecimal");
                cq.ConvertirDecimales = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasConvertirDecimales"));
                cq.LetraCapital = true;

                string TOTAL_TEXTO = cq.ToCustomCardinal((TOTAL / 100).ToString());

                // Convertir Porcentaje a entero de nuevo para guardar.
                NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO * 100;
                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD * 100;

                ActualizarNotaDePeso
                    (NOTAS_ID,
                    ESTADOS_NOTA_ID,
                    SOCIOS_ID,
                    CLASIFICACIONES_CAFE_ID,
                    NOTAS_FECHA,
                    NOTAS_TRANSPORTE_COOPERATIVA,
                    NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA,
                    NOTAS_PORCENTAJE_DEFECTO,
                    NOTAS_PORCENTAJE_HUMEDAD,
                    DESCUENTO_POR_TRANSPORTE,
                    DESCUENTO_POR_DEFECTO,
                    DESCUENTO_POR_HUMEDAD,
                    DESCUENTO,
                    NOTAS_PESO_SUMA,
                    NOTAS_PESO_TARA,
                    TOTAL,
                    TOTAL_TEXTO,
                    NOTAS_SACOS_RETENIDOS,
                    MODIFICADO_POR,
                    DateTime.Today,
                    Detalles);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar nota de peso. Calculos.", ex);
                throw;
            }
        }

        /*
         *                  -----Flujo-----
         *  cambiar clasificacion de café a la clasificación actual
         *  verificar si hubo cambio de estado
         *  cambiar estado a nuevo estado
         *  notificar a usuarios
         *
         */

        /// <summary>
        /// Actualiza la nota de peso II. Esta fase guarda la nota de peso y envía las respectivas notificaciones.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <param name="ESTADOS_NOTA_ID"></param>
        /// <param name="SOCIOS_ID"></param>
        /// <param name="CLASIFICACIONES_CAFE_ID"></param>
        /// <param name="NOTAS_FECHA"></param>
        /// <param name="NOTAS_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PORCENTAJE_DEFECTO"></param>
        /// <param name="NOTAS_PORCENTAJE_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_TRANSPORTE_COOPERATIVA"></param>
        /// <param name="NOTAS_PESO_DEFECTO"></param>
        /// <param name="NOTAS_PESO_HUMEDAD"></param>
        /// <param name="NOTAS_PESO_DESCUENTO"></param>
        /// <param name="NOTAS_PESO_SUMA"></param>
        /// <param name="NOTAS_PESO_TARA"></param>
        /// <param name="NOTAS_PESO_TOTAL_RECIBIDO"></param>
        /// <param name="NOTAS_PESO_TOTAL_RECIBIDO_TEXTO"></param>
        /// <param name="NOTAS_SACOS_RETENIDOS"></param>
        /// <param name="MODIFICADO_POR"></param>
        /// <param name="FECHA_MODIFICACION"></param>
        /// <param name="Detalles"></param>
        private void ActualizarNotaDePeso
            (int NOTAS_ID,
            int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            Boolean NOTAS_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PORCENTAJE_DEFECTO,
            decimal NOTAS_PORCENTAJE_HUMEDAD,
            decimal NOTAS_PESO_TRANSPORTE_COOPERATIVA,
            decimal NOTAS_PESO_DEFECTO,
            decimal NOTAS_PESO_HUMEDAD,
            decimal NOTAS_PESO_DESCUENTO,
            decimal NOTAS_PESO_SUMA,
            decimal NOTAS_PESO_TARA,
            decimal NOTAS_PESO_TOTAL_RECIBIDO,
            string NOTAS_PESO_TOTAL_RECIBIDO_TEXTO,
            int NOTAS_SACOS_RETENIDOS,
            string MODIFICADO_POR,
            DateTime FECHA_MODIFICACION,
            Dictionary<string, string>[] Detalles)
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

                        note.SOCIOS_ID = SOCIOS_ID;
                        note.NOTAS_FECHA = NOTAS_FECHA;
                        note.NOTAS_TRANSPORTE_COOPERATIVA = NOTAS_TRANSPORTE_COOPERATIVA;
                        note.NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA = NOTAS_PORCENTAJE_TRANSPORTE_COOPERATIVA;
                        note.NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO;
                        note.NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD;
                        note.NOTAS_PESO_TRANSPORTE_COOPERATIVA = NOTAS_PESO_TRANSPORTE_COOPERATIVA;
                        note.NOTAS_PESO_DEFECTO = NOTAS_PESO_DEFECTO;
                        note.NOTAS_PESO_HUMEDAD = NOTAS_PESO_HUMEDAD;
                        note.NOTAS_PESO_DESCUENTO = NOTAS_PESO_DESCUENTO;
                        note.NOTAS_PESO_TARA = NOTAS_PESO_TARA;
                        note.NOTAS_PESO_SUMA = NOTAS_PESO_SUMA;
                        note.NOTAS_PESO_TOTAL_RECIBIDO = NOTAS_PESO_TOTAL_RECIBIDO;
                        note.NOTAS_PESO_TOTAL_RECIBIDO_TEXTO = NOTAS_PESO_TOTAL_RECIBIDO_TEXTO;
                        note.NOTAS_SACOS_RETENIDOS = NOTAS_SACOS_RETENIDOS;
                        note.MODIFICADO_POR = MODIFICADO_POR;
                        note.FECHA_MODIFICACION = FECHA_MODIFICACION;

                        // cambiar clasificacion de café a la clasificación actual
                        note.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;

                        note.notas_detalles.Clear();

                        foreach (Dictionary<string, string> detalle in Detalles)
                            note.notas_detalles.Add(new nota_detalle() { DETALLES_PESO = Convert.ToDecimal(detalle["DETALLES_PESO"]), DETALLES_CANTIDAD_SACOS = Convert.ToInt32(detalle["DETALLES_CANTIDAD_SACOS"]) });

                        db.SaveChanges();

                        // verificar si hubo cambio de estado
                        if (note.ESTADOS_NOTA_ID != ESTADOS_NOTA_ID)
                        {
                            // cambiar estado a nuevo estado
                            note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;

                            // notificar a usuarios
                            this.NotificarUsuarios(EstadoNotaDePesoLogic.PREFIJO_PLANTILLA + note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE, EstadoNotaDePesoLogic.PREFIJO_PRIVILEGIO + note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE, note, db);
                        }

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

        #region Delete

        /// <summary>
        /// Elimina la nota de peso.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        public void EliminarNotaDePeso(int NOTAS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    db.DeleteObject(note);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Registrar

        /// <summary>
        /// Registra la nota de peso.
        /// </summary>
        /// <param name="NOTAS_ID"></param>
        /// <param name="ESTADO_ID"></param>
        /// <param name="MODIFICADO_POR"></param>
        public int RegistrarNotaDePeso(int NOTAS_ID, int ESTADO_ID, string MODIFICADO_POR)
        {
            try
            {
                int transactionNum = -1;
                using (var db = new colinasEntities())
                {
                    using (var scope1 = new TransactionScope())
                    {
                        EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);
                        var n = db.GetObjectByKey(k);
                        nota_de_peso note = (nota_de_peso)n;

                        note.ESTADOS_NOTA_ID = ESTADO_ID;
                        note.MODIFICADO_POR = MODIFICADO_POR;
                        note.FECHA_MODIFICACION = DateTime.Today;

                        db.SaveChanges();

                        if (note.estados_nota_de_peso.estados_detalles.ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN == true)
                        {
                            InventarioDeCafeLogic inventariodecafelogic = new InventarioDeCafeLogic();
                            note.TRANSACCION_NUMERO = inventariodecafelogic.InsertarTransaccionInventarioDeCafeDeSocio(note, db);
                            db.SaveChanges();
                            transactionNum = note.TRANSACCION_NUMERO == null ? transactionNum : Convert.ToInt32(note.TRANSACCION_NUMERO);
                        }

                        scope1.Complete();
                    }
                }

                return transactionNum;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al registrar nota de peso.", ex);
                throw;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifica usuarios sobre cambio de estado de nota de peso.
        /// </summary>
        /// <param name="PLANTILLAS_LLAVE"></param>
        /// <param name="PRIVS_LLAVE"></param>
        /// <param name="note"></param>
        /// <param name="db"></param>
        private void NotificarUsuarios(string PLANTILLAS_LLAVE, string PRIVS_LLAVE, nota_de_peso note, colinasEntities db)
        {
            try
            {
                string[] notaid = { note.NOTAS_ID.ToString() };

                PlantillaLogic plantillalogic = new PlantillaLogic();
                plantilla_notificacion pl = plantillalogic.GetPlantilla(PLANTILLAS_LLAVE);

                NotificacionLogic notificacionlogic = new NotificacionLogic();
                notificacionlogic.NotifyUsers(PRIVS_LLAVE, EstadosNotificacion.Creado, pl.PLANTILLAS_ASUNTO, pl.PLANTILLAS_MENSAJE, notaid);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al notificar usuarios.", ex);
                throw;
            }
        }

        public bool NotaDePesoRegistrada(int NOTAS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    if (note.TRANSACCION_NUMERO != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar nota de peso.", ex);
                throw;
            }
        }

        #endregion
    }
}
