<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<%@ Import Namespace="COCASJOL.LOGIC.Seguridad" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colinas Login</title>
    <style type="text/css">
          body
          {
              background-image: url('Images/login.jpg') !important;
              background-position: center;
              background-repeat: no-repeat;
          }
          
          .loginEl {
            position: relative !important;
          }
    </style>
    <script type="text/javascript" src="Scripts/md5.js"></script>
    <script type="text/javascript">
        var validate = function () {
            if (!Ext.getCmp('txtUsername').validate() || !Ext.getCmp('txtPassword').validate()) {
                Ext.Msg.alert('Login', 'El nombre de usuario y contraseña son necesarios.');
                return;
            } else {
                txtPassword.setValue(faultylabs.MD5(txtPassword.getValue()));
                Ext.net.DirectMethods.Button1_Click({ eventMask: { showMask: true } });
            }
        };

        var KeyUpEvent = function (sender, e) {
            if (e.getKey() == 13) {
                validate();
            }
        };

        var alinearLogin = function () {
            Window1.getEl().alignTo(Ext.getBody(), 'bl', [50, -200], false);
        };
    </script>

    <script runat="server">
        [DirectMethod]
        public void Button1_Click()
        {
            UsuarioLogic usuarioLogic = new UsuarioLogic();
            if (usuarioLogic.Autenticar(this.txtUsername.Text, this.txtPassword.Text))
            {
                Session["username"] = this.txtUsername.Text;

                Window1.Close();
                Response.Redirect("~/Source/Desktop.aspx");
            }
            else
                X.Msg.Alert("Inicio de Sesión", "El nombre de usuario o contraseña son incorrectos.").Show();
        }
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server">
            <Listeners>
                <DocumentReady Handler="alinearLogin();" />
            </Listeners>
        </ext:ResourceManager>
        
        <ext:Window ID="Window1" runat="server" Closable="false" Resizable="false" Height="150"
            Icon="Lock" Title="Login" Draggable="false" Width="350" Layout="FitLayout"
            Cls="loginEl">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="Form Panel" Header="false" Frame="false" ButtonAlign="Right" Border="false">
                    <Items>
                        <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout"
                            Border="false">
                            <Items>
                                <ext:TextField ID="txtUsername" runat="server" FieldLabel="Usuario" AllowBlank="false"
                                    BlankText="Se requiere su nombre de usuario." EnableKeyEvents="true" LabelAlign="Right" AnchorHorizontal="90%">
                                    <Listeners>
                                        <KeyUp Handler="KeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:TextField ID="txtPassword" runat="server" InputType="Password" FieldLabel="Clave"
                                    AllowBlank="false" BlankText="Se requiere su clave." EnableKeyEvents="true" LabelAlign="Right" AnchorHorizontal="90%">
                                    <Listeners>
                                        <KeyUp Handler="KeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:TextField>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Login" Icon="Accept">
                    <Listeners>
                        <Click Handler="validate();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>
