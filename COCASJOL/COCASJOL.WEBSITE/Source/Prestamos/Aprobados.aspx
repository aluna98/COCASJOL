<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aprobados.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Prestamos.Aprobados" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prestamos Aprobados</title>
    <link rel="Stylesheet" type="text/css" href="../../resources/css/ComboBox_Grid.css" />
    <script type="text/javascript" src="../../resources/js/prestamos/PrestamosAprobados.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="RM1" runat="server">
            <Listeners>
                <DocumentReady Handler="SolicitudX.setReferences();" />
            </Listeners>
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
                                                <ext:RecordField Name="SOLICITUD_ESTADO" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <TopBar>
                                <ext:Toolbar ID="ToolBar1" runat="server">
                                    <Items>
                                        <ext:Button ID="EditBtn" runat="server" Text="Visualizar" Icon="Find">
                                            <Listeners>
                                                <Click Handler="SolicitudX.edit()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="ApproveBtn" runat="server" Text="Finalizar" Icon="MoneyAdd">
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
                                                            <ext:TextField ID="FilterPrestamoSocioId" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterPrestamoId" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterPrestamoNombre" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                    <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterPrestamoMonto" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                <Listeners>
                                                                    <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                                </Listeners>
                                                            </ext:TextField>
                                                        </Component>
                                                    </ext:HeaderColumn>

                                                 <ext:HeaderColumn Cls="x-small-editor">
                                                        <Component>
                                                            <ext:TextField ID="FilterPrestamoEstado" runat="server" EnableKeyEvents="true" Icon="Find">
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
                autoheight="true" Title="Visualizar Prestamo"
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
                                     <ext:Panel ID="PanelSocio" runat="server" Title="Datos de Socio" Layout="AnchorLayout" AutoHeight="true" Icon="UserComment" LabelWidth="150">
                                        <Items>
                                            <ext:Panel ID="Panel5" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                                <Items>
                                                    <ext:TextField ID="EditIdSolicitud"     ReadOnly="true" runat="server" FieldLabel="Solicitud Id" DataIndex="SOLICITUDES_ID" AnchorHorizontal="90%"  AllowBlank="false" />                                                    
                                                    <ext:TextField ID="EditSocioid"         ReadOnly="true" runat="server" FieldLabel="ID Socio" AnchorHorizontal="90%" />
                                                    <ext:TextField ID="EditNombre"    ReadOnly="true" runat="server" FieldLabel="Nombre" AnchorHorizontal="90%" />
                                                    <ext:NumberField    runat="server" ReadOnly="true" ID="EditMontoTxt"     DataIndex="SOLICITUDES_MONTO"           AnchorHorizontal="90%"      FieldLabel="Monto                     " AllowBlank="false" MaxLength="11" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                    <ext:DateField      runat="server" ReadOnly="true" ID="EditPlazo"     DataIndex="SOLICITUDES_PLAZO"           AnchorHorizontal="90%"      FieldLabel="Fecha de Plazo            " AllowBlank="false" />
                                                    <ext:TextField      runat="server" ReadOnly="true" ID="EditPagoTxt"      DataIndex="SOLICITUDES_PAGO"            AnchorHorizontal="90%"      FieldLabel="Tipo de Pago              " AllowBlank="false" MaxLength="45" />
                                                    <ext:TextField      runat="server" ReadOnly="true" ID="EditDestinoTxt"   DataIndex="SOLICITUDES_DESTINO"         AnchorHorizontal="90%"      FieldLabel="Destino                   " AllowBlank="false" MaxLength="45" />
                                                    <ext:ComboBox runat="server"    ID="EditCmbPrestamo"     DataIndex="PRESTAMOS_ID"  ReadOnly="true"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tipo de Prestamo" AllowBlank="false" MsgTarget="Side"
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
                                                    <ext:NumberField    runat="server" ID="EditInteres"   ReadOnly="true" DataIndex="SOLICITUDES_INTERES"         AnchorHorizontal="90%"      FieldLabel="Interes                   " AllowBlank="false" MinValue="0" MaxValue="100" AllowDecimals="false" />
                                                    <ext:TextField      runat="server" ID="EditCargoTxt"  ReadOnly="true"   DataIndex="SOLICITUDES_CARGO"           AnchorHorizontal="90%"      FieldLabel="Cargo en la cooperativa   " MaxLength="45" />                                            
                                                    <ext:NumberField    runat="server" ID="EditPromedio"  ReadOnly="true" DataIndex="SOLICITUDES_PROMEDIO3"       AnchorHorizontal="90%"      FieldLabel="Promedio produccion (3 años)" MinValue="0" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                    <ext:NumberField    runat="server" ID="EditProd"      ReadOnly="true"  DataIndex="SOLICITUDES_PRODUCCIONACT"   AnchorHorizontal="90%"      FieldLabel="Promedio Actual           " MinValue="0" AllowDecimals="true" DecimalPrecision="3" DecimalSeparator="." />
                                                    <ext:TextField      runat="server" ID="EditNorteTxt"  ReadOnly="true"   DataIndex="SOLICITUDES_NORTE"           AnchorHorizontal="90%"      FieldLabel="Colindancias Norte" MaxLength="45" />
                                                    <ext:TextField      runat="server" ID="EditSurTxt"    ReadOnly="true"   DataIndex="SOLICITUDES_SUR"             AnchorHorizontal="90%"      FieldLabel="Colindancias Sur" MaxLength="45" />
                                                    <ext:TextField      runat="server" ID="EditEsteTxt"   ReadOnly="true"   DataIndex="SOLICITUDES_ESTE"            AnchorHorizontal="90%"      FieldLabel="Colindancias Este" MaxLength="45" />
                                                    <ext:TextField      runat="server" ID="EditOesteTxt"  ReadOnly="true"   DataIndex="SOLICITUDES_OESTE"           AnchorHorizontal="90%"      FieldLabel="Colindancias Oeste" MaxLength="45" />
                                                </Items>
                                            </ext:Panel>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="PanelSolicitud" runat="server" Title="Datos de Prestamo" Layout="AnchorLayout" AutoHeight="true" Icon="User" LabelWidth="150" >
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
                                                    <ext:Checkbox runat="server" ID="EditCarro" Selectable="false" DataIndex="SOLICITUDES_VEHICULO" FieldLabel="Acceso en Vehiculo?" LabelWidth="100" />
                                                    <ext:Checkbox runat="server" ID="EditAgua"  Selectable="false"   DataIndex="SOLICITUDES_AGUA" FieldLabel="Posee Agua?" />
                                                    <ext:Checkbox runat="server" ID="EditLuz"   Selectable="false"   DataIndex="SOLICITUDES_ENEE" FieldLabel="Posee Energia Electrica?" LabelWidth="100" /> 
                                                    <ext:Checkbox runat="server" ID="EditCasa"  Selectable="false"      DataIndex="SOLICITUDES_CASA" FieldLabel="Posee Casa Propia?" />                                
                                                    <ext:Checkbox       runat="server" ID="EditBeneficio" Selectable="false"   DataIndex="SOLICITUDES_BENEFICIO" FieldLabel="Posee Beneficio?" />
                                                    <ext:TextField      runat="server" ID="EditOtrosTxt" ReadOnly="true"    DataIndex="SOLICITUD_OTROSCULTIVOS" FieldLabel="Otros Cultivos?" MaxLength="45" AnchorHorizontal="90%" />
                                                    <ext:ComboBox ID="EditCalifCmb" ReadOnly="true"
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
                                                                    <ext:Button ID="EditarReferenciaBtn" runat="server" Text="Visualizar" Icon="Find" >
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
                                                                    <ext:Button ID="EditarAvalesBtn" runat="server" Text="Visualizar" Icon="Find" >
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
                    <ext:Button ID="EditSaveBtn" runat="server" Text="Salir" Icon="Cancel">
                        <Listeners>
                            <Click Handler="DirectX.CerrarPrestamo()" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
         </ext:Window>

         <ext:Window ID="EditarReferenciaWin"
            runat= "server"
            Hidden="true"
            ICon="UserEdit"
            Title="Visualizar Referencia"
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
                                        <ext:TextField runat="server" ID="EditNombreRef" DataIndex="REFERENCIAS_NOMBRE" FieldLabel="Nombre" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" />
                                        <ext:TextField runat="server" ID="EditTelRef" DataIndex="REFERENCIAS_TELEFONO" FieldLabel="Telefono" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" />
                                        <ext:ComboBox ID="EditTipoRef" ReadOnly="true"
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
                        <ext:Button ID="EditarRefBtn" runat="server" Text="Salir" Icon="Cancel" FormBind="true">
                            <Listeners>
                                <Click Handler="DirectX.CerrarReferencias()" />
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
            Title="Visualizar Aval"
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
                                        <ext:ComboBox ID="EditarCalificacionAval" ReadOnly="true"
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
                        <ext:Button ID="Button2" runat="server" Text="Salir" Icon="Cancel" FormBind="true">
                            <Listeners>
                                <Click Handler="DirectX.CerrarAvales()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>
    </form>
</body>
</html>
