<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Socios.aspx.cs" Inherits="COCASJOL.WEBSITE.Socios.Socios" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Socios</title>
     <script type="text/javascript" src="../../resources/js/socios/socios.js" ></script>
</head>
<body>
  <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Slate">
        <Listeners>
            <DocumentReady Handler="PageX.setReferences()" />
        </Listeners>
    </ext:ResourceManager>

        <ext:KeyNav ID="KeyNav1" runat="server" Target="={document.body}" >
            <Home Handler="PageX.navHome();" />
            <PageUp Handler="PageX.navPrev();" />
            <PageDown Handler="PageX.navNext();" />
            <End Handler="PageX.navEnd();" />
        </ext:KeyNav>

    <aud:Auditoria runat="server" ID="AudWin" />

    <div>
        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Usuarios"
                    Icon="Group" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="SociosGridP" runat="server" Height="300"
                            Title="Socios" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="SociosSt" runat="server" SkipIdForNewRecords="false" AutoSave="false" WarningOnDirty="false" OnRefreshData="SociosSt_Reload">
                                    <Reader>
                                        <ext:JsonReader IDProperty="SOCIOS_ID">
                                            <Fields>
                                                <ext:RecordField Name="SOCIOS_ID" />
                                                <ext:RecordField Name="SOCIOS_PRIMER_NOMBRE" />
                                                <ext:RecordField Name="SOCIOS_SEGUNDO_NOMBRE" />
                                                <ext:RecordField Name="SOCIOS_PRIMER_APELLIDO" />
                                                <ext:RecordField Name="SOCIOS_SEGUNDO_APELLIDO" />
                                                <ext:RecordField Name="SOCIOS_RESIDENCIA" />
                                                <ext:RecordField Name="SOCIOS_ESTADO_CIVIL" />
                                                <ext:RecordField Name="SOCIOS_LUGAR_DE_NACIMIENTO" />
                                                <ext:RecordField Name="SOCIOS_FECHA_DE_NACIMIENTO" Type="Date"/>
                                                <ext:RecordField Name="SOCIOS_NIVEL_EDUCATIVO" />
                                                <ext:RecordField Name="SOCIOS_IDENTIDAD" />
                                                <ext:RecordField Name="SOCIOS_PROFESION" />
                                                <ext:RecordField Name="SOCIOS_RTN" />
                                                <ext:RecordField Name="SOCIOS_TELEFONO" />
                                                <ext:RecordField Name="SOCIOS_LUGAR_DE_EMISION" />
                                                <ext:RecordField Name="SOCIOS_FECHA_DE_EMISION" type="Date" />
                                                <ext:RecordField Name="SOCIOS_ESTATUS">
                                                    <Convert Handler="return value === 1;" />
                                                </ext:RecordField>
                                                <ext:RecordField Name="CREADO_POR" />
                                                <ext:RecordField Name="FECHA_CREACION" Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR" />
                                                <ext:RecordField Name="FECHA_MODIFICACION" Type="Date" />

                                                <ext:RecordField Name="GENERAL_CARNET_IHCAFE" ServerMapping="socios_generales.GENERAL_CARNET_IHCAFE"/>
                                                <ext:RecordField Name="GENERAL_ORGANIZACION_SECUNDARIA" ServerMapping="socios_generales.GENERAL_ORGANIZACION_SECUNDARIA" />
                                                <ext:RecordField Name="GENERAL_NUMERO_CARNET" ServerMapping="socios_generales.GENERAL_NUMERO_CARNET" />
                                                <ext:RecordField Name="GENERAL_EMPRESA_LABORA" ServerMapping="socios_generales.GENERAL_EMPRESA_LABORA" />
                                                <ext:RecordField Name="GENERAL_EMPRESA_CARGO" ServerMapping="socios_generales.GENERAL_EMPRESA_CARGO" />
                                                <ext:RecordField Name="GENERAL_EMPRESA_DIRECCION" ServerMapping="socios_generales.GENERAL_EMPRESA_DIRECCION" />
                                                <ext:RecordField Name="GENERAL_EMPRESA_TELEFONO" ServerMapping="socios_generales.GENERAL_EMPRESA_TELEFONO" />
                                                <ext:RecordField Name="GENERAL_SEGURO" ServerMapping="socios_generales.GENERAL_SEGURO" />
                                                <ext:RecordField Name="GENERAL_FECHA_ACEPTACION" type="Date" ServerMapping="socios_generales.GENERAL_FECHA_ACEPTACION" />
                                                <ext:RecordField Name="PRODUCCION_UBICACION_FINCA" ServerMapping="socios_produccion.PRODUCCION_UBICACION_FINCA" />
                                                <ext:RecordField Name="PRODUCCION_AREA" ServerMapping="socios_produccion.PRODUCCION_AREA" />
                                                <ext:RecordField Name="PRODUCCION_VARIEDAD" ServerMapping="socios_produccion.PRODUCCION_VARIEDAD" />
                                                <ext:RecordField Name="PRODUCCION_ALTURA" ServerMapping="socios_produccion.PRODUCCION_ALTURA" />
                                                <ext:RecordField Name="PRODUCCION_DISTANCIA" ServerMapping="socios_produccion.PRODUCCION_DISTANCIA" />
                                                <ext:RecordField Name="PRODUCCION_ANUAL" ServerMapping="socios_produccion.PRODUCCION_ANUAL" />
                                                <ext:RecordField Name="PRODUCCION_BENEFICIO_PROPIO" ServerMapping="socios_produccion.PRODUCCION_BENEFICIO_PROPIO" />
                                                <ext:RecordField Name="PRODUCCION_ANALISIS_SUELO" ServerMapping="socios_produccion.PRODUCCION_ANALISIS_SUELO" />
                                                <ext:RecordField Name="PRODUCCION_TIPO_INSUMOS" ServerMapping="socios_produccion.PRODUCCION_TIPO_INSUMOS" />
                                                <ext:RecordField Name="PRODUCCION_MANZANAS_CULTIVADAS" ServerMapping="socios_produccion.PRODUCCION_MANZANAS_CULTIVADAS" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                    <DirectEvents>
                                        <Update OnEvent="SociosSt_Update" Success="#{SociosSt}.reload()">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{SociosGridP}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="SOCIOS_ID"                         Mode="Raw"     Value="#{EditsocioIdTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_PRIMER_NOMBRE"              Mode="Raw"     Value="#{Edit1erNombreTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_SEGUNDO_NOMBRE"             Mode="Raw"     Value="#{Edit2doNombreTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_PRIMER_APELLIDO"            Mode="Raw"     Value="#{Edit1erApellidoTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_SEGUNDO_APELLIDO"           Mode="Raw"     Value="#{Edit2doApellidoTxt}.getValue()" />   
                                                <ext:Parameter Name="SOCIOS_RESIDENCIA"                 Mode="Raw"     Value="#{EditResidenciaTxt}.getValue()"  />                                    
                                                <ext:Parameter Name="SOCIOS_ESTADO_CIVIL"               Mode="Raw"     Value="#{EditEstadoCivilTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_LUGAR_DE_NACIMIENTO"        Mode="Raw"     Value="#{EditLugarNacimientoTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_FECHA_DE_NACIMIENTO"        Mode="Raw"     Value="#{EditFechaNacimientoTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_NIVEL_EDUCATIVO"            Mode="Raw"     Value="#{EditNivelEducativoTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_IDENTIDAD"                  Mode="Raw"     Value="#{EditIdentidadTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_PROFESION"                  Mode="Raw"     Value="#{EditSocioProfesionTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_RTN"                        Mode="Raw"     Value="#{EditSocioRTNTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_TELEFONO"                   Mode="Raw"     Value="#{EditSocioTelfono}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_LUGAR_DE_EMISION"           Mode="Raw"     Value="#{EditLugarEmisionTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_FECHA_DE_EMISION"           Mode="Raw"     Value="#{EditFechaEmisionTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_CARNET_IHCAFE"             Mode="Raw"     Value="#{EditCarnetIhcafeTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_ORGANIZACION_SECUNDARIA"   Mode="Raw"     Value="#{EditOrgSecundTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_NUMERO_CARNET"             Mode="Raw"     Value="#{EditNumCarnetTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_EMPRESA_LABORA"            Mode="Raw"     Value="#{EditEmpresaTxt}.getValue()" /> 
                                                <ext:Parameter Name="GENERAL_EMPRESA_CARGO"             Mode="Raw"     Value="#{EditCargoTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_EMPRESA_DIRECCION"         Mode="Raw"     Value="#{EditDireccionTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_EMPRESA_TELEFONO"          Mode="Raw"     Value="#{EditTelefonoTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_SEGURO"                    Mode="Raw"     Value="#{EditSeguroTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_FECHA_ACEPTACION"          Mode="Raw"     Value="#{EditAceptacionTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_UBICACION_FINCA"        Mode="Raw"     Value="#{EditUbicacionTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_AREA"                   Mode="Raw"     Value="#{EditAreaTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_VARIEDAD"               Mode="Raw"     Value="#{EditVariedadTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_ALTURA"                 Mode="Raw"     Value="#{EditAlturaTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_DISTANCIA"              Mode="Raw"     Value="#{EditDistanciaTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_ANUAL"                  Mode="Raw"     Value="#{EditAnualTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_BENEFICIO_PROPIO"       Mode="Raw"     Value="#{EditBeneficioTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_ANALISIS_SUELO"         Mode="Raw"     Value="#{EditAnalisisTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_TIPO_INSUMOS"           Mode="Raw"     Value="#{EditInsumosTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_MANZANAS_CULTIVADAS"    Mode="Raw"     Value="#{EditCantManzanasTxt}.getValue()" />
                                            </ExtraParams>                                                                      
                                        </Update>
                                    </DirectEvents>
                                    <DirectEvents>
                                        <Add OnEvent="SociosSt_Insert" Success="#{SociosSt}.reload()">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{SociosGridP}" />
                                            <ExtraParams>
                                                <ext:Parameter Name="SOCIOS_ID"                         Mode="Raw"     Value="#{AddSocioIdTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_PRIMER_NOMBRE"              Mode="Raw"     Value="#{Add1erNombreTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_SEGUNDO_NOMBRE"             Mode="Raw"     Value="#{Add2doNombreTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_PRIMER_APELLIDO"            Mode="Raw"     Value="#{Add1erApellidoTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_SEGUNDO_APELLIDO"           Mode="Raw"     Value="#{Add2doApellidoTxt}.getValue()" />   
                                                <ext:Parameter Name="SOCIOS_RESIDENCIA"                 Mode="Raw"     Value="#{AddResidenciaTxt}.getValue()"  />                                    
                                                <ext:Parameter Name="SOCIOS_ESTADO_CIVIL"               Mode="Raw"     Value="#{AddEstadoCivilTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_LUGAR_DE_NACIMIENTO"        Mode="Raw"     Value="#{AddLugarNacTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_FECHA_DE_NACIMIENTO"        Mode="Raw"     Value="#{AddFechaNacTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_NIVEL_EDUCATIVO"            Mode="Raw"     Value="#{AddNivelEducTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_IDENTIDAD"                  Mode="Raw"     Value="#{AddIdentidadTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_PROFESION"                  Mode="Raw"     Value="#{AddProfesionTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_RTN"                        Mode="Raw"     Value="#{AddRTNTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_TELEFONO"                   Mode="Raw"     Value="#{AddTelefonoTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_LUGAR_DE_EMISION"           Mode="Raw"     Value="#{AddLugarEmisionTxt}.getValue()" />
                                                <ext:Parameter Name="SOCIOS_FECHA_DE_EMISION"           Mode="Raw"     Value="#{AddFechaEmisionTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_CARNET_IHCAFE"             Mode="Raw"     Value="#{AddCarnetIHCAFETxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_ORGANIZACION_SECUNDARIA"   Mode="Raw"     Value="#{AddOrganizacionSecunTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_NUMERO_CARNET"             Mode="Raw"     Value="#{AddNumCarnetTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_EMPRESA_LABORA"            Mode="Raw"     Value="#{AddEmpresaTxt}.getValue()" /> 
                                                <ext:Parameter Name="GENERAL_EMPRESA_CARGO"             Mode="Raw"     Value="#{AddCargoTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_EMPRESA_DIRECCION"         Mode="Raw"     Value="#{AddDireccionTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_EMPRESA_TELEFONO"          Mode="Raw"     Value="#{AddEmpresaTelefonoTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_SEGURO"                    Mode="Raw"     Value="#{AddSeguroTxt}.getValue()" />
                                                <ext:Parameter Name="GENERAL_FECHA_ACEPTACION"          Mode="Raw"     Value="#{AddFechaAceptacionTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_UBICACION_FINCA"        Mode="Raw"     Value="#{AddUbicacionTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_AREA"                   Mode="Raw"     Value="#{AddAreaTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_VARIEDAD"               Mode="Raw"     Value="#{AddVariedadTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_ALTURA"                 Mode="Raw"     Value="#{AddAlturaTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_DISTANCIA"              Mode="Raw"     Value="#{AddDistanciaTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_ANUAL"                  Mode="Raw"     Value="#{AddAnualTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_BENEFICIO_PROPIO"       Mode="Raw"     Value="#{AddBeneficioTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_ANALISIS_SUELO"         Mode="Raw"     Value="#{AddAnalisisSueloTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_TIPO_INSUMOS"           Mode="Raw"     Value="#{AddTipoInsumosTxt}.getValue()" />
                                                <ext:Parameter Name="PRODUCCION_MANZANAS_CULTIVADAS"    Mode="Raw"     Value="#{AddManzanaTxt}.getValue()" />
                                            </ExtraParams> 
                                        </Add>
                                    </DirectEvents>
                                </ext:Store>
                            </Store>
                             <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="Button1" runat="server" Text="Agregar" Icon="Add">
                                            <Listeners>
                                                <Click Handler="PageX.add()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="Button3" runat="server" Text="Editar" Icon="ApplicationEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="Button2" runat="server" Text="Desactivar" Icon="StatusBusy">
                                            <Listeners>
                                                <Click Handler="CompanyX.DoConfirmDisable()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="Button4" runat="server" Text="Activar" Icon="StatusOnline">
                                            <Listeners>
                                                <Click Handler="CompanyX.DoConfirmEnable()" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:Button ID="ImportarSociosBtn" runat="server" Text="Importar Socios" Icon="PageExcel">
                                            <Listeners>
                                                <Click Handler="PageX.openExcelImport();" />
                                            </Listeners>
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
                                                        <ext:TextField ID="FilterSocioId" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="applyFilter(this);" Buffer="250" />                                                
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="Filter1erNombre" runat="server"  EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="applyFilter(this);" Buffer="250" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="Filter2doNombre" runat="server"  EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="applyFilter(this);" Buffer="250" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="Filter1erApellido" runat="server"  EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="applyFilter(this);" Buffer="250" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="Filter2doApellido" runat="server"  EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="applyFilter(this);" Buffer="250" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                 
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="FilterResidencia" runat="server"  EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="applyFilter(this);" Buffer="250" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                 
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="FilterIdentidad" runat="server"  EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="applyFilter(this);" Buffer="250" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="FilterRTN" runat="server"  EnableKeyEvents="true" Icon="Find">
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
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column         ColumnID="SociosId"         Header="ID"                 DataIndex="SOCIOS_ID" />
                                    <ext:Column         ColumnID="Socios1nombre"    Header="Primer Nombre"      DataIndex="SOCIOS_PRIMER_NOMBRE" Fixed="true" />
                                    <ext:Column         ColumnID="Socios2nombre"    Header="Segundo Nombre"     DataIndex="SOCIOS_SEGUNDO_NOMBRE" />
                                    <ext:Column         ColumnID="Socios1apellido"  Header="Primer Apellido"    DataIndex="SOCIOS_PRIMER_APELLIDO" />
                                    <ext:Column         ColumnID="Socios2apellido"  Header="Segundo Apellido"   DataIndex="SOCIOS_SEGUNDO_APELLIDO" />
                                    <ext:Column         ColumnID="Sociosres"        Header="Residencia"         DataIndex="SOCIOS_RESIDENCIA" Fixed="true" />
                                    <ext:Column         ColumnID="SociosIdentidad"  Header="No. Identidad"      DataIndex="SOCIOS_IDENTIDAD" />
                                    <ext:Column         ColumnID="SociosRTN"        Header="RTN"                DataIndex="SOCIOS_RTN" />
                                    <ext:CheckColumn    ColumnID="SociosEstatus"    Header="Activo"             DataIndex="SOCIOS_ESTATUS" Sortable="false" MenuDisabled="true"/>
                                    <ext:Column Width="28" DataIndex="SOCIOS_ID" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                        <Renderer Handler="return '';" />
                                    </ext:Column>
                                   </Columns>
                            </ColumnModel>
                            <selectionmodel>
                                <ext:rowselectionmodel id="rowselectionmodel1" runat="server" singleselect="true">
                                    <Listeners>
                                        <RowSelect Handler="#{StoreBeneficiarios}.reload();" />
                                    </Listeners>
                                </ext:rowselectionmodel>
                            </selectionmodel>
                            <KeyMap>
                                <ext:KeyBinding Ctrl="true" >
                                    <Keys>
                                        <ext:Key Code="INSERT" />
                                        <ext:Key Code="ENTER" />
                                        <ext:Key Code="DELETE" />
                                    </Keys>
                                    <Listeners>
                                        <Event Handler="PageX.gridKeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:KeyBinding>
                            </KeyMap>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" StoreID="SociosSt"
                                    runat="server"
                                    PageSize="5"
                                    DisplayInfo="true"
                                    DisplayMsg="Displaying socios {0} - {1} of {2}"
                                    EmptyMsg="No hay socios para mostrar"/>
                            </BottomBar>
                            <LoadMask ShowMask="true"/>
                            <Listeners>
                                <RowDblClick Handler="PageX.edit()"/>
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                 </ext:Panel>
             </Items>
          </ext:Viewport>
    </div>
    <div>
    </div>
    <div>
        <ext:Window ID="EditarSocioWin"            
            runat="server"
            Hidden="true"
            Icon="UserEdit"
            Title="Editar Socio"
            Width="550"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
               <Hide Handler="EditarSocioFormP.getForm().reset()" />
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
                <ext:FormPanel ID="EditarSocioFormP" 
                        runat="server" 
                        Title="Form Panel" 
                        Header="false" 
                        ButtonAlign="Right" 
                        MonitorValid="true" 
                        LabelAlign="Right">
                    <Items>
                        <ext:TabPanel ID="TabPanel11" runat="server">
                            <Items>
                                <ext:Panel ID="PanelPer" runat="server" Title="Datos Personales" Layout="AnchorLayout" AutoHeight="True" Icon="User" LabelWidth="150" >
                                    <Items>
                                        <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="EditsocioIdTxt"           DataIndex="SOCIOS_ID"                    FieldLabel="Id de Socio         "   AnchorHorizontal="90%" AllowBlank="false"   ReadOnly="true"></ext:TextField>
                                                <ext:TextField runat="server" ID="Edit1erNombreTxt"         DataIndex="SOCIOS_PRIMER_NOMBRE"         FieldLabel="Primer Nombre       "   AnchorHorizontal="90%" MaxLength="45" AllowBlank="false" ></ext:TextField>
                                                <ext:TextField runat="server" ID="Edit2doNombreTxt"         DataIndex="SOCIOS_SEGUNDO_NOMBRE"        FieldLabel="Segundo Nombre      "   AnchorHorizontal="90%" MaxLength="45" ></ext:TextField>
                                                <ext:TextField runat="server" ID="Edit1erApellidoTxt"       DataIndex="SOCIOS_PRIMER_APELLIDO"       FieldLabel="Primer Apellido     "   AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="Edit2doApellidoTxt"       DataIndex="SOCIOS_SEGUNDO_APELLIDO"      FieldLabel="Segundo Apellido    "   AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditResidenciaTxt"        DataIndex="SOCIOS_RESIDENCIA"            FieldLabel="Residencia          "   AnchorHorizontal="90%" MaxLength="100" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditEstadoCivilTxt"       DataIndex="SOCIOS_ESTADO_CIVIL"          FieldLabel="Estado Civil        "   AnchorHorizontal="90%" MaxLength="20" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditLugarNacimientoTxt"   DataIndex="SOCIOS_LUGAR_DE_NACIMIENTO"   FieldLabel="Lugar de Nacimiento "   AnchorHorizontal="90%" MaxLength="100" AllowBlank="false"   ></ext:TextField>
                                                <ext:DateField runat="server" ID="EditFechaNacimientoTxt"   DataIndex="SOCIOS_FECHA_DE_NACIMIENTO"   FieldLabel="Fecha de Nacimiento "   AnchorHorizontal="90%"                 AllowBlank="false" ></ext:DateField>
                                                <ext:TextField runat="server" ID="EditNivelEducativoTxt"    DataIndex="SOCIOS_NIVEL_EDUCATIVO"       FieldLabel="Nivel Educativo     "   AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditSocioProfesionTxt"    DataIndex="SOCIOS_PROFESION"             FieldLabel="Profesion           "   AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditSocioRTNTxt"          DataIndex="SOCIOS_RTN"                   FieldLabel="RTN                 "   AnchorHorizontal="90%" MaxLength="25" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditSocioTelfono"         DataIndex="SOCIOS_TELEFONO"              FieldLabel="Telefono            "   AnchorHorizontal="90%" MaxLength="20" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditIdentidadTxt"         DataIndex="SOCIOS_IDENTIDAD"             FieldLabel="Tarjeta de Identidad"   AnchorHorizontal="90%" MaxLength="20" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="EditLugarEmisionTxt"      DataIndex="SOCIOS_LUGAR_DE_EMISION"      FieldLabel="Lugar de Emision    "   AnchorHorizontal="90%" MaxLength="100" AllowBlank="false"   ></ext:TextField>
                                                <ext:DateField runat="server" ID="EditFechaEmisionTxt"      DataIndex="SOCIOS_FECHA_DE_EMISION"      FieldLabel="Fecha de Emision    "   AnchorHorizontal="90%" AllowBlank="false" ></ext:DateField>
                                                <ext:DateField runat="server" ID="EditAceptacionTxt"        DataIndex="GENERAL_FECHA_ACEPTACION"     FieldLabel="Fecha Aceptacion"  AnchorHorizontal="90%" AllowBlank="false"></ext:DateField>
                                            </Items>                                                               
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="PanelGrl" runat="server" Title="Datos Generales" Layout="AnchorLayout" AutoHeight="true" Icon="UserB">
                                    <Items>
                                        <ext:Panel ID="Panel2" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="EditCarnetIhcafeTxt"  DataIndex="GENERAL_CARNET_IHCAFE"            FieldLabel="Carnet IHCAFE"     AnchorHorizontal="90%" MaxLength="25" />
                                                <ext:TextField runat="server" ID="EditOrgSecundTxt"     DataIndex="GENERAL_ORGANIZACION_SECUNDARIA"  FieldLabel="Org. Secundaria"   AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="EditNumCarnetTxt"     DataIndex="GENERAL_NUMERO_CARNET"            FieldLabel="No. Carnet"        AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="EditEmpresaTxt"       DataIndex="GENERAL_EMPRESA_LABORA"           FieldLabel="Empresa"           AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="EditCargoTxt"         DataIndex="GENERAL_EMPRESA_CARGO"            FieldLabel="Cargo"             AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="EditDireccionTxt"     DataIndex="GENERAL_EMPRESA_DIRECCION"        FieldLabel="Direccion"         AnchorHorizontal="90%" MaxLength="100" />
                                                <ext:TextField runat="server" ID="EditTelefonoTxt"      DataIndex="GENERAL_EMPRESA_TELEFONO"         FieldLabel="Telefono"          AnchorHorizontal="90%" MaxLength="20" />
                                                <ext:TextField runat="server" ID="EditSeguroTxt"        DataIndex="GENERAL_SEGURO"                   FieldLabel="Carnet Seguro"     AnchorHorizontal="90%" MaxLength="45" />
                                            </Items>
                                         </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="PanelProd" runat="server" Title="Datos Produccion" Layout="AnchorLayout" AutoHeight="true" Icon="BuildingEdit" LabelWidth="120">
                                    <Items>
                                        <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="EditUbicacionTxt"    DataIndex="PRODUCCION_UBICACION_FINCA"       FieldLabel="Ubicacion Finca"     AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditAreaTxt"         DataIndex="PRODUCCION_AREA"                  FieldLabel="Area"                AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditVariedadTxt"     DataIndex="PRODUCCION_VARIEDAD"              FieldLabel="Variedad"            AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditAlturaTxt"       DataIndex="PRODUCCION_ALTURA"                FieldLabel="Altura"              AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditDistanciaTxt"    DataIndex="PRODUCCION_DISTANCIA"             FieldLabel="Distancia"           AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditAnualTxt"        DataIndex="PRODUCCION_ANUAL"                 FieldLabel="Produccion Anual"    AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditBeneficioTxt"    DataIndex="PRODUCCION_BENEFICIO_PROPIO"      FieldLabel="Beneficio Propio"    AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditAnalisisTxt"     DataIndex="PRODUCCION_ANALISIS_SUELO"        FieldLabel="Analisis Suelo"      AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditInsumosTxt"      DataIndex="PRODUCCION_TIPO_INSUMOS"          FieldLabel="Tipo Insumos"        AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="EditCantManzanasTxt" DataIndex="PRODUCCION_MANZANAS_CULTIVADAS"   FieldLabel="Manzanas Cultuvadas" AnchorHorizontal="90%" />
                                           </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="PanelBeneficiarios" runat="server" Title="Beneficiarios" Layout="AnchorLayout" AutoHeight="true" Icon="GroupAdd" LabelWidth="120">
                                    <Listeners>
                                        <Activate Handler="CompanyX.RefrescarBeneficiarios();" />
                                    </Listeners>
                                    <Items>
                                        <ext:Panel ID="Panel4" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:GridPanel ID="BeneficiariosGridP" Height="250" runat="server" AutoExpandColumn="BENEFICIARIO_NOMBRE" Title="Beneficiarios por socio" Header="false" SelectionMemory="Disabled">
                                                    
                                                    <Store>
                                                    <ext:Store ID="StoreBeneficiarios" runat="server" OnRefreshData="Beneficiarios_Refresh">
                                                        <Reader>
                                                            <ext:JsonReader IDProperty="SOCIO_ID">
                                                                <Fields>
                                                                    <ext:RecordField Name="SOCIO_ID" />
                                                                    <ext:RecordField Name="BENEFICIARIO_IDENTIFICACION" />
                                                                    <ext:RecordField Name="BENEFICIARIO_NOMBRE" />
                                                                    <ext:RecordField Name="BENEFICIARIO_PARENTEZCO" />
                                                                    <ext:RecordField Name="BENEFICIARIO_NACIMIENTO" Type="Date" />
                                                                    <ext:RecordField Name="BENEFICIARIO_LUGAR_NACIMIENTO" />
                                                                    <ext:RecordField Name="BENEFICIARIO_PORCENTAJE" Type="Float"/>
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
                                                            <ext:Column DataIndex="BENEFICIARIO_NOMBRE"             Header="Nombre"   />         
                                                            <ext:Column DataIndex="BENEFICIARIO_IDENTIFICACION"     Header="Identificacion" /> 
                                                            <ext:Column DataIndex="BENEFICIARIO_PARENTEZCO"         Header="Parentezco"     /> 
                                                            <ext:DateColumn DataIndex="BENEFICIARIO_NACIMIENTO"         Header="Fecha Nacimiento"/>
                                                            <ext:Column DataIndex="BENEFICIARIO_LUGAR_NACIMIENTO"   Header="Lugar Nacimiento" />
                                                            <ext:Column DataIndex="BENEFICIARIO_PORCENTAJE"         Header="Porcentaje"      />
                                                        </Columns>
                                                    </ColumnModel>
                                                    <SelectionModel>
                                                        <ext:RowSelectionModel ID="RowSelectionModelBeneficiarios">
                                                        </ext:RowSelectionModel>
                                                    </SelectionModel>
                                                    <TopBar>
                                                        <ext:Toolbar ID="ToolbarBeneficiarios" runat="server">
                                                            <Items>
                                                                <ext:Button ID="AgregarBeneficiarioBtn" runat="server" Text="Agregar" Icon="CogAdd">
                                                                    <Listeners>
                                                                        <Click Handler="PageX.addben()" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:Button ID="EliminarBeneficiarioBtn" runat="server" Text="Eliminar" Icon="CogDelete">
                                                                    <Listeners>
                                                                        <Click  Handler="PageX.removeben()" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:Button ID="EditarBeneficiarioBtn" runat="server" Text="Editar" Icon="CogEdit" >
                                                                    <Listeners>
                                                                        <Click Handler="PageX.editben()" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <BottomBar>
                                                        <ext:PagingToolbar ID="PaginacionBeneficiarios" runat="server" PageSize="5" StoreID="StoreBeneficiarios" />
                                                    </BottomBar>
                                                    <LoadMask ShowMask="true" />
                                                    <SaveMask ShowMask="true" />
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="EditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                            <Listeners>
                                <Click Handler="PageX.previous(); CompanyX.RefrescarBeneficiarios()" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                            <Listeners>
                                <Click Handler="PageX.next(); CompanyX.RefrescarBeneficiarios()" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="PageX.update()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    <div>
        <ext:Window ID="NuevoSocioWin"
            runat="server"
            Hidden="true"
            Icon="UserAdd"
            Title="Agregar Nuevo Socio"
            Width="550"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
               <Hide Handler="NuevoSocioForm.getForm().reset()" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="NuevoSocioForm" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:TabPanel ID="TabSocioAdd" runat="server" LabelAlign="Right">
                            <Items>
                                <ext:Panel ID="PanelPersonal" runat="server" Title="Datos Personales" Layout="AnchorLayout" AutoHeight="True" Icon="User" LabelWidth="150">
                                    <Items>
                                        <ext:Panel ID="Panel5" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="AddSocioIdTxt"            DataIndex="SOCIOS_ID"                   FieldLabel="Id de Socio"            AnchorHorizontal="90%" Text="Codigo" AllowBlank="false" Hidden="true"></ext:TextField>
                                                <ext:TextField runat="server" ID="Add1erNombreTxt"          DataIndex="SOCIOS_PRIMER_NOMBRE"        FieldLabel="Primer Nombre"          AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="Add2doNombreTxt"          DataIndex="SOCIOS_SEGUNDO_NOMBRE"       FieldLabel="Segundo Nombre"         AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="Add1erApellidoTxt"        DataIndex="SOCIOS_PRIMER_APELLIDO"      FieldLabel="Primer Apellido"        AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="Add2doApellidoTxt"        DataIndex="SOCIOS_SEGUNDO_APELLIDO"     FieldLabel="Segundo Apellido"       AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddResidenciaTxt"         DataIndex="SOCIOS_RESIDENCIA"           FieldLabel="Residencia"             AnchorHorizontal="90%" MaxLength="100" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddEstadoCivilTxt"        DataIndex="SOCIOS_ESTADO_CIVIL"         FieldLabel="Estado Civil"           AnchorHorizontal="90%" MaxLength="20" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddLugarNacTxt"           DataIndex="SOCIOS_LUGAR_DE_NACIMIENTO"  FieldLabel="Lugar de Nacimiento"    AnchorHorizontal="90%" MaxLength="100" AllowBlank="false"   ></ext:TextField>
                                                <ext:DateField runat="server" ID="AddFechaNacTxt"           DataIndex="SOCIOS_FECHA_DE_NACIMIENTO"  FieldLabel="Fecha de Nacimiento"    AnchorHorizontal="90%"                AllowBlank="false" ></ext:DateField>
                                                <ext:TextField runat="server" ID="AddNivelEducTxt"          DataIndex="SOCIOS_NIVEL_EDUCATIVO"      FieldLabel="Nivel Educativo"        AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddProfesionTxt"          DataIndex="SOCIOS_PROFESION"            FieldLabel="Profesion"              AnchorHorizontal="90%" MaxLength="45" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddRTNTxt"                DataIndex="SOCIOS_RTN"                  FieldLabel="RTN"                    AnchorHorizontal="90%" MaxLength="25" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddTelefonoTxt"           DataIndex="SOCIOS_TELEFONO"             FieldLabel="Telefono"               AnchorHorizontal="90%" MaxLength="20" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddIdentidadTxt"          DataIndex="SOCIOS_IDENTIDAD"            FieldLabel="Tarjeta de Identidad"   AnchorHorizontal="90%" MaxLength="20" AllowBlank="false"   ></ext:TextField>
                                                <ext:TextField runat="server" ID="AddLugarEmisionTxt"       DataIndex="SOCIOS_LUGAR_DE_EMISION"     FieldLabel="Lugar de Emision"       AnchorHorizontal="90%" MaxLength="100" AllowBlank="false"   ></ext:TextField>
                                                <ext:DateField runat="server" ID="AddFechaEmisionTxt"       DataIndex="SOCIOS_FECHA_DE_EMISION"     FieldLabel="Fecha de Emision"       AnchorHorizontal="90%" AllowBlank="false" ></ext:DateField>
                                                <ext:DateField runat="server" ID="AddFechaAceptacionTxt"    DataIndex="GENERAL_FECHA_ACEPTACION"    FieldLabel="Fecha de Aceptacion"   AnchorHorizontal="90%" AllowBlank="false"  />
                                            </Items>                                                               
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="PanelGeneral" runat="server" Title="Datos Generales" Layout="AnchorLayout" AutoHeight="true" Icon="UserB">
                                    <Items>
                                        <ext:Panel ID="Panel7" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="AddCarnetIHCAFETxt"       DataIndex="GENERAL_CARNET_IHCAFE"           FieldLabel="Carnet IHCAFE"      AnchorHorizontal="90%" MaxLength="25" />
                                                <ext:TextField runat="server" ID="AddOrganizacionSecunTxt"  DataIndex="GENERAL_ORGANIZACION_SECUNDARIA" FieldLabel="Org. Secundaria"    AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="AddNumCarnetTxt"          DataIndex="GENERAL_NUMERO_CARNET"           FieldLabel="No. Carnet"         AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="AddEmpresaTxt"            DataIndex="GENERAL_EMPRESA_LABORA"          FieldLabel="Empresa"            AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="AddCargoTxt"              DataIndex="GENERAL_EMPRESA_CARGO"           FieldLabel="Cargo"              AnchorHorizontal="90%" MaxLength="45" />
                                                <ext:TextField runat="server" ID="AddDireccionTxt"          DataIndex="GENERAL_EMPRESA_DIRECCION"       FieldLabel="Direccion"          AnchorHorizontal="90%" MaxLength="100" />
                                                <ext:TextField runat="server" ID="AddEmpresaTelefonoTxt"    DataIndex="GENERAL_EMPRESA_TELEFONO"        FieldLabel="Telefono"           AnchorHorizontal="90%" MaxLength="20" />
                                                <ext:TextField runat="server" ID="AddSeguroTxt"             DataIndex="GENERAL_SEGURO"                  FieldLabel="Carnet del Seguro"  AnchorHorizontal="90%" MaxLength="45" />
                                            </Items>
                                         </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="PanelProduccion" runat="server" Title="Datos Produccion" Layout="AnchorLayout" AutoHeight="true" Icon="BuildingEdit" LabelWidth="125"  >
                                    <Items>
                                        <ext:Panel ID="Panel9" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:TextField runat="server" ID="AddUbicacionTxt"      DataIndex="PRODUCCION_UBICACION_FINCA"      FieldLabel="Ubicacion Finca"     AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddAreaTxt"           DataIndex="PRODUCCION_AREA"                 FieldLabel="Area"                AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddVariedadTxt"       DataIndex="PRODUCCION_VARIEDAD"             FieldLabel="Variedad"            AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddAlturaTxt"         DataIndex="PRODUCCION_ALTURA"               FieldLabel="Altura"              AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddDistanciaTxt"      DataIndex="PRODUCCION_DISTANCIA"            FieldLabel="Distancia"           AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddAnualTxt"          DataIndex="PRODUCCION_ANUAL"                FieldLabel="Produccion Anual"    AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddBeneficioTxt"      DataIndex="PRODUCCION_BENEFICIO_PROPIO"     FieldLabel="Beneficio Propio"    AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddAnalisisSueloTxt"  DataIndex="PRODUCCION_ANALISIS_SUELO"       FieldLabel="Analisis Suelo"      AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddTipoInsumosTxt"    DataIndex="PRODUCCION_TIPO_INSUMOS"         FieldLabel="Tipo Insumos"        AnchorHorizontal="90%" />
                                                <ext:TextField runat="server" ID="AddManzanaTxt"        DataIndex="PRODUCCION_MANZANAS_CULTIVADAS"  FieldLabel="Manzanas Cultuvadas" AnchorHorizontal="90%" />
                                           </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddGuardarBtn" runat="server" Text="Crear" Icon="Add" FormBind="true">
                            <Listeners>
                                <Click Handler="PageX.insert()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    <div>
        <ext:Window ID="NuevoBeneficiarioWin"
            runat="server"
            Hidden="true"
            Icon="UserAdd"
            Title="Agregar Beneficiario"
            Width="550"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true" 
            X="15" Y="35">
            <Listeners>
                <Hide Handler="NuevoBeneficiarioForm.getForm().reset(); CompanyX.Error100();" />
            </Listeners>
            <Items>
                <ext:FormPanel runat="server" ID="NuevoBeneficiarioForm" Title="Form Panel" Handler="false" ButtonAlign="Right" MonitorValid="true" Header="false">
                    <Items>
                        <ext:Panel ID="PanelBeneficiario" runat="server" Title="Datos Beneficiario Nuevo" Layout="AnchorLayout" AutoHeight="True" Icon="User" LabelWidth="150">
                            <Items>
                                <ext:Panel ID="Panel8" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                    <Items>
                                        <ext:TextField runat="server" ID="AddBenefID"            DataIndex="BENEFICIARIO_IDENTIFICACION"           FieldLabel="Identificacion"    AnchorHorizontal="90%" AllowBlank="false" ></ext:TextField>
                                        <ext:TextField runat="server" ID="AddBenefNombre"        DataIndex="BENEFICIARIO_NOMBRE"                   FieldLabel="Nombre"            AnchorHorizontal="90%" AllowBlank="false" ></ext:TextField>
                                        <ext:TextField runat="server" ID="AddBenefParentezco"    DataIndex="BENEFICIARIO_PARENTEZCO"               FieldLabel="Parentezco"        AnchorHorizontal="90%" AllowBlank="false" ></ext:TextField>
                                        <ext:DateField runat="server" ID="AddBenefFechaNacimiento"    DataIndex="BENEFICIARIO_NACIMIENTO"               FieldLabel="Fecha Nacimiento"  AnchorHorizontal="90%" AllowBlank="false"></ext:DateField>
                                        <ext:TextField runat="server" ID="AddBenefLugarNac"      DataIndex="BENEFICIARIO_LUGAR_NACIMIENTO"         FieldLabel="Lugar Nacimiento"  AnchorHorizontal="90%" ></ext:TextField>
                                        <ext:NumberField runat="server" ID="AddBenefPorcentaje"    DataIndex="BENEFICIARIO_PORCENTAJE"               FieldLabel="Porcentaje"        AnchorHorizontal="90%" AllowBlank="false" AllowDecimals="false" MaxValue="100" MinValue="0" ></ext:NumberField>
                                    </Items>                                                               
                                </ext:Panel>
                            </Items>
                          </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddBenefBtn" runat="server" Text="Crear" Icon="Add" FormBind="true">
                            <Listeners>
                                <Click Handler="PageX.insertben()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>
    </div>
    <div>
        <ext:Window ID="EditarBeneficiarioWin"
            runat="server"
            Hidden="true"
            Icon="UserAdd"
            Title="Editar Beneficiario"
            Width="550"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true" 
            X="15" Y="35">
            <Listeners>
                <Hide Handler="EditarBeneficiarioForm.getForm().reset()" />
            </Listeners>
            <Items>
                <ext:FormPanel runat="server" ID="EditarBeneficiarioForm" Title="Form Panel" Header="false" Handler="false" ButtonAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:Panel ID="PanelEditarBeneficiario" runat="server" Title="Datos Beneficiario" Layout="AnchorLayout" AutoHeight="True" Icon="User" LabelWidth="150">
                            <Items>
                                <ext:Panel ID="Panel10" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                    <Items>
                                        <ext:TextField runat="server" ID="EditBenefId"            DataIndex="BENEFICIARIO_IDENTIFICACION"           FieldLabel="Identificacion"    AnchorHorizontal="90%" AllowBlank="false"  ReadOnly="true"></ext:TextField>
                                        <ext:TextField runat="server" ID="EditBenefNombre"        DataIndex="BENEFICIARIO_NOMBRE"                   FieldLabel="Nombre"            AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                        <ext:TextField runat="server" ID="EditBenefParentezco"    DataIndex="BENEFICIARIO_PARENTEZCO"               FieldLabel="Parentezco"        AnchorHorizontal="90%" AllowBlank="false"></ext:TextField>
                                        <ext:DateField runat="server" ID="EditBenefNacimiento"    DataIndex="BENEFICIARIO_NACIMIENTO"               FieldLabel="Fecha Nacimiento"  AnchorHorizontal="90%" AllowBlank="false" ></ext:DateField>
                                        <ext:TextField runat="server" ID="EditBenLugarNac"      DataIndex="BENEFICIARIO_LUGAR_NACIMIENTO"         FieldLabel="Lugar Nacimiento"  AnchorHorizontal="90%" ></ext:TextField>
                                        <ext:NumberField runat="server" ID="EditBenefPorcentaje"    DataIndex="BENEFICIARIO_PORCENTAJE"               FieldLabel="Porcentaje"        AnchorHorizontal="90%" AllowBlank="false" AllowDecimals="false" MaxValue="100" MinValue="0" ></ext:NumberField>
                                    </Items>                                                               
                                </ext:Panel>
                            </Items>
                          </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button5" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="PageX.updateben()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
         </ext:Window>

         
         <ext:Window ID="ImportarSociosWin" runat="server" Hidden="true" Icon="CogAdd" Title="Importación de Socios"
            Width="600" Layout="FormLayout" AutoHeight="True" Resizable="false" Shadow="None" Modal="true" >
            <Items>         
            <ext:FormPanel runat="server" ID="BasicForm" Title="Form Panel" Header="false" Handler="false" ButtonAlign="Right" MonitorValid="true">
                <Items>
                    <ext:Panel ID="Panel6" runat="server" Frame="false" Padding="5" Layout="FormLayout" Border="false">
                        <Items>
                            <ext:FileUploadField ID="FileUploadField1" runat="server" EmptyText="Seleccione Excel" FieldLabel="Excel" Icon="PageAttach" MsgTarget="Side" AnchorHorizontal="90%" LabelAlign="Right" AllowBlank="false" ButtonText="" />
                        </Items>
                    </ext:Panel>
                </Items>
                <Buttons>
                    <ext:Button ID="GetTemplateBtn" runat="server" Text="Obtener Plantilla" Icon="ArrowDown">
                        <DirectEvents>
                            <Click OnEvent="GetImportTemplateClick" >
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="ExcelUploadBtn" runat="server" Text="Subir" Icon="ArrowUp">
                        <DirectEvents>
                            <Click OnEvent="UploadClick" >
                                <EventMask Target="CustomTarget" CustomTarget="ImportarSociosWin" ShowMask="true" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>