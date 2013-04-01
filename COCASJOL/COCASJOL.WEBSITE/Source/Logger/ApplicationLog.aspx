<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationLog.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Logger.ApplicationLog" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bitácora de Aplicación</title>
    <script type="text/javascript" src="../../resources/js/logger/applicationLog.js" ></script>
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
        
        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Bitácora de Aplicación" Icon="Basket" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="AppLogGridP" runat="server" AutoExpandColumn="message" Height="300"
                            Title="Bitácora de Aplicación" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
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
                                <ext:Store ID="AppLogSt" runat="server" OnRefreshData="stLog_OnRefreshData">
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="appdomain" />
                                                <ext:RecordField Name="aspnetcache" />
                                                <ext:RecordField Name="aspnetcontext" />
                                                <ext:RecordField Name="aspnetrequest" />
                                                <ext:RecordField Name="aspnetsession" />
                                                <ext:RecordField Name="date" Type="Date" />
                                                <ext:RecordField Name="exception" />
                                                <ext:RecordField Name="file" />
                                                <ext:RecordField Name="identity" />
                                                <ext:RecordField Name="location" />
                                                <ext:RecordField Name="level" />
                                                <ext:RecordField Name="line" />
                                                <ext:RecordField Name="logger" />
                                                <ext:RecordField Name="message" />
                                                <ext:RecordField Name="method" />
                                                <ext:RecordField Name="ndc" />
                                                <ext:RecordField Name="property" />
                                                <ext:RecordField Name="stacktrace" />
                                                <ext:RecordField Name="stacktracedetail" />
                                                <ext:RecordField Name="timestamp" />
                                                <ext:RecordField Name="thread" />
                                                <ext:RecordField Name="type" />
                                                <ext:RecordField Name="username" />
                                                <ext:RecordField Name="utcdate" Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column ColumnID="level" Header="Nivel" DataIndex="level">
                                    <Renderer Fn="fnLevel" />
                                </ext:Column>
                                <ext:DateColumn ColumnID="date" Width="150" Header="Fecha" DataIndex="date" Format="dd/MMM/yyyy hh:mm:ss a" />
                                <ext:Column ColumnID="logger" Header="Registrador" Width="150" DataIndex="logger" />
                                <ext:Column ColumnID="message" Header="Mensaje" Width="150" DataIndex="message" />
                                <ext:Column ColumnID="username" Header="Usuario" Width="150" DataIndex="username" />
                                <ext:Column ColumnID="location" Header="Ubicación" Width="150" DataIndex="location" />
                                <ext:Column ColumnID="method" Header="Método" Width="100" DataIndex="method" />
                                <ext:Column ColumnID="line" Header="Línea" Width="50" DataIndex="line" />
                                <ext:Column ColumnID="appdomain" Header="Dóminio de Aplicación" DataIndex="appdomain" Hidden="true" />
                                <ext:Column ColumnID="aspnetcache" Header="aspnetcache" DataIndex="aspnetcache" Hidden="true" />
                                <ext:Column ColumnID="aspnetcontext" Header="aspnetcontext" DataIndex="aspnetcontext"
                                    Hidden="true" />
                                <ext:Column ColumnID="aspnetrequest" Header="aspnetrequest" DataIndex="aspnetrequest"
                                    Hidden="true" />
                                <ext:Column ColumnID="aspnetsession" Header="aspnetsession" DataIndex="aspnetsession"
                                    Hidden="true" />
                                <ext:Column ColumnID="exception" Header="Excepción" DataIndex="exception" Hidden="true" />
                                <ext:Column ColumnID="file" Header="Archivo" DataIndex="file" Hidden="true" />
                                <ext:Column ColumnID="identity" Header="Identificación" DataIndex="identity" Hidden="true" />
                                <ext:Column ColumnID="ndc" Header="ndc" DataIndex="ndc" Hidden="true" />
                                <ext:Column ColumnID="property" Header="Propiedad" DataIndex="property" Hidden="true" />
                                <ext:Column ColumnID="stacktrace" Header="Pista de Pila" DataIndex="stacktrace" Hidden="true" />
                                <ext:Column ColumnID="stacktracedetail" Header="Detalle Pista de Pila" DataIndex="stacktracedetail"
                                    Hidden="true" />
                                <ext:Column ColumnID="timestamp" Header="Fecha y Hora" DataIndex="timestamp" Hidden="true" />
                                <ext:Column ColumnID="thread" Header="Hilo" DataIndex="thread" Hidden="true" />
                                <ext:Column ColumnID="type" Header="Tipo" DataIndex="type" Hidden="true" />
                                <ext:DateColumn ColumnID="utcdate" Header="Fecha UTC" DataIndex="utcdate" Hidden="true"
                                    Format="dd/MMM/yyyy hh:mm:ss am" />


                                    <ext:Column DataIndex="level" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
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
                                                        <ext:ComboBox ID="ff_level" runat="server" TriggerAction="All" Mode="Remote" Icon="Find">
                                                            <Items>
                                                                <ext:ListItem Value="" Text="*" />
                                                                <ext:ListItem Value="DEBUG" Text="DEBUG" />
                                                                <ext:ListItem Value="INFO" Text="INFO" />
                                                                <ext:ListItem Value="WARN" Text="WARN" />
                                                                <ext:ListItem Value="ERROR" Text="ERROR" />
                                                                <ext:ListItem Value="FATAL" Text="FATAL" />
                                                            </Items>
                                                            <SelectedItem Value="*" />
                                                            <DirectEvents>
                                                                <Select OnEvent="ApplyFilter">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </Select>
                                                            </DirectEvents>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:DropDownField ID="ddfFecha" AllowBlur="true" runat="server" Editable="false"
                                                             Mode="ValueText"  Icon="Find" TriggerIcon="SimpleArrowDown" CollapseMode="Default" >
                                                            <Component>
                                                                <ext:FormPanel ID="FormPanel2" runat="server" Height="100" Width="270" 
                                                                    Frame="true" LabelWidth="50" ButtonAlign="Left" BodyStyle="padding:2px 2px;">
                                                                    <Items>
                                                                        <ext:CompositeField ID="CompositeField4" runat="server" FieldLabel="De" LabelWidth="50">
                                                                            <Items>
                                                                                <ext:DateField ID="ff_date_from" Vtype="daterange" runat="server" Flex="1" Width="100" CausesValidation="false">
                                                                                    <CustomConfig>
                                                                                        <ext:ConfigItem Name="endDateField" Value="#{ff_date_to}" Mode="Value" />
                                                                                    </CustomConfig>
                                                                                    <Listeners>
                                                                                        <KeyUp Fn="calendar.validateDateRange" />
                                                                                    </Listeners>
                                                                                </ext:DateField>
                                                                                <ext:TimeField ID="ff_time_from" runat="server" Flex="1" Width="80" CausesValidation="false" />
                                                                            </Items>
                                                                        </ext:CompositeField>
                                                                        <ext:CompositeField ID="CompositeField2" runat="server" FieldLabel="Hasta" LabelWidth="50">
                                                                            <Items>
                                                                                <ext:DateField ID="ff_date_to" runat="server" Vtype="daterange" Width="100">
                                                                                    <CustomConfig>
                                                                                        <ext:ConfigItem Name="startDateField" Value="#{ff_date_from}" Mode="Value" />
                                                                                    </CustomConfig>
                                                                                    <Listeners>
                                                                                        <KeyUp Fn="calendar.validateDateRange" />
                                                                                    </Listeners>
                                                                                </ext:DateField>
                                                                                <ext:TimeField ID="ff_time_to" runat="server" Width="80" />
                                                                            </Items>
                                                                        </ext:CompositeField>
                                                                    </Items>
                                                                    <Buttons>
                                                                        <ext:Button ID="Button1" Text="Ok" Icon="Accept" runat="server">
                                                                            <Listeners>
                                                                                <Click Handler="
                                                                                    calendar.setFecha();
                                                                                    " />
                                                                            </Listeners>
                                                                            <DirectEvents>
                                                                                <Click OnEvent="ApplyFilter">
                                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                                </Click>
                                                                            </DirectEvents>
                                                                        </ext:Button>
                                                                        <ext:Button ID="Button2" Text="Clear" Icon="Cancel" runat="server">
                                                                            <Listeners>
                                                                                <Click Handler="
                                                                                    calendar.clearFecha();
                                                                                    " />
                                                                            </Listeners> 
                                                                            <DirectEvents>
                                                                                <Click OnEvent="ApplyFilter">
                                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                                </Click>
                                                                            </DirectEvents>
                                                                        </ext:Button>
                                                                    </Buttons>
                                                                </ext:FormPanel>
                                                            </Component>
                                                            <Listeners>
                                                                <Collapse Handler="calendar.setFecha();" />
                                                            </Listeners>
                                                        </ext:DropDownField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_logger" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_message" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_username" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_method" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_location" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_line" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_appdomain" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_aspnetcache" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_aspnetcontext" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_aspnetrequest" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_aspnetsession" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_exception" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_file" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_identity" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_ndc" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_property" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_stacktrace" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_stacktracedetail" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_timestamp" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_thread" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="ff_type" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <DirectEvents>
                                                                <KeyUp OnEvent="ApplyFilter" Before="return KeyUpEvent(this, e);">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </KeyUp>
                                                            </DirectEvents>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:DateField ID="ff_utcdate" runat="server" AllowBlank="true" Editable="true" Format="yyyy/MM/dd"
                                                            Icon="Find">
                                                            <DirectEvents>
                                                                <Select OnEvent="ApplyFilter">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{AppLogGridP}" />
                                                                </Select>
                                                            </DirectEvents>
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
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="AppLogSt" />
                            </BottomBar>
                            <LoadMask ShowMask="true" />
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
            Icon="Clipboard"
            Title="Bitácora de Aplicación"
            Width="800"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="EditarTipoFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Title="Bitácora de Aplicación" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Items>
                                <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="FormLayout" Border="false" AnchorHorizontal="100%">
                                    <Items>
                                        <ext:TextField ID="EditLevelTxt" runat="server" FieldLabel="Nivel" Width="100" DataIndex="level"></ext:TextField>
                                        <ext:TextArea ID="EditMessagetxt" FieldLabel="Mensaje" runat="server" DataIndex="message" Anchor="0" Height="50" ReadOnly="true" />
                                        <ext:TextField ID="EditLocation" runat="server" FieldLabel="Origen" DataIndex="location" Anchor="0" ReadOnly="true" />
                                        <ext:CompositeField ID="cfieldExcStk" runat="server" FieldLabel="Exception/Stacktrace">
                                            <Items>
                                                <ext:TextArea ID="EditExceptionTxt" runat="server" DataIndex="exception" Flex="1" Anchor="0" Height="50" ReadOnly="true" />
                                                <ext:TextArea ID="EditStacktxt" runat="server" DataIndex="stacktracedetail" Flex="1" Height="50" Anchor="0" ReadOnly="true" />
                                            </Items>
                                        </ext:CompositeField>
                                        <ext:CompositeField ID="cfieldCtxRqst" runat="server" FieldLabel="ASP Context/Request">
                                            <Items>
                                                <ext:TextArea ID="EditContextTxt" runat="server" DataIndex="aspnetcontext" Flex="1" Height="50" Anchor="0" ReadOnly="true" />
                                                <ext:TextArea ID="EditRequestTxt" runat="server" DataIndex="aspnetrequest" Flex="1" Height="50" Anchor="0" ReadOnly="true" />
                                            </Items>
                                        </ext:CompositeField>
                                        <ext:TextArea ID="EditPropertyTxt" runat="server" FieldLabel="log4Net Property" DataIndex="property" Anchor="0" Height="50" ReadOnly="true" />
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
