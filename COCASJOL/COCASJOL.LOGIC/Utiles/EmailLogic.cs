using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.Objects;

using COCASJOL.LOGIC;
using COCASJOL.LOGIC.Seguridad;

namespace COCASJOL.LOGIC.Utiles
{
    public class EmailLogic
    {
        public static void EnviarCorreoRolNuevo(string USR_USERNAME, int ROL_ID)
        {
            try
            {
                UsuarioLogic usuariologica = new UsuarioLogic();
                usuario user = usuariologica.GetUsuario(USR_USERNAME);

                string mailto = user.USR_CORREO;
                string nombre = user.USR_NOMBRE + user.USR_APELLIDO;

                string rol = "";
                string privs = "";

                string subject = "";
                string message = ""; 

                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);
                    var r = db.GetObjectByKey(k);
                    rol role = (rol)r;

                    rol = role.ROL_NOMBRE + " - " + role.ROL_DESCRIPCION;

                    foreach (privilegio p in role.privilegios)
                        privs += p.PRIV_NOMBRE + ", ";
                }

                message = message.Replace("{NOMBRE}", nombre);
                message = message.Replace("{USUARIO}", USR_USERNAME);
                message = message.Replace("{ROL}", rol);
                message = message.Replace("{PRIVILEGIOS}", privs);

                string strUsePassword = System.Configuration.ConfigurationManager.AppSettings.Get("CorreoUsarPassword");
                string mailfrom = System.Configuration.ConfigurationManager.AppSettings.Get("CorreoLocal");
                string host = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_SERVER");

                bool usePassword = Convert.ToBoolean(strUsePassword);

                if (usePassword)
                {
                    string fromPassword = System.Configuration.ConfigurationManager.AppSettings.Get("CorreoLocalPassword");
                    sendMail(mailto, mailfrom, fromPassword, message, subject, host);
                }
                else
                    sendMail(mailto, mailfrom, message, subject, host);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static void EnviarCorreoPrivilegioNuevo(string USR_USERNAME, int PRIV_ID)
        {
            try
            {
                UsuarioLogic usuariologica = new UsuarioLogic();
                usuario user = usuariologica.GetUsuario(USR_USERNAME);

                string mailto = user.USR_CORREO;
                string nombre = user.USR_NOMBRE + user.USR_APELLIDO;

                string priv = "";

                string subject = "";
                string message = "";

                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.privilegios", "PRIV_ID", PRIV_ID);

                    var p = db.GetObjectByKey(k);

                    privilegio priv2 = (privilegio)p;

                    priv = priv2.PRIV_NOMBRE;
                }

                message = message.Replace("{NOMBRE}", nombre);
                message = message.Replace("{USUARIO}", USR_USERNAME);
                message = message.Replace("{PRIVILEGIO}", priv);

                string strUsePassword = System.Configuration.ConfigurationManager.AppSettings.Get("CorreoUsarPassword");
                string mailfrom = System.Configuration.ConfigurationManager.AppSettings.Get("CorreoLocal");
                string host = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_SERVER");

                bool usePassword = Convert.ToBoolean(strUsePassword);

                if (usePassword)
                {
                    string fromPassword = System.Configuration.ConfigurationManager.AppSettings.Get("CorreoLocalPassword");
                    sendMail(mailto, mailfrom, fromPassword, message, subject, host);
                }
                else
                    sendMail(mailto, mailfrom, message, subject, host);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private static void sendMail(string to, string from, string message, string subject, string server)
        {
            try
            {
                MailMessage correo = new MailMessage(from, to, subject, message);
                correo.IsBodyHtml = true;
                SmtpClient smtpcliente = new SmtpClient(server);
                smtpcliente.Send(correo);
                correo.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void sendMail(string to, string from, string fromPassword, string message, string subject, string server)
        {
            try
            {
                MailMessage correo = new MailMessage(from, to, subject, message);
                correo.IsBodyHtml = true;
                SmtpClient smtpcliente = new SmtpClient 
                {
                    Host = server,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from, fromPassword)
                };
                smtpcliente.Send(correo);
                correo.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
