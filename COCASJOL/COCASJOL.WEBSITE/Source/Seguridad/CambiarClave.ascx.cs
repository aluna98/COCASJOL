using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using COCASJOL.LOGIC.Seguridad; 
using COCASJOL.LOGIC.Web;

using System.Data;
using System.Data.Objects;
using log4net;

namespace COCASJOL.WEBSITE.Source.Seguridad
{
    public partial class CambiarClave : System.Web.UI.UserControl
    {
        private static ILog log = LogManager.GetLogger(typeof(CambiarClave).Name);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [DirectMethod(RethrowException=true)]
        public void CargarClave()
        {
            try
            {
                string loggedUsr = Session["username"] as string;

                if (string.IsNullOrEmpty(loggedUsr))
                    return;

                if (loggedUsr.CompareTo("DEVELOPER") != 0)
                {
                    UsuarioLogic usuariologic = new UsuarioLogic();

                    COCASJOL.LOGIC.usuario user = usuariologic.GetUsuario(loggedUsr);

                    this.CambiarClaveUsernameTxt.Text = loggedUsr;
                    this.CambiarClaveActualTxt.Text = user.USR_PASSWORD;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar control de cambiar clave.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void CambiarClaveGuardarBtn_Click()
        {
            try
            {
                string loggedUsr = Session["username"] as string;

                UsuarioLogic usuariologic = new UsuarioLogic();
                usuariologic.ActualizarClave(this.CambiarClaveUsernameTxt.Text, this.CambiarClaveNuevaConfirmarTxt.Text, loggedUsr);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al actualizar clave.", ex);
                throw;
            }
        }
    }
}