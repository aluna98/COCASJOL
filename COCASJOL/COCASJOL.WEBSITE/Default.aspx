<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<%@ Import Namespace="COCASJOL.LOGIC.Seguridad" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon"/>
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>

    <title>Colinas Login</title>

    <style type="text/css">         
        html, body
        {
            background: Black url('resources/images/fondo.jpg') no-repeat center;
            margin: 0;
            padding: 0;
            border: 0 none;
            overflow: hidden;
            height: 100%;
        }
        
        .bg_logo
        {
            background: url(resources/images/logo_transparente.gif) no-repeat center;
            progid:DXImageTransform.Microsoft.AlphaImageLoader(src='resources/images/logo_transparente.gif', sizingMethod='center');
        }
        
        .loginEl
        {
            position: relative !important;
        }
    </style>
    <script type="text/javascript" src="resources/js/md5.js"></script>
    <script type="text/javascript">
        var validate = function () {
            if (!Ext.getCmp('txtUsername').validate() || !Ext.getCmp('txtPassword').validate()) {
                Ext.Msg.alert('Login', 'El nombre de usuario y contraseña son necesarios.');
                return;
            } else {
                var encryptedpass = md5(txtPassword.getValue());
                txtPassword.setValue(encryptedpass);
                Ext.net.DirectMethods.Button1_Click({ eventMask: { showMask: true, minDelay: 1000} });
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string loggedUser = Session["username"] as string;

                if (!string.IsNullOrEmpty(loggedUser))
                {
                    Window1.Close();
                    Response.Redirect("~/Source/Desktop.aspx");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    
        [DirectMethod(RethrowException=true)]
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
                    this.txtUsername.Clear();
                    this.txtPassword.Clear();
                    X.Msg.Alert("Inicio de Sesión", "El nombre de usuario o contraseña son incorrectos.", "#{txtUsername}.focus();").Show();
                }
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg_logo">
        <ext:ResourceManager ID="ResourceManager1" runat="server" DisableViewState="true">
            <Listeners>
                <DocumentReady Handler="alinearLogin();" />
            </Listeners>
        </ext:ResourceManager>
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
            Layout="FitLayout"
            Cls="loginEl"
            MonitorResize="true">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="Form Panel" Header="false" Frame="false" ButtonAlign="Right" Border="false" MonitorValid="true">
                    <Items>
                        <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout"
                            Border="false">
                            <Items>
                                <ext:TextField ID="txtUsername" runat="server" FieldLabel="Usuario" AllowBlank="false" BlankText="Se requiere su nombre de usuario." EnableKeyEvents="true" LabelAlign="Right" AnchorHorizontal="90%" MsgTarget="Side" AutoFocus="true" AutoFocusDelay="1000">
                                    <Listeners>                                        
                                        <KeyUp Handler="KeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:TextField ID="txtPassword" runat="server" InputType="Password" FieldLabel="Clave" AllowBlank="false" BlankText="Se requiere su clave." EnableKeyEvents="true" LabelAlign="Right" AnchorHorizontal="90%" MsgTarget="Side">
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
        </div>
    </form>
</body>
</html>
