<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudesDePrestamos.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Prestamos.SolicitudesDePrestamos" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitudes de Prestamo</title>
    <link rel="Stylesheet" type="text/css" href="../../resources/css/ComboBox_Grid.css" />
    <script type="text/javascript" src="../../resources/js/prestamos/solicitudesDePrestamos.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="RM1" runat="server">
            <Listeners>
                <DocumentReady Handler="SolicitudX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <ext:KeyNav ID="KeyNav1" runat="server" Target="={document.body}" >
            <Home Handler="SolicitudX.navHome();" />
            <PageUp Handler="SolicitudX.navPrev();" />
            <PageDown Handler="SolicitudX.navNext();" />
            <End Handler="SolicitudX.navEnd();" />
        </ext:KeyNav>

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
                TypeName="COCASJOL.LOGIC.Prestamos.TiposPrestamoLogic"
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
        <ext:Viewport ID="view1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="panel1" runat="server" Frame="false" Header="false" Title="Solicitudes" Icon="Money" Layout="FitLayout">
                    <Items>
                        <ext:GridPanel ID="SolicitudesGriP" runat="server" Height="300" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <KeyMap>
                                <ext:KeyBinding Ctrl="true" >
                                    <Keys>
                                        <ext:Key Code="INSERT" />
                                        <ext:Key Code="ENTER" />
                                    </Keys>
                                    <Listeners>
                                        <Event Handler="SolicitudX.gridKeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:KeyBinding>
                            </KeyMap>
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
                                                <ext:RecordField Name="SOLICITUD_ESTADO" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                    <DirectEvents>
                                        <Add OnEvent="SolicitudesSt_Insert" Success="#{SolicitudesSt}.reload()">
                                            <EventMask ShowMask="true" Target="CustomTarget" />
                                        </Add>
                                        <Update OnEvent="SolicitudesSt_Update" Success="#{SolicitudesSt}.reload()">
                                            <EventMask ShowMask="true" Target="CustomTarget" />
                                        </Update>
                                    </DirectEvents>
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
                                                <Click Handler="SolicitudX.edit()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="DeleteBtn" runat="server"  Text="Denegar" Icon="MoneyDelete">
                                            <Listeners>
                                                <Click Handler="DirectX.DoConfirmDisable()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="ApproveBtn" runat="server" Text="Aprobar" Icon="MoneyAdd">
                                            <Listeners>
                                                <Click Handler="DirectX.DoConfirmEnable()" />
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
                                                            <ext:TextField ID="FilterSolicitudEstado" runat="server" EnableKeyEvents="true" Icon="Find">
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
                                    <ext:Column ColumnID="SolicitudEstado" Header="Estado" DataIndex="SOLICITUD_ESTADO" />
                                    <ext:DateColumn ColumnID="SolicitudPlazo" Header="Plazo" DataIndex="SOLICITUDES_PLAZO" />
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
                                <RowDblClick Handler="SolicitudX.edit()" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>

        <ext:Window ID="EditarSolicitudWin"
                runat="server"
                hidden="true"
                Icon="Money"
                width="550"
                layout="FormLayout"
                autoheight="true" Title="Editar Solicitud"
                resizable="false"
                shadow="None"
                modal="true"
                x="10" Y="30">
                <Listeners>
                    <Hide Handler="EditarSolicitudFormP.getForm().reset()" />
                </Listeners>
                <TopBar>
                    <ext:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <ext:Button ID="ImprimirBtn" runat="server" Text="Versión para Imprimir" Icon="Printer">
                                <Listeners>
                                    <Click Handler="SolicitudX.print();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel ID="EditarSolicitudFormP" runat="server" Header="false" ButtonAlign="Right" MonitorValid="true" LabelAlign="Right">
                        <Items>
                             <ext:TabPanel ID="tabpanel1" runat="server">
                                <Items>
                                     <ext:Panel ID="PanelSocio" runat="server" Title="Datos de Socio" Layout="AnchorLayout" AutoHeight="true" Icon="UserComment" LabelWidth="150">
                                        <Items>
                                            <ext:Panel ID="Panel5" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                                <Items>
                                                    <ext:TextField ID="EditIdSolicitud"     ReadOnly="true" runat="server" FieldLabel="Solicitud Id" DataIndex="SOLICITUDES_ID" AnchorHorizontal="90%"  AllowBlank="false" />                                                    
                                                    <ext:TextField ID="EditSocioid"         ReadOnly="true" runat="server" FieldLabel="ID Socio" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditNombre"    ReadOnly="true" runat="server" FieldLabel="Nombre" AnchorHorizontal="90%" />
                                                    <ext:NumberField    runat="server" ID="EditMontoTxt"     DataIndex="SOLICITUDES_MONTO"           AnchorHorizontal="90%"      FieldLabel="Monto                     " AllowBlank="false" MaxLength="11" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                    <ext:DateField      runat="server" ID="EditPlazo"     DataIndex="SOLICITUDES_PLAZO"           AnchorHorizontal="90%"      FieldLabel="Fecha de Plazo            " AllowBlank="false" />
                                                    <ext:TextField      runat="server" ID="EditPagoTxt"      DataIndex="SOLICITUDES_PAGO"            AnchorHorizontal="90%"      FieldLabel="Tipo de Pago              " AllowBlank="false" MaxLength="45" />
                                                    <ext:TextField      runat="server" ID="EditDestinoTxt"   DataIndex="SOLICITUDES_DESTINO"         AnchorHorizontal="90%"      FieldLabel="Destino                   " AllowBlank="false" MaxLength="45" />
                                                    <ext:ComboBox runat="server"    ID="EditCmbPrestamo"     DataIndex="PRESTAMOS_ID"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tipo de Prestamo" AllowBlank="false" MsgTarget="Side"
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
                                                            <Select Handler="DirectX.SeleccionarInteres();" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:NumberField    runat="server" ID="EditInteres"   DataIndex="SOLICITUDES_INTERES"         AnchorHorizontal="90%"      FieldLabel="Interes                   " AllowBlank="false" MinValue="0" MaxValue="100" AllowDecimals="false" />
                                                    <ext:TextField      runat="server" ID="EditCargoTxt"     DataIndex="SOLICITUDES_CARGO"           AnchorHorizontal="90%"      FieldLabel="Cargo en la cooperativa   " MaxLength="45" />                                            
                                                    <ext:NumberField    runat="server" ID="EditPromedio"  DataIndex="SOLICITUDES_PROMEDIO3"       AnchorHorizontal="90%"      FieldLabel="Promedio produccion (3 años)" MinValue="0" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                    <ext:NumberField    runat="server" ID="EditProd"   DataIndex="SOLICITUDES_PRODUCCIONACT"   AnchorHorizontal="90%"      FieldLabel="Promedio Actual           " MinValue="0" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                    <ext:TextField      runat="server" ID="EditNorteTxt"     DataIndex="SOLICITUDES_NORTE"           AnchorHorizontal="90%"      FieldLabel="Colindancias Norte" MaxLength="45" />
                                                    <ext:TextField      runat="server" ID="EditSurTxt"       DataIndex="SOLICITUDES_SUR"             AnchorHorizontal="90%"      FieldLabel="Colindancias Sur" MaxLength="45" />
                                                    <ext:TextField      runat="server" ID="EditEsteTxt"      DataIndex="SOLICITUDES_ESTE"            AnchorHorizontal="90%"      FieldLabel="Colindancias Este" MaxLength="45" />
                                                    <ext:TextField      runat="server" ID="EditOesteTxt"     DataIndex="SOLICITUDES_OESTE"           AnchorHorizontal="90%"      FieldLabel="Colindancias Oeste" MaxLength="45" />
                                                </Items>
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="PanelSolicitud" runat="server" Title="Datos de Solicitud" Layout="AnchorLayout" AutoHeight="true" Icon="User" LabelWidth="150" >
                                        <Items>
                                            <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                                <Items>
                                                    <ext:TextField ID="EditIdentidad"       ReadOnly="true" runat="server" FieldLabel="Identidad" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditLugarNcax"       ReadOnly="true" runat="server" FieldLabel="Lugar de Nacimiento" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditCarnetIHCAFE"    ReadOnly="true" runat="server" FieldLabel="Carnet IHCAFE" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditRTN"             ReadOnly="true" runat="server" FieldLabel="RTN" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditEstadoCivil"     ReadOnly="true" runat="server" FieldLabel="Estado Civil" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditProfesion"       ReadOnly="true" runat="server" FieldLabel="Profesion" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditTelefono"        ReadOnly="true" runat="server" FieldLabel="Telefono" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditResidencia"      ReadOnly="true" runat="server" FieldLabel="Residencia" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditManzanas"        ReadOnly="true" runat="server" FieldLabel="Manzanas Cultivadas" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditUbicacion"       ReadOnly="true" runat="server" FieldLabel="Ubicacion de la finca" AnchorHorizontal="90%" />
                                                    <ext:Checkbox runat="server" ID="EditCarro"     DataIndex="SOLICITUDES_VEHICULO" FieldLabel="Acceso en Vehiculo?" LabelWidth="100" />
                                                    <ext:Checkbox runat="server" ID="EditAgua"         DataIndex="SOLICITUDES_AGUA" FieldLabel="Posee Agua?" />
                                                    <ext:Checkbox runat="server" ID="EditLuz"         DataIndex="SOLICITUDES_ENEE" FieldLabel="Posee Energia Electrica?" LabelWidth="100" /> 
                                                    <ext:Checkbox runat="server" ID="EditCasa"         DataIndex="SOLICITUDES_CASA" FieldLabel="Posee Casa Propia?" />                                
                                                    <ext:Checkbox       runat="server" ID="EditBeneficio"    DataIndex="SOLICITUDES_BENEFICIO" FieldLabel="Posee Beneficio?" />
                                                    <ext:TextField      runat="server" ID="EditOtrosTxt"     DataIndex="SOLICITUD_OTROSCULTIVOS" FieldLabel="Otros Cultivos?" MaxLength="45" AnchorHorizontal="90%" />
                                                    <ext:ComboBox ID="EditCalifCmb"
                                                        runat="server" DataIndex="SOLICITUD_CALIFICACION"
                                                        Editable="false" FieldLabel="Calificacion de Socio"         
                                                        EmptyText="Seleccione Calificacion..." AnchorHorizontal="90%">
                                                        <Items>
                                                            <ext:ListItem Text="Muy Bueno" Value="Muy Bueno" />
                                                            <ext:ListItem Text="Bueno" Value="Bueno" />
                                                            <ext:ListItem Text="Regular" Value="Regular" />
                                                        </Items>
                                                    </ext:ComboBox>
                                                </Items>
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>

                                    <ext:Panel ID="PanelRefer" runat="server" Title="Referencias" Layout="AnchorLayout" AutoHeight="true" Icon="GroupAdd">
                                         <Listeners>
                                            <Activate Handler="DirectX.refreshTabReferencias();" />
                                        </Listeners>
                                        <Items>
                                            <ext:Panel ID="Panel4" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                                <Items>
                                                    <ext:GridPanel ID="ReferenciasGridP" Height="250" runat="server" AutoExpandColumn="REFERENCIAS_NOMBRE" Title="Referencias p Socio" Header="false" SelectionMemory="Disabled">
                                                        <Store>
                                                            <ext:Store ID="StoreReferencias" runat="server" OnRefreshData="Referencias_Refresh">
                                                                <Reader>
                                                                    <ext:JsonReader IDProperty="REFERENCIAS_ID" >
                                                                        <Fields>
                                                                            <ext:RecordField Name="SOLICITUDES_ID" />
                                                                            <ext:RecordField Name="REFERENCIAS_ID" />
                                                                            <ext:RecordField Name="REFERENCIAS_NOMBRE" />
                                                                            <ext:RecordField Name="REFERENCIAS_TELEFONO" />
                                                                            <ext:RecordField Name="REFERENCIAS_TIPO" />
                                                                        </Fields>
                                                                    </ext:JsonReader>
                                                                </Reader>
                                                                <Listeners>
                                                                    <LoadException Handler="Ext.Msg.alert('Ha ocurrido un problema cargando los beneficiarios!', e.message || response.statusText);" />
                                                                </Listeners>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel>
                                                            <Columns>
                                                                <ext:Column DataIndex="SOLICITUDES_ID"          Header="ID SOLICITUD"   />         
                                                                <ext:Column DataIndex="REFERENCIAS_ID"          Header="Identificacion" /> 
                                                                <ext:Column DataIndex="REFERENCIAS_NOMBRE"      Header="Nombre"     /> 
                                                                <ext:Column DataIndex="REFERENCIAS_TELEFONO"    Header="Telefono" />
                                                                <ext:Column DataIndex="REFERENCIAS_TIPO"        Header="Tipo"      />
                                                            </Columns>
                                                        </ColumnModel>
                                                        
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModelReferencias" runat="server" SingleSelect="true">
                                                                
                                                            </ext:RowSelectionModel>
                                                        </SelectionModel>
                                                        <TopBar>
                                                            <ext:Toolbar ID="ToolbarReferencias" runat="server">
                                                                <Items>
                                                                    <ext:Button ID="AgregarReferenciasBtn" runat="server" Text="Agregar" Icon="CogAdd">
                                                                        <Listeners>
                                                                            <Click Handler="SolicitudX.addRef()" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="EliminarReferenciasBtn" runat="server" Text="Eliminar" Icon="CogDelete">
                                                                        <Listeners>
                                                                            <click  handler="SolicitudX.removeRef()" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="EditarReferenciaBtn" runat="server" Text="Editar" Icon="CogEdit" >
                                                                        <Listeners>
                                                                            <Click Handler="SolicitudX.editRef()" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:Toolbar>
                                                        </TopBar>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PaginacionBeneficiarios" runat="server" PageSize="5" StoreID="StoreReferencias" />
                                                        </BottomBar>
                                                        <LoadMask ShowMask="true" />
                                                        <SaveMask ShowMask="true" />
                                                        <Listeners>
                                                            <RowDblClick Handler="SolicitudX.editRef()" />
                                                        </Listeners>
                                                    </ext:GridPanel>
                                                </Items>
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                   
                                    <ext:Panel ID="PanelAvales" runat="server" Title="Avales" Layout="AnchorLayout" AutoHeight="true" Icon="GroupKey">
                                        <Listeners>
                                            <Activate Handler="DirectX.refreshTabAvales();" />
                                        </Listeners>
                                        <Items>
                                            <ext:Panel ID="Panel10" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                                <Items>
                                                    <ext:GridPanel ID="AvalesGridP" Height="250" runat="server" Title="Avales por Solicitud" Header="false" SelectionMemory="Disabled">
                                                        <Store>
                                                            <ext:Store ID="StoreAvales" runat="server" OnRefreshData="Avales_Refresh">
                                                                <Reader>
                                                                    <ext:JsonReader IDProperty="SOCIOS_ID" >
                                                                        <Fields>
                                                                            <ext:RecordField Name="SOCIOS_ID" />
                                                                            <ext:RecordField Name="SOCIOS_PRIMER_NOMBRE" ServerMapping="socios.SOCIOS_PRIMER_NOMBRE" />
                                                                            <ext:RecordField Name="SOCIOS_PRIMER_APELLIDO" ServerMapping="socios.SOCIOS_PRIMER_APELLIDO" />
                                                                            <ext:RecordField Name="SOLICITUDES_ID" />
                                                                            <ext:RecordField Name="AVALES_CALIFICACION" />
                                                                        </Fields>
                                                                    </ext:JsonReader>
                                                                </Reader>
                                                                <Listeners>
                                                                    <LoadException Handler="Ext.Msg.alert('Ha ocurrido un problema cargando los Avales de la solicitud!', e.message || response.statusText);" />
                                                                </Listeners>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel>
                                                            <Columns>
                                                                <ext:Column DataIndex="SOLICITUDES_ID"          Header="ID SOLICITUD"   />         
                                                                <ext:Column DataIndex="SOCIOS_ID"          Header="Codigo Socio" /> 
                                                                <ext:Column DataIndex="SOCIOS_PRIMER_NOMBRE"      Header="Nombre"     /> 
                                                                <ext:Column DataIndex="SOCIOS_PRIMER_APELLIDO"    Header="Apellido" />
                                                                <ext:Column DataIndex="AVALES_CALIFICACION"        Header="Calificacion"      />
                                                            </Columns>
                                                        </ColumnModel>
                                                        
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModelAvales" runat="server" SingleSelect="true">
                                                                
                                                            </ext:RowSelectionModel>
                                                        </SelectionModel>
                                                        <TopBar>
                                                            <ext:Toolbar ID="ToolbarAvales" runat="server">
                                                                <Items>
                                                                    <ext:Button ID="AgregarAvalesBtn" runat="server" Text="Agregar" Icon="CogAdd">
                                                                        <Listeners>
                                                                            <Click Handler="SolicitudX.addAval()" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="EliminarAvalesBtn" runat="server" Text="Eliminar" Icon="CogDelete">
                                                                        <Listeners>
                                                                            <click Handler="SolicitudX.removeAval()" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="EditarAvalesBtn" runat="server" Text="Editar" Icon="CogEdit" >
                                                                        <listeners>
                                                                            <click Handler="SolicitudX.editAval()" />
                                                                        </listeners>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:Toolbar>
                                                        </TopBar>
                                                        <BottomBar>
                                                            <ext:PagingToolbar ID="PaginacionAvales" runat="server" PageSize="5" StoreID="StoreAvales" />
                                                        </BottomBar>
                                                        <LoadMask ShowMask="true" />
                                                        <SaveMask ShowMask="true" />
                                                        <Listeners>
                                                            <RowDblClick Handler="SolicitudX.editAval()"/>
                                                        </Listeners>
                                                    </ext:GridPanel>
                                                </Items>
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
                            <Click Handler="SolicitudX.previous()" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="EditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                        <Listeners>
                            <Click Handler="SolicitudX.next()" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="EditSaveBtn" runat="server" Text="Guardar" Icon="Disk">
                        <Listeners>
                            <Click Handler="SolicitudX.update()" />
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
                                <ext:Panel ID="AddPanelDatos" runat="server" Title="Datos Solicitud" Layout="AnchorLayout" AutoHeight="true" Icon="PageEdit" LabelWidth="200">
                                    <Items>
                                        <ext:Panel ID="Panel2" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:ComboBox ID="cbSociosId" runat="server" EmptyText="Seleccione Socio"  TypeAhead="true"
                                                    ForceSelection="true" StoreID="ComboBoxSt" Mode="Local" DisplayField="SOCIOS_ID" FieldLabel="Codigo Socio" 
                                                    ValueField="SOCIOS_ID" MinChars="2" ListWidth="350" PageSize="10" ItemSelector="tr.list-item" AnchorHorizontal="90%">
                                                    <Template ID="Template1" runat="server" Width="200">
                                                        <Html>
					                                        <tpl for=".">
						                                        <tpl if="[xindex] == 1">
							                                        <table class="cbStates-list">
								                                        <tr>
									                                        <th>Id Socio</th>
									                                        <th>Primer Nombre</th>
                                                                            <th>Primer Apellido</th>
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
                                                        <Select Handler="this.triggers[0].show();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:NumberField    runat="server" ID="AddMontoTxt"     DataIndex="SOLICITUDES_MONTO"           AnchorHorizontal="90%"      FieldLabel="Monto                     " AllowBlank="false" MaxLength="14" MinValue="0" />
                                                <ext:DateField      runat="server" ID="AddPlazoTxt"     DataIndex="SOLICITUDES_PLAZO"           AnchorHorizontal="90%"      FieldLabel="Fecha de Plazo            " AllowBlank="false">
                                                    <RemoteValidation OnValidation="AddFecha_Change" />
                                                </ext:DateField>
                                                <ext:TextField      runat="server" ID="AddPagoTxt"      DataIndex="SOLICITUDES_PAGO"            AnchorHorizontal="90%"      FieldLabel="Tipo de Pago              " AllowBlank="false" MaxLength="45" />
                                                <ext:TextField      runat="server" ID="AddDestinoTxt"   DataIndex="SOLICITUDES_DESTINO"         AnchorHorizontal="90%"      FieldLabel="Destino                   " AllowBlank="false" MaxLength="45" />
                                                <ext:ComboBox runat="server"    ID="AddTipoDeProdIdCmb"     DataIndex="PRESTAMOS_ID"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tipo de Prestamo" AllowBlank="false" MsgTarget="Side"
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
                                                        <Select Handler="DirectX.SeleccionarInteres();" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:NumberField    runat="server" ID="AddInteresTxt"   DataIndex="SOLICITUDES_INTERES"         AnchorHorizontal="90%"      FieldLabel="Interes                   " AllowBlank="false" MinValue="0" MaxValue="100" AllowDecimals="false" />
                                                <ext:TextField      runat="server" ID="AddCargoTxt"     DataIndex="SOLICITUDES_CARGO"           AnchorHorizontal="90%"      FieldLabel="Cargo en la cooperativa   " MaxLength="45" />
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="AddPanelGrl" runat="server" Title="Datos Generales" Layout="AnchorLayout" AutoHeight="true" Icon="UserEdit" LabelWidth="200">
                                    <Items>
                                        <ext:Panel ID="P1" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:NumberField    runat="server" ID="AddPromedioTxt"  DataIndex="SOLICITUDES_PROMEDIO3"       AnchorHorizontal="90%"      FieldLabel="Promedio produccion (3 años)" MinValue="0" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                <ext:NumberField    runat="server" ID="AddPromActTxt"   DataIndex="SOLICITUDES_PRODUCCIONACT"   AnchorHorizontal="90%"      FieldLabel="Promedio Actual           " MinValue="0" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                <ext:TextField      runat="server" ID="AddNorteTxt"     DataIndex="SOLICITUDES_NORTE"           AnchorHorizontal="90%"      FieldLabel="Colindancias Norte" MaxLength="45" />
                                                <ext:TextField      runat="server" ID="AddSurTxt"       DataIndex="SOLICITUDES_SUR"             AnchorHorizontal="90%"      FieldLabel="Colindancias Sur" MaxLength="45" />
                                                <ext:TextField      runat="server" ID="AddEsteTxt"      DataIndex="SOLICITUDES_ESTE"            AnchorHorizontal="90%"      FieldLabel="Colindancias Este" MaxLength="45" />
                                                <ext:TextField      runat="server" ID="AddOesteTxt"     DataIndex="SOLICITUDES_OESTE"           AnchorHorizontal="90%"      FieldLabel="Colindancias Oeste" MaxLength="45" />
                                                
                                                <ext:Checkbox runat="server" ID="AddVehiculo"     DataIndex="SOLICITUDES_VEHICULO" FieldLabel="Acceso en Vehiculo?" LabelWidth="100" />
                                                <ext:Checkbox runat="server" ID="AddAgua"         DataIndex="SOLICITUDES_AGUA" FieldLabel="Posee Agua?" />
                                                    
                                                <ext:Checkbox runat="server" ID="AddENEE"         DataIndex="SOLICITUDES_ENEE" FieldLabel="Posee Energia Electrica?" LabelWidth="100" /> 
                                                <ext:Checkbox runat="server" ID="AddCasa"         DataIndex="SOLICITUDES_CASA" FieldLabel="Posee Casa Propia?" />                                
                                                
                                                <ext:Checkbox       runat="server" ID="AddBeneficio"    DataIndex="SOLICITUDES_BENEFICIO" FieldLabel="Posee Beneficio?" />
                                                <ext:TextField      runat="server" ID="AddOtrosTxt"     DataIndex="SOLICITUD_OTROSCULTIVOS" FieldLabel="Otros Cultivos?" MaxLength="45" AnchorHorizontal="90%" />
                                                <ext:ComboBox ID="AddCalificacion"
                                                    runat="server" 
                                                    Editable="false" FieldLabel="Calificacion de Socio"         
                                                    EmptyText="Seleccione Calificacion..." AnchorHorizontal="90%">
                                                    <Items>
                                                        <ext:ListItem Text="Muy Bueno" Value="Muy Bueno" />
                                                        <ext:ListItem Text="Bueno" Value="Bueno" />
                                                        <ext:ListItem Text="Regular" Value="Regular" />
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddGuardarBtn" runat="server" Text="Crear" Icon="Add" formBind="true">
                            <Listeners>
                                <Click Handler="SolicitudX.insert()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>

         <ext:Window ID="NuevaReferenciaWin"
            runat= "server"
            Hidden="true"
            ICon="UserAdd"
            Title="Agregar Referencia"
            Width="550"
            Layout="FormLayout"
            Autoheight="True"
            resizable="false"
            Shadow="None"
            modal="true"
            x="15" Y="35" >
            <Listeners>
                <Hide Handler="NuevaReferenciaForm.getForm().reset()" />
           </Listeners>   
            <Items>
                <ext:FormPanel runat="server" ID="NuevaReferenciaForm" Title="Form" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:Panel ID="PanelRefencia" runat="server" Title="Datos Referencia Nueva" Layout="AnchorLayout" AutoHeight="true" Icon="UserAdd" LabelWidth="150">
                            <Items>
                                <ext:Panel ID="Panel" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                    <Items>
                                        <ext:TextField runat="server" ID="AddReferenciaId" FieldLabel="No. de Identificacion" AnchorHorizontal="90%" AllowBlank="false" /> 
                                        <ext:TextField runat="server" ID="AddReferenciaNombre" FieldLabel="Nombre" AnchorHorizontal="90%" AllowBlank="false" />
                                        <ext:TextField runat="server" ID="AddReferenciaTel" FieldLabel="Telefono" AnchorHorizontal="90%" AllowBlank="false" />
                                        <ext:ComboBox ID="AddReferenciaTipo"
                                            runat="server" 
                                            Editable="false" FieldLabel="Tipo de Referencia"         
                                            EmptyText="Seleccione un tipo..." AnchorHorizontal="90%">
                                            <Items>
                                                <ext:ListItem Text="Personal" Value="Personal" />
                                                <ext:ListItem Text="Comercial" Value="Comercial" />
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddReferenciaBtn" runat="server" Text="Crear" Icon="Add" FormBind="true">
                            <Listeners>
                                <Click Handler="SolicitudX.insertRef();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>

         <ext:Window ID="EditarReferenciaWin"
            runat= "server"
            Hidden="true"
            ICon="UserEdit"
            Title="Editar Referencia"
            Width="550"
            Layout="FormLayout"
            Autoheight="True"
            resizable="false"
            Shadow="None"
            modal="true"
            x="15" Y="35" >
            <Listeners>
                <Hide Handler="EditarReferenciaForm.getForm().reset()" />
           </Listeners>  
            <Items>
                <ext:FormPanel runat="server" ID="EditarReferenciaForm" Title="Form" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:Panel ID="Panel7" runat="server" Title="Datos Referencia" Layout="AnchorLayout" AutoHeight="true" Icon="UserAdd" LabelWidth="150">
                            <Items>
                                <ext:Panel ID="Panel8" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                    <Items>
                                        <ext:TextField runat="server" ID="EditIdRef" DataIndex="REFERENCIAS_ID" FieldLabel="No. de Identificacion" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" /> 
                                        <ext:TextField runat="server" ID="EditNombreRef" DataIndex="REFERENCIAS_NOMBRE" FieldLabel="Nombre" AnchorHorizontal="90%" AllowBlank="false" />
                                        <ext:TextField runat="server" ID="EditTelRef" DataIndex="REFERENCIAS_TELEFONO" FieldLabel="Telefono" AnchorHorizontal="90%" AllowBlank="false" />
                                        <ext:ComboBox ID="EditTipoRef"
                                            runat="server" DataIndex="REFERENCIAS_TIPO"
                                            Editable="false" FieldLabel="Tipo de Referencia"         
                                            EmptyText="Seleccione un tipo..." AnchorHorizontal="90%">
                                            <Items>
                                                <ext:ListItem Text="Personal" Value="Personal" />
                                                <ext:ListItem Text="Comercial" Value="Comercial" />
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="EditarRefBtn" runat="server" Text="Editar" Icon="UserEdit" FormBind="true">
                            <Listeners>
                                <Click Handler="DirectX.ActualizarReferencias()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>

         <ext:Window ID="NuevoAvalWin"
            runat= "server"
            Hidden="true"
            ICon="UserAdd"
            Title="Agregar Aval"
            Width="550"
            Layout="FormLayout"
            Autoheight="True"
            resizable="false"
            Shadow="None"
            modal="true"
            x="15" Y="35" >
            <Listeners>
                <Hide Handler="NuevoAvalForm.getForm().reset()" />
           </Listeners>   
            <Items>
                <ext:FormPanel runat="server" ID="NuevoAvalForm" Title="Form" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:Panel ID="Panel6" runat="server" Title="Datos Aval" Layout="AnchorLayout" AutoHeight="true" Icon="UserAdd" LabelWidth="150">
                            <Items>
                                <ext:Panel ID="Panel9" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                    <Items>
                                        <ext:ComboBox ID="CodigoAval" runat="server" EmptyText="Seleccione Socio"  TypeAhead="true"
                                            ForceSelection="true" StoreID="ComboBoxSt" Mode="Local" DisplayField="SOCIOS_ID" FieldLabel="Codigo Socio" 
                                            ValueField="SOCIOS_ID" MinChars="2" ListWidth="350" PageSize="10" ItemSelector="tr.list-item" AnchorHorizontal="90%">
                                            <Template ID="Template2" runat="server" Width="200">
                                                <Html>
					                                <tpl for=".">
						                                <tpl if="[xindex] == 1">
							                                <table class="cbStates-list">
								                                <tr>
									                                <th>Id Socio</th>
									                                <th>Primer Nombre</th>
                                                                    <th>Primer Apellido</th>
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
                                                <Select Handler="this.triggers[0].show(); "/>
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="AvalCalificacion"
                                            runat="server" 
                                            Editable="false" FieldLabel="Calificacion del aval"         
                                            EmptyText="Seleccione Calificacion..." AnchorHorizontal="90%">
                                            <Items>
                                                <ext:ListItem Text="Muy Bueno" Value="Muy Bueno" />
                                                <ext:ListItem Text="Bueno" Value="Bueno" />
                                                <ext:ListItem Text="Regular" Value="Regular" />
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button1" runat="server" Text="Crear" Icon="Add" FormBind="true">
                            <Listeners>
                                <Click Handler="SolicitudX.insertAval();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>

         <ext:Window ID="EditarAvalWin"
            runat= "server"
            Hidden="true"
            ICon="UserEdit"
            Title="Editar Aval"
            Width="550"
            Layout="FormLayout"
            Autoheight="True"
            resizable="false"
            Shadow="None"
            modal="true"
            x="15" Y="35" >
            <Listeners>
                <Hide Handler="EditarAvalForm.getForm().reset()" />
           </Listeners>  
            <Items>
                <ext:FormPanel runat="server" ID="EditarAvalForm" Title="Form" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:Panel ID="Panel11" runat="server" Title="Datos Aval" Layout="AnchorLayout" AutoHeight="true" Icon="UserAdd" LabelWidth="150">
                            <Items>
                                <ext:Panel ID="Panel12" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                    <Items>
                                        <ext:TextField runat="server" ID="EditAvalId" DataIndex = "SOCIOS_ID" FieldLabel="Id. Socio" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" /> 
                                        <ext:TextField runat="server" ID="EditAvalNombre" FieldLabel="Nombre Socio" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" /> 
                                        <ext:TextField runat="server" ID="EditAvalAportaciones" FieldLabel="Aportaciones" AnchorHorizontal="90%" ReadOnly="true" /> 
                                        <ext:TextField runat="server" ID="EditAvalAntiguedad" FieldLabel="Antigüedad" AnchorHorizontal="90%" ReadOnly="true" />
                                        <ext:ComboBox ID="EditarCalificacionAval"
                                            runat="server" DataIndex="AVALES_CALIFICACION" 
                                            Editable="false" FieldLabel="Calificacion del Aval"         
                                            EmptyText="Seleccione Calificacion..." AnchorHorizontal="90%">
                                            <Items>
                                                <ext:ListItem Text="Muy Bueno" Value="Muy Bueno" />
                                                <ext:ListItem Text="Bueno" Value="Bueno" />
                                                <ext:ListItem Text="Regular" Value="Regular" />
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button2" runat="server" Text="Editar" Icon="UserEdit" FormBind="true">
                            <Listeners>
                                <Click Handler="DirectX.ActualizarAvales()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>
    </form>
</body>
</html>
