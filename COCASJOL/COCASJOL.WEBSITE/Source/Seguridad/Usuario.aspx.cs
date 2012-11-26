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
using COCASJOL.LOGIC.Web;

namespace COCASJOL.WEBSITE.Source.Seguridad
{
    public partial class Usuario : COCASJOLBASE //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!X.IsAjaxRequest)
                {
                    string loggedUsr = Session["username"] as string;
                    this.LoggedUserHdn.Text = loggedUsr;

                    this.ValidarCredenciales("SYS_USER");
                }
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
                UsuarioLogic usuariologica = new UsuarioLogic();

                string user = this.EditUsernameTxt.Text;

                List<int> roles = new List<int>();

                foreach (SelectedRow row in this.RolesDeUsuarioSelectionM.SelectedRows)
                {
                    roles.Add(Convert.ToInt32(row.RecordID));
                }

                usuariologica.EliminarRoles(user, roles);

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
                string user = this.EditUsernameTxt.Text;

                List<int> roles = new List<int>();

                foreach (SelectedRow row in this.RolesNoDeUsuarioSelectionM.SelectedRows)
                {
                    roles.Add(Convert.ToInt32(row.RecordID));
                }

                UsuarioLogic usuariologica = new UsuarioLogic();
                usuariologica.InsertarRoles(user, roles);

                this.RolesNoDeUsuarioSelectionM.ClearSelections();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion
    }
}