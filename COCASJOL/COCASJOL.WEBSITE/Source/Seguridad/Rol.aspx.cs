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
    public partial class Rol : COCASJOLBASE //System.Web.UI.Page
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
                //log
                throw;
            }
        }

        protected void RolDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!this.IsPostBack)
                e.Cancel = true;
        }

        #region Privilegios

        protected void PrivilegiosDeRolSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                int rol_id = string.IsNullOrEmpty(this.EditIdTxt.Text) ? 0 : Convert.ToInt32(this.EditIdTxt.Text);

                RolLogic rollogic = new RolLogic();
                int priv_id = string.IsNullOrEmpty(this.f_PRIV_ID.Text) ? 0 : Convert.ToInt32(this.f_PRIV_ID.Text);
                this.PrivilegiosDeRolSt.DataSource = rollogic.GetPrivilegios(rol_id, priv_id, this.f_ROL_NOMBRE.Text, this.f_PRIV_LLAVE.Text);
                this.PrivilegiosDeRolSt.DataBind();
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void EditRolDeletePrivilegioBtn_Click()
        {
            try
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
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void PrivilegiosNoDeRolesSt_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            try
            {
                int rol_id = Convert.ToInt32(this.EditIdTxt.Text);

                RolLogic rollogic = new RolLogic();
                int priv_id = string.IsNullOrEmpty(this.f2_PRIV_ID.Text) ? 0 : Convert.ToInt32(this.f2_PRIV_ID.Text);
                this.PrivilegiosNoDeRolesSt.DataSource = rollogic.GetPrivilegiosNoDeRol(rol_id, priv_id, this.f2_PRIV_NOMBRE.Text, this.f2_PRIV_LLAVE.Text);
                this.PrivilegiosNoDeRolesSt.DataBind();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void AddPrivilegiosAddPrivilegioBtn_Click()
        {
            try
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
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion
    }
}