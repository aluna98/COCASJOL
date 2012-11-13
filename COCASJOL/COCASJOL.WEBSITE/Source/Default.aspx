<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>
<%@ Import Namespace="COCASJOL.LOGIC.Security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colinas</title>    
    
    <script runat="server">
        protected void Button1_Click(object sender, DirectEventArgs e)
        {
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            if (usuarioLogic.Autenticar(this.txtUsername.Text, this.txtPassword.Text))
            {
                Window1.Close();
                Response.Redirect("Desktop.aspx");
            }
            else
                X.Msg.Alert("Inicio de Sesión", "El nombre de usuario o contraseña son incorrectos.").Show();
        }
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="150" 
            Icon="Lock" 
            Title="Login"
            Draggable="false"
            Width="350"
            Modal="true"
            Padding="5"
            Layout="Form">
            <Items>
                <ext:TextField 
                    ID="txtUsername" 
                    runat="server" 
                    FieldLabel="Usuario" 
                    AllowBlank="false"
                    BlankText="Se requiere su nombre de usuario."
                    />
                <ext:TextField 
                    ID="txtPassword" 
                    runat="server" 
                    InputType="Password" 
                    FieldLabel="Clave" 
                    AllowBlank="false" 
                    BlankText="Se requiere su clave."
                    />
            </Items>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Login" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="Button1_Click">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="1000" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>