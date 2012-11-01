using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;

using System.Data;
using System.Data.Objects;
using COCASJOL.LOGIC;

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

        #region Roles

        protected void RolesDeUsuarioSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            string user = this.EditUsernameTxt.Text;

            UsuarioLogic usuariologic = new UsuarioLogic();
            this.RolesDeUsuarioSt.DataSource = usuariologic.GetRoles(user, this.f_ROL_ID.Text, this.f_ROL_NOMBRE.Text, this.f_ROL_DESCRIPCION.Text);
            this.RolesDeUsuarioSt.DataBind();
        }

        protected void RolesDeUsuarioDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        protected void EditUsuarioDeleteRolBtn_Click(object sender, DirectEventArgs e)
        {
            UsuarioLogic usuariologica = new UsuarioLogic();

            string user = this.EditUsernameTxt.Text;

            List<string> roles = new List<string>();

            foreach (SelectedRow row in this.RolesDeUsuarioSelectionM.SelectedRows)
            {
                roles.Add(row.RecordID);
            }

            usuariologica.EliminarRoles(user, roles);

            this.RolesDeUsuarioSelectionM.ClearSelections();
        }

        protected void RolesNoDeUsuarioSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            string user = this.EditUsernameTxt.Text;

            UsuarioLogic usuariologica = new UsuarioLogic();
            this.RolesNoDeUsuarioSt.DataSource = usuariologica.GetRolesNoDeUsuario(user, this.f2_ROL_ID.Text, this.f2_ROL_NOMBRE.Text, this.f2_ROL_DESCRIPCION.Text);
            this.RolesNoDeUsuarioSt.DataBind();
        }

        protected void AddRolesAddRolBtn_Click(object sender, DirectEventArgs e)
        {
            UsuarioLogic usuariologica = new UsuarioLogic();

            string user = this.EditUsernameTxt.Text;

            List<string> roles = new List<string>();

            foreach (SelectedRow row in this.RolesNoDeUsuarioSelectionM.SelectedRows)
            {
                roles.Add(row.RecordID);
            }

            usuariologica.InsertarRoles(user, roles);

            this.RolesNoDeUsuarioSelectionM.ClearSelections();
        }

        #endregion
    }
}