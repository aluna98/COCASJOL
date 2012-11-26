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

namespace COCASJOL.Website.Source.Seguridad
{
    public partial class UsuarioActual : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [DirectMethod(RethrowException = true)]
        public void CargarUsuario()
        {
            try
            {
                string loggedUsr = Session["username"] as string;
                this.EditUsernameTxt.Text = loggedUsr;

                if (loggedUsr.CompareTo("DEVELOPER") != 0)
                {
                    UsuarioLogic usuarioActual = new UsuarioLogic();
                    COCASJOL.LOGIC.usuario user = usuarioActual.GetUsuario(loggedUsr);

                    this.EditNombreTxt.Text = user.USR_NOMBRE;
                    this.EditSegundoNombreTxt.Text = user.USR_SEGUNDO_NOMBRE;
                    this.EditApellidoTxt.Text = user.USR_APELLIDO;
                    this.EditSegundoApellidoTxt.Text = user.USR_SEGUNDO_APELLIDO;
                    this.EditEmailTxt.Text = user.USR_CORREO;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void GuardarCambios()
        {
            try
            {
                string loggedUsr = Session["username"] as string;

                if (loggedUsr.CompareTo("DEVELOPER") != 0)
                {
                    UsuarioLogic usuarioActual = new UsuarioLogic();

                    usuarioActual.ActualizarUsuario(
                        this.EditUsernameTxt.Text,
                        this.EditNombreTxt.Text,
                        this.EditSegundoNombreTxt.Text,
                        this.EditApellidoTxt.Text,
                        this.EditSegundoApellidoTxt.Text,
                        this.EditEmailTxt.Text,
                        loggedUsr);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}