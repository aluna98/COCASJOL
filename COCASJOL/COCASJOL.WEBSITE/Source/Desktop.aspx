<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="COCASJOL.WEBSITE.Desktop" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colinas</title>    
    
    <style type="text/css">        
        .start-button {
            background-image: url(../Images/vista_start_button.gif) !important;
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
            openWindows: new Array(),

            popWindow: function(window) {
                var idx = -1;
                for (var i = 0; i < this.openWindows.length; i++) {
                    if (this.openWindows[i].title == window.title)
                    {
                        idx = i;
                        break;
                    }
                }

                if (idx != -1)
                    this.openWindows = this.openWindows.slice(idx, idx);

                return true;
            },

            createDynamicWindow: function (app,ico, title, url) {
                for (var i = 0; i < this.openWindows.length; i++) {
                    if (this.openWindows[i].title == title) {
                        return;
                    }
                }

                var desk = app.getDesktop();

                var w = desk.createWindow({
                    title: title,
                    width: 1000,
                    height: 600,
                    maximizable: true,
                    minimizable: true,
                    closeAction: 'close',
                    listeners: {
                        beforeClose: function () { DesktopX.popWindow(this); }
                    },
                    autoLoad: {
                        url: url,
                        mode: "iframe",
                        showMask: true
                    }
                });

                w.center();
                w.show();

                this.openWindows.push(w);
            }
        };


        var ShorcutClickHandler = function (app, id) {
            var d = app.getDesktop(); 
            if(id == 'scTile') {
                d.tile();
            } else if (id == 'scCascade') {
                d.cascade();
            } else if (id == 'scUsuarios') {
                DesktopX.createDynamicWindow(app, 'group', 'Usuarios', 'Seguridad/Usuario.aspx');
            } else if (id == 'scRoles') {
                DesktopX.createDynamicWindow(app, 'cog', 'Roles', 'Seguridad/Rol.aspx');
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>

        <ext:Desktop 
            ID="MyDesktop" 
            runat="server" 
            BackgroundColor="Black" 
            ShortcutTextColor="White" >
            <Listeners>
                <ShortcutClick Handler="ShorcutClickHandler(#{MyDesktop}, id);" />
            </Listeners>

            <StartButton Text="Start" IconCls="start-button" />
            <Modules>                
                <ext:DesktopModule ModuleID="UsuariosModule">
                    <Launcher ID="UsuariosLauncher" runat="server" Text="Usuarios" Icon="Group" >
                        <Listeners>
                            <Click Handler="DesktopX.createDynamicWindow(#{MyDesktop}, 'group', 'Usuarios', 'Seguridad/Usuario.aspx');" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="RolesModule">
                    <Launcher ID="RolesLauncher" runat="server" Text="Roles" Icon="Cog" >
                        <Listeners>
                            <Click Handler="DesktopX.createDynamicWindow(#{MyDesktop}, 'cog', 'Roles', 'Seguridad/Rol.aspx');" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
            </Modules>  
            
            <Shortcuts>
                <ext:DesktopShortcut ShortcutID="scUsuarios" Text="Usuarios" IconCls="shortcut-icon icon-usuarios" />
                <ext:DesktopShortcut ShortcutID="scRoles" Text="Roles" IconCls="shortcut-icon icon-roles" />
                <ext:DesktopShortcut ShortcutID="scTile" Text="Tile windows" IconCls="shortcut-icon icon-window48" X="{DX}-90" Y="{DY}-90" />
                <ext:DesktopShortcut ShortcutID="scCascade" Text="Cascade windows" IconCls="shortcut-icon icon-window48" X="{DX}-90" Y="{DY}-170" />
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
                                <EventMask ShowMask="true" Msg="Good Bye..." MinDelay="1000" />
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
                                    <ext:MenuItem Text="Usuarios" Icon="Group">
                                        <Listeners>
                                            <Click Handler="DesktopX.createDynamicWindow(#{MyDesktop}, 'group', 'Usuarios', 'Seguridad/Usuario.aspx');" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="Roles" Icon="Cog">
                                        <Listeners>
                                            <Click Handler="DesktopX.createDynamicWindow(#{MyDesktop}, 'cog', 'Roles', 'Seguridad/Rol.aspx');" />
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
    </div>
    </form>
</body>
</html>
