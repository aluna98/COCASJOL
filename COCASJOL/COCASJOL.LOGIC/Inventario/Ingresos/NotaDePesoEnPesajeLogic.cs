using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;
using System.Transactions;

using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Utiles;

using log4net;

namespace COCASJOL.LOGIC.Inventario.Ingresos
{
    public class NotaDePesoEnPesajeLogic : NotaDePesoLogic
    {
        private static ILog log = LogManager.GetLogger(typeof(NotaDePesoEnPesajeLogic).Name);

        public NotaDePesoEnPesajeLogic() : base("PESAJE") { }

        #region Select

        public override List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    EstadoNotaDePesoLogic estadoslogic = new EstadoNotaDePesoLogic();

                    db.estados_nota_de_peso.MergeOption = MergeOption.NoTracking;

                    var queryPadre = from enp in db.estados_nota_de_peso.Include("estados_nota_de_peso_hijos")
                                     where enp.ESTADOS_NOTA_ID == this.ESTADOS_NOTA_ID
                                     select enp;

                    estado_nota_de_peso padre = queryPadre.First();
                    List<estado_nota_de_peso> estadolist = GetHijos(padre);

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
                        note.MODIFICADO_POR = CREADO_POR;
                        note.FECHA_MODIFICACION = FECHA_CREACION;

                        note.notas_detalles.Clear();

                        foreach (Dictionary<string, string> detalle in Detalles)
                            note.notas_detalles.Add(new nota_detalle() { DETALLES_PESO = Convert.ToDecimal(detalle["DETALLES_PESO"]), DETALLES_CANTIDAD_SACOS = Convert.ToInt32(detalle["DETALLES_CANTIDAD_SACOS"]) });

                        db.notas_de_peso.AddObject(note);

                        db.SaveChanges();

                        // verificar si hubo cambio de estado
                        if (note.ESTADOS_NOTA_ID != this.ESTADOS_NOTA_ID)
                        {
                            // notificar a usuarios
                            //this.NotificarUsuarios("NOTASCATACION", "MANT_NOTASPESOENCATACION", note, db);

                            string ESTADO_NOTA_LLAVE = note.estados_nota_de_peso.ESTADOS_NOTA_LLAVE;
                            this.NotificarUsuarios("NOTAS" + ESTADO_NOTA_LLAVE, "MANT_NOTASPESOEN" + ESTADO_NOTA_LLAVE, note, db);
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
                        if (ESTADOS_NOTA_ID != this.ESTADOS_NOTA_ID)
                        {
                            // cambiar estado a nuevo estado
                            note.ESTADOS_NOTA_ID = ESTADOS_NOTA_ID;

                            // notificar a usuarios
                            this.NotificarUsuarios("NOTASCATACION", "MANT_NOTASPESOENCATACION", note, db);
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
