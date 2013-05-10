<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportePrestamosPorSocios.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Reportes.ReportePrestamosPorSocios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Prestamos por Socio</title>
    <link rel="Stylesheet" type="text/css" href="../../resources/css/ComboBox_Grid.css" />
 </head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="RM1" runat="server">
        </ext:ResourceManager>

        <asp:ObjectDataSource ID="SociosDS" runat="server"
                TypeName="COCASJOL.LOGIC.Socios.SociosLogic"
                SelectMethod="getSociosActivos" >
        </asp:ObjectDataSource>

        <ext:Store ID="SocioSt" runat="server" DataSourceID="SociosDS">
            <Reader>
                <ext:JsonReader IDProperty="SOCIOS_ID">
                    <Fields>
                        <ext:RecordField Name="SOCIOS_ID" />
                        <ext:RecordField Name="SOCIOS_PRIMER_NOMBRE" />
                        <ext:RecordField Name="SOCIOS_SEGUNDO_NOMBRE" />
                        <ext:RecordField Name="SOCIOS_PRIMER_APELLIDO" />
                        <ext:RecordField Name="SOCIOS_SEGUNDO_APELLIDO" />
                        <ext:RecordField Name="PRODUCCION_UBICACION_FINCA" ServerMapping="socios_produccion.PRODUCCION_UBICACION_FINCA" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <asp:ObjectDataSource ID="TiposPrestamoDS" runat="server"
                TypeName="COCASJOL.LOGIC.Prestamos.TiposPrestamoLogic"
                SelectMethod="getData">
        </asp:ObjectDataSource>

        <ext:Store ID="TiposDePrestamoSt" runat="server" DataSourceID="TiposPrestamoDS" >
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

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="Panel3" runat="server" Padding="5" Layout="FormLayout" AnchorHorizontal="100%" Title="Parámetros, Filtros y Agrupaciones" ButtonAlign="Left">
                    <Items>
                        <ext:FieldSet ID="AddNotaHeaderFS" runat="server" Header="false" Padding="2" Layout="AnchorLayout" Anchor="100%" Border="false">
                            <Items>
                                <ext:Panel ID="AddSocioPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                    <LayoutConfig>
                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                    </LayoutConfig>
                                    <Items>
                                        <ext:Panel ID="Panel4" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                            <Items>
                                                <ext:ComboBox  runat="server" ID="f_SOCIOS_ID" FieldLabel="Código de Socio" LabelAlign="Right" AnchorHorizontal="100%" LabelWidth="150"
                                                    AllowBlank="true"
                                                    TypeAhead="true"
                                                    EmptyText="Seleccione un Socio"
                                                    ForceSelection="true" 
                                                    StoreID="SocioSt"
                                                    Mode="Local" 
                                                    DisplayField="SOCIOS_ID"
                                                    ValueField="SOCIOS_ID"
                                                    MinChars="2"
                                                    ListWidth="450" 
                                                    PageSize="10" 
                                                    ItemSelector="tr.list-item" >
                                                    <Template ID="Template2" runat="server" Width="200">
                                                        <Html>
					                                        <tpl for=".">
						                                        <tpl if="[xindex] == 1">
							                                        <table class="cbStates-list">
								                                        <tr>
								        	                                <th>ID</th>
								        	                                <th>PRIMER NOMBRE</th>
                                                                            <th>SEGUNDO NOMBRE</th>
                                                                            <th>PRIMER APELLIDO</th>
                                                                            <th>SEGUNDO APELLIDO</th>
								                                        </tr>
						                                        </tpl>
						                                        <tr class="list-item">
							                                        <td style="padding:3px 0px;">{SOCIOS_ID}</td>
							                                        <td>{SOCIOS_PRIMER_NOMBRE}</td>
                                                                    <td>{SOCIOS_SEGUNDO_NOMBRE}</td>
                                                                    <td>{SOCIOS_PRIMER_APELLIDO}</td>
                                                                    <td>{SOCIOS_SEGUNDO_APELLIDO}</td>
						                                        </tr>
						                                        <tpl if="[xcount-xindex]==0">
							                                        </table>
						                                        </tpl>
					                                        </tpl>
				                                        </Html>
                                                    </Template>
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>

                                                <ext:ComboBox
                                                    ID="f_PRESTAMOS_ID" FieldLabel="Tipo de Prestamo" LabelAlign="Right" AnchorHorizontal="100%" LabelWidth="150"
                                                    runat="server"
                                                    AllowBlank="true"
                                                    ForceSelection="true"
                                                    StoreID="TiposDePrestamoSt"
                                                    ValueField="PRESTAMOS_ID" 
                                                    DisplayField="PRESTAMOS_NOMBRE"
                                                    Mode="Local"
                                                    TypeAhead="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                
                                                <ext:ComboBox ID="f_SALIDA_FORMATO" runat="server" FieldLabel="Formato de Salida" LabelAlign="Right" AnchorHorizontal="100%" ForceSelection="true" Text="PDF" LabelWidth="150">
                                                    <Items>
                                                        <ext:ListItem Text="PDF" Value="PDF" />
                                                        <ext:ListItem Text="Excel" Value="EXCEL" />
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel1" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" PaddingSummary="0 0 5 5">
                                            <Items>
                                                <ext:Checkbox runat="server" ID="g_SOCIOS_ID" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Socio" MsgTarget="Side" ></ext:Checkbox>
                                                <ext:Checkbox runat="server" ID="g_PRESTAMOS_ID" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Tipo de Prestamo" MsgTarget="Side" ></ext:Checkbox>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button3" runat="server" Icon="ControlPlay" Text="Ejecutar" >
                            <DirectEvents>
                                <Click OnEvent="Report_Execution" IsUpload="true"></Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
