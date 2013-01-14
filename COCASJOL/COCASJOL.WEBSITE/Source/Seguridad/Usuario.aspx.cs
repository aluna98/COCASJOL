using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;

using COCASJOL.LOGIC.Seguridad;
using COCASJOL.LOGIC.Utiles;

namespace COCASJOL.WEBSITE.Source.Seguridad
{
    public partial class Usuario : COCASJOL.LOGIC.Web.COCASJOLBASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    
                }

                string loggedUsr = Session["username"] as string;
                this.LoggedUserHdn.Text = loggedUsr;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void UsuarioDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        #region Usuarios

        [DirectMethod(RethrowException = true)]
        public void CambiarClaveGuardarBtn_Click()
        {
            try
            {
                string user = this.CambiarClaveUsernameTxt.Text;

                UsuarioLogic usuariologic = new UsuarioLogic();

                usuariologic.ActualizarClave(user, this.CambiarClaveConfirmarTxt.Text, this.LoggedUserHdn.Text);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #region Validacion

        protected void AddUsernameTxt_Change(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string username = this.AddUsernameTxt.Text;

                UsuarioLogic usuariologic = new UsuarioLogic();
                if (usuariologic.UsuarioExiste(username))
                {
                    e.Success = false;
                    e.ErrorMessage = "El nombre de usuario ingresado ya existe.";
                }
                else
                    e.Success = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void AddCedulaTxt_Change(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string cedula = this.AddCedulaTxt.Text;
                UsuarioLogic usuarioLogic = new UsuarioLogic();
                if (usuarioLogic.CedulaExiste(cedula))
                {
                    e.Success = false;
                    e.ErrorMessage = "La cedula ingresada ya existe.";
                }
                else
                    e.Success = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void EditCedulaTxt_Change(object sender, RemoteValidationEventArgs e)
        {
            try
            {
                string cedula = this.EditCedulaTxt.Text;
                string username = this.EditUsernameTxt.Text;
                UsuarioLogic usuarioLogic = new UsuarioLogic();
                if (usuarioLogic.CedulaExiste(cedula, username))
                {
                    e.Success = false;
                    e.ErrorMessage = "La cedula ingresada ya existe.";
                }
                else
                    e.Success = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Roles

        protected void RolesDeUsuarioSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string user = this.EditUsernameTxt.Text;

                UsuarioLogic usuariologic = new UsuarioLogic();
                int rol_id = string.IsNullOrEmpty(this.f_ROL_ID.Text) ? 0 : Convert.ToInt32(this.f_ROL_ID.Text);
                this.RolesDeUsuarioSt.DataSource = usuariologic.GetRoles(user, rol_id, this.f_ROL_NOMBRE.Text);
                this.RolesDeUsuarioSt.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void EditUsuarioDeleteRolBtn_Click()
        {
            try
            {
                string loggeduser = this.LoggedUserHdn.Text;
                string user = this.EditUsernameTxt.Text;

                List<int> roles = new List<int>();

                foreach (SelectedRow row in this.RolesDeUsuarioSelectionM.SelectedRows)
                {
                    roles.Add(Convert.ToInt32(row.RecordID));
                }

                UsuarioLogic usuariologica = new UsuarioLogic();
                usuariologica.EliminarRoles(user, roles, loggeduser);

                this.RolesDeUsuarioSelectionM.ClearSelections();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void RolesNoDeUsuarioSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                string user = this.EditUsernameTxt.Text;

                UsuarioLogic usuariologica = new UsuarioLogic();
                int rol_id = string.IsNullOrEmpty(this.f2_ROL_ID.Text) ? 0 : Convert.ToInt32(this.f2_ROL_ID.Text);
                this.RolesNoDeUsuarioSt.DataSource = usuariologica.GetRolesNoDeUsuario(user, rol_id, this.f2_ROL_NOMBRE.Text);
                this.RolesNoDeUsuarioSt.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void AddRolesAddRolBtn_Click()
        {
            try
            {
                string loggeduser = this.LoggedUserHdn.Text;
                string user = this.EditUsernameTxt.Text;

                List<int> roles = new List<int>();

                foreach (SelectedRow row in this.RolesNoDeUsuarioSelectionM.SelectedRows)
                {
                    roles.Add(Convert.ToInt32(row.RecordID));
                }

                UsuarioLogic usuariologica = new UsuarioLogic();
                usuariologica.InsertarRoles(user, roles, loggeduser);

                //this.RolesNoDeUsuarioSelectionM.ClearSelections();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Correos

        [DirectMethod(RethrowException = true)]
        public void EnviarCorreoUsuarioNuevo(string USR_PASSWORD)
        {
            try
            {
                string USR_USERNAME = this.AddUsernameTxt.Text;
                EmailLogic.EnviarCorreoUsuarioNuevo(USR_USERNAME, USR_PASSWORD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void EnviarCorreoUsuarioPasswordNuevo(string USR_PASSWORD)
        {
            try
            {
                string USR_USERNAME = this.CambiarClaveUsernameTxt.Text;
                EmailLogic.EnviarCorreoUsuarioPasswordNuevo(USR_USERNAME, USR_PASSWORD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [DirectMethod(RethrowException = true)]
        public void EnviarCorreoRolesNuevos()
        {
            try
            {
                string USR_USERNAME = this.EditUsernameTxt.Text;

                List<string> rolesList = this.RolesNoDeUsuarioSelectionM.SelectedRows.Select(s => s.RecordID).ToList<string>();

                foreach (string r in rolesList)
                    EmailLogic.EnviarCorreoRolNuevo(USR_USERNAME, Convert.ToInt32(r));
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}