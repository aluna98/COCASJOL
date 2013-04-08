<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportePrestamosPorSocio.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Reportes.ReportePrestamosPorSocio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Prestamos por Socio</title>
    <link rel="Stylesheet" type="text/css" href="../../resources/css/ComboBox_Grid.css" />
    <script type="text/javascript" src="../../resources/js/prestamos/ReportePrestamos.js" ></script>
 </head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="RM1" runat="server">
        </ext:ResourceManager>
        <ext:Store ID="ComboBoxSt" runat="server" OnRefreshData="SociosSt_Reload">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="SOCIOS_ID" />
                        <ext:RecordField Name="SOCIOS_PRIMER_NOMBRE" />
                        <ext:RecordField Name="SOCIOS_PRIMER_APELLIDO" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="10" Mode="Raw" />
            </AutoLoadParams>
        </ext:Store>
        <asp:ObjectDataSource ID="TiposPrestamoDS" runat="server"
                TypeName="COCASJOL.LOGIC.Prestamos.PrestamosLogic"
                SelectMethod="getData">
        </asp:ObjectDataSource>
        <ext:Store ID="TiposDePrestamoSt" runat="server" DataSourceID="TiposPrestamoDS" SkipIdForNewRecords="false">
            <Reader>
                <ext:JsonReader IDProperty="PRESTAMOS_ID">
                    <Fields>
                        <ext:RecordField Name="PRESTAMOS_ID" />
                        <ext:RecordField Name="PRESTAMOS_NOMBRE" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <ext:Hidden ID="LoggedUserHdn" runat="server"/>
        <Items>
                <ext:FormPanel ID="AgregarNotasFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelAlign="Right" LabelWidth="130" Layout="ContainerLayout" AutoScroll="true" >
                    <Listeners>
                        <Show Handler="this.getForm().reset();" />
                    </Listeners>
                    <Items>
                        <ext:Panel ID="Panel2" runat="server" Title="Reporte Prestamos Pendientes" Header="true" Layout="AnchorLayout" AutoHeight="True" Resizable="false" AnchorHorizontal="100%">
                            <Items>
                                <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" AnchorHorizontal="100%" Border="false">
                                    <Items>
                                        <ext:FieldSet ID="AddNotaHeaderFS" runat="server" Header="false" Padding="5">
                                            <Items>
                                                <ext:Panel ID="AddSocioPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                                    <LayoutConfig>
                                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                                    </LayoutConfig>
                                                    <Items>
                                                        <ext:Panel ID="Panel4" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth="1">
                                                            <Items>
                                                                <ext:ComboBox  runat="server" ID="AddSociosIdTxt"  LabelAlign="Right" AnchorHorizontal="50%" FieldLabel="Codigo Socio" MsgTarget="Side"
                                                                    TypeAhead="true"
                                                                    EmptyText="Seleccione un Socio"
                                                                    StoreID="ComboBoxSt"
                                                                    Mode="Local" 
                                                                    DisplayField="SOCIOS_ID"
                                                                    ValueField="SOCIOS_ID"
                                                                    MinChars="2" 
                                                                    ListWidth="450" 
                                                                    PageSize="10" 
                                                                    ItemSelector="tr.list-item" >
                                                                    <Template ID="Template1" runat="server" Width="200">
                                                                        <Html>
					                                                        <tpl for=".">
						                                                        <tpl if="[xindex] == 1">
							                                                        <table class="cbStates-list">
								                                                        <tr>
								                	                                        <th>ID</th>
								                	                                        <th>PRIMER NOMBRE</th>
                                                                                            <th>PRIMER APELLIDO</th>
								                                                        </tr>
						                                                        </tpl>
						                                                        <tr class="list-item">
							                                                        <td style="padding:3px 0px;">{SOCIOS_ID}</td>
							                                                        <td>{SOCIOS_PRIMER_NOMBRE}</td>
                                                                                    <td>{SOCIOS_PRIMER_APELLIDO}</td>
						                                                        </tr>
						                                                        <tpl if="[xcount-xindex]==0">
							                                                        </table>
						                                                        </tpl>
					                                                        </tpl>
				                                                        </Html>
                                                                    </Template>
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide();}" />
                                                                        <Select Handler="this.triggers[0].show(); PageX.getNombreDeSocio(Ext.getCmp('AddSociosIdTxt'), Ext.getCmp('AddNombreTxt'));" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <ext:TextField   runat="server" ID="AddNombreTxt" LabelAlign="Right" AnchorHorizontal="50%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:TextField>
                                                                <ext:ComboBox runat="server"    ID="EditCmbPrestamo"     DataIndex="PRESTAMOS_ID"          LabelAlign="Right" AnchorHorizontal="50%" FieldLabel="Tipo de Prestamo" AllowBlank="false" MsgTarget="Side"
                                                                    StoreID="TiposDePrestamoSt"
                                                                    ValueField="PRESTAMOS_ID" 
                                                                    DisplayField="PRESTAMOS_NOMBRE" 
                                                                    Mode="Local"
                                                                    TypeAhead="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <TriggerClick Handler="this.clearValue();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel ID="Panel7" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                                    <LayoutConfig>
                                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                                    </LayoutConfig>
                                                    <Items>
                                                        <ext:Panel ID="Panel1" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:Button ID="EjecturaReporte" runat="server" Icon="ControlPlay" Text="Ejecutar" >
                                                                    <DirectEvents>
                                                                        <Click OnEvent="Reporte_Refresh"></Click>
                                                                    </DirectEvents>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Panel>
                                                   </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:Panel ID="PanelPrestmamosPnl" runat ="server" Border="false" AnchorHorizontal="100%" >
                                            <Items>
                                                <ext:Panel ID="Panel6" runat="server" Layout="AnchorLayout" Border="false" AnchorHorizontal="95%">
                                                    <Items>
                                                        <ext:FieldSet ID="PanelPrestamosFS" runat="server" Title="Resultados" Padding="5">
                                                            <Items>
                                                                <ext:GridPanel ID="PrestamosGrid" runat="server" Height="300" Header="false" Border="false" StripeRows="true" TrackMouseOver="true" Width="750">
                                                                    <Store>
                                                                        <ext:Store ID="PrestamosSt" runat="server" SkipIdForNewRecords="false" AutoSave="false" WarningOnDirty="false" OnRefreshData="SolicitudesSt_Reload">
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
                                                                                        <ext:RecordField Name="PRESTAMOS_NOMBRE" ServerMapping="prestamos.PRESTAMOS_NOMBRE" />
                                                                                        <ext:RecordField Name="SOLICITUD_ESTADO" />
                                                                                    </Fields>
                                                                                </ext:JsonReader>
                                                                            </Reader>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <ColumnModel ID="Model1" runat="server">
                                                                        <Columns>
                                                                            <ext:Column ColumnID="SociosId" Header="Socio ID" DataIndex="SOCIOS_ID" />
                                                                            <ext:Column ColumnID="SolicitudId" Header="Solicitud ID" DataIndex="SOLICITUDES_ID" />
                                                                            <ext:Column ColumnID="SociosNombre" Header="Nombre Socio" DataIndex="SOCIOS_PRIMER_NOMBRE" />
                                                                            <ext:Column ColumnID="SolicitudMonto" Header="Monto" DataIndex="SOLICITUDES_MONTO" />
                                                                            <ext:Column ColumnID="SolicitudEstado" Header="Estado" DataIndex="SOLICITUD_ESTADO" />
                                                                            <ext:Column ColumnID="SolicitudPrestamo" Header="Tipo Prestamo" DataIndex="PRESTAMOS_NOMBRE" />
                                                                            <ext:DateColumn ColumnID="SolicitudPlazo" Header="Plazo" DataIndex="SOLICITUDES_PLAZO" />
                                                                        </Columns>
                                                                    </ColumnModel>
                                                                    <SelectionModel>
                                                                        <ext:RowSelectionModel ID="RowSelectionModel" runat="server" SingleSelect="true" />
                                                                    </SelectionModel>
                                                                    <BottomBar>
                                                                        <ext:PagingToolbar ID="Paginacion1" StoreID="PrestamosSt" runat="server" PageSize="10" DisplayMsg="Prestamos {0} - {1} de {2}" EmptyMsg="No hay prestamos para mostrar" />
                                                                    </BottomBar>
                                                                    <LoadMask ShowMask="true" />
                                                                </ext:GridPanel>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="PanelTotales" runat ="server" Border="false" AnchorHorizontal="100%" >
                                            <Items>
                                                <ext:Panel ID="Panel5" runat="server" Layout="AnchorLayout" Border="false" AnchorHorizontal="95%">
                                                    <Items>
                                                        <ext:FieldSet ID="FieldSet1" runat="server" Title="Totales" Padding="5">
                                                            <Items>
                                                                <ext:TextField ID="MontoTotal" runat="server" ReadOnly="true" FieldLabel="Monto Total" AnchorHorizontal="50%" LabelAlign="Right">
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="El monto total es de solo lectura" Title="Monto" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
    </div>
    </form>
</body>
</html>
