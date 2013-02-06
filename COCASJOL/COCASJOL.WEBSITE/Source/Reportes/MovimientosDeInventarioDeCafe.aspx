<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovimientosDeInventarioDeCafe.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Reportes.MovimientosDeInventarioDeCafe" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inventario de Café por Socio</title>
    <script type="text/javascript" src="../../resources/js/reportes/movimientosDeInventarioDeCafe.js" ></script>
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
            <PageUp Handler="PageX.navPrev();" />
            <PageDown Handler="PageX.navNext();" />
            <End Handler="PageX.navEnd();" />
        </ext:KeyNav>

        <asp:ObjectDataSource ID="MovimientoDeInventarioCafeDS" runat="server"
                TypeName="COCASJOL.LOGIC.Reportes.MovimientosDeInventarioDeCafeLogic"
                SelectMethod="GetMovimientosDeInventarioDeCafeLogic" onselecting="MovimientoDeInventarioCafeDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="TRANSACCION_NUMERO"           Type="Int32"    ControlID="nullHdn"                        PropertyName="Text" />
                    <asp:ControlParameter Name="FECHA"                        Type="DateTime" ControlID="nullHdn"                        PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_DESDE"                  Type="DateTime" ControlID="f_DATE_FROM"                    PropertyName="Text" />
                    <asp:ControlParameter Name="FECHA_HASTA"                  Type="DateTime" ControlID="f_DATE_TO"                      PropertyName="Text" />
                    <asp:ControlParameter Name="SOCIOS_ID"                    Type="String"   ControlID="f_SOCIOS_ID"                    PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_NOMBRE"  Type="String"   ControlID="f_CLASIFICACIONES_CAFE_NOMBRE"  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="DESCRIPCION"                  Type="String"   ControlID="f_DESCRIPCION"                  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="INVENTARIO_ENTRADAS_CANTIDAD" Type="Decimal"  ControlID="f_INVENTARIO_ENTRADAS_CANTIDAD" PropertyName="Text" DefaultValue="-1"/>
                    <asp:ControlParameter Name="INVENTARIO_SALIDAS_SALDO"     Type="Decimal"  ControlID="f_INVENTARIO_SALIDAS_SALDO"     PropertyName="Text" DefaultValue="-1"/>
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
                        <ext:GridPanel ID="InventarioCafeGridP" runat="server" AutoExpandColumn="FECHA" Height="300"
                            Title="Inventario de Café" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="InventarioCafeSt" runat="server" DataSourceID="MovimientoDeInventarioCafeDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="TRANSACCION_NUMERO"           />
                                                <ext:RecordField Name="FECHA"                        Type="Date" />
                                                <ext:RecordField Name="FECHA_DESDE"                  Type="Date" />
                                                <ext:RecordField Name="FECHA_HASTA"                  Type="Date" />
                                                <ext:RecordField Name="DESCRIPCION"                  />
                                                <ext:RecordField Name="SOCIOS_ID"                    />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE"  />
                                                <ext:RecordField Name="INVENTARIO_ENTRADAS_CANTIDAD" />
                                                <ext:RecordField Name="INVENTARIO_SALIDAS_SALDO"     />
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
                                    <ext:Column DataIndex="SOCIOS_ID"                    Header="Id de Socio" Sortable="true"></ext:Column>
                                    <ext:DateColumn DataIndex="FECHA"                        Header="Fecha" Sortable="true"></ext:DateColumn>
                                    <ext:Column DataIndex="CLASIFICACIONES_CAFE_NOMBRE"  Header="Clasificación de Café" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="DESCRIPCION"                  Header="Descripción" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="INVENTARIO_ENTRADAS_CANTIDAD" Header="Cantidad" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="INVENTARIO_SALIDAS_SALDO"     Header="Saldo de Salidas" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="SOCIOS_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                        <Renderer Handler="return '';" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
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
                                                        <%--<ext:TextField ID="f_SOCIOS_ID" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="5">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>--%>
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
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="InventarioCafeSt" />
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
    </div>
    </form>
</body>
</html>