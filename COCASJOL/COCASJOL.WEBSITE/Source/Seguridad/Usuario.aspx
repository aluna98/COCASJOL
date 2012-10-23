<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Seguridad.Usuario" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

                Grid.insertRecord(0, fields, false);
                AddForm.getForm().reset();
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

            edit: function () {
                if (Grid.getSelectionModel().hasSelection()) {
                    var record = Grid.getSelectionModel().getSelected();
                    var index = Grid.store.indexOf(record);
                    this.setIndex(index);
                    this.open();
                } else {
                    var msg = Ext.Msg;
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

            keyUpEvent: function (sender, e) {
                if (e.getKey() == 13) 
                    GridStore.reload();
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
            <Listeners>
                <DocumentReady Handler="PageX.setReferences()" />
            </Listeners>
        </ext:ResourceManager>

        <asp:ObjectDataSource ID="UsuariosDS" runat="server"
                TypeName="COCASJOL.LOGIC.UsuarioLogic"
                SelectMethod="GetUsuarios"
                InsertMethod="InsertarUsuario"
                UpdateMethod="ActualizarUsuario"
                DeleteMethod="EliminarUsuario" >
                <SelectParameters>
                    <asp:ControlParameter Name="USR_USERNAME"       Type="String"   ControlID="f_USR_USERNAME"  PropertyName="Text" />
                    <asp:ControlParameter Name="USR_NOMBRE"         Type="String"   ControlID="f_USR_NOMBRE"    PropertyName="Text" />
                    <asp:ControlParameter Name="USR_APELLIDO"       Type="String"   ControlID="f_USR_APELLIDO"  PropertyName="Text" />
                    <asp:ControlParameter Name="USR_CEDULA"         Type="Int32"    ControlID="f_USR_CEDULA"    PropertyName="Text" />
                    <asp:ControlParameter Name="USR_CORREO"         Type="String"   ControlID="f_USR_CORREO"    PropertyName="Text" />
                    <asp:ControlParameter Name="USR_PUESTO"         Type="String"   ControlID="f_USR_PUESTO"    PropertyName="Text" />
                    <asp:ControlParameter Name="USR_PASSWORD"       Type="String"   ControlID="nullHdn"         PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="CREADO_POR"         Type="String"   ControlID="nullHdn"         PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"     Type="DateTime" ControlID="nullHdn"         PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"     Type="String"   ControlID="nullHdn"         PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION" Type="DateTime" ControlID="nullHdn"         PropertyName="Text" DefaultValue="" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="USR_USERNAME"          Type="String" />
                    <asp:Parameter Name="USR_NOMBRE"            Type="String" />
                    <asp:Parameter Name="USR_APELLIDO"          Type="String" />
                    <asp:Parameter Name="USR_CEDULA"            Type="Int32" />
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
                    <asp:Parameter Name="USR_APELLIDO"          Type="String" />
                    <asp:Parameter Name="USR_CEDULA"            Type="Int32" />
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
                                <ext:Store ID="UsuariosSt" runat="server" DataSourceID="UsuariosDS" AutoSave="true" SkipIdForNewRecords="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="USR_USERNAME">
                                            <Fields>
                                                <ext:RecordField Name="USR_USERNAME"        />
                                                <ext:RecordField Name="USR_NOMBRE"          />
                                                <ext:RecordField Name="USR_APELLIDO"        />
                                                <ext:RecordField Name="USR_CEDULA"          />
                                                <ext:RecordField Name="USR_CORREO"          />
                                                <ext:RecordField Name="USR_PUESTO"          />
                                                <ext:RecordField Name="USR_PASSWORD"        />
                                                <ext:RecordField Name="CREADO_POR"          />
                                                <ext:RecordField Name="FECHA_CREACION"      Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"      />
                                                <ext:RecordField Name="FECHA_MODIFICACION"  Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="USR_USERNAME" Header="Usuario" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_NOMBRE"   Header="Nombre" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_APELLIDO" Header="Apellido" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_CEDULA"   Header="Cedula" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_CORREO"   Header="Email" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_PUESTO"   Header="Puesto" Sortable="true"></ext:Column>
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
                                        <ext:Button ID="EliminarUsuarioBtn" runat="server" Text="Eliminar" Icon="UserDelete">
                                            <Listeners>
                                                <Click Handler="PageX.remove()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarFill />
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
                                                        <ext:TextField ID="f_USR_USERNAME" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_APELLIDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_CEDULA" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_CORREO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_USR_PUESTO" runat="server" EnableKeyEvents="true" Icon="Find">
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
                                <RowDblClick Handler="PageX.edit()" />
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
            <Items>
                <ext:FormPanel ID="AddUsuarioFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Listeners>
                        <Show Handler="this.getForm().reset();" />
                    </Listeners>
                    <Items>
                        <ext:TabPanel ID="TabPanel1" runat="server">
                            <Items>
                                <ext:Panel ID="Panel2" runat="server" Title="Información" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Items>
                                        <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="AddUsernameTxt"           DataIndex="USR_USERNAME"        LabelAlign="Right" FieldLabel="Nombre de Usuario" AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddNombreTxt"             DataIndex="USR_NOMBRE"          LabelAlign="Right" FieldLabel="Nombre" AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddApellidoTxt"           DataIndex="USR_APELLIDO"        LabelAlign="Right" FieldLabel="Apellido" AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCedulaTxt"             DataIndex="USR_CEDULA"          LabelAlign="Right" FieldLabel="Cedula" AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddEmailTxt"              DataIndex="USR_CORREO"          LabelAlign="Right" FieldLabel="Email" AnchorHorizontal="90%" Vtype="email" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddPuestoTxt"             DataIndex="USR_PUESTO"          LabelAlign="Right" FieldLabel="Puesto" AnchorHorizontal="90%"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddPasswordTxt"           DataIndex="USR_PASSWORD"        LabelAlign="Right" FieldLabel="Clave" AnchorHorizontal="90%" InputType="Password" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCreatedByTxt"          DataIndex="CREADO_POR"          LabelAlign="Right" FieldLabel="Creado por" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCreatedDateTxt"        DataIndex="FECHA_CREACION"      LabelAlign="Right" FieldLabel="Fecha de Creacion" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddModifiedByTxt"         DataIndex="MODIFICADO_POR"      LabelAlign="Right" FieldLabel="Modificado por" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddModificationDateTxt"   DataIndex="FECHA_MODIFICACION"  LabelAlign="Right" FieldLabel="Fecha de Modificacion" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
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
            <Items>
                <ext:FormPanel ID="EditarUsuarioFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Listeners>
                        <Show Handler="this.getForm().reset();" />
                    </Listeners>
                    <Items>
                        <ext:TabPanel ID="TabPanel11" runat="server">
                            <Items>
                                <ext:Panel ID="Panel12" runat="server" Title="Información" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Items>
                                        <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="EditUsernameTxt"      DataIndex="USR_USERNAME"        LabelAlign="Right" FieldLabel="Nombre de Usuario" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditNombreTxt"        DataIndex="USR_NOMBRE"          LabelAlign="Right" FieldLabel="Nombre" AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditApellidoTxt"      DataIndex="USR_APELLIDO"        LabelAlign="Right" FieldLabel="Apellido" AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCedulaTxt"        DataIndex="USR_CEDULA"          LabelAlign="Right" FieldLabel="Cedula" AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditEmailTxt"         DataIndex="USR_CORREO"          LabelAlign="Right" FieldLabel="Email" AnchorHorizontal="90%" Vtype="email" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditPuestoTxt"        DataIndex="USR_PUESTO"          LabelAlign="Right" FieldLabel="Puesto" AnchorHorizontal="90%"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditPasswordTxt"      DataIndex="USR_PASSWORD"        LabelAlign="Right" FieldLabel="Clave" AnchorHorizontal="90%" InputType="Password"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCreatedByTxt"     DataIndex="CREADO_POR"          LabelAlign="Right" FieldLabel="Creado_por" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCreationDateTxt"  DataIndex="FECHA_CREACION"      LabelAlign="Right" FieldLabel="Fecha de Creacion" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditModifiedByTxt"    DataIndex="MODIFICADO_POR"      LabelAlign="Right" FieldLabel="Modificado por" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditModificationDate" DataIndex="FECHA_MODIFICACION"  LabelAlign="Right" FieldLabel="Fecha de Modificacion" AnchorHorizontal="90%" Hidden="true" ></ext:TextField>
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
    </div>
    </form>
</body>
</html>
