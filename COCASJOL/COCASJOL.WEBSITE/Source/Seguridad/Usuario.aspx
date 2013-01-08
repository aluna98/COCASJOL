<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Seguridad.Usuario" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../resources/js/md5.js"></script>
    <script type="text/javascript">
        var Grid = null;
        var GridStore = null;
        var AddWindow = null;
        var AddForm = null;
        var EditWindow = null; 
        var EditForm = null;

        var AlertSelMsgTitle = "Atención";
        var AlertSelMsg = "Debe seleccionar 1 elemento";

        var ConfirmMsgTitle = "Usuario";
        var ConfirmUpdate = "Seguro desea modificar el usuario?";
        var ConfirmDelete = "Seguro desea eliminar el usuario?";

        var PageX = {
            _index: 0,

            setReferences: function () {
                Grid = UsuariosGridP;
                GridStore = UsuariosSt;
                AddWindow = AgregarUsuarioWin;
                AddForm = AddUsuarioFormP
                EditWindow = EditarUsuarioWin;
                EditForm = EditarUsuarioFormP;
            },

            add: function () {
                AddWindow.show();
            },

            insert: function () {
                AddPasswordTxt.setValue(md5(AddPasswordTxt.getValue()));
                var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

                Grid.insertRecord(0, fields, false);
                AddForm.getForm().reset();
            },

            insertRol: function () {
                if (RolesNoDeUsuarioGridP.getSelectionModel().hasSelection()) {
                    Ext.Msg.confirm('Agregar Roles', 'Seguro desea agregar estos roles?', function (btn, text) {
                        if (btn == 'yes') {
                            Ext.net.DirectMethods.AddRolesAddRolBtn_Click({ success: function () { RolesDeUsuarioSt.reload(); RolesNoDeUsuarioSt.reload(); Ext.Msg.alert('Agregar Roles', 'Roles agregado exitosamente.'); } }, { eventMask: { showMask: true, target: 'customtarget', customTarget: RolesNoDeUsuarioGridP} }, { failure: function () { Ext.Msg.alert('Agregar Roles', 'Error al agregar roles.'); } });
                        }
                    });
                } else {
                    var msg = Ext.Msg;
                    Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
                }
            },

            getIndex: function () {
                return this._index;
            },

            setIndex: function (index) {
                if (index > -1 && index < Grid.getStore().getCount()) {
                    this._index = index;
                }
            },

            getRecord: function () {
                var rec = Grid.getStore().getAt(this.getIndex());  // Get the Record

                if (rec != null) {
                    return rec;
                }
            },

            editPassword: function () {
                if (Grid.getSelectionModel().hasSelection()) {
                    var record = Grid.getSelectionModel().getSelected();
                    var index = Grid.store.indexOf(record);
                    this.setIndex(index);

                    rec = this.getRecord();

                    if (rec != null) {
                        CambiarClaveWin.show();
                        FormPanel2.getForm().loadRecord(rec);
                        FormPanel2.record = rec;
                    }
                } else {
                    Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
                }
            },

            edit: function () {
                if (Grid.getSelectionModel().hasSelection()) {
                    var record = Grid.getSelectionModel().getSelected();
                    var index = Grid.store.indexOf(record);
                    this.setIndex(index);
                    this.open();
                } else {
                    Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
                }
            },

            edit2: function (index) {
                this.setIndex(index);
                this.open();
            },

            next: function () {
                this.edit2(this.getIndex() + 1);
            },

            previous: function () {
                this.edit2(this.getIndex() - 1);
            },

            open: function () {
                rec = this.getRecord();

                if (rec != null) {
                    EditWindow.show();
                    EditForm.getForm().loadRecord(rec);
                    EditForm.record = rec;
                    RolesDeUsuarioGridP.getStore().removeAll();
                }
            },

            update: function () {
                if (EditForm.record == null) {
                    return;
                }

                Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
                    if (btn == 'yes') {
                        EditForm.getForm().updateRecord(EditForm.record);
                    }
                });
            },

            updatePassword: function () {
                Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
                    if (btn == 'yes') {
                        var encrypted = md5(CambiarClaveConfirmarTxt.getValue());
                        CambiarClaveTxt.setValue(encrypted);
                        CambiarClaveConfirmarTxt.setValue(encrypted);
                        Ext.net.DirectMethods.CambiarClaveGuardarBtn_Click({ success: function () { Ext.Msg.alert('Cambiar Contraseña', 'Contraseña actualizada exitosamente.'); FormPanel2.getForm().reset(); CambiarClaveWin.hide(); } }, { eventMask: { showMask: true, target: 'customtarget', customTarget: FormPanel2} });
                    }
                });
            },

            remove: function () {
                if (Grid.getSelectionModel().hasSelection()) {
                    Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
                        if (btn == 'yes') {
                            var record = Grid.getSelectionModel().getSelected();
                            Grid.deleteRecord(record);
                        }
                    });
                } else {
                    var msg = Ext.Msg;
                    Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
                }
            },

            removeRol: function () {
                if (RolesDeUsuarioGridP.getSelectionModel().hasSelection()) {
                    Ext.Msg.confirm('Eliminar Roles', 'Seguro desea eliminar estos roles?', function (btn, text) {
                        if (btn == 'yes') {
                            Ext.net.DirectMethods.EditUsuarioDeleteRolBtn_Click({ success: function () { RolesDeUsuarioSt.reload(); Ext.Msg.alert('Eliminar Roles', 'Roles eliminados exitosamente.'); } }, { eventMask: { showMask: true, target: 'customtarget', customTarget: RolesDeUsuarioGridP} }, { failure: function () { Ext.Msg.alert('Eliminar Roles', 'Error al eliminar roles.'); } });
                        }
                    });
                } else {
                    var msg = Ext.Msg;
                    Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
                }
            },

            keyUpEvent: function (sender, e) {
                if (e.getKey() == 13)
                    GridStore.reload();
            },

            keyUpEvent2: function (sender, e) {
                if (e.getKey() == 13)
                    RolesDeUsuarioSt.reload();
            },

            keyUpEvent3: function (sender, e) {
                if (e.getKey() == 13)
                    RolesNoDeUsuarioSt.reload();
            }
        };

        var HideButtons = function () {
            EditPreviousBtn.hide();
            EditNextBtn.hide();
            EditGuardarBtn.hide();
        }

        var ShowButtons = function () {
            EditPreviousBtn.show();
            EditNextBtn.show();
            EditGuardarBtn.show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
            <Listeners>
                <DocumentReady Handler="PageX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <aud:Auditoria runat="server" ID="AudWin" />

        <asp:ObjectDataSource ID="UsuariosDS" runat="server"
                TypeName="COCASJOL.LOGIC.Seguridad.UsuarioLogic"
                SelectMethod="GetUsuarios"
                InsertMethod="InsertarUsuario"
                UpdateMethod="ActualizarUsuario"
                DeleteMethod="EliminarUsuario" 
            onselecting="UsuarioDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="USR_USERNAME"         Type="String"   ControlID="f_USR_USERNAME"         PropertyName="Text" />
                    <asp:ControlParameter Name="USR_NOMBRE"           Type="String"   ControlID="f_USR_NOMBRE"           PropertyName="Text" />
                    <asp:ControlParameter Name="USR_SEGUNDO_NOMBRE"   Type="String"   ControlID="f_USR_SEGUNDO_NOMBRE"   PropertyName="Text" />
                    <asp:ControlParameter Name="USR_APELLIDO"         Type="String"   ControlID="f_USR_APELLIDO"         PropertyName="Text" />
                    <asp:ControlParameter Name="USR_SEGUNDO_APELLIDO" Type="String"   ControlID="f_USR_SEGUNDO_APELLIDO" PropertyName="Text" />
                    <asp:ControlParameter Name="USR_CEDULA"           Type="String"   ControlID="f_USR_CEDULA"           PropertyName="Text" />
                    <asp:ControlParameter Name="USR_CORREO"           Type="String"   ControlID="f_USR_CORREO"           PropertyName="Text" />
                    <asp:ControlParameter Name="USR_PUESTO"           Type="String"   ControlID="f_USR_PUESTO"           PropertyName="Text" />
                    <asp:ControlParameter Name="USR_PASSWORD"         Type="String"   ControlID="nullHdn"                PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="CREADO_POR"           Type="String"   ControlID="nullHdn"                PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"       Type="DateTime" ControlID="nullHdn"                PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"       Type="String"   ControlID="nullHdn"                PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION"   Type="DateTime" ControlID="nullHdn"                PropertyName="Text" DefaultValue="" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="USR_USERNAME"          Type="String" />
                    <asp:Parameter Name="USR_NOMBRE"            Type="String" />
                    <asp:Parameter Name="USR_SEGUNDO_NOMBRE"    Type="String" />
                    <asp:Parameter Name="USR_APELLIDO"          Type="String" />
                    <asp:Parameter Name="USR_SEGUNDO_APELLIDO"  Type="String" />
                    <asp:Parameter Name="USR_CEDULA"            Type="String" />
                    <asp:Parameter Name="USR_CORREO"            Type="String" />
                    <asp:Parameter Name="USR_PUESTO"            Type="String" />
                    <asp:Parameter Name="USR_PASSWORD"          Type="String" />
                    <asp:Parameter Name="CREADO_POR"            Type="String" />
                    <asp:Parameter Name="FECHA_CREACION"        Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"        Type="String" />
                    <asp:Parameter Name="FECHA_MODIFICACION"    Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="USR_USERNAME"          Type="String" />
                    <asp:Parameter Name="USR_NOMBRE"            Type="String" />
                    <asp:Parameter Name="USR_SEGUNDO_NOMBRE"    Type="String" />
                    <asp:Parameter Name="USR_APELLIDO"          Type="String" />
                    <asp:Parameter Name="USR_SEGUNDO_APELLIDO"  Type="String" />
                    <asp:Parameter Name="USR_CEDULA"            Type="String" />
                    <asp:Parameter Name="USR_CORREO"            Type="String" />
                    <asp:Parameter Name="USR_PUESTO"            Type="String" />
                    <asp:Parameter Name="USR_PASSWORD"          Type="String" />
                    <asp:Parameter Name="CREADO_POR"            Type="String" />
                    <asp:Parameter Name="FECHA_CREACION"        Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"        Type="String" />
                    <asp:Parameter Name="FECHA_MODIFICACION"    Type="DateTime" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name="USR_USERNAME" Type="String" />
                </DeleteParameters>
        </asp:ObjectDataSource>
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Usuarios" Icon="Group" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="UsuariosGridP" runat="server" AutoExpandColumn="USR_NOMBRE" Height="300"
                            Title="Usuarios" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="UsuariosSt" runat="server" DataSourceID="UsuariosDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="USR_USERNAME">
                                            <Fields>
                                                <ext:RecordField Name="USR_USERNAME"         />
                                                <ext:RecordField Name="USR_NOMBRE"           />
                                                <ext:RecordField Name="USR_SEGUNDO_NOMBRE"   />
                                                <ext:RecordField Name="USR_APELLIDO"         />
                                                <ext:RecordField Name="USR_SEGUNDO_APELLIDO" />
                                                <ext:RecordField Name="USR_CEDULA"           />
                                                <ext:RecordField Name="USR_CORREO"           />
                                                <ext:RecordField Name="USR_PUESTO"           />
                                                <ext:RecordField Name="USR_PASSWORD"         />
                                                <ext:RecordField Name="CREADO_POR"           />
                                                <ext:RecordField Name="FECHA_CREACION"       Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"       />
                                                <ext:RecordField Name="FECHA_MODIFICACION"   Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                    <Listeners>
                                        <CommitDone Handler="Ext.Msg.alert('Guardar', 'Cambios guardados exitosamente.');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="USR_USERNAME"         Header="Usuario" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_NOMBRE"           Header="Primer Nombre" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_SEGUNDO_NOMBRE"   Header="Segundo Nombre" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_APELLIDO"         Header="Primer Apellido" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_SEGUNDO_APELLIDO" Header="Segundo Apellido" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_CEDULA"           Header="Cedula" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_CORREO"           Header="Email" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_PUESTO"           Header="Puesto" Sortable="true"></ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="AgregarUsuarioBtn" runat="server" Text="Agregar" Icon="UserAdd" >
                                            <Listeners>
                                                <Click Handler="PageX.add();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EditarUsuarioBtn" runat="server" Text="Editar" Icon="UserEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="CambiarClaveBtn" runat="server" Text="Cambiar Contraseña" Icon="Key">
                                            <Listeners>
                                                <Click Handler="PageX.editPassword();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EliminarUsuarioBtn" runat="server" Text="Eliminar" Icon="UserDelete">
                                            <Listeners>
                                                <Click Handler="PageX.remove();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:Button ID="AuditoriaBtn" runat="server" Text="Auditoria" Icon="Cog">
                                            <Listeners>
                                                <Click Handler="PageX.showAudit();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <HeaderRows>
                                        <ext:HeaderRow>
                                            <Columns>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_USERNAME" runat="server" EnableKeyEvents="true" Icon="Find"  MaxLength="45">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_SEGUNDO_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_APELLIDO" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_SEGUNDO_APELLIDO" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_CEDULA" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="20">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_CORREO" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="30">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_PUESTO" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="30">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="UsuariosSt" />
                            </BottomBar>
                            <LoadMask ShowMask="true" />
                            <SaveMask ShowMask="true" />
                            <Listeners>
                                <RowDblClick Handler="PageX.edit();" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>

        <ext:Window ID="AgregarUsuarioWin"
            runat="server"
            Hidden="true"
            Icon="UserAdd"
            Title="Agregar Usuario"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
                <Show Handler="#{AddUsuarioFormP}.getForm().reset();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="AddUsuarioFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Items>
                        <ext:TabPanel ID="TabPanel1" runat="server">
                            <Items>
                                <ext:Panel ID="Panel2" runat="server" Title="Información" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Items>
                                        <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="AddUsernameTxt"         DataIndex="USR_USERNAME"         LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Nombre de Usuario" AllowBlank="false" MsgTarget="Side" Vtype="alphanum" IsRemoteValidation="true" >
                                                    <RemoteValidation OnValidation="AddUsernameTxt_Change" />
                                                </ext:TextField>
                                                <ext:TextField runat="server" ID="AddNombreTxt"           DataIndex="USR_NOMBRE"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Primer Nombre" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddSegundoNombreTxt"    DataIndex="USR_SEGUNDO_NOMBRE"   LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Segundo Nombre" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddApellidoTxt"         DataIndex="USR_APELLIDO"         LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Primer Apellido" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddSegundoApellidoTxt"  DataIndex="USR_SEGUNDO_APELLIDO" LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Segundo Apellido" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCedulaTxt"           DataIndex="USR_CEDULA"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="20" FieldLabel="Cedula" AllowBlank="false" MsgTarget="Side" Vtype="alphanum" IsRemoteValidation="true">
                                                    <RemoteValidation OnValidation="AddCedulaTxt_Change" />
                                                </ext:TextField>
                                                <ext:TextField runat="server" ID="AddEmailTxt"            DataIndex="USR_CORREO"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="30" FieldLabel="Email" Vtype="email"  MsgTarget="Side"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddPuestoTxt"           DataIndex="USR_PUESTO"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="30" FieldLabel="Puesto" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddPasswordTxt"         DataIndex="USR_PASSWORD"         LabelAlign="Right" AnchorHorizontal="90%" MaxLength="32" FieldLabel="Clave" InputType="Password" AllowBlank="false" MsgTarget="Side" MinLength="6"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCreatedByTxt"        DataIndex="CREADO_POR"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCreatedDateTxt"      DataIndex="FECHA_CREACION"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddModifiedByTxt"       DataIndex="MODIFICADO_POR"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddModificationDateTxt" DataIndex="FECHA_MODIFICACION"   LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="#{AddCreatedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.insert();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window ID="EditarUsuarioWin"
            runat="server"
            Hidden="true"
            Icon="UserEdit"
            Title="Editar Usuario"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
                <Show Handler="#{RolesDeUsuarioSt}.removeAll();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="EditarUsuarioFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Items>
                        <ext:TabPanel ID="TabPanel11" runat="server">
                            <Items>
                                <ext:Panel ID="Panel12" runat="server" Title="Información" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Listeners>
                                        <Activate Handler="ShowButtons();" />
                                    </Listeners>
                                    <Items>
                                        <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="EditUsernameTxt"        DataIndex="USR_USERNAME"         LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Nombre de Usuario" AllowBlank="false" ReadOnly="true">
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Html="El nombre de usuario es de solo lectura."
                                                            Title="Nombre de Usuario" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                                <ext:TextField runat="server" ID="EditNombreTxt"          DataIndex="USR_NOMBRE"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Nombre" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditSegundoNombreTxt"   DataIndex="USR_SEGUNDO_NOMBRE"   LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Segundo Nombre" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditApellidoTxt"        DataIndex="USR_APELLIDO"         LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Primer Apellido" AllowBlank="false" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditSegundoApellidoTxt" DataIndex="USR_SEGUNDO_APELLIDO" LabelAlign="Right" AnchorHorizontal="90%" MaxLength="45" FieldLabel="Segundo Apellido" MsgTarget="Side" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCedulaTxt"          DataIndex="USR_CEDULA"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="20" FieldLabel="Cedula" AllowBlank="false" MsgTarget="Side" Vtype="alphanum" IsRemoteValidation="true" >
                                                    <RemoteValidation OnValidation="EditCedulaTxt_Change" ValidationEvent="blur">
                                                    </RemoteValidation>
                                                </ext:TextField>
                                                <ext:TextField runat="server" ID="EditEmailTxt"           DataIndex="USR_CORREO"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="30" FieldLabel="Email" Vtype="email"  MsgTarget="Side"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditPuestoTxt"          DataIndex="USR_PUESTO"           LabelAlign="Right" AnchorHorizontal="90%" MaxLength="30" FieldLabel="Puesto" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditPasswordTxt"        DataIndex="USR_PASSWORD"         LabelAlign="Right" AnchorHorizontal="90%" MaxLength="32" FieldLabel="Clave" InputType="Password" AllowBlank="false" MsgTarget="Side" Hidden="true" ReadOnly="true"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCreatedByTxt"       DataIndex="CREADO_POR"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCreationDateTxt"    DataIndex="FECHA_CREACION"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditModifiedByTxt"      DataIndex="MODIFICADO_POR"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditModificationDate"   DataIndex="FECHA_MODIFICACION"   LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel14" runat="server" Title="Roles" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Listeners>
                                        <Activate Handler="HideButtons(); #{RolesDeUsuarioSt}.reload();" />
                                        <Deactivate Handler="#{RolesDeUsuarioSt}.removeAll();" />
                                    </Listeners>
                                    <Items>
                                        <ext:Panel ID="Panel16" runat="server" Frame="false" Padding="5" Layout="AnchorLayout"
                                            Border="false">
                                            <Items>
                                                <ext:GridPanel ID="RolesDeUsuarioGridP" runat="server" AutoExpandColumn="ROL_NOMBRE"
                                                    Height="250" Title="Roles de Usuario" Header="false" Border="true" StripeRows="true"
                                                    TrackMouseOver="true" SelectionMemory="Disabled">
                                                    <Store>   
                                                        <ext:Store ID="RolesDeUsuarioSt" runat="server" AutoSave="true" SkipIdForNewRecords="false" AutoLoad="false" OnRefreshData="RolesDeUsuarioSt_Refresh" >
                                                            <Reader>
                                                                <ext:JsonReader IDProperty="ROL_ID">
                                                                    <Fields>
                                                                        <ext:RecordField Name="ROL_ID" />
                                                                        <ext:RecordField Name="ROL_NOMBRE" />
                                                                        <ext:RecordField Name="ROL_DESCRIPCION" />
                                                                    </Fields>
                                                                </ext:JsonReader>
                                                            </Reader>
                                                            <BaseParams>
                                                                <ext:Parameter Name="USR_USERNAME" Mode="Raw" Value="#{EditUsernameTxt}.getValue()"></ext:Parameter>
                                                            </BaseParams>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel>
                                                        <Columns>
                                                            <ext:Column DataIndex="ROL_ID" Header="Id" Sortable="true"></ext:Column>
                                                            <ext:Column DataIndex="ROL_NOMBRE" Header="Nombre" Sortable="true"></ext:Column>
                                                        </Columns>
                                                    </ColumnModel>
                                                    <View>
                                                        <ext:GridView ID="GridView2" runat="server" AutoFill="false" ForceFit="false" >
                                                            <HeaderRows>
                                                                <ext:HeaderRow>
                                                                    <Columns>
                                                                        <ext:HeaderColumn />
                                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                                            <Component>
                                                                                <ext:NumberField ID="f_ROL_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                                    <Listeners>
                                                                                        <KeyUp Handler="PageX.keyUpEvent2(this, e);" />
                                                                                    </Listeners>
                                                                                </ext:NumberField>
                                                                            </Component>
                                                                        </ext:HeaderColumn>
                                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                                            <Component>
                                                                                <ext:TextField ID="f_ROL_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                                                    <Listeners>
                                                                                        <KeyUp Handler="PageX.keyUpEvent2(this, e);" />
                                                                                    </Listeners>
                                                                                </ext:TextField>
                                                                            </Component>
                                                                        </ext:HeaderColumn>
                                                                    </Columns>
                                                                </ext:HeaderRow>
                                                            </HeaderRows>
                                                        </ext:GridView>
                                                    </View>
                                                    <SelectionModel>
                                                        <ext:CheckboxSelectionModel ID="RolesDeUsuarioSelectionM" runat="server">
                                                        </ext:CheckboxSelectionModel>
                                                    </SelectionModel>
                                                    <TopBar>
                                                        <ext:Toolbar ID="Toolbar2" runat="server">
                                                            <Items>
                                                                <ext:Button ID="EditUsuarioAddRolBtn" runat="server" Text="Agregar" Icon="CogAdd">
                                                                    <Listeners>
                                                                        <Click Handler="#{AgregarRolesWin}.show();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:Button ID="EditUsuarioDeleteRolBtn" runat="server" Text="Eliminar" Icon="CogDelete">
                                                                    <Listeners>
                                                                        <Click Handler="PageX.removeRol();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <BottomBar>
                                                        <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="20" StoreID="RolesDeUsuarioSt" />
                                                    </BottomBar>
                                                    <LoadMask ShowMask="true" />
                                                    <SaveMask ShowMask="true" />
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="EditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                            <Listeners>
                                <Click Handler="PageX.previous();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                            <Listeners>
                                <Click Handler="PageX.next();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="#{EditModifiedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.update();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window ID="AgregarRolesWin" runat="server" Hidden="true" Icon="CogAdd" Title="Agregar Roles"
            Width="400" Layout="FormLayout" AutoHeight="True" Resizable="false" Shadow="None"
            X="30" Y="70" Modal="true">
            <Listeners>
                <Show Handler="#{RolesNoDeUsuarioSt}.reload();" />
                <Hide Handler="#{RolesNoDeUsuarioSt}.removeAll();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right">
                    <Items>
                        <ext:Panel ID="Panel9" runat="server" Frame="false" Padding="5">
                            <Items>
                                <ext:GridPanel ID="RolesNoDeUsuarioGridP" runat="server" AutoExpandColumn="ROL_NOMBRE"
                                    Height="250" Title="Agregar Roles" Header="false" Border="true" StripeRows="true"
                                    TrackMouseOver="true" SelectionMemory="Enabled">
                                    <Store>
                                        <ext:Store ID="RolesNoDeUsuarioSt" runat="server" OnRefreshData="RolesNoDeUsuarioSt_Refresh"
                                            WarningOnDirty="false">
                                            <Reader>
                                                <ext:JsonReader IDProperty="ROL_ID">
                                                    <Fields>
                                                        <ext:RecordField Name="ROL_ID" />
                                                        <ext:RecordField Name="ROL_NOMBRE" />
                                                        <ext:RecordField Name="ROL_DESCRIPCION" />
                                                    </Fields>
                                                </ext:JsonReader>
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel2">
                                        <Columns>
                                            <ext:Column DataIndex="ROL_ID" Header="Id" Sortable="true"></ext:Column>
                                            <ext:Column DataIndex="ROL_NOMBRE" Header="Nombre" Sortable="true"></ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView3" runat="server" AutoFill="false" ForceFit="false">
                                            <HeaderRows>
                                                <ext:HeaderRow>
                                                    <Columns>
                                                        <ext:HeaderColumn />
                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                            <Component>
                                                                <ext:NumberField ID="f2_ROL_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                    <Listeners>
                                                                        <KeyUp Handler="PageX.keyUpEvent3(this, e);" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                            </Component>
                                                        </ext:HeaderColumn>
                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                            <Component>
                                                                <ext:TextField ID="f2_ROL_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                                    <Listeners>
                                                                        <KeyUp Handler="PageX.keyUpEvent3(this, e);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Component>
                                                        </ext:HeaderColumn>
                                                    </Columns>
                                                </ext:HeaderRow>
                                            </HeaderRows>
                                        </ext:GridView>
                                    </View>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel ID="RolesNoDeUsuarioSelectionM" runat="server">
                                        </ext:CheckboxSelectionModel>
                                    </SelectionModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar3" runat="server">
                                            <Items>
                                                <ext:Button ID="AddRolesAddRolBtn" runat="server" Text="Agregar" Icon="CogAdd">
                                                    <Listeners>
                                                        <Click Handler="PageX.insertRol();" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="10" StoreID="RolesNoDeUsuarioSt" />
                                    </BottomBar>
                                    <LoadMask ShowMask="true" />
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    
        <ext:Window ID="CambiarClaveWin" runat="server" Hidden="true" Icon="Key" Title="Cambiar Contraseña"
            Width="400" Layout="FormLayout" AutoHeight="True" Resizable="false" Shadow="None"
            X="30" Y="70" Modal="true">
            <Listeners>
                <Show Handler="#{FormPanel2}.getForm().reset();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="FormPanel2" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Items>
                        <ext:Panel ID="Panel4" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                            <Items>
                                <ext:TextField runat="server" ID="CambiarClaveUsernameTxt"  DataIndex="USR_USERNAME" MaxLength="32" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre de Usuario" AllowBlank="false" Hidden="true" ReadOnly="true"></ext:TextField>
                                <ext:TextField runat="server" ID="CambiarClaveTxt"                                   MaxLength="32" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nueva Contraseña"     InputType="Password" AllowBlank="false" MsgTarget="Side" MinLength="6" ></ext:TextField>
                                <ext:TextField runat="server" ID="CambiarClaveConfirmarTxt"                          MaxLength="32" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Confirmar Contraseña" InputType="Password" AllowBlank="false" Vtype="password" MsgTarget="Side" MinLength="6" >
                                    <CustomConfig>
                                        <ext:ConfigItem Name="initialPassField" Value="#{CambiarClaveTxt}" Mode="Value" />
                                    </CustomConfig>
                                </ext:TextField>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="CambiarClaveGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="PageX.updatePassword();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>