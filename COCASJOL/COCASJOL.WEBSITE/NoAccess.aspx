<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string title = "Error de Acceso", message = "Usted no tiene los accesos necesarios para este recurso, para obtener acceso contactese con el administrador del sistema.", currentPage = Request.QueryString["aspxerrorpath"];

            log.ErrorFormat("{0}. Se intento acceder a una pagina a la cual no tienen el acceso necesario. URL Actual: {1}", title, currentPage);
            
            ExtNet.Msg.Show(new MessageBoxConfig
            {
                Title = title,
                Message = message,
                Closable = false
            });
        }
        catch (Exception ex)
        {
            log.Fatal("Error fatal al cargar pagina de errores de accesos.", ex);
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colinas</title>
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon"/>
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
        </ext:ResourceManager>
    </div>
    </form>
</body>
</html>
