<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracionDeSistema.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Configuracion.ConfiguracionDeSistema" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<meta http-equiv="refresh" content="3" />--%>
    <title>Configuración de Sistema</title>
    <script type="text/javascript" src="../../resources/js/configuracion/configuracionDeSistema.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" DisableViewState="true" >
            <Listeners>
                <DocumentReady Handler="PageX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Frame="false" Header="false" Icon="ApplicationOsxTerminal" Layout="FormLayout">
                    <Items>
                        <ext:FieldSet runat="server" Title="Ventanas" Layout="AnchorLayout">
                            <Items>
                                <ext:Panel ID="EditVentanasPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                    <LayoutConfig>
                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                    </LayoutConfig>
                                    <Items>
                                        <ext:Panel ID="Panel2" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                            <Items>
                                                <ext:Checkbox runat="server" ID="EditVentanasMaximizarChk" LabelWidth="200" LabelAlign="Right" FieldLabel="Maximizar Ventanas al Cargar" AllowBlank="false" MsgTarget="Side" ></ext:Checkbox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel3" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                            <Items>
                                                <ext:Checkbox runat="server" ID="EditVentanasCargarDatosChk" LabelWidth="200" LabelAlign="Right" FieldLabel="Cargar Datos al Iniciar" AllowBlank="false" MsgTarget="Side" ></ext:Checkbox>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet1" runat="server" Title="Consolidado de Inventario" Layout="AnchorLayout">
                            <Items>
                                <ext:Panel ID="EditReporteConsolidadoInventarioPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                    <LayoutConfig>
                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                    </LayoutConfig>
                                    <Items>
                                        <ext:Panel ID="Panel5" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                            <Items>
                                                <ext:DateField runat="server" ID="EditConsolidadoFechaInicialTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Inicio de Período" AllowBlank="false" MsgTarget="Side" ></ext:DateField>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel6" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                            <Items>
                                                <ext:DateField runat="server" ID="EditConsolidadoFechaFinalTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Final de Período" AllowBlank="false" MsgTarget="Side" ></ext:DateField>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet runat="server" Title="Correo" Layout="AnchorLayout">
                            <Items>
                                <ext:Panel ID="EditCorreoPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                    <LayoutConfig>
                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                    </LayoutConfig>
                                    <Items>
                                        <ext:Panel ID="Panel4" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                            <Items>
                                                <ext:TextField   runat="server" ID="EditCorreoLocalTxt"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Correo Local" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                                                <ext:Checkbox   runat="server" ID="EditCorreoUsarPasswordChk"  LabelAlign="Right" FieldLabel="Usar Contraseña" AllowBlank="false" MsgTarget="Side" >
                                                    <Listeners>
                                                        <Change Handler="if (#{EditCorreoUsarPasswordChk}.getValue() == true) #{EditCorreoPasswordTxt}.show(); else #{EditCorreoPasswordTxt}.hide(); " />
                                                    </Listeners>
                                                </ext:Checkbox>
                                                <ext:TextField   runat="server" ID="EditCorreoPasswordTxt"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Contraseña" AllowBlank="false" MsgTarget="Side" >
                                                    <Listeners>
                                                        <AfterRender Handler="if (#{EditCorreoUsarPasswordChk}.getValue() == true) #{EditCorreoPasswordTxt}.show(); else #{EditCorreoPasswordTxt}.hide(); " />
                                                    </Listeners>
                                                </ext:TextField>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel1" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                            <Items>
                                                <ext:TextField   runat="server" ID="EditCorreoSmtpServerTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Servidor SMTP" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField   runat="server" ID="EditCorreoPortTxt"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Puerto" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                                                <ext:Checkbox   runat="server" ID="EditCorreoUsarSSLChk"     LabelAlign="Right" FieldLabel="Usar SSL" AllowBlank="false" MsgTarget="Side" ></ext:Checkbox>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:FieldSet>

                        <ext:Panel ID="Panel7" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                            <LayoutConfig>
                                <ext:ColumnLayoutConfig FitHeight="false" />
                            </LayoutConfig>
                            <Items>
                                <ext:Panel ID="Panel8" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                    <Items>
                                        <ext:TextField   runat="server" ID="AuditUserName" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre de Usuario" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Usuario" Html="El nombre de usuario es de solo lectura." />
                                            </ToolTips>
                                        </ext:TextField>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel9" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                    <Items>
                                        <ext:DateField   runat="server" ID="AuditDate" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Fecha de Modificación" Html="La fecha de modificación es de solo lectura." />
                                            </ToolTips>
                                        </ext:DateField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button runat="server" ID="SaveBtn" Text="Guardar" Icon="Disk">
                            <Listeners>
                                <Click Handler="PageX.update();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
