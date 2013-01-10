<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AportacionesPorSocio.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Aportaciones.AportacionesPorSocio" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        var Grid = null;
        var GridStore = null;
        var EditWindow = null;
        var EditForm = null;

        var AlertSelMsgTitle = "Atención";
        var AlertSelMsg = "Debe seleccionar 1 elemento";

        var ConfirmMsgTitle = "Aportaciones";
        var ConfirmUpdate = "Seguro desea modificar las aportaciones?";
        var ConfirmDelete = "Seguro desea eliminar las aportaciones?";

        var PageX = {
            _index: 0,

            setReferences: function () {
                Grid = AportacionesGridP;
                GridStore = Grid.getStore();
                EditWindow = EditarAportacionWin;
                EditForm = EditarAportacionFormP;
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

                    var EditSocioId = Ext.getCmp('EditSociosIdTxt');
                    var EditNombre = Ext.getCmp('EditNombreTxt');

                    this.getNombreDeSocio(EditSocioId, EditNombre);
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

            getNombreDeSocio: function (sociosIdTxt, nombreTxt) {
                var value = sociosIdTxt.getValue();
                var record = SocioSt.getById(value);

                var nombreCompleto = record.data.SOCIOS_PRIMER_NOMBRE +
                                     (record.data.SOCIOS_SEGUNDO_NOMBRE != '' ? (' ' + record.data.SOCIOS_SEGUNDO_NOMBRE) : '') +
                                     (record.data.SOCIOS_PRIMER_APELLIDO !== '' ? (' ' + record.data.SOCIOS_PRIMER_APELLIDO) : '') +
                                     (record.data.SOCIOS_SEGUNDO_APELLIDO != '' ? (' ' + record.data.SOCIOS_SEGUNDO_APELLIDO) : '');

                nombreTxt.setValue(nombreCompleto);
            },

            keyUpEvent: function (sender, e) {
                if (e.getKey() == 13)
                    GridStore.reload();
            },

            reloadGridStore: function () {
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
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
            <Listeners>
                <DocumentReady Handler="PageX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <aud:Auditoria runat="server" ID="AudWin" />

        <asp:ObjectDataSource ID="AportacionesDs" runat="server"
                TypeName="COCASJOL.LOGIC.Aportaciones.AportacionLogic"
                SelectMethod="GetAportaciones" onselecting="AportacionesDs_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="SOCIOS_ID"                   Type="String"   ControlID="f_SOCIOS_ID"               PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="APORTACIONES_SALDO"          Type="Decimal"  ControlID="f_APORTACIONES_SALDO"      PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="CREADO_POR"                  Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"              Type="DateTime" ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"              Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION"          Type="DateTime" ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                </SelectParameters>
        </asp:ObjectDataSource>
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Inventario de Café" Icon="Basket" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="AportacionesGridP" runat="server" AutoExpandColumn="APORTACIONES_SALDO" Height="300"
                            Title="Usuarios" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="AportacionesSt" runat="server" DataSourceID="AportacionesDs" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="SOCIOS_ID">
                                            <Fields>
                                                <ext:RecordField Name="SOCIOS_ID"          />
                                                <ext:RecordField Name="APORTACIONES_SALDO" />
                                                <ext:RecordField Name="CREADO_POR"         />
                                                <ext:RecordField Name="FECHA_CREACION"     Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"     />
                                                <ext:RecordField Name="FECHA_MODIFICACION" Type="Date" />
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
                                    <ext:Column       DataIndex="SOCIOS_ID"           Header="Id de Socio" Sortable="true"></ext:Column>
                                    <ext:NumberColumn DataIndex="APORTACIONES_SALDO"  Header="Saldo" Sortable="true"></ext:NumberColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="EditarBtn" runat="server" Text="Editar" Icon="Coins">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
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
                                                        <ext:TextField ID="f_SOCIOS_ID" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="5">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_APORTACIONES_SALDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="AportacionesSt" />
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

        <ext:Window ID="EditarAportacionWin"
            runat="server"
            Hidden="true"
            Icon="Coins"
            Title="Aportaciones de Socio"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="EditarAportacionFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Title="Aportaciones" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Listeners>
                                <Activate Handler="ShowButtons();" />
                            </Listeners>
                            <Items>
                                <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false" AnchorHorizontal="100%">
                                    <Items>
                                        <ext:TextField runat="server" ID="EditIdTxt"             DataIndex="SOCIOS_ID"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Socio" AllowBlank="false" ReadOnly="true">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Title="Id de Socio" Html="El Id de Socio es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:TextField   runat="server" ID="EditNombreTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip2" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:NumberField runat="server" ID="EditAportacionSaldoTxt"  DataIndex="APORTACIONES_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Cantidad" AllowBlank="false" ReadOnly="true" MsgTarget="Side">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip3" runat="server" Title="Saldo" Html="El saldo es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:NumberField>
                                        <ext:TextField runat="server"   ID="EditCreatedByTxt"     DataIndex="CREADO_POR"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                        <ext:TextField runat="server"   ID="EditCreationDateTxt"  DataIndex="FECHA_CREACION"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                        <ext:TextField runat="server"   ID="EditModifiedByTxt"    DataIndex="MODIFICADO_POR"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField runat="server"   ID="EditModificationDate" DataIndex="FECHA_MODIFICACION"  LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
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
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true" Hidden="true">
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