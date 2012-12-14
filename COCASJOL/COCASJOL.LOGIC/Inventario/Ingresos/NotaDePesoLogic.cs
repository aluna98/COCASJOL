using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class NotaDePesoLogic
    {
        public NotaDePesoLogic() { } 

        #region Select

        public List<nota_de_peso> GetNotasDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    db.notas_de_peso.MergeOption = MergeOption.NoTracking;

                    return db.notas_de_peso.Include("notas_de_peso").Include("socios").Include("clasificaciones_cafe").ToList<nota_de_peso>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public nota_de_peso GetNotaDePeso(int NOTAS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    return note;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<nota_detalle> GetDetalleNotaDePeso(int NOTAS_ID)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    return note.notas_detalles.ToList<nota_detalle>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<nota_de_peso> GetNotasDePeso
            (int NOTAS_ID,
            int ESTADOS_NOTA_ID,
            string ESTADOS_NOTA_NOMBRE,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            string CLASIFICACIONES_CAFE_NOMBRE,
            DateTime NOTAS_FECHA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            Boolean? NOTAS_TRANSPORTE_COOPERATIVA,
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

                    var query = from notasPeso in db.notas_de_peso.Include("socios").Include("clasificaciones_cafe").Include("estados_nota_de_peso")
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

        #endregion

        #region Insert

        /*
         *               -----Calculos-----
         * 
         *                  Tara = Peso de los sacos
         *            Peso bruto = Peso total de café
         * 
         *             % Defecto = Peso de  muestra / peso de gramos malos
         * Descuento por Defecto = ((Peso Bruto) - Tara) * (% Defecto)
         * 
         *             % Humedad = Valor devuelto por maquina?
         * Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
         * 
         *             Descuento = (Descuento por Defecto) + (Descuento por Humedad)
         *                 Total = (Peso Bruto) - Tara - Descuento
         * 
         */

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
                decimal DESCUENTO_POR_DEFECTO = (NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_DEFECTO;

                // Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
                string strPORCENTAJEHUMEDADMIN = Variables["PORCENTAJEHUMEDADMIN"];
                decimal PORCENTAJEHUMEDADMIN = Convert.ToDecimal(strPORCENTAJEHUMEDADMIN);

                if (NOTAS_PORCENTAJE_HUMEDAD < PORCENTAJEHUMEDADMIN)
                    NOTAS_PORCENTAJE_HUMEDAD = 0;

                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD / 100;

                decimal DESCUENTO_POR_HUMEDAD = (NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_HUMEDAD;

                // Descuento = (Descuento por Defecto) + (Descuento por Humedad)
                decimal DESCUENTO = DESCUENTO_POR_DEFECTO + DESCUENTO_POR_HUMEDAD;

                // Total = (Peso Bruto) - Tara - Descuento
                decimal TOTAL = NOTAS_PESO_SUMA - NOTAS_PESO_TARA - DESCUENTO;

                string localization = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasLocalizacion");

                COCASJOL.LOGIC.Utiles.Numalet cq = new COCASJOL.LOGIC.Utiles.Numalet();
                cq.SeparadorDecimalSalida = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasSeparadorDecimalSalida");
                cq.MascaraSalidaDecimal = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasMascaraSalidaDecimal");
                cq.ConvertirDecimales = bool.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasConvertirDecimales"));
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
                    NOTAS_PORCENTAJE_DEFECTO,
                    NOTAS_PORCENTAJE_HUMEDAD,
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
                    note.NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO;
                    note.NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD;
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

                    foreach(Dictionary<string, string> detalle in Detalles)
                        note.notas_detalles.Add(new nota_detalle() { DETALLES_PESO = Convert.ToDecimal(detalle["DETALLES_PESO"]), DETALLES_CANTIDAD_SACOS = Convert.ToInt32(detalle["DETALLES_CANTIDAD_SACOS"]) });

                    db.notas_de_peso.AddObject(note);


                    /*
                     * Agregar a Inventario
                     */
                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k = new EntityKey("colinasEntities.inventario_cafe_de_socio", entityKeyValues);

                    Object invCafSoc = null;

                    if (db.TryGetObjectByKey(k, out invCafSoc))
                    {
                        inventario_cafe_de_socio asocInventory = (inventario_cafe_de_socio)invCafSoc;
                        asocInventory.INVENTARIO_CANTIDAD += NOTAS_PESO_TOTAL_RECIBIDO;
                        asocInventory.MODIFICADO_POR = CREADO_POR;
                        asocInventory.FECHA_MODIFICACION = FECHA_CREACION;
                    }
                    else
                    {
                        inventario_cafe_de_socio asocInventory = new inventario_cafe_de_socio();
                        asocInventory.SOCIOS_ID = SOCIOS_ID;
                        asocInventory.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        asocInventory.INVENTARIO_CANTIDAD = NOTAS_PESO_TOTAL_RECIBIDO;
                        asocInventory.CREADO_POR = CREADO_POR;
                        asocInventory.FECHA_CREACION = FECHA_CREACION;
                        asocInventory.MODIFICADO_POR = CREADO_POR;
                        asocInventory.FECHA_MODIFICACION = FECHA_CREACION;

                        db.inventario_cafe_de_socio.AddObject(asocInventory);
                    }

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
         *               -----Calculos-----
         * 
         *                  Tara = Peso de los sacos
         *            Peso bruto = Peso total de café
         * 
         *             % Defecto = Peso de  muestra / peso de gramos malos
         * Descuento por Defecto = ((Peso Bruto) - Tara) * (% Defecto)
         * 
         *             % Humedad = Valor devuelto por maquina?
         * Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
         * 
         *             Descuento = (Descuento por Defecto) + (Descuento por Humedad)
         *                 Total = (Peso Bruto) - Tara - Descuento
         * 
         */

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
                decimal DESCUENTO_POR_DEFECTO = (NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_DEFECTO;

                // Descuento por Humedad = ((Peso Bruto) - Tara) * (% Humedad)
                string strPORCENTAJEHUMEDADMIN = Variables["PORCENTAJEHUMEDADMIN"];
                decimal PORCENTAJEHUMEDADMIN = Convert.ToDecimal(strPORCENTAJEHUMEDADMIN);

                if (NOTAS_PORCENTAJE_HUMEDAD < PORCENTAJEHUMEDADMIN)
                    NOTAS_PORCENTAJE_HUMEDAD = 0;

                NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD / 100;

                decimal DESCUENTO_POR_HUMEDAD = (NOTAS_PESO_SUMA - NOTAS_PESO_TARA) * NOTAS_PORCENTAJE_HUMEDAD;

                // Descuento = (Descuento por Defecto) + (Descuento por Humedad)
                decimal DESCUENTO = DESCUENTO_POR_DEFECTO + DESCUENTO_POR_HUMEDAD;

                // Total = (Peso Bruto) - Tara - Descuento
                decimal TOTAL = NOTAS_PESO_SUMA - NOTAS_PESO_TARA - DESCUENTO;

                string localization = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasLocalizacion");

                COCASJOL.LOGIC.Utiles.Numalet cq = new COCASJOL.LOGIC.Utiles.Numalet();
                cq.SeparadorDecimalSalida = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasSeparadorDecimalSalida");
                cq.MascaraSalidaDecimal = System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasMascaraSalidaDecimal");
                cq.ConvertirDecimales = bool.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("numerosALetrasConvertirDecimales"));
                cq.LetraCapital = true;

                //string TOTAL_TEXTO = COCASJOL.LOGIC.Utiles.Numalet.ToCardinal(TOTAL.ToString(), new System.Globalization.CultureInfo(localization));

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
                    NOTAS_PORCENTAJE_DEFECTO,
                    NOTAS_PORCENTAJE_HUMEDAD,
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

        private void ActualizarNotaDePeso
            (int NOTAS_ID,
            int ESTADOS_NOTA_ID,
            string SOCIOS_ID,
            int CLASIFICACIONES_CAFE_ID,
            DateTime NOTAS_FECHA,
            Boolean NOTAS_TRANSPORTE_COOPERATIVA,
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

                    decimal NOTAS_PESO_TOTAL_RECIBIDO_ANTERIOR = note.NOTAS_PESO_TOTAL_RECIBIDO;
                    int CLASIFICACIONES_CAFE_ID_ANTERIOR = note.CLASIFICACIONES_CAFE_ID;

                    note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;
                    note.SOCIOS_ID = SOCIOS_ID;
                    note.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                    note.NOTAS_FECHA = NOTAS_FECHA;
                    note.NOTAS_TRANSPORTE_COOPERATIVA = NOTAS_TRANSPORTE_COOPERATIVA;
                    note.NOTAS_PORCENTAJE_DEFECTO = NOTAS_PORCENTAJE_DEFECTO;
                    note.NOTAS_PORCENTAJE_HUMEDAD = NOTAS_PORCENTAJE_HUMEDAD;
                    note.NOTAS_PESO_DEFECTO = NOTAS_PESO_DEFECTO;
                    note.NOTAS_PESO_HUMEDAD = NOTAS_PESO_HUMEDAD;
                    note.NOTAS_PESO_TARA = NOTAS_PESO_TARA;
                    note.NOTAS_PESO_SUMA = NOTAS_PESO_SUMA;
                    note.NOTAS_PESO_TOTAL_RECIBIDO = NOTAS_PESO_TOTAL_RECIBIDO;
                    note.NOTAS_PESO_TOTAL_RECIBIDO_TEXTO = NOTAS_PESO_TOTAL_RECIBIDO_TEXTO;
                    note.NOTAS_SACOS_RETENIDOS = NOTAS_SACOS_RETENIDOS;
                    note.MODIFICADO_POR = MODIFICADO_POR;
                    note.FECHA_MODIFICACION = FECHA_MODIFICACION;

                    note.notas_detalles.Clear();

                    foreach (Dictionary<string, string> detalle in Detalles)
                        note.notas_detalles.Add(new nota_detalle() { DETALLES_PESO = Convert.ToDecimal(detalle["DETALLE_PESO"]), DETALLES_CANTIDAD_SACOS = Convert.ToInt32(detalle["DETALLE_CANTIDAD_SACOS"]) });

                    db.notas_de_peso.AddObject(note);


                    /*
                     * Actualizar el Inventario! Si actualizan la nota se le resta el Total anterior y se le suma el total actual?
                     */

                    // Si hubo cambio de clasificacion, hay que remover la cantidad de la clasificacione anterior y agregar la nueva cantidad a la nueva clasificacion.

                    if (CLASIFICACIONES_CAFE_ID_ANTERIOR != CLASIFICACIONES_CAFE_ID)
                    {
                        IEnumerable<KeyValuePair<string, object>> entityKeyValuesAnterior =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };
                    }

                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", CLASIFICACIONES_CAFE_ID) };

                    EntityKey k2 = new EntityKey("colinasEntities.inventario_cafe_de_socio", entityKeyValues);

                    Object invCafSoc = null;

                    if (db.TryGetObjectByKey(k2, out invCafSoc))
                    {
                        inventario_cafe_de_socio asocInventory = (inventario_cafe_de_socio)invCafSoc;
                        asocInventory.INVENTARIO_CANTIDAD -= NOTAS_PESO_TOTAL_RECIBIDO_ANTERIOR;
                        asocInventory.INVENTARIO_CANTIDAD += NOTAS_PESO_TOTAL_RECIBIDO;
                        asocInventory.MODIFICADO_POR = MODIFICADO_POR;
                        asocInventory.FECHA_MODIFICACION = FECHA_MODIFICACION;
                    }
                    else
                    {
                        inventario_cafe_de_socio asocInventory = new inventario_cafe_de_socio();
                        asocInventory.SOCIOS_ID = SOCIOS_ID;
                        asocInventory.CLASIFICACIONES_CAFE_ID = CLASIFICACIONES_CAFE_ID;
                        asocInventory.INVENTARIO_CANTIDAD = NOTAS_PESO_TOTAL_RECIBIDO;
                        asocInventory.CREADO_POR = MODIFICADO_POR;
                        asocInventory.FECHA_CREACION = FECHA_MODIFICACION;
                        asocInventory.MODIFICADO_POR = MODIFICADO_POR;
                        asocInventory.FECHA_MODIFICACION = FECHA_MODIFICACION;

                        db.inventario_cafe_de_socio.AddObject(asocInventory);
                    }

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

        public void EliminarNotasDePeso(int NOTAS_ID, string USR_USERNAME)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.notas_de_peso", "NOTAS_ID", NOTAS_ID);

                    var n = db.GetObjectByKey(k);

                    nota_de_peso note = (nota_de_peso)n;

                    db.DeleteObject(note);

                    /*
                     * Actualizar Inventario! Si borran la nota de peso hay que restar el total que se registro?
                     */
                    IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                        new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("SOCIOS_ID", note.SOCIOS_ID),
                            new KeyValuePair<string, object>("CLASIFICACIONES_CAFE_ID", note.CLASIFICACIONES_CAFE_ID) };

                    EntityKey k2 = new EntityKey("colinasEntities.inventario_cafe_de_socio", entityKeyValues);

                    Object invCafSoc = null;

                    if (db.TryGetObjectByKey(k2, out invCafSoc))
                    {
                        inventario_cafe_de_socio asocInventory = (inventario_cafe_de_socio)invCafSoc;
                        asocInventory.INVENTARIO_CANTIDAD -= note.NOTAS_PESO_TOTAL_RECIBIDO;
                        asocInventory.MODIFICADO_POR = USR_USERNAME;
                        asocInventory.FECHA_MODIFICACION = DateTime.Today;
                    }
                    //else
                    //{
                    //    //what to do?
                    //}


                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
