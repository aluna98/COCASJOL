using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using log4net;

using COCASJOL.LOGIC.Configuracion;

namespace COCASJOL.WEBSITE.Source.Configuracion
{
    public partial class ConfiguracionDeSistema : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        private static ILog log = LogManager.GetLogger(typeof(ConfiguracionDeSistema).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    this.LoadConfiguration();
                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de configuracion de sistema.", ex);
                throw;
            }
        }

        private void LoadConfiguration()
        {
            try
            {
                ConfiguracionDeSistemaLogic configLogic = new ConfiguracionDeSistemaLogic(this.docConfiguracion);

                this.EditSociosImportacionChk.Checked = configLogic.SociosImportacion;
                
                this.EditVentanasMaximizarChk.Checked = configLogic.VentanasMaximizar;
                this.EditVentanasCargarDatosChk.Checked = configLogic.VentanasCargarDatos;

                this.EditConsolidadoFechaInicialTxt.Value = configLogic.ConsolidadoInventarioInicioPeriodo;
                this.EditConsolidadoFechaFinalTxt.Value = configLogic.ConsolidadoInventarioFinalPeriodo;

                this.EditCorreoLocalTxt.Text = configLogic.CorreoCorreoLocal;
                this.EditCorreoUsarPasswordChk.Checked = configLogic.CorreoUsarPassword;
                this.EditCorreoPasswordTxt.Text = configLogic.CorreoPassword;
                this.EditCorreoSmtpServerTxt.Text = configLogic.CorreoSMTP;
                this.EditCorreoPortTxt.Text = configLogic.CorreoPuerto.ToString();
                this.EditCorreoUsarSSLChk.Checked = configLogic.CorreoUsarSSL;

                this.AuditUserName.Text = configLogic.AuditoriaUserName;
                this.AuditDate.Value = configLogic.AuditoriaDate;
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar configuracion de sistema.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void GuardarConfiguracionBtn_Click()
        {
            try
            {
                string loggeduser = Session["username"] as string;

                ConfiguracionDeSistemaLogic configLogic = new ConfiguracionDeSistemaLogic(this.docConfiguracion);

                configLogic.SociosImportacion = this.EditSociosImportacionChk.Checked;

                configLogic.VentanasMaximizar = this.EditVentanasMaximizarChk.Checked;
                configLogic.VentanasCargarDatos = this.EditVentanasCargarDatosChk.Checked;

                configLogic.ConsolidadoInventarioInicioPeriodo = Convert.ToDateTime(this.EditConsolidadoFechaInicialTxt.Text);
                configLogic.ConsolidadoInventarioFinalPeriodo = Convert.ToDateTime(this.EditConsolidadoFechaFinalTxt.Text);

                configLogic.CorreoCorreoLocal = this.EditCorreoLocalTxt.Text;
                configLogic.CorreoUsarPassword = this.EditCorreoUsarPasswordChk.Checked;
                configLogic.CorreoPassword = this.EditCorreoPasswordTxt.Text;
                configLogic.CorreoSMTP = this.EditCorreoSmtpServerTxt.Text;
                configLogic.CorreoPuerto = Convert.ToInt32(this.EditCorreoPortTxt.Text);
                configLogic.CorreoUsarSSL = this.EditCorreoUsarSSLChk.Checked;

                configLogic.AuditoriaUserName = loggeduser;
                configLogic.AuditoriaDate = DateTime.Today;

                configLogic.SaveMembers();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al guardar configuracion de sistema.", ex);
                throw;
            }
        }
    }
}