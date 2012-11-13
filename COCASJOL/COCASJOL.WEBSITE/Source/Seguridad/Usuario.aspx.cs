using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Objects;
using Ext.Net;
using COCASJOL.LOGIC.Security;

namespace COCASJOL.WEBSITE.Source.Seguridad
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string loggedUsr = "DEVELOPER";
                
#if !DEBUG
                loggedUsr = Session["username"] as string;
#endif
                this.LoggedUserHdn.Text = loggedUsr;
            }
        }

        protected void UsuarioDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        #region Roles

        protected void RolesDeUsuarioSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            string user = this.EditUsernameTxt.Text;

            UsuarioLogic usuariologic = new UsuarioLogic();
            int rol_id = string.IsNullOrEmpty(this.f_ROL_ID.Text) ? 0 : Convert.ToInt32(this.f_ROL_ID.Text);
            this.RolesDeUsuarioSt.DataSource = usuariologic.GetRoles(user, rol_id, this.f_ROL_NOMBRE.Text, this.f_ROL_DESCRIPCION.Text);
            this.RolesDeUsuarioSt.DataBind();
        }

        [DirectMethod]
        public void EditUsuarioDeleteRolBtn_Click()
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

        protected void RolesNoDeUsuarioSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            string user = this.EditUsernameTxt.Text;

            UsuarioLogic usuariologica = new UsuarioLogic();
            int rol_id = string.IsNullOrEmpty(this.f2_ROL_ID.Text) ? 0 : Convert.ToInt32(this.f2_ROL_ID.Text);
            this.RolesNoDeUsuarioSt.DataSource = usuariologica.GetRolesNoDeUsuario(user, rol_id, this.f2_ROL_NOMBRE.Text, this.f2_ROL_DESCRIPCION.Text);
            this.RolesNoDeUsuarioSt.DataBind();
        }

        [DirectMethod]
        public void AddRolesAddRolBtn_Click()
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

        #endregion
    }
}