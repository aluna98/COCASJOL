<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prestamos.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Prestamos.Prestamos" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var Grid = null;
        var EditWindow = null;
        var EditForm = null;
        var AddWindow = null;
        var AddForm = null;
        var ConfirmMsgTitle = "Prestamos"; 
        var ConfirmEdit = "Seguro desea modificar la informacion  del prestamo?";
        var Confirmacion = "Se ha finalizado correctamente";
        var ConfirmDelete = "Esta seguro que desea eliminar el prestamo?";

        var PrestamoX = {
            _index: 0,

            setReferences: function () {
                Grid = PrestamosGridP;
                EditWindow = EditarPrestamosWin;
                EditForm = EditarPrestamoFormP;
                AddWindow = AgregarPrestamosWin;
                AddForm = AgregarPrestamoFormP;
            },

            getRecord: function () {
                var registro = Grid.getStore().getAt(this.getIndex());

                if (registro != null) {
                    return registro;
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

                Ext.Msg.confirm(ConfirmMsgTitle, ConfirmEdit, function (btn, text) {
                    if (btn == 'yes') {
                        EditForm.getForm().updateRecord(EditForm.record);
                    }
                });
            },

            edit: function () {
                if (Grid.getSelectionModel().hasSelection()) {
                    var record = Grid.getSelectionModel().getSelected();
                    var index = Grid.store.indexOf(record);
                    this.setIndex(index);
                    this.open();
                } else {
                    var msg = Ext.Msg;
                    Ext.Msg.alert('Atención', 'Seleccione al menos 1 elemento');
                }
            },

            edit2: function (index) {
                this.setIndex(index);
                this.open();
            },

            add: function () {
                AddWindow.show();
            },

            insert: function () {
                var fields = AddForm.getForm().getFieldValues(false, "");
                Grid.insertRecord(0, fields, false);
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
                    Ext.Msg.alert('Atención', 'Seleccione al menos 1 elemento');
                }
            }
        };
    </script>
    <ext:XScript ID="XScript1" runat="server">
        <script type="text/javascript">
            var applyFilter = function (field) {                
                var store = #{PrestamosGridP}.getStore();
                store.suspendEvents();
                store.filterBy(getRecordFilter());                                
                store.resumeEvents();
                #{PrestamosGridP}.getView().refresh(false);
            };
             
            var clearFilter = function () {
                #{FilterPrestamoId}.reset();
                #{FilterPrestamoNombre}.reset();
                #{FilterPrestamoDescrip}.reset();
                #{FilterPrestamoCantMax}.reset();
                #{FilterPrestamosInteres}.reset();

                #{PrestamosSt}.clearFilter();
            }
 
            var filterString = function (value, dataIndex, record) {
                var val = record.get(dataIndex);
                
                if (typeof val != "string") {
                    return value.length == 0;
                }
                
                return val.toLowerCase().indexOf(value.toLowerCase()) > -1;
            };
 
            var filterDate = function (value, dataIndex, record) {
                var val = record.get(dataIndex).clearTime(true).getTime();
 
                if (!Ext.isEmpty(value, false) && val != value.clearTime(true).getTime()) {
                    return false;
                }
                return true;
            };
 
            var filterNumber = function (value, dataIndex, record) {
                var val = record.get(dataIndex);
 
                if (!Ext.isEmpty(value, false) && val != value) {
                    return false;
                }
                
                return true;
            };
 
            var getRecordFilter = function () {
                var f = [];

                f.push({
                    filter: function (record) {                         
                        return filterString(#{FilterPrestamoId}.getValue(), "PRESTAMOS_ID", record);
                    }
                });
 
                f.push({
                    filter: function (record) {                         
                        return filterString(#{FilterPrestamoNombre}.getValue(), "PRESTAMOS_NOMBRE", record);
                    }
                });
               
                f.push({
                    filter: function (record) {                         
                        return filterString(#{FilterPrestamoDescrip}.getValue(), "PRESTAMOS_DESCRIPCION", record);
                    }
                });
                 
                f.push({
                    filter: function (record) {                      
                        return filterNumber(#{FilterPrestamoCantMax}.getValue(), "PRESTAMOS_CANT_MAXIMA", record);
                    }
                });
                 
                f.push({
                    filter: function (record) {                         
                        return filterNumber(#{FilterPrestamosInteres}.getValue(), "PRESTAMOS_INTERES", record);
                    }
                });
 
                var len = f.length;
                 
                return function (record) {
                    for (var i = 0; i < len; i++) {
                        if (!f[i].filter(record)) {
                            return false;
                        }
                    }
                    return true;
                };
            };
        </script>
    </ext:XScript>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
            <Listeners>
                <DocumentReady Handler="PrestamoX.setReferences()" />
            </Listeners>
        </ext:ResourceManager>
        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>
        <div>
            <ext:Viewport ID="view1" runat="server" Layout="FitLayout">
                <Items>
                    <ext:Panel ID="panel1" runat="server" Frame="false" Header="false" Title="Prestamos" Icon="Money" Layout="FitLayout">
                        <Items>
                            <ext:GridPanel ID="PrestamosGridP" runat="server" Height="300" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                                <Store>
                                    <ext:Store ID="PrestamosSt" runat="server" SkipIdForNewRecords="false" AutoSave="false" WarningOnDirty="false" OnRefreshData="PrestamosSt_Reload">
                                        <Reader>
                                            <ext:JsonReader IDProperty="PRESTAMOS_ID">
                                                <Fields>
                                                    <ext:RecordField Name="PRESTAMOS_ID" Type="Int" />
                                                    <ext:RecordField Name="PRESTAMOS_NOMBRE" />
                                                    <ext:RecordField Name="PRESTAMOS_DESCRIPCION" />
                                                    <ext:RecordField Name="PRESTAMOS_CANT_MAXIMA" Type="Int" />
                                                    <ext:RecordField Name="PRESTAMOS_INTERES" Type="Int" />
                                                </Fields>
                                            </ext:JsonReader>
                                        </Reader>
                                        <DirectEvents>
                                            <Update OnEvent="PrestamosSt_Update" Success="#{PrestamosSt}.reload()">
                                                <EventMask ShowMask="true" Target="CustomTarget" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="PRESTAMOS_ID"          Mode="Raw" Value="#{EditPrestamoIDTxt}.getValue()"/>
                                                    <ext:Parameter Name="PRESTAMOS_NOMBRE"      Mode="Raw" Value="#{EditPrestamoNombreTxt}.getValue()"/>
                                                    <ext:Parameter Name="PRESTAMOS_DESCRIPCION" Mode="Raw" Value="#{EditPrestamoDescripTxt}.getValue()"/>
                                                    <ext:Parameter Name="PRESTAMOS_CANT_MAXIMA" Mode="Raw" Value="#{EditPrestamoCantMaxTxt}.getValue()"/>
                                                    <ext:Parameter Name="PRESTAMOS_INTERES"     Mode="Raw" Value="#{EditPrestamoInteresTxt}.getValue()"/>
                                                </ExtraParams>
                                            </Update>
                                        </DirectEvents>
                                        <DirectEvents>
                                            <Add OnEvent="PrestamosSt_Insert" Success="#{PrestamosSt}.reload()">
                                                <EventMask ShowMask="true" Target="CustomTarget" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="PRESTAMOS_NOMBRE"      Mode="Raw" Value="#{AddNombre}.getValue()"/>
                                                    <ext:Parameter Name="PRESTAMOS_DESCRIPCION" Mode="Raw" Value="#{AddDescrip}.getValue()"/>
                                                    <ext:Parameter Name="PRESTAMOS_CANT_MAXIMA" Mode="Raw" Value="#{AddCantMax}.getValue()"/>
                                                    <ext:Parameter Name="PRESTAMOS_INTERES"     Mode="Raw" Value="#{AddInteres}.getValue()"/>
                                                </ExtraParams>
                                            </Add>
                                        </DirectEvents>
                                        <DirectEvents>
                                            <Remove OnEvent="PrestamosSt_Delete" Success="#{PrestamosSt}.reload()">
                                                <EventMask ShowMask="true" Target="CustomTarget" />
                                            </Remove>
                                        </DirectEvents>
                                    </ext:Store>
                                </Store>
                                <TopBar>
                                    <ext:Toolbar ID="Toolbar1" runat="server">
                                        <Items>
                                            <ext:Button ID="AddBtn" runat="server" Text="Agregar" Icon="Add">
                                                <Listeners>
                                                    <Click Handler="PrestamoX.add()" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button ID="EditBtn" runat="server" Text="Editar" Icon="ApplicationEdit">
                                                <Listeners>
                                                    <Click Handler="PrestamoX.edit()" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button ID="DeleteBtn" runat="server" Text="Eliminar" Icon="Delete">
                                                <Listeners>
                                                    <Click Handler="PrestamoX.remove()" />
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
                                                            <ext:TextField ID="FilterPrestamoId" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterPrestamoNombre" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component> 
                                                            <ext:TextField ID="FilterPrestamoDescrip" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterPrestamoCantMax" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterPrestamosInteres" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:Button ID="ClearFilterButton" runat="server" Icon="Cancel">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip1" runat="server" Html="Clear filter" />
                                                            </ToolTips>
                                                            <Listeners>
                                                                <Click Handler="clearFilter();" />
                                                            </Listeners>                                            
                                                        </ext:Button>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                </Columns>
                                            </ext:HeaderRow>
                                        </HeaderRows>
                                    </ext:GridView>
                                </View>
                                <ColumnModel ID="ColumnModel1" runat="server">
                                    <Columns>
                                        <ext:Column ColumnID="PrestamosId" Header="ID" DataIndex="PRESTAMOS_ID" />
                                        <ext:Column ColumnID="PrestamosNombre" Header="Nombre" DataIndex="PRESTAMOS_NOMBRE" Fixed="true" />
                                        <ext:Column ColumnID="PrestamosDescrip" Header="Descripcion" DataIndex="PRESTAMOS_DESCRIPCION" />
                                        <ext:Column ColumnID="PrestamosCantMax" Header="% Cant Maxima" DataIndex="PRESTAMOS_CANT_MAXIMA" />
                                        <ext:Column ColumnID="PrestamosInteres" Header="% Interes" DataIndex="PRESTAMOS_INTERES" />
                                         <ext:Column Width="28" DataIndex="PRESTAMOS_ID" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                        <Renderer Handler="return '';" />
                                    </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel ID="rowselectionmodel" runat="server" SingleSelect="true">
                                        <%--<Listeners>
                                            <RowSelect Handler="#{PrestamosSt}.reload();" />
                                        </Listeners>--%>
                                    </ext:RowSelectionModel>
                                </SelectionModel>
                                <BottomBar>
                                    <ext:PagingToolbar ID="pagingtoolbar" StoreID="PrestamosSt" runat="server" PageSize="5" DisplayInfo="true" DisplayMsg="Prestamos {0} - {1} de {2}" EmptyMsg="No hay prestamos para mostrar" />
                                </BottomBar>
                                <LoadMask ShowMask="true" />
                                <Listeners>
                                    <RowDblClick Handler="PrestamoX.edit()" />
                                </Listeners>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Viewport>
        </div>
        <div>
            <ext:Window ID="EditarPrestamosWin"
                runat="server"
                Hidden="true"
                icon="MoneyDollar"
                Title="Editar Prestamos"
                Width="550"
                Layout="FormLayout"
                AutoHeight="true" 
                Resizable="false" 
                Shadow="None"
                Modal="true" 
                X="10" 
                Y="30">
                    <Listeners>
                        <Hide Handler="EditarPrestamoFormP.getForm().reset()" />
                    </Listeners>
                    <Items>
                        <ext:FormPanel ID="EditarPrestamoFormP" 
                                    runat="server" 
                                    Header="false"
                                    ButtonAlign="Right"
                                    MonitorValid="true"
                                    LabelAlign="Right">
                            <Items>
                                <ext:Panel ID="PanelInformacion" runat="server" LabelWidth="150"
                                    Title="Informacion Prestamo" Layout="AnchorLayout" AutoHeight="true"
                                     Icon="Coins" Border="false" Header="false">
                                        <Items>
                                            <ext:Panel ID="Panel" runat="server" frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                                <Items>
                                                    <ext:NumberField runat="server" ID="EditPrestamoIDTxt"      DataIndex="PRESTAMOS_ID" FieldLabel="Id" ReadOnly="true" />
                                                    <ext:TextField runat="server"   ID="EditPrestamoNombreTxt"  DataIndex="PRESTAMOS_NOMBRE" FieldLabel="Nombre"  AnchorHorizontal="90%" AllowBlank="false" MaxLength="45"/>
                                                    <ext:TextField runat="server"   ID="EditPrestamoDescripTxt" DataIndex="PRESTAMOS_DESCRIPCION" FieldLabel="Descripcion" AnchorHorizontal="90%" AllowBlank="true" MaxLength="100" />
                                                    <ext:NumberField runat="server" ID="EditPrestamoCantMaxTxt" DataIndex="PRESTAMOS_CANT_MAXIMA" FieldLabel="% Cantidad Maxima" AnchorHorizontal="90%" AllowBlank="false" MinValue="0" MaxValue="100" AllowDecimals="false" />
                                                    <ext:NumberField runat="server" ID="EditPrestamoInteresTxt" DataIndex="PRESTAMOS_INTERES" FieldLabel="% Interes" AnchorHorizontal="90%" AllowBlank="false" MinValue="0" MaxValue="100" AllowDecimals="false" />
                                                </Items>
                                            </ext:Panel>
                                        </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="EditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                                    <Listeners>
                                        <Click Handler="PrestamoX.previous()" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="EditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                                    <Listeners>
                                        <Click Handler="PrestamoX.next()" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="EditSaveBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                                    <Listeners>
                                        <Click Handler="PrestamoX.update()" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
                       </ext:FormPanel>
                    </Items>
                </ext:Window>
        </div>
        <div>
            <ext:Window ID="AgregarPrestamosWin"
                runat="server"
                Hidden="true"
                icon="MoneyDollar"
                Title="Agregar Prestamos"
                Width="550"
                Layout="FormLayout"
                AutoHeight="true" 
                Resizable="false" 
                Shadow="None"
                Modal="true" 
                X="10" 
                Y="30">
                    <Listeners>
                        <Hide Handler="AgregarPrestamoFormP.getForm().reset()" />
                    </Listeners>
                    <Items>
                        <ext:FormPanel ID="AgregarPrestamoFormP" 
                                    runat="server" 
                                    Header="false"
                                    ButtonAlign="Right"
                                    MonitorValid="true"
                                    LabelAlign="Right">
                            <Items>
                                <ext:Panel ID="Panel2" runat="server" LabelWidth="150" 
                                    Title="Informacion Prestamo" Layout="AnchorLayout" AutoHeight="true"
                                     Icon="Coins" Border="false" Header="false">
                                        <Items>
                                            <ext:Panel ID="Panel3" runat="server" frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                                <Items>
                                                    <ext:TextField runat="server"   ID="AddNombre"  DataIndex="PRESTAMOS_NOMBRE" FieldLabel="Nombre"  AnchorHorizontal="90%" AllowBlank="false" MaxLength="45"/>
                                                    <ext:TextField runat="server"   ID="AddDescrip" DataIndex="PRESTAMOS_DESCRIPCION" FieldLabel="Descripcion" AnchorHorizontal="90%" AllowBlank="true" MaxLength="100" />
                                                    <ext:NumberField runat="server" ID="AddCantMax" DataIndex="PRESTAMOS_CANT_MAXIMA" FieldLabel="% Cantidad Maxima" AnchorHorizontal="90%" AllowBlank="false" MinValue="0" MaxValue="100" AllowDecimals="false" />
                                                    <ext:NumberField runat="server" ID="AddInteres" DataIndex="PRESTAMOS_INTERES" FieldLabel="% Interes" AnchorHorizontal="90%" AllowBlank="false" MinValue="0" MaxValue="100" AllowDecimals="false" />
                                                </Items>
                                            </ext:Panel>
                                        </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="Button3" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                                    <Listeners>
                                        <Click Handler="PrestamoX.insert()" />
                                    </Listeners>
                                </ext:Button>
                            </Buttons>
                       </ext:FormPanel>
                    </Items>
                    <LoadMask ShowMask="true" />
                </ext:Window>
        </div>
    </form>
</body>
</html>
