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
        /*body
          {
              background-image: url('resources/images/login.jpg') !important;
              background-position: center;
              background-repeat: no-repeat;
          }*/
        
        html, body
        {
            background: #3d71b8 url('resources/images/login.jpg') no-repeat left top;
            font: normal 12px tahoma, arial, verdana, sans-serif;
            margin: 0;
            padding: 0;
            border: 0 none;
            overflow: hidden;
            height: 100%;
        }
        
        /*#bg
        {
            position: fixed;
            width: 200%;
            height: 200%;
        }
        #bg img
        {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            margin: auto;
            min-width: 50%;
            min-height: 50%;
        }*/
        
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
                txtPassword.setValue(faultylabs.MD5(txtPassword.getValue()));
                Ext.net.DirectMethods.Button1_Click({ eventMask: { showMask: true, minDelay: 1000 } });
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
            catch (Exception ex)
            {
                //log
                throw;
            }
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
        <div id="bg">
        <%--<img src="resources/images/login.jpg" alt="" />--%>
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
            Cls="loginEl">
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
