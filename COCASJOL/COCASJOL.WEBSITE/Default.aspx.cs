using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COCASJOL.LOGIC.Seguridad;
using Ext.Net;
using log4net;

namespace COCASJOL.WEBSITE
{
    public partial class Default : System.Web.UI.Page
    {
        private static ILog log = LogManager.GetLogger(typeof(Default).Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string loggedUser = Session["username"] as string;

                if (!string.IsNullOrEmpty(loggedUser))
                {
                    this.Window1.Close();
                    Response.Redirect("~/Source/Desktop.aspx");
                }
            }
            catch (System.Threading.ThreadAbortException tex)
            {
                log.Warn("Error de terminacion de hilo al cargar pagina de login (Default.aspx). Nota: Este error pudo ser causado por Response.Redirect.", tex);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal cargar pagina de login (Default.aspx).", ex);
            }
        }

        [DirectMethod(RethrowException = true)]
        public void Button1_Click()
        {
            try
            {
                UsuarioLogic usuarioLogic = new UsuarioLogic();
                if (usuarioLogic.Autenticar(this.txtUsername.Text, this.txtPassword.Text) == true)
                {
                    Session["username"] = this.txtUsername.Text;

                    Window1.Close();
                    Response.Redirect("~/Source/Desktop.aspx");
                }
                else
                {
                    log.WarnFormat("Error al intentar autenticar usuario. Username: {0} - Password (Encriptada): {1} .", this.txtUsername.Text, this.txtPassword.Text);

                    this.txtUsername.Clear();
                    this.txtPassword.Clear();
                    X.Msg.Alert("Inicio de Sesión", "El nombre de usuario o contraseña son incorrectos.", "#{txtUsername}.focus();").Show();
                }
            }
            catch (System.Threading.ThreadAbortException tex)
            {
                log.Warn("Error de terminacion de hilo al intentar autenticar usuario. Nota: Este error pudo ser causado por Response.Redirect.", tex);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al intentar autenticar usuario.", ex);
            }
        }
    }
}