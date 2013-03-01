using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

namespace COCASJOL.LOGIC.Reportes
{
    public class ReporteConsolidadoDeCafe
    {
        private decimal _TotalIngresado, _TotalComprado, _TotalDeposito;

        public decimal TotalIngresado
        {
            get { return this._TotalIngresado; }
            set { this._TotalIngresado = value; }
        }

        public decimal TotalComprado
        {
            get { return this._TotalComprado; }
            set { this._TotalComprado = value; }
        }

        public decimal TotalDeposito
        {
            get { return this._TotalDeposito; }
            set { this._TotalDeposito = value; }
        }

        public ReporteConsolidadoDeCafe(decimal Total_Ingresado, decimal Total_Comprado, decimal Total_Deposito)
        {
            this._TotalIngresado = Total_Ingresado / 100;// QQ
            this._TotalComprado = Total_Comprado / 100;// QQ
            this._TotalDeposito = Total_Deposito / 100;// QQ
        }
    }

    public class ConsolidadoDeInventarioDeCafeLogic
    {
        public ConsolidadoDeInventarioDeCafeLogic() { }

        #region Select

        public ReporteConsolidadoDeCafe GetReporte()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var queryIngresado = (from n in db.notas_de_peso
                                          where n.TRANSACCION_NUMERO != null
                                          select n).ToList();

                    var queryComprado = (from l in db.liquidaciones
                                        select l).ToList();

                    decimal TotalIngresado = queryIngresado.Sum(n => n.NOTAS_PESO_TOTAL_RECIBIDO);
                    decimal TotalComprado = Convert.ToDecimal(queryComprado.Sum(l => l.LIQUIDACIONES_TOTAL_LIBRAS));
                    decimal TotalDeposito = TotalIngresado - TotalComprado;

                    return new ReporteConsolidadoDeCafe(TotalIngresado, TotalComprado, TotalDeposito);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public ReporteConsolidadoDeCafe GetReporte(int MesInicial, int MesFinal, int DiaInicial, int DiaFinal)
        {
            try
            {
                int ActualYear = DateTime.Now.Year;

                DateTime? FechaDesde = MesInicial <= 0 ? (DateTime?)null : new DateTime(ActualYear, MesInicial, DiaInicial <= 0 ? 1 : DiaInicial);
                DateTime? FechaHasta = MesFinal <= 0 ? (DateTime?)null : new DateTime(ActualYear, MesFinal, DiaFinal <= 0 ? DateTime.DaysInMonth(ActualYear, MesFinal) : DiaFinal);

                return GetReporte(FechaDesde, FechaHasta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ReporteConsolidadoDeCafe GetReporte(DateTime? dFECHA_DESDE, DateTime? dFECHA_HASTA)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    decimal? Ingresado = db.GetSumatoriaNotasDePeso(dFECHA_DESDE, dFECHA_HASTA).FirstOrDefault();
                    decimal? Comprado = db.GetSumatoriaHojasDeLiquidacion(dFECHA_DESDE, dFECHA_HASTA).FirstOrDefault();

                    decimal TotalIngresado = Ingresado == null ? 0 : Convert.ToDecimal(Ingresado);
                    decimal TotalComprado = Comprado == null ? 0 : Convert.ToDecimal(Comprado);
                    decimal TotalDeposito = TotalIngresado - TotalComprado;

                    return new ReporteConsolidadoDeCafe(TotalIngresado, TotalComprado, TotalDeposito);
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
