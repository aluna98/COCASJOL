<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovimientosDeInventarioDeCafe.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Reportes.MovimientosDeInventarioDeCafe" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Movimientos de Inventario de Café</title>
    <script type="text/javascript" src="../../resources/js/reportes/movimientosDeInventarioDeCafe.js" ></script>

    <style type="text/css">
        .x-grid3-cell-inner {
            font-family:"segoe ui",tahoma, arial, sans-serif;
        }
        
        .x-grid-group-hd div {
            font-family:"segoe ui",tahoma, arial, sans-serif;
        }
        
        .x-grid3-hd-inner {
            font-family:"segoe ui",tahoma, arial, sans-serif;
            font-size:12px;
        }
        
        .x-grid3-body .x-grid3-td-Cost {
            background-color:#f1f2f4;
        }
        
        .x-grid3-summary-row .x-grid3-td-Cost {
            background-color:#e1e2e4;
        }     
    </style>

    <script type="text/javascript">
        // this "setGroupStyle" function is called when the GroupingView is refreshed.     
        var setGroupStyle = function (view) {
            // get an instance of the Groups
            var groups = view.getGroups();

            for (var i = 0; i < groups.length; i++) {
                var spans = Ext.query("span", groups[i]);

                if (spans && spans.length > 0) {
                    // Loop through the Groups, the do a query to find the <span> with our ColorCode
                    // Get the "id" from the <span> and split on the "-", the second array item should be our ColorCode
                    var color = "#" + spans[0].id.split("-")[1];

                    // Set the "background-color" of the original Group node.
                    Ext.get(groups[i]).setStyle("background-color", color);
                }
            }
        };
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

        <ext:KeyNav ID="KeyNav1" runat="server" Target="={document.body}" >
            <Home Handler="PageX.navHome();" />
        </ext:KeyNav>

        <asp:ObjectDataSource ID="MovimientoDeInventarioCafeDS" runat="server"
                TypeName="COCASJOL.LOGIC.Reportes.MovimientosDeInventarioDeCafeLogic"
                SelectMethod="GetMovimientosDeInventarioDeCafeLogic" onselecting="MovimientoDeInventarioCafeDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="SOCIOS_ID"                    Type="String"   ControlID="f_SOCIOS_ID"                    PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="TRANSACCION_NUMERO"           Type="Int32"    ControlID="f_TRANSACCION_NUMERO"           PropertyName="Text" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_NOMBRE"  Type="String"   ControlID="f_CLASIFICACIONES_CAFE_NOMBRE"  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA"                        Type="DateTime" ControlID="nullHdn"                        PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_DESDE"                  Type="DateTime" ControlID="f_DATE_FROM"                    PropertyName="Text" />
                    <asp:ControlParameter Name="FECHA_HASTA"                  Type="DateTime" ControlID="f_DATE_TO"                      PropertyName="Text" />
                    <asp:ControlParameter Name="DESCRIPCION"                  Type="String"   ControlID="f_DESCRIPCION"                  PropertyName="Text" DefaultValue="" />
                                                                                                                                         
                    <asp:ControlParameter Name="ENTRADAS_CANTIDAD"            Type="Decimal"  ControlID="f_ENTRADAS_CANTIDAD"            PropertyName="Text" DefaultValue="-1"/>
                    <asp:ControlParameter Name="SALIDAS_CANTIDAD"             Type="Decimal"  ControlID="f_SALIDAS_CANTIDAD"             PropertyName="Text" DefaultValue="-1"/>
                    <asp:ControlParameter Name="SALIDAS_COSTO"                Type="Decimal"  ControlID="f_SALIDAS_COSTO"                PropertyName="Text" DefaultValue="-1"/>
                    <asp:ControlParameter Name="SALIDAS_MONTO"                Type="Decimal"  ControlID="f_SALIDAS_MONTO"                PropertyName="Text" DefaultValue="-1"/>


                    <asp:ControlParameter Name="INVENTARIO_ENTRADAS_CANTIDAD" Type="Decimal"  ControlID="f_INVENTARIO_ENTRADAS_CANTIDAD" PropertyName="Text" DefaultValue="-1"/>
                    <asp:ControlParameter Name="INVENTARIO_SALIDAS_SALDO"     Type="Decimal"  ControlID="f_INVENTARIO_SALIDAS_SALDO"     PropertyName="Text" DefaultValue="-1"/>

                    <asp:ControlParameter Name="CREADO_POR"                   Type="String"   ControlID="f_CREADO_POR"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"               Type="DateTime" ControlID="f_FECHA_CREACION"               PropertyName="Text" DefaultValue="" />
                </SelectParameters>
        </asp:ObjectDataSource>

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
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Inventario de Café" Icon="Basket" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="MovimientoInventarioCafeGridP" runat="server" AutoExpandColumn="FECHA" Height="300"
                            Title="Inventario de Café" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="MovimientoInventarioCafeSt" runat="server" DataSourceID="MovimientoDeInventarioCafeDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="SOCIOS_ID"                    />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE"  />
                                                <ext:RecordField Name="DESCRIPCION"                  />
                                                <ext:RecordField Name="FECHA"                        Type="Date" />
                                                <ext:RecordField Name="FECHA_DESDE"                  Type="Date" />
                                                <ext:RecordField Name="FECHA_HASTA"                  Type="Date" />
                                                <ext:RecordField Name="TRANSACCION_NUMERO"           />
                                                <ext:RecordField Name="ENTRADAS_CANTIDAD"            />
                                                <ext:RecordField Name="SALIDAS_CANTIDAD"             />
                                                <ext:RecordField Name="SALIDAS_COSTO"                />
                                                <ext:RecordField Name="SALIDAS_MONTO"                />
                                                <ext:RecordField Name="INVENTARIO_ENTRADAS_CANTIDAD" />
                                                <ext:RecordField Name="INVENTARIO_SALIDAS_SALDO"     />
                                                <ext:RecordField Name="CREADO_POR"                   />
                                                <ext:RecordField Name="FECHA_CREACION"               Type="Date" />
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
                                    <ext:GroupingSummaryColumn
                                        ColumnID="SociosId"
                                        Header="Id de Socio"
                                        Sortable="true"
                                        DataIndex="SOCIOS_ID"
                                        Hideable="false"
                                        SummaryType="None">
                                        <SummaryRenderer Handler="return '-';" />    
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="ClasificacionCafe"
                                        Header="Clasificación de Café"
                                        Sortable="true"
                                        DataIndex="CLASIFICACIONES_CAFE_NOMBRE"
                                        Hideable="false"
                                        SummaryType="None">
                                        <SummaryRenderer Handler="return '-';" />    
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="Descripcion"
                                        Header="Descripción"
                                        Sortable="true"
                                        DataIndex="DESCRIPCION"
                                        Hideable="false"
                                        SummaryType="None">
                                        <SummaryRenderer Handler="return '-';" />    
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="Fecha"
                                        Header="Fecha"
                                        Sortable="true"
                                        DataIndex="FECHA"
                                        SummaryType="Max">
                                        <Renderer Format="Date" FormatArgs="'m/d/Y'" />
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="Transaccion"
                                        Header="Transacción #"
                                        Sortable="true"
                                        Groupable="false"
                                        DataIndex="TRANSACCION_NUMERO"
                                        Hideable="false"
                                        SummaryType="Count">
                                        <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' Transacciones)' : '(0 Transacción)');" />    
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="CantidadEntrada"
                                        Header="Cantidad de Entrada"
                                        Sortable="false"
                                        Groupable="false"
                                        DataIndex="ENTRADAS_CANTIDAD"
                                        SummaryType="Sum">
                                        <SummaryRenderer Handler="return '(' + Ext.util.Format.number(value,'0.0000') + ' Total' + ')';" />
                                        <Renderer Format="Number" FormatArgs="'0.0000'" />
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="CantidadSalida"
                                        Header="Cantidad de Salida"
                                        Sortable="false"
                                        Groupable="false"
                                        DataIndex="SALIDAS_CANTIDAD"
                                        SummaryType="Sum">
                                        <SummaryRenderer Handler="return '(' + Ext.util.Format.number(value,'0.0000') + ' Total' + ')';" />
                                        <Renderer Format="Number" FormatArgs="'0.0000'" />
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="CostoSalida"
                                        Header="Costo de Salida"
                                        Sortable="false"
                                        Groupable="false"
                                        DataIndex="SALIDAS_COSTO"
                                        SummaryType="Average">
                                        <SummaryRenderer Handler="return '(' + Ext.util.Format.number(value,'0,000.0000') + ' Promedio' + ')';" />
                                        <Renderer Format="Number" FormatArgs="'0,000.0000'" />
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="MontoSalida"
                                        Header="Monto de Salida"
                                        Sortable="false"
                                        Groupable="false"
                                        DataIndex="SALIDAS_MONTO"
                                        SummaryType="Sum">
                                        <SummaryRenderer Handler="return '(' + Ext.util.Format.number(value,'0,000.0000') + ' Total' + ')';" />
                                        <Renderer Format="Number" FormatArgs="'0,000.0000'" />
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="CantidadTotalInventario"
                                        Header="Cantidad en Inventario"
                                        Sortable="false"
                                        Groupable="false"
                                        DataIndex="INVENTARIO_ENTRADAS_CANTIDAD"
                                        SummaryType="Average">
                                        <SummaryRenderer Handler="return '(' + Ext.util.Format.number(value,'0.0000') + ' Promedio' + ')';" />
                                        <Renderer Format="Number" FormatArgs="'0.0000'" />
                                    </ext:GroupingSummaryColumn>

                                    <ext:GroupingSummaryColumn
                                        ColumnID="SaldoTotalInventario"
                                        Header="Saldo de Salidas"
                                        Sortable="false"
                                        Groupable="false"
                                        DataIndex="INVENTARIO_SALIDAS_SALDO"
                                        SummaryType="Average">
                                        <SummaryRenderer Handler="return '(' + Ext.util.Format.number(value,'0,000.0000') + ' Promedio' + ')';" />
                                        <Renderer Format="Number" FormatArgs="'0,000.0000'" />
                                    </ext:GroupingSummaryColumn>

                                    <ext:Column DataIndex="CREADO_POR" Header="Creado Por" Sortable="true" ></ext:Column>

                                    <ext:DateColumn DataIndex="FECHA_CREACION" Header="Fecha de Creación" Sortable="true" ></ext:DateColumn>

                                    <ext:Column DataIndex="SOCIOS_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                        <Renderer Handler="return '';" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                            </SelectionModel>
                            <Plugins>
                                <ext:GroupingSummary ID="GroupingSummary1" runat="server">
                                </ext:GroupingSummary>
                            </Plugins>
                            <View>
                                <ext:GroupingView ID="GridView1" runat="server"
                                    HideGroupedColumn="true"
                                    ForceFit="true"
                                    MarkDirty="false"
                                    ShowGroupName="false"
                                    EnableNoGroups="true">
                                    <Listeners>
                                        <Refresh Fn="setGroupStyle" />
                                    </Listeners>
                                    <HeaderRows>
                                        <ext:HeaderRow>
                                            <Columns>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:ComboBox  runat="server" ID="f_SOCIOS_ID" 
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
                                                                <Select Handler="PageX.reloadGridStore();" />
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                                <TriggerClick Handler="this.clearValue();" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:ComboBox
                                                            ID="f_CLASIFICACIONES_CAFE_NOMBRE" 
                                                            runat="server"
                                                            Icon="Find"
                                                            AllowBlank="true"
                                                            ForceSelection="true"
                                                            StoreID="ClasificacionesCafeSt"
                                                            ValueField="CLASIFICACIONES_CAFE_NOMBRE" 
                                                            DisplayField="CLASIFICACIONES_CAFE_NOMBRE"
                                                            Mode="Local"
                                                            TypeAhead="true">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear"/>
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="PageX.reloadGridStore();" />
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                                <TriggerClick Handler="this.clearValue();" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:ComboBox ID="f_DESCRIPCION" runat="server" EnableKeyEvents="true" Icon="Find" AllowBlank="true" ForceSelection="true" TypeAhead="true">
                                                            <Items>
                                                                <ext:ListItem Text="Deposito de Café" Value="ENTRADA" />
                                                                <ext:ListItem Text="Hoja de Liquidación" Value="SALIDA" />
                                                            </Items>
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear"/>
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="PageX.reloadGridStore();" />
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                                <TriggerClick Handler="this.clearValue();" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:DropDownField ID="f_FECHA" AllowBlur="true" runat="server" Editable="false"
                                                            Mode="ValueText" Icon="Find" TriggerIcon="SimpleArrowDown" CollapseMode="Default">
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
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_TRANSACCION_NUMERO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_ENTRADAS_CANTIDAD" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_SALIDAS_CANTIDAD" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_SALIDAS_COSTO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_SALIDAS_MONTO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_INVENTARIO_ENTRADAS_CANTIDAD" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_INVENTARIO_SALIDAS_SALDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_CREADO_POR" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:DateField ID="f_FECHA_CREACION" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:DateField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                               
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:Button ID="ClearFilterButton" runat="server" Icon="Cancel">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip4" runat="server" Html="Clear filter" />
                                                            </ToolTips>
                                                            <Listeners>
                                                                <Click Handler="PageX.clearFilter();" />
                                                            </Listeners>                                            
                                                        </ext:Button>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:GroupingView>
                            </View>
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Button ID="Button3" runat="server" Text="Mostrar Totales" ToolTip="Mostrar Totales en los grupos.">
                                            <Listeners>
                                                <Click Handler="#{MovimientoInventarioCafeGridP}.getGroupingSummary().toggleSummaries();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button 
                                            ID="btnToggleGroups" 
                                            runat="server" 
                                            Text="Expandir/Cerrar Grupos"
                                            Icon="TableSort"
                                            Style="margin-left: 6px;"
                                            AutoPostBack="false">
                                            <Listeners>
                                                <Click Handler="#{MovimientoInventarioCafeGridP}.getView().toggleAllGroups();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <LoadMask ShowMask="true" />
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>