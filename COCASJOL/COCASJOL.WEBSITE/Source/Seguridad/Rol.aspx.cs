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
    public partial class Rol : System.Web.UI.Page
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

        protected void PrivilegiosDeRolSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            int rol_id = string.IsNullOrEmpty(this.EditIdTxt.Text) ? 0 : Convert.ToInt32(this.EditIdTxt.Text);

            RolLogic rollogic = new RolLogic();
            int priv_id = string.IsNullOrEmpty(this.f_PRIV_ID.Text) ? 0 : Convert.ToInt32(this.f_PRIV_ID.Text);
            this.PrivilegiosDeRolSt.DataSource = rollogic.GetPrivilegios(rol_id, priv_id, this.f_ROL_NOMBRE.Text, this.f_ROL_DESCRIPCION.Text, this.f_PRIV_LLAVE.Text);
            this.PrivilegiosDeRolSt.DataBind();
        }

        [DirectMethod]
        public void EditRolDeletePrivilegioBtn_Click()
        {
            RolLogic rollogic = new RolLogic();

            int rol_id = Convert.ToInt32(this.EditIdTxt.Text);

            List<int> privs = new List<int>();

            foreach (SelectedRow row in this.PrivilegiosDeRolSelectionM.SelectedRows)
            {
                privs.Add(Convert.ToInt32(row.RecordID));
            }

            rollogic.EliminarPrivilegios(rol_id, privs);

            this.PrivilegiosDeRolSelectionM.ClearSelections();
        }

        protected void PrivilegiosNoDeRolesSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            int rol_id = Convert.ToInt32(this.EditIdTxt.Text);

            RolLogic rollogic = new RolLogic();
            int priv_id = string.IsNullOrEmpty(this.f2_PRIV_ID.Text) ? 0 : Convert.ToInt32(this.f2_PRIV_ID.Text);
            this.PrivilegiosNoDeRolesSt.DataSource = rollogic.GetPrivilegiosNoDeRol(rol_id, priv_id, this.f2_PRIV_NOMBRE.Text, this.f2_PRIV_DESCRIPCION.Text, this.f2_PRIV_LLAVE.Text);
            this.PrivilegiosNoDeRolesSt.DataBind();
        }

        [DirectMethod]
        public void AddPrivilegiosAddPrivilegioBtn_Click()
        {

            int rol_id = Convert.ToInt32(this.EditIdTxt.Text);

            List<int> privs = new List<int>();

            foreach (SelectedRow row in this.PrivilegiosNoDeRolSelectionM.SelectedRows)
            {
                privs.Add(Convert.ToInt32(row.RecordID));
            }


            RolLogic rollogic = new RolLogic();
            rollogic.InsertarPrivilegios(rol_id, privs);

            this.PrivilegiosNoDeRolSelectionM.ClearSelections();
        }

        #endregion
    }
}