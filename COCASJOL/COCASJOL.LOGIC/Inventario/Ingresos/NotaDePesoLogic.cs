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
        protected int ESTADOS_NOTA_ID;

        public NotaDePesoLogic(string ESTADOS_NOTA_LLAVE)
        {
            try
            {
                EstadoNotaDePesoLogic estadologic = new EstadoNotaDePesoLogic();
                estado_nota_de_peso esn = estadologic.GetEstadoNotaDePeso(ESTADOS_NOTA_LLAVE);
                this.ESTADOS_NOTA_ID = esn == null ? 0 : esn.ESTADOS_NOTA_ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Select

        public virtual List<nota_de_peso> GetNotasDePeso()
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

        public virtual List<nota_de_peso> GetNotasDePeso
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

        public virtual List<estado_nota_de_peso> GetEstadosNotaDePeso()
        {
            try
            {
                EstadoNotaDePesoLogic estadosnotadepesologic = new EstadoNotaDePesoLogic();
                return estadosnotadepesologic.GetEstadosNotaDePeso();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected List<estado_nota_de_peso> GetHijos(estado_nota_de_peso padre)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    List<estado_nota_de_peso> hijos = new List<estado_nota_de_peso>();
                    hijos.Add(padre);
                    foreach (estado_nota_de_peso hijo in padre.estados_nota_de_peso_hijos)
                    {
                        hijos.AddRange(GetHijos(hijo));
                    }
                    return hijos;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Insert

        public virtual void InsertarNotaDePeso
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
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Update

        public virtual void ActualizarNotaDePeso
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
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
