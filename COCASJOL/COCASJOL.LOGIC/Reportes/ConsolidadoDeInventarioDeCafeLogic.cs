using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using log4net;

namespace COCASJOL.LOGIC.Reportes
{
    public class ReporteConsolidadoDeCafeDeSocios
    {
        private decimal _TotalIngresado, _TotalAjustado, _TotalComprado, _TotalDeposito;

        public decimal TotalIngresado
        {
            get { return this._TotalIngresado; }
        }

        public decimal TotalComprado
        {
            get { return this._TotalComprado; }
        }

        public decimal TotalAjustado
        {
            get { return this._TotalAjustado; }
        }

        public decimal TotalDeposito
        {
            get { return this._TotalDeposito; }
        }

        public ReporteConsolidadoDeCafeDeSocios(decimal Total_Ingresado, decimal Total_Ajustado, decimal Total_Comprado)
        {
            this._TotalIngresado = Total_Ingresado / 100;// QQ
            this._TotalAjustado = Total_Ajustado / 100;//QQ
            this._TotalComprado = Total_Comprado / 100;// QQ

            this._TotalDeposito = (Total_Ingresado - Total_Ajustado - Total_Comprado) / 100;//QQ
        }
    }

    public class ReporteConsolidadoDeCafe
    {
        private decimal _TotalComprado, _TotalVendido, _TotalDeposito;

        public decimal TotalComprado
        {
            get { return this._TotalComprado; }
        }

        public decimal TotalVendido
        {
            get { return this._TotalVendido; }
        }

        public decimal TotalDeposito
        {
            get { return this._TotalDeposito; }
        }

        public ReporteConsolidadoDeCafe(decimal Total_Comprado, decimal Total_Vendido)
        {
            this._TotalComprado = Total_Comprado / 100;// QQ
            this._TotalVendido = Total_Vendido / 100;// QQ

            this._TotalDeposito = (Total_Comprado - Total_Vendido) / 100;//QQ
        }
    }

    public class ConsolidadoDeInventarioDeCafeLogic
    {
        private static ILog log = LogManager.GetLogger(typeof(ConsolidadoDeInventarioDeCafeLogic).Name);

        public ConsolidadoDeInventarioDeCafeLogic() { }

        #region Select

        public ReporteConsolidadoDeCafeDeSocios GetReporteCafeDeSocios()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var queryIngresado = (from n in db.notas_de_peso
                                          where n.TRANSACCION_NUMERO != null
                                          select n).ToList();

                    var queryAjustado = (from a in db.ajustes_inventario_cafe_x_socio
                                         select a).ToList();

                    var queryComprado = (from l in db.liquidaciones
                                        select l).ToList();

                    decimal TotalIngresado = queryIngresado.Sum(n => n.NOTAS_PESO_TOTAL_RECIBIDO);
                    decimal TotalAjustado = queryAjustado.Sum(a => a.AJUSTES_INV_CAFE_CANTIDAD_LIBRAS);
                    decimal TotalComprado = queryComprado.Sum(l => l.LIQUIDACIONES_TOTAL_LIBRAS);

                    return new ReporteConsolidadoDeCafeDeSocios(TotalIngresado, TotalAjustado, TotalComprado);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte.", ex);
                throw;
            }
        }

        public ReporteConsolidadoDeCafeDeSocios GetReporteCafeDeSocios(int MesInicial, int MesFinal, int DiaInicial, int DiaFinal)
        {
            try
            {
                int ActualYear = DateTime.Now.Year;

                DateTime? FechaDesde = MesInicial <= 0 ? (DateTime?)null : new DateTime(ActualYear, MesInicial, DiaInicial <= 0 ? 1 : DiaInicial);
                DateTime? FechaHasta = MesFinal <= 0 ? (DateTime?)null : new DateTime(ActualYear, MesFinal, DiaFinal <= 0 ? DateTime.DaysInMonth(ActualYear, MesFinal) : DiaFinal);

                return GetReporteCafeDeSocios(FechaDesde, FechaHasta);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte.", ex);
                throw;
            }
        }

        public ReporteConsolidadoDeCafeDeSocios GetReporteCafeDeSocios(DateTime? dFECHA_DESDE, DateTime? dFECHA_HASTA)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    decimal? Ingresado = db.GetSumatoriaNotasDePeso(dFECHA_DESDE, dFECHA_HASTA).FirstOrDefault();
                    decimal? Ajustado = db.GetSumatoriaAjustesDeInventarioDeCafeDeSocios(dFECHA_DESDE, dFECHA_HASTA).FirstOrDefault();
                    decimal? Comprado = db.GetSumatoriaHojasDeLiquidacion(dFECHA_DESDE, dFECHA_HASTA).FirstOrDefault();

                    decimal TotalIngresado = Ingresado == null ? 0 : Convert.ToDecimal(Ingresado);
                    decimal TotalAjustado = Ajustado == null ? 0 : Convert.ToDecimal(Ajustado);
                    decimal TotalComprado = Comprado == null ? 0 : Convert.ToDecimal(Comprado);

                    return new ReporteConsolidadoDeCafeDeSocios(TotalIngresado, TotalAjustado, TotalComprado);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte.", ex);
                throw;
            }
        }

        public ReporteConsolidadoDeCafe GetReporteCafeCooperativa()
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    var queryComprado = (from l in db.liquidaciones
                                         select l).ToList();

                    var queryVendido = (from v in db.ventas_inventario_cafe
                                        select v).ToList();

                    decimal TotalComprado = queryComprado.Sum(l => l.LIQUIDACIONES_TOTAL_LIBRAS);
                    decimal TotalVendido = queryVendido.Sum(n => n.VENTAS_INV_CAFE_CANTIDAD_LIBRAS);

                    return new ReporteConsolidadoDeCafe(TotalComprado, TotalVendido);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte.", ex);
                throw;
            }
        }

        public ReporteConsolidadoDeCafe GetReporteCafeCooperativa(int MesInicial, int MesFinal, int DiaInicial, int DiaFinal)
        {
            try
            {
                int ActualYear = DateTime.Now.Year;

                DateTime? FechaDesde = MesInicial <= 0 ? (DateTime?)null : new DateTime(ActualYear, MesInicial, DiaInicial <= 0 ? 1 : DiaInicial);
                DateTime? FechaHasta = MesFinal <= 0 ? (DateTime?)null : new DateTime(ActualYear, MesFinal, DiaFinal <= 0 ? DateTime.DaysInMonth(ActualYear, MesFinal) : DiaFinal);

                return GetReporteCafeCooperativa(FechaDesde, FechaHasta);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte.", ex);
                throw;
            }
        }

        public ReporteConsolidadoDeCafe GetReporteCafeCooperativa(DateTime? dFECHA_DESDE, DateTime? dFECHA_HASTA)
        {
            try
            {
                using (var db = new colinasEntities())
                {
                    decimal? Comprado = db.GetSumatoriaHojasDeLiquidacion(dFECHA_DESDE, dFECHA_HASTA).FirstOrDefault();
                    decimal? Vendido = db. GetSumatoriaVentasDeInventarioDeCafe(dFECHA_DESDE, dFECHA_HASTA).FirstOrDefault();

                    decimal TotalComprado = Comprado == null ? 0 : Convert.ToDecimal(Comprado);
                    decimal TotalVendido = Vendido == null ? 0 : Convert.ToDecimal(Vendido);

                    return new ReporteConsolidadoDeCafe(TotalComprado, TotalVendido);
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al obtener reporte.", ex);
                throw;
            }
        }

        #endregion
    }
}
