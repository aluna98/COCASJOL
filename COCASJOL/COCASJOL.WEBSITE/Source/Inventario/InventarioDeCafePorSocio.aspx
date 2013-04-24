<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventarioDeCafePorSocio.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Inventario.InventarioDeCafePorSocio" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inventario de Café por Socio</title>
    <script type="text/javascript" src="../../resources/js/inventario/inventarioDeCafePorSocio.js" ></script>
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

        <asp:ObjectDataSource ID="InventarioCafeDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.InventarioDeCafeLogic"
                SelectMethod="GetInventarioDeCafeDeSocios" onselecting="InventarioCafeDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="SOCIOS_ID"                    Type="String"   ControlID="f_SOCIOS_ID"                    PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="SOCIOS_NOMBRE_COMPLETO"       Type="String"   ControlID="f_SOCIOS_NOMBRE_COMPLETO"       PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_ID"      Type="Int32"    ControlID="f_CLASIFICACIONES_CAFE_ID"      PropertyName="Text" DefaultValue="0" />
                    <asp:ControlParameter Name="INVENTARIO_ENTRADAS_CANTIDAD" Type="Decimal"  ControlID="f_INVENTARIO_ENTRADAS_CANTIDAD" PropertyName="Text" DefaultValue="-1"/>
                    <asp:ControlParameter Name="INVENTARIO_SALIDAS_SALDO"     Type="Decimal"  ControlID="f_INVENTARIO_SALIDAS_SALDO"     PropertyName="Text" DefaultValue="-1"/>
                    <asp:ControlParameter Name="CREADO_POR"                   Type="String"   ControlID="nullHdn"                        PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"               Type="DateTime" ControlID="nullHdn"                        PropertyName="Text" DefaultValue="" />
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
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Inventario de Café por Socio" Icon="Basket" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="InventarioCafeGridP" runat="server" AutoExpandColumn="SOCIOS_NOMBRE_COMPLETO" Height="300"
                            Title="Inventario de Café" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <KeyMap>
                                <ext:KeyBinding Ctrl="true" >
                                    <Keys>
                                        <ext:Key Code="ENTER" />
                                    </Keys>
                                    <Listeners>
                                        <Event Handler="PageX.gridKeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:KeyBinding>
                            </KeyMap>
                            <Store>
                                <ext:Store ID="InventarioCafeSt" runat="server" DataSourceID="InventarioCafeDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="SOCIOS_ID"                    />
                                                <ext:RecordField Name="SOCIOS_NOMBRE_COMPLETO"       />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_ID"      />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE"  />
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
                                    <ext:Column DataIndex="SOCIOS_ID"                    Header="Id de Socio" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="SOCIOS_NOMBRE_COMPLETO"       Header="Nombre Completo de Socio" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="CLASIFICACIONES_CAFE_NOMBRE"  Header="Clasificación de Café" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="INVENTARIO_ENTRADAS_CANTIDAD" Header="Cantidad Total" Sortable="true">
                                        <Renderer Handler="if (#{p_QUINTALES}.checked == true) return value / 100; else return value;" />
                                    </ext:Column>
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
                                        <ext:Button ID="EditarBtn" runat="server" Text="Ver" Icon="BrickEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Checkbox runat="server" ID="p_QUINTALES" LabelAlign="Right" FieldLabel="Mostrar en Quintales" ></ext:Checkbox>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:Button ID="Export_PDFBtn" runat="server" Text="Exportar PDF" Icon="PageWhiteAcrobat">
                                            <DirectEvents>
                                                <Click OnEvent="Export_PDFBtn_Click" IsUpload="true"></Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="Export_ExcelBtn" runat="server" Text="Exportar Excel" Icon="PageExcel">
                                            <DirectEvents>
                                                <Click OnEvent="Export_ExcelBtn_Click" IsUpload="true"></Click>
                                            </DirectEvents>
                                        </ext:Button>
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
                                                        <ext:TextField ID="f_SOCIOS_NOMBRE_COMPLETO" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="5">
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

        <ext:Window ID="EditarInventarioCafeWin"
            runat="server"
            Hidden="true"
            Icon="BrickEdit"
            Title="Inventario de Café por Socio"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="EditarTipoFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Title="Inventario de Café de Socio" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Items>
                                <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false" AnchorHorizontal="100%">
                                    <Items>
                                        <ext:TextField   runat="server" ID="EditSociosIdTxt"           DataIndex="SOCIOS_ID"                   LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Socio" AllowBlank="false" ReadOnly="true">
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Id de Socio" Html="El Id de Socio es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:TextField   runat="server" ID="EditNombreTxt"             DataIndex="SOCIOS_NOMBRE_COMPLETO"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:TextField   runat="server" ID="EditTipoDeCafeIdtxt"       DataIndex="CLASIFICACIONES_CAFE_NOMBRE" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tipo de Café" AllowBlank="false" ReadOnly="true" MsgTarget="Side">
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Tipo de Café" Html="El tipo de café es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:NumberField runat="server" ID="EditInventarioCantidadTxt" DataIndex="INVENTARIO_ENTRADAS_CANTIDAD" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Cantidad Total (lb)" AllowBlank="false" ReadOnly="true" MsgTarget="Side">
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Cantidad" Html="La cantidad de inventario es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:NumberField>
                                        <ext:NumberField runat="server" ID="EditInventarioSalidasTxt"  DataIndex="INVENTARIO_SALIDAS_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo de Salidas" AllowBlank="false" ReadOnly="true" MsgTarget="Side">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Title="Salidas" Html="El saldo de salidas de inventario es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:NumberField>
                                        <ext:TextField runat="server"   ID="EditCreatedByTxt"          DataIndex="CREADO_POR"                  LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                        <ext:TextField runat="server"   ID="EditCreationDateTxt"       DataIndex="FECHA_CREACION"              LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
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