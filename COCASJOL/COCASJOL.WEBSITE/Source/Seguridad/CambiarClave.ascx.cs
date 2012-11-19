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
    public partial class CambiarClave : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [DirectMethod]
        public void CargarClave()
        {
            string loggedUsr = Session["username"] as string;

            if (loggedUsr.CompareTo("DEVELOPER") != 0)
            {
                UsuarioLogic usuariologic = new UsuarioLogic();

                COCASJOL.LOGIC.usuario user = usuariologic.GetUsuario(loggedUsr);

                this.CambiarClaveUsernameTxt.Text = loggedUsr;
                this.CambiarClaveActualTxt.Text = user.USR_PASSWORD;
            }
        }

        [DirectMethod]
        public void CambiarClaveGuardarBtn_Click()
        {
            string loggedUsr = Session["username"] as string;

            UsuarioLogic usuariologic = new UsuarioLogic();
            usuariologic.ActualizarClave(this.CambiarClaveUsernameTxt.Text, this.CambiarClaveNuevaConfirmarTxt.Text, loggedUsr);
        }
    }
}