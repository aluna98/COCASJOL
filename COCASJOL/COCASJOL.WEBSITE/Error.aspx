<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string currentPage = base.Request.QueryString["aspxerrorpath"];
            ExtNet.Msg.Show(new MessageBoxConfig
            {
                Title = "Error",
                Message = "Ocurrio un error inesperado.",
                Buttons = MessageBox.Button.OK, 
                Handler = "window.location = '" + currentPage + "'"
            });
        }
        catch (Exception ex)
        {
            //log
            throw;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
