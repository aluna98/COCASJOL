<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudPrestamo.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Prestamos.SolicitudPrestamo" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
         var Grid = null;
         var EditWindow = null;
         var EditForm = null;
         var AddWindow = null;
         var AddForm = null;
         var ConfirmMsgTitle = "Socio";
         var ConfirmUpdate = "Seguro que desea editar la Solicitud?";
         var ConfirmDelete = "Seguro desea rechazar la Solicitud?";
         var ConfirmDelete = "Seguro desea aprobar la Solicitud?";
         var Confirmacion = "Se ha finalizado correctamente";

         var SolicitudX = {
             _index: 0,

             setReferences: function () {
                 Grid = SolicitudesGriP;
                 EditWindow = EditarSolicitudWin;
                 EditForm = EditarSolicitudFormP;
                 AddWindow = NuevaSolicitudWin;
                 AddForm = NuevaSolicitudFormP;
             },

             add: function () {
                 AddWindow.show();
             },

             open: function () {
                 //rec = this.getRecord();
                 //if (rec != null) {
                     EditWindow.show();
                     //EditForm.getForm().loadRecord(rec);
                     //EditForm.record = rec;
                 //}
             }
         };
     </script>
    <ext:XScript ID="XScript1" runat="server">
        <script type="text/javascript">
            var applyFilter = function (field) {                
                var store = #{SolicitudesGriP}.getStore();
                store.suspendEvents();
                store.filterBy(getRecordFilter());                                
                store.resumeEvents();
                #{SolicitudesGriP}.getView().refresh(false);
            };
             
            var clearFilter = function () {
                #{FilterSolicitudSocioId}.reset();
                #{FilterSolicitudId}.reset();
                #{FilterSolicitudNombre}.reset();
                #{FilterSolicitudMonto}.reset();

                #{SolicitudesSt}.clearFilter();
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
                        return filterString(#{FilterSolicitudSocioId}.getValue(), "SOCIOS_ID", record);
                    }
                });

                f.push({
                    filter: function (record) {                      
                        return filterNumber(#{FilterSolicitudId}.getValue(), "SOLICITUDES_ID", record);
                    }
                });
 
                f.push({
                    filter: function (record) {                         
                        return filterString(#{FilterSolicitudNombre}.getValue(), "SOCIOS_PRIMER_NOMBRE", record);
                    }
                });
                 
                f.push({
                    filter: function (record) {                      
                        return filterNumber(#{FilterSolicitudMonto}.getValue(), "SOLICITUDES_MONTO", record);
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
        <ext:ResourceManager ID="RM1" runat="server">
            <Listeners>
                <DocumentReady Handler="SolicitudX.setReferences()" />
            </Listeners>
        </ext:ResourceManager>
        <ext:Hidden ID="LoggedUserHdn" runat="server"/>
        <ext:Viewport ID="view1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="panel1" runat="server" Frame="false" Header="false" Title="Solicitudes" Icon="Money" Layout="FitLayout">
                    <Items>
                        <ext:GridPanel ID="SolicitudesGriP" runat="server" Height="300" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="SolicitudesSt" runat="server" SkipIdForNewRecords="false" AutoSave="false" WarningOnDirty="false" OnRefreshData="SolicitudesSt_Reload">
                                    <Reader>
                                        <ext:JsonReader IDProperty="SOLICITUDES_ID">
                                            <Fields>
                                                <ext:RecordField Name="SOCIOS_ID"/>
                                                <ext:RecordField Name="SOCIOS_PRIMER_NOMBRE" ServerMapping="socios.SOCIOS_PRIMER_NOMBRE" />
                                                <ext:RecordField Name="SOLICITUDES_ID" />
                                                <ext:RecordField Name="SOLICITUDES_MONTO" />
                                                <ext:RecordField Name="SOLICITUDES_INTERES" />
                                                <ext:RecordField Name="SOLICITUDES_PLAZO" Type="Date" />
                                                <ext:RecordField Name="SOLICITUDES_PAGO" />
                                                <ext:RecordField Name="SOLICITUDES_DESTINO" />
                                                <ext:RecordField Name="PRESTAMOS_ID" />
                                                <ext:RecordField Name="SOLICITUDES_CARGO" />
                                                <ext:RecordField Name="SOLICITUDES_PROMEDIO3" />
                                                <ext:RecordField Name="SOLICITUDES_PRODUCCIONACT" />
                                                <ext:RecordField Name="SOLICITUDES_NORTE" />
                                                <ext:RecordField Name="SOLICITUDES_SUR" />
                                                <ext:RecordField Name="SOLICITUDES_ESTE" />
                                                <ext:RecordField Name="SOLICITUDES_OESTE" />
                                                <ext:RecordField Name="SOLICITUDES_VEHICULO" />
                                                <ext:RecordField Name="SOLICITUDES_AGUA" />
                                                <ext:RecordField Name="SOLICITUDES_ENEE" />
                                                <ext:RecordField Name="SOLICITUDES_CASA" />
                                                <ext:RecordField Name="SOLICITUDES_BENEFICIO" />
                                                <ext:RecordField Name="SOLICITUD_OTROSCULTIVOS" />
                                                <ext:RecordField Name="SOLICITUD_CALIFICACION" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <TopBar>
                                <ext:Toolbar ID="ToolBar1" runat="server">
                                    <Items>
                                        <ext:Button ID="AddBtn" runat="server" Text="Agregar" Icon="Add">
                                            <Listeners>
                                                <Click Handler="SolicitudX.add()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EditBtn" runat="server" Text="Editar" Icon="ApplicationEdit">
                                            <Listeners>
                                                <Click Handler="SolicitudX.open()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="DeleteBtn" runat="server"  Text="Denegar" Icon="MoneyDelete">
                                            <Listeners>
                                            
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="ApproveBtn" runat="server" Text="Aprobar" Icon="MoneyAdd">
                                            <Listeners>
                                            
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
                                                            <ext:TextField ID="FilterSolicitudSocioId" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterSolicitudId" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterSolicitudNombre" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterSolicitudMonto" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
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
                            <ColumnModel ID="Model1" runat="server">
                                <Columns>
                                    <ext:Column ColumnID="SociosId" Header="Socio ID" DataIndex="SOCIOS_ID" />
                                    <ext:Column ColumnID="SolicitudId" Header="Solicitud ID" DataIndex="SOLICITUDES_ID" />
                                    <ext:Column ColumnID="SociosNombre" Header="Nombre Socio" DataIndex="SOCIOS_PRIMER_NOMBRE" />
                                    <ext:Column ColumnID="SolicitudMonto" Header="Monto" DataIndex="SOLICITUDES_MONTO" />
                                    <ext:Column ColumnID="SolicitudPlazo" Header="Plazo" DataIndex="SOLICITUDES_PLAZO" />
                                    <ext:Column Width="28" DataIndex="SOCIOS_ID" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                        <Renderer Handler="return '';" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="Paginacion1" StoreID="SolicitudesSt" runat="server" PageSize="10" DisplayMsg="Solicitudes {0} - {1} de {2}" EmptyMsg="Recargue para mostrar/ No hay prestamos para mostrar" />
                            </BottomBar>
                            <LoadMask ShowMask="true" />
                            <Listeners>
                                <RowDblClick />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>

        <ext:Window ID="EditarSolicitudWin"
                runat="server"
                hidden="true"
                Icon="EmoticonWaii"
                width="550"
                layout="FormLayout"
                autoheight="true"
                resizable="false"
                shadow="None"
                modal="true"
                x="10" Y="30">
                <Listeners>
                    <Hide Handler="EditarSolicitudFormP.getForm().reset()" />
                </Listeners>
                <Items>
                    <ext:FormPanel ID="EditarSolicitudFormP" runat="server" Header="false" ButtonAlign="Right" MonitorValid="true" LabelAlign="Right">
                        <Items>
                             <ext:TabPanel ID="tabpanel1" runat="server">
                                <Items>
                                    <ext:Panel ID="PanelPer" runat="server" Title="Datos Personales" Layout="AnchorLayout" AutoHeight="true" Icon="User" LabelWidth="150" >
                                        <Items>
                                            <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="PanelRefer" runat="server" Title="Referencias" Layout="AnchorLayout" AutoHeight="true" Icon="GroupAdd">
                                        <Items>
                                            <ext:Panel ID="Panel4" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="PanelGrl" runat="server" Title="Datos Generales" Layout="AnchorLayout" AutoHeight="true" Icon="UserComment">
                                        <Items>
                                            <ext:Panel ID="Panel5" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="PanelAvales" runat="server" Title="Avales" Layout="AnchorLayout" AutoHeight="true" Icon="GroupKey">
                                        <Items>
                                            <ext:Panel ID="Panel6" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:TabPanel>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="EditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                        <Listeners>
                        
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="EditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                        <Listeners>
                        
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="EditSaveBtn" runat="server" Text="Guardar" Icon="Disk">
                        <Listeners>
                        
                        </Listeners>
                    </ext:Button>
                </Buttons>
         </ext:Window>

         <ext:Window ID="NuevaSolicitudWin" 
                    runat="server" 
                    Hidden="true" 
                    Icon="ApplicationAdd" 
                    Title="Agregar Nueva Solicitud" 
                    Width="550" 
                    Layout="FormLayout" 
                    AutoHeight="true" 
                    Resizable="false" 
                    Shadow="None" 
                    Modal="true" 
                    X="10" Y="30">
            <Listeners>
                <Hide Handler="NuevaSolicitudFormP.getForm().reset()" />
            </Listeners>         
            <Items>
                <ext:FormPanel ID="NuevaSolicitudFormP" runat="server" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:TabPanel ID="TabSolicitudAdd" runat="server" LabelAlign="Right">
                            <Items>
                                <ext:Panel ID="EditPanelDatos" runat="server" Title="Datos Solicitud" Layout="AnchorLayout" AutoHeight="true" Icon="PageEdit" LabelWidth="150">
                                    <Items>
                                        <ext:Panel ID="Panel2" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="EditPanelGrl" runat="server" Title="Datos Generales" Layout="AnchorLayout" AutoHeight="true" Icon="UserEdit" LabelWidth="150">
                                    <Items>
                                        <ext:Panel ID="P1" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="EditPanelRef" runat="server" Title="Referencias" Layout="AnchorLayout" AutoHeight="true" Icon="GroupAdd">
                                    <Items>
                                        <ext:Panel ID="Panel8" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="EditPanelAval" runat="server" Title="Avales" Layout="AnchorLayout" AutoHeight="true" Icon="GroupKey">
                                    <Items>
                                        <ext:Panel ID="Panel9" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                        
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddGuardarBtn" runat="server" Text="Crear" Icon="Add" formBind="true">
                            <Listeners>
                            
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>
    </form>
</body>
</html>
