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
using COCASJOL.LOGIC.Web;

using log4net;

namespace COCASJOL.WEBSITE.Source.Seguridad
{ 
    public partial class Roles : COCASJOLBASE //System.Web.UI.Page
    {
        private static ILog log = LogManager.GetLogger(typeof(Roles).Name);

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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar pagina de roles.", ex);
                throw;
            }
        }

        protected void RolDS_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic configLogic = new COCASJOL.LOGIC.Configuracion.ConfiguracionDeSistemaLogic(this.docConfiguracion);
                    if (configLogic.VentanasCargarDatos == true)
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar roles.", ex);
                throw;
            }
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar privilegios de rol.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void EditRolDeletePrivilegioBtn_Click()
        {
            try
            {
                string loggeduser = this.LoggedUserHdn.Text;
                RolLogic rollogic = new RolLogic();

                int rol_id = Convert.ToInt32(this.EditIdTxt.Text);

                List<int> privs = new List<int>();

                foreach (SelectedRow row in this.PrivilegiosDeRolSelectionM.SelectedRows)
                {
                    privs.Add(Convert.ToInt32(row.RecordID));
                }

                rollogic.EliminarPrivilegios(rol_id, privs, loggeduser);

                this.PrivilegiosDeRolSelectionM.ClearSelections();
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al eliminar privilegios de rol.", ex);
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
            catch (Exception ex)
            {
                log.Fatal("Error fatal al cargar privilegios no de rol.", ex);
                throw;
            }
        }

        [DirectMethod(RethrowException=true)]
        public void AddPrivilegiosAddPrivilegioBtn_Click()
        {
            try
            {
                string loggeduser = this.LoggedUserHdn.Text;
                int rol_id = Convert.ToInt32(this.EditIdTxt.Text);

                List<int> privs = new List<int>();

                foreach (SelectedRow row in this.PrivilegiosNoDeRolSelectionM.SelectedRows)
                {
                    privs.Add(Convert.ToInt32(row.RecordID));
                }


                RolLogic rollogic = new RolLogic();
                rollogic.InsertarPrivilegios(rol_id, privs, loggeduser);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al agregar privilegios de rol.", ex);
                throw;
            }
        }

        #endregion

        #region Correo

        [DirectMethod(RethrowException = true)]
        public void EnviarCorreoPrivilegiosNuevos()
        {
            try
            {
                int ROL_ID = Convert.ToInt32(this.EditIdTxt.Value);
                List<string> privsList = this.PrivilegiosNoDeRolSelectionM.SelectedRows.Select(s => s.RecordID).ToList<string>();

                EmailLogic.EnviarCorreosPrivilegiosNuevos(Convert.ToInt32(ROL_ID), privsList, this.docConfiguracion);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al enviar correo de rol nuevo.", ex);
                throw;
            }
        }

        #endregion
    }
}