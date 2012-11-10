<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Privilegio.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Seguridad.Privilegio" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        var ConfirmMsgTitle = "Privilegio";
        var ConfirmUpdate = "Seguro desea modificar el privilegio?";
        var ConfirmDelete = "Seguro desea eliminar el privilegio?";

        var PageX = {
            _index: 0,

            setReferences: function () {
                Grid = PrivilegiosGridP;
                GridStore = PrivilegiosSt;
                AddWindow = AgregarPrivilegioWin;
                AddForm = AddPrivilegioFormP
                EditWindow = EditarRolWin;
                EditForm = EditarRolFormP;
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
        <ext:ResourceManager ID="ResourceManager1" runat="server">
            <Listeners>
                <DocumentReady Handler="PageX.setReferences()" />
            </Listeners>
        </ext:ResourceManager>

        <asp:ObjectDataSource ID="PrivilegioDS" runat="server"
                TypeName="COCASJOL.LOGIC.PrivilegioLogic"
                SelectMethod="GetPrivilegios"
                InsertMethod="InsertarPrivilegio"
                UpdateMethod="ActualizarPrivilegio"
                DeleteMethod="EliminarPrivilegio" >
                <SelectParameters>
                    <asp:ControlParameter Name="PRIV_ID"            Type="Int32"    ControlID="f_PRIV_ID"          PropertyName="Text" />
                    <asp:ControlParameter Name="PRIV_NOMBRE"        Type="String"   ControlID="f_PRIV_NOMBRE"      PropertyName="Text" />
                    <asp:ControlParameter Name="PRIV_DESCRIPCION"   Type="String"   ControlID="f_PRIV_DESCRIPCION" PropertyName="Text" />
                    <asp:ControlParameter Name="PRIV_LLAVE"         Type="String"   ControlID="f_PRIV_LLAVE"       PropertyName="Text" />
                    <asp:ControlParameter Name="CREADO_POR"         Type="String"   ControlID="nullHdn"            PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"     Type="DateTime" ControlID="nullHdn"            PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"     Type="String"   ControlID="nullHdn"            PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION" Type="DateTime" ControlID="nullHdn"            PropertyName="Text" DefaultValue="" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="PRIV_ID"            Type="Int32" />
                    <asp:Parameter Name="PRIV_NOMBRE"        Type="String" />
                    <asp:Parameter Name="PRIV_DESCRIPCION"   Type="String" />
                    <asp:Parameter Name="PRIV_LLAVE"         Type="String" />
                    <asp:Parameter Name="CREADO_POR"         Type="String" />
                    <asp:Parameter Name="FECHA_CREACION"     Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"     Type="String" />
                    <asp:Parameter Name="FECHA_MODIFICACION" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="PRIV_ID"            Type="Int32" />
                    <asp:Parameter Name="PRIV_NOMBRE"        Type="String" />
                    <asp:Parameter Name="PRIV_DESCRIPCION"   Type="String" />
                    <asp:Parameter Name="PRIV_LLAVE"         Type="String" />
                    <asp:Parameter Name="CREADO_POR"         Type="String" />
                    <asp:Parameter Name="FECHA_CREACION"     Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"     Type="String" />
                    <asp:Parameter Name="FECHA_MODIFICACION" Type="DateTime" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name="PRIV_ID" Type="Int32" />
                </DeleteParameters>
        </asp:ObjectDataSource>
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Privilegios" Icon="GroupKey" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="PrivilegiosGridP" runat="server" AutoExpandColumn="PRIV_DESCRIPCION" Height="300"
                            Title="Usuarios" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="PrivilegiosSt" runat="server" DataSourceID="PrivilegioDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="PRIV_ID">
                                            <Fields>
                                                <ext:RecordField Name="PRIV_ID"            />
                                                <ext:RecordField Name="PRIV_LLAVE"         />
                                                <ext:RecordField Name="PRIV_NOMBRE"        />
                                                <ext:RecordField Name="PRIV_DESCRIPCION"   />
                                                <ext:RecordField Name="CREADO_POR"         />
                                                <ext:RecordField Name="FECHA_CREACION"     Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"     />
                                                <ext:RecordField Name="FECHA_MODIFICACION" Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="PRIV_ID"          Header="Id de Privilegio" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="PRIV_LLAVE"       Header="Llave" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="PRIV_NOMBRE"      Header="Nombre" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="PRIV_DESCRIPCION" Header="Descripción" Sortable="true"></ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="AgregarPrivilegioBtn" runat="server" Text="Agregar" Icon="KeyAdd" >
                                            <Listeners>
                                                <Click Handler="PageX.add();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EditarPrivilegioBtn" runat="server" Text="Editar" Icon="KeyGo">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EliminarPrivilegioBtn" runat="server" Text="Eliminar" Icon="KeyDelete">
                                            <Listeners>
                                                <Click Handler="PageX.remove();" />
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
                                                        <ext:TextField ID="f_PRIV_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_PRIV_LLAVE" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_PRIV_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_PRIV_DESCRIPCION" runat="server" EnableKeyEvents="true" Icon="Find">
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
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="PrivilegiosSt" />
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

        <ext:Window ID="AgregarPrivilegioWin"
            runat="server"
            Hidden="true"   
            Icon="KeyAdd"
            Title="Agregar Privilegio"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="AddPrivilegioFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true">
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
                                                <ext:TextField runat="server" ID="AddIdTxt"               DataIndex="PRIV_ID"            LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Privilegio" AllowBlank="false" IndicatorIcon="BulletRed"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddLlaveTxt"            DataIndex="PRIV_LLAVE"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Llave" AllowBlank="false" IndicatorIcon="BulletRed"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddNombreTxt"           DataIndex="PRIV_NOMBRE"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre" AllowBlank="false" IndicatorIcon="BulletRed"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddDescripcionTxt"      DataIndex="PRIV_DESCRIPCION"   LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Descripción"></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCreatedByTxt"        DataIndex="CREADO_POR"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddCreatedDateTxt"      DataIndex="FECHA_CREACION"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddModifiedByTxt"       DataIndex="MODIFICADO_POR"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddModificationDateTxt" DataIndex="FECHA_MODIFICACION" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
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

        <ext:Window ID="EditarRolWin"
            runat="server"
            Hidden="true"
            Icon="CogEdit"
            Title="Editar Rol"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="EditarRolFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Listeners>
                        <Show Handler="this.getForm().reset();" />
                    </Listeners>
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
                                                <ext:TextField runat="server" ID="EditIdTxt"            DataIndex="PRIV_ID"            LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Privilegio" AllowBlank="false" ReadOnly="true"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditLlaveTxt"         DataIndex="PRIV_LLAVE"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Llave" AllowBlank="false" IndicatorIcon="BulletRed"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditNombreTxt"        DataIndex="PRIV_NOMBRE"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre" AllowBlank="false" IndicatorIcon="BulletRed"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditDescripcionTxt"   DataIndex="PRIV_DESCRIPCION"   LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Descripción"></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCreatedByTxt"     DataIndex="CREADO_POR"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditCreationDateTxt"  DataIndex="FECHA_CREACION"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditModifiedByTxt"    DataIndex="MODIFICADO_POR"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditModificationDate" DataIndex="FECHA_MODIFICACION" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
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

