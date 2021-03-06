﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="COCASJOL.WEBSITE.Desktop" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Seguridad/UsuarioActual.ascx" TagName="UsuarioActual" TagPrefix="usera" %>
<%@ Register Src="~/Source/Seguridad/CambiarClave.ascx" TagName="CambiarClave" TagPrefix="cclave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COCASJOL</title>
    <link rel="shortcut icon" href="../favicon.ico" type="image/x-icon"/>
    <link rel="icon" href="../favicon.ico" type="image/x-icon"/>

    <link rel="Stylesheet" type="text/css" href="../resources/css/Desktop.css" />
    <link rel="Stylesheet" type="text/css" href="../resources/css/DesktopShortcuts.css" />
    <script type="text/javascript" src="../resources/js/md5.js" ></script>
    <script type="text/javascript" src="../resources/js/desktop.js" ></script>
</head>
<body>
    <script type="text/javascript">
        function CheckNotifications() {
            Ext.net.DirectMethods.CheckForNotifications(
            {
                failure: function () {
                    window.clearInterval(DesktopintervalVariable);
                    Ext.Msg.alert('Notificaciones', 'Las notificaciones han sido desactivadas debido a un error. Cuando la pagina se recargue volveran a estar disponibles. Para Refrescar la página presione la tecla F5.');
                }
            });
        }

        var DesktopintervalVariable = setInterval(CheckNotifications, 10000);
    </script>
    <form id="form1" runat="server">
    <div class="desktop_body">
        <ext:ResourceManager ID="ResourceManager1" runat="server" DisableViewState="true">
            <Listeners>
                <DocumentReady Handler="DesktopX.alignPanels(); DesktopX.initialCheckForNotification();" />
                <WindowResize Handler=" DesktopX.alignPanels();" />
            </Listeners>
        </ext:ResourceManager>

        <%--Context Menu--%>

        <ext:Menu runat="server" ID="cmenu">
            <Items>
            <ext:MenuItem Text="Opciones" Icon="Wrench">
                <Listeners>
                    <Click Handler="WindowX.settings();" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator />
            <ext:MenuItem Text="Ventanas en Cascada" Icon="ApplicationCascade">
                <Listeners>
                    <Click Handler="DesktopX.cascadeWindows(#{MyDesktop});" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem Text="Ventanas en Grupo Horizontal" Icon="ApplicationTileHorizontal">
                <Listeners>
                    <Click Handler="DesktopX.tileFitWindows(#{MyDesktop}, true);" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem Text="Ventanas en Grupo Vertical" Icon="ApplicationTileVertical">
                <Listeners>
                    <Click Handler="DesktopX.tileFitWindows(#{MyDesktop}, false);" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem Text="Ventanas en Mosaico" Icon="ApplicationTileVertical">
                <Listeners>
                    <Click Handler="DesktopX.tileWindows(#{MyDesktop});" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem Text="Ventanas en Tablero" Icon="ApplicationViewTile">
                <Listeners>
                    <Click Handler="DesktopX.checkerboardWindows(#{MyDesktop});" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator />
            <ext:MenuItem Text="Cerrar Ventanas" Icon="ApplicationDelete">
                <Listeners>
                    <Click Handler="DesktopX.closeAllWindows(#{MyDesktop});" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator />
            <ext:MenuItem Text="Restaurar Ventanas" Icon="ApplicationGet">
                <Listeners>
                    <Click Handler="DesktopX.showAllWindows(#{MyDesktop});" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem Text="Minimizar Ventanas" Icon="ApplicationPut">
                <Listeners>
                    <Click Handler="DesktopX.minimizeAllWindows(#{MyDesktop});" />
                </Listeners>
            </ext:MenuItem>
            </Items>
        </ext:Menu>
        
        <%--Context Menu--%>

        <%--Desktop--%>

        <ext:Checkbox runat="server" ID="maximizarVentanasHdn" Hidden="true"></ext:Checkbox>

        <ext:Desktop
            ID="MyDesktop" 
            runat="server" 
            BackgroundColor="White" 
            ShortcutTextColor="Black" >
            <Listeners>
                <ShortcutClick Handler="ShorcutClickHandler(#{MyDesktop}, id);" />
                <Ready Handler="Ext.get('x-desktop').on('contextmenu', function(e){e.stopEvent();e.preventDefault();cmenu.showAt(e.getPoint());});
                                Ext.get('ux-taskbar').on('contextmenu', function(e){e.stopEvent();e.preventDefault();cmenu.showAt(e.getPoint());});" />
            </Listeners>
            <Content>
                <ext:Panel
                    ID="pnlSample"
                    runat="server"
                    Title="Consolidado de Inventario de Café"
                    Cls="desktopEl"
                    AutoHeight="true"
                    Width="450"
                    Padding="5"
                    Collapsible="true" Layout="FormLayout">
                    <Items>
                        <ext:FieldSet ID="FieldSet1" runat="server" Header="false" Layout="ColumnLayout" >
                            <LayoutConfig>
                                <ext:ColumnLayoutConfig FitHeight="false" />
                            </LayoutConfig>
                            <Items>
                                <ext:Panel ID="Panel5" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" Padding="1">
                                    <Items>
                                        <ext:DateField runat="server" ID="ConsolidadoFechaInicialTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Desde" AllowBlank="true" MsgTarget="Side" ReadOnly="true" ></ext:DateField>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel6" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" Padding="1">
                                    <Items>
                                        <ext:DateField runat="server" ID="ConsolidadoFechaFinalTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Hasta" AllowBlank="true" MsgTarget="Side" ReadOnly="true" ></ext:DateField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:FieldSet>
                        <ext:FieldSet ID="FieldSet2" runat="server" Title="Ingresos de Café de Socios a la Fecha" Padding="5" Layout="AnchorLayout">
                            <Items>
                                <ext:TextField runat="server" ID="TotalIngresadoTxt" FieldLabel="Total Ingresado (QQ)" AnchorHorizontal="90%" ReadOnly="true" LabelWidth="150" ></ext:TextField>
                                <ext:TextField runat="server" ID="TotalAjustadoTxt" FieldLabel="Total Ajustado (QQ)" AnchorHorizontal="90%" ReadOnly="true" LabelWidth="150" ></ext:TextField>
                                <ext:TextField runat="server" ID="TotalCompradoTxt" FieldLabel="Total Comprado (QQ)" AnchorHorizontal="90%" ReadOnly="true" LabelWidth="150" ></ext:TextField>
                                <ext:TextField runat="server" ID="TotalDepositoTxt" FieldLabel="Total Deposito (QQ)" AnchorHorizontal="90%" ReadOnly="true" LabelWidth="150" ></ext:TextField>
                            </Items>
                        </ext:FieldSet>
                        <ext:FieldSet ID="FieldSet3" runat="server" Title="Ingresos de Café de Cooperativa a la Fecha" Padding="5" Layout="AnchorLayout">
                            <Items>
                                <ext:TextField runat="server" ID="TotalCoopCompradoTxt" FieldLabel="Total Comprado (QQ)" AnchorHorizontal="90%" ReadOnly="true" LabelWidth="150" ></ext:TextField>
                                <ext:TextField runat="server" ID="TotalCoopVendidoTxt" FieldLabel="Total Vendido (QQ)" AnchorHorizontal="90%" ReadOnly="true" LabelWidth="150" ></ext:TextField>
                                <ext:TextField runat="server" ID="TotalCoopDepositoTxt" FieldLabel="Total Deposito (QQ)" AnchorHorizontal="90%" ReadOnly="true" LabelWidth="150" ></ext:TextField>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>
            </Content>


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

                <%--Utiles--%>

                <ext:DesktopModule ModuleID="ConfiguracionDeSistemaModule">
                    <Launcher ID="ConfiguracionDeSistemaLauncher" runat="server" Text="Configuración de Sistema" Icon="ServerEdit" >
                        <Listeners>
                            <Click Handler="WindowX.configuracionDeSistema(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>

                <ext:DesktopModule ModuleID="PlantillasNotificacionesModule">
                    <Launcher ID="PlantillasNotificacionesLauncher" runat="server" Text="Plantillas de Notificaciones" IconCls="icon-plantillasNotificaciones16" >
                        <Listeners>
                            <Click Handler="WindowX.plantillasNotificaciones(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>

                <%--Utiles--%>

                <%--Socios--%>
                <ext:DesktopModule ModuleID="SociosModule">
                    <Launcher ID="SociosLauncher" runat="server" Text="Socios" Icon="Group" >
                        <Listeners>
                            <Click Handler="WindowX.socios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Socios--%>

                <%--Notas De Peso--%>
                <ext:DesktopModule ModuleID="EstadosNotasDePesoModule">
                    <Launcher ID="EstadosNotasDePesoLauncher" runat="server" Text="Estados de Notas De Peso" Icon="PageGo" >
                        <Listeners>
                            <Click Handler="WindowX.estadosNotasDePeso(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="NotasDePesoModule">
                    <Launcher ID="NotasDePesoLauncher" runat="server" Text="Notas de Peso" Icon="PageWhiteOffice" >
                        <Listeners>
                            <Click Handler="WindowX.notasDePeso(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Notas De Peso--%>

                <%--Inventario de Cafe--%>
                <ext:DesktopModule ModuleID="ClasificacionesDeCafeModule">
                    <Launcher ID="ClasificacionesDeCafeLauncher" runat="server" Text="Clasificaciones de Café" Icon="TableGo" >
                        <Listeners>
                            <Click Handler="WindowX.clasificacionesDeCafe(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="InventarioDeCafePorSocioModule">
                    <Launcher ID="InventarioDeCafePorSocioLauncher" runat="server" Text="Inventario de Café por Socio" Icon="Brick" >
                        <Listeners>
                            <Click Handler="WindowX.inventarioDeCafePorSocio(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="InventarioDeCafeModule">
                    <Launcher ID="InventarioDeCafeLauncher" runat="server" Text="Inventario de Café" Icon="Bricks" >
                        <Listeners>
                            <Click Handler="WindowX.inventarioDeCafe(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Inventario de Cafe--%>

                <%--Hojas De Liquidación--%>
                <ext:DesktopModule ModuleID="HojasDeLiquidacionModule">
                    <Launcher ID="HojasDeLiquidacionLauncher" runat="server" Text="Hojas de Liquidación" Icon="Script" >
                        <Listeners>
                            <Click Handler="WindowX.hojasDeLiquidacion(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Hojas De Liquidación--%>

                <%--Ajustes de Inventario de Café de Socios--%>
                <ext:DesktopModule ModuleID="AjustesDeInventarioDeCafeDeSociosModule">
                    <Launcher ID="AjustesDeInventarioDeCafeDeSociosLauncher" runat="server" Text="Ajustes de Inventario de Café de Socios" IconCls="icon-ajustesDeInventarioDeCafeDeSocios16" >
                        <Listeners>
                            <Click Handler="WindowX.ajustesDeInventarioDeCafeDeSocios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Ajustes de Inventario de Café de Socios--%>

                <%--Ventas de Inventario de Café--%>
                <ext:DesktopModule ModuleID="VentasDeInventarioDeCafeModule">
                    <Launcher ID="VentasDeInventarioDeCafeLauncher" runat="server" Text="Ventas de Inventario de Café" Icon="CartFull" >
                        <Listeners>
                            <Click Handler="WindowX.ventasDeInventarioDeCafe(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Ventas de Inventario de Café--%>

                <%--Aportaciones por Socio--%>
                <ext:DesktopModule ModuleID="AportacionesPorSocioModule">
                    <Launcher ID="AportacionesPorSocioLauncher" runat="server" Text="Aportaciones por Socio" IconCls="icon-aportacionesPorSocio16" >
                        <Listeners>
                            <Click Handler="WindowX.aportacionesPorSocio(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>

                <ext:DesktopModule ModuleID="RetiroDeAportacionesModule">
                    <Launcher ID="RetiroAportacionesLauncher" runat="server" Text="Retiro de Aportaciones" IconCls="icon-retiroAportaciones16" >
                        <Listeners>
                            <Click Handler="WindowX.retiroDeAportaciones(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Aportaciones por Socio--%>

                <%--Prestamos--%>
                <ext:DesktopModule ModuleID="PrestamosModule">
                    <Launcher ID="PrestamosLauncher" runat="server" Text="Tipos de Prestamo" IconCls="icon-prestamos16" >
                        <Listeners>
                            <Click Handler="WindowX.prestamos(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="SolicitudesDePrestamoModule">
                    <Launcher ID="SolicitudesDePrestamoLauncher" runat="server" Text="Solicitudes de Prestamo" Icon="PageWhiteText" >
                        <Listeners>
                            <Click Handler="WindowX.solicitudesDePrestamo(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="PrestamosAprobadosModule">
                    <Launcher ID="PrestamosAprobadosModule" runat="server" Text="Solicitudes de Prestamo Aprobado" IconCls="icon-prestamosAprobados16" >
                        <Listeners>
                            <Click Handler="WindowX.prestamosAprobados(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Prestamos--%>

                <%--Reportes--%>
                <ext:DesktopModule ModuleID="ReporteMovimientosInventarioDeCafeDeSociosModule">
                    <Launcher ID="ReporteMovimientosInventarioDeCafeDeSociosLauncher" runat="server" Text="Reporte de Movimientos de Inventario de Café de Socios" IconCls="icon-reporteMovimientosInventarioDeCafeDeSocios16" >
                        <Listeners>
                            <Click Handler="WindowX.reporteMovimientosInventarioDeCafeDeSocios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="ReporteMovimientosInventarioDeCafeDeCooperativaModule">
                    <Launcher ID="ReporteMovimientosInventarioDeCafeDeCooperativaLauncher" runat="server" Text="Reporte de Movimientos de Inventario de Café de Cooperativa" IconCls="icon-reporteMovimientosInventarioDeCafeDeCooperativa16" >
                        <Listeners>
                            <Click Handler="WindowX.reporteMovimientosInventarioDeCafeDeCooperativa(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="ReporteHojasDeLiquidacionModule">
                    <Launcher ID="ReporteHojasDeLiquidacionLauncher" runat="server" Text="Reporte de Hojas de Liquidación" IconCls="icon-reporteHojasDeLiquidacion16" >
                        <Listeners>
                            <Click Handler="WindowX.reporteHojasDeLiquidacion(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="ReporteDetalleNotasDePesoModule">
                    <Launcher ID="ReporteDetalleNotasDePesoLauncher" runat="server" Text="Reporte Detalle de Notas de Peso" IconCls="icon-reporteDetalleNotasDePeso16" >
                        <Listeners>
                            <Click Handler="WindowX.reporteDetalleDeNotasDePeso(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="ReporteDetalleAportacionesPorSocioModule">
                    <Launcher ID="ReporteDetalleAportacionesPorSocioLauncher" runat="server" Text="Reporte Detalle de Aportaciones por Socio" IconCls="icon-reporteDetalleAportacionesPorSocio16" >
                        <Listeners>
                            <Click Handler="WindowX.reporteDetalleDeAportacionesPorSocio(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="ReportePrestamosPorSociosModule">
                    <Launcher ID="ReportePrestamosPorSociosLauncher" runat="server" Text="Reporte de Prestamos por Socios" IconCls="icon-reportePrestamosPorSocios16" >
                        <Listeners>
                            <Click Handler="WindowX.reportePrestamosPorSocios(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Reportes--%>

                <%--Configuracion--%>
                <ext:DesktopModule ModuleID="VariablesDeEntornoModule">
                    <Launcher ID="VariablesDeEntornoLauncher" runat="server" Text="Variables de Entorno" Icon="Database" >
                        <Listeners>
                            <Click Handler="WindowX.variablesDeEntorno(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="SettingsModule" WindowID="SettingsWin" >
                    <Launcher ID="SettingsLauncher" runat="server" Text="Opeciones" Icon="Wrench" />
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="NotificacioModule" WindowID="NotificationsWin" >
                    <Launcher ID="NotificacioLauncher" runat="server" Text="Notificaciones" Icon="Mail" />
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="UsuarioActualModule" WindowID="UsuarioActualWin" >
                    <Launcher ID="UsuarioActualLauncher" runat="server" Text="Editar Usuario" Icon="UserEdit" />
                </ext:DesktopModule>
                <ext:DesktopModule ModuleID="CambiarClaveModule" WindowID="CambiarClaveWin" >
                    <Launcher ID="CambiarClaveLauncher" runat="server" Text="Cambiar Clave" Icon="Key" />
                </ext:DesktopModule>
                <%--Configuracion--%>

                <%--Soporte--%>
                <ext:DesktopModule ModuleID="ApplicationLogModule">
                    <Launcher ID="ApplicationLogLauncher" runat="server" Text="Bitácora de Aplicación" Icon="Clipboard" >
                        <Listeners>
                            <Click Handler="WindowX.variablesDeEntorno(#{MyDesktop});" />
                        </Listeners>
                    </Launcher>
                </ext:DesktopModule>
                <%--Soporte--%>
            </Modules>  
            
            <Shortcuts>
                <ext:DesktopShortcut ShortcutID="scUsuarios"                                        Text="Usuarios"                                                    IconCls="shortcut-icon icon-usuarios" />
                <ext:DesktopShortcut ShortcutID="scRoles"                                           Text="Roles"                                                       IconCls="shortcut-icon icon-roles" />
                <ext:DesktopShortcut ShortcutID="scConfiguracionDeSistema"                          Text="Configuración de Sistema"                                    IconCls="shortcut-icon icon-configuracionDeSistema" />
                <ext:DesktopShortcut ShortcutID="scPlantillasNotificaciones"                        Text="Plantillas de Notificaciones"                                IconCls="shortcut-icon icon-plantillasNotificaciones" />
                <ext:DesktopShortcut ShortcutID="scVariablesDeEntorno"                              Text="Variables de Entorno"                                        IconCls="shortcut-icon icon-variablesEntorno" />
                <ext:DesktopShortcut ShortcutID="scApplicationLog"                                  Text="Bitácora de Aplicación"                                      IconCls="shortcut-icon icon-applicationLog" />
                <ext:DesktopShortcut ShortcutID="scSocios"                                          Text="Socios"                                                      IconCls="shortcut-icon icon-socios" />
                <ext:DesktopShortcut ShortcutID="scEstadosNotasDePeso"                              Text="Estados de Notas de Peso"                                    IconCls="shortcut-icon icon-estadosNotasDePeso" />
                <ext:DesktopShortcut ShortcutID="scNotasDePeso"                                     Text="Notas de Peso"                                               IconCls="shortcut-icon icon-notasDePeso" />
                <ext:DesktopShortcut ShortcutID="scClasificacionesDeCafe"                           Text="Clasificaciones de Café"                                     IconCls="shortcut-icon icon-clasificacionesDeCafe" />
                <ext:DesktopShortcut ShortcutID="scInventarioDeCafePorSocio"                        Text="Inventario de Café por Socio"                                IconCls="shortcut-icon icon-inventarioDeCafePorSocio" />
                <ext:DesktopShortcut ShortcutID="scInventarioDeCafe"                                Text="Inventario de Café"                                          IconCls="shortcut-icon icon-inventarioDeCafe" />
                <ext:DesktopShortcut ShortcutID="scHojasDeLiquidacion"                              Text="Hojas de Liquidación"                                        IconCls="shortcut-icon icon-hojaDeLiquidacion" />
                <ext:DesktopShortcut ShortcutID="scAjustesDeInventarioDeCafeDeSocios"               Text="Ajustes de Inventario de Café de Socios"                     IconCls="shortcut-icon icon-ajustesDeInventarioDeCafeDeSocios" />
                <ext:DesktopShortcut ShortcutID="scVentasDeInventarioDeCafe"                        Text="Ventas de Inventario de Café"                                IconCls="shortcut-icon icon-ventasDeInventarioDeCafe" />
                <ext:DesktopShortcut ShortcutID="scAportacionesPorSocio"                            Text="Aportaciones por Socio"                                      IconCls="shortcut-icon icon-aportacionesPorSocio" />
                <ext:DesktopShortcut ShortcutID="scRetiroDeAportaciones"                            Text="Retiro de Aportaciones"                                      IconCls="shortcut-icon icon-retiroAportaciones" />
                <ext:DesktopShortcut ShortcutID="scPrestamos"                                       Text="Tipos de Prestamo"                                           IconCls="shortcut-icon icon-prestamos" />
                <ext:DesktopShortcut ShortcutID="scSolicitudesDePrestamo"                           Text="Solicitudes de Prestamo"                                     IconCls="shortcut-icon icon-solicitudesDePrestamo" />
                <ext:DesktopShortcut ShortcutID="scPrestamosAprobados"                              Text="Solicitudes de Prestamo Aprobado"                            IconCls="shortcut-icon icon-prestamosAprobados" />
                <ext:DesktopShortcut ShortcutID="scReporteMovimientosInventarioDeCafeDeSocios"      Text="Reporte de Movimientos de Inventario de Café de Socios"      IconCls="shortcut-icon icon-reporteMovimientosInventarioDeCafeDeSocios" />
                <ext:DesktopShortcut ShortcutID="scReporteMovimientosInventarioDeCafeDeCooperativa" Text="Reporte de Movimientos de Inventario de Café de Coopetativa" IconCls="shortcut-icon icon-reporteMovimientosInventarioDeCafeDeCooperativa" />
                <ext:DesktopShortcut ShortcutID="scReporteDeHojasDeLiquidacion"                     Text="Reporte de Hojas de Liquidación"                             IconCls="shortcut-icon icon-reporteHojasDeLiquidacion" />
                <ext:DesktopShortcut ShortcutID="scReporteDetalleDeNotasDePeso"                     Text="Reporte Detalle de Notas de Peso"                            IconCls="shortcut-icon icon-reporteDetalleNotasDePeso" />
                <ext:DesktopShortcut ShortcutID="scReporteDetalleDeAportacionesPorSocio"            Text="Reporte Detalle de Aportaciones por Socio"                   IconCls="shortcut-icon icon-reporteDetalleAportacionesPorSocio" />
                <ext:DesktopShortcut ShortcutID="scReportePrestamosPorSocios"                       Text="Reporte de Prestamos por Socios"                             IconCls="shortcut-icon icon-reportePrestamosPorSocios" />
            </Shortcuts>

            <StartMenu Height="550" Width="500" ToolsWidth="127" Title="Start Menu" Icon="UserSuit">
                <ToolItems>
                    <ext:MenuItem Text="Opciones" Icon="Wrench">
                        <Listeners>
                            <Click Handler="WindowX.settings();" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem Text="Salir" Icon="Disconnect">
                        <DirectEvents>
                            <Click OnEvent="Logout_Click" After="window.clearInterval(DesktopintervalVariable)">
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
                    <ext:MenuItem ID="SettingsMenu" runat="server" Text="Configuración" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="ConfiguracionDeSistemaMenuItem" Text="Configuración de Sistema" Icon="ServerEdit">
                                        <Listeners>
                                            <Click Handler="WindowX.configuracionDeSistema(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="UtilsMenu" runat="server" Text="Utiles" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="PlantillasNotificacionesMenuItem" Text="Plantillas de Notificaciones" IconCls="icon-plantillasNotificaciones16">
                                        <Listeners>
                                            <Click Handler="WindowX.plantillasNotificaciones(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="EnvironmentMenu" runat="server" Text="Entorno" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="VariablesDeEntornoMenuItem" Text="Variables de Entorno" Icon="Database" >
                                        <Listeners>
                                            <click Handler="WindowX.variablesDeEntorno(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="SupportMenu" runat="server" Text="Soporte" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="ApplicationLogMenuItem" Text="Bitácora de Aplicación" Icon="Clipboard" >
                                        <Listeners>
                                            <click Handler="WindowX.applicationLog(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                    <ext:MenuItem ID="AsociatesMenu" runat="server" Text="Socios" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
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
                    <ext:MenuItem ID="NotasDePesoMenu" runat="server" Text="Notas de Peso" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="EstadosNotasDePesoMenuItem" Text="Estados de Notas de Peso" Icon="PageGo" >
                                        <Listeners>
                                            <click Handler="WindowX.estadosNotasDePeso(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="NotasDePesoMenuItem" Text="Notas de Peso" Icon="PageWhiteOffice" >
                                        <Listeners>
                                            <click Handler="WindowX.notasDePeso(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="InventarioDeCafeMenu" runat="server" Text="Inventario de Café" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="Menu1" runat="server">
                                <Items>
                                    <ext:MenuItem ID="ClasificacionesDeCafeMenuItem" Text="Clasificaciones de Café" Icon="Cup" >
                                        <Listeners>
                                            <click Handler="WindowX.clasificacionesDeCafe(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="InventarioDeCafePorSocioMenuItem" Text="Inventario de Café por Socio" Icon="Brick" >
                                        <Listeners>
                                            <click Handler="WindowX.inventarioDeCafePorSocio(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="AjustesDeInventarioDeCafeDeSociosMenuItem" Text="Ajustes de Inventario de Café de Socios" IconCls="icon-ajustesDeInventarioDeCafeDeSocios16" >
                                        <Listeners>
                                            <click Handler="WindowX.ajusteDeInventarioDeCafeDeSocios(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="InventarioDeCafeMenuItem" Text="Inventario de Café" Icon="Bricks" >
                                        <Listeners>
                                            <click Handler="WindowX.inventarioDeCafe(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="VentasDeInventarioDeCafeMenuItem" Text="Ventas de Inventario de Café" Icon="CartFull" >
                                        <Listeners>
                                            <click Handler="WindowX.ventasDeInventarioDeCafe(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="LiquidacionesMenu" runat="server" Text="Hojas de Liquidación" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="LiquidacionesMenuItem" Text="Hojas de Liquidación" Icon="Script" >
                                        <Listeners>
                                            <click Handler="WindowX.hojasDeLiquidacion(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="AportacionesPorSocioMenu" runat="server" Text="Aportaciones por Socio" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="AportacionesPorSocioMenuItem" Text="Aportaciones por Socio" IconCls="icon-aportacionesPorSocio16" >
                                        <Listeners>
                                            <click Handler="WindowX.aportacionesPorSocio(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>

                                    <ext:MenuItem ID="RetiroDeAportacionesMenuItem" Text="Retiro de Aportaciones" IconCls="icon-retiroAportaciones16" >
                                        <Listeners>
                                            <click Handler="WindowX.retiroDeAportaciones(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="PrestamosMenu" runat="server" Text="Prestamos" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="PrestamosMenuItem" Text="Tipos de Prestamo" IconCls="icon-prestamos16" >
                                        <Listeners>
                                            <click Handler="WindowX.prestamos(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="SolicitudesDePrestamoMenuItem" Text="Solicitudes de Prestamo" Icon="PageWhiteText" >
                                        <Listeners>
                                            <click Handler="WindowX.solicitudesDePrestamo(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="PrestamosAprobadosMenuItem" Text="Solicitudes de Prestamo Aprobado" IconCls="icon-prestamosAprobados16" >
                                        <Listeners>
                                            <click Handler="WindowX.prestamosAprobados(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuItem ID="ReportesMenu" runat="server" Text="Reportes" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem ID="ReporteMovimientosInventarioDeCafeDeSociosMenuItem" Text="Reporte de Movimientos de Inventario de Café de Socios" IconCls="icon-reporteMovimientosInventarioDeCafeDeSocios16" >
                                        <Listeners>
                                            <click Handler="WindowX.reporteMovimientosInventarioDeCafeDeSocios(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="ReporteMovimientosInventarioDeCafeDeCooperativaMenuItem" Text="Reporte de Movimientos de Inventario de Café de Cooperativa" IconCls="icon-reporteMovimientosInventarioDeCafeDeCooperativa16" >
                                        <Listeners>
                                            <click Handler="WindowX.reporteMovimientosInventarioDeCafeDeCooperativa(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    
                                    <ext:MenuItem ID="ReporteHojasDeLiquidacionMenuItem" Text="Reporte de Hojas de Liquidación" IconCls="icon-reporteHojasDeLiquidacion16" >
                                        <Listeners>
                                            <click Handler="WindowX.reporteHojasDeLiquidacion(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="ReporteDetalleDeNotasDePesoMenuItem" Text="Reporte Detalle de Notas de Peso" IconCls="icon-reporteDetalleNotasDePeso16" >
                                        <Listeners>
                                            <click Handler="WindowX.reporteDetalleDeNotasDePeso(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="ReporteDetalleDeAportacionesPorSocioMenuItem" Text="Reporte Detalle de Aportaciones por Socio" IconCls="icon-reporteDetalleAportacionesPorSocio16" >
                                        <Listeners>
                                            <click Handler="WindowX.reporteDetalleDeAportacionesPorSocio(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>

                                    <ext:MenuItem ID="ReportePrestamosPorSociosMenuItem" Text="Reporte de Prestamos por Socios" IconCls="icon-reportePrestamosPorSocios16" >
                                        <Listeners>
                                            <click Handler="WindowX.reportePrestamosPorSocios(#{MyDesktop});" />
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

        <%--Desktop--%>

        <ext:DesktopWindow
            ID="SettingsWin"
            runat="server"
            Title="Opciones"
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
                <ext:Panel ID="Panel2" runat="server" Height="200" Title="Notificaciones" Layout="FitLayout">
                    <Items>
                        <ext:Portal ID="portal2" runat="server" AutoHeight="true" Header="false" Layout="Column">
                            <Items>
                                <ext:PortalColumn ID="portalcolumn1" runat="server" StyleSpec="padding:10px 10px 10px 10px"
                                    ColumnWidth="1" Layout="Anchor" Height="200" >
                                    <Items>
                                        <ext:Portlet runat="server" Title="Bandeja de Notificaciones" Selectable="true" ID="Portlet12" Draggable="false" Collapsible="false" Icon="Mail">
                                            <Items>
                                                <ext:TableLayout ID="TableLayout2" runat="server" >
                                                    <Cells>
                                                        <ext:Cell>
                                                            <ext:ImageButton ID="NotificacioneBtn" runat="server" Height="32" Width="32"
                                                                ImageUrl="../resources/images/mail_box.png">
                                                                <Listeners>
                                                                    <Click Handler="#{NotificationsWin}.show();" />
                                                                </Listeners>
                                                            </ext:ImageButton>
                                                        </ext:Cell>
                                                        <ext:Cell CellCls="labelCell">
                                                            <ext:Label ID="Label111" runat="server" Text="Ver todas las notificaciones recibidas.">
                                                                <ToolTips>
                                                                    <ext:ToolTip runat="server" Html="Presione click sobre la imagen."></ext:ToolTip>
                                                                </ToolTips>
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
                                                        <ext:Cell CellCls="labelCell">
                                                            <ext:Label ID="Label11" runat="server" Text="Permite Cambiar la información del usuario actual.">
                                                                <ToolTips>
                                                                    <ext:ToolTip runat="server" Html="Presione click sobre la imagen."></ext:ToolTip>
                                                                </ToolTips>
                                                            </ext:Label>
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
                                                        <ext:Cell CellCls="labelCell">
                                                            <ext:Label ID="Label12" runat="server" Text="Permite Cambiar la contraseña del usuario actual." >
                                                                <ToolTips>
                                                                    <ext:ToolTip runat="server" Html="Presione click sobre la imagen."></ext:ToolTip>
                                                                </ToolTips>
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
            </Items>
        </ext:DesktopWindow>

        <ext:Store ID="dsReport" runat="server" OnRefreshData="dsReport_Refresh" AutoLoad="false">
            <Reader>
                <ext:JsonReader IDProperty="NOTIFICACION_ID">
                    <Fields>
                        <ext:RecordField Name="NOTIFICACION_ID" />
                        <ext:RecordField Name="NOTIFICACION_ESTADO" >
                            <Convert Handler="
                            switch(value)
                            {
                                case 1: return 'Notificando';
                                case 2: return 'Leido';
                               default: return 'Creado';
                            }"
                            />
                        </ext:RecordField>
                        <ext:RecordField Name="USR_USERNAME" />
                        <ext:RecordField Name="NOTIFICACION_TITLE" />
                        <ext:RecordField Name="NOTIFICACION_MENSAJE" />
                        <ext:RecordField Name="NOTIFICACION_FECHA" Type="Date" DateFormat="yyyy-MM-ddTHH:mm:ss"/>
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <BeforeLoad Handler="#{NotificacionesGridP}.plugins[0].collapseAll();" />
            </Listeners>
        </ext:Store>

        <ext:DesktopWindow
            ID="NotificationsWin"
            runat="server"
            Title="Notificaciones"
            Width="480"
            Maximizable="false"
            CloseAction="Hide"
            InitCenter="true"
            Icon="Mail"
            AutoHeight="true"
            Layout="FitLayout"
            Resizable="false"
            Hidden="true">
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <ext:Button runat="server" ID="DeleteReadNotificationBtn" Text="Eliminar Leidos" Icon="Delete">
                            <Listeners>
                                <Click Handler="DesktopX.deleteReadNotifications();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Listeners>
                <Show Handler="#{dsReport}.reload();" />
                <Hide Handler="#{dsReport}.removeAll();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" AutoScroll="true" Layout="FormLayout" AutoHeight="true">
                    <Items>
                        <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5">
                            <Items>
                                <ext:GridPanel ID="NotificacionesGridP" runat="server" AutoExpandColumn="NOTIFICACION_TITLE"
                                    Height="250" Title="Notificaciones" Header="false" Border="true" StripeRows="true"
                                    TrackMouseOver="true" SelectionMemory="Enabled" StoreID="dsReport">
                                    <Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel2">
                                        <Columns>
                                            <ext:Column     DataIndex="NOTIFICACION_ID"     Header="Id" Sortable="true"></ext:Column>
                                            <ext:DateColumn DataIndex="NOTIFICACION_FECHA"  Header="Fecha" Sortable="true" Format="yyyy-MM-dd" ></ext:DateColumn>
                                            <ext:Column     DataIndex="NOTIFICACION_ESTADO" Header="Estado" Sortable="true"></ext:Column>
                                            <ext:Column     DataIndex="NOTIFICACION_TITLE"  Header="Título" Sortable="true"></ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <Plugins>
                                        <ext:RowExpander ID="RowExpander1" runat="server">
                                            <Template ID="Template1" runat="server">
                                                <Html>
                                                    <div style="padding:5px;">
                                                    <b>
                                                        {NOTIFICACION_FECHA}
                                                    </b>
                                                    <br />
		                            				<b>Titulo: 
                                                        <div style="margin-left: 25px;">{NOTIFICACION_TITLE}</div>
                                                    </b>
                                                    <br />
                                                    <b>Estado: 
                                                        <div style="margin-left: 25px;">{NOTIFICACION_ESTADO}</div>
                                                    </b>
                                                    <br />
		                            				<b>Mensaje:
                                                        <div style="margin-left:25px !important; ">
                                                            {NOTIFICACION_MENSAJE}
                                                        </div>
                                                    </b>
                                                    </div>
		                            			</Html>
                                            </Template>
                                        </ext:RowExpander>
                                    </Plugins>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="10" StoreID="dsReport" />
                                    </BottomBar>
                                    <LoadMask ShowMask="true" />
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
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
            Height="250"
            Resizable="false"
            Hidden="true">
            <Items>
                <ext:Label runat="server" ID="Label1" Text="Proyecto de Graduación para Ingeniería en Sistemas Computacionales de UNITEC."></ext:Label>
                <ext:FieldSet runat="server" Padding="2">
                    <Items>
                        <ext:Label runat="server" ID="WebAssemblyTitle"></ext:Label>
                        <ext:Label runat="server" ID="WebAssemblyVersion"></ext:Label>
                        <ext:Label runat="server" ID="LogicAssemblyTitle"></ext:Label>
                        <ext:Label runat="server" ID="LogicAssemblyVersion"></ext:Label>
                        <ext:Label runat="server" ID="DataAccessAssemblyTitle"></ext:Label>
                        <ext:Label runat="server" ID="DataAccessAssemblyVersion"></ext:Label>
                    </Items>
                </ext:FieldSet>
            </Items>
        </ext:Window>

        <%--shortcuts' tooltips--%>

        <ext:ToolTip runat="server" ID="scUsuariosTooltip"                                        Html="Usuarios"                                                    Target="scUsuarios-shortcut"                                        ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scRolesTooltip"                                           Html="Roles"                                                       Target="scRoles-shortcut"                                           ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scConfiguracionDeSistemaTooltip"                          Html="Configuración de Sistema"                                    Target="scConfiguracionDeSistema-shortcut"                          ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scPlantillasNotificacionesTooltip"                        Html="Plantillas de Notificaciones"                                Target="scPlantillasNotificaciones-shortcut"                        ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scVariablesDeEntornoTooltip"                              Html="Variables de Entorno"                                        Target="scVariablesDeEntorno-shortcut"                              ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scApplicationLogTooltip"                                  Html="Bitácora de Aplicación"                                      Target="scApplicationLog-shortcut"                                  ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scSociosTooltip"                                          Html="Socios"                                                      Target="scSocios-shortcut"                                          ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scEstadosNotasDePesoTooltip"                              Html="Estados de Notas de Peso"                                    Target="scEstadosNotasDePeso-shortcut"                              ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scNotasDePesoTooltip"                                     Html="Notas de Peso"                                               Target="scNotasDePeso-shortcut"                                     ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scClasificacionesDeCafeTooltip"                           Html="Clasificaciones de Café"                                     Target="scClasificacionesDeCafe-shortcut"                           ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scInventarioDeCafePorSocioTooltip"                        Html="Inventario de Café por Socio"                                Target="scInventarioDeCafePorSocio-shortcut"                        ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scInventarioDeCafeTooltip"                                Html="Inventario de Café"                                          Target="scInventarioDeCafe-shortcut"                                ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scHojasDeLiquidacionTooltip"                              Html="Hojas de Liquidación"                                        Target="scHojasDeLiquidacion-shortcut"                              ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scAjustesDeInventarioDeCafeDeSociosTooltip"               Html="Ajustes de Inventario de Café de Socios"                     Target="scAjustesDeInventarioDeCafeDeSocios-shortcut"               ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scVentasDeInventarioDeCafeTooltip"                        Html="Ventas de Inventario de Café"                                Target="scVentasDeInventarioDeCafe-shortcut"                        ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scAportacionesPorSocioTooltip"                            Html="Aportaciones por Socio"                                      Target="scAportacionesPorSocio-shortcut"                            ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scRetiroDeAportacionesTooltip"                            Html="Retiro de Aportaciones"                                      Target="scRetiroDeAportaciones-shortcut"                            ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scPrestamosTooltip"                                       Html="Tipos de Prestamo"                                           Target="scPrestamos-shortcut"                                       ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scSolicitudesDePrestamoTooltip"                           Html="Solicitudes de Prestamo"                                     Target="scSolicitudesDePrestamo-shortcut"                           ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scPrestamosAprobadosTooltip"                              Html="Solicitudes de Prestamo Aprobado"                            Target="scPrestamosAprobados-shortcut"                              ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scReporteMovimientosInventarioDeCafeDeSociosTooltip"      Html="Reporte de Movimientos de Inventario de Café de Socios"      Target="scReporteMovimientosInventarioDeCafeDeSocios-shortcut"      ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scReporteMovimientosInventarioDeCafeDeCooperativaTooltip" Html="Reporte de Movimientos de Inventario de Café de Cooperativa" Target="scReporteMovimientosInventarioDeCafeDeCooperativa-shortcut" ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scReportePrestamosPorSociosTooltip"                       Html="Reporte de Prestamos por Socios"                             Target="scReportePrestamosPorSocios-shortcut"                       ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scReporteDeHojasDeLiquidacionTooltip"                     Html="Reporte de Hojas de Liquidación"                             Target="scReporteDeHojasDeLiquidacion-shortcut"                     ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scReporteDetalleDeNotasDePesoTooltip"                     Html="Reporte Detalle de Notas de Peso"                            Target="scReporteDetalleDeNotasDePeso-shortcut"                     ></ext:ToolTip>
        <ext:ToolTip runat="server" ID="scReporteDetalleDeAportacionesPorSocioTooltip"            Html="Reporte Detalle de Aportaciones por Socio"                   Target="scReporteDetalleDeAportacionesPorSocio-shortcut"            ></ext:ToolTip>

        <%--shortcuts' tooltips--%>

        <usera:UsuarioActual runat="server" ID="UsuarioActualCtl" />
        <cclave:CambiarClave runat="server" ID="CambiarClaveCtl" />
    </div>
    </form>
    <a href="http://www.unitec.edu" target="_blank" alt="Powered by Ext .Net" id="poweredby"> <div></div> </a>
</body>
</html>
