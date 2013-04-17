using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Objects;

using COCASJOL.DATAACCESS;

using log4net;

namespace COCASJOL.LOGIC.Reportes
{
    /// <summary>
    /// Clase con logica de Reporte Consolidado de Inventario de Café de Socios
    /// </summary>
    public class ReporteConsolidadoDeCafeDeSocios
    {
        /// <summary>
        /// Total Ingresado.
        /// </summary>
        private decimal _TotalIngresado;
        /// <summary>
        /// Total Ajustado.
        /// </summary>
        private decimal _TotalAjustado;
        /// <summary>
        /// Total Comprado.
        /// </summary>
        private decimal _TotalComprado;
        /// <summary>
        /// Total Deposito.
        /// </summary>
        private decimal _TotalDeposito;

        /// <summary>
        /// Get. Total Ingresado.
        /// </summary>
        public decimal TotalIngresado
        {
            get { return this._TotalIngresado; }
        }
        /// <summary>
        /// Get. Total Comprado.
        /// </summary>
        public decimal TotalComprado
        {
            get { return this._TotalComprado; }
        }
        /// <summary>
        /// Get. Total Ajustado.
        /// </summary>
        public decimal TotalAjustado
        {
            get { return this._TotalAjustado; }
        }
        /// <summary>
        /// Get. Total Deposito.
        /// </summary>
        public decimal TotalDeposito
        {
            get { return this._TotalDeposito; }
        }

        /// <summary>
        /// Constructor. Inicializa los miembros y calcula el total deposito.
        /// </summary>
        /// <param name="Total_Ingresado"></param>
        /// <param name="Total_Ajustado"></param>
        /// <param name="Total_Comprado"></param>
        public ReporteConsolidadoDeCafeDeSocios(decimal Total_Ingresado, decimal Total_Ajustado, decimal Total_Comprado)
        {
            this._TotalIngresado = Total_Ingresado / 100;// QQ
            this._TotalAjustado = Total_Ajustado / 100;//QQ
            this._TotalComprado = Total_Comprado / 100;// QQ

            this._TotalDeposito = (Total_Ingresado - Total_Ajustado - Total_Comprado) / 100;//QQ
        }
    }

    /// <summary>
    /// Clase con logica de Reporte Consolidado de Inventario de Café de Cooperativa
    /// </summary>
    public class ReporteConsolidadoDeCafe
    {
        /// <summary>
        /// Total Comprado
        /// </summary>
        private decimal _TotalComprado;
        /// <summary>
        /// Total Vendido
        /// </summary>
        private decimal _TotalVendido;
        /// <summary>
        /// Total Deposito
        /// </summary>
        private decimal _TotalDeposito;

        /// <summary>
        /// Get. Total Comprado
        /// </summary>
        public decimal TotalComprado
        {
            get { return this._TotalComprado; }
        }
        /// <summary>
        /// Get. Total Vendido
        /// </summary>
        public decimal TotalVendido
        {
            get { return this._TotalVendido; }
        }
        /// <summary>
        /// Get. Total Depósito
        /// </summary>
        public decimal TotalDeposito
        {
            get { return this._TotalDeposito; }
        }

        /// <summary>
        /// Constructor. Inicializa los miembros y calcula el total deposito.
        /// </summary>
        /// <param name="Total_Comprado"></param>
        /// <param name="Total_Vendido"></param>
        public ReporteConsolidadoDeCafe(decimal Total_Comprado, decimal Total_Vendido)
        {
            this._TotalComprado = Total_Comprado / 100;// QQ
            this._TotalVendido = Total_Vendido / 100;// QQ

            this._TotalDeposito = (Total_Comprado - Total_Vendido) / 100;//QQ
        }
    }

    /// <summary>
    /// Clase con logica de Reporte Consolidado de Inventario de Café
    /// </summary>
    public class ConsolidadoDeInventarioDeCafeLogic
    {
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(ConsolidadoDeInventarioDeCafeLogic).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConsolidadoDeInventarioDeCafeLogic() { }

        #region Select
        /// <summary>
        /// Obtiene Reporte Consolidado de Inventario de Café de Socios.
        /// </summary>
        /// <returns>Reporte Consolidado de Inventario de Café de Socios.</returns>
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

        /// <summary>
        /// Obtiene Reporte Consolidado de Inventario de Café de Socios.
        /// </summary>
        /// <param name="MesInicial"></param>
        /// <param name="MesFinal"></param>
        /// <param name="DiaInicial"></param>
        /// <param name="DiaFinal"></param>
        /// <returns>Reporte Consolidado de Inventario de Café de Socios.</returns>
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

        /// <summary>
        /// Obtiene Reporte Consolidado de Inventario de Café de Socios.
        /// </summary>
        /// <param name="dFECHA_DESDE"></param>
        /// <param name="dFECHA_HASTA"></param>
        /// <returns>Reporte Consolidado de Inventario de Café de Socios.</returns>
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

        /// <summary>
        /// Obtiene Reporte Consolidado de Inventario de Café de Cooperativa.
        /// </summary>
        /// <returns>Reporte Consolidado de Inventario de Café de Cooperativa.</returns>
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

        /// <summary>
        /// Obtiene Reporte Consolidado de Inventario de Café de Cooperativa.
        /// </summary>
        /// <param name="MesInicial"></param>
        /// <param name="MesFinal"></param>
        /// <param name="DiaInicial"></param>
        /// <param name="DiaFinal"></param>
        /// <returns>Reporte Consolidado de Inventario de Café de Cooperativa.</returns>
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

        /// <summary>
        /// Obtiene Reporte Consolidado de Inventario de Café de Cooperativa.
        /// </summary>
        /// <param name="dFECHA_DESDE"></param>
        /// <param name="dFECHA_HASTA"></param>
        /// <returns>Reporte Consolidado de Inventario de Café de Cooperativa.</returns>
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
