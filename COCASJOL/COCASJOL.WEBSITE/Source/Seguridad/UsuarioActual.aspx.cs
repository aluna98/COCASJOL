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
using COCASJOL.LOGIC;//para acceder al ado entity

namespace COCASJOL.Website.Source.Seguridad
{
    public partial class UsuarioActual : COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
        }

        [DirectMethod]
        public void CargarUsuario()
        {
            if (this.LoggedUserHdn.Text.CompareTo("DEVELOPER") != 0)
            {
                UsuarioLogic usuarioActual = new UsuarioLogic();
                usuario user = usuarioActual.GetUsuario(this.LoggedUserHdn.Text);

                this.EditUsernameTxt.Text = user.USR_USERNAME;
                this.EditNombreTxt.Text = user.USR_NOMBRE;
                this.EditApellidoTxt.Text = user.USR_APELLIDO;
                this.EditCedulaTxt.Text = user.USR_CEDULA;
                this.EditEmailTxt.Text = user.USR_CORREO;
                this.EditPuestoTxt.Text = user.USR_PUESTO;
            }
        }

        [DirectMethod]
        public void GuardarCambios()
        {
            if (this.LoggedUserHdn.Text.CompareTo("DEVELOPER") != 0)
            {
                UsuarioLogic usuarioActual = new UsuarioLogic();

                usuarioActual.ActualizarUsuario(
                    this.EditUsernameTxt.Text,
                    this.EditNombreTxt.Text,
                    this.EditApellidoTxt.Text,
                    this.EditCedulaTxt.Text,
                    this.EditEmailTxt.Text,
                    this.EditPuestoTxt.Text, this.LoggedUserHdn.Text);
            }
        }
    }
}