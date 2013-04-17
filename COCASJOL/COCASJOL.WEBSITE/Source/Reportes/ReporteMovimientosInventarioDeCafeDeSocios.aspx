<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteMovimientosInventarioDeCafeDeSocios.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Reportes.ReporteMovimientosInventarioDeCafeDeSocios" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporte Movimientos de Inventario de Café de Socios</title>
    <script type="text/javascript">
        var calendar = {
            setFecha: function () {
                var dateFrom = Ext.getCmp('f_DATE_FROM').getValue();
                if (dateFrom != "")
                    dateFrom = dateFrom.dateFormat('d/M/y');
                else
                    dateFrom = "";

                var dateTo = Ext.getCmp('f_DATE_TO').getValue();
                if (dateTo != "")
                    dateTo = dateTo.dateFormat('d/M/y');
                else
                    dateTo = "";


                var strDate = dateFrom + (dateFrom == "" || dateTo == "" ? "" : " - ") + dateTo;

                Ext.getCmp('f_FECHA').setValue("", strDate);
            },

            clearFecha: function () {
                this.resetDateFields(Ext.getCmp('f_DATE_FROM'), Ext.getCmp('f_DATE_TO'));
                this.setFecha();
            },

            validateDateRange: function (field) {
                var v = this.processValue(this.getRawValue()), field;

                if (this.startDateField) {
                    field = Ext.getCmp(this.startDateField);
                    field.setMaxValue();
                    this.dateRangeMax = null;
                } else if (this.endDateField) {
                    field = Ext.getCmp(this.endDateField);
                    field.setMinValue();
                    this.dateRangeMin = null;
                }

                field.validate();
            },

            resetDateFields: function (field1, field2) {
                field1.dateRangeMin = null;
                field2.dateRangeMax = null;
                field1.setMaxValue();
                field2.setMinValue();
                field1.reset();
                field2.reset();
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="false" >
        </ext:ResourceManager>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:ObjectDataSource ID="ClasificacionesCafeDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.ClasificacionDeCafeLogic"
                SelectMethod="GetClasificacionesDeCafe">
        </asp:ObjectDataSource>

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

        <ext:Store ID="ClasificacionesCafeSt" runat="server" DataSourceID="ClasificacionesCafeDS" >
            <Reader>
                <ext:JsonReader IDProperty="CLASIFICACIONES_CAFE_ID">
                    <Fields>
                        <ext:RecordField Name="CLASIFICACIONES_CAFE_ID" />
                        <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

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
                                                    ID="f_CLASIFICACIONES_CAFE_ID" FieldLabel="Clasificación de Café" LabelAlign="Right" AnchorHorizontal="100%" LabelWidth="150"
                                                    runat="server"
                                                    AllowBlank="true"
                                                    ForceSelection="true"
                                                    StoreID="ClasificacionesCafeSt"
                                                    ValueField="CLASIFICACIONES_CAFE_ID" 
                                                    DisplayField="CLASIFICACIONES_CAFE_NOMBRE"
                                                    Mode="Local"
                                                    TypeAhead="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>

                                                <ext:ComboBox ID="f_DESCRIPCION" runat="server" FieldLabel="Descripción" LabelAlign="Right" AnchorHorizontal="100%" AllowBlank="true" ForceSelection="true" TypeAhead="true" LabelWidth="150" >
                                                    <Items>
                                                        <ext:ListItem Text="Deposito de Café" Value="ENTRADA" />
                                                        <ext:ListItem Text="Hoja de Liquidación" Value="SALIDA" />
                                                    </Items>
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.clearValue();" />
                                                    </Listeners>
                                                </ext:ComboBox>

                                                <ext:DropDownField ID="f_FECHA" runat="server" FieldLabel="Fecha" LabelAlign="Right" AnchorHorizontal="100%" LabelWidth="150"
                                                    Mode="ValueText" TriggerIcon="SimpleArrowDown" CollapseMode="Default">
                                                    <Component>
                                                        <ext:FormPanel ID="FormPanel1" runat="server" Height="100" Width="170" Frame="true"
                                                            LabelWidth="50" ButtonAlign="Left" BodyStyle="padding:2px 2px;">
                                                            <Items>
                                                                <ext:CompositeField ID="CompositeField1" runat="server" FieldLabel="Desde" LabelWidth="50">
                                                                    <Items>
                                                                        <ext:DateField ID="f_DATE_FROM" Vtype="daterange" runat="server" Flex="1" Width="100"
                                                                            CausesValidation="false">
                                                                            <CustomConfig>
                                                                                <ext:ConfigItem Name="endDateField" Value="#{f_DATE_TO}" Mode="Value" />
                                                                            </CustomConfig>
                                                                            <Listeners>
                                                                                <KeyUp Fn="calendar.validateDateRange" />
                                                                            </Listeners>
                                                                        </ext:DateField>
                                                                    </Items>
                                                                </ext:CompositeField>
                                                                <ext:CompositeField ID="CompositeField2" runat="server" FieldLabel="Hasta" LabelWidth="50">
                                                                    <Items>
                                                                        <ext:DateField ID="f_DATE_TO" runat="server" Vtype="daterange" Width="100">
                                                                            <CustomConfig>
                                                                                <ext:ConfigItem Name="startDateField" Value="#{f_DATE_FROM}" Mode="Value" />
                                                                            </CustomConfig>
                                                                            <Listeners>
                                                                                <KeyUp Fn="calendar.validateDateRange" />
                                                                            </Listeners>
                                                                        </ext:DateField>
                                                                    </Items>
                                                                </ext:CompositeField>
                                                            </Items>
                                                            <Buttons>
                                                                <ext:Button ID="Button1" Text="Ok" Icon="Accept" runat="server">
                                                                    <Listeners>
                                                                        <Click Handler="calendar.setFecha();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:Button ID="Button2" Text="Cancelar" Icon="Cancel" runat="server">
                                                                    <Listeners>
                                                                        <Click Handler="calendar.clearFecha();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Buttons>
                                                        </ext:FormPanel>
                                                        </Component>
                                                </ext:DropDownField>
                                                
                                                <ext:TextField ID="f_CREADO_POR" runat="server" FieldLabel="Creado Por" LabelAlign="Right" AnchorHorizontal="100%" LabelWidth="150" ></ext:TextField>
                                                
                                                <ext:DateField ID="f_FECHA_CREACION" runat="server" FieldLabel="Fecha de Creación" LabelAlign="Right" AnchorHorizontal="100%" LabelWidth="150" ></ext:DateField>
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel1" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" PaddingSummary="0 0 5 5">
                                            <Items>
                                                <ext:Checkbox runat="server" ID="g_SOCIOS_ID" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Socio" MsgTarget="Side" ></ext:Checkbox>
                                                <ext:Checkbox runat="server" ID="g_CLASIFICACIONES_CAFE_ID" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Clasificación de Café" MsgTarget="Side" ></ext:Checkbox>
                                                <ext:Checkbox runat="server" ID="g_DESCRIPCION" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Descripción" MsgTarget="Side" ></ext:Checkbox>
                                                <ext:Checkbox runat="server" ID="g_FECHA" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Fecha" MsgTarget="Side" ></ext:Checkbox>
                                                <ext:Checkbox runat="server" ID="g_CREADO_POR" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Creador" MsgTarget="Side" ></ext:Checkbox>
                                                <ext:Checkbox runat="server" ID="g_FECHA_CREACION" LabelWidth="200" LabelAlign="Left" FieldLabel="Agrupar por Fecha de Creación" MsgTarget="Side" ></ext:Checkbox>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:FieldSet>
                        <ext:ComboBox ID="f_SALIDA_FORMATO" runat="server" LabelWidth="250" FieldLabel="Formato de Salida" AnchorHorizontal="100%" ForceSelection="true" Text="PDF">
                            <Items>
                                <ext:ListItem Text="PDF" Value="PDF" />
                                <ext:ListItem Text="Excel" Value="EXCEL" />
                            </Items>
                        </ext:ComboBox>
                        <ext:Checkbox runat="server" ID="p_QUINTALES" LabelWidth="250" LabelAlign="Left" FieldLabel="Mostrar peso en Quintales (QQ)" MsgTarget="Side" Checked="true" ></ext:Checkbox>
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