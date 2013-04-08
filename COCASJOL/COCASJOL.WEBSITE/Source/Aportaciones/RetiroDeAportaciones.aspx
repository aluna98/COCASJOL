<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetiroDeAportaciones.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Aportaciones.RetiroDeAportaciones" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retiro de Aportaciones</title>

    <link rel="Stylesheet" type="text/css" href="../../resources/css/ComboBox_Grid.css" />
    <script type="text/javascript" src="../../resources/js/aportaciones/retiroAportaciones.js" ></script>
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

        <asp:ObjectDataSource ID="RetiroAportacionesDs" runat="server"
                TypeName="COCASJOL.LOGIC.Aportaciones.RetiroAportacionLogic"
                SelectMethod="GetRetirosDeAportaciones" onselecting="RetiroAportacionesDs_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="RETIROS_AP_ID"                       Type="Int32"    ControlID="f_RETIROS_AP_ID"             PropertyName="Text" DefaultValue="0" />
                    <asp:ControlParameter Name="SOCIOS_ID"                           Type="String"   ControlID="f_SOCIOS_ID"                 PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="RETIROS_AP_FECHA"                    Type="DateTime" ControlID="nullHdn"                        PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_DESDE"                         Type="DateTime" ControlID="f_DATE_FROM"                 PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_HASTA"                         Type="DateTime" ControlID="f_DATE_TO"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="RETIROS_AP_ORDINARIA"                Type="Decimal"  ControlID="nullHdn"                     PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="RETIROS_AP_EXTRAORDINARIA"           Type="Decimal"  ControlID="nullHdn"                     PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="RETIROS_AP_CAPITALIZACION_RETENCION" Type="Decimal"  ControlID="nullHdn"                     PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="RETIROS_AP_INTERESES_S_APORTACION"   Type="Decimal"  ControlID="nullHdn"                     PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="RETIROS_AP_EXCEDENTE_PERIODO"        Type="Decimal"  ControlID="nullHdn"                     PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="RETIROS_AP_TOTAL_RETIRADO"           Type="Decimal"  ControlID="f_RETIROS_AP_TOTAL_RETIRADO" PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="CREADO_POR"                          Type="String"   ControlID="nullHdn"                     PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"                      Type="DateTime" ControlID="nullHdn"                     PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"                      Type="String"   ControlID="nullHdn"                     PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION"                  Type="DateTime" ControlID="nullHdn"                     PropertyName="Text" DefaultValue="" />
                </SelectParameters>
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

        <asp:ObjectDataSource ID="AportacionesDS" runat="server"
                TypeName="COCASJOL.LOGIC.Aportaciones.AportacionLogic"
                SelectMethod="GetAportaciones" >
        </asp:ObjectDataSource>

        <ext:Store ID="AportacionesSt" runat="server" DataSourceID="AportacionesDS" AutoSave="true" SkipIdForNewRecords="false" >
            <Reader>
                <ext:JsonReader IDProperty="SOCIOS_ID">
                    <Fields>
                        <ext:RecordField Name="SOCIOS_ID"                                   />
                        <ext:RecordField Name="SOCIOS_NOMBRE_COMPLETO"                      />
                        <ext:RecordField Name="APORTACIONES_ORDINARIA_SALDO"                />
                        <ext:RecordField Name="APORTACIONES_EXTRAORDINARIA_SALDO"           />
                        <ext:RecordField Name="APORTACIONES_CAPITALIZACION_RETENCION_SALDO" />
                        <ext:RecordField Name="APORTACIONES_INTERESES_S_APORTACION_SALDO"   />
                        <ext:RecordField Name="APORTACIONES_EXCEDENTE_PERIODO_SALDO"        />
                        <ext:RecordField Name="APORTACIONES_SALDO_TOTAL"                    />
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
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Aportaciones" Icon="Coins" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="RetiroAportacionesGridP" runat="server" AutoExpandColumn="SOCIOS_ID" Height="300"
                            Title="Retiro de Aportaciones" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
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
                                <ext:Store ID="RetiroAportacionesSt" runat="server" DataSourceID="RetiroAportacionesDs" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="RETIROS_AP_ID">
                                            <Fields>
                                                <ext:RecordField Name="RETIROS_AP_ID"                       />
                                                <ext:RecordField Name="SOCIOS_ID"                           />
                                                <ext:RecordField Name="RETIROS_AP_FECHA"                    Type="Date" />
                                                <ext:RecordField Name="FECHA_DESDE"                         />
                                                <ext:RecordField Name="FECHA_HASTA"                         />
                                                <ext:RecordField Name="RETIROS_AP_ORDINARIA"                />
                                                <ext:RecordField Name="RETIROS_AP_EXTRAORDINARIA"           />
                                                <ext:RecordField Name="RETIROS_AP_CAPITALIZACION_RETENCION" />
                                                <ext:RecordField Name="RETIROS_AP_INTERESES_S_APORTACION"   />
                                                <ext:RecordField Name="RETIROS_AP_EXCEDENTE_PERIODO"        />
                                                <ext:RecordField Name="RETIROS_AP_TOTAL_RETIRADO"           />
                                                <ext:RecordField Name="CREADO_POR"                          />
                                                <ext:RecordField Name="FECHA_CREACION"                      Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"                      />
                                                <ext:RecordField Name="FECHA_MODIFICACION"                  Type="Date" />
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
                                    <ext:Column       DataIndex="RETIROS_AP_ID"             Header="Numero de Retiro" Sortable="true"></ext:Column>
                                    <ext:Column       DataIndex="SOCIOS_ID"                 Header="Id de Socio" Sortable="true"></ext:Column>
                                    <ext:DateColumn   DataIndex="RETIROS_AP_FECHA"          Header="Fecha" Sortable="true"></ext:DateColumn>
                                    <ext:NumberColumn DataIndex="RETIROS_AP_TOTAL_RETIRADO" Header="Total Retirado" Sortable="true"></ext:NumberColumn>

                                    <ext:Column DataIndex="RETIROS_AP_FECHA" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
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
                                                        <ext:NumberField ID="f_RETIROS_AP_ID" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="5">
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
                                                        <ext:DropDownField ID="f_RETIROS_AP_FECHA" AllowBlur="true" runat="server" Editable="false"
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
                                                        <ext:NumberField ID="f_RETIROS_AP_TOTAL_RETIRADO" runat="server" EnableKeyEvents="true" Icon="Find">
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
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="RetiroAportacionesSt" />
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

        <ext:Window ID="AgregarRetiroAportacionWin"
            runat="server"
            Hidden="true"
            Icon="CoinsAdd"
            Title="Agregar Retiro de Aportaciones"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
                <Show Handler="#{AddFechaRetiroTxt}.setValue(new Date()); #{AddFechaRetiroTxt}.focus(false,200);" />
                <Hide Handler="#{AgregarRetiroAportacionFormP}.getForm().reset();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="AgregarRetiroAportacionFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="200">
                    <Items>
                        <ext:Panel ID="Panel2" runat="server" Title="Aportaciones" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Items>
                                <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="FormLayout" Border="false" AnchorHorizontal="100%" >
                                    <Items>
                                        <ext:DateField runat="server" ID="AddFechaRetiroTxt" DataIndex="RETIROS_AP_FECHA" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ></ext:DateField>
                                        <ext:ComboBox  runat="server" ID="AddSociosIdTxt"  LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Código Socio" AllowBlank="false" MsgTarget="Side"
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
                                                <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide(); AgregarRetiroAportacionFormP.getForm().reset(); }" />
                                                <Select Handler="this.triggers[0].show(); PageX.addGetNombreDeSocio(Ext.getCmp('AddSociosIdTxt'), Ext.getCmp('AddNombreTxt'));" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:TextField   runat="server" ID="AddNombreTxt" DataIndex="SOCIOS_NOMBRE_COMPLETO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip11" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>

                                        <ext:Panel ID="AddAportacionesRetirosPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:FieldSet runat="server" ID="AddAportacionesFS" Title="Saldos" Padding="5" LabelWidth="200" ColumnWidth=".8" >
                                                    <Items>
                                                        <ext:NumberField runat="server" ID="AddAportacionOrdinariaSaldoTxt"  DataIndex="APORTACIONES_ORDINARIA_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Aportación Ordinaria" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip12" runat="server" Title="Saldo Aportación Ordinaria" Html="El saldo aportación ordinaria es de solo lectura." Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddAportacionExtraordinariaSaldoTxt"  DataIndex="APORTACIONES_EXTRAORDINARIA_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Aportación Extraordinaria" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip13" runat="server" Title="Saldo Aportación Extraordinaria" Html="El saldo aportación extraordinaria es de solo lectura." Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddAportacionCapRetencionSaldoTxt"  DataIndex="APORTACIONES_CAPITALIZACION_RETENCION_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Capitalización por Retención" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip14" runat="server" Title="Saldo Capitalización por Retención" Html="El saldo capitalización por retención es de solo lectura." Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddAportacionInteresesSAportacionesSaldoTxt"  DataIndex="APORTACIONES_INTERESES_S_APORTACION_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Intereses sobre Aportaciones" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip16" runat="server" Title="Saldo Intereses sobre Aportaciones" Html="El saldo intereses sobre aportaciones es de solo lectura." Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddAportacionExcedentePeriodoSaldoTxt"  DataIndex="APORTACIONES_EXCEDENTE_PERIODO_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Excedente de Período" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip17" runat="server" Title="Saldo Excedente de Período" Html="El saldo excedente de período es de solo lectura." Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>

                                                        <ext:NumberField runat="server" ID="AddAportacionTotalSaldoTxt"  DataIndex="APORTACIONES_SALDO_TOTAL" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Total" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip18" runat="server" Title="Saldo Total" Html="El saldo es de solo lectura." Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                    </Items>
                                                </ext:FieldSet>

                                                <ext:FieldSet runat="server" ID="AddRetirosFS" Title="Retiros" HideLabels="true" Padding="5" ColumnWidth=".2">
                                                    <Items>
                                                        <ext:NumberField runat="server" ID="AddRetiroAportacionOrdinariaSaldoTxt" Text="0" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" >
                                                            <Listeners>
                                                                <Change Handler="PageX.validarCantidadRetiro(#{AddAportacionOrdinariaSaldoTxt} ,#{AddRetiroAportacionOrdinariaSaldoTxt}); PageX.loadTotalRetiro();" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddRetiroAportacionExtraordinariaSaldoTxt" Text="0" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" >
                                                            <Listeners>
                                                                <Change Handler="PageX.validarCantidadRetiro(#{AddAportacionExtraordinariaSaldoTxt}, #{AddRetiroAportacionExtraordinariaSaldoTxt}); PageX.loadTotalRetiro();" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddRetiroAportacionCapRetencionSaldoTxt" Text="0" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" >
                                                            <Listeners>
                                                                <Change Handler="PageX.validarCantidadRetiro(#{AddAportacionCapRetencionSaldoTxt}, #{AddRetiroAportacionCapRetencionSaldoTxt}); PageX.loadTotalRetiro();" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddRetiroAportacionInteresesSAportacionesSaldoTxt" Text="0" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" >
                                                            <Listeners>
                                                                <Change Handler="PageX.validarCantidadRetiro(#{AddAportacionInteresesSAportacionesSaldoTxt}, #{AddRetiroAportacionInteresesSAportacionesSaldoTxt}); PageX.loadTotalRetiro();" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddRetiroAportacionExcedentePeriodoSaldoTxt" Text="0" AnchorHorizontal="90%" AllowBlank="false" MsgTarget="Side" >
                                                            <Listeners>
                                                                <Change Handler="PageX.validarCantidadRetiro(#{AddAportacionExcedentePeriodoSaldoTxt}, #{AddRetiroAportacionExcedentePeriodoSaldoTxt}); PageX.loadTotalRetiro();" />
                                                            </Listeners>
                                                        </ext:NumberField>

                                                        <ext:NumberField runat="server" ID="AddRetiroAportacionTotalSaldoTxt" Text="0" AnchorHorizontal="90%" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip19" runat="server" Title="Saldo Total" Html="El saldo es de solo lectura." Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                    </Items>
                                                </ext:FieldSet>
                                            </Items>
                                        </ext:Panel>
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

        <ext:Window ID="EditarRetiroAportacionWin"
            runat="server"
            Hidden="true"
            Icon="Coins"
            Title="Retiro de Aportaciones"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="EditarRetiroAportacionFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="200">
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Title="Aportaciones" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Items>
                                <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="FormLayout" Border="false" AnchorHorizontal="100%" >
                                    <Items>
                                        <ext:DateField runat="server" ID="EditFechaRetiroTxt" DataIndex="RETIROS_AP_FECHA" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ReadOnly="true" ></ext:DateField>
                                        <ext:TextField runat="server" ID="EditSociosIdTxt"    DataIndex="SOCIOS_ID"        LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Socio" AllowBlank="false" ReadOnly="true">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Title="Id de Socio" Html="El Id de Socio es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:TextField   runat="server" ID="EditNombreTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip2" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                            
                                        <ext:FieldSet runat="server" ID="EditAportacionesFS" Title="Retiros" Padding="5" LabelWidth="250" >
                                            <Items>
                                                <ext:NumberField runat="server" ID="EditRetiroAportacionOrdinariaSaldoTxt"  DataIndex="RETIROS_AP_ORDINARIA" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Retiro de Aportación Ordinaria" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip5" runat="server" Title="Retiro de Aportación Ordinaria" Html="El retiro de aportación ordinaria es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditRetiroAportacionExtraordinariaSaldoTxt"  DataIndex="RETIROS_AP_EXTRAORDINARIA" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Retiro de Aportación Extraordinaria" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip6" runat="server" Title="Retiro de Aportación Extraordinaria" Html="El retiro de aportación extraordinaria es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditRetiroAportacionCapRetencionSaldoTxt"  DataIndex="RETIROS_AP_CAPITALIZACION_RETENCION" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Retiro de Capitalización por Retención" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip7" runat="server" Title="Retiro de Capitalización por Retención" Html="El retiro de capitalización por retención es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditRetiroAportacionInteresesSAportacionesSaldoTxt"  DataIndex="RETIROS_AP_INTERESES_S_APORTACION" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Retiro de Intereses sobre Aportaciones" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip8" runat="server" Title="Retiro de Intereses sobre Aportaciones" Html="El retiro de intereses sobre aportaciones es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditRetiroAportacionExcedentePeriodoSaldoTxt"  DataIndex="RETIROS_AP_EXCEDENTE_PERIODO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Retiro de Excedente de Período" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip9" runat="server" Title="Retiro de Excedente de Período" Html="El retiro de excedente de período es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>

                                                <ext:NumberField runat="server" ID="EditRetiroAportacionTotalSaldoTxt"  DataIndex="RETIROS_AP_TOTAL_RETIRADO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Retiro Total" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip3" runat="server" Title="Retiro Total" Html="El Retiro es de solo lectura." Width="200" TrackMouse="true" />
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
