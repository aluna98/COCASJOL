<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HojasDeLiquidacion.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Inventario.Salidas.HojasDeLiquidacion" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hojas De Liquidación</title>
    <%--<meta http-equiv="refresh" content="3" />--%>
     
    <link rel="Stylesheet" type="text/css" href="../../../resources/css/ComboBox_Grid.css" />

    <script type="text/javascript" src="../../../resources/js/inventario/salidas/hojasDeLiquidacion.js" ></script>
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

        <asp:ObjectDataSource ID="HojasDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.Salidas.HojaDeLiquidacionLogic"
                SelectMethod="GetHojasDeLiquidacion"
                InsertMethod="InsertarHojaDeLiquidacion" onselecting="HojasDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="LIQUIDACIONES_ID"                                   Type="Int32"    ControlID="f_LIQUIDACIONES_ID"        PropertyName="Text" />
                    <asp:ControlParameter Name="SOCIOS_ID"                                          Type="String"   ControlID="f_SOCIOS_ID"               PropertyName="Text" />
                    <asp:ControlParameter Name="LIQUIDACIONES_FECHA"                                Type="DateTime" ControlID="f_LIQUIDACIONES_FECHA"     PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_DESDE"                                        Type="DateTime" ControlID="f_DATE_FROM"               PropertyName="Text" />
                    <asp:ControlParameter Name="FECHA_HASTA"                                        Type="DateTime" ControlID="f_DATE_TO"                 PropertyName="Text" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_ID"                            Type="Int32"    ControlID="f_CLASIFICACIONES_CAFE_ID" PropertyName="Text" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_NOMBRE"                        Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="LIQUIDACIONES_TOTAL_LIBRAS"                         Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_PRECIO_LIBRAS"                        Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_VALOR_TOTAL"                          Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_CUOTA_INGRESO"                      Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_GASTOS_ADMIN"                       Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_APORTACION_ORDINARIO"               Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA"          Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION"           Type="Int32"    ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD"  Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_INTERESES_S_APORTACIONES"           Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_EXCEDENTE_PERIODO"                  Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO"               Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO"                Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_PRESTAMO_PRENDARIO"                 Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_CUENTAS_X_COBRAR"                   Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_INTERESES_X_COBRAR"                 Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_OTRAS_DEDUCCIONES"                  Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_TOTAL_DEDUCCIONES"                  Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_AF_SOCIO"                           Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="LIQUIDACIONES_D_TOTAL"                              Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="CREADO_POR"                                         Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"                                     Type="DateTime" ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"                                     Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION"                                 Type="DateTime" ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="SOCIOS_APORTACION_EXTRAORD_COOP_COUNT"              Type="Int32"    ControlID="nullHdn"                   PropertyName="Text" DefaultValue="0" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="LIQUIDACIONES_ID"                                  Type="Int32"    />
                    <asp:Parameter Name="SOCIOS_ID"                                         Type="String"   />
                    <asp:Parameter Name="LIQUIDACIONES_FECHA"                               Type="DateTime" />
                    <asp:Parameter Name="FECHA_DESDE"                                       Type="DateTime" />
                    <asp:Parameter Name="FECHA_HASTA"                                       Type="DateTime" />
                    <asp:Parameter Name="CLASIFICACIONES_CAFE_ID"                           Type="Int32"    />
                    <asp:Parameter Name="CLASIFICACIONES_CAFE_NOMBRE"                       Type="String"   />
                    <asp:Parameter Name="LIQUIDACIONES_TOTAL_LIBRAS"                        Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_PRECIO_LIBRAS"                       Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_VALOR_TOTAL"                         Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_CUOTA_INGRESO"                     Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_GASTOS_ADMIN"                      Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_APORTACION_ORDINARIO"              Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA"         Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION"          Type="Int32"    />
                    <asp:Parameter Name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD" Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_INTERESES_S_APORTACIONES"          Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_EXCEDENTE_PERIODO"                 Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO"              Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO"               Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_PRESTAMO_PRENDARIO"                Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_CUENTAS_X_COBRAR"                  Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_INTERESES_X_COBRAR"                Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_OTRAS_DEDUCCIONES"                 Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_TOTAL_DEDUCCIONES"                 Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_AF_SOCIO"                          Type="Decimal"  />
                    <asp:Parameter Name="LIQUIDACIONES_D_TOTAL"                             Type="Decimal"  />
                    <asp:Parameter Name="CREADO_POR"                                        Type="String"   />
                    <asp:Parameter Name="FECHA_CREACION"                                    Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"                                    Type="String"   />
                    <asp:Parameter Name="FECHA_MODIFICACION"                                Type="DateTime" />
                    <asp:Parameter Name="SOCIOS_APORTACION_EXTRAORD_COOP_COUNT"             Type="Int32"    />
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

        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:NumberField ID="CantidadDeAportacionExtraordParaCoopHdn" runat="server" >
        </ext:NumberField>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Hoja de Liquidación" Icon="Script" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="HojasGridP" runat="server" AutoExpandColumn="CLASIFICACIONES_CAFE_NOMBRE" Height="300"
                            Title="Hojas de Liquidación" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <KeyMap>
                                <ext:KeyBinding>
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
                                <ext:Store ID="HojasSt" runat="server" DataSourceID="HojasDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="LIQUIDACIONES_ID">
                                            <Fields>
                                                <ext:RecordField Name="LIQUIDACIONES_ID"                                  />
                                                <ext:RecordField Name="SOCIOS_ID"                                         />
                                                <ext:RecordField Name="LIQUIDACIONES_FECHA"                               Type="Date" />
                                                <ext:RecordField Name="FECHA_DESDE"                                       Type="Date" DefaultValue="" />
                                                <ext:RecordField Name="FECHA_HASTA"                                       Type="Date" DefaultValue="" />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_ID"                           />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE"                       ServerMapping="clasificaciones_cafe.CLASIFICACIONES_CAFE_NOMBRE"/>
                                                <ext:RecordField Name="LIQUIDACIONES_TOTAL_LIBRAS"                        />
                                                <ext:RecordField Name="LIQUIDACIONES_PRECIO_LIBRAS"                       />
                                                <ext:RecordField Name="LIQUIDACIONES_VALOR_TOTAL"                         />
                                                <ext:RecordField Name="LIQUIDACIONES_D_CUOTA_INGRESO"                     />
                                                <ext:RecordField Name="LIQUIDACIONES_D_GASTOS_ADMIN"                      />
                                                <ext:RecordField Name="LIQUIDACIONES_D_APORTACION_ORDINARIO"              />
                                                <ext:RecordField Name="LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA"         />
                                                <ext:RecordField Name="LIQUIDACIONES_D_CUOTA_ADMIN"                       />
                                                <ext:RecordField Name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION"          />
                                                <ext:RecordField Name="LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD" />
                                                <ext:RecordField Name="LIQUIDACIONES_D_INTERESES_S_APORTACIONES"          />
                                                <ext:RecordField Name="LIQUIDACIONES_D_EXCEDENTE_PERIODO"                 />
                                                <ext:RecordField Name="LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO"              />
                                                <ext:RecordField Name="LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO"               />
                                                <ext:RecordField Name="LIQUIDACIONES_D_PRESTAMO_PRENDARIO"                />
                                                <ext:RecordField Name="LIQUIDACIONES_D_CUENTAS_X_COBRAR"                  />
                                                <ext:RecordField Name="LIQUIDACIONES_D_INTERESES_X_COBRAR"                />
                                                <ext:RecordField Name="LIQUIDACIONES_D_OTRAS_DEDUCCIONES"                 />
                                                <ext:RecordField Name="LIQUIDACIONES_D_TOTAL_DEDUCCIONES"                 />
                                                <ext:RecordField Name="LIQUIDACIONES_D_AF_SOCIO"                          />
                                                <ext:RecordField Name="LIQUIDACIONES_D_TOTAL"                             />
                                                <ext:RecordField Name="SOCIOS_APORTACION_EXTRAORD_COOP_COUNT"             />
                                                <ext:RecordField Name="CREADO_POR"                                        />
                                                <ext:RecordField Name="FECHA_CREACION"                                    Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"                                    />
                                                <ext:RecordField Name="FECHA_MODIFICACION"                                Type="Date" />
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
                                    <ext:Column     DataIndex="LIQUIDACIONES_ID"            Header="Numero" Sortable="true"></ext:Column>
                                    <ext:Column     DataIndex="SOCIOS_ID"                   Header="Socio" Sortable="true"></ext:Column>
                                    <ext:Column     DataIndex="CLASIFICACIONES_CAFE_NOMBRE" Header="Clasificación de Café" Sortable="true"></ext:Column>
                                    <ext:DateColumn DataIndex="LIQUIDACIONES_FECHA"         Header="Fecha" Sortable="true" Width="150" ></ext:DateColumn>

                                    <ext:Column DataIndex="LIQUIDACIONES_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
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
                                        <ext:Button ID="AgregarBtn" runat="server" Text="Agregar" Icon="ScriptAdd">
                                            <Listeners>
                                                <Click Handler="PageX.add();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EditarBtn" runat="server" Text="Editar" Icon="ScriptEdit">
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
                                                        <ext:NumberField ID="f_LIQUIDACIONES_ID" runat="server" EnableKeyEvents="true" Icon="Find">
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
                                                        <ext:DropDownField ID="f_LIQUIDACIONES_FECHA" AllowBlur="true" runat="server" Editable="false"
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
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="HojasSt" />
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

        <ext:Window ID="AgregarHojasWin"
            runat="server"
            Hidden="true"
            Icon="ScriptAdd"
            Title="Agregar Hoja de Liquidación"
            Width="800"
            Height="480"
            Resizable="false"
            Shadow="None"
            Modal="true"
            Maximizable="false"
            InitCenter="true"
            ConstrainHeader="true" Layout="FitLayout" >
            <Listeners>
                <Show Handler="#{AddFechaHojaTxt}.setValue(new Date()); PageX.loadCapitalizacionXRetencion(); PageX.AddCalcularTotalDeducciones();" />
                <Hide Handler="#{AgregarHojasFormP}.getForm().reset();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="AgregarHojasFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelAlign="Right" LabelWidth="130" Layout="ContainerLayout" AutoScroll="true" >
                    <Items>
                        <ext:Panel ID="Panel10" runat="server" Title="Hoja de Liquidación" Header="true" Layout="AnchorLayout" AutoHeight="True" Resizable="false" AnchorHorizontal="100%" >
                            <Items>
                                <ext:Panel ID="Panel11" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" AnchorHorizontal="100%" Border="false">
                                    <Items>
                                        <ext:FieldSet ID="AddHojaHeaderFS" runat="server" Header="false" Padding="5">
                                            <Items>
                                                <ext:Panel ID="AddSocioPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                                    <LayoutConfig>
                                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                                    </LayoutConfig>
                                                    <Items>
                                                        <ext:Panel ID="Panel12" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="AddSociosAportacionesExtraCoopTxt" DataIndex="SOCIOS_APORTACION_EXTRAORD_COOP_COUNT" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="" Hidden="true" ReadOnly="true">
                                                                    <Listeners>
                                                                        <Render Handler="AddSociosAportacionesExtraCoopTxt.setValue(CantidadDeAportacionExtraordParaCoopHdn.getValue());" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:DateField runat="server" ID="AddFechaHojaTxt" DataIndex="LIQUIDACIONES_FECHA" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side"></ext:DateField>
                                                                <ext:ComboBox  runat="server" ID="AddSociosIdTxt"  DataIndex="SOCIOS_ID"           LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Código de Socio" AllowBlank="false" MsgTarget="Side"
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
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide(); Ext.getCmp('EditNombreTxt').reset(); Ext.getCmp('EditDireccionFincaTxt').reset(); }" />
                                                                        <Select Handler="this.triggers[0].show(); PageX.getNombreDeSocio(Ext.getCmp('AddSociosIdTxt'), Ext.getCmp('AddNombreTxt')); PageX.getDireccionDeFinca(Ext.getCmp('AddSociosIdTxt'), Ext.getCmp('AddDireccionFincaTxt')); PageX.loadCuotaDeIngreso(); PageX.loadAportacionesOrdinariaYExtraordinaria(); PageX.getInventarioFueraDeCatacion();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel ID="Panel23" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:ComboBox runat="server"    ID="AddClasificacionCafeCmb" DataIndex="CLASIFICACIONES_CAFE_ID" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Tipo de Café" AllowBlank="false" MsgTarget="Side"
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
                                                                        <Select Handler="this.triggers[0].show(); PageX.getInventarioFueraDeCatacion();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <ext:NumberField runat="server" ID="AddInventarioCafeTxt" LabelAlign="Right" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Inventario de Café Disponible (Lbs)" ReadOnly="true" Text="0" AllowNegative="false" ></ext:NumberField>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:TextField   runat="server" ID="AddNombreTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip6" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                                <ext:TextField   runat="server" ID="AddDireccionFincaTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Dirección de la Finca" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip7" runat="server" Html="La dirección de la finca de solo lectura." Title="Dirección de la Finca" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:Panel ID="AddCalculosDeduccionesPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:Panel ID="AddCalculosPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" Padding="5" >
                                                    <Items>
                                                        <ext:FieldSet ID="AddCalculosFS" runat="server" Title="Calculo de Valor del Producto" Padding="5" LabelWidth="200" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="AddTotalLibrasTxt"   DataIndex="LIQUIDACIONES_TOTAL_LIBRAS"  LabelAlign="Right" Text="0" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Total Lbs. Netas" IsRemoteValidation="true" >
                                                                    <RemoteValidation OnValidation="AddTotalLibrasTxt_Blur" ValidationEvent="blur" />
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalculosTotalProducto();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddPrecioLibraTxt"   DataIndex="LIQUIDACIONES_PRECIO_LIBRAS"  LabelAlign="Right" Text="0" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Precio por Libra" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalculosTotalProducto();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddTotalProductoTxt" DataIndex="LIQUIDACIONES_VALOR_TOTAL" LabelAlign="Right" Text="0" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Valor Total del Producto" ReadOnly="true" ></ext:NumberField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel ID="AddDeduccionesPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" Padding="5" >
                                                    <Items>
                                                        <ext:FieldSet ID="AddDeduccionesFS" runat="server" Title="Deducciones" Padding="5" LabelWidth="200" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="AddCuotaIngresoTxt"                     DataIndex="LIQUIDACIONES_D_CUOTA_INGRESO"                     LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Cuota de Ingreso" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddGastosAdminTxt"                      DataIndex="LIQUIDACIONES_D_GASTOS_ADMIN"                      LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Gastos de Administración" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddAportacionOrdinariaTxt"              DataIndex="LIQUIDACIONES_D_APORTACION_ORDINARIO"              LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Aportación Ordinaria" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddAportacionExtraOrdinariaTxt"         DataIndex="LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA"         LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Aportación Extraordinaria" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddCapitalizacionXRetencionTxt"         DataIndex="LIQUIDACIONES_D_CAPITALIZACION_RETENCION"          LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="% Capitalización por Retención" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularCapitalizacionXRetencion(); PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddCapitalizacionXRetencionCantidadTxt" DataIndex="LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD" LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Capitalización por Retención" ReadOnly="true" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddInteresesSobreAportacionesTxt"       DataIndex="LIQUIDACIONES_D_INTERESES_S_APORTACIONES"          LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Intereses sobre Aportaciones" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddExcedentePeriodoTxt"                 DataIndex="LIQUIDACIONES_D_EXCEDENTE_PERIODO"                 LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Excedente por Período" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddPrestamoHipotecarioTxt"              DataIndex="LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO"              LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Prestamo Hipotecario" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddPrestamoFiduciarioTxt"               DataIndex="LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO"               LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Prestamo Fiduciario" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddPrestamoPrendarioTxt"                DataIndex="LIQUIDACIONES_D_PRESTAMO_PRENDARIO"                LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Prestamo Prendario" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddCuentasXCobrarTxt"                   DataIndex="LIQUIDACIONES_D_CUENTAS_X_COBRAR"                  LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Cuentas por Cobrar" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddInteresesXCobrarTxt"                 DataIndex="LIQUIDACIONES_D_INTERESES_X_COBRAR"                LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Intereses por Cobrar" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddOtrasDeduccionesTxt"                 DataIndex="LIQUIDACIONES_D_OTRAS_DEDUCCIONES"                 LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Otras Deducciones" >
                                                                    <Listeners>
                                                                        <Change Handler="PageX.AddCalcularTotalDeducciones();" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddTotalDeduccionesTxt"                 DataIndex="LIQUIDACIONES_D_TOTAL_DEDUCCIONES"                 LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="Total Deducciones" ReadOnly="true" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddAFSocioTxt"                          DataIndex="LIQUIDACIONES_D_AF_SOCIO"                          LabelAlign="Right" AnchorHorizontal="100%" AllowNegative="false" AllowBlank="false" Text="0" MsgTarget="Side" FieldLabel="A/F del Socio" ReadOnly="true" ></ext:NumberField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="AddTotalesCalculosDeduccionesPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:FieldSet ID="AddTotalCalculosPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:Panel ID="AddTotalCalculos2Pnl" runat="server" Padding="5" LabelWidth="200" Frame="true" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="AddTotalCalculosFSTxt"    LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Total" AllowBlank="false" MsgTarget="Side" ReadOnly="true" Text="0" ></ext:NumberField>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:FieldSet>
                                                <ext:FieldSet ID="AddTotalDeduccionesPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:Panel ID="AddTotalDeducciones2Pnl" runat="server" Padding="5" LabelWidth="200" Frame="true" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="AddTotalDeduccionesFSTxt" DataIndex="LIQUIDACIONES_D_TOTAL" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Total" MsgTarget="Side" ReadOnly="true" Text="0" ></ext:NumberField>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:FieldSet>
                                            </Items>
                                        </ext:Panel>
                                        <ext:TextField   runat="server" ID="AddCreatedByTxt"        DataIndex="CREADO_POR"               LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="AddCreatedDateTxt"      DataIndex="FECHA_CREACION"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="AddModifiedByTxt"       DataIndex="MODIFICADO_POR"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="AddModificationDateTxt" DataIndex="FECHA_MODIFICACION"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true" >
                            <Listeners>
                                <Click Handler="#{AddCreatedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.insert();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window ID="EditarHojasWin"
            runat="server"
            Hidden="true"
            Icon="ScriptEdit"
            Title="Editar Hoja de Liquidación"
            Width="800"
            Height="480"
            Resizable="false"
            Shadow="None"
            Modal="true"
            Maximizable="false"
            InitCenter="true"
            ConstrainHeader="true" Layout="FitLayout" >
            <Listeners>
                <Show Handler="#{EditFechaHojaTxt}.setValue(new Date());" />
                <Hide Handler="#{EditarHojasFormP}.getForm().reset();" />
            </Listeners>
            <TopBar>
                <ext:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <ext:Button ID="ImprimirBtn" runat="server" Text="Versión para Imprimir" Icon="Printer">
                            <Listeners>
                                <Click Handler="PageX.print();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:FormPanel ID="EditarHojasFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelAlign="Right" LabelWidth="130" Layout="ContainerLayout" AutoScroll="true" >
                    <Items>
                        <ext:Panel ID="Panel22" runat="server" Title="Hoja de Liquidación" Header="true" Layout="AnchorLayout" AutoHeight="True" Resizable="false" AnchorHorizontal="100%" >
                            <Items>
                                <ext:Panel ID="Panel33" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" AnchorHorizontal="100%" Border="false">
                                    <Items>
                                        <ext:FieldSet ID="EditHojaHeaderFS" runat="server" Header="false" Padding="5">
                                            <Items>
                                                <ext:Panel ID="EditSocioPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                                    <LayoutConfig>
                                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                                    </LayoutConfig>
                                                    <Items>
                                                        <ext:Panel ID="Panel14" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:TextField runat="server" ID="EditHojasIdTxt"      DataIndex="LIQUIDACIONES_ID"    LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Numero Hoja de Liquidación" AllowBlank="false" MsgTarget="Side" ReadOnly="true" Hidden="true" ></ext:TextField>
                                                                <ext:DateField runat="server" ID="EditFechaHojaTxt" DataIndex="LIQUIDACIONES_FECHA" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ReadOnly="true" ></ext:DateField>
                                                                <ext:TextField runat="server" ID="EditSociosIdTxt"  DataIndex="SOCIOS_ID"           LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Código de Socio" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="El código de socio es de solo lectura." Title="Código de Socio" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel ID="Panel15" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:TextField runat="server"   ID="EditClasificacionCafeCmb" DataIndex="CLASIFICACIONES_CAFE_NOMBRE" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Tipo de Café" AllowBlank="false" MsgTarget="Side" ReadOnly="true" >
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="La clasificación de café es de solo lectura." Title="Clasificación de Café" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:TextField>
                                                                <%--<ext:NumberField runat="server" ID="EditInventarioCafeTxt" LabelAlign="Right" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Inventario de Café Disponible (Lbs)" ReadOnly="true" Text="0" AllowNegative="false" ></ext:NumberField>--%>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:TextField   runat="server" ID="EditNombreTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip11" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                                <ext:TextField   runat="server" ID="EditDireccionFincaTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Dirección de la Finca" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip12" runat="server" Html="La dirección de la finca de solo lectura." Title="Dirección de la Finca" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:Panel ID="EditCalculosDeduccionesPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:Panel ID="EditCalculosPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" Padding="5" >
                                                    <Items>
                                                        <ext:FieldSet ID="EditCalculosFS" runat="server" Title="Calculo de Valor del Producto" Padding="5" LabelWidth="200" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="EditTotalLibrasTxt"   DataIndex="LIQUIDACIONES_TOTAL_LIBRAS" LabelAlign="Right" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Total Lbs. Netas" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditPrecioLibraTxt"   DataIndex="LIQUIDACIONES_PRECIO_LIBRAS" LabelAlign="Right" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Precio por Libra" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditTotalProductoTxt" DataIndex="LIQUIDACIONES_VALOR_TOTAL" LabelAlign="Right" AnchorHorizontal="100%" AllowBlank="false" MsgTarget="Side" FieldLabel="Valor Total del Producto" ReadOnly="true" ></ext:NumberField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel ID="EditDeduccionesPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" Padding="5" >
                                                    <Items>
                                                        <ext:FieldSet ID="EditDeduccionesFS" runat="server" Title="Deducciones" Padding="5" LabelWidth="200" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="EditCuotaIngresoTxt"                     DataIndex="LIQUIDACIONES_D_CUOTA_INGRESO"                     LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Cuota de Ingreso" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditGastosAdminTxt"                      DataIndex="LIQUIDACIONES_D_GASTOS_ADMIN"                      LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Gastos de Administración" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditAportacionOrdinariaTxt"              DataIndex="LIQUIDACIONES_D_APORTACION_ORDINARIO"              LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Aportación Ordinaria" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditAportacionExtraOrdinariaTxt"         DataIndex="LIQUIDACIONES_D_APORTACION_EXTRAORDINARIA"         LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Aportación Extraordinaria" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditCapitalizacionXRetencionTxt"         DataIndex="LIQUIDACIONES_D_CAPITALIZACION_RETENCION"          LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="% Capitalización por Retención" ReadOnly="true" AllowBlank="false" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditCapitalizacionXRetencionCantidadTxt" DataIndex="LIQUIDACIONES_D_CAPITALIZACION_RETENCION_CANTIDAD" LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Capitalización por Retención" ReadOnly="true" AllowBlank="false" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditInteresesSobreAportacionesTxt"       DataIndex="LIQUIDACIONES_D_INTERESES_S_APORTACIONES"          LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Intereses sobre Aportaciones" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditExcedentePeriodoTxt"                 DataIndex="LIQUIDACIONES_D_EXCEDENTE_PERIODO"                 LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Excedente por Período" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditPrestamoHipotecarioTxt"              DataIndex="LIQUIDACIONES_D_PRESTAMO_HIPOTECARIO"              LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Prestamo Hipotecario" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditPrestamoFiduciarioTxt"               DataIndex="LIQUIDACIONES_D_PRESTAMO_FIDUCIARIO"               LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Prestamo Fiduciario" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditPrestamoPrendarioTxt"                DataIndex="LIQUIDACIONES_D_PRESTAMO_PRENDARIO"                LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Prestamo Prendario" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditCuentasXCobrarTxt"                   DataIndex="LIQUIDACIONES_D_CUENTAS_X_COBRAR"                  LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Cuentas por Cobrar" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditInteresesXCobrarTxt"                 DataIndex="LIQUIDACIONES_D_INTERESES_X_COBRAR"                LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Intereses por Cobrar" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditOtrasDeduccionesTxt"                 DataIndex="LIQUIDACIONES_D_OTRAS_DEDUCCIONES"                 LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Otras Deducciones" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditTotalDeduccionesTxt"                 DataIndex="LIQUIDACIONES_D_TOTAL_DEDUCCIONES"                 LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="Total Deducciones" ReadOnly="true" ></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditAFSocioTxt"                          DataIndex="LIQUIDACIONES_D_AF_SOCIO"                          LabelAlign="Right" AnchorHorizontal="100%" MsgTarget="Side" FieldLabel="A/F del Socio" ></ext:NumberField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="EditTotalesCalculosDeduccionesPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:FieldSet ID="EditTotalCalculosPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:Panel ID="EditTotalCalculos2Pnl" runat="server" Padding="5" LabelWidth="200" Frame="true" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="EditTotalCalculosFSTxt" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Total" AllowBlank="false" MsgTarget="Side" ReadOnly="true"></ext:NumberField>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:FieldSet>
                                                <ext:FieldSet ID="EditTotalDeduccionesPnl" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:Panel ID="EditTotalDeducciones2Pnl" runat="server" Padding="5" LabelWidth="200" Frame="true" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="EditTotalDeduccionesFSTxt" DataIndex="LIQUIDACIONES_D_TOTAL" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Total" MsgTarget="Side" ReadOnly="true" ></ext:NumberField>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:FieldSet>
                                            </Items>
                                        </ext:Panel>
                                        <ext:TextField   runat="server" ID="EditCreatedByTxt"        DataIndex="CREADO_POR"               LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="EditCreatedDateTxt"      DataIndex="FECHA_CREACION"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="EditModifiedByTxt"       DataIndex="MODIFICADO_POR"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="EditModificationDateTxt" DataIndex="FECHA_MODIFICACION"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
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
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true" Hidden="true" >
                            <Listeners>
                                <Click Handler="#{EditCreatedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.update();" />
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
