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

using System.Xml;

using log4net;

namespace COCASJOL.LOGIC.Utiles
{
    public class EmailLogic
    {
        private static ILog log = LogManager.GetLogger(typeof(EmailLogic).Name);

        public static void EnviarCorreoUsuarioNuevo(string USR_USERNAME, string USR_PASSWORD, XmlDocument Configuracion)
        {
            try
            {
                UsuarioLogic usuariologica = new UsuarioLogic();
                usuario user = usuariologica.GetUsuario(USR_USERNAME);

                string mailto = user.USR_CORREO;
                string nombre = user.USR_NOMBRE + " " + user.USR_APELLIDO;

                string subject = "";
                string message = "";

                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.plantillas_notificaciones", "PLANTILLAS_LLAVE", "USUARIONUEVO");
                    var pl = db.GetObjectByKey(k);
                    plantilla_notificacion plantilla = (plantilla_notificacion)pl;

                    subject = plantilla.PLANTILLAS_ASUNTO;
                    message = plantilla.PLANTILLAS_MENSAJE;
                }

                message = message.Replace("{NOMBRE}", nombre);
                message = message.Replace("{USUARIO}", USR_USERNAME);
                message = message.Replace("{CONTRASEÑA}", USR_PASSWORD);

                EnviarCorreo(mailto, subject, message, Configuracion);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al enviar correo de usuario nuevo.", ex);
                throw;
            }
        }

        public static void EnviarCorreoUsuarioPasswordNuevo(string USR_USERNAME, string USR_PASSWORD, XmlDocument Configuracion)
        {
            try
            {
                UsuarioLogic usuariologica = new UsuarioLogic();
                usuario user = usuariologica.GetUsuario(USR_USERNAME);

                string mailto = user.USR_CORREO;
                string nombre = user.USR_NOMBRE + " " + user.USR_APELLIDO;

                string subject = "";
                string message = "";

                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.plantillas_notificaciones", "PLANTILLAS_LLAVE", "PASSWORDNUEVO");
                    var pl = db.GetObjectByKey(k);
                    plantilla_notificacion plantilla = (plantilla_notificacion)pl;

                    subject = plantilla.PLANTILLAS_ASUNTO;
                    message = plantilla.PLANTILLAS_MENSAJE;
                }

                message = message.Replace("{NOMBRE}", nombre);
                message = message.Replace("{USUARIO}", USR_USERNAME);
                message = message.Replace("{CONTRASEÑA}", USR_PASSWORD);

                EnviarCorreo(mailto, subject, message, Configuracion);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al enviar correo de password nuevo.", ex);
                throw;
            }
        }

        public static void EnviarCorreoRolNuevo(string USR_USERNAME, int ROL_ID, XmlDocument Configuracion)
        {
            try
            {
                UsuarioLogic usuariologica = new UsuarioLogic();
                usuario user = usuariologica.GetUsuario(USR_USERNAME);

                string mailto = user.USR_CORREO;
                string nombre = user.USR_NOMBRE + " " + user.USR_APELLIDO;

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

                    if (privs.Length > 2)
                        privs.Remove(privs.Length - 2);


                    EntityKey k2 = new EntityKey("colinasEntities.plantillas_notificaciones", "PLANTILLAS_LLAVE", "ROLNUEVO");
                    var pl = db.GetObjectByKey(k2);
                    plantilla_notificacion plantilla = (plantilla_notificacion)pl;

                    subject = plantilla.PLANTILLAS_ASUNTO;
                    message = plantilla.PLANTILLAS_MENSAJE;
                }

                message = message.Replace("{NOMBRE}", nombre);
                message = message.Replace("{USUARIO}", USR_USERNAME);
                message = message.Replace("{ROL}", rol);
                message = message.Replace("{PRIVILEGIOS}", privs);

                EnviarCorreo(mailto, subject, message, Configuracion);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al enviar correo de rol nuevo.", ex);
                throw;
            }
        }

        public static void EnviarCorreosPrivilegiosNuevos(int ROL_ID, List<string> PRIVS_ID, XmlDocument Configuracion)
        {
            try
            {
                string mailto = "";
                string nombre = "";

                string priv = "";

                string subject = "";
                string message = "";

                using (var db = new colinasEntities())
                {
                    EntityKey k = new EntityKey("colinasEntities.roles", "ROL_ID", ROL_ID);

                    var r = db.GetObjectByKey(k);

                    rol role = (rol)r;

                    foreach (string privRec in PRIVS_ID)
                    {
                        int PRIV_ID = Convert.ToInt32(privRec);

                        EntityKey k2 = new EntityKey("colinasEntities.privilegios", "PRIV_ID", PRIV_ID);

                        var p = db.GetObjectByKey(k2);

                        privilegio priv2 = (privilegio)p;

                        priv += priv2.PRIV_NOMBRE + ", ";
                    }

                    if (priv.Length > 2)
                        priv.Remove(priv.Length - 2);

                    EntityKey k3 = new EntityKey("colinasEntities.plantillas_notificaciones", "PLANTILLAS_LLAVE", "PRIVILEGIONUEVO");
                    var pl = db.GetObjectByKey(k3);
                    plantilla_notificacion plantilla = (plantilla_notificacion)pl;

                    subject = plantilla.PLANTILLAS_ASUNTO;
                    message = plantilla.PLANTILLAS_MENSAJE;

                    foreach (usuario user in role.usuarios)
                    {
                        mailto = user.USR_CORREO;
                        nombre = user.USR_NOMBRE + " " + user.USR_APELLIDO;

                        message = message.Replace("{NOMBRE}", nombre);
                        message = message.Replace("{USUARIO}", user.USR_USERNAME);
                        message = message.Replace("{PRIVILEGIO}", priv);

                        EnviarCorreo(mailto, subject, message, Configuracion);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al enviar correo de privilegios nuevos.", ex);
                throw;
            }
        }

        private static void EnviarCorreo(string mailto, string subject, string message, XmlDocument Configuracion)
        {
            try
            {
                Configuracion.ConfiguracionDeSistemaLogic configLogic = new Configuracion.ConfiguracionDeSistemaLogic(Configuracion);

                string mailfrom = configLogic.CorreoCorreoLocal;
                string host = configLogic.CorreoSMTP;

                bool usePassword = configLogic.CorreoUsarPassword;

                if (usePassword)
                {
                    string fromPassword = configLogic.CorreoPassword;
                    int port = configLogic.CorreoPuerto;
                    bool enableSSL = configLogic.CorreoUsarSSL;

                    sendMail(mailto, mailfrom, fromPassword, message, subject, host, port, enableSSL);
                }
                else
                    sendMail(mailto, mailfrom, message, subject, host);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al preparar correo para envio.", ex);
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
            catch (SmtpException smtpex)
            {
                log.Error("Error de smtp al enviar correo.", smtpex);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al enviar correo.", ex);
                throw;
            }
        }

        private static void sendMail(string to, string from, string fromPassword, string message, string subject, string server, int port, bool enableSSL)
        {
            try
            {
                MailMessage correo = new MailMessage(from, to, subject, message);
                correo.IsBodyHtml = true;
                SmtpClient smtpcliente = new SmtpClient 
                {
                    Host = server,
                    Port = port,
                    EnableSsl = enableSSL,
                    Credentials = new NetworkCredential(from, fromPassword)
                };
                smtpcliente.Send(correo);
                correo.Dispose();
            }
            catch (SmtpException smtpex)
            {
                log.Error("Error de smtp al enviar correo.", smtpex);
            }
            catch (Exception ex)
            {
                log.Fatal("Error fatal al enviar correo.", ex);
                throw;
            }
        }
    }
}
