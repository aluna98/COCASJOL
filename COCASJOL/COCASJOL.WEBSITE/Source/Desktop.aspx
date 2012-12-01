<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="COCASJOL.WEBSITE.Desktop" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Seguridad/UsuarioActual.ascx" TagName="UsuarioActual" TagPrefix="usera" %>
<%@ Register Src="~/Source/Seguridad/CambiarClave.ascx" TagName="CambiarClave" TagPrefix="cclave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colinas</title>    
    <link rel="stylesheet" type="text/css" href="../resources/css/desktop.css" />
    <%--<style type="text/css">
        .start-button
        {
            background-image: url(../resources/images/cocasjol_start_button.gif) !important;
        }
        
        .shortcut-icon
        {
            width: 48px;
            height: 48px;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/window.png", sizingMethod="scale");
        }
        
        .icon-grid48
        {
            background-image: url(../Images/grid48x48.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/grid48x48.png", sizingMethod="scale");
        }
        
        .icon-usuarios
        {
            background-image: url(../Images/user.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/user.png", sizingMethod="scale");
        }
        
        .icon-roles
        {
            background-image: url(../Images/gear_in.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/gear_in.png", sizingMethod="scale");
        }
        
        .icon-socios
        {
            background-image: url(../Images/group.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/group.png", sizingMethod="scale");
        }
        
        .icon-tiposDeProducto
        {
            background-image: url(../Images/basket_put.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/basket_put.png", sizingMethod="scale");
        }
        
        .icon-productos
        {
            background-image: url(../Images/basket.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/basket.png", sizingMethod="scale");
        }
        
        .icon-window48
        {
            background-image: url(../Images/window48x48.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="../resources/images/window48x48.png", sizingMethod="scale");
        }
        
        #poweredby
        {
            position: absolute;
            bottom: 40px;
            right: 20px;
            z-index: 15000; /* IE 5-7 */
            filter: alpha(opacity=70); /* Netscape */
            -moz-opacity: 0.7; /* Safari 1.x */
            -khtml-opacity: 0.7; /* Good browsers */
            opacity: 0.7;
        }
        #poweredby div
        {
            position: relative;
            width: 104px;
            height: 50px;
            background-image: url(../resources/images/dev-by-unitec.png);
            background-repeat: no-repeat;
        }
        /* The simple background image PNG does not work in IE6-8, but does in IE9 */
        .x-ie6 #poweredby div, .x-ie7 #poweredby div, .x-ie8 #poweredby div
        {
            background-image: none;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='../resources/images/dev-by-unitec.png', sizingMethod='scale');
        }
    </style>--%>
    <script type="text/javascript" src="../resources/js/md5.js"></script>
    <script type="text/javascript">
        var DesktopX = {
            createDynamicWindow: function (app, ico, title, url, width, height) {
                width = width == null ? 640 : width;
                height = height == null ? 480 : height;

                var desk = app.getDesktop();

                var w = desk.getWindow(title + '-win');

                if (!w) {
                    w = desk.createWindow({
                        id: title + '-win',
                        iconCls: 'icon-' + ico,
                        title: title,
                        width: width,
                        height: height,
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

            socios: function (app) {
                DesktopX.createDynamicWindow(app, 'group', 'Socios', 'Socios/Socios.aspx', 800, 600);
            },

            tiposDeProductos: function (app) {
                DesktopX.createDynamicWindow(app, 'basketput', 'Tipos de producto', 'Productos/TiposDeProductos.aspx');
            },

            productos: function (app) {
                DesktopX.createDynamicWindow(app, 'basket', 'Productos', 'Productos/Productos.aspx');
            },

            settings: function () {
                SettingsWin.show();
            },

            about: function () {
                AboutWin.show();
            }
        };

        var ShorcutClickHandler = function (app, id) {
            var d = app.getDesktop();

            if (id == 'scUsuarios') {
                WindowX.usuarios(app);
            } else if (id == 'scRoles') {
                WindowX.roles(app);
            } else if (id == 'scSocios') {
                WindowX.socios(app);
            } else if (id == 'scTiposDeProductos') {
                WindowX.tiposDeProductos(app);
            } else if (id == 'scProductos') {
                WindowX.productos(app);
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
            <ext:MenuItem Text="Configuración" Icon="Wrench">
                <Listeners>
                    <Click Handler="WindowX.settings();" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator />
            <ext:MenuItem Text="Ventanas en Mosaico" Icon="ApplicationTileVertical">
                <Listeners>
                    <Click Handler="#{MyDesktop}.getDesktop().tile();" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem Text="Ventanas en Cascada" Icon="ApplicationCascade">
                <Listeners>
                    <Click Handler="#{MyDesktop}.getDesktop().cascade();" />
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
                <%--Seguridad--%>
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
                <%--Seguridad--%>

                <%--Socios--%>
                <ext:DesktopModule ModuleID="SociosModule">
                    <Launcher ID="SociosLauncher" runat="server" Text="Socios" Icon="Group" >
                        <Listeners>
                            <Click Handler="WindowX.socios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Socios--%>

                <%--Productos--%>
                <ext:DesktopModule ModuleID="TiposDeProductoModule">
                    <Launcher ID="TiposDeProductoLauncher" runat="server" Text="Tipos de Producto" Icon="BasketPut" >
                        <Listeners>
                            <Click Handler="WindowX.tiposDeProductos(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="ProductosModule">
                    <Launcher ID="ProductosLauncher" runat="server" Text="Productos" Icon="Basket" >
                        <Listeners>
                            <Click Handler="WindowX.productos(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Productos--%>


                <%--Configuracion--%>
                <ext:DesktopModule ModuleID="SettingsModule" WindowID="SettingsWin" >
                    <Launcher ID="SettingsLauncher" runat="server" Text="Configuración" Icon="Wrench" />
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="UsuarioActualModule" WindowID="UsuarioActualWin" >
                    <Launcher ID="UsuarioActualLauncher" runat="server" Text="Editar Usuario" Icon="UserEdit" />
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="CambiarClaveModule" WindowID="CambiarClaveWin" >
                    <Launcher ID="CambiarClaveLauncher" runat="server" Text="Cambiar Clave" Icon="Key" />
                </ext:DesktopModule>
                <%--Configuracion--%>
            </Modules>  
            
            <Shortcuts>
                <ext:DesktopShortcut ShortcutID="scUsuarios" Text="Usuarios" IconCls="shortcut-icon icon-usuarios" />
                <ext:DesktopShortcut ShortcutID="scRoles" Text="Roles" IconCls="shortcut-icon icon-roles" />

                <ext:DesktopShortcut ShortcutID="scSocios" Text="Socios" IconCls="shortcut-icon icon-socios" />

                <ext:DesktopShortcut ShortcutID="scTiposDeProductos" Text="Tipos de Productos" IconCls="shortcut-icon icon-tiposDeProducto" />
                <ext:DesktopShortcut ShortcutID="scProductos" Text="Productos" IconCls="shortcut-icon icon-productos" />

                <%--<ext:DesktopShortcut ShortcutID="scTile" Text="Tile windows" IconCls="shortcut-icon icon-window48" X="{DX}-90" Y="{DY}-90" />
                <ext:DesktopShortcut ShortcutID="scCascade" Text="Cascade windows" IconCls="shortcut-icon icon-window48" X="{DX}-90" Y="{DY}-170" />--%>
            </Shortcuts>

            <StartMenu Height="400" Width="300" ToolsWidth="127" Title="Start Menu">
                <ToolItems>
                    <ext:MenuItem Text="Configuración" Icon="Wrench">
                        <Listeners>
                            <Click Handler="WindowX.settings();" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem Text="Salir" Icon="Disconnect">
                        <DirectEvents>
                            <Click OnEvent="Logout_Click">
                                <EventMask ShowMask="true" Msg="Adios..." MinDelay="1000" />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                    <ext:MenuItem Text="About" Icon="Information">
                        <Listeners>
                            <Click Handler="WindowX.about();" />
                        </Listeners>
                    </ext:MenuItem>
                </ToolItems>                
                <Items>
                    <ext:MenuItem ID="SecurityMenu" runat="server" Text="Seguridad" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="UsuariosMenuItem" Text="Usuarios" Icon="User">
                                        <Listeners>
                                            <Click Handler="WindowX.usuarios(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="RolesMenuItem" Text="Roles" Icon="Cog">
                                        <Listeners>
                                            <Click Handler="WindowX.roles(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="AsociatesMenu" runat="server" Text="Socios" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat ="server">
                                <Items>
                                    <ext:MenuItem ID="SociosMenuItem" Text="Socios" Icon="Group" >
                                        <Listeners>
                                            <click Handler="WindowX.socios(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="ProductsMenu" runat="server" Text="Productos" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat ="server">
                                <Items>
                                    <ext:MenuItem ID="ProductosMenuItem" Text="Productos" Icon="Basket" >
                                        <Listeners>
                                            <click Handler="WindowX.productos(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="TiposDeProductosMenuItem" Text="Tipos de Productos" Icon="BasketPut" >
                                        <Listeners>
                                            <click Handler="WindowX.tiposDeProductos(#{MyDesktop});" />
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
            Resizable="false"
            Shadow="None"
            Layout="AccordionLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" AutoHeight="true" Title="Editar Usuario" Layout="FitLayout">
                    <Items>
                        <ext:Portal ID="portal1" runat="server" AutoHeight="true" Header="false" Layout="Column">
                            <Items>
                                <ext:PortalColumn ID="portalcolumn" runat="server" StyleSpec="padding:10px 10px 10px 10px"
                                    ColumnWidth="1" Layout="Anchor" Height="150">
                                    <Items>
                                        <ext:Portlet runat="server" Title="Editar Información" Selectable="true" ID="UserInfoPortlet" Draggable="false" Collapsible="false" Icon="UserEdit">
                                            <Items>
                                                <ext:TableLayout runat="server" >
                                                    <Cells>
                                                        <ext:Cell>
                                                            <ext:ImageButton ID="UsuarioActulBtn" runat="server" Height="32" Width="32"
                                                                ImageUrl="../resources/images/user_edit.png">
                                                                <Listeners>
                                                                    <Click Handler="#{UsuarioActualWin}.show();" />
                                                                </Listeners>
                                                            </ext:ImageButton>
                                                        </ext:Cell>
                                                        <ext:Cell>
                                                            <ext:Label ID="Label11" runat="server" Text="Permite Cambiar la información del usuario actual."></ext:Label>
                                                        </ext:Cell>
                                                    </Cells>
                                                </ext:TableLayout>
                                            </Items>
                                        </ext:Portlet>
                                        <ext:Portlet runat="server" Title="Cambiar Contraseña" Selectable="true" ID="PasswordPortlet" Draggable="false" Collapsible="false" Icon="Key">
                                            <Items>
                                                <ext:TableLayout runat="server" ID="TableLayout1">
                                                    <Cells>
                                                        <ext:Cell>
                                                            <ext:ImageButton ID="CambiarClaveBtn" runat="server" Height="32" Width="32"
                                                                ImageUrl="../resources/images/key.png">
                                                                <Listeners>
                                                                    <Click Handler="#{CambiarClaveWin}.show();" />
                                                                </Listeners>
                                                            </ext:ImageButton>
                                                        </ext:Cell>
                                                        <ext:Cell>
                                                            <ext:Label ID="Label12" runat="server" Text="Permite Cambiar la contraseña del usuario actual.">
                                                            </ext:Label>
                                                        </ext:Cell>
                                                    </Cells>
                                                </ext:TableLayout>
                                            </Items>
                                        </ext:Portlet>
                                    </Items>
                                </ext:PortalColumn>
                            </Items>
                        </ext:Portal>
                    </Items>
                </ext:Panel>
                <%--<ext:Panel ID="Panel2" runat="server" Height="200" Title="Fondo de Escritorio" Layout="FitLayout">
                </ext:Panel>--%>
            </Items>
        </ext:DesktopWindow>

        <ext:Window
            ID="AboutWin"
            runat="server"
            Title="About"
            Width="300"
            Maximizable="false"
            CloseAction="Hide"
            InitCenter="true"
            Icon="Information"
            Height="200"
            Resizable="false"
            Hidden="true">
            <Content>
                Proyecto de Graduación para Ingeniería en Sistemas Computacionales.
            </Content>
        </ext:Window>

        <usera:UsuarioActual runat="server" ID="UsuarioActualCtl" />
        <cclave:CambiarClave runat="server" ID="CambiarClaveCtl" />
    </div>
    </form>
    <a href="http://www.unitec.edu" target="_blank" alt="Powered by Ext .Net"id="poweredby"><div></div></a>
</body>
</html>
