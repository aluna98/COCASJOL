﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsuarioActual.ascx.cs" Inherits="COCASJOL.WEBSITE.Source.Seguridad.UsuarioActual" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<ext:XScript runat="server" >
<script type="text/javascript">
    var loadUser = function () {
        #{DirectMethods}.CargarUsuario({ eventMask: { showMask: true, target: 'customtarget', customTarget: #{EditarUsuarioFormP} } });
    }

    saveChanges = function () {
        #{DirectMethods}.GuardarCambios({ eventMask: { showMask: true, target: 'customtarget', customTarget: #{EditarUsuarioFormP} } }, { success: function () { Ext.Msg.alert('Guardar', 'Cambios guardados exitosamente.'); } });
    }
</script> 
</ext:XScript>

<ext:DesktopWindow
    ID="UsuarioActualWin"
    runat="server"
    Title="Editar Usuario"
    Width="300"
    Maximizable="false"
    BodyBorder="false"
    Icon="UserEdit"
    AutoHeight="true"
    Resizable="false"
    Layout="FormLayout"
    Modal="true" ConstrainHeader="true" Shim="false" Minimizable="false" AnimCollapse="false" >
        <Listeners>
            <Show Handler="loadUser();" />
        </Listeners>
        <Items>
            <ext:FormPanel ID="EditarUsuarioFormP" AutoHeight="true" runat="server" Title="Información" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                <Items>
                    <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                        <Items>
                            <ext:TextField runat="server" ID="EditUsernameTxt"        MaxLength="45" LabelAlign="Right" AnchorHorizontal="90%"
                                FieldLabel="Nombre de Usuario" AllowBlank="false" ReadOnly="true">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip1" runat="server" Html="El nombre de usuario es de solo lectura."
                                        Title="Nombre de Usuario" Width="200" TrackMouse="true" />
                                </ToolTips>
                            </ext:TextField>
                            <ext:TextField runat="server" ID="EditNombreTxt"          MaxLength="45" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Primer Nombre" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                            <ext:TextField runat="server" ID="EditSegundoNombreTxt"   MaxLength="45" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Segundo Nombre" MsgTarget="Side" ></ext:TextField>
                            <ext:TextField runat="server" ID="EditApellidoTxt"        MaxLength="45" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Primer Apellido" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                            <ext:TextField runat="server" ID="EditSegundoApellidoTxt" MaxLength="45" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Segundo Apellido" MsgTarget="Side" ></ext:TextField>
                            <ext:TextField runat="server" ID="EditEmailTxt"           MaxLength="30" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Email" Vtype="email" MsgTarget="Side"></ext:TextField>
                        </Items>
                    </ext:Panel>
                </Items>
                <Buttons>
                    <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                        <Listeners>
                            <Click Handler="saveChanges();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
</ext:DesktopWindow>
