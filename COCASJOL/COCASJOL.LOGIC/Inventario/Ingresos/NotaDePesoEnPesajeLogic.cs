using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Utiles;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class NotaDePesoEnPesajeLogic : NotaDePesoLogic
    {
        public NotaDePesoEnPesajeLogic() : base("PESAJE") { }

        #region Select

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

        public override void InsertarNotaDePeso
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
            Dictionary<string, string> Variables)
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
                string strNOTA_PORCENTAJEHUMEDADMIN = Variables["NOTA_PORCENTAJEHUMEDADMIN"];
                decimal NOTA_PORCENTAJEHUMEDADMIN = Convert.ToDecimal(strNOTA_PORCENTAJEHUMEDADMIN);

                if (NOTAS_PORCENTAJE_HUMEDAD < NOTA_PORCENTAJEHUMEDADMIN)
                    NOTAS_PORCENTAJE_HUMEDAD = 0;

                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD / 100;

                decimal DESCUENTO_POR_HUMEDAD = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_HUMEDAD);

                // Descuento por Transporte = ((Peso Bruto) - Tara) * (% Transporte Cooperativa)
                string strNOTA_TRANSPORTECOOP = Variables["NOTA_TRANSPORTECOOP"];
                decimal NOTA_TRANSPORTECOOP = Convert.ToDecimal(strNOTA_TRANSPORTECOOP);

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
            catch (Exception)
            {

                throw;
            }
        }

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
                    nota_de_peso note = new nota_de_peso();

                    note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;
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

                    // verificar si hubo cambio de estado
                    if (note.ESTADOS_NOTA_ID != this.ESTADOS_NOTA_ID)
                    {
                        /* --------Modificar Inventario de Café Actual-------- */

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
                            asocInventory.MODIFICADO_POR = CREADO_POR;
                            asocInventory.FECHA_MODIFICACION = DateTime.Now;
                        }
                        else
                        {
                            // si no hay inventario de café actual crearlo
                            inventario_cafe_de_socio asocInventory = new inventario_cafe_de_socio();
                            asocInventory.SOCIOS_ID = note.SOCIOS_ID;
                            asocInventory.CLASIFICACIONES_CAFE_ID = note.CLASIFICACIONES_CAFE_ID;
                            asocInventory.INVENTARIO_CANTIDAD = note.NOTAS_PESO_TOTAL_RECIBIDO;
                            asocInventory.CREADO_POR = asocInventory.MODIFICADO_POR = CREADO_POR;
                            asocInventory.FECHA_CREACION = DateTime.Now;
                            asocInventory.FECHA_MODIFICACION = asocInventory.FECHA_CREACION;

                            db.inventario_cafe_de_socio.AddObject(asocInventory);
                        }

                        // notificar a usuarios
                        PrivilegioLogic privilegiologic = new Seguridad.PrivilegioLogic();
                        List<usuario> usuarios = privilegiologic.GetUsuariosWithPrivilege("MANT_NOTASPESOENCATACION");

                        foreach (usuario usr in usuarios)
                        {
                            notificacion notification = new notificacion();
                            notification.NOTIFICACION_ESTADO = (int)EstadosNotificacion.Creado;
                            notification.USR_USERNAME = usr.USR_USERNAME;
                            notification.NOTIFICACION_TITLE = "Notas de Peso en Catación";
                            notification.NOTIFICACION_MENSAJE = "Ya tiene disponible la nota de peso #" + note.NOTAS_ID + ".";

                            db.notificaciones.AddObject(notification);
                        }
                    }

                    note.notas_detalles.Clear();

                    foreach (Dictionary<string, string> detalle in Detalles)
                        note.notas_detalles.Add(new nota_detalle() { DETALLES_PESO = Convert.ToDecimal(detalle["DETALLES_PESO"]), DETALLES_CANTIDAD_SACOS = Convert.ToInt32(detalle["DETALLES_CANTIDAD_SACOS"]) });

                    db.notas_de_peso.AddObject(note);

                    db.SaveChanges();
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
                string strNOTA_PORCENTAJEHUMEDADMIN = Variables["NOTA_PORCENTAJEHUMEDADMIN"];
                decimal NOTA_PORCENTAJEHUMEDADMIN = Convert.ToDecimal(strNOTA_PORCENTAJEHUMEDADMIN);

                if (NOTAS_PORCENTAJE_HUMEDAD < NOTA_PORCENTAJEHUMEDADMIN)
                    NOTAS_PORCENTAJE_HUMEDAD = 0;

                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD / 100;

                decimal DESCUENTO_POR_HUMEDAD = System.Math.Round((NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_HUMEDAD);

                // Descuento por Transporte = ((Peso Bruto) - Tara) * (% Transporte Cooperativa)
                string strNOTA_TRANSPORTECOOP = Variables["NOTA_TRANSPORTECOOP"];
                decimal NOTA_TRANSPORTECOOP = Convert.ToDecimal(strNOTA_TRANSPORTECOOP);

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
            catch (Exception)
            {

                throw;
            }
        }

        /*
         *                  -----Flujo-----
         *  verificar si hubo cambio de estado
         *  cambiar estado a nuevo estado
         *      --------Modificar Inventario de Café Actual--------
         *      cambiar clasificacion de café a la clasificación actual
         *      intentar obtener el inventario de café actual
         *          si hay inventario de café actual modificarlo
         *          si no hay inventario de café actual crearlo
         *          
         *          notificar a usuarios
         *
         */

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
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

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
                    note.MODIFICADO_POR = MODIFICADO_POR;
                    note.FECHA_MODIFICACION = FECHA_MODIFICACION;

                    // verificar si hubo cambio de estado
                    if (ESTADOS_NOTA_ID != this.ESTADOS_NOTA_ID)
                    {
                        // cambiar estado a nuevo estado
                        note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;

                        /* --------Modificar Inventario de Café Actual-------- */

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

                        // notificar a usuarios
                        PrivilegioLogic privilegiologic = new Seguridad.PrivilegioLogic();
                        List<usuario> usuarios = privilegiologic.GetUsuariosWithPrivilege("MANT_NOTASPESOENCATACION");

                        foreach (usuario usr in usuarios)
                        {
                            notificacion notification = new notificacion();
                            notification.NOTIFICACION_ESTADO = (int)EstadosNotificacion.Creado;
                            notification.USR_USERNAME = usr.USR_USERNAME;
                            notification.NOTIFICACION_TITLE = "Notas de Peso en Catación";
                            notification.NOTIFICACION_MENSAJE = "Ya tiene disponible la nota de peso #" + note.NOTAS_ID + ".";

                            db.notificaciones.AddObject(notification);
                        }
                    }

                    note.notas_detalles.Clear();

                    foreach (Dictionary<string, string> detalle in Detalles)
                        note.notas_detalles.Add(new nota_detalle() { DETALLES_PESO = Convert.ToDecimal(detalle["DETALLES_PESO"]), DETALLES_CANTIDAD_SACOS = Convert.ToInt32(detalle["DETALLES_CANTIDAD_SACOS"]) });

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Delete

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
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
