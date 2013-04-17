using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

using log4net;

namespace COCASJOL.LOGIC.Configuracion
{
    /// <summary>
    /// Clase con logica de Configuración de Sistema
    /// </summary>
    public class ConfiguracionDeSistemaLogic
    {
        #region Private Members

        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(ConfiguracionDeSistemaLogic).Name);

        /// <summary>
        /// Vector de inicialización para encriptación TripleDES.
        /// </summary>
        private string vector = "***PDG**";
        /// <summary>
        /// Llave de encriptación para algoritmo TripleDES.
        /// </summary>
        private string key = "********!!PDG1!!********";

        /// <summary>
        /// XML con información de configuración de sistema.
        /// </summary>
        private XmlDocument Configuracion;

        /// <summary>
        /// Maximizar Ventanas al Inicio.
        /// </summary>
        private bool ventana_Maximizar;
        /// <summary>
        /// Cargar Datos en Ventanas al Inicio.
        /// </summary>
        private bool ventana_CargarDatos;

        /// <summary>
        /// Fecha de Inicio de Periódo.
        /// </summary>
        private DateTime consolidado_InicioPeriodo;
        /// <summary>
        /// Fecha de Final de Periódo.
        /// </summary>
        private DateTime consolidado_FinalPeriodo;

        /// <summary>
        /// Dirección de Correo Local.
        /// </summary>
        private string correo_CorreoLocal;
        /// <summary>
        /// Usar Password para Correo Local.
        /// </summary>
        private bool correo_UsarPassword;
        /// <summary>
        /// Password Desencryptado.
        /// </summary>
        private string correo_Password;
        /// <summary>
        /// Dirección de Servidor de Correos SMTP.
        /// </summary>
        private string correo_SMTP;
        /// <summary>
        /// Puerto a Utilizar para Enviar Correos.
        /// </summary>
        private int correo_Puerto;
        /// <summary>
        /// Usar SSL para Envío de Correos.
        /// </summary>
        private bool correo_UsarSSL;

        /// <summary>
        /// Ultimo Usuario que Modificó Registro.
        /// </summary>
        private string auditoria_username;
        /// <summary>
        /// Ultima Fecha de Modificación.
        /// </summary>
        private DateTime auditoria_date;

        /// <summary>
        /// Indica si la importacion de Socios esta Disponíble.
        /// </summary>
        private bool socios_importacion;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or Set. Maximizar Ventanas al Inicio.
        /// </summary>
        public bool VentanasMaximizar
        {
            get
            {
                return this.ventana_Maximizar;
            }

            set
            {
                this.ventana_Maximizar = value;
            }
        }
        /// <summary>
        /// Get or Set. Cargar Datos en Ventanas al Inicio.
        /// </summary>
        public bool VentanasCargarDatos
        {
            get
            {
                return this.ventana_CargarDatos;
            }
            set
            {
                this.ventana_CargarDatos = value;
            }
        }

        /// <summary>
        /// Get or Set. Fecha de Inicio de Periódo.
        /// </summary>
        public DateTime ConsolidadoInventarioInicioPeriodo
        {
            get
            {
                return this.consolidado_InicioPeriodo;
            }
            set
            {
                this.consolidado_InicioPeriodo = value;
            }
        }
        /// <summary>
        /// Get or Set. Fecha de Final de Periódo.
        /// </summary>
        public DateTime ConsolidadoInventarioFinalPeriodo
        {
            get
            {
                return this.consolidado_FinalPeriodo;
            }
            set
            {
                this.consolidado_FinalPeriodo = value;
            }
        }

        /// <summary>
        /// Get or Set. Dirección de Correo Local.
        /// </summary>
        public string CorreoCorreoLocal
        {
            get
            {
                return this.correo_CorreoLocal;
            }
            set
            {
                this.correo_CorreoLocal = value;
            }
        }
        /// <summary>
        /// Get or Set. Usar Password para Correo Local.
        /// </summary>
        public bool CorreoUsarPassword
        {
            get
            {
                return this.correo_UsarPassword;
            }
            set
            {
                this.correo_UsarPassword = value;
            }
        }
        /// <summary>
        /// Get or Set. Password Desencryptado.
        /// </summary>
        public string CorreoPassword
        {
            get
            {
                return this.correo_Password;
            }
            set
            {
                this.correo_Password = value;
            }
        }
        /// <summary>
        /// Get or Set. Dirección de Servidor de Correos SMTP.
        /// </summary>
        public string CorreoSMTP
        {
            get
            {
                return this.correo_SMTP;
            }
            set
            {
                this.correo_SMTP = value;
            }
        }
        /// <summary>
        /// Get or Set. Puerto a Utilizar para Enviar Correos.
        /// </summary>
        public int CorreoPuerto
        {
            get
            {
                return this.correo_Puerto;
            }
            set
            {
                this.correo_Puerto = value;
            }
        }
        /// <summary>
        /// Get or Set. Usar SSL para Envío de Correos.
        /// </summary>
        public bool CorreoUsarSSL
        {
            get
            {
                return this.correo_UsarSSL;
            }
            set
            {
                this.correo_UsarSSL = value;
            }
        }

        /// <summary>
        /// Get or Set. Ultimo Usuario que Modificó Registro.
        /// </summary>
        public string AuditoriaUserName
        {
            get
            {
                return this.auditoria_username;
            }
            set
            {
                this.auditoria_username = value;
            }
        }
        /// <summary>
        /// Get or Set. Ultima Fecha de Modificación.
        /// </summary>
        public DateTime AuditoriaDate
        {
            get
            {
                return this.auditoria_date;
            }
            set
            {
                this.auditoria_date = value;
            }
        }

        /// <summary>
        /// Get or Set. Indica si la importacion de Socios esta Disponíble.
        /// </summary>
        public bool SociosImportacion
        {
            get
            {
                return this.socios_importacion;
            }
            set
            {
                this.socios_importacion = value;
            }
        }

        #endregion


        #region Constructors
        /// <summary>
        /// Bitacora de Aplicacion. Log4net
        /// </summary>
        private static object lockObj = new object();

        /// <summary>
        /// Constructor. Lee archivo XML de Configuración de Sistema en Cache de Aplicación. Inicializa miembros con información de Configuracion de Sistema.
        /// </summary>
        public ConfiguracionDeSistemaLogic()
        {
            try
            {
                string strConfigurationPath = System.Configuration.ConfigurationManager.AppSettings.Get("configuracionDeSistemaXML");
                var varConfigXML = HttpContext.Current.Cache.Get("configuracionDeSistemaXML");

                if (varConfigXML == null)
                {
                    lock (lockObj)
                    {
                        Configuracion = new XmlDocument();
                        Configuracion.Load(HttpContext.Current.Server.MapPath(strConfigurationPath));

                        HttpContext.Current.Cache.Insert("configuracionDeSistemaXML", Configuracion,
                            new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath(strConfigurationPath)));
                    }
                }
                else
                    Configuracion = varConfigXML as XmlDocument;

                string path = System.Configuration.ConfigurationManager.AppSettings.Get("configuracionDeSistemaXML");
                Configuracion = new XmlDocument();
                Configuracion.Load(System.Web.HttpContext.Current.Server.MapPath(path));

                this.LoadMembers();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al construir ConfiguracionDeSistemaLogic.", ex);
                throw;
            }
        }

        /// <summary>
        /// Constructor. Inicializa miembros con información de Configuracion de Sistema.
        /// </summary>
        /// <param name="SysConfig"></param>
        public ConfiguracionDeSistemaLogic(XmlDocument SysConfig)
        {
            try
            {
                this.Configuracion = SysConfig;
                this.LoadMembers();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al construir ConfiguracionDeSistemaLogic.", ex);
                throw;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Encriptar Texto usando TripleDES
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Texto Encriptado</returns>
        private string encryptTDES(string plainText)
        {
            try
            {
                Seguridad.CriptografiaTDES des = new Seguridad.CriptografiaTDES(key, vector);
                return des.EncondeString(plainText);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al encriptar TDES.", ex);
                throw;
            }
        }
        /// <summary>
        /// Desencriptar Texto usando TripleDES
        /// </summary>
        /// <param name="cypherText"></param>
        /// <returns>Texto Desencriptado</returns>
        private string dencryptTDES(string cypherText)
        {
            try
            {
                Seguridad.CriptografiaTDES des = new Seguridad.CriptografiaTDES(key, vector);
                return des.DecodeString(cypherText);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al desencriptar AES.", ex);
                throw;
            }
        }

        /// <summary>
        /// Cargar miembros privados con informacion de XML privado.
        /// </summary>
        private void LoadMembers()
        {
            try
            {
                string strSociosImportacion = Configuracion.SelectSingleNode("configuracion/socios/importacionInicial").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                string strMaximizar = Configuracion.SelectSingleNode("configuracion/ventanas/MaximizarAlCargar").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strCargarDatos = Configuracion.SelectSingleNode("configuracion/ventanas/CargarDatosAlInicio").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                string strConsolidadoInicio = Configuracion.SelectSingleNode("configuracion/consolidadoInventario/InicioPeriodo").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strConsolidadoFinal = Configuracion.SelectSingleNode("configuracion/consolidadoInventario/FinalPeriodo").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                string strCorreoLocal = Configuracion.SelectSingleNode("configuracion/correo/correoLocal").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strCorreoUsarPassword = Configuracion.SelectSingleNode("configuracion/correo/usarPassword").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strCorreoPassword = Configuracion.SelectSingleNode("configuracion/correo/password").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strCorreoSMTP = Configuracion.SelectSingleNode("configuracion/correo/smtp").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strCorreoPuerto = Configuracion.SelectSingleNode("configuracion/correo/puerto").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strCorreoUsarSSL = Configuracion.SelectSingleNode("configuracion/correo/usarSSL").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();

                string strAuditoriaUsername = Configuracion.SelectSingleNode("configuracion/auditoria/username").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();
                string strAuditoriaDate = Configuracion.SelectSingleNode("configuracion/auditoria/date").InnerText.Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Trim();


                bool.TryParse(strSociosImportacion, out this.socios_importacion);

                bool.TryParse(strMaximizar, out this.ventana_Maximizar);
                bool.TryParse(strCargarDatos, out this.ventana_CargarDatos);

                DateTime.TryParse(strConsolidadoInicio, out this.consolidado_InicioPeriodo);
                DateTime.TryParse(strConsolidadoFinal, out this.consolidado_FinalPeriodo);

                this.correo_CorreoLocal = strCorreoLocal;
                bool.TryParse(strCorreoUsarPassword, out this.correo_UsarPassword);
                this.correo_Password = string.IsNullOrEmpty(strCorreoPassword) ? "" : this.dencryptTDES(strCorreoPassword);
                this.correo_SMTP = strCorreoSMTP;
                int.TryParse(strCorreoPuerto, out this.correo_Puerto);
                bool.TryParse(strCorreoUsarSSL, out this.correo_UsarSSL);

                this.auditoria_username = strAuditoriaUsername;
                DateTime.TryParse(strAuditoriaDate, out this.auditoria_date);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar propiedades", ex);
                throw;
            }
        }

        /// <summary>
        /// Guardar miembros privados actualizando informacion de XML privado.
        /// </summary>
        public void SaveMembers()
        {
            try
            {
                string passwrd = string.IsNullOrEmpty(this.correo_Password) ? "" : this.encryptTDES(this.correo_Password);

                Configuracion.SelectSingleNode("configuracion/socios/importacionInicial").InnerText = this.socios_importacion.ToString();

                Configuracion.SelectSingleNode("configuracion/ventanas/MaximizarAlCargar").InnerText = this.ventana_Maximizar.ToString();
                Configuracion.SelectSingleNode("configuracion/ventanas/CargarDatosAlInicio").InnerText = this.ventana_CargarDatos.ToString();

                Configuracion.SelectSingleNode("configuracion/consolidadoInventario/InicioPeriodo").InnerText = this.consolidado_InicioPeriodo.ToShortDateString();
                Configuracion.SelectSingleNode("configuracion/consolidadoInventario/FinalPeriodo").InnerText = this.consolidado_FinalPeriodo.ToShortDateString();

                Configuracion.SelectSingleNode("configuracion/correo/correoLocal").InnerText = this.correo_CorreoLocal;
                Configuracion.SelectSingleNode("configuracion/correo/usarPassword").InnerText = this.correo_UsarPassword.ToString();
                Configuracion.SelectSingleNode("configuracion/correo/password").InnerText = passwrd;
                Configuracion.SelectSingleNode("configuracion/correo/smtp").InnerText = this.correo_SMTP;
                Configuracion.SelectSingleNode("configuracion/correo/puerto").InnerText = this.correo_Puerto.ToString();
                Configuracion.SelectSingleNode("configuracion/correo/usarSSL").InnerText = this.correo_UsarSSL.ToString();

                Configuracion.SelectSingleNode("configuracion/auditoria/username").InnerText = this.auditoria_username;
                Configuracion.SelectSingleNode("configuracion/auditoria/date").InnerText = this.auditoria_date.ToShortDateString();

                string SavePath = System.Configuration.ConfigurationManager.AppSettings.Get("configuracionDeSistemaXML");

                Configuracion.Save(HttpContext.Current.Server.MapPath(SavePath));
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al guardar propiedades", ex);
                throw;
            }
        }

        #endregion
    }
}
