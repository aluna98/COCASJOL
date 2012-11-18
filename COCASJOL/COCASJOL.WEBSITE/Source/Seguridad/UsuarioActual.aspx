<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioActual.aspx.cs" Inherits="COCASJOL.Website.Source.Seguridad.UsuarioActual" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var PageX = {
            loadUser: function () {
                Ext.net.DirectMethods.CargarUsuario({ eventMask: { showMask: true, target: 'customtarget', customTarget: EditarUsuarioFormP } });
            },

            saveChanges: function () {
                Ext.net.DirectMethods.GuardarCambios({ eventMask: { showMask: true, target: 'customtarget', customTarget: EditarUsuarioFormP} }, { success: function () { Ext.Msg.alert('Guardar', 'Cambios guardados exitosamente.'); } });
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
            <Listeners>
                <DocumentReady Handler="PageX.loadUser();" />
            </Listeners>
        </ext:ResourceManager>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <%--<ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>--%>
                <ext:FormPanel ID="EditarUsuarioFormP" AutoHeight="true" runat="server" Title="Información" ButtonAlign="Right" MonitorValid="true">
                <%--<LayoutConfig>
        <ext:ColumnLayoutConfig FitHeight="false" />
    </LayoutConfig>--%>
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Title="Información" Layout="AnchorLayout" Resizable="false" Frame="false" Border="false" Header="false" Height="300" >
                            <Listeners>
                                <Activate Handler="ShowButtons();" />
                            </Listeners>
                            <Items>
                                <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                    <Items>
                                        <ext:TextField runat="server" ID="EditUsernameTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre de Usuario" AllowBlank="false" ReadOnly="true">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Html="El nombre de usuario es de solo lectura."
                                                    Title="Nombre de Usuario" Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:TextField runat="server" ID="EditNombreTxt"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre" AllowBlank="false" MsgTarget="Side"></ext:TextField>
                                        <ext:TextField runat="server" ID="EditApellidoTxt"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Apellido" AllowBlank="false" MsgTarget="Side"></ext:TextField>
                                        <ext:TextField runat="server" ID="EditCedulaTxt"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Cedula" AllowBlank="false" MsgTarget="Side"></ext:TextField>
                                        <ext:TextField runat="server" ID="EditEmailTxt"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Email" Vtype="email" MsgTarget="Side"></ext:TextField>
                                        <ext:TextField runat="server" ID="EditPuestoTxt"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Puesto" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="PageX.saveChanges();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            <%--</Items>
        </ext:Viewport>--%>
    </div>
    </form>
</body>
</html>
