<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentasDeInventarioDeCafe.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Inventario.Salidas.VentasDeInventarioDeCafe" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ventas de Inventario de Café</title>

    <link rel="Stylesheet" type="text/css" href="../../../resources/css/ComboBox_Grid.css" />
    <script type="text/javascript" src="../../../resources/js/inventario/salidas/ventasDeInventarioDeCafe.js" ></script>
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

        <aud:Auditoria runat="server" ID="AudWin" />

        <asp:ObjectDataSource ID="VentaInventarioDeCafeDs" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.Salidas.VentaDeInventarioDeCafeLogic"
                SelectMethod="GetVentasDeInventarioDeCafe"
                InsertMethod="InsertarVentaDeInventarioDeCafe" onselecting="VentaInventarioDeCafeDs_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="VENTAS_INV_CAFE_ID"                  Type="Int32"    ControlID="f_VENTAS_INV_CAFE_ID"              PropertyName="Text" DefaultValue="0" />
                    <asp:ControlParameter Name="SOCIOS_ID"                           Type="String"   ControlID="f_SOCIOS_ID"                       PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_ID"             Type="Int32"    ControlID="f_CLASIFICACIONES_CAFE_ID"         PropertyName="Text" DefaultValue="0" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_NOMBRE"         Type="String"   ControlID="nullHdn"                           PropertyName="Text" DefaultValue="0" />
                    <asp:ControlParameter Name="VENTAS_INV_CAFE_FECHA"               Type="DateTime" ControlID="f_VENTAS_INV_CAFE_FECHA"           PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_DESDE"                         Type="DateTime" ControlID="f_DATE_FROM"                       PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_HASTA"                         Type="DateTime" ControlID="f_DATE_TO"                         PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="VENTAS_INV_CAFE_CANTIDAD_LIBRAS"     Type="Decimal"  ControlID="nullHdn"                           PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="VENTAS_INV_CAFE_PRECIO_LIBRAS"       Type="Decimal"  ControlID="nullHdn"                           PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="VENTAS_INV_CAFE_SALDO_TOTAL"         Type="Decimal"  ControlID="f_VENTAS_INV_CAFE_SALDO_TOTAL"     PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="CREADO_POR"                          Type="String"   ControlID="nullHdn"                           PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"                      Type="DateTime" ControlID="nullHdn"                           PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"                      Type="String"   ControlID="nullHdn"                           PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION"                  Type="DateTime" ControlID="nullHdn"                           PropertyName="Text" DefaultValue="" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="VENTAS_INV_CAFE_ID"              Type="Int32"    />
                    <asp:Parameter Name="SOCIOS_ID"                       Type="String"   />
                    <asp:Parameter Name="CLASIFICACIONES_CAFE_ID"         Type="Int32"    />
                    <asp:Parameter Name="CLASIFICACIONES_CAFE_NOMBRE"     Type="String"   />
                    <asp:Parameter Name="VENTAS_INV_CAFE_FECHA"           Type="DateTime" />
                    <asp:Parameter Name="FECHA_DESDE"                     Type="DateTime" />
                    <asp:Parameter Name="FECHA_HASTA"                     Type="DateTime" />
                    <asp:Parameter Name="VENTAS_INV_CAFE_CANTIDAD_LIBRAS" Type="Decimal"  />
                    <asp:Parameter Name="VENTAS_INV_CAFE_PRECIO_LIBRAS"   Type="Decimal"  />
                    <asp:Parameter Name="VENTAS_INV_CAFE_SALDO_TOTAL"     Type="Decimal"  />
                    <asp:Parameter Name="CREADO_POR"                      Type="String"   />
                    <asp:Parameter Name="FECHA_CREACION"                  Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"                  Type="String"   />
                    <asp:Parameter Name="FECHA_MODIFICACION"              Type="DateTime" />
                </InsertParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="SociosDS" runat="server"
                TypeName="COCASJOL.LOGIC.Socios.SociosLogic"
                SelectMethod="getSociosActivos" >
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="ClasificacionesCafeDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.ClasificacionDeCafeLogic"
                SelectMethod="GetClasificacionesDeCafe" >
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="InventarioDeCafeDS" runat="server"
                TypeName="COCASJOL.LOGIC.Aportaciones.AportacionLogic"
                SelectMethod="GetAportaciones" >
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

        <ext:Store ID="ClasificacionesCafeSt" runat="server" DataSourceID="ClasificacionesCafeDS">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="CLASIFICACIONES_CAFE_ID" />
                        <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Store ID="InventarioDeCafeSt" runat="server" DataSourceID="InventarioDeCafeDS" AutoSave="true" SkipIdForNewRecords="false" >
            <Reader>
                <ext:JsonReader IDProperty="SOCIOS_ID">
                    <Fields>
                        <ext:RecordField Name="SOCIOS_ID"                    />
                        <ext:RecordField Name="SOCIOS_NOMBRE_COMPLETO"       />
                        <ext:RecordField Name="APORTACIONES_ORDINARIA_SALDO" />
                        <ext:RecordField Name="INVENTARIO_ENTRADAS_CANTIDAD" />
                        <ext:RecordField Name="INVENTARIO_SALIDAS_SALDO"     />
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
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Ventas de Inventario de Café" Icon="CartFull" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="VentaInventarioDeCafeGridP" runat="server" AutoExpandColumn="SOCIOS_ID" Height="300"
                            Title="Ventas de Inventario de Café" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <KeyMap>
                                <ext:KeyBinding Ctrl="true" >
                                    <Keys>
                                        <ext:Key Code="INSERT" />
                                        <ext:Key Code="ENTER" />
                                    </Keys>
                                    <Listeners>
                                        <Event Handler="PageX.gridKeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:KeyBinding>
                            </KeyMap>
                            <Store>
                                <ext:Store ID="VentaInventarioDeCafeSt" runat="server" DataSourceID="VentaInventarioDeCafeDs" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="RETIROS_AP_ID">
                                            <Fields>
                                                <ext:RecordField Name="VENTAS_INV_CAFE_ID"              />
                                                <ext:RecordField Name="SOCIOS_ID"                       />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_ID"         />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE"     ServerMapping="clasificaciones_cafe.CLASIFICACIONES_CAFE_NOMBRE"/>
                                                <ext:RecordField Name="VENTAS_INV_CAFE_FECHA"           Type="Date" />
                                                <ext:RecordField Name="FECHA_DESDE"                     />
                                                <ext:RecordField Name="FECHA_HASTA"                     />
                                                <ext:RecordField Name="VENTAS_INV_CAFE_CANTIDAD_LIBRAS" />
                                                <ext:RecordField Name="VENTAS_INV_CAFE_PRECIO_LIBRAS"   />
                                                <ext:RecordField Name="VENTAS_INV_CAFE_SALDO_TOTAL"     />
                                                <ext:RecordField Name="CREADO_POR"                      />
                                                <ext:RecordField Name="FECHA_CREACION"                  Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"                  />
                                                <ext:RecordField Name="FECHA_MODIFICACION"              Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                    <Listeners>
                                        <CommitDone Handler="InventarioDeCafeSt.reload(); Ext.Msg.alert('Guardar', 'Cambios guardados exitosamente.');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column       DataIndex="VENTAS_INV_CAFE_ID"          Header="Numero de Venta" Sortable="true"></ext:Column>
                                    <ext:Column       DataIndex="SOCIOS_ID"                   Header="Id de Socio" Sortable="true"></ext:Column>
                                    <ext:Column       DataIndex="CLASIFICACIONES_CAFE_NOMBRE" Header="Clasificación de Café" Sortable="true"></ext:Column>
                                    <ext:DateColumn   DataIndex="VENTAS_INV_CAFE_FECHA"       Header="Fecha" Sortable="true"></ext:DateColumn>
                                    <ext:NumberColumn DataIndex="VENTAS_INV_CAFE_SALDO_TOTAL" Header="Saldo Total" Sortable="true"></ext:NumberColumn>

                                    <ext:Column DataIndex="VENTAS_INV_CAFE_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
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
                                        <ext:Button ID="AgregarBtn" runat="server" Text="Agregar" Icon="CoinsAdd">
                                            <Listeners>
                                                <Click Handler="PageX.add();" />
                                            </Listeners>
                                        </ext:Button>
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
                                                        <ext:NumberField ID="f_VENTAS_INV_CAFE_ID" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="5">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
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
                                                        <ext:ComboBox
                                                            ID="f_CLASIFICACIONES_CAFE_ID" 
                                                            runat="server"
                                                            Icon="Find"
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
                                                                <Select Handler="PageX.reloadGridStore();" />
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                                <TriggerClick Handler="this.clearValue();" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:DropDownField ID="f_VENTAS_INV_CAFE_FECHA" AllowBlur="true" runat="server" Editable="false"
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
                                                        <ext:NumberField ID="f_VENTAS_INV_CAFE_SALDO_TOTAL" runat="server" EnableKeyEvents="true" Icon="Find">
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
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="VentaInventarioDeCafeSt" />
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

        <ext:Window ID="AgregarVentaDeInventarioDeCafeWin"
            runat="server"
            Hidden="true"
            Icon="CartAdd"
            Title="Agregar Venta de Inventario de Café"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
                <Show Handler="#{AddFechaVentaTxt}.setValue(new Date()); #{AddFechaVentaTxt}.focus(false,200);" />
                <Hide Handler="#{AgregarVentaDeInventarioDeCafeFormP}.getForm().reset();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="AgregarVentaDeInventarioDeCafeFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="200">
                    <Items>
                        <ext:Panel ID="Panel2" runat="server" Title="Venta de Inventario de Café" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Items>
                                <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="FormLayout" Border="false" AnchorHorizontal="100%" >
                                    <Items>
                                        <ext:DateField runat="server" ID="AddFechaVentaTxt" DataIndex="VENTAS_INV_CAFE_FECHA" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ></ext:DateField>
                                        
                                        <ext:ComboBox  runat="server" ID="AddSociosIdTxt"  DataIndex="SOCIOS_ID" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Código Socio" AllowBlank="false" MsgTarget="Side"
                                            TypeAhead="true"
                                            EmptyText="Seleccione un Socio"
                                            ForceSelection="true" 
                                            StoreID="SocioSt"
                                            Mode="Local" 
                                            DisplayField="SOCIOS_ID"
                                            ValueField="SOCIOS_ID"
                                            MinChars="2" 
                                            ListWidth="400" 
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
                                                <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide(); AgregarVentaDeInventarioDeCafeFormP.getForm().reset(); }" />
                                                <Select Handler="this.triggers[0].show(); PageX.addGetNombreDeSocio(Ext.getCmp('AddSociosIdTxt'), Ext.getCmp('AddNombreTxt')); PageX.getInventarioFueraDeCatacion();" />
                                            </Listeners>
                                        </ext:ComboBox>

                                        <ext:TextField   runat="server" ID="AddNombreTxt" DataIndex="SOCIOS_NOMBRE_COMPLETO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip11" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>

                                        <ext:ComboBox runat="server"    ID="AddClasificacionCafeCmb" DataIndex="CLASIFICACIONES_CAFE_ID" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tipo de Café" AllowBlank="false" MsgTarget="Side"
                                            StoreID="ClasificacionesCafeSt"
                                            EmptyText="Seleccione un Tipo"
                                            ValueField="CLASIFICACIONES_CAFE_ID" 
                                            DisplayField="CLASIFICACIONES_CAFE_NOMBRE"
                                            ForceSelection="true"
                                            Mode="Local"
                                            TypeAhead="true">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide();}" />
                                                <Select Handler="this.triggers[0].show(); PageX.getInventarioFueraDeCatacion(); PageX.validarCantidadVenta(#{AddCantidadLibrasTxt}, #{AddPrecioLibrasTxt}, #{AddInventarioDeCafeCantidadTxt}, #{AddTotalSaldoTxt});" />
                                            </Listeners>
                                        </ext:ComboBox>

                                        <ext:FieldSet runat="server" ID="AddVentasFS" Title="Inventario de Café de Socio" Padding="5" LabelWidth="200" >
                                            <Items>
                                                <ext:NumberField runat="server" ID="AddInventarioDeCafeCantidadTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Disponible (Lbs)" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip13" runat="server" Title="Cantidad Disponible" Html="La cantidad disponible de inventario de café es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet runat="server" ID="AddRetiroFS" Title="Venta" Padding="5" >
                                            <Items>
                                                <ext:NumberField runat="server" ID="AddCantidadLibrasTxt" FieldLabel="Total Lbs. Netas" Text="0" DataIndex="VENTAS_INV_CAFE_CANTIDAD_LIBRAS" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" >
                                                    <Listeners>
                                                        <Change Handler="PageX.validarCantidadVenta(#{AddCantidadLibrasTxt} ,#{AddPrecioLibrasTxt}, #{AddInventarioDeCafeCantidadTxt}, #{AddTotalSaldoTxt}); PageX.loadTotalRetiro();" />
                                                    </Listeners>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="AddPrecioLibrasTxt" FieldLabel="Precio por Libra" Text="0" DataIndex="VENTAS_INV_CAFE_PRECIO_LIBRAS" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" >
                                                    <Listeners>
                                                        <Change Handler="PageX.validarCantidadVenta(#{AddCantidadLibrasTxt}, #{AddPrecioLibrasTxt}, #{AddInventarioDeCafeCantidadTxt}, #{AddTotalSaldoTxt}); PageX.loadTotalRetiro();" />
                                                    </Listeners>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="AddTotalSaldoTxt" FieldLabel="Valor Total del Producto" Text="0" DataIndex="VENTAS_INV_CAFE_SALDO_TOTAL" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip19" runat="server" Title="Valor Total del Producto" Html="El valor total es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:TextField runat="server"   ID="AddCreatedByTxt"     DataIndex="CREADO_POR"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                        <ext:TextField runat="server"   ID="AddCreationDateTxt"  DataIndex="FECHA_CREACION"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button3" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="#{AddCreatedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.insert();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window ID="EditarVentaDeInventarioDeCafeWin"
            runat="server"
            Hidden="true"
            Icon="CartEdit"
            Title="Venta de Inventario de Café"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="EditarVentaDeInventarioDeCafeFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="200">
                    <Items>
                        <ext:Panel ID="Panel4" runat="server" Title="Venta de Inventario de Café" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Items>
                                <ext:Panel ID="Panel5" runat="server" Frame="false" Padding="5" Layout="FormLayout" Border="false" AnchorHorizontal="100%" >
                                    <Items>
                                        <ext:DateField runat="server" ID="EditFechaVentaTxt" DataIndex="VENTAS_INV_CAFE_FECHA" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip2" runat="server" Title="Fecha" Html="La fecha es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:DateField>
                                        
                                        <ext:ComboBox  runat="server" ID="EditSociosIdTxt"  DataIndex="SOCIOS_ID" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Código Socio" AllowBlank="false" MsgTarget="Side"
                                            TypeAhead="true"
                                            EmptyText="Seleccione un Socio"
                                            ForceSelection="true" 
                                            StoreID="SocioSt"
                                            Mode="Local" 
                                            DisplayField="SOCIOS_ID"
                                            ValueField="SOCIOS_ID"
                                            ReadOnly="true">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip3" runat="server" Title="Socio" Html="El socio es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:ComboBox>

                                        <ext:TextField   runat="server" ID="EditNombreTxt" DataIndex="SOCIOS_NOMBRE_COMPLETO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip111" runat="server" Title="Socio" Html="El socio es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>

                                        <ext:ComboBox runat="server"    ID="EditClasificacionCafeCmb" DataIndex="CLASIFICACIONES_CAFE_ID" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tipo de Café" AllowBlank="false" MsgTarget="Side"
                                            StoreID="ClasificacionesCafeSt"
                                            EmptyText="Seleccione un Tipo"
                                            ValueField="CLASIFICACIONES_CAFE_ID" 
                                            DisplayField="CLASIFICACIONES_CAFE_NOMBRE"
                                            ForceSelection="true"
                                            Mode="Local"
                                            TypeAhead="true"
                                            ReadOnly="true">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Title="Clasificación de Café" Html="La clasificación de café es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:ComboBox>

                                        <ext:FieldSet runat="server" ID="EditRetiroFS" Title="Venta" Padding="5" >
                                            <Items>
                                                <ext:NumberField runat="server" ID="EditCantidadLibrasTxt" FieldLabel="Total Lbs. Netas" Text="0" DataIndex="VENTAS_INV_CAFE_CANTIDAD_LIBRAS" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip5" runat="server" Title="Total Lbs. Netas" Html="Solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditPrecioLibrasTxt" FieldLabel="Precio por Libra" Text="0" DataIndex="VENTAS_INV_CAFE_PRECIO_LIBRAS" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip6" runat="server" Title="Precio por Libra" Html="Solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditTotalSaldoTxt" FieldLabel="Valor Total del Producto" Text="0" DataIndex="VENTAS_INV_CAFE_SALDO_TOTAL" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip7" runat="server" Title="Valor Total del Producto" Html="Solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:TextField runat="server"   ID="EditCreatedByTxt"     DataIndex="CREADO_POR"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                        <ext:TextField runat="server"   ID="EditCreationDateTxt"  DataIndex="FECHA_CREACION"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
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
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>
