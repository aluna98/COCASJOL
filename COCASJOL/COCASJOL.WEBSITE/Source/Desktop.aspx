<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="COCASJOL.WEBSITE.Desktop" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register src="~/Source/Seguridad/CambiarClave.ascx" tagname="EditarClave" tagprefix="cclave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colinas</title>    
    
    <style type="text/css">        
        .start-button {
            background-image: url(../Images/cocasjol_start_button.gif) !important;
        }
        
        .shortcut-icon {
            width: 48px;
            height: 48px;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../Images/window.png", sizingMethod="scale");
        }
        
        .icon-grid48 {
            background-image: url(../Images/grid48x48.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../Images/grid48x48.png", sizingMethod="scale");
        }
        
        .icon-usuarios {
            background-image: url(../Images/user.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../Images/user.png", sizingMethod="scale");
        }
        
        .icon-socios
        {
            background-image: url(../Images/group.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../Images/group.png", sizingMethod="scale");
        }
        
        .icon-roles {
            background-image: url(../Images/gear_in.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../Images/gear_in.png", sizingMethod="scale");
        }
        
        .icon-window48 {
            background-image: url(../Images/window48x48.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../Images/window48x48.png", sizingMethod="scale");
        }
    </style>
    
    <script type="text/javascript">
        var DesktopX = {
            createDynamicWindow: function (app, ico, title, url) {
                var desk = app.getDesktop();

                var w = desk.getWindow(title + '-win');

                if (!w) {
                    w = desk.createWindow({
                        id: title + '-win',
                        iconCls: 'icon-' + ico,
                        title: title,
                        width: 1000,
                        height: 600,
                        maximizable: true,
                        minimizable: true,
                        closeAction: 'close',
                        shim: false,
                        animCollapse: false,
                        constrainHeader: true,
                        layout: 'fit',
                        autoLoad: {
                            url: url,
                            mode: "iframe",
                            showMask: true
                        }
                    });
                    w.center();
                }
                w.show();
            }
        };

        var WindowX = {
            usuarios: function (app) {
                DesktopX.createDynamicWindow(app, 'user', 'Usuarios', 'Seguridad/Usuario.aspx');
            },

            roles: function (app) {
                DesktopX.createDynamicWindow(app, 'cog', 'Roles', 'Seguridad/Rol.aspx');
            },

            socios: function(app) {
                DesktopX.createDynamicWindow(app, 'group', 'Socios', 'Socios/Socios.aspx');
            },

            settings: function() {
                SettingsWin.show();
            }
        };

        var ShorcutClickHandler = function (app, id) {
            var d = app.getDesktop();

            if (id == 'scTile') {
                d.tile();
            } else if (id == 'scCascade') {
                d.cascade();
            } else if (id == 'scUsuarios') {
                WindowX.usuarios(app);
            } else if (id == 'scRoles') {
                WindowX.roles(app);
            } else if (id == 'scSocios') {
                WindowX.socios(app);
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>

        <ext:Menu runat="server" ID="cmenu">
            <Items>
            <ext:MenuItem Text="Settings" Icon="Wrench">
                <Listeners>
                    <Click Handler="WindowX.settings();" />
                </Listeners>
            </ext:MenuItem>
            </Items>
        </ext:Menu>

        <ext:Desktop 
            ID="MyDesktop" 
            runat="server" 
            BackgroundColor="Black" 
            ShortcutTextColor="White" >
            <Listeners>
                <ShortcutClick Handler="ShorcutClickHandler(#{MyDesktop}, id);" />
                <Ready Handler="Ext.get('x-desktop').on('contextmenu', function(e){e.stopEvent();e.preventDefault();cmenu.showAt(e.getPoint());});" />
            </Listeners>
            <StartButton Text="Inicio" IconCls="start-button" />

            <Modules>
                <ext:DesktopModule ModuleID="SettingsModule" WindowID="SettingsWin" >
                    <Launcher ID="SettingsLauncher" runat="server" Text="Configuración" Icon="Wrench"></Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="UsuariosModule">
                    <Launcher ID="Launcher1" runat="server" Text="Usuarios" Icon="User" >
                        <Listeners>
                            <Click Handler="WindowX.usuarios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="UsuariosModule">
                    <Launcher ID="UsuariosLauncher" runat="server" Text="Usuarios" Icon="User" >
                        <Listeners>
                            <Click Handler="WindowX.usuarios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="RolesModule">
                    <Launcher ID="RolesLauncher" runat="server" Text="Roles" Icon="Cog" >
                        <Listeners>
                            <Click Handler="WindowX.roles(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="SociosModule">
                    <Launcher ID="SociosLauncher" runat="server" Text="Socios" Icon="Group" >
                        <Listeners>
                            <Click Handler="WindowX.socios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
            </Modules>  
            
            <Shortcuts>
                <ext:DesktopShortcut ShortcutID="scUsuarios" Text="Usuarios" IconCls="shortcut-icon icon-usuarios" />
                <ext:DesktopShortcut ShortcutID="scRoles" Text="Roles" IconCls="shortcut-icon icon-roles" />
                <ext:DesktopShortcut ShortcutID="scTile" Text="Tile windows" IconCls="shortcut-icon icon-window48" X="{DX}-90" Y="{DY}-90" />
                <ext:DesktopShortcut ShortcutID="scCascade" Text="Cascade windows" IconCls="shortcut-icon icon-window48" X="{DX}-90" Y="{DY}-170" />
                <ext:DesktopShortcut ShortcutID="scSocios" Text="Socios" IconCls="shortcut-icon icon-socios" />
            </Shortcuts>

            <StartMenu Height="400" Width="300" ToolsWidth="127" Title="Start Menu">
                <ToolItems>
                    <ext:MenuItem Text="Settings" Icon="Wrench">
                        <Listeners>
                            <Click Handler="Ext.Msg.alert('Message', 'Settings Clicked');" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem Text="Logout" Icon="Disconnect">
                        <DirectEvents>
                            <Click OnEvent="Logout_Click">
                                <EventMask ShowMask="true" Msg="Adios..." MinDelay="1000" />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                    <%--<ext:MenuSeparator />--%>
                </ToolItems>                
                <Items>
                    <ext:MenuItem ID="MenuItem4" runat="server" Text="All" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="Menu2" runat="server">
                                <Items>
                                    <ext:MenuItem Text="Usuarios" Icon="User">
                                        <Listeners>
                                            <Click Handler="WindowX.usuarios(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="Roles" Icon="Cog">
                                        <Listeners>
                                            <Click Handler="WindowX.roles(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                </Items>
            </StartMenu>
        </ext:Desktop>

        <ext:DesktopWindow
            ID="SettingsWin"
            runat="server"
            Title="Configuración"
            Width="300"
            Maximizable="false"
            BodyBorder="false"
            InitCenter="false"
            Icon="Wrench"
            AutoHeight="true"
            Shadow="None"
            Resizable="false">
        </ext:DesktopWindow>
    </div>
    </form>
</body>
</html>
